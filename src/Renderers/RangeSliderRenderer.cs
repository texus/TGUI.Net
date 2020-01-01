/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2020 Bruno Van de Velde (vdv_b@tgui.eu)
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented;
//    you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment
//    in the product documentation would be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such,
//    and must not be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Security;
using System.Runtime.InteropServices;
using SFML.Graphics;

namespace TGUI
{
    public class RangeSliderRenderer : WidgetRenderer
    {
        public RangeSliderRenderer()
            : base(tguiRangeSliderRenderer_create())
        {
        }

        protected internal RangeSliderRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public RangeSliderRenderer(RangeSliderRenderer copy)
            : base(tguiRangeSliderRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get { return new Outline(tguiRangeSliderRenderer_getBorders(CPointer)); }
            set { tguiRangeSliderRenderer_setBorders(CPointer, value.CPointer); }
        }

        public Color TrackColor
        {
            get { return tguiRangeSliderRenderer_getTrackColor(CPointer); }
            set { tguiRangeSliderRenderer_setTrackColor(CPointer, value); }
        }

        public Color TrackColorHover
        {
            get { return tguiRangeSliderRenderer_getTrackColorHover(CPointer); }
            set { tguiRangeSliderRenderer_setTrackColorHover(CPointer, value); }
        }

        public Color SelectedTrackColor
        {
            get { return tguiRangeSliderRenderer_getSelectedTrackColor(CPointer); }
            set { tguiRangeSliderRenderer_setSelectedTrackColor(CPointer, value); }
        }

        public Color SelectedTrackColorHover
        {
            get { return tguiRangeSliderRenderer_getSelectedTrackColorHover(CPointer); }
            set { tguiRangeSliderRenderer_setSelectedTrackColorHover(CPointer, value); }
        }

        public Color ThumbColor
        {
            get { return tguiRangeSliderRenderer_getThumbColor(CPointer); }
            set { tguiRangeSliderRenderer_setThumbColor(CPointer, value); }
        }

        public Color ThumbColorHover
        {
            get { return tguiRangeSliderRenderer_getThumbColorHover(CPointer); }
            set { tguiRangeSliderRenderer_setThumbColorHover(CPointer, value); }
        }

        public Color BorderColor
        {
            get { return tguiRangeSliderRenderer_getBorderColor(CPointer); }
            set { tguiRangeSliderRenderer_setBorderColor(CPointer, value); }
        }

        public Color BorderColorHover
        {
            get { return tguiRangeSliderRenderer_getBorderColorHover(CPointer); }
            set { tguiRangeSliderRenderer_setBorderColorHover(CPointer, value); }
        }

        public Texture TextureTrack
        {
            set { tguiRangeSliderRenderer_setTextureTrack(CPointer, value.CPointer); }
        }

        public Texture TextureTrackHover
        {
            set { tguiRangeSliderRenderer_setTextureTrackHover(CPointer, value.CPointer); }
        }

        public Texture TextureThumb
        {
            set { tguiRangeSliderRenderer_setTextureThumb(CPointer, value.CPointer); }
        }

        public Texture TextureThumbHover
        {
            set { tguiRangeSliderRenderer_setTextureThumbHover(CPointer, value.CPointer); }
        }

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiRangeSliderRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiRangeSliderRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiRangeSliderRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setTrackColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiRangeSliderRenderer_getTrackColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setTrackColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiRangeSliderRenderer_getTrackColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setSelectedTrackColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiRangeSliderRenderer_getSelectedTrackColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setSelectedTrackColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiRangeSliderRenderer_getSelectedTrackColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setThumbColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiRangeSliderRenderer_getThumbColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setThumbColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiRangeSliderRenderer_getThumbColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiRangeSliderRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setBorderColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiRangeSliderRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setTextureTrack(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setTextureTrackHover(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setTextureThumb(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSliderRenderer_setTextureThumbHover(IntPtr cPointer, IntPtr texture);

        #endregion
    }
}
