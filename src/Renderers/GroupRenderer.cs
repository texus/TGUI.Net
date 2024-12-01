// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for Group widgets
    /// </summary>
    public class GroupRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GroupRenderer()
            : base(tguiGroupRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal GroupRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public GroupRenderer(GroupRenderer copy)
            : base(tguiGroupRenderer_copy(copy.CPointer))
        {
        }

        public Outline Padding
        {
            get => new Outline(tguiGroupRenderer_getPadding(CPointer));
            set => tguiGroupRenderer_setPadding(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGroupRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGroupRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGroupRenderer_getPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGroupRenderer_setPadding(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
