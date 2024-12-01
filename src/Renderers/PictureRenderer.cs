// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for Picture widgets
    /// </summary>
    public class PictureRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PictureRenderer()
            : base(tguiPictureRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal PictureRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public PictureRenderer(PictureRenderer copy)
            : base(tguiPictureRenderer_copy(copy.CPointer))
        {
        }

        public Texture Texture
        {
            get => new Texture(tguiPictureRenderer_getTexture(CPointer));
            set => tguiPictureRenderer_setTexture(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPictureRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPictureRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPictureRenderer_getTexture(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPictureRenderer_setTexture(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
