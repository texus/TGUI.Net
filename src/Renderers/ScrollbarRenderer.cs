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
    public class ScrollbarRenderer : WidgetRenderer
    {
        public ScrollbarRenderer()
            : base(tguiScrollbarRenderer_create())
        {
        }

        protected internal ScrollbarRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public ScrollbarRenderer(ScrollbarRenderer copy)
            : base(tguiScrollbarRenderer_copy(copy.CPointer))
        {
        }

        public Color TrackColor
        {
            get { return tguiScrollbarRenderer_getTrackColor(CPointer); }
            set { tguiScrollbarRenderer_setTrackColor(CPointer, value); }
        }

        public Color TrackColorHover
        {
            get { return tguiScrollbarRenderer_getTrackColorHover(CPointer); }
            set { tguiScrollbarRenderer_setTrackColorHover(CPointer, value); }
        }

        public Color ThumbColor
        {
            get { return tguiScrollbarRenderer_getThumbColor(CPointer); }
            set { tguiScrollbarRenderer_setThumbColor(CPointer, value); }
        }

        public Color ThumbColorHover
        {
            get { return tguiScrollbarRenderer_getThumbColorHover(CPointer); }
            set { tguiScrollbarRenderer_setThumbColorHover(CPointer, value); }
        }

        public Color ArrowBackgroundColor
        {
            get { return tguiScrollbarRenderer_getArrowBackgroundColor(CPointer); }
            set { tguiScrollbarRenderer_setArrowBackgroundColor(CPointer, value); }
        }

        public Color ArrowBackgroundColorHover
        {
            get { return tguiScrollbarRenderer_getArrowBackgroundColorHover(CPointer); }
            set { tguiScrollbarRenderer_setArrowBackgroundColorHover(CPointer, value); }
        }

        public Color ArrowColor
        {
            get { return tguiScrollbarRenderer_getArrowColor(CPointer); }
            set { tguiScrollbarRenderer_setArrowColor(CPointer, value); }
        }

        public Color ArrowColorHover
        {
            get { return tguiScrollbarRenderer_getArrowColorHover(CPointer); }
            set { tguiScrollbarRenderer_setArrowColorHover(CPointer, value); }
        }

        public Texture TextureTrack
        {
            set { tguiScrollbarRenderer_setTextureTrack(CPointer, value.CPointer); }
        }

        public Texture TextureTrackHover
        {
            set { tguiScrollbarRenderer_setTextureTrackHover(CPointer, value.CPointer); }
        }

        public Texture TextureThumb
        {
            set { tguiScrollbarRenderer_setTextureThumb(CPointer, value.CPointer); }
        }

        public Texture TextureThumbHover
        {
            set { tguiScrollbarRenderer_setTextureThumbHover(CPointer, value.CPointer); }
        }

        public Texture TextureArrowUp
        {
            set { tguiScrollbarRenderer_setTextureArrowUp(CPointer, value.CPointer); }
        }

        public Texture TextureArrowUpHover
        {
            set { tguiScrollbarRenderer_setTextureArrowUpHover(CPointer, value.CPointer); }
        }

        public Texture TextureArrowDown
        {
            set { tguiScrollbarRenderer_setTextureArrowDown(CPointer, value.CPointer); }
        }

        public Texture TextureArrowDownHover
        {
            set { tguiScrollbarRenderer_setTextureArrowDownHover(CPointer, value.CPointer); }
        }

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiScrollbarRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiScrollbarRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTrackColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiScrollbarRenderer_getTrackColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTrackColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiScrollbarRenderer_getTrackColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setThumbColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiScrollbarRenderer_getThumbColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setThumbColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiScrollbarRenderer_getThumbColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setArrowBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiScrollbarRenderer_getArrowBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setArrowBackgroundColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiScrollbarRenderer_getArrowBackgroundColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setArrowColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiScrollbarRenderer_getArrowColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setArrowColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiScrollbarRenderer_getArrowColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTextureTrack(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTextureTrackHover(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTextureThumb(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTextureThumbHover(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTextureArrowUp(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTextureArrowUpHover(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTextureArrowDown(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbarRenderer_setTextureArrowDownHover(IntPtr cPointer, IntPtr texture);

        #endregion
    }
}
