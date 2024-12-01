// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for Label widgets
    /// </summary>
    public class LabelRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelRenderer()
            : base(tguiLabelRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal LabelRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public LabelRenderer(LabelRenderer copy)
            : base(tguiLabelRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiLabelRenderer_getBorders(CPointer));
            set => tguiLabelRenderer_setBorders(CPointer, value.CPointer);
        }

        public Outline Padding
        {
            get => new Outline(tguiLabelRenderer_getPadding(CPointer));
            set => tguiLabelRenderer_setPadding(CPointer, value.CPointer);
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiLabelRenderer_getTextColor(CPointer));
            set => tguiLabelRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextOutlineColor
        {
            get => Util.GetColorFromC(tguiLabelRenderer_getTextOutlineColor(CPointer));
            set => tguiLabelRenderer_setTextOutlineColor(CPointer, Util.ConvertColorForC(value));
        }

        public float TextOutlineThickness
        {
            get => tguiLabelRenderer_getTextOutlineThickness(CPointer);
            set => tguiLabelRenderer_setTextOutlineThickness(CPointer, value);
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiLabelRenderer_getBackgroundColor(CPointer));
            set => tguiLabelRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiLabelRenderer_getBorderColor(CPointer));
            set => tguiLabelRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public TextStyles TextStyle
        {
            get => tguiLabelRenderer_getTextStyle(CPointer);
            set => tguiLabelRenderer_setTextStyle(CPointer, value);
        }

        public RendererData Scrollbar
        {
            get => new RendererData(tguiLabelRenderer_getScrollbar(CPointer));
            set => tguiLabelRenderer_setScrollbar(CPointer, value.CPointer);
        }

        public float ScrollbarWidth
        {
            get => tguiLabelRenderer_getScrollbarWidth(CPointer);
            set => tguiLabelRenderer_setScrollbarWidth(CPointer, value);
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiLabelRenderer_getTextureBackground(CPointer));
            set => tguiLabelRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiLabelRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiLabelRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiLabelRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiLabelRenderer_getPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setPadding(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiLabelRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiLabelRenderer_getTextOutlineColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setTextOutlineColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiLabelRenderer_getTextOutlineThickness(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setTextOutlineThickness(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiLabelRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiLabelRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiLabelRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiLabelRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setScrollbar(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiLabelRenderer_getScrollbarWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setScrollbarWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiLabelRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabelRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
