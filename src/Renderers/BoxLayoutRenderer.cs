// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for BoxLayout widgets
    /// </summary>
    public class BoxLayoutRenderer : GroupRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BoxLayoutRenderer()
            : base(tguiBoxLayoutRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal BoxLayoutRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public BoxLayoutRenderer(BoxLayoutRenderer copy)
            : base(tguiBoxLayoutRenderer_copy(copy.CPointer))
        {
        }

        public float SpaceBetweenWidgets
        {
            get => tguiBoxLayoutRenderer_getSpaceBetweenWidgets(CPointer);
            set => tguiBoxLayoutRenderer_setSpaceBetweenWidgets(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiBoxLayoutRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiBoxLayoutRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiBoxLayoutRenderer_getSpaceBetweenWidgets(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiBoxLayoutRenderer_setSpaceBetweenWidgets(IntPtr cPointer, float value);

        #endregion
    }
}
