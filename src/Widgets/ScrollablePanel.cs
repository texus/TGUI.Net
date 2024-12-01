// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// ScrollablePanel widget
    /// </summary>
    public class ScrollablePanel : Panel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ScrollablePanel()
            : base(tguiScrollablePanel_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ScrollablePanel(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ScrollablePanel(ScrollablePanel copy)
            : base(copy)
        {
        }

        public ScrollbarAccessor VerticalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getVerticalScrollbar(CPointer));
        public ScrollbarAccessor HorizontalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getHorizontalScrollbar(CPointer));

        public new ScrollablePanelRenderer Renderer
        {
            get => new ScrollablePanelRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new ScrollablePanelRenderer SharedRenderer => new ScrollablePanelRenderer(tguiWidget_getSharedRenderer(CPointer));

        public Vector2f ContentSize
        {
            get => tguiScrollablePanel_getContentSize(CPointer);
            set => tguiScrollablePanel_setContentSize(CPointer, value);
        }

        public Vector2f GetContentOffset()
        {
            return tguiScrollablePanel_getContentOffset(CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollablePanel_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiDualScrollbarChildInterface_getVerticalScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiDualScrollbarChildInterface_getHorizontalScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiScrollablePanel_getContentSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollablePanel_setContentSize(IntPtr cPointer, Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiScrollablePanel_getContentOffset(IntPtr cPointer);

        #endregion
    }
}
