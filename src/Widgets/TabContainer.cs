// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public enum TabContainerTabAlign
    {
        Top,
        Bottom,
    }

    /// <summary>
    /// TabContainer widget
    /// </summary>
    public class TabContainer : Container
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TabContainer()
            : base(tguiTabContainer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal TabContainer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public TabContainer(TabContainer copy)
            : base(copy)
        {
        }

        public Panel AddTab(string name, bool select = true)
        {
            return new Panel(tguiTabContainer_addTab(CPointer, Util.ConvertStringForC_UTF32(name), select ? (byte)1 : (byte)0));
        }

        public Panel InsertTab(int index, string name, bool select = true)
        {
            return new Panel(tguiTabContainer_insertTab(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(name), select ? (byte)1 : (byte)0));
        }

        public int GetIndex(Panel panel)
        {
            return tguiTabContainer_getIndex(CPointer, panel.CPointer);
        }

        public Panel? GetSelected()
        {
            IntPtr cPointer = tguiTabContainer_getSelected(CPointer);
            if (cPointer != IntPtr.Zero)
                return new Panel(cPointer);
            else
                return null;
        }

        public Panel? GetPanel(int index)
        {
            IntPtr cPointer = tguiTabContainer_getPanel(CPointer, index);
            if (cPointer != IntPtr.Zero)
                return new Panel(cPointer);
            else
                return null;
        }

        public Tabs GetTabs()
        {
            return new Tabs(tguiTabContainer_getTabs(CPointer));
        }

        public TabsRenderer TabsRenderer
        {
            get => new TabsRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  TabsRenderer TabsSharedRenderer => new TabsRenderer(tguiWidget_getSharedRenderer(CPointer));

        public void Select(int index)
        {
            tguiTabContainer_select(CPointer, (UIntPtr)index);
        }

        public int GetPanelCount()
        {
            return (int)tguiTabContainer_getPanelCount(CPointer);
        }

        public int GetSelectedIndex()
        {
            return tguiTabContainer_getSelectedIndex(CPointer);
        }

        public string GetTabText(int index)
        {
            return Util.GetStringFromC_UTF32(tguiTabContainer_getTabText(CPointer, (UIntPtr)index));
        }

        public bool ChangeTabText(int index, string text)
        {
            return tguiTabContainer_changeTabText(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public float TabFixedSize
        {
            get => tguiTabContainer_getTabFixedSize(CPointer);
            set => tguiTabContainer_setTabFixedSize(CPointer, value);
        }

        public TabContainerTabAlign TabAlignment
        {
            get => tguiTabContainer_getTabAlignment(CPointer);
            set => tguiTabContainer_setTabAlignment(CPointer, value);
        }

        public void SetTabsHeight(float height)
        {
            tguiTabContainer_setTabsHeight(CPointer, height);
        }

        public void SetTabsHeight(Layout layout)
        {
            tguiTabContainer_setTabsHeightFromLayout(CPointer, layout.CPointer);
        }

        public bool RemoveTab(string text)
        {
            return tguiTabContainer_removeTabWithName(CPointer, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool RemoveTab(int index)
        {
            return tguiTabContainer_removeTabWithIndex(CPointer, (UIntPtr)index) != 0;
        }

        public class SelectionChangeEventArgs : EventArgs
        {
            public SelectionChangeEventArgs(int index)
            {
                Index = index;
            }
            public int Index { get; }
        }
        public event EventHandler<SelectionChangeEventArgs> OnSelectionChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackInt func = (int val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new SelectionChangeEventArgs(val));
                };
                uint id = tguiWidget_signalIntConnect(CPointer, Util.ConvertStringForC_UTF32("SelectionChanged"), func);
                ConnectEventHandler(id, "SelectionChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("SelectionChanged", value);
            }
        }

        public class SelectionChangingEventArgs : EventArgs
        {
            public SelectionChangingEventArgs(int index, bool vetoed)
            {
                Index = index;
                Vetoed = vetoed;
            }
            public int Index { get; }
            public bool Vetoed { get; set; }
        }
        public unsafe event EventHandler<SelectionChangingEventArgs> OnSelectionChanging
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackTabSelectionChanging func = (int index, byte* vetoed) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    var e = new SelectionChangingEventArgs(index, *vetoed != 0);
                    value(sender, e);
                    *vetoed = e.Vetoed ? (byte)1 : (byte)0;
                };
                uint id = tguiWidget_signalTabSelectionChangingConnect(CPointer, Util.ConvertStringForC_UTF32("SelectionChanging"), func);
                ConnectEventHandler(id, "SelectionChanging", value, func);
            }
            remove
            {
                DisconnectEventHandler("SelectionChanging", value);
            }
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabContainer_addTab(IntPtr cPointer, IntPtr name, byte select);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabContainer_insertTab(IntPtr cPointer, UIntPtr index, IntPtr name, byte select);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiTabContainer_getIndex(IntPtr cPointer, IntPtr panel);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabContainer_getSelected(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabContainer_getPanel(IntPtr cPointer, int index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabContainer_getTabs(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabContainer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabContainer_select(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTabContainer_getPanelCount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiTabContainer_getSelectedIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabContainer_getTabText(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabContainer_changeTabText(IntPtr cPointer, UIntPtr index, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTabContainer_getTabFixedSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabContainer_setTabFixedSize(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TabContainerTabAlign tguiTabContainer_getTabAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabContainer_setTabAlignment(IntPtr cPointer, TabContainerTabAlign value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabContainer_setTabsHeight(IntPtr cPointer, float height);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabContainer_setTabsHeightFromLayout(IntPtr cPointer, IntPtr layout);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabContainer_removeTabWithName(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabContainer_removeTabWithIndex(IntPtr cPointer, UIntPtr index);

        #endregion
    }
}
