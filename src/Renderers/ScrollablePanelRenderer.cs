// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for ScrollablePanel widgets
    /// </summary>
    public class ScrollablePanelRenderer : PanelRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ScrollablePanelRenderer()
            : base(tguiScrollablePanelRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ScrollablePanelRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ScrollablePanelRenderer(ScrollablePanelRenderer copy)
            : base(tguiScrollablePanelRenderer_copy(copy.CPointer))
        {
        }

        public RendererData Scrollbar
        {
            get => new RendererData(tguiScrollablePanelRenderer_getScrollbar(CPointer));
            set => tguiScrollablePanelRenderer_setScrollbar(CPointer, value.CPointer);
        }

        public float ScrollbarWidth
        {
            get => tguiScrollablePanelRenderer_getScrollbarWidth(CPointer);
            set => tguiScrollablePanelRenderer_setScrollbarWidth(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollablePanelRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollablePanelRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollablePanelRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollablePanelRenderer_setScrollbar(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiScrollablePanelRenderer_getScrollbarWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollablePanelRenderer_setScrollbarWidth(IntPtr cPointer, float value);

        #endregion
    }
}
