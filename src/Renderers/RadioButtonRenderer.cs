// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for RadioButton widgets
    /// </summary>
    public class RadioButtonRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RadioButtonRenderer()
            : base(tguiRadioButtonRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal RadioButtonRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public RadioButtonRenderer(RadioButtonRenderer copy)
            : base(tguiRadioButtonRenderer_copy(copy.CPointer))
        {
        }

        public float TextDistanceRatio
        {
            get => tguiRadioButtonRenderer_getTextDistanceRatio(CPointer);
            set => tguiRadioButtonRenderer_setTextDistanceRatio(CPointer, value);
        }

        public Outline Borders
        {
            get => new Outline(tguiRadioButtonRenderer_getBorders(CPointer));
            set => tguiRadioButtonRenderer_setBorders(CPointer, value.CPointer);
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getTextColor(CPointer));
            set => tguiRadioButtonRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorHover
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getTextColorHover(CPointer));
            set => tguiRadioButtonRenderer_setTextColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDisabled
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getTextColorDisabled(CPointer));
            set => tguiRadioButtonRenderer_setTextColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorChecked
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getTextColorChecked(CPointer));
            set => tguiRadioButtonRenderer_setTextColorChecked(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorCheckedHover
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getTextColorCheckedHover(CPointer));
            set => tguiRadioButtonRenderer_setTextColorCheckedHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorCheckedDisabled
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getTextColorCheckedDisabled(CPointer));
            set => tguiRadioButtonRenderer_setTextColorCheckedDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBackgroundColor(CPointer));
            set => tguiRadioButtonRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorHover
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBackgroundColorHover(CPointer));
            set => tguiRadioButtonRenderer_setBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDisabled
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBackgroundColorDisabled(CPointer));
            set => tguiRadioButtonRenderer_setBackgroundColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorChecked
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBackgroundColorChecked(CPointer));
            set => tguiRadioButtonRenderer_setBackgroundColorChecked(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorCheckedHover
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBackgroundColorCheckedHover(CPointer));
            set => tguiRadioButtonRenderer_setBackgroundColorCheckedHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorCheckedDisabled
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBackgroundColorCheckedDisabled(CPointer));
            set => tguiRadioButtonRenderer_setBackgroundColorCheckedDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBorderColor(CPointer));
            set => tguiRadioButtonRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorHover
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBorderColorHover(CPointer));
            set => tguiRadioButtonRenderer_setBorderColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorFocused
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBorderColorFocused(CPointer));
            set => tguiRadioButtonRenderer_setBorderColorFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorDisabled
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBorderColorDisabled(CPointer));
            set => tguiRadioButtonRenderer_setBorderColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorChecked
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBorderColorChecked(CPointer));
            set => tguiRadioButtonRenderer_setBorderColorChecked(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorCheckedHover
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBorderColorCheckedHover(CPointer));
            set => tguiRadioButtonRenderer_setBorderColorCheckedHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorCheckedFocused
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBorderColorCheckedFocused(CPointer));
            set => tguiRadioButtonRenderer_setBorderColorCheckedFocused(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorCheckedDisabled
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getBorderColorCheckedDisabled(CPointer));
            set => tguiRadioButtonRenderer_setBorderColorCheckedDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? CheckColor
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getCheckColor(CPointer));
            set => tguiRadioButtonRenderer_setCheckColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? CheckColorHover
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getCheckColorHover(CPointer));
            set => tguiRadioButtonRenderer_setCheckColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? CheckColorDisabled
        {
            get => Util.GetColorFromC(tguiRadioButtonRenderer_getCheckColorDisabled(CPointer));
            set => tguiRadioButtonRenderer_setCheckColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureUnchecked
        {
            get => new Texture(tguiRadioButtonRenderer_getTextureUnchecked(CPointer));
            set => tguiRadioButtonRenderer_setTextureUnchecked(CPointer, value.CPointer);
        }

        public Texture TextureChecked
        {
            get => new Texture(tguiRadioButtonRenderer_getTextureChecked(CPointer));
            set => tguiRadioButtonRenderer_setTextureChecked(CPointer, value.CPointer);
        }

        public Texture TextureUncheckedHover
        {
            get => new Texture(tguiRadioButtonRenderer_getTextureUncheckedHover(CPointer));
            set => tguiRadioButtonRenderer_setTextureUncheckedHover(CPointer, value.CPointer);
        }

        public Texture TextureCheckedHover
        {
            get => new Texture(tguiRadioButtonRenderer_getTextureCheckedHover(CPointer));
            set => tguiRadioButtonRenderer_setTextureCheckedHover(CPointer, value.CPointer);
        }

        public Texture TextureUncheckedFocused
        {
            get => new Texture(tguiRadioButtonRenderer_getTextureUncheckedFocused(CPointer));
            set => tguiRadioButtonRenderer_setTextureUncheckedFocused(CPointer, value.CPointer);
        }

        public Texture TextureCheckedFocused
        {
            get => new Texture(tguiRadioButtonRenderer_getTextureCheckedFocused(CPointer));
            set => tguiRadioButtonRenderer_setTextureCheckedFocused(CPointer, value.CPointer);
        }

        public Texture TextureUncheckedDisabled
        {
            get => new Texture(tguiRadioButtonRenderer_getTextureUncheckedDisabled(CPointer));
            set => tguiRadioButtonRenderer_setTextureUncheckedDisabled(CPointer, value.CPointer);
        }

        public Texture TextureCheckedDisabled
        {
            get => new Texture(tguiRadioButtonRenderer_getTextureCheckedDisabled(CPointer));
            set => tguiRadioButtonRenderer_setTextureCheckedDisabled(CPointer, value.CPointer);
        }

        public TextStyles TextStyle
        {
            get => tguiRadioButtonRenderer_getTextStyle(CPointer);
            set => tguiRadioButtonRenderer_setTextStyle(CPointer, value);
        }

        public TextStyles TextStyleChecked
        {
            get => tguiRadioButtonRenderer_getTextStyleChecked(CPointer);
            set => tguiRadioButtonRenderer_setTextStyleChecked(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiRadioButtonRenderer_getTextDistanceRatio(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextDistanceRatio(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getTextColorChecked(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextColorChecked(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getTextColorCheckedHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextColorCheckedHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getTextColorCheckedDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextColorCheckedDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBackgroundColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBackgroundColorChecked(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBackgroundColorChecked(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBackgroundColorCheckedHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBackgroundColorCheckedHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBackgroundColorCheckedDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBackgroundColorCheckedDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorderColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBorderColorFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorderColorFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBorderColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorderColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBorderColorChecked(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorderColorChecked(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBorderColorCheckedHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorderColorCheckedHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBorderColorCheckedFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorderColorCheckedFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getBorderColorCheckedDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setBorderColorCheckedDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getCheckColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setCheckColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getCheckColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setCheckColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRadioButtonRenderer_getCheckColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setCheckColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getTextureUnchecked(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextureUnchecked(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getTextureChecked(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextureChecked(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getTextureUncheckedHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextureUncheckedHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getTextureCheckedHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextureCheckedHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getTextureUncheckedFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextureUncheckedFocused(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getTextureCheckedFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextureCheckedFocused(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getTextureUncheckedDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextureUncheckedDisabled(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonRenderer_getTextureCheckedDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextureCheckedDisabled(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiRadioButtonRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiRadioButtonRenderer_getTextStyleChecked(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonRenderer_setTextStyleChecked(IntPtr cPointer, TextStyles value);

        #endregion
    }
}
