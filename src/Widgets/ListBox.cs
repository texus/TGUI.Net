// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// ListBox widget
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

        public ScrollbarAccessor Scrollbar => new ScrollbarAccessor(tguiScrollbarChildInterface_getScrollbar(CPointer));

        public new ListBoxRenderer Renderer
        {
            get => new ListBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new ListBoxRenderer SharedRenderer => new ListBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        public int AddItem(string item, string id = "")
        {
            return (int)tguiListBox_addItem(CPointer, Util.ConvertStringForC_UTF32(item), Util.ConvertStringForC_UTF32(id));
        }

        public void AddMultipleItems(ReadOnlySpan<string> items)
        {
            IntPtr[] itemsForC = new IntPtr[items.Length];
            for (int i = 0; i < items.Length; ++i)
                itemsForC[i] = Util.ConvertStringForC_UTF32(items[i]);

            tguiListBox_addMultipleItems(CPointer, itemsForC, (UIntPtr)itemsForC.Length);
        }

        public string GetItemById(string id)
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getItemById(CPointer, Util.ConvertStringForC_UTF32(id)));
        }

        public string GetItemByIndex(int index)
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getItemByIndex(CPointer, (UIntPtr)index));
        }

        public int GetIndexById(string id)
        {
            return tguiListBox_getIndexById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        public string GetIdByIndex(int index)
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getIdByIndex(CPointer, (UIntPtr)index));
        }

        public bool SetSelectedItem(string item)
        {
            return tguiListBox_setSelectedItem(CPointer, Util.ConvertStringForC_UTF32(item)) != 0;
        }

        public bool SetSelectedItemById(string id)
        {
            return tguiListBox_setSelectedItemById(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
        }

        public bool SetSelectedItemByIndex(int index)
        {
            return tguiListBox_setSelectedItemByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public void DeselectItem()
        {
            tguiListBox_deselectItem(CPointer);
        }

        public bool RemoveItem(string item)
        {
            return tguiListBox_removeItem(CPointer, Util.ConvertStringForC_UTF32(item)) != 0;
        }

        public bool RemoveItemById(string id)
        {
            return tguiListBox_removeItemById(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
        }

        public bool RemoveItemByIndex(int index)
        {
            return tguiListBox_removeItemByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public void RemoveAllItems()
        {
            tguiListBox_removeAllItems(CPointer);
        }

        public string SelectedItem
        {
            get => Util.GetStringFromC_UTF32(tguiListBox_getSelectedItem(CPointer));
        }

        public string SelectedItemId
        {
            get => Util.GetStringFromC_UTF32(tguiListBox_getSelectedItemId(CPointer));
        }

        public int SelectedItemIndex
        {
            get => tguiListBox_getSelectedItemIndex(CPointer);
        }

        public int HoveredItemIndex
        {
            get => tguiListBox_getHoveredItemIndex(CPointer);
        }

        public bool ChangeItem(string originalValue, string newValue)
        {
            return tguiListBox_changeItem(CPointer, Util.ConvertStringForC_UTF32(originalValue), Util.ConvertStringForC_UTF32(newValue)) != 0;
        }

        public bool ChangeItemById(string id, string newValue)
        {
            return tguiListBox_changeItemById(CPointer, Util.ConvertStringForC_UTF32(id), Util.ConvertStringForC_UTF32(newValue)) != 0;
        }

        public bool ChangeItemByIndex(int index, string newValue)
        {
            return tguiListBox_changeItemByIndex(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(newValue)) != 0;
        }

        public int ItemCount
        {
            get => (int)tguiListBox_getItemCount(CPointer);
        }

        public unsafe IReadOnlyList<string> Items
        {
            get
            {
                IntPtr* returnStringsC = tguiListBox_getItems(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public unsafe IReadOnlyList<string> ItemIds
        {
            get
            {
                IntPtr* returnStringsC = tguiListBox_getItemIds(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public void SetItemData(int index, string data)
        {
            tguiListBox_setItemData(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(data));
        }

        public string GetItemData(int index)
        {
            return Util.GetStringFromC_UTF32(tguiListBox_getItemData(CPointer, (UIntPtr)index));
        }

        public int ItemHeight
        {
            get => (int)tguiListBox_getItemHeight(CPointer);
            set => tguiListBox_setItemHeight(CPointer, (uint)value);
        }

        public int MaximumItems
        {
            get => (int)tguiListBox_getMaximumItems(CPointer);
            set => tguiListBox_setMaximumItems(CPointer, (UIntPtr)value);
        }

        public bool AutoScroll
        {
            get => tguiListBox_getAutoScroll(CPointer) != 0;
            set => tguiListBox_setAutoScroll(CPointer, value ? (byte)1 : (byte)0);
        }

        public HorizontalAlignment TextAlignment
        {
            get => tguiListBox_getTextAlignment(CPointer);
            set => tguiListBox_setTextAlignment(CPointer, value);
        }

        public bool Contains(string item)
        {
            return tguiListBox_contains(CPointer, Util.ConvertStringForC_UTF32(item)) != 0;
        }

        public bool ContainsId(string id)
        {
            return tguiListBox_containsId(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
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
                UnmanagedCallbackItem func = (int index) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ItemSelectEventArgs(index));
                };
                uint id = tguiWidget_signalItemConnect(CPointer, Util.ConvertStringForC_UTF32("ItemSelected"), func);
                ConnectEventHandler(id, "ItemSelected", value, func);
            }
            remove
            {
                DisconnectEventHandler("ItemSelected", value);
            }
        }

        public class MousePressEventArgs : EventArgs
        {
            public MousePressEventArgs(int index)
            {
                Index = index;
            }
            public int Index { get; }
        }
        public event EventHandler<MousePressEventArgs> OnMousePress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackItem func = (int index) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new MousePressEventArgs(index));
                };
                uint id = tguiWidget_signalItemConnect(CPointer, Util.ConvertStringForC_UTF32("MousePressed"), func);
                ConnectEventHandler(id, "MousePressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("MousePressed", value);
            }
        }

        public class MouseReleaseEventArgs : EventArgs
        {
            public MouseReleaseEventArgs(int index)
            {
                Index = index;
            }
            public int Index { get; }
        }
        public event EventHandler<MouseReleaseEventArgs> OnMouseRelease
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackItem func = (int index) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new MouseReleaseEventArgs(index));
                };
                uint id = tguiWidget_signalItemConnect(CPointer, Util.ConvertStringForC_UTF32("MouseReleased"), func);
                ConnectEventHandler(id, "MouseReleased", value, func);
            }
            remove
            {
                DisconnectEventHandler("MouseReleased", value);
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
                UnmanagedCallbackItem func = (int index) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new DoubleClickEventArgs(index));
                };
                uint id = tguiWidget_signalItemConnect(CPointer, Util.ConvertStringForC_UTF32("DoubleClicked"), func);
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
                UnmanagedCallbackItem func = (int index) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new RightClickEventArgs(index));
                };
                uint id = tguiWidget_signalItemConnect(CPointer, Util.ConvertStringForC_UTF32("RightClicked"), func);
                ConnectEventHandler(id, "RightClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("RightClicked", value);
            }
        }

        public class ScrollEventArgs : EventArgs
        {
            public ScrollEventArgs(uint val)
            {
                Value = val;
            }
            public uint Value { get; }
        }
        public event EventHandler<ScrollEventArgs> OnScroll
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackUInt func = (uint val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ScrollEventArgs(val));
                };
                uint id = tguiWidget_signalUIntConnect(CPointer, Util.ConvertStringForC_UTF32("Scrolled"), func);
                ConnectEventHandler(id, "Scrolled", value, func);
            }
            remove
            {
                DisconnectEventHandler("Scrolled", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBox_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarChildInterface_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiListBox_addItem(IntPtr cPointer, IntPtr item, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBox_addMultipleItems(IntPtr cPointer, IntPtr[] items, UIntPtr itemsLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBox_getItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBox_getItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiListBox_getIndexById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBox_getIdByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_setSelectedItem(IntPtr cPointer, IntPtr item);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_setSelectedItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_setSelectedItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBox_deselectItem(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_removeItem(IntPtr cPointer, IntPtr item);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_removeItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_removeItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBox_removeAllItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBox_getSelectedItem(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBox_getSelectedItemId(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiListBox_getSelectedItemIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiListBox_getHoveredItemIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_changeItem(IntPtr cPointer, IntPtr originalValue, IntPtr newValue);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_changeItemById(IntPtr cPointer, IntPtr id, IntPtr newValue);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_changeItemByIndex(IntPtr cPointer, UIntPtr index, IntPtr newValue);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiListBox_getItemCount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiListBox_getItems(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiListBox_getItemIds(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBox_setItemData(IntPtr cPointer, UIntPtr index, IntPtr data);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBox_getItemData(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiListBox_getItemHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBox_setItemHeight(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiListBox_getMaximumItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBox_setMaximumItems(IntPtr cPointer, UIntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_getAutoScroll(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBox_setAutoScroll(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern HorizontalAlignment tguiListBox_getTextAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBox_setTextAlignment(IntPtr cPointer, HorizontalAlignment value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_contains(IntPtr cPointer, IntPtr item);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiListBox_containsId(IntPtr cPointer, IntPtr id);

        #endregion
    }
}
