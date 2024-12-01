// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public enum ComboBoxExpandDirection
    {
        Down,
        Up,
        Automatic,
    }

    /// <summary>
    /// ComboBox widget
    /// </summary>
    public class ComboBox : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ComboBox()
            : base(tguiComboBox_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ComboBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ComboBox(ComboBox copy)
            : base(copy)
        {
        }

        public new ComboBoxRenderer Renderer
        {
            get => new ComboBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new ComboBoxRenderer SharedRenderer => new ComboBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        public int ItemsToDisplay
        {
            get => (int)tguiComboBox_getItemsToDisplay(CPointer);
            set => tguiComboBox_setItemsToDisplay(CPointer, (UIntPtr)value);
        }

        public int AddItem(string item, string id)
        {
            return (int)tguiComboBox_addItem(CPointer, Util.ConvertStringForC_UTF32(item), Util.ConvertStringForC_UTF32(id));
        }

        public void AddMultipleItems(ReadOnlySpan<string> items)
        {
            IntPtr[] itemsForC = new IntPtr[items.Length];
            for (int i = 0; i < items.Length; ++i)
                itemsForC[i] = Util.ConvertStringForC_UTF32(items[i]);

            tguiComboBox_addMultipleItems(CPointer, itemsForC, (UIntPtr)itemsForC.Length);
        }

        public string GetItemById(string id)
        {
            return Util.GetStringFromC_UTF32(tguiComboBox_getItemById(CPointer, Util.ConvertStringForC_UTF32(id)));
        }

        public string GetItemByIndex(int index)
        {
            return Util.GetStringFromC_UTF32(tguiComboBox_getItemByIndex(CPointer, (UIntPtr)index));
        }

        public int GetIndexById(string id)
        {
            return tguiComboBox_getIndexById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        public string GetIdByIndex(int index)
        {
            return Util.GetStringFromC_UTF32(tguiComboBox_getIdByIndex(CPointer, (UIntPtr)index));
        }

        public IReadOnlyList<string> GetItems()
        {
            unsafe
            {
                IntPtr* returnStringsC = tguiComboBox_getItems(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public IReadOnlyList<string> GetItemIds()
        {
            unsafe
            {
                IntPtr* returnStringsC = tguiComboBox_getItemIds(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public bool SetSelectedItem(string item)
        {
            return tguiComboBox_setSelectedItem(CPointer, Util.ConvertStringForC_UTF32(item)) != 0;
        }

        public bool SetSelectedItemById(string item)
        {
            return tguiComboBox_setSelectedItemById(CPointer, Util.ConvertStringForC_UTF32(item)) != 0;
        }

        public bool SetSelectedItemByIndex(int index)
        {
            return tguiComboBox_setSelectedItemByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public void DeselectItem()
        {
            tguiComboBox_deselectItem(CPointer);
        }

        public bool RemoveItem(string item)
        {
            return tguiComboBox_removeItem(CPointer, Util.ConvertStringForC_UTF32(item)) != 0;
        }

        public bool RemoveItemById(string id)
        {
            return tguiComboBox_removeItemById(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
        }

        public bool RemoveItemByIndex(int index)
        {
            return tguiComboBox_removeItemByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public void RemoveAllItems()
        {
            tguiComboBox_removeAllItems(CPointer);
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
            return tguiComboBox_changeItem(CPointer, Util.ConvertStringForC_UTF32(originalValue), Util.ConvertStringForC_UTF32(newValue)) != 0;
        }

        public bool ChangeItemById(string id, string newValue)
        {
            return tguiComboBox_changeItemById(CPointer, Util.ConvertStringForC_UTF32(id), Util.ConvertStringForC_UTF32(newValue)) != 0;
        }

        public bool ChangeItemByIndex(int index, string newValue)
        {
            return tguiComboBox_changeItemByIndex(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(newValue)) != 0;
        }

        public int GetItemCount()
        {
            return (int)tguiComboBox_getItemCount(CPointer);
        }

        public void SetItemData(int index, string data)
        {
            tguiComboBox_setItemData(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(data));
        }

        public string GetItemData(int index)
        {
            return Util.GetStringFromC_UTF32(tguiComboBox_getItemData(CPointer, (UIntPtr)index));
        }

        public int MaximumItems
        {
            get => (int)tguiComboBox_getMaximumItems(CPointer);
            set => tguiComboBox_setMaximumItems(CPointer, (UIntPtr)value);
        }

        public string DefaultText
        {
            get => Util.GetStringFromC_UTF32(tguiComboBox_getDefaultText(CPointer));
            set => tguiComboBox_setDefaultText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public ComboBoxExpandDirection ExpandDirection
        {
            get => tguiComboBox_getExpandDirection(CPointer);
            set => tguiComboBox_setExpandDirection(CPointer, value);
        }

        public bool ChangeItemOnScroll
        {
            get => tguiComboBox_getChangeItemOnScroll(CPointer) != 0;
            set => tguiComboBox_setChangeItemOnScroll(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool Contains(string item)
        {
            return tguiComboBox_contains(CPointer, Util.ConvertStringForC_UTF32(item)) != 0;
        }

        public bool ContainsId(string id)
        {
            return tguiComboBox_containsId(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
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

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBox_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiComboBox_getItemsToDisplay(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_setItemsToDisplay(IntPtr cPointer, UIntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiComboBox_addItem(IntPtr cPointer, IntPtr item, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_addMultipleItems(IntPtr cPointer, IntPtr[] items, UIntPtr itemsLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBox_getItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBox_getItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiComboBox_getIndexById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBox_getIdByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiComboBox_getItems(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiComboBox_getItemIds(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_setSelectedItem(IntPtr cPointer, IntPtr item);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_setSelectedItemById(IntPtr cPointer, IntPtr item);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_setSelectedItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_deselectItem(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_removeItem(IntPtr cPointer, IntPtr item);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_removeItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_removeItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_removeAllItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBox_getSelectedItem(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBox_getSelectedItemId(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiComboBox_getSelectedItemIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_changeItem(IntPtr cPointer, IntPtr originalValue, IntPtr newValue);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_changeItemById(IntPtr cPointer, IntPtr id, IntPtr newValue);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_changeItemByIndex(IntPtr cPointer, UIntPtr index, IntPtr newValue);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiComboBox_getItemCount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_setItemData(IntPtr cPointer, UIntPtr index, IntPtr data);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBox_getItemData(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiComboBox_getMaximumItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_setMaximumItems(IntPtr cPointer, UIntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBox_getDefaultText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_setDefaultText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ComboBoxExpandDirection tguiComboBox_getExpandDirection(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_setExpandDirection(IntPtr cPointer, ComboBoxExpandDirection value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_getChangeItemOnScroll(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBox_setChangeItemOnScroll(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_contains(IntPtr cPointer, IntPtr item);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiComboBox_containsId(IntPtr cPointer, IntPtr id);

        #endregion
    }
}
