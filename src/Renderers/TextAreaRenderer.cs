// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for TextArea widgets
    /// </summary>
    public class TextAreaRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TextAreaRenderer()
            : base(tguiTextAreaRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal TextAreaRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public TextAreaRenderer(TextAreaRenderer copy)
            : base(tguiTextAreaRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiTextAreaRenderer_getBorders(CPointer));
            set => tguiTextAreaRenderer_setBorders(CPointer, value.CPointer);
        }

        public Outline Padding
        {
            get => new Outline(tguiTextAreaRenderer_getPadding(CPointer));
            set => tguiTextAreaRenderer_setPadding(CPointer, value.CPointer);
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiTextAreaRenderer_getBackgroundColor(CPointer));
            set => tguiTextAreaRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiTextAreaRenderer_getTextColor(CPointer));
            set => tguiTextAreaRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? DefaultTextColor
        {
            get => Util.GetColorFromC(tguiTextAreaRenderer_getDefaultTextColor(CPointer));
            set => tguiTextAreaRenderer_setDefaultTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColor
        {
            get => Util.GetColorFromC(tguiTextAreaRenderer_getSelectedTextColor(CPointer));
            set => tguiTextAreaRenderer_setSelectedTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextBackgroundColor
        {
            get => Util.GetColorFromC(tguiTextAreaRenderer_getSelectedTextBackgroundColor(CPointer));
            set => tguiTextAreaRenderer_setSelectedTextBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiTextAreaRenderer_getBorderColor(CPointer));
            set => tguiTextAreaRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? CaretColor
        {
            get => Util.GetColorFromC(tguiTextAreaRenderer_getCaretColor(CPointer));
            set => tguiTextAreaRenderer_setCaretColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiTextAreaRenderer_getTextureBackground(CPointer));
            set => tguiTextAreaRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        public float CaretWidth
        {
            get => tguiTextAreaRenderer_getCaretWidth(CPointer);
            set => tguiTextAreaRenderer_setCaretWidth(CPointer, value);
        }

        public RendererData Scrollbar
        {
            get => new RendererData(tguiTextAreaRenderer_getScrollbar(CPointer));
            set => tguiTextAreaRenderer_setScrollbar(CPointer, value.CPointer);
        }

        public float ScrollbarWidth
        {
            get => tguiTextAreaRenderer_getScrollbarWidth(CPointer);
            set => tguiTextAreaRenderer_setScrollbarWidth(CPointer, value);
        }

        public float RoundedBorderRadius
        {
            get => tguiTextAreaRenderer_getRoundedBorderRadius(CPointer);
            set => tguiTextAreaRenderer_setRoundedBorderRadius(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextAreaRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextAreaRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextAreaRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextAreaRenderer_getPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setPadding(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTextAreaRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTextAreaRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTextAreaRenderer_getDefaultTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setDefaultTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTextAreaRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setSelectedTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTextAreaRenderer_getSelectedTextBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setSelectedTextBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTextAreaRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTextAreaRenderer_getCaretColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setCaretColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextAreaRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTextAreaRenderer_getCaretWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setCaretWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextAreaRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setScrollbar(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTextAreaRenderer_getScrollbarWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setScrollbarWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTextAreaRenderer_getRoundedBorderRadius(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextAreaRenderer_setRoundedBorderRadius(IntPtr cPointer, float value);

        #endregion
    }
}
