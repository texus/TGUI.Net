// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for Tabs widgets
    /// </summary>
    public class TabsRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TabsRenderer()
            : base(tguiTabsRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal TabsRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public TabsRenderer(TabsRenderer copy)
            : base(tguiTabsRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiTabsRenderer_getBorders(CPointer));
            set => tguiTabsRenderer_setBorders(CPointer, value.CPointer);
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getBackgroundColor(CPointer));
            set => tguiTabsRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorHover
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getBackgroundColorHover(CPointer));
            set => tguiTabsRenderer_setBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBackgroundColor
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getSelectedBackgroundColor(CPointer));
            set => tguiTabsRenderer_setSelectedBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBackgroundColorHover
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getSelectedBackgroundColorHover(CPointer));
            set => tguiTabsRenderer_setSelectedBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorDisabled
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getBackgroundColorDisabled(CPointer));
            set => tguiTabsRenderer_setBackgroundColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getTextColor(CPointer));
            set => tguiTabsRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorHover
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getTextColorHover(CPointer));
            set => tguiTabsRenderer_setTextColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColor
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getSelectedTextColor(CPointer));
            set => tguiTabsRenderer_setSelectedTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColorHover
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getSelectedTextColorHover(CPointer));
            set => tguiTabsRenderer_setSelectedTextColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDisabled
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getTextColorDisabled(CPointer));
            set => tguiTabsRenderer_setTextColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getBorderColor(CPointer));
            set => tguiTabsRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorHover
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getBorderColorHover(CPointer));
            set => tguiTabsRenderer_setBorderColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBorderColor
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getSelectedBorderColor(CPointer));
            set => tguiTabsRenderer_setSelectedBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBorderColorHover
        {
            get => Util.GetColorFromC(tguiTabsRenderer_getSelectedBorderColorHover(CPointer));
            set => tguiTabsRenderer_setSelectedBorderColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureTab
        {
            get => new Texture(tguiTabsRenderer_getTextureTab(CPointer));
            set => tguiTabsRenderer_setTextureTab(CPointer, value.CPointer);
        }

        public Texture TextureTabHover
        {
            get => new Texture(tguiTabsRenderer_getTextureTabHover(CPointer));
            set => tguiTabsRenderer_setTextureTabHover(CPointer, value.CPointer);
        }

        public Texture TextureSelectedTab
        {
            get => new Texture(tguiTabsRenderer_getTextureSelectedTab(CPointer));
            set => tguiTabsRenderer_setTextureSelectedTab(CPointer, value.CPointer);
        }

        public Texture TextureSelectedTabHover
        {
            get => new Texture(tguiTabsRenderer_getTextureSelectedTabHover(CPointer));
            set => tguiTabsRenderer_setTextureSelectedTabHover(CPointer, value.CPointer);
        }

        public Texture TextureDisabledTab
        {
            get => new Texture(tguiTabsRenderer_getTextureDisabledTab(CPointer));
            set => tguiTabsRenderer_setTextureDisabledTab(CPointer, value.CPointer);
        }

        public float DistanceToSide
        {
            get => tguiTabsRenderer_getDistanceToSide(CPointer);
            set => tguiTabsRenderer_setDistanceToSide(CPointer, value);
        }

        public float RoundedBorderRadius
        {
            get => tguiTabsRenderer_getRoundedBorderRadius(CPointer);
            set => tguiTabsRenderer_setRoundedBorderRadius(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getSelectedBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setSelectedBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getSelectedBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setSelectedBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setBackgroundColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setTextColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setSelectedTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getSelectedTextColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setSelectedTextColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setTextColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setBorderColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getSelectedBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setSelectedBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTabsRenderer_getSelectedBorderColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setSelectedBorderColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsRenderer_getTextureTab(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setTextureTab(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsRenderer_getTextureTabHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setTextureTabHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsRenderer_getTextureSelectedTab(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setTextureSelectedTab(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsRenderer_getTextureSelectedTabHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setTextureSelectedTabHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabsRenderer_getTextureDisabledTab(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setTextureDisabledTab(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTabsRenderer_getDistanceToSide(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setDistanceToSide(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTabsRenderer_getRoundedBorderRadius(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabsRenderer_setRoundedBorderRadius(IntPtr cPointer, float value);

        #endregion
    }
}
