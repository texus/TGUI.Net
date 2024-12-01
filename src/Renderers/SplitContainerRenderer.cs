// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for SplitContainer widgets
    /// </summary>
    public class SplitContainerRenderer : GroupRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SplitContainerRenderer()
            : base(tguiSplitContainerRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SplitContainerRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public SplitContainerRenderer(SplitContainerRenderer copy)
            : base(tguiSplitContainerRenderer_copy(copy.CPointer))
        {
        }

        public Color? SplitterColor
        {
            get => Util.GetColorFromC(tguiSplitContainerRenderer_getSplitterColor(CPointer));
            set => tguiSplitContainerRenderer_setSplitterColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SplitterColorHover
        {
            get => Util.GetColorFromC(tguiSplitContainerRenderer_getSplitterColorHover(CPointer));
            set => tguiSplitContainerRenderer_setSplitterColorHover(CPointer, Util.ConvertColorForC(value));
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSplitContainerRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSplitContainerRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSplitContainerRenderer_getSplitterColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSplitContainerRenderer_setSplitterColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSplitContainerRenderer_getSplitterColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSplitContainerRenderer_setSplitterColorHover(IntPtr cPointer, ColorCTGUI value);

        #endregion
    }
}
