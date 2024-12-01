// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for ListView widgets
    /// </summary>
    public class ListViewRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ListViewRenderer()
            : base(tguiListViewRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ListViewRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ListViewRenderer(ListViewRenderer copy)
            : base(tguiListViewRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiListViewRenderer_getBorders(CPointer));
            set => tguiListViewRenderer_setBorders(CPointer, value.CPointer);
        }

        public Outline Padding
        {
            get => new Outline(tguiListViewRenderer_getPadding(CPointer));
            set => tguiListViewRenderer_setPadding(CPointer, value.CPointer);
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getBackgroundColor(CPointer));
            set => tguiListViewRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorHover
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getBackgroundColorHover(CPointer));
            set => tguiListViewRenderer_setBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBackgroundColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getSelectedBackgroundColor(CPointer));
            set => tguiListViewRenderer_setSelectedBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBackgroundColorHover
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getSelectedBackgroundColorHover(CPointer));
            set => tguiListViewRenderer_setSelectedBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getTextColor(CPointer));
            set => tguiListViewRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorHover
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getTextColorHover(CPointer));
            set => tguiListViewRenderer_setTextColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getSelectedTextColor(CPointer));
            set => tguiListViewRenderer_setSelectedTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColorHover
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getSelectedTextColorHover(CPointer));
            set => tguiListViewRenderer_setSelectedTextColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? HeaderBackgroundColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getHeaderBackgroundColor(CPointer));
            set => tguiListViewRenderer_setHeaderBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? HeaderTextColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getHeaderTextColor(CPointer));
            set => tguiListViewRenderer_setHeaderTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getBorderColor(CPointer));
            set => tguiListViewRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SeparatorColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getSeparatorColor(CPointer));
            set => tguiListViewRenderer_setSeparatorColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? GridLinesColor
        {
            get => Util.GetColorFromC(tguiListViewRenderer_getGridLinesColor(CPointer));
            set => tguiListViewRenderer_setGridLinesColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureHeaderBackground
        {
            get => new Texture(tguiListViewRenderer_getTextureHeaderBackground(CPointer));
            set => tguiListViewRenderer_setTextureHeaderBackground(CPointer, value.CPointer);
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiListViewRenderer_getTextureBackground(CPointer));
            set => tguiListViewRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        public RendererData Scrollbar
        {
            get => new RendererData(tguiListViewRenderer_getScrollbar(CPointer));
            set => tguiListViewRenderer_setScrollbar(CPointer, value.CPointer);
        }

        public float ScrollbarWidth
        {
            get => tguiListViewRenderer_getScrollbarWidth(CPointer);
            set => tguiListViewRenderer_setScrollbarWidth(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListViewRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListViewRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListViewRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListViewRenderer_getPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setPadding(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getSelectedBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setSelectedBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getSelectedBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setSelectedBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setTextColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setSelectedTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getSelectedTextColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setSelectedTextColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getHeaderBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setHeaderBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getHeaderTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setHeaderTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getSeparatorColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setSeparatorColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiListViewRenderer_getGridLinesColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setGridLinesColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListViewRenderer_getTextureHeaderBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setTextureHeaderBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListViewRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiListViewRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setScrollbar(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiListViewRenderer_getScrollbarWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiListViewRenderer_setScrollbarWidth(IntPtr cPointer, float value);

        #endregion
    }
}
