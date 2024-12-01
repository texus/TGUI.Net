// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for Button widgets
    /// </summary>
    public class ButtonRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ButtonRenderer()
            : base(tguiButtonRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ButtonRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ButtonRenderer(ButtonRenderer copy)
            : base(tguiButtonRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiButtonRenderer_getBorders(CPointer));
            set => tguiButtonRenderer_setBorders(CPointer, value.CPointer);
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextColor(CPointer));
            set => tguiButtonRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorHover
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextColorHover(CPointer));
            set => tguiButtonRenderer_setTextColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorFocused
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextColorFocused(CPointer));
            set => tguiButtonRenderer_setTextColorFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDisabled
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextColorDisabled(CPointer));
            set => tguiButtonRenderer_setTextColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDown
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextColorDown(CPointer));
            set => tguiButtonRenderer_setTextColorDown(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDownHover
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextColorDownHover(CPointer));
            set => tguiButtonRenderer_setTextColorDownHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDownFocused
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextColorDownFocused(CPointer));
            set => tguiButtonRenderer_setTextColorDownFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDownDisabled
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextColorDownDisabled(CPointer));
            set => tguiButtonRenderer_setTextColorDownDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBackgroundColor(CPointer));
            set => tguiButtonRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorHover
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBackgroundColorHover(CPointer));
            set => tguiButtonRenderer_setBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorFocused
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBackgroundColorFocused(CPointer));
            set => tguiButtonRenderer_setBackgroundColorFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDisabled
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBackgroundColorDisabled(CPointer));
            set => tguiButtonRenderer_setBackgroundColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDown
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBackgroundColorDown(CPointer));
            set => tguiButtonRenderer_setBackgroundColorDown(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDownHover
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBackgroundColorDownHover(CPointer));
            set => tguiButtonRenderer_setBackgroundColorDownHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDownFocused
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBackgroundColorDownFocused(CPointer));
            set => tguiButtonRenderer_setBackgroundColorDownFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDownDisabled
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBackgroundColorDownDisabled(CPointer));
            set => tguiButtonRenderer_setBackgroundColorDownDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBorderColor(CPointer));
            set => tguiButtonRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorHover
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBorderColorHover(CPointer));
            set => tguiButtonRenderer_setBorderColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorFocused
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBorderColorFocused(CPointer));
            set => tguiButtonRenderer_setBorderColorFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorDisabled
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBorderColorDisabled(CPointer));
            set => tguiButtonRenderer_setBorderColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorDown
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBorderColorDown(CPointer));
            set => tguiButtonRenderer_setBorderColorDown(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorDownHover
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBorderColorDownHover(CPointer));
            set => tguiButtonRenderer_setBorderColorDownHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorDownFocused
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBorderColorDownFocused(CPointer));
            set => tguiButtonRenderer_setBorderColorDownFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorDownDisabled
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getBorderColorDownDisabled(CPointer));
            set => tguiButtonRenderer_setBorderColorDownDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Texture Texture
        {
            get => new Texture(tguiButtonRenderer_getTexture(CPointer));
            set => tguiButtonRenderer_setTexture(CPointer, value.CPointer);
        }

        public Texture TextureHover
        {
            get => new Texture(tguiButtonRenderer_getTextureHover(CPointer));
            set => tguiButtonRenderer_setTextureHover(CPointer, value.CPointer);
        }

        public Texture TextureFocused
        {
            get => new Texture(tguiButtonRenderer_getTextureFocused(CPointer));
            set => tguiButtonRenderer_setTextureFocused(CPointer, value.CPointer);
        }

        public Texture TextureDisabled
        {
            get => new Texture(tguiButtonRenderer_getTextureDisabled(CPointer));
            set => tguiButtonRenderer_setTextureDisabled(CPointer, value.CPointer);
        }

        public Texture TextureDown
        {
            get => new Texture(tguiButtonRenderer_getTextureDown(CPointer));
            set => tguiButtonRenderer_setTextureDown(CPointer, value.CPointer);
        }

        public Texture TextureDownHover
        {
            get => new Texture(tguiButtonRenderer_getTextureDownHover(CPointer));
            set => tguiButtonRenderer_setTextureDownHover(CPointer, value.CPointer);
        }

        public Texture TextureDownFocused
        {
            get => new Texture(tguiButtonRenderer_getTextureDownFocused(CPointer));
            set => tguiButtonRenderer_setTextureDownFocused(CPointer, value.CPointer);
        }

        public Texture TextureDownDisabled
        {
            get => new Texture(tguiButtonRenderer_getTextureDownDisabled(CPointer));
            set => tguiButtonRenderer_setTextureDownDisabled(CPointer, value.CPointer);
        }

        public TextStyles TextStyle
        {
            get => tguiButtonRenderer_getTextStyle(CPointer);
            set => tguiButtonRenderer_setTextStyle(CPointer, value);
        }

        public TextStyles TextStyleHover
        {
            get => tguiButtonRenderer_getTextStyleHover(CPointer);
            set => tguiButtonRenderer_setTextStyleHover(CPointer, value);
        }

        public TextStyles TextStyleFocused
        {
            get => tguiButtonRenderer_getTextStyleFocused(CPointer);
            set => tguiButtonRenderer_setTextStyleFocused(CPointer, value);
        }

        public TextStyles TextStyleDisabled
        {
            get => tguiButtonRenderer_getTextStyleDisabled(CPointer);
            set => tguiButtonRenderer_setTextStyleDisabled(CPointer, value);
        }

        public TextStyles TextStyleDown
        {
            get => tguiButtonRenderer_getTextStyleDown(CPointer);
            set => tguiButtonRenderer_setTextStyleDown(CPointer, value);
        }

        public TextStyles TextStyleDownHover
        {
            get => tguiButtonRenderer_getTextStyleDownHover(CPointer);
            set => tguiButtonRenderer_setTextStyleDownHover(CPointer, value);
        }

        public TextStyles TextStyleDownFocused
        {
            get => tguiButtonRenderer_getTextStyleDownFocused(CPointer);
            set => tguiButtonRenderer_setTextStyleDownFocused(CPointer, value);
        }

        public TextStyles TextStyleDownDisabled
        {
            get => tguiButtonRenderer_getTextStyleDownDisabled(CPointer);
            set => tguiButtonRenderer_setTextStyleDownDisabled(CPointer, value);
        }

        public Color? TextOutlineColor
        {
            get => Util.GetColorFromC(tguiButtonRenderer_getTextOutlineColor(CPointer));
            set => tguiButtonRenderer_setTextOutlineColor(CPointer, Util.ConvertColorForC(value));
        }

        public float TextOutlineThickness
        {
            get => tguiButtonRenderer_getTextOutlineThickness(CPointer);
            set => tguiButtonRenderer_setTextOutlineThickness(CPointer, value);
        }

        public float RoundedBorderRadius
        {
            get => tguiButtonRenderer_getRoundedBorderRadius(CPointer);
            set => tguiButtonRenderer_setRoundedBorderRadius(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextColorFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextColorFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextColorDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextColorDown(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextColorDownHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextColorDownHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextColorDownFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextColorDownFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextColorDownDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextColorDownDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBackgroundColorFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBackgroundColorFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBackgroundColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBackgroundColorDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBackgroundColorDown(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBackgroundColorDownHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBackgroundColorDownHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBackgroundColorDownFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBackgroundColorDownFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBackgroundColorDownDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBackgroundColorDownDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorderColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBorderColorFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorderColorFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBorderColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorderColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBorderColorDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorderColorDown(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBorderColorDownHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorderColorDownHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBorderColorDownFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorderColorDownFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getBorderColorDownDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setBorderColorDownDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getTexture(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTexture(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getTextureHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextureHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getTextureFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextureFocused(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getTextureDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextureDisabled(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getTextureDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextureDown(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getTextureDownHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextureDownHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getTextureDownFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextureDownFocused(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonRenderer_getTextureDownDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextureDownDisabled(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiButtonRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiButtonRenderer_getTextStyleHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextStyleHover(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiButtonRenderer_getTextStyleFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextStyleFocused(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiButtonRenderer_getTextStyleDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextStyleDisabled(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiButtonRenderer_getTextStyleDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextStyleDown(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiButtonRenderer_getTextStyleDownHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextStyleDownHover(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiButtonRenderer_getTextStyleDownFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextStyleDownFocused(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiButtonRenderer_getTextStyleDownDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextStyleDownDisabled(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiButtonRenderer_getTextOutlineColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextOutlineColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiButtonRenderer_getTextOutlineThickness(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setTextOutlineThickness(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiButtonRenderer_getRoundedBorderRadius(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonRenderer_setRoundedBorderRadius(IntPtr cPointer, float value);

        #endregion
    }
}
