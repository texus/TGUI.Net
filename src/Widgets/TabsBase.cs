// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// TabsBase widget
    /// </summary>
    public class TabsBase : Widget
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal TabsBase(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public TabsBase(TabsBase copy)
            : base(copy)
        {
        }

        public new TabsRenderer Renderer
        {
            get => new TabsRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new TabsRenderer SharedRenderer => new TabsRenderer(tguiWidget_getSharedRenderer(CPointer));

        public int Add(string text, bool select = true)
        {
            return (int)tguiTabsBase_add(CPointer, Util.ConvertStringForC_UTF32(text), select ? (byte)1 : (byte)0);
        }

        public void Insert(int index, string text, bool select = true)
        {
            tguiTabsBase_insert(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(text), select ? (byte)1 : (byte)0);
        }

        public string GetText(int index)
        {
            return Util.GetStringFromC_UTF32(tguiTabsBase_getText(CPointer, (UIntPtr)index));
        }

        public bool ChangeText(int index, string text)
        {
            return tguiTabsBase_changeText(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public void Deselect()
        {
            tguiTabsBase_deselect(CPointer);
        }

        public void RemoveAll()
        {
            tguiTabsBase_removeAll(CPointer);
        }

        public string Selected
        {
            get => Util.GetStringFromC_UTF32(tguiTabsBase_getSelected(CPointer));
        }

        public int SelectedIndex
        {
            get => tguiTabsBase_getSelectedIndex(CPointer);
        }

        public int HoveredIndex
        {
            get => tguiTabsBase_getHoveredIndex(CPointer);
        }

        public int TabsCount
        {
            get => (int)tguiTabsBase_getTabsCount(CPointer);
        }

        public void SetTabVisible(int index, bool visible)
        {
            tguiTabsBase_setTabVisible(CPointer, (UIntPtr)index, visible ? (byte)1 : (byte)0);
        }

        public bool GetTabVisible(int index)
        {
            return tguiTabsBase_getTabVisible(CPointer, (UIntPtr)index) != 0;
        }

        public void SetTabEnabled(int index, bool visible)
        {
            tguiTabsBase_setTabEnabled(CPointer, (UIntPtr)index, visible ? (byte)1 : (byte)0);
        }

        public bool GetTabEnabled(int index)
        {
            return tguiTabsBase_getTabEnabled(CPointer, (UIntPtr)index) != 0;
        }

        public bool Select(string text)
        {
            return tguiTabsBase_selectByText(CPointer, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool Select(int index)
        {
            return tguiTabsBase_selectByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public bool Remove(string text)
        {
            return tguiTabsBase_removeByText(CPointer, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool Remove(int index)
        {
            return tguiTabsBase_removeByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public class TabSelectEventArgs : EventArgs
        {
            public TabSelectEventArgs(string tabText)
            {
                TabText = tabText;
            }
            public string TabText { get; }
        }
        public event EventHandler<TabSelectEventArgs> OnTabSelect
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new TabSelectEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("TabSelected"), func);
                ConnectEventHandler(id, "TabSelected", value, func);
            }
            remove
            {
                DisconnectEventHandler("TabSelected", value);
            }
        }

        public class TabRightClickEventArgs : EventArgs
        {
            public TabRightClickEventArgs(string tabText)
            {
                TabText = tabText;
            }
            public string TabText { get; }
        }
        public event EventHandler<TabRightClickEventArgs> OnTabRightClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new TabRightClickEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("TabRightClicked"), func);
                ConnectEventHandler(id, "TabRightClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("TabRightClicked", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsBase_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTabsBase_add(IntPtr cPointer, IntPtr text, byte select);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsBase_insert(IntPtr cPointer, UIntPtr index, IntPtr text, byte select);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsBase_getText(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabsBase_changeText(IntPtr cPointer, UIntPtr index, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsBase_deselect(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsBase_removeAll(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsBase_getSelected(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiTabsBase_getSelectedIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiTabsBase_getHoveredIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTabsBase_getTabsCount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsBase_setTabVisible(IntPtr cPointer, UIntPtr index, byte visible);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabsBase_getTabVisible(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsBase_setTabEnabled(IntPtr cPointer, UIntPtr index, byte visible);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabsBase_getTabEnabled(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabsBase_selectByText(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabsBase_selectByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabsBase_removeByText(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabsBase_removeByIndex(IntPtr cPointer, UIntPtr index);

        #endregion
    }
}
