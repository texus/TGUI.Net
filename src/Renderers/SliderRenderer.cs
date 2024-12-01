// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for Slider widgets
    /// </summary>
    public class SliderRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SliderRenderer()
            : base(tguiSliderRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SliderRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public SliderRenderer(SliderRenderer copy)
            : base(tguiSliderRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiSliderRenderer_getBorders(CPointer));
            set => tguiSliderRenderer_setBorders(CPointer, value.CPointer);
        }

        public Color? TrackColor
        {
            get => Util.GetColorFromC(tguiSliderRenderer_getTrackColor(CPointer));
            set => tguiSliderRenderer_setTrackColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TrackColorHover
        {
            get => Util.GetColorFromC(tguiSliderRenderer_getTrackColorHover(CPointer));
            set => tguiSliderRenderer_setTrackColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ThumbColor
        {
            get => Util.GetColorFromC(tguiSliderRenderer_getThumbColor(CPointer));
            set => tguiSliderRenderer_setThumbColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ThumbColorHover
        {
            get => Util.GetColorFromC(tguiSliderRenderer_getThumbColorHover(CPointer));
            set => tguiSliderRenderer_setThumbColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiSliderRenderer_getBorderColor(CPointer));
            set => tguiSliderRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColorHover
        {
            get => Util.GetColorFromC(tguiSliderRenderer_getBorderColorHover(CPointer));
            set => tguiSliderRenderer_setBorderColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureTrack
        {
            get => new Texture(tguiSliderRenderer_getTextureTrack(CPointer));
            set => tguiSliderRenderer_setTextureTrack(CPointer, value.CPointer);
        }

        public Texture TextureTrackHover
        {
            get => new Texture(tguiSliderRenderer_getTextureTrackHover(CPointer));
            set => tguiSliderRenderer_setTextureTrackHover(CPointer, value.CPointer);
        }

        public Texture TextureThumb
        {
            get => new Texture(tguiSliderRenderer_getTextureThumb(CPointer));
            set => tguiSliderRenderer_setTextureThumb(CPointer, value.CPointer);
        }

        public Texture TextureThumbHover
        {
            get => new Texture(tguiSliderRenderer_getTextureThumbHover(CPointer));
            set => tguiSliderRenderer_setTextureThumbHover(CPointer, value.CPointer);
        }

        public bool ThumbWithinTrack
        {
            get => tguiSliderRenderer_getThumbWithinTrack(CPointer) != 0;
            set => tguiSliderRenderer_setThumbWithinTrack(CPointer, value ? (byte)1 : (byte)0);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSliderRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSliderRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSliderRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSliderRenderer_getTrackColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setTrackColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSliderRenderer_getTrackColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setTrackColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSliderRenderer_getThumbColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setThumbColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSliderRenderer_getThumbColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setThumbColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSliderRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiSliderRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setBorderColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSliderRenderer_getTextureTrack(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setTextureTrack(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSliderRenderer_getTextureTrackHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setTextureTrackHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSliderRenderer_getTextureThumb(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setTextureThumb(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSliderRenderer_getTextureThumbHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setTextureThumbHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiSliderRenderer_getThumbWithinTrack(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSliderRenderer_setThumbWithinTrack(IntPtr cPointer, byte value);

        #endregion
    }
}
