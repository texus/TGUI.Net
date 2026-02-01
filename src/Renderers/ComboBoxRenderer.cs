// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for ComboBox widgets
    /// </summary>
    public class ComboBoxRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ComboBoxRenderer()
            : base(tguiComboBoxRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ComboBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ComboBoxRenderer(ComboBoxRenderer copy)
            : base(tguiComboBoxRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiComboBoxRenderer_getBorders(CPointer));
            set => tguiComboBoxRenderer_setBorders(CPointer, value.CPointer);
        }

        public Outline Padding
        {
            get => new Outline(tguiComboBoxRenderer_getPadding(CPointer));
            set => tguiComboBoxRenderer_setPadding(CPointer, value.CPointer);
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getBackgroundColor(CPointer));
            set => tguiComboBoxRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDisabled
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getBackgroundColorDisabled(CPointer));
            set => tguiComboBoxRenderer_setBackgroundColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getTextColor(CPointer));
            set => tguiComboBoxRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDisabled
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getTextColorDisabled(CPointer));
            set => tguiComboBoxRenderer_setTextColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? DefaultTextColor
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getDefaultTextColor(CPointer));
            set => tguiComboBoxRenderer_setDefaultTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowBackgroundColor
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getArrowBackgroundColor(CPointer));
            set => tguiComboBoxRenderer_setArrowBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowBackgroundColorHover
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getArrowBackgroundColorHover(CPointer));
            set => tguiComboBoxRenderer_setArrowBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowBackgroundColorDisabled
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getArrowBackgroundColorDisabled(CPointer));
            set => tguiComboBoxRenderer_setArrowBackgroundColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowColor
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getArrowColor(CPointer));
            set => tguiComboBoxRenderer_setArrowColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowColorHover
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getArrowColorHover(CPointer));
            set => tguiComboBoxRenderer_setArrowColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowColorDisabled
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getArrowColorDisabled(CPointer));
            set => tguiComboBoxRenderer_setArrowColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiComboBoxRenderer_getBorderColor(CPointer));
            set => tguiComboBoxRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiComboBoxRenderer_getTextureBackground(CPointer));
            set => tguiComboBoxRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        public Texture TextureBackgroundDisabled
        {
            get => new Texture(tguiComboBoxRenderer_getTextureBackgroundDisabled(CPointer));
            set => tguiComboBoxRenderer_setTextureBackgroundDisabled(CPointer, value.CPointer);
        }

        public Texture TextureArrow
        {
            get => new Texture(tguiComboBoxRenderer_getTextureArrow(CPointer));
            set => tguiComboBoxRenderer_setTextureArrow(CPointer, value.CPointer);
        }

        public Texture TextureArrowHover
        {
            get => new Texture(tguiComboBoxRenderer_getTextureArrowHover(CPointer));
            set => tguiComboBoxRenderer_setTextureArrowHover(CPointer, value.CPointer);
        }

        public Texture TextureArrowDisabled
        {
            get => new Texture(tguiComboBoxRenderer_getTextureArrowDisabled(CPointer));
            set => tguiComboBoxRenderer_setTextureArrowDisabled(CPointer, value.CPointer);
        }

        public TextStyles TextStyle
        {
            get => tguiComboBoxRenderer_getTextStyle(CPointer);
            set => tguiComboBoxRenderer_setTextStyle(CPointer, value);
        }

        public TextStyles DefaultTextStyle
        {
            get => tguiComboBoxRenderer_getDefaultTextStyle(CPointer);
            set => tguiComboBoxRenderer_setDefaultTextStyle(CPointer, value);
        }

        public RendererData ListBox
        {
            get => new RendererData(tguiComboBoxRenderer_getListBox(CPointer));
            set => tguiComboBoxRenderer_setListBox(CPointer, value.CPointer);
        }

        public float RoundedBorderRadius
        {
            get => tguiComboBoxRenderer_getRoundedBorderRadius(CPointer);
            set => tguiComboBoxRenderer_setRoundedBorderRadius(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_getPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setPadding(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setBackgroundColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setTextColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getDefaultTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setDefaultTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getArrowBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setArrowBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getArrowBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setArrowBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getArrowBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setArrowBackgroundColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getArrowColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setArrowColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getArrowColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setArrowColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getArrowColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setArrowColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiComboBoxRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_getTextureBackgroundDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setTextureBackgroundDisabled(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_getTextureArrow(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setTextureArrow(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_getTextureArrowHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setTextureArrowHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_getTextureArrowDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setTextureArrowDisabled(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiComboBoxRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiComboBoxRenderer_getDefaultTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setDefaultTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiComboBoxRenderer_getListBox(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setListBox(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiComboBoxRenderer_getRoundedBorderRadius(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiComboBoxRenderer_setRoundedBorderRadius(IntPtr cPointer, float value);

        #endregion
    }
}
