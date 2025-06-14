// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// PanelListBox widget
    /// </summary>
    public class PanelListBox : ScrollablePanel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PanelListBox()
            : base(tguiPanelListBox_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal PanelListBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public PanelListBox(PanelListBox copy)
            : base(copy)
        {
        }

        public Panel? addItem(string id = "", int index = -1)
        {
            IntPtr cPointer;
            if (index >= 0)
                cPointer = tguiPanelListBox_addItemAtIndex(CPointer, Util.ConvertStringForC_UTF32(id), (UIntPtr)index);
            else
                cPointer = tguiPanelListBox_addItem(CPointer, Util.ConvertStringForC_UTF32(id));

            if (cPointer != IntPtr.Zero)
                return new Panel(cPointer);
            else
                return null;
        }

        public Panel getPanelTemplate()
        {
            return new Panel(tguiPanelListBox_getPanelTemplate(CPointer));
        }

        public bool SetSelectedItem(Panel panel)
        {
            return tguiPanelListBox_setSelectedItem(CPointer, panel.CPointer) != 0;
        }

        public Panel? GetSelectedItem()
        {
            IntPtr cPointer = tguiPanelListBox_getSelectedItem(CPointer);
            if (cPointer != IntPtr.Zero)
                return new Panel(cPointer);
            else
                return null;
        }

        public bool RemoveItem(Panel panel)
        {
            return tguiPanelListBox_removeItem(CPointer, panel.CPointer) != 0;
        }

        public Panel? GetItemById(string id)
        {
            IntPtr cPointer = tguiPanelListBox_getItemById(CPointer, Util.ConvertStringForC_UTF32(id));
            if (cPointer != IntPtr.Zero)
                return new Panel(cPointer);
            else
                return null;
        }

        public Panel? GetItemByIndex(int index)
        {
            IntPtr cPointer = tguiPanelListBox_getItemByIndex(CPointer, (UIntPtr)index);
            if (cPointer != IntPtr.Zero)
                return new Panel(cPointer);
            else
                return null;
        }

        public int GetIndexById(string id)
        {
            return tguiPanelListBox_getIndexById(CPointer, Util.ConvertStringForC_UTF32(id));
        }

        public int GetIndexByItem(Panel panel)
        {
            return tguiPanelListBox_getIndexByItem(CPointer, panel.CPointer);
        }

        public string GetIdByIndex(int index)
        {
            return Util.GetStringFromC_UTF32(tguiPanelListBox_getIdByIndex(CPointer, (UIntPtr)index));
        }

        public unsafe IReadOnlyList<Widget> GetItems()
        {
            IntPtr* returnWidgetsC = tguiPanelListBox_getItems(CPointer, out UIntPtr returnCount);
            Panel[] returnWidgets = new Panel[(int)returnCount];
            for (int i = 0; i < (int)returnCount; ++i)
                returnWidgets[i] = (Panel?)Util.GetWidgetFromC(returnWidgetsC[i]) ?? throw new ArgumentNullException();

            return returnWidgets;
        }

        public bool Contains(Panel panel)
        {
            return tguiPanelListBox_contains(CPointer, panel.CPointer) != 0;
        }

        public new PanelListBoxRenderer Renderer
        {
            get => new PanelListBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new PanelListBoxRenderer SharedRenderer => new PanelListBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        public void DeselectItem()
        {
            tguiPanelListBox_deselectItem(CPointer);
        }

        public void RemoveAllItems()
        {
            tguiPanelListBox_removeAllItems(CPointer);
        }

        public bool SetSelectedItemById(string id)
        {
            return tguiPanelListBox_setSelectedItemById(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
        }

        public bool SetSelectedItemByIndex(int index)
        {
            return tguiPanelListBox_setSelectedItemByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public bool RemoveItemById(string id)
        {
            return tguiPanelListBox_removeItemById(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
        }

        public bool RemoveItemByIndex(int index)
        {
            return tguiPanelListBox_removeItemByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public string SelectedItemId
        {
            get => Util.GetStringFromC_UTF32(tguiPanelListBox_getSelectedItemId(CPointer));
        }

        public int SelectedItemIndex
        {
            get => tguiPanelListBox_getSelectedItemIndex(CPointer);
        }

        public int HoveredItemIndex
        {
            get => tguiPanelListBox_getHoveredItemIndex(CPointer);
        }

        public int ItemCount
        {
            get => (int)tguiPanelListBox_getItemCount(CPointer);
        }

        public unsafe IReadOnlyList<string> ItemIds
        {
            get
            {
                IntPtr* returnStringsC = tguiPanelListBox_getItemIds(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public int MaximumItems
        {
            get => (int)tguiPanelListBox_getMaximumItems(CPointer);
            set => tguiPanelListBox_setMaximumItems(CPointer, (UIntPtr)value);
        }

        public bool ContainsId(string id)
        {
            return tguiPanelListBox_containsId(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
        }

        public Layout ItemsWidth
        {
            get => new Layout(tguiPanelListBox_getItemsWidth(CPointer));
        }

        public Layout ItemsHeight
        {
            get => new Layout(tguiPanelListBox_getItemsHeight(CPointer));
            set => tguiPanelListBox_setItemsHeight(CPointer, value.CPointer);
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
                UnmanagedCallbackPanelListBoxItem func = (int index) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ItemSelectEventArgs(index));
                };
                uint id = tguiWidget_signalPanelListBoxItemConnect(CPointer, Util.ConvertStringForC_UTF32("ItemSelected"), func);
                ConnectEventHandler(id, "ItemSelected", value, func);
            }
            remove
            {
                DisconnectEventHandler("ItemSelected", value);
            }
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_addItem(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_addItemAtIndex(IntPtr cPointer, IntPtr id, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_getPanelTemplate(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanelListBox_setSelectedItem(IntPtr cPointer, IntPtr panel);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_getSelectedItem(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanelListBox_removeItem(IntPtr cPointer, IntPtr panel);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_getItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_getItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiPanelListBox_getIndexById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiPanelListBox_getIndexByItem(IntPtr cPointer, IntPtr panel);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_getIdByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiPanelListBox_getItems(IntPtr cPointer, out UIntPtr count);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanelListBox_contains(IntPtr cPointer, IntPtr panel);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanelListBox_deselectItem(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanelListBox_removeAllItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanelListBox_setSelectedItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanelListBox_setSelectedItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanelListBox_removeItemById(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanelListBox_removeItemByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_getSelectedItemId(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiPanelListBox_getSelectedItemIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiPanelListBox_getHoveredItemIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiPanelListBox_getItemCount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiPanelListBox_getItemIds(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiPanelListBox_getMaximumItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanelListBox_setMaximumItems(IntPtr cPointer, UIntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanelListBox_containsId(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_getItemsWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBox_getItemsHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanelListBox_setItemsHeight(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
