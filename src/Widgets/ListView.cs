// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// ListView widget
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

        public void Sort(int columnIndex, Func<string, string, bool> compareFunc)
        {
            UnmanagedCallbackSort func = (IntPtr a, IntPtr b) => compareFunc(Util.GetStringFromC_UTF32(a), Util.GetStringFromC_UTF32(b)) ? (byte)1 : (byte)0;
            tguiListView_sort(CPointer, (UIntPtr)columnIndex, func);
        }

        private delegate byte UnmanagedCallbackSort(IntPtr a, IntPtr b);

        public ScrollbarAccessor VerticalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getVerticalScrollbar(CPointer));
        public ScrollbarAccessor HorizontalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getHorizontalScrollbar(CPointer));

        public new ListViewRenderer Renderer
        {
            get => new ListViewRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new ListViewRenderer SharedRenderer => new ListViewRenderer(tguiWidget_getSharedRenderer(CPointer));

        public int AddColumn(string text, float width = 0, HorizontalAlignment columnAlignment = HorizontalAlignment.Left)
        {
            return (int)tguiListView_addColumn(CPointer, Util.ConvertStringForC_UTF32(text), width, columnAlignment);
        }

        public void SetColumnText(int index, string text)
        {
            tguiListView_setColumnText(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(text));
        }

        public string GetColumnText(int index)
        {
            return Util.GetStringFromC_UTF32(tguiListView_getColumnText(CPointer, (UIntPtr)index));
        }

        public void SetColumnWidth(int index, float width)
        {
            tguiListView_setColumnWidth(CPointer, (UIntPtr)index, width);
        }

        public float GetColumnWidth(int index)
        {
            return tguiListView_getColumnWidth(CPointer, (UIntPtr)index);
        }

        public float GetColumnDesignWidth(int index)
        {
            return tguiListView_getColumnDesignWidth(CPointer, (UIntPtr)index);
        }

        public void SetColumnAlignment(int index, HorizontalAlignment columnAlignment)
        {
            tguiListView_setColumnAlignment(CPointer, (UIntPtr)index, columnAlignment);
        }

        public HorizontalAlignment GetColumnAlignment(int index)
        {
            return tguiListView_getColumnAlignment(CPointer, (UIntPtr)index);
        }

        public void SetColumnAutoResize(int index, bool autoResize)
        {
            tguiListView_setColumnAutoResize(CPointer, (UIntPtr)index, autoResize ? (byte)1 : (byte)0);
        }

        public bool GetColumnAutoResize(int index)
        {
            return tguiListView_getColumnAutoResize(CPointer, (UIntPtr)index) != 0;
        }

        public void SetColumnExpanded(int index, bool expand)
        {
            tguiListView_setColumnExpanded(CPointer, (UIntPtr)index, expand ? (byte)1 : (byte)0);
        }

        public bool GetColumnExpanded(int index)
        {
            return tguiListView_getColumnExpanded(CPointer, (UIntPtr)index) != 0;
        }

        public void RemoveAllColumns()
        {
            tguiListView_removeAllColumns(CPointer);
        }

        public int GetColumnCount()
        {
            return (int)tguiListView_getColumnCount(CPointer);
        }

        public bool HeaderVisible
        {
            get => tguiListView_getHeaderVisible(CPointer) != 0;
            set => tguiListView_setHeaderVisible(CPointer, value ? (byte)1 : (byte)0);
        }

        public float HeaderHeight
        {
            get => tguiListView_getHeaderHeight(CPointer);
            set => tguiListView_setHeaderHeight(CPointer, value);
        }

        public float GetCurrentHeaderHeight()
        {
            return tguiListView_getCurrentHeaderHeight(CPointer);
        }

        public int AddItem(string text)
        {
            return (int)tguiListView_addItem(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        public int AddItem(ReadOnlySpan<string> item)
        {
            IntPtr[] itemForC = new IntPtr[item.Length];
            for (int i = 0; i < item.Length; ++i)
                itemForC[i] = Util.ConvertStringForC_UTF32(item[i]);

            return (int)tguiListView_addItemRow(CPointer, itemForC, (UIntPtr)itemForC.Length);
        }

        public void InsertItem(int index, string text)
        {
            tguiListView_insertItem(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(text));
        }

        public void InsertItem(int index, ReadOnlySpan<string> item)
        {
            IntPtr[] itemForC = new IntPtr[item.Length];
            for (int i = 0; i < item.Length; ++i)
                itemForC[i] = Util.ConvertStringForC_UTF32(item[i]);

            tguiListView_insertItemRow(CPointer, (UIntPtr)index, itemForC, (UIntPtr)itemForC.Length);
        }

        public bool ChangeItem(int index, ReadOnlySpan<string> item)
        {
            IntPtr[] itemForC = new IntPtr[item.Length];
            for (int i = 0; i < item.Length; ++i)
                itemForC[i] = Util.ConvertStringForC_UTF32(item[i]);

            return tguiListView_changeItem(CPointer, (UIntPtr)index, itemForC, (UIntPtr)itemForC.Length) != 0;
        }

        public bool ChangeSubItem(int index, int column, string text)
        {
            return tguiListView_changeSubItem(CPointer, (UIntPtr)index, (UIntPtr)column, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool RemoveItem(int index)
        {
            return tguiListView_removeItem(CPointer, (UIntPtr)index) != 0;
        }

        public void RemoveAllItems()
        {
            tguiListView_removeAllItems(CPointer);
        }

        public void SetSelectedItem(int index)
        {
            tguiListView_setSelectedItem(CPointer, (UIntPtr)index);
        }

        public void SetSelectedItems(HashSet<int> indices)
        {
            UIntPtr[] indicesArray = Array.ConvertAll(indices.ToArray(), val => (UIntPtr)val);

            tguiListView_setSelectedItems(CPointer, indicesArray, (UIntPtr)indicesArray.Length);
        }

        public int GetSelectedItemIndex()
        {
            return tguiListView_getSelectedItemIndex(CPointer);
        }

        public int GetHoveredItemIndex()
        {
            return tguiListView_getHoveredItemIndex(CPointer);
        }

        public HashSet<int> GetSelectedItemIndices()
        {
            unsafe
            {
                UIntPtr* returnIntsC = tguiListView_getSelectedItemIndices(CPointer, out UIntPtr returnCount);
                HashSet<int> returnInts = new HashSet<int>();
                for (int i = 0; i < (int)returnCount; ++i)
                    returnInts.Add((int)returnIntsC[i]);

                return returnInts;
            }
        }

        public void DeselectItems()
        {
            tguiListView_deselectItems(CPointer);
        }

        public bool MultiSelect
        {
            get => tguiListView_getMultiSelect(CPointer) != 0;
            set => tguiListView_setMultiSelect(CPointer, value ? (byte)1 : (byte)0);
        }

        public void SetItemData(int index, string data)
        {
            tguiListView_setItemData(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(data));
        }

        public string GetItemData(int index)
        {
            return Util.GetStringFromC_UTF32(tguiListView_getItemData(CPointer, (UIntPtr)index));
        }

        public void SetItemIcon(int index, Texture texture)
        {
            tguiListView_setItemIcon(CPointer, (UIntPtr)index, texture.CPointer);
        }

        public int GetItemCount()
        {
            return (int)tguiListView_getItemCount(CPointer);
        }

        public string GetItem(int index)
        {
            return Util.GetStringFromC_UTF32(tguiListView_getItem(CPointer, (UIntPtr)index));
        }

        public string GetItemCell(int rowIndex, int columnIndex)
        {
            return Util.GetStringFromC_UTF32(tguiListView_getItemCell(CPointer, (UIntPtr)rowIndex, (UIntPtr)columnIndex));
        }

        public IReadOnlyList<string> GetItemRow(int index)
        {
            unsafe
            {
                IntPtr* returnStringsC = tguiListView_getItemRow(CPointer, (UIntPtr)index, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public IReadOnlyList<string> GetItems()
        {
            unsafe
            {
                IntPtr* returnStringsC = tguiListView_getItems(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public int ItemHeight
        {
            get => (int)tguiListView_getItemHeight(CPointer);
            set => tguiListView_setItemHeight(CPointer, (uint)value);
        }

        public int HeaderTextSize
        {
            get => (int)tguiListView_getHeaderTextSize(CPointer);
            set => tguiListView_setHeaderTextSize(CPointer, (uint)value);
        }

        public int SeparatorWidth
        {
            get => (int)tguiListView_getSeparatorWidth(CPointer);
            set => tguiListView_setSeparatorWidth(CPointer, (uint)value);
        }

        public int HeaderSeparatorHeight
        {
            get => (int)tguiListView_getHeaderSeparatorHeight(CPointer);
            set => tguiListView_setHeaderSeparatorHeight(CPointer, (uint)value);
        }

        public int GridLinesWidth
        {
            get => (int)tguiListView_getGridLinesWidth(CPointer);
            set => tguiListView_setGridLinesWidth(CPointer, (uint)value);
        }

        public bool AutoScroll
        {
            get => tguiListView_getAutoScroll(CPointer) != 0;
            set => tguiListView_setAutoScroll(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool ShowVerticalGridLines
        {
            get => tguiListView_getShowVerticalGridLines(CPointer) != 0;
            set => tguiListView_setShowVerticalGridLines(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool ShowHorizontalGridLines
        {
            get => tguiListView_getShowHorizontalGridLines(CPointer) != 0;
            set => tguiListView_setShowHorizontalGridLines(CPointer, value ? (byte)1 : (byte)0);
        }

        public Vector2f FixedIconSize
        {
            get => tguiListView_getFixedIconSize(CPointer);
            set => tguiListView_setFixedIconSize(CPointer, value);
        }

        public bool ResizableColumns
        {
            get => tguiListView_getResizableColumns(CPointer) != 0;
            set => tguiListView_setResizableColumns(CPointer, value ? (byte)1 : (byte)0);
        }

        public class ItemSelectEventArgs : EventArgs
        {
            public ItemSelectEventArgs(int index)
            {
                Index = index;
            }
            public int Index { get; }
        }
        public event EventHandler<ItemSelectEventArgs> OnItemSelect
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackInt func = (int val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ItemSelectEventArgs(val));
                };
                uint id = tguiWidget_signalIntConnect(CPointer, Util.ConvertStringForC_UTF32("ItemSelected"), func);
                ConnectEventHandler(id, "ItemSelected", value, func);
            }
            remove
            {
                DisconnectEventHandler("ItemSelected", value);
            }
        }

        public class DoubleClickEventArgs : EventArgs
        {
            public DoubleClickEventArgs(int index)
            {
                Index = index;
            }
            public int Index { get; }
        }
        public event EventHandler<DoubleClickEventArgs> OnDoubleClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackInt func = (int val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new DoubleClickEventArgs(val));
                };
                uint id = tguiWidget_signalIntConnect(CPointer, Util.ConvertStringForC_UTF32("DoubleClicked"), func);
                ConnectEventHandler(id, "DoubleClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("DoubleClicked", value);
            }
        }

        public class RightClickEventArgs : EventArgs
        {
            public RightClickEventArgs(int index)
            {
                Index = index;
            }
            public int Index { get; }
        }
        public event EventHandler<RightClickEventArgs> OnRightClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackInt func = (int val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new RightClickEventArgs(val));
                };
                uint id = tguiWidget_signalIntConnect(CPointer, Util.ConvertStringForC_UTF32("RightClicked"), func);
                ConnectEventHandler(id, "RightClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("RightClicked", value);
            }
        }

        public class HeaderClickEventArgs : EventArgs
        {
            public HeaderClickEventArgs(int index)
            {
                Index = index;
            }
            public int Index { get; }
        }
        public event EventHandler<HeaderClickEventArgs> OnHeaderClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackInt func = (int val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new HeaderClickEventArgs(val));
                };
                uint id = tguiWidget_signalIntConnect(CPointer, Util.ConvertStringForC_UTF32("HeaderClicked"), func);
                ConnectEventHandler(id, "HeaderClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("HeaderClicked", value);
            }
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_sort(IntPtr cPointer, UIntPtr index, UnmanagedCallbackSort func);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListView_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiDualScrollbarChildInterface_getVerticalScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiDualScrollbarChildInterface_getHorizontalScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiListView_addColumn(IntPtr cPointer, IntPtr text, float width, HorizontalAlignment columnAlignment);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setColumnText(IntPtr cPointer, UIntPtr index, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListView_getColumnText(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setColumnWidth(IntPtr cPointer, UIntPtr index, float width);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiListView_getColumnWidth(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiListView_getColumnDesignWidth(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setColumnAlignment(IntPtr cPointer, UIntPtr index, HorizontalAlignment columnAlignment);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern HorizontalAlignment tguiListView_getColumnAlignment(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setColumnAutoResize(IntPtr cPointer, UIntPtr index, byte autoResize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_getColumnAutoResize(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setColumnExpanded(IntPtr cPointer, UIntPtr index, byte expand);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_getColumnExpanded(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_removeAllColumns(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiListView_getColumnCount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_getHeaderVisible(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setHeaderVisible(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiListView_getHeaderHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setHeaderHeight(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiListView_getCurrentHeaderHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiListView_addItem(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiListView_addItemRow(IntPtr cPointer, IntPtr[] item, UIntPtr itemLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_insertItem(IntPtr cPointer, UIntPtr index, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_insertItemRow(IntPtr cPointer, UIntPtr index, IntPtr[] item, UIntPtr itemLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_changeItem(IntPtr cPointer, UIntPtr index, IntPtr[] item, UIntPtr itemLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_changeSubItem(IntPtr cPointer, UIntPtr index, UIntPtr column, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_removeItem(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_removeAllItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setSelectedItem(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setSelectedItems(IntPtr cPointer, UIntPtr[] indices, UIntPtr indicesLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiListView_getSelectedItemIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiListView_getHoveredItemIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe UIntPtr* tguiListView_getSelectedItemIndices(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_deselectItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_getMultiSelect(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setMultiSelect(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setItemData(IntPtr cPointer, UIntPtr index, IntPtr data);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListView_getItemData(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setItemIcon(IntPtr cPointer, UIntPtr index, IntPtr texture);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiListView_getItemCount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListView_getItem(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListView_getItemCell(IntPtr cPointer, UIntPtr rowIndex, UIntPtr columnIndex);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiListView_getItemRow(IntPtr cPointer, UIntPtr index, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiListView_getItems(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiListView_getItemHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setItemHeight(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiListView_getHeaderTextSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setHeaderTextSize(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiListView_getSeparatorWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setSeparatorWidth(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiListView_getHeaderSeparatorHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setHeaderSeparatorHeight(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiListView_getGridLinesWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setGridLinesWidth(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_getAutoScroll(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setAutoScroll(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_getShowVerticalGridLines(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setShowVerticalGridLines(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_getShowHorizontalGridLines(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setShowHorizontalGridLines(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiListView_getFixedIconSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setFixedIconSize(IntPtr cPointer, Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListView_getResizableColumns(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListView_setResizableColumns(IntPtr cPointer, byte value);

        #endregion
    }
}
