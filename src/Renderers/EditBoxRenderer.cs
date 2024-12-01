// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for EditBox widgets
    /// </summary>
    public class EditBoxRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EditBoxRenderer()
            : base(tguiEditBoxRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal EditBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public EditBoxRenderer(EditBoxRenderer copy)
            : base(tguiEditBoxRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiEditBoxRenderer_getBorders(CPointer));
            set => tguiEditBoxRenderer_setBorders(CPointer, value.CPointer);
        }

        public Outline Padding
        {
            get => new Outline(tguiEditBoxRenderer_getPadding(CPointer));
            set => tguiEditBoxRenderer_setPadding(CPointer, value.CPointer);
        }

        public float CaretWidth
        {
            get => tguiEditBoxRenderer_getCaretWidth(CPointer);
            set => tguiEditBoxRenderer_setCaretWidth(CPointer, value);
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getTextColor(CPointer));
            set => tguiEditBoxRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? DefaultTextColor
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getDefaultTextColor(CPointer));
            set => tguiEditBoxRenderer_setDefaultTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorFocused
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getTextColorFocused(CPointer));
            set => tguiEditBoxRenderer_setTextColorFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDisabled
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getTextColorDisabled(CPointer));
            set => tguiEditBoxRenderer_setTextColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColor
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getSelectedTextColor(CPointer));
            set => tguiEditBoxRenderer_setSelectedTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextBackgroundColor
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getSelectedTextBackgroundColor(CPointer));
            set => tguiEditBoxRenderer_setSelectedTextBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getBackgroundColor(CPointer));
            set => tguiEditBoxRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorHover
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getBackgroundColorHover(CPointer));
            set => tguiEditBoxRenderer_setBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorFocused
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getBackgroundColorFocused(CPointer));
            set => tguiEditBoxRenderer_setBackgroundColorFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDisabled
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getBackgroundColorDisabled(CPointer));
            set => tguiEditBoxRenderer_setBackgroundColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? CaretColor
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getCaretColor(CPointer));
            set => tguiEditBoxRenderer_setCaretColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? CaretColorHover
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getCaretColorHover(CPointer));
            set => tguiEditBoxRenderer_setCaretColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getBorderColor(CPointer));
            set => tguiEditBoxRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorHover
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getBorderColorHover(CPointer));
            set => tguiEditBoxRenderer_setBorderColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorFocused
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getBorderColorFocused(CPointer));
            set => tguiEditBoxRenderer_setBorderColorFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorDisabled
        {
            get => Util.GetColorFromC(tguiEditBoxRenderer_getBorderColorDisabled(CPointer));
            set => tguiEditBoxRenderer_setBorderColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Texture Texture
        {
            get => new Texture(tguiEditBoxRenderer_getTexture(CPointer));
            set => tguiEditBoxRenderer_setTexture(CPointer, value.CPointer);
        }

        public Texture TextureHover
        {
            get => new Texture(tguiEditBoxRenderer_getTextureHover(CPointer));
            set => tguiEditBoxRenderer_setTextureHover(CPointer, value.CPointer);
        }

        public Texture TextureFocused
        {
            get => new Texture(tguiEditBoxRenderer_getTextureFocused(CPointer));
            set => tguiEditBoxRenderer_setTextureFocused(CPointer, value.CPointer);
        }

        public Texture TextureDisabled
        {
            get => new Texture(tguiEditBoxRenderer_getTextureDisabled(CPointer));
            set => tguiEditBoxRenderer_setTextureDisabled(CPointer, value.CPointer);
        }

        public TextStyles TextStyle
        {
            get => tguiEditBoxRenderer_getTextStyle(CPointer);
            set => tguiEditBoxRenderer_setTextStyle(CPointer, value);
        }

        public TextStyles DefaultTextStyle
        {
            get => tguiEditBoxRenderer_getDefaultTextStyle(CPointer);
            set => tguiEditBoxRenderer_setDefaultTextStyle(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxRenderer_getPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setPadding(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiEditBoxRenderer_getCaretWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setCaretWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getDefaultTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setDefaultTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getTextColorFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setTextColorFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setTextColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setSelectedTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getSelectedTextBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setSelectedTextBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getBackgroundColorFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBackgroundColorFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBackgroundColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getCaretColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setCaretColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getCaretColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setCaretColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBorderColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getBorderColorFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBorderColorFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiEditBoxRenderer_getBorderColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setBorderColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxRenderer_getTexture(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setTexture(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxRenderer_getTextureHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setTextureHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxRenderer_getTextureFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setTextureFocused(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxRenderer_getTextureDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setTextureDisabled(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiEditBoxRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiEditBoxRenderer_getDefaultTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxRenderer_setDefaultTextStyle(IntPtr cPointer, TextStyles value);

        #endregion
    }
}
