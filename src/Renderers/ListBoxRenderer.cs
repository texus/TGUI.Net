// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for ListBox widgets
    /// </summary>
    public class ListBoxRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ListBoxRenderer()
            : base(tguiListBoxRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ListBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ListBoxRenderer(ListBoxRenderer copy)
            : base(tguiListBoxRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiListBoxRenderer_getBorders(CPointer));
            set => tguiListBoxRenderer_setBorders(CPointer, value.CPointer);
        }

        public Outline Padding
        {
            get => new Outline(tguiListBoxRenderer_getPadding(CPointer));
            set => tguiListBoxRenderer_setPadding(CPointer, value.CPointer);
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getBackgroundColor(CPointer));
            set => tguiListBoxRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorHover
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getBackgroundColorHover(CPointer));
            set => tguiListBoxRenderer_setBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBackgroundColor
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getSelectedBackgroundColor(CPointer));
            set => tguiListBoxRenderer_setSelectedBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBackgroundColorHover
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getSelectedBackgroundColorHover(CPointer));
            set => tguiListBoxRenderer_setSelectedBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getTextColor(CPointer));
            set => tguiListBoxRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorHover
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getTextColorHover(CPointer));
            set => tguiListBoxRenderer_setTextColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColor
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getSelectedTextColor(CPointer));
            set => tguiListBoxRenderer_setSelectedTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColorHover
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getSelectedTextColorHover(CPointer));
            set => tguiListBoxRenderer_setSelectedTextColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiListBoxRenderer_getBorderColor(CPointer));
            set => tguiListBoxRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiListBoxRenderer_getTextureBackground(CPointer));
            set => tguiListBoxRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        public TextStyles TextStyle
        {
            get => tguiListBoxRenderer_getTextStyle(CPointer);
            set => tguiListBoxRenderer_setTextStyle(CPointer, value);
        }

        public TextStyles SelectedTextStyle
        {
            get => tguiListBoxRenderer_getSelectedTextStyle(CPointer);
            set => tguiListBoxRenderer_setSelectedTextStyle(CPointer, value);
        }

        public RendererData Scrollbar
        {
            get => new RendererData(tguiListBoxRenderer_getScrollbar(CPointer));
            set => tguiListBoxRenderer_setScrollbar(CPointer, value.CPointer);
        }

        public float ScrollbarWidth
        {
            get => tguiListBoxRenderer_getScrollbarWidth(CPointer);
            set => tguiListBoxRenderer_setScrollbarWidth(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBoxRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBoxRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBoxRenderer_getPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setPadding(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getSelectedBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setSelectedBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getSelectedBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setSelectedBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setTextColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setSelectedTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getSelectedTextColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setSelectedTextColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListBoxRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBoxRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiListBoxRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiListBoxRenderer_getSelectedTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setSelectedTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListBoxRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setScrollbar(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiListBoxRenderer_getScrollbarWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListBoxRenderer_setScrollbarWidth(IntPtr cPointer, float value);

        #endregion
    }
}
