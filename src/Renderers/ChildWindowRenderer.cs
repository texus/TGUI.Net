/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2016 Bruno Van de Velde (vdv_b@tgui.eu)
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
    public class ChildWindowRenderer : WidgetRenderer
    {
        public ChildWindowRenderer()
            : base(tguiChildWindowRenderer_create())
        {
        }

        protected internal ChildWindowRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public ChildWindowRenderer(ChildWindowRenderer copy)
            : base(tguiChildWindowRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get { return new Outline(tguiChildWindowRenderer_getBorders(CPointer)); }
            set { tguiChildWindowRenderer_setBorders(CPointer, value.CPointer); }
        }

        public Color TitleBarColor
        {
            get { return tguiChildWindowRenderer_getTitleBarColor(CPointer); }
            set { tguiChildWindowRenderer_setTitleBarColor(CPointer, value); }
        }

        public Color TitleColor
        {
            get { return tguiChildWindowRenderer_getTitleColor(CPointer); }
            set { tguiChildWindowRenderer_setTitleColor(CPointer, value); }
        }

        public Color BackgroundColor
        {
            get { return tguiChildWindowRenderer_getBackgroundColor(CPointer); }
            set { tguiChildWindowRenderer_setBackgroundColor(CPointer, value); }
        }

        public Color BorderColor
        {
            get { return tguiChildWindowRenderer_getBorderColor(CPointer); }
            set { tguiChildWindowRenderer_setBorderColor(CPointer, value); }
        }

        public float BorderBelowTitleBar
        {
            get { return tguiChildWindowRenderer_getBorderBelowTitleBar(CPointer); }
            set { tguiChildWindowRenderer_setBorderBelowTitleBar(CPointer, value); }
        }

        public float TitleBarHeight
        {
            get { return tguiChildWindowRenderer_getTitleBarHeight(CPointer); }
            set { tguiChildWindowRenderer_setTitleBarHeight(CPointer, value); }
        }

        public float DistanceToSide
        {
            get { return tguiChildWindowRenderer_getDistanceToSide(CPointer); }
            set { tguiChildWindowRenderer_setDistanceToSide(CPointer, value); }
        }

        public float PaddingBetweenButtons
        {
            get { return tguiChildWindowRenderer_getPaddingBetweenButtons(CPointer); }
            set { tguiChildWindowRenderer_setPaddingBetweenButtons(CPointer, value); }
        }

        public float MinimumResizableBorderWidth
        {
            get { return tguiChildWindowRenderer_getMinimumResizableBorderWidth(CPointer); }
            set { tguiChildWindowRenderer_setMinimumResizableBorderWidth(CPointer, value); }
        }

        public bool ShowTextOnTitleButtons
        {
            get { return tguiChildWindowRenderer_getShowTextOnTitleButtons(CPointer); }
            set { tguiChildWindowRenderer_setShowTextOnTitleButtons(CPointer, value); }
        }

        public Texture TextureTitleBar
        {
            set { tguiChildWindowRenderer_setTextureTitleBar(CPointer, value.CPointer); }
        }

        public RendererData CloseButton
        {
            get { return new RendererData(tguiChildWindowRenderer_getCloseButton(CPointer)); }
            set { tguiChildWindowRenderer_setCloseButton(CPointer, value.CPointer); }
        }

        public RendererData MaximizeButton
        {
            get { return new RendererData(tguiChildWindowRenderer_getMaximizeButton(CPointer)); }
            set { tguiChildWindowRenderer_setMaximizeButton(CPointer, value.CPointer); }
        }

        public RendererData MinimizeButton
        {
            get { return new RendererData(tguiChildWindowRenderer_getMinimizeButton(CPointer)); }
            set { tguiChildWindowRenderer_setMinimizeButton(CPointer, value.CPointer); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiChildWindowRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiChildWindowRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiChildWindowRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setTitleBarColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiChildWindowRenderer_getTitleBarColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setTitleColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiChildWindowRenderer_getTitleColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiChildWindowRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiChildWindowRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setBorderBelowTitleBar(IntPtr cPointer, float border);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiChildWindowRenderer_getBorderBelowTitleBar(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setTitleBarHeight(IntPtr cPointer, float height);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiChildWindowRenderer_getTitleBarHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setDistanceToSide(IntPtr cPointer, float distance);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiChildWindowRenderer_getDistanceToSide(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setPaddingBetweenButtons(IntPtr cPointer, float padding);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiChildWindowRenderer_getPaddingBetweenButtons(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setMinimumResizableBorderWidth(IntPtr cPointer, float minimumBorderWidth);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiChildWindowRenderer_getMinimumResizableBorderWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setShowTextOnTitleButtons(IntPtr cPointer, bool showText);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiChildWindowRenderer_getShowTextOnTitleButtons(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setTextureTitleBar(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setCloseButton(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiChildWindowRenderer_getCloseButton(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setMaximizeButton(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiChildWindowRenderer_getMaximizeButton(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChildWindowRenderer_setMinimizeButton(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiChildWindowRenderer_getMinimizeButton(IntPtr cPointer);

        #endregion
    }
}
