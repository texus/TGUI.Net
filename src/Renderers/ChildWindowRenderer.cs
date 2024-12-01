// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for ChildWindow widgets
    /// </summary>
    public class ChildWindowRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ChildWindowRenderer()
            : base(tguiChildWindowRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ChildWindowRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ChildWindowRenderer(ChildWindowRenderer copy)
            : base(tguiChildWindowRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiChildWindowRenderer_getBorders(CPointer));
            set => tguiChildWindowRenderer_setBorders(CPointer, value.CPointer);
        }

        public Color? TitleBarColor
        {
            get => Util.GetColorFromC(tguiChildWindowRenderer_getTitleBarColor(CPointer));
            set => tguiChildWindowRenderer_setTitleBarColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TitleColor
        {
            get => Util.GetColorFromC(tguiChildWindowRenderer_getTitleColor(CPointer));
            set => tguiChildWindowRenderer_setTitleColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiChildWindowRenderer_getBackgroundColor(CPointer));
            set => tguiChildWindowRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiChildWindowRenderer_getBorderColor(CPointer));
            set => tguiChildWindowRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorFocused
        {
            get => Util.GetColorFromC(tguiChildWindowRenderer_getBorderColorFocused(CPointer));
            set => tguiChildWindowRenderer_setBorderColorFocused(CPointer, Util.ConvertColorForC(value));
        }

        public float BorderBelowTitleBar
        {
            get => tguiChildWindowRenderer_getBorderBelowTitleBar(CPointer);
            set => tguiChildWindowRenderer_setBorderBelowTitleBar(CPointer, value);
        }

        public float TitleBarHeight
        {
            get => tguiChildWindowRenderer_getTitleBarHeight(CPointer);
            set => tguiChildWindowRenderer_setTitleBarHeight(CPointer, value);
        }

        public float DistanceToSide
        {
            get => tguiChildWindowRenderer_getDistanceToSide(CPointer);
            set => tguiChildWindowRenderer_setDistanceToSide(CPointer, value);
        }

        public float PaddingBetweenButtons
        {
            get => tguiChildWindowRenderer_getPaddingBetweenButtons(CPointer);
            set => tguiChildWindowRenderer_setPaddingBetweenButtons(CPointer, value);
        }

        public float MinimumResizableBorderWidth
        {
            get => tguiChildWindowRenderer_getMinimumResizableBorderWidth(CPointer);
            set => tguiChildWindowRenderer_setMinimumResizableBorderWidth(CPointer, value);
        }

        public bool ShowTextOnTitleButtons
        {
            get => tguiChildWindowRenderer_getShowTextOnTitleButtons(CPointer) != 0;
            set => tguiChildWindowRenderer_setShowTextOnTitleButtons(CPointer, value ? (byte)1 : (byte)0);
        }

        public Texture TextureTitleBar
        {
            get => new Texture(tguiChildWindowRenderer_getTextureTitleBar(CPointer));
            set => tguiChildWindowRenderer_setTextureTitleBar(CPointer, value.CPointer);
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiChildWindowRenderer_getTextureBackground(CPointer));
            set => tguiChildWindowRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        public RendererData CloseButton
        {
            get => new RendererData(tguiChildWindowRenderer_getCloseButton(CPointer));
            set => tguiChildWindowRenderer_setCloseButton(CPointer, value.CPointer);
        }

        public RendererData MaximizeButton
        {
            get => new RendererData(tguiChildWindowRenderer_getMaximizeButton(CPointer));
            set => tguiChildWindowRenderer_setMaximizeButton(CPointer, value.CPointer);
        }

        public RendererData MinimizeButton
        {
            get => new RendererData(tguiChildWindowRenderer_getMinimizeButton(CPointer));
            set => tguiChildWindowRenderer_setMinimizeButton(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindowRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindowRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindowRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChildWindowRenderer_getTitleBarColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setTitleBarColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChildWindowRenderer_getTitleColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setTitleColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChildWindowRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChildWindowRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChildWindowRenderer_getBorderColorFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setBorderColorFocused(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiChildWindowRenderer_getBorderBelowTitleBar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setBorderBelowTitleBar(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiChildWindowRenderer_getTitleBarHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setTitleBarHeight(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiChildWindowRenderer_getDistanceToSide(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setDistanceToSide(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiChildWindowRenderer_getPaddingBetweenButtons(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setPaddingBetweenButtons(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiChildWindowRenderer_getMinimumResizableBorderWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setMinimumResizableBorderWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiChildWindowRenderer_getShowTextOnTitleButtons(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setShowTextOnTitleButtons(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindowRenderer_getTextureTitleBar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setTextureTitleBar(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindowRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindowRenderer_getCloseButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setCloseButton(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindowRenderer_getMaximizeButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setMaximizeButton(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindowRenderer_getMinimizeButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindowRenderer_setMinimizeButton(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
