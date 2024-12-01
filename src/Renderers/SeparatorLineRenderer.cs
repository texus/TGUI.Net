// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for SeparatorLine widgets
    /// </summary>
    public class SeparatorLineRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SeparatorLineRenderer()
            : base(tguiSeparatorLineRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SeparatorLineRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public SeparatorLineRenderer(SeparatorLineRenderer copy)
            : base(tguiSeparatorLineRenderer_copy(copy.CPointer))
        {
        }

        public Color? Color
        {
            get => Util.GetColorFromC(tguiSeparatorLineRenderer_getColor(CPointer));
            set => tguiSeparatorLineRenderer_setColor(CPointer, Util.ConvertColorForC(value));
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSeparatorLineRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSeparatorLineRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSeparatorLineRenderer_getColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSeparatorLineRenderer_setColor(IntPtr cPointer, ColorCTGUI value);

        #endregion
    }
}
