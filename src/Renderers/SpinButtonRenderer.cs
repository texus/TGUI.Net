// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for SpinButton widgets
    /// </summary>
    public class SpinButtonRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SpinButtonRenderer()
            : base(tguiSpinButtonRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SpinButtonRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public SpinButtonRenderer(SpinButtonRenderer copy)
            : base(tguiSpinButtonRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiSpinButtonRenderer_getBorders(CPointer));
            set => tguiSpinButtonRenderer_setBorders(CPointer, value.CPointer);
        }

        public float BorderBetweenArrows
        {
            get => tguiSpinButtonRenderer_getBorderBetweenArrows(CPointer);
            set => tguiSpinButtonRenderer_setBorderBetweenArrows(CPointer, value);
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiSpinButtonRenderer_getBackgroundColor(CPointer));
            set => tguiSpinButtonRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BackgroundColorHover
        {
            get => Util.GetColorFromC(tguiSpinButtonRenderer_getBackgroundColorHover(CPointer));
            set => tguiSpinButtonRenderer_setBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowColor
        {
            get => Util.GetColorFromC(tguiSpinButtonRenderer_getArrowColor(CPointer));
            set => tguiSpinButtonRenderer_setArrowColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowColorHover
        {
            get => Util.GetColorFromC(tguiSpinButtonRenderer_getArrowColorHover(CPointer));
            set => tguiSpinButtonRenderer_setArrowColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiSpinButtonRenderer_getBorderColor(CPointer));
            set => tguiSpinButtonRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureArrowUp
        {
            get => new Texture(tguiSpinButtonRenderer_getTextureArrowUp(CPointer));
            set => tguiSpinButtonRenderer_setTextureArrowUp(CPointer, value.CPointer);
        }

        public Texture TextureArrowUpHover
        {
            get => new Texture(tguiSpinButtonRenderer_getTextureArrowUpHover(CPointer));
            set => tguiSpinButtonRenderer_setTextureArrowUpHover(CPointer, value.CPointer);
        }

        public Texture TextureArrowDown
        {
            get => new Texture(tguiSpinButtonRenderer_getTextureArrowDown(CPointer));
            set => tguiSpinButtonRenderer_setTextureArrowDown(CPointer, value.CPointer);
        }

        public Texture TextureArrowDownHover
        {
            get => new Texture(tguiSpinButtonRenderer_getTextureArrowDownHover(CPointer));
            set => tguiSpinButtonRenderer_setTextureArrowDownHover(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinButtonRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinButtonRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinButtonRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinButtonRenderer_getBorderBetweenArrows(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setBorderBetweenArrows(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSpinButtonRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSpinButtonRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSpinButtonRenderer_getArrowColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setArrowColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSpinButtonRenderer_getArrowColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setArrowColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSpinButtonRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinButtonRenderer_getTextureArrowUp(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setTextureArrowUp(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinButtonRenderer_getTextureArrowUpHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setTextureArrowUpHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinButtonRenderer_getTextureArrowDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setTextureArrowDown(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinButtonRenderer_getTextureArrowDownHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButtonRenderer_setTextureArrowDownHover(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
