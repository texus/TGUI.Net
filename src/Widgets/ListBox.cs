/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2020 Bruno Van de Velde (vdv_b@tgui.eu)
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
    /// <summary>
    /// List box widget
    /// </summary>
    public class ListBox : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ListBox()
            : base(tguiListBox_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ListBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ListBox(ListBox copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets or sets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After retrieving the renderer, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new ListBoxRenderer Renderer
        {
            get { return new ListBoxRenderer(tguiWidget_getRenderer(CPointer)); }
            set { SetRenderer(value.Data); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new ListBoxRenderer SharedRenderer
        {
            get { return new ListBoxRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        /// <param name="item">The name of the item you want to add (this is the text that will be displayed inside the list box)</param>
        /// <param name="id">Optional unique id given to this item for the purpose to later identifying this item</param>
        /// <returns>
        /// - true when the item was successfully added
        /// - false when the list box is full (you have set a maximum item limit and you are trying to add more items)
        /// - false when there is no scrollbar and you try to have more items than fit inside the list box
        /// </returns>
        public bool AddItem(string item, string id = "")
        {
            return tguiListBox_addItem(CPointer, Util.ConvertStringForC_UTF32(item), Util.ConvertStringForC_UTF32(id));
        }

        /// <summary>
        /// Selects an item in the list box
        /// </summary>
        /// <param name="item">The item you want to select</param>
        /// <returns>
        /// - true on success
        /// - false when none of the items matches the name
        /// </returns>
        /// <remarks>
        /// In case the id would not be unique, the first item with that id will be selected.
        /// </remarks>
        public bool SetSelectedItem(string item)
        {
            return tguiListBox_setSelectedItem(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        /// <summary>
        /// Selects an item in the list box
        /// </summary>
        /// <param name="id">Unique id passed to AddItem</param>
        /// <returns>
        /// - true on success
        /// - false when none of the items has the given id
        /// </returns>
        /// <remarks>
        /// In case the names are not unique, the first item with that name will be selected.
        /// </remarks>
        public bool SetSelectedItemById(string id)
        {
            return tguiListBox_setSelectedItemById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        /// <summary>
        /// Selects an item in the list box
        /// </summary>
        /// <param name="index">Index of the item in the list box</param>
        /// <returns>
        /// - true on success
        /// - false when the index was too high
        /// </returns>
        public bool SetSelectedItemByIndex(uint index)
        {
            return tguiListBox_setSelectedItemByIndex(CPointer, index);
        }

        /// <summary>
        /// Deselects the selected item
        /// </summary>
        public void DeselectItem()
        {
            tguiListBox_deselectItem(CPointer);
        }

        /// <summary>
        /// Removes the item from the list with the given name
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>
        /// - true when the item was removed
        /// - false when the name did not match any item
        /// </returns>
        /// <remarks>
        /// In case the names are not unique, only the first item with that name will be removed.
        /// </remarks>
        public bool RemoveItem(string item)
        {
            return tguiListBox_removeItem(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        /// <summary>
        /// Removes the item that were added with the given id
        /// </summary>
        /// <param name="id">Id that was given to the AddItem function</param>
        /// <returns>
        /// - true when the item was removed
        /// - false when there was no item with the given id
        /// </returns>
        /// <remarks>
        /// In case the id is not unique, only the first item with that id will be removed.
        /// </remarks>
        public bool RemoveItemById(string id)
        {
            return tguiListBox_removeItemById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        /// <summary>
        /// Removes the item from the list box
        /// </summary>
        /// <param name="index">Index of the item in the list box</param>
        /// <returns>
        /// - true when the item was removed
        /// - false when the index was too high
        /// </returns>
        public bool RemoveItemByIndex(uint index)
        {
            return tguiListBox_removeItemByIndex(CPointer, index);
        }

        /// <summary>
        /// Removes all items from the list
        /// </summary>
        public void RemoveAllItems()
        {
            tguiListBox_removeAllItems(CPointer);
        }

        /// <summary>
        /// Returns the item name of the item with the given id
        /// </summary>
        /// <param name="id">The id of the item that was given to it when it was added</param>
        /// <returns>The requested item, or an empty string when no item matches the id</returns>
        /// <remarks>
        /// In case the id is not unique, the first item with that id will be returned.
        /// </remarks>
        public string GetItemById(string id)
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getItemById(CPointer, Util.ConvertStringForC_UTF32(id)));
        }

        /// <summary>
        /// Returns the item name of the item at the given index
        /// </summary>
        /// <param name="index">The index of the item to return</param>
        /// <returns>The requested item, or an empty string when the index was too high</returns>
        public string GetItemByIndex(uint index)
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getItemByIndex(CPointer, index));
        }

        /// <summary>
        /// Returns the currently selected item
        /// </summary>
        /// <returns>
        /// The selected item.
        /// When no item was selected then this function will return an empty string.
        /// </returns>
        public string GetSelectedItem()
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getSelectedItem(CPointer));
        }

        /// <summary>
        /// Gets the id of the selected item
        /// </summary>
        /// <returns>
        /// The id of the selected item, which was the optional id passed to the AddItem function.
        /// When no item was selected then this function returns an empty string.
        /// </returns>
        public string GetSelectedItemId()
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getSelectedItemId(CPointer));
        }

        /// <summary>
        /// Gets the index of the selected item
        /// </summary>
        /// <returns>The index of the selected item, or -1 when no item was selected</returns>
        public int GetSelectedItemIndex()
        {
            return tguiListBox_getSelectedItemIndex(CPointer);
        }

        /// <summary>
        /// Changes an item with name originalValue to newValue
        /// </summary>
        /// <param name="originalValue">The name of the item which you want to change</param>
        /// <param name="newValue">The new name for that item</param>
        /// <returns>
        /// - true when the item was changed
        /// - false when none of the items had the given name
        /// </returns>
        /// <remarks>
        /// In case the names are not unique, only the first item with that name will be changed.
        /// </remarks>
        public bool ChangeItem(string originalValue, string newValue)
        {
            return tguiListBox_changeItem(CPointer, Util.ConvertStringForC_UTF32(originalValue), Util.ConvertStringForC_UTF32(newValue));
        }

        /// <summary>
        /// Changes the name of an item with the given id to newValue
        /// </summary>
        /// <param name="id">The unique id of the item which you want to change</param>
        /// <param name="newValue">The new name for that item</param>
        /// <returns>
        /// - true when the item was changed
        /// - false when none of the items had the given id
        /// </returns>
        /// <remarks>
        /// In case the id is not unique, only the first item with that id will be changed.
        /// </remarks>
        public bool ChangeItemById(string id, string newValue)
        {
            return tguiListBox_changeItemById(CPointer, Util.ConvertStringForC_UTF32(id), Util.ConvertStringForC_UTF32(newValue));
        }

        /// <summary>
        /// Changes the name of an item at the given index to newValue
        /// </summary>
        /// <param name="index">The index of the item which you want to change</param>
        /// <param name="newValue">The new name for that item</param>
        /// <returns>
        /// - true when the item was changed
        /// - false when the index was too high
        /// </returns>
        public bool ChangeItemByIndex(uint index, string newValue)
        {
            return tguiListBox_changeItemByIndex(CPointer, index, Util.ConvertStringForC_UTF32(newValue));
        }

        /// <summary>
        /// Returns the amount of items in the list box
        /// </summary>
        /// <returns>Number of items inside the list box</returns>
        public uint GetItemCount()
        {
            return tguiListBox_getItemCount(CPointer);
        }

        /// <summary>
        /// Returns a copy of the items in the list box
        /// </summary>
        /// <returns>List of items</returns>
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

        /// <summary>
        /// Returns a copy of the item ids in the list box
        /// </summary>
        /// <returns>List of item ids.</returns>
        /// <remarks>
        /// Items that were not given an id simply have an empty string as id.
        /// </remarks>
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

        /// <summary>
        /// Gets or sets the height of the items in the list box
        /// </summary>
        public uint ItemHeight
        {
            get { return tguiListBox_getItemHeight(CPointer); }
            set { tguiListBox_setItemHeight(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the character size of the items
        /// </summary>
        /// <remarks>
        /// This will not change the height that each item has.
        /// When passing 0 to this function, the text will be auto-sized to nicely fit inside this item height.
        /// </remarks>
        public uint TextSize
        {
            get { return tguiListBox_getTextSize(CPointer); }
            set { tguiListBox_setTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the maximum items inside the list box. When the maximum is 0 then the limit is disabled.
        /// </summary>
        public uint MaximumItems
        {
            get { return tguiListBox_getMaximumItems(CPointer); }
            set { tguiListBox_setMaximumItems(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the list box scrolls to the bottom when a new item is added
        /// </summary>
        /// <remarks>
        /// Auto scrolling is enabled by default.
        /// </remarks>
        public bool AutoScroll
        {
            get { return tguiListBox_getAutoScroll(CPointer); }
            set { tguiListBox_setAutoScroll(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the thumb position of the scrollbar
        /// </summary>
        public uint ScrollbarValue
        {
            get { return tguiListBox_getScrollbarValue(CPointer); }
            set { tguiListBox_setScrollbarValue(CPointer, value); }
        }

        /// <summary>
        /// Returns whether the list box contains the given item
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>Does the list box contain the item?</returns>
        public bool Contains(string item)
        {
            return tguiListBox_contains(CPointer, Util.ConvertStringForC_UTF32(item));
        }

        /// <summary>
        /// Returns whether the list box contains an item with the given id
        /// </summary>
        /// <param name="id">The id of the item to search for</param>
        /// <returns>Does the list box contain the id?</returns>
        public bool ContainsId(string id)
        {
            return tguiListBox_containsId(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            ItemSelectedCallback = new CallbackActionItemSelected((item, id) => SendSignal(myItemSelectedEventKey, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id))));
            AddInternalSignal(tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("ItemSelected"), ItemSelectedCallback));

            MousePressedCallback = new CallbackActionItemSelected((item, id) => SendSignal(myMousePressedEventKey, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id))));
            AddInternalSignal(tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("MousePressed"), MousePressedCallback));

            MouseReleasedCallback = new CallbackActionItemSelected((item, id) => SendSignal(myMouseReleasedEventKey, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id))));
            AddInternalSignal(tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("MouseReleased"), MouseReleasedCallback));

            DoubleClickedCallback = new CallbackActionItemSelected((item, id) => SendSignal(myDoubleClickedEventKey, new SignalArgsItem(Util.GetStringFromC_UTF32(item), Util.GetStringFromC_UTF32(id))));
            AddInternalSignal(tguiWidget_connectItemSelected(CPointer, Util.ConvertStringForC_ASCII("DoubleClicked"), DoubleClickedCallback));
        }

        /// <summary>Event handler for the ItemSelected signal</summary>
        public event EventHandler<SignalArgsItem> ItemSelected
        {
            add { myEventHandlerList.AddHandler(myItemSelectedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myItemSelectedEventKey, value); }
        }

        /// <summary>Event handler for the MousePressed signal</summary>
        public event EventHandler<SignalArgsItem> MousePressed
        {
            add { myEventHandlerList.AddHandler(myMousePressedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myMousePressedEventKey, value); }
        }

        /// <summary>Event handler for the MouseReleased signal</summary>
        public event EventHandler<SignalArgsItem> MouseReleased
        {
            add { myEventHandlerList.AddHandler(myMouseReleasedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myMouseReleasedEventKey, value); }
        }

        /// <summary>Event handler for the DoubleClicked signal</summary>
        public event EventHandler<SignalArgsItem> DoubleClicked
        {
            add { myEventHandlerList.AddHandler(myDoubleClickedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myDoubleClickedEventKey, value); }
        }


        private CallbackActionItemSelected ItemSelectedCallback;
        private CallbackActionItemSelected MousePressedCallback;
        private CallbackActionItemSelected MouseReleasedCallback;
        private CallbackActionItemSelected DoubleClickedCallback;

        static readonly object myItemSelectedEventKey = new object();
        static readonly object myMousePressedEventKey = new object();
        static readonly object myMouseReleasedEventKey = new object();
        static readonly object myDoubleClickedEventKey = new object();


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListBox_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_addItem(IntPtr cPointer, IntPtr itemName, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_setSelectedItem(IntPtr cPointer, IntPtr itemName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_setSelectedItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_setSelectedItemByIndex(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListBox_deselectItem(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_removeItem(IntPtr cPointer, IntPtr itemName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_removeItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_removeItemByIndex(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListBox_removeAllItems(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListBox_getItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListBox_getItemByIndex(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListBox_getSelectedItem(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListBox_getSelectedItemId(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private int tguiListBox_getSelectedItemIndex(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_changeItem(IntPtr cPointer, IntPtr originalValue, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_changeItemById(IntPtr cPointer, IntPtr id, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_changeItemByIndex(IntPtr cPointer, uint index, IntPtr newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListBox_getItemCount(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiListBox_getItems(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiListBox_getItemIds(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListBox_setItemHeight(IntPtr cPointer, uint itemHeight);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListBox_getItemHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListBox_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListBox_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListBox_setMaximumItems(IntPtr cPointer, uint maximumItems);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListBox_getMaximumItems(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListBox_setAutoScroll(IntPtr cPointer, bool autoScroll);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_getAutoScroll(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListBox_setScrollbarValue(IntPtr cPointer, uint newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListBox_getScrollbarValue(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_contains(IntPtr cPointer, IntPtr item);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListBox_containsId(IntPtr cPointer, IntPtr id);

        #endregion
    }
}
