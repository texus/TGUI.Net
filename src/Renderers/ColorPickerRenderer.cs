// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for ColorPicker widgets
    /// </summary>
    public class ColorPickerRenderer : ChildWindowRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ColorPickerRenderer()
            : base(tguiColorPickerRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ColorPickerRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ColorPickerRenderer(ColorPickerRenderer copy)
            : base(tguiColorPickerRenderer_copy(copy.CPointer))
        {
        }

        public RendererData Button
        {
            get => new RendererData(tguiColorPickerRenderer_getButton(CPointer));
            set => tguiColorPickerRenderer_setButton(CPointer, value.CPointer);
        }

        public RendererData Label
        {
            get => new RendererData(tguiColorPickerRenderer_getLabel(CPointer));
            set => tguiColorPickerRenderer_setLabel(CPointer, value.CPointer);
        }

        public RendererData Slider
        {
            get => new RendererData(tguiColorPickerRenderer_getSlider(CPointer));
            set => tguiColorPickerRenderer_setSlider(CPointer, value.CPointer);
        }

        public RendererData EditBox
        {
            get => new RendererData(tguiColorPickerRenderer_getEditBox(CPointer));
            set => tguiColorPickerRenderer_setEditBox(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiColorPickerRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiColorPickerRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiColorPickerRenderer_getButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiColorPickerRenderer_setButton(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiColorPickerRenderer_getLabel(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiColorPickerRenderer_setLabel(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiColorPickerRenderer_getSlider(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiColorPickerRenderer_setSlider(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiColorPickerRenderer_getEditBox(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiColorPickerRenderer_setEditBox(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
