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
    public class ListBox : Widget
    {
        public ListBox()
            : base(tguiListBox_create())
        {
        }

        protected internal ListBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public ListBox(ListBox copy)
            : base(copy)
        {
        }

        public new ListBoxRenderer Renderer
        {
            get { return new ListBoxRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        public new ListBoxRenderer SharedRenderer
        {
            get { return new ListBoxRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        public bool AddItem(string item, string id = "")
        {
            return tguiListBox_addItem(CPointer, Util.ConvertStringForC_UTF32(item), Util.ConvertStringForC_UTF32(id));
        }

        public bool SetSelectedItem(string item)
        {
            return tguiListBox_setSelectedItem(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        public bool SetSelectedItemById(string id)
        {
            return tguiListBox_setSelectedItemById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        public bool SetSelectedItemByIndex(uint index)
        {
            return tguiListBox_setSelectedItemByIndex(CPointer, index);
        }

        public void DeselectItem()
        {
            tguiListBox_deselectItem(CPointer);
        }

        public bool RemoveItem(string item)
        {
            return tguiListBox_removeItem(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        public bool RemoveItemById(string id)
        {
            return tguiListBox_removeItemById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        public bool RemoveItemByIndex(uint index)
        {
            return tguiListBox_removeItemByIndex(CPointer, index);
        }

        public void RemoveAllItems()
        {
            tguiListBox_removeAllItems(CPointer);
        }

        public string GetItemById(string id)
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getItemById(CPointer, Util.ConvertStringForC_UTF32(id)));
        }

        public string GetSelectedItem()
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getSelectedItem(CPointer));
        }

        public string GetSelectedItemId()
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getSelectedItemId(CPointer));
        }

        public int GetSelectedItemIndex()
        {
            return tguiListBox_getSelectedItemIndex(CPointer);
        }

        public bool ChangeItem(string originalValue, string newValue)
        {
            return tguiListBox_changeItem(CPointer, Util.ConvertStringForC_UTF32(originalValue), Util.ConvertStringForC_UTF32(newValue));
        }

        public bool ChangeItemById(string id, string newValue)
        {
            return tguiListBox_changeItemById(CPointer, Util.ConvertStringForC_UTF32(id), Util.ConvertStringForC_UTF32(newValue));
        }

        public bool ChangeItemByIndex(uint index, string newValue)
        {
            return tguiListBox_changeItemByIndex(CPointer, index, Util.ConvertStringForC_UTF32(newValue));
        }

        public uint GetItemCount()
        {
            return tguiListBox_getItemCount(CPointer);
        }

        public List<string> GetItems()
        {
            unsafe
            {
                IntPtr* ItemsPtr = tguiListBox_getItems(CPointer, out uint Count);
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
                IntPtr* ItemIdsPtr = tguiListBox_getItemIds(CPointer, out uint Count);
                List<string> ItemIds = new List<string>();
                for (uint i = 0; i < Count; ++i)
                    ItemIds.Add(Util.GetStringFromC_UTF32(ItemIdsPtr[i]));

                return ItemIds;
            }
        }

        public uint ItemHeight
        {
            get { return tguiListBox_getItemHeight(CPointer); }
            set { tguiListBox_setItemHeight(CPointer, value); }
        }

        public uint TextSize
        {
            get { return tguiListBox_getTextSize(CPointer); }
            set { tguiListBox_setTextSize(CPointer, value); }
        }

        public uint MaximumItems
        {
            get { return tguiListBox_getMaximumItems(CPointer); }
            set { tguiListBox_setMaximumItems(CPointer, value); }
        }

        public bool AutoScroll
        {
            get { return tguiListBox_getAutoScroll(CPointer); }
            set { tguiListBox_setAutoScroll(CPointer, value); }
        }

        public bool Contains(string item)
        {
            return tguiListBox_contains(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        public bool ContainsId(string id)
        {
            return tguiListBox_containsId(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        protected override void InitSignals()
        {
            base.InitSignals();

            ItemSelectedCallback = new CallbackActionItemSelected(ProcessItemSelectedSignal);
            if (tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("ItemSelected"), ItemSelectedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            MousePressedCallback = new CallbackActionItemSelected(ProcessMousePressedSignal);
            if (tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("MousePressed"), MousePressedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            MouseReleasedCallback = new CallbackActionItemSelected(ProcessMouseReleasedSignal);
            if (tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("MouseReleased"), MouseReleasedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            DoubleClickedCallback = new CallbackActionItemSelected(ProcessDoubleClickedSignal);
            if (tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("DoubleClicked"), DoubleClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessItemSelectedSignal(IntPtr item, IntPtr id)
        {
            ItemSelected?.Invoke(this, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id)));
        }

        private void ProcessMousePressedSignal(IntPtr item, IntPtr id)
        {
            MousePressed?.Invoke(this, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id)));
        }

        private void ProcessMouseReleasedSignal(IntPtr item, IntPtr id)
        {
            MouseReleased?.Invoke(this, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id)));
        }

        private void ProcessDoubleClickedSignal(IntPtr item, IntPtr id)
        {
            DoubleClicked?.Invoke(this, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id)));
        }

        /// <summary>Event handler for the ItemSelected signal</summary>
        public event EventHandler<SignalArgsItem> ItemSelected = null;

        /// <summary>Event handler for the MousePressed signal</summary>
        public event EventHandler<SignalArgsItem> MousePressed = null;

        /// <summary>Event handler for the MouseReleased signal</summary>
        public event EventHandler<SignalArgsItem> MouseReleased = null;

        /// <summary>Event handler for the DoubleClicked signal</summary>
        public event EventHandler<SignalArgsItem> DoubleClicked = null;


        private CallbackActionItemSelected ItemSelectedCallback;
        private CallbackActionItemSelected MousePressedCallback;
        private CallbackActionItemSelected MouseReleasedCallback;
        private CallbackActionItemSelected DoubleClickedCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiListBox_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_addItem(IntPtr cPointer, IntPtr itemName, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_setSelectedItem(IntPtr cPointer, IntPtr itemName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_setSelectedItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_setSelectedItemByIndex(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiListBox_deselectItem(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_removeItem(IntPtr cPointer, IntPtr itemName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_removeItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_removeItemByIndex(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiListBox_removeAllItems(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiListBox_getItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiListBox_getSelectedItem(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiListBox_getSelectedItemId(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected int tguiListBox_getSelectedItemIndex(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_changeItem(IntPtr cPointer, IntPtr originalValue, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_changeItemById(IntPtr cPointer, IntPtr id, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_changeItemByIndex(IntPtr cPointer, uint index, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiListBox_getItemCount(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern protected IntPtr* tguiListBox_getItems(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern protected IntPtr* tguiListBox_getItemIds(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiListBox_setItemHeight(IntPtr cPointer, uint itemHeight);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiListBox_getItemHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiListBox_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiListBox_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiListBox_setMaximumItems(IntPtr cPointer, uint maximumItems);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiListBox_getMaximumItems(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiListBox_setAutoScroll(IntPtr cPointer, bool autoHide);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_getAutoScroll(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_contains(IntPtr cPointer, IntPtr item);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiListBox_containsId(IntPtr cPointer, IntPtr id);

        #endregion
    }
}
