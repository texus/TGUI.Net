// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for ProgressBar widgets
    /// </summary>
    public class ProgressBarRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ProgressBarRenderer()
            : base(tguiProgressBarRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ProgressBarRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ProgressBarRenderer(ProgressBarRenderer copy)
            : base(tguiProgressBarRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiProgressBarRenderer_getBorders(CPointer));
            set => tguiProgressBarRenderer_setBorders(CPointer, value.CPointer);
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiProgressBarRenderer_getTextColor(CPointer));
            set => tguiProgressBarRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorFilled
        {
            get => Util.GetColorFromC(tguiProgressBarRenderer_getTextColorFilled(CPointer));
            set => tguiProgressBarRenderer_setTextColorFilled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiProgressBarRenderer_getBackgroundColor(CPointer));
            set => tguiProgressBarRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? FillColor
        {
            get => Util.GetColorFromC(tguiProgressBarRenderer_getFillColor(CPointer));
            set => tguiProgressBarRenderer_setFillColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiProgressBarRenderer_getBorderColor(CPointer));
            set => tguiProgressBarRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiProgressBarRenderer_getTextureBackground(CPointer));
            set => tguiProgressBarRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        public Texture TextureFill
        {
            get => new Texture(tguiProgressBarRenderer_getTextureFill(CPointer));
            set => tguiProgressBarRenderer_setTextureFill(CPointer, value.CPointer);
        }

        public TextStyles TextStyle
        {
            get => tguiProgressBarRenderer_getTextStyle(CPointer);
            set => tguiProgressBarRenderer_setTextStyle(CPointer, value);
        }

        public Color? TextOutlineColor
        {
            get => Util.GetColorFromC(tguiProgressBarRenderer_getTextOutlineColor(CPointer));
            set => tguiProgressBarRenderer_setTextOutlineColor(CPointer, Util.ConvertColorForC(value));
        }

        public float TextOutlineThickness
        {
            get => tguiProgressBarRenderer_getTextOutlineThickness(CPointer);
            set => tguiProgressBarRenderer_setTextOutlineThickness(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiProgressBarRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiProgressBarRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiProgressBarRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiProgressBarRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiProgressBarRenderer_getTextColorFilled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setTextColorFilled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiProgressBarRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiProgressBarRenderer_getFillColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setFillColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiProgressBarRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiProgressBarRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiProgressBarRenderer_getTextureFill(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setTextureFill(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiProgressBarRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiProgressBarRenderer_getTextOutlineColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setTextOutlineColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiProgressBarRenderer_getTextOutlineThickness(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBarRenderer_setTextOutlineThickness(IntPtr cPointer, float value);

        #endregion
    }
}
