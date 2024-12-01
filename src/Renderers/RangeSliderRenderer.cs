// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for RangeSlider widgets
    /// </summary>
    public class RangeSliderRenderer : SliderRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RangeSliderRenderer()
            : base(tguiRangeSliderRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal RangeSliderRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public RangeSliderRenderer(RangeSliderRenderer copy)
            : base(tguiRangeSliderRenderer_copy(copy.CPointer))
        {
        }

        public Color? SelectedTrackColor
        {
            get => Util.GetColorFromC(tguiRangeSliderRenderer_getSelectedTrackColor(CPointer));
            set => tguiRangeSliderRenderer_setSelectedTrackColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTrackColorHover
        {
            get => Util.GetColorFromC(tguiRangeSliderRenderer_getSelectedTrackColorHover(CPointer));
            set => tguiRangeSliderRenderer_setSelectedTrackColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureSelectedTrack
        {
            get => new Texture(tguiRangeSliderRenderer_getTextureSelectedTrack(CPointer));
            set => tguiRangeSliderRenderer_setTextureSelectedTrack(CPointer, value.CPointer);
        }

        public Texture TextureSelectedTrackHover
        {
            get => new Texture(tguiRangeSliderRenderer_getTextureSelectedTrackHover(CPointer));
            set => tguiRangeSliderRenderer_setTextureSelectedTrackHover(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRangeSliderRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRangeSliderRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRangeSliderRenderer_getSelectedTrackColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSliderRenderer_setSelectedTrackColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiRangeSliderRenderer_getSelectedTrackColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSliderRenderer_setSelectedTrackColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRangeSliderRenderer_getTextureSelectedTrack(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSliderRenderer_setTextureSelectedTrack(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRangeSliderRenderer_getTextureSelectedTrackHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSliderRenderer_setTextureSelectedTrackHover(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
