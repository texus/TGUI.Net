// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Tabs widget
    /// </summary>
    public class Tabs : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Tabs()
            : base(tguiTabs_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Tabs(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Tabs(Tabs copy)
            : base(copy)
        {
        }

        public new TabsRenderer Renderer
        {
            get => new TabsRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new TabsRenderer SharedRenderer => new TabsRenderer(tguiWidget_getSharedRenderer(CPointer));

        public bool AutoSize
        {
            get => tguiTabs_getAutoSize(CPointer) != 0;
            set => tguiTabs_setAutoSize(CPointer, value ? (byte)1 : (byte)0);
        }

        public int Add(string text, bool select)
        {
            return (int)tguiTabs_add(CPointer, Util.ConvertStringForC_UTF32(text), select ? (byte)1 : (byte)0);
        }

        public void Insert(int index, string text, bool select)
        {
            tguiTabs_insert(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(text), select ? (byte)1 : (byte)0);
        }

        public string GetText(int index)
        {
            return Util.GetStringFromC_UTF32(tguiTabs_getText(CPointer, (UIntPtr)index));
        }

        public bool ChangeText(int index, string text)
        {
            return tguiTabs_changeText(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public void Deselect()
        {
            tguiTabs_deselect(CPointer);
        }

        public void RemoveAll()
        {
            tguiTabs_removeAll(CPointer);
        }

        public string GetSelected()
        {
            return Util.GetStringFromC_UTF32(tguiTabs_getSelected(CPointer));
        }

        public int GetSelectedIndex()
        {
            return tguiTabs_getSelectedIndex(CPointer);
        }

        public int GetHoveredIndex()
        {
            return tguiTabs_getHoveredIndex(CPointer);
        }

        public int GetTabsCount()
        {
            return (int)tguiTabs_getTabsCount(CPointer);
        }

        public void SetTabVisible(int index, bool visible)
        {
            tguiTabs_setTabVisible(CPointer, (UIntPtr)index, visible ? (byte)1 : (byte)0);
        }

        public bool GetTabVisible(int index)
        {
            return tguiTabs_getTabVisible(CPointer, (UIntPtr)index) != 0;
        }

        public void SetTabEnabled(int index, bool visible)
        {
            tguiTabs_setTabEnabled(CPointer, (UIntPtr)index, visible ? (byte)1 : (byte)0);
        }

        public bool GetTabEnabled(int index)
        {
            return tguiTabs_getTabEnabled(CPointer, (UIntPtr)index) != 0;
        }

        public float MaximumTabWidth
        {
            get => tguiTabs_getMaximumTabWidth(CPointer);
            set => tguiTabs_setMaximumTabWidth(CPointer, value);
        }

        public float MinimumTabWidth
        {
            get => tguiTabs_getMinimumTabWidth(CPointer);
            set => tguiTabs_setMinimumTabWidth(CPointer, value);
        }

        public bool Select(string text)
        {
            return tguiTabs_selectByText(CPointer, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool Select(int index)
        {
            return tguiTabs_selectByIndex(CPointer, (UIntPtr)index) != 0;
        }

        public bool Remove(string text)
        {
            return tguiTabs_removeByText(CPointer, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool Remove(int index)
        {
            return tguiTabs_removeByIndex(CPointer, (UIntPtr)index) != 0;
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

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabs_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_getAutoSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setAutoSize(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTabs_add(IntPtr cPointer, IntPtr text, byte select);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_insert(IntPtr cPointer, UIntPtr index, IntPtr text, byte select);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabs_getText(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_changeText(IntPtr cPointer, UIntPtr index, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_deselect(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_removeAll(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabs_getSelected(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiTabs_getSelectedIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiTabs_getHoveredIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTabs_getTabsCount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setTabVisible(IntPtr cPointer, UIntPtr index, byte visible);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_getTabVisible(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setTabEnabled(IntPtr cPointer, UIntPtr index, byte visible);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_getTabEnabled(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTabs_getMaximumTabWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setMaximumTabWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTabs_getMinimumTabWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setMinimumTabWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_selectByText(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_selectByIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_removeByText(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_removeByIndex(IntPtr cPointer, UIntPtr index);

        #endregion
    }
}
