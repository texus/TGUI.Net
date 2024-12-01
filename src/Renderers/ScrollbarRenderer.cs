// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for Scrollbar widgets
    /// </summary>
    public class ScrollbarRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ScrollbarRenderer()
            : base(tguiScrollbarRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ScrollbarRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ScrollbarRenderer(ScrollbarRenderer copy)
            : base(tguiScrollbarRenderer_copy(copy.CPointer))
        {
        }

        public Color? TrackColor
        {
            get => Util.GetColorFromC(tguiScrollbarRenderer_getTrackColor(CPointer));
            set => tguiScrollbarRenderer_setTrackColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TrackColorHover
        {
            get => Util.GetColorFromC(tguiScrollbarRenderer_getTrackColorHover(CPointer));
            set => tguiScrollbarRenderer_setTrackColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ThumbColor
        {
            get => Util.GetColorFromC(tguiScrollbarRenderer_getThumbColor(CPointer));
            set => tguiScrollbarRenderer_setThumbColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ThumbColorHover
        {
            get => Util.GetColorFromC(tguiScrollbarRenderer_getThumbColorHover(CPointer));
            set => tguiScrollbarRenderer_setThumbColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowBackgroundColor
        {
            get => Util.GetColorFromC(tguiScrollbarRenderer_getArrowBackgroundColor(CPointer));
            set => tguiScrollbarRenderer_setArrowBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowBackgroundColorHover
        {
            get => Util.GetColorFromC(tguiScrollbarRenderer_getArrowBackgroundColorHover(CPointer));
            set => tguiScrollbarRenderer_setArrowBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowColor
        {
            get => Util.GetColorFromC(tguiScrollbarRenderer_getArrowColor(CPointer));
            set => tguiScrollbarRenderer_setArrowColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ArrowColorHover
        {
            get => Util.GetColorFromC(tguiScrollbarRenderer_getArrowColorHover(CPointer));
            set => tguiScrollbarRenderer_setArrowColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureTrack
        {
            get => new Texture(tguiScrollbarRenderer_getTextureTrack(CPointer));
            set => tguiScrollbarRenderer_setTextureTrack(CPointer, value.CPointer);
        }

        public Texture TextureTrackHover
        {
            get => new Texture(tguiScrollbarRenderer_getTextureTrackHover(CPointer));
            set => tguiScrollbarRenderer_setTextureTrackHover(CPointer, value.CPointer);
        }

        public Texture TextureThumb
        {
            get => new Texture(tguiScrollbarRenderer_getTextureThumb(CPointer));
            set => tguiScrollbarRenderer_setTextureThumb(CPointer, value.CPointer);
        }

        public Texture TextureThumbHover
        {
            get => new Texture(tguiScrollbarRenderer_getTextureThumbHover(CPointer));
            set => tguiScrollbarRenderer_setTextureThumbHover(CPointer, value.CPointer);
        }

        public Texture TextureArrowUp
        {
            get => new Texture(tguiScrollbarRenderer_getTextureArrowUp(CPointer));
            set => tguiScrollbarRenderer_setTextureArrowUp(CPointer, value.CPointer);
        }

        public Texture TextureArrowUpHover
        {
            get => new Texture(tguiScrollbarRenderer_getTextureArrowUpHover(CPointer));
            set => tguiScrollbarRenderer_setTextureArrowUpHover(CPointer, value.CPointer);
        }

        public Texture TextureArrowDown
        {
            get => new Texture(tguiScrollbarRenderer_getTextureArrowDown(CPointer));
            set => tguiScrollbarRenderer_setTextureArrowDown(CPointer, value.CPointer);
        }

        public Texture TextureArrowDownHover
        {
            get => new Texture(tguiScrollbarRenderer_getTextureArrowDownHover(CPointer));
            set => tguiScrollbarRenderer_setTextureArrowDownHover(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiScrollbarRenderer_getTrackColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTrackColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiScrollbarRenderer_getTrackColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTrackColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiScrollbarRenderer_getThumbColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setThumbColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiScrollbarRenderer_getThumbColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setThumbColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiScrollbarRenderer_getArrowBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setArrowBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiScrollbarRenderer_getArrowBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setArrowBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiScrollbarRenderer_getArrowColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setArrowColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiScrollbarRenderer_getArrowColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setArrowColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_getTextureTrack(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTextureTrack(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_getTextureTrackHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTextureTrackHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_getTextureThumb(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTextureThumb(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_getTextureThumbHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTextureThumbHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_getTextureArrowUp(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTextureArrowUp(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_getTextureArrowUpHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTextureArrowUpHover(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_getTextureArrowDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTextureArrowDown(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarRenderer_getTextureArrowDownHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarRenderer_setTextureArrowDownHover(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
