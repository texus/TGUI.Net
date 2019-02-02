/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2016 Bruno Van de Velde (vdv_b@tgui.eu)
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented;
//    you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment
//    in the product documentation would be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such,
//    and must not be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace TGUI
{
    public class ComboBox : Widget
    {
        public enum Direction
        {
            Down,
            Up
        }


        public ComboBox()
            : base(tguiComboBox_create())
        {
        }

        protected internal ComboBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public ComboBox(ComboBox copy)
            : base(copy)
        {
        }

        public new ComboBoxRenderer Renderer
        {
            get { return new ComboBoxRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        public new ComboBoxRenderer SharedRenderer
        {
            get { return new ComboBoxRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        public uint ItemsToDisplay
        {
            get { return tguiComboBox_getItemsToDisplay(CPointer); }
            set { tguiComboBox_setItemsToDisplay(CPointer, value); }
        }

        public bool AddItem(string item, string id = "")
        {
            return tguiComboBox_addItem(CPointer, Util.ConvertStringForC_UTF32(item), Util.ConvertStringForC_UTF32(id));
        }

        public bool SetSelectedItem(string item)
        {
            return tguiComboBox_setSelectedItem(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        public bool SetSelectedItemById(string id)
        {
            return tguiComboBox_setSelectedItemById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        public bool SetSelectedItemByIndex(uint index)
        {
            return tguiComboBox_setSelectedItemByIndex(CPointer, index);
        }

        public void DeselectItem()
        {
            tguiComboBox_deselectItem(CPointer);
        }

        public bool RemoveItem(string item)
        {
            return tguiComboBox_removeItem(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        public bool RemoveItemById(string id)
        {
            return tguiComboBox_removeItemById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        public bool RemoveItemByIndex(uint index)
        {
            return tguiComboBox_removeItemByIndex(CPointer, index);
        }

        public void RemoveAllItems()
        {
            tguiComboBox_removeAllItems(CPointer);
        }

        public string GetItemById(string id)
        {
            return Util.GetStringFromC_UTF32(tguiComboBox_getItemById(CPointer, Util.ConvertStringForC_UTF32(id)));
        }

        public string GetSelectedItem()
        {
            return Util.GetStringFromC_UTF32(tguiComboBox_getSelectedItem(CPointer));
        }

        public string GetSelectedItemId()
        {
            return Util.GetStringFromC_UTF32(tguiComboBox_getSelectedItemId(CPointer));
        }

        public int GetSelectedItemIndex()
        {
            return tguiComboBox_getSelectedItemIndex(CPointer);
        }

        public bool ChangeItem(string originalValue, string newValue)
        {
            return tguiComboBox_changeItem(CPointer, Util.ConvertStringForC_UTF32(originalValue), Util.ConvertStringForC_UTF32(newValue));
        }

        public bool ChangeItemById(string id, string newValue)
        {
            return tguiComboBox_changeItemById(CPointer, Util.ConvertStringForC_UTF32(id), Util.ConvertStringForC_UTF32(newValue));
        }

        public bool ChangeItemByIndex(uint index, string newValue)
        {
            return tguiComboBox_changeItemByIndex(CPointer, index, Util.ConvertStringForC_UTF32(newValue));
        }

        public uint GetItemCount()
        {
            return tguiComboBox_getItemCount(CPointer);
        }

        public List<string> GetItems()
        {
            unsafe
            {
                IntPtr* ItemsPtr = tguiComboBox_getItems(CPointer, out uint Count);
                List<string> Items = new List<string>();
                for (uint i = 0; i < Count; ++i)
                    Items.Add(Util.GetStringFromC_UTF32(ItemsPtr[i]));

                return Items;
            }
        }

        public List<string> GetItemIds()
        {
            unsafe
            {
                IntPtr* ItemIdsPtr = tguiComboBox_getItemIds(CPointer, out uint Count);
                List<string> ItemIds = new List<string>();
                for (uint i = 0; i < Count; ++i)
                    ItemIds.Add(Util.GetStringFromC_UTF32(ItemIdsPtr[i]));

                return ItemIds;
            }
        }

        public uint MaximumItems
        {
            get { return tguiComboBox_getMaximumItems(CPointer); }
            set { tguiComboBox_setMaximumItems(CPointer, value); }
        }

        public uint TextSize
        {
            get { return tguiComboBox_getTextSize(CPointer); }
            set { tguiComboBox_setTextSize(CPointer, value); }
        }

        public Direction ExpandDirection
        {
            get { return tguiComboBox_getExpandDirection(CPointer); }
            set { tguiComboBox_setExpandDirection(CPointer, value); }
        }

        public bool Contains(string item)
        {
            return tguiComboBox_contains(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        public bool ContainsId(string id)
        {
            return tguiComboBox_containsId(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        protected override void InitSignals()
        {
            base.InitSignals();

            ItemSelectedCallback = new CallbackActionItemSelected(ProcessItemSelectedSignal);
            if (tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("ItemSelected"), ItemSelectedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessItemSelectedSignal(IntPtr item, IntPtr id)
        {
            ItemSelected?.Invoke(this, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id)));
        }

        /// <summary>Event handler for the ItemSelected signal</summary>
        public event EventHandler<SignalArgsItem> ItemSelected = null;

        private CallbackActionItemSelected ItemSelectedCallback;

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiComboBox_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiComboBox_setItemsToDisplay(IntPtr cPointer, uint itemsToDisplay);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiComboBox_getItemsToDisplay(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_addItem(IntPtr cPointer, IntPtr itemName, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_setSelectedItem(IntPtr cPointer, IntPtr itemName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_setSelectedItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_setSelectedItemByIndex(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiComboBox_deselectItem(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_removeItem(IntPtr cPointer, IntPtr itemName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_removeItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_removeItemByIndex(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiComboBox_removeAllItems(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiComboBox_getItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiComboBox_getSelectedItem(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiComboBox_getSelectedItemId(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private int tguiComboBox_getSelectedItemIndex(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_changeItem(IntPtr cPointer, IntPtr originalValue, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_changeItemById(IntPtr cPointer, IntPtr id, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_changeItemByIndex(IntPtr cPointer, uint index, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiComboBox_getItemCount(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiComboBox_getItems(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiComboBox_getItemIds(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiComboBox_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiComboBox_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiComboBox_setMaximumItems(IntPtr cPointer, uint maximumItems);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiComboBox_getMaximumItems(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiComboBox_setExpandDirection(IntPtr cPointer, Direction expandDirection);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Direction tguiComboBox_getExpandDirection(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_contains(IntPtr cPointer, IntPtr item);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiComboBox_containsId(IntPtr cPointer, IntPtr id);

        #endregion
    }
}
