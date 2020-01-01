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
using SFML.Graphics;

namespace TGUI
{
    /// <summary>
    /// List view widget
    /// </summary>
    public class ListView : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ListView()
            : base(tguiListView_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ListView(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ListView(ListView copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After calling this function, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new ListViewRenderer Renderer
        {
            get { return new ListViewRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new ListViewRenderer SharedRenderer
        {
            get { return new ListViewRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Adds a column
        /// </summary>
        /// <param name="text">The caption of the new column</param>
        /// <param name="width">Width of the column. Set width to 0 to make it depend on the width of the column caption.</param>
        /// <param name="columnAlignment">The text alignment for all texts in the column</param>
        /// <returns>Index of the item that was just added</returns>
        public uint AddColumn(string text, float width, HorizontalAlignment columnAlignment)
        {
            return tguiListView_addColumn(CPointer, Util.ConvertStringForC_UTF32(text), width, columnAlignment);
        }

        /// <summary>
        /// Changes the text of a column
        /// </summary>
        /// <param name="index">Index of the column to change</param>
        /// <param name="text">Caption of the column</param>
        public void SetColumnText(uint index, string text)
        {
            tguiListView_setColumnText(CPointer, index, Util.ConvertStringForC_UTF32(text));
        }

        /// <summary>
        /// Returns the text of a column
        /// </summary>
        /// <param name="index">Index of the column</param>
        /// <returns>Caption of the column</returns>
        public string GetColumnText(uint index)
        {
            return Util.GetStringFromC_UTF32(tguiListView_getColumnText(CPointer, index));
        }

        /// <summary>
        /// Changes the width of a column
        /// </summary>
        /// <param name="index">Index of the column to change</param>
        /// <param name="width">Width of the column. Set width to 0 to make it depend on the width of the column caption.</param>
        public void SetColumnText(uint index, float width)
        {
            tguiListView_setColumnWidth(CPointer, index, width);
        }

        /// <summary>
        /// Returns the width of a column
        /// </summary>
        /// <param name="index">Index of the column</param>
        /// <returns>Width of the column</returns>
        public float GetColumnWidth(uint index)
        {
            return tguiListView_getColumnWidth(CPointer, index);
        }

        /// <summary>
        /// Changes the text alignment within a column
        /// </summary>
        /// <param name="index">Index of the column to change</param>
        /// <param name="columnAlignment">The text alignment for all texts in the column</param>
        public void SetColumnAlignment(uint index, HorizontalAlignment columnAlignment)
        {
            tguiListView_setColumnAlignment(CPointer, index, columnAlignment);
        }

        /// <summary>
        /// Returns the current text alignment within a column
        /// </summary>
        /// <param name="index">Index of the column</param>
        /// <returns>Text alignment for all texts in the column</returns>
        public HorizontalAlignment GetColumnAlignment(uint index)
        {
            return tguiListView_getColumnAlignment(CPointer, index);
        }

        /// <summary>
        /// Removes all columns
        /// </summary>
        public void RemoveAllColumns()
        {
            tguiListView_removeAllColumns(CPointer);
        }

        /// <summary>
        /// Returns the amount of columns in the list view
        /// </summary>
        /// <returns>Number of columns</returns>
        public uint GetColumnCount()
        {
            return tguiListView_getColumnCount(CPointer);
        }

        /// <summary>
        /// Gets or sets the height of the header row
        /// </summary>
        public float HeaderHeight
        {
            get { return tguiListView_getHeaderHeight(CPointer); }
            set { tguiListView_setHeaderHeight(CPointer, value); }
        }

        /// <summary>
        /// Gets the total height of the header (HeaderHeight + HeaderSeparatorHeight) or 0 if no header row is shown
        /// </summary>
        public float CurrentHeaderHeight
        {
            get { return tguiListView_getCurrentHeaderHeight(CPointer); }
        }

        /// <summary>
        /// Gets or sets whether the header is shown
        /// </summary>
        public bool HeaderVisible
        {
            get { return tguiListView_getHeaderVisible(CPointer); }
            set { tguiListView_setHeaderVisible(CPointer, value); }
        }

        /// <summary>
        /// Adds an item to the list
        /// </summary>
        /// <param name="text">The caption of the item you want to add</param>
        /// <returns>Index of the item that was just added</returns>
        public uint AddItem(string text)
        {
            return tguiListView_addItem(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        /// <summary>
        /// Adds an item with values for multiple columns to the list
        /// </summary>
        /// <param name="item">Texts for each column</param>
        /// <returns>Index of the item that was just added</returns>
        public uint AddItem(List<string> item)
        {
            IntPtr[] itemForC = new IntPtr[item.Count];
            for (int i = 0; i < item.Count; ++i)
                itemForC[i] = Util.ConvertStringForC_UTF32(item[i]);

            return tguiListView_addItemRow(CPointer, itemForC, (uint)itemForC.Length);
        }

        /// <summary>
        /// Changes an item with values for multiple columns to the list
        /// </summary>
        /// <param name="index">Index of the item to update</param>
        /// <param name="item">Texts for each column</param>
        /// <returns>True when the item was updated, false when the index was too high</returns>
        public bool ChangeItem(uint index, List<string> item)
        {
            IntPtr[] itemForC = new IntPtr[item.Count];
            for (int i = 0; i < item.Count; ++i)
                itemForC[i] = Util.ConvertStringForC_UTF32(item[i]);

            return tguiListView_changeItem(CPointer, index, itemForC, (uint)itemForC.Length);
        }

        /// <summary>
        /// Changes the caption of a single value in the item
        /// </summary>
        /// <param name="index">Index of the item to update</param>
        /// <param name="column">Index of the column to change</param>
        /// <param name="item">Texts for the given column for the given item</param>
        /// <returns>True when the item was updated, false when the index was too high</returns>
        public bool ChangeSubItem(uint index, uint column, string item)
        {
            return tguiListView_changeSubItem(CPointer, index, column, Util.ConvertStringForC_UTF32(item));
        }

        /// <summary>
        /// Removes the item from the list view
        /// </summary>
        /// <param name="index">Index of the item in the list view</param>
        /// <returns>True when the item was removed, false when the index was too high</returns>
        public bool RemoveItem(uint index)
        {
            return tguiListView_removeItem(CPointer, index);
        }

        /// <summary>
        /// Removes all items from the list
        /// </summary>
        public void RemoveAllItems()
        {
            tguiListView_removeAllItems(CPointer);
        }

        /// <summary>
        /// Selects an item in the list view
        /// </summary>
        /// <param name="index">Index of the item in the list view</param>
        public void SetSelectedItem(uint index)
        {
            tguiListView_setSelectedItem(CPointer, index);
        }

        /// <summary>
        /// Gets the index of the selected item
        /// </summary>
        /// <returns>The index of the selected item, or -1 when no item was selected</returns>
        public int GetSelectedItemIndex()
        {
            return tguiListView_getSelectedItemIndex(CPointer);
        }

        /// <summary>
        /// Deselects the selected item
        /// </summary>
        public void DeselectItem()
        {
            tguiListView_deselectItem(CPointer);
        }

        /// <summary>
        /// Sets a small icon in front of the item
        /// </summary>
        /// <param name="index">Index of the item</param>
        /// <param name="icon">Texture of the item icon</param>
        public void SetItemIcon(uint index, Texture icon)
        {
            tguiListView_setItemIcon(CPointer, index, icon.CPointer);
        }

        /// <summary>
        /// Returns the amount of items in the list view
        /// </summary>
        /// <returns>Number of items inside the list view</returns>
        public uint GetItemCount()
        {
            return tguiListView_getItemCount(CPointer);
        }

        /// <summary>
        /// Retrieves an item in the list
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <returns>Text of the item or an empty string when the index was higher than the amount of items</returns>
        public string GetItem(uint index)
        {
            return Util.GetStringFromC_UTF32(tguiListView_getItem(CPointer, index));
        }

        /// <summary>
        /// Retrieves the values of all columns for an item in the list
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <returns>Texts of the item for each column or an list of empty strings when the index was too high</returns>
        /// <remarks>
        /// The returned list has the same length as the amount of columns.
        /// </remarks>
        public List<string> GetItemRow(uint index)
        {
            unsafe
            {
                IntPtr* TextsPtr = tguiListView_getItemRow(CPointer, index, out uint Count);
                List<string> Row = new List<string>();
                for (uint i = 0; i < Count; ++i)
                    Row.Add(Util.GetStringFromC_UTF32(TextsPtr[i]));

                return Row;
            }
        }

        /// <summary>
        /// Returns a list of the texts in the first column for all items in the list view
        /// </summary>
        /// <returns>Texts of the first column of items</returns>
        public List<string> GetItems()
        {
            unsafe
            {
                IntPtr* ItemsPtr = tguiListView_getItems(CPointer, out uint Count);
                List<string> Items = new List<string>();
                for (uint i = 0; i < Count; ++i)
                    Items.Add(Util.GetStringFromC_UTF32(ItemsPtr[i]));

                return Items;
            }
        }

        /// <summary>
        /// Gets or sets the height of the items in the list view
        /// </summary>
        public uint ItemHeight
        {
            get { return tguiListView_getItemHeight(CPointer); }
            set { tguiListView_setItemHeight(CPointer, value); }
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
            get { return tguiListView_getTextSize(CPointer); }
            set { tguiListView_setTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the character size of the header captions
        /// </summary>
        /// <remarks>
        /// By default, header text size is the same as the text size of the items.
        /// </remarks>
        public uint HeaderTextSize
        {
            get { return tguiListView_getHeaderTextSize(CPointer); }
            set { tguiListView_setHeaderTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the width of the column separator
        /// </summary>
        public uint SeparatorWidth
        {
            get { return tguiListView_getSeparatorWidth(CPointer); }
            set { tguiListView_setSeparatorWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the height of the separator between the header and the items
        /// </summary>
        public uint HeaderSeparatorHeight
        {
            get { return tguiListView_getHeaderSeparatorHeight(CPointer); }
            set { tguiListView_setHeaderSeparatorHeight(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the width of the grid lines
        /// </summary>
        public uint GridLinesWidth
        {
            get { return tguiListView_getGridLinesWidth(CPointer); }
            set { tguiListView_setGridLinesWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the list view scrolls to the bottom when a new item is added
        /// </summary>
        /// <remarks>
        /// Auto scrolling is enabled by default.
        /// </remarks>
        public bool AutoScroll
        {
            get { return tguiListView_getAutoScroll(CPointer); }
            set { tguiListView_setAutoScroll(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether lines are drawn between items
        /// </summary>
        public bool ShowVerticalGridLines
        {
            get { return tguiListView_getShowVerticalGridLines(CPointer); }
            set { tguiListView_setShowVerticalGridLines(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether lines are drawn between items
        /// </summary>
        public bool ShowHorizontalGridLines
        {
            get { return tguiListView_getShowVerticalGridLines(CPointer); }
            set { tguiListView_setShowVerticalGridLines(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the last column is expanded to fill the list view (if all columns fit inside the list view)
        /// </summary>
        public bool ExpandLastColumn
        {
            get { return tguiListView_getExpandLastColumn(CPointer); }
            set { tguiListView_setExpandLastColumn(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets when the vertical scrollbar should be displayed
        /// </summary>
        public Scrollbar.Policy VerticalScrollbarPolicy
        {
            get { return tguiListView_getVerticalScrollbarPolicy(CPointer); }
            set { tguiListView_setVerticalScrollbarPolicy(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets when the horizontal scrollbar should be displayed
        /// </summary>
        public Scrollbar.Policy HorizontalScrollbarPolicy
        {
            get { return tguiListView_getHorizontalScrollbarPolicy(CPointer); }
            set { tguiListView_setHorizontalScrollbarPolicy(CPointer, value); }
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            ItemSelectedCallback = new CallbackActionInt(ProcessItemSelectedSignal);
            if (tguiWidget_connectInt(CPointer, Util.ConvertStringForC_ASCII("ItemSelected"), ItemSelectedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            DoubleClickedCallback = new CallbackActionInt(ProcessDoubleClickedSignal);
            if (tguiWidget_connectInt(CPointer, Util.ConvertStringForC_ASCII("DoubleClicked"), DoubleClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            RightClickedCallback = new CallbackActionInt(ProcessRightClickedSignal);
            if (tguiWidget_connectInt(CPointer, Util.ConvertStringForC_ASCII("RightClicked"), RightClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessItemSelectedSignal(int index)
        {
            ItemSelected?.Invoke(this, new SignalArgsInt(index));
        }

        private void ProcessDoubleClickedSignal(int index)
        {
            DoubleClicked?.Invoke(this, new SignalArgsInt(index));
        }

        private void ProcessRightClickedSignal(int index)
        {
            RightClicked?.Invoke(this, new SignalArgsInt(index));
        }

        /// <summary>Event handler for the ItemSelected signal</summary>
        public event EventHandler<SignalArgsInt> ItemSelected = null;

        /// <summary>Event handler for the DoubleClicked signal</summary>
        public event EventHandler<SignalArgsInt> DoubleClicked = null;

        /// <summary>Event handler for the RightClicked signal</summary>
        public event EventHandler<SignalArgsInt> RightClicked = null;


        private CallbackActionInt ItemSelectedCallback;
        private CallbackActionInt DoubleClickedCallback;
        private CallbackActionInt RightClickedCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListView_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_addColumn(IntPtr cPointer, IntPtr text, float width, HorizontalAlignment columnAlignment);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setColumnText(IntPtr cPointer, uint index, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListView_getColumnText(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setColumnWidth(IntPtr cPointer, uint index, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiListView_getColumnWidth(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setColumnAlignment(IntPtr cPointer, uint index, HorizontalAlignment columnAlignment);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private HorizontalAlignment tguiListView_getColumnAlignment(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_removeAllColumns(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_getColumnCount(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setHeaderHeight(IntPtr cPointer, float height);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiListView_getHeaderHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiListView_getCurrentHeaderHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setHeaderVisible(IntPtr cPointer, bool showHeader);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListView_getHeaderVisible(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_addItem(IntPtr cPointer, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_addItemRow(IntPtr cPointer, IntPtr[] item, uint itemLength);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListView_changeItem(IntPtr cPointer, uint index, IntPtr[] item, uint itemLength);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListView_changeSubItem(IntPtr cPointer, uint index, uint column, IntPtr item);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListView_removeItem(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_removeAllItems(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setSelectedItem(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private int tguiListView_getSelectedItemIndex(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_deselectItem(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setItemIcon(IntPtr cPointer, uint index, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_getItemCount(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListView_getItem(IntPtr cPointer, uint index);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiListView_getItemRow(IntPtr cPointer, uint index, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiListView_getItems(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setItemHeight(IntPtr cPointer, uint itemHeight);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_getItemHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setHeaderTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_getHeaderTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setSeparatorWidth(IntPtr cPointer, uint width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_getSeparatorWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setHeaderSeparatorHeight(IntPtr cPointer, uint height);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_getHeaderSeparatorHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setGridLinesWidth(IntPtr cPointer, uint width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiListView_getGridLinesWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setAutoScroll(IntPtr cPointer, bool autoScroll);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListView_getAutoScroll(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setShowVerticalGridLines(IntPtr cPointer, bool showGridLines);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListView_getShowVerticalGridLines(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setShowHorizontalGridLines(IntPtr cPointer, bool showGridLines);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListView_getShowHorizontalGridLines(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setExpandLastColumn(IntPtr cPointer, bool expand);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiListView_getExpandLastColumn(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setVerticalScrollbarPolicy(IntPtr cPointer, Scrollbar.Policy policy);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Scrollbar.Policy tguiListView_getVerticalScrollbarPolicy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListView_setHorizontalScrollbarPolicy(IntPtr cPointer, Scrollbar.Policy policy);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Scrollbar.Policy tguiListView_getHorizontalScrollbarPolicy(IntPtr cPointer);

        #endregion
    }
}
