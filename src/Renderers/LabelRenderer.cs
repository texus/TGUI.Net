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
    public class LabelRenderer : WidgetRenderer
    {
        public LabelRenderer()
            : base(tguiLabelRenderer_create())
        {
        }

        protected internal LabelRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public LabelRenderer(LabelRenderer copy)
            : base(tguiLabelRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get { return new Outline(tguiLabelRenderer_getBorders(CPointer)); }
            set { tguiLabelRenderer_setBorders(CPointer, value.CPointer); }
        }

        public Outline Padding
        {
            get { return new Outline(tguiLabelRenderer_getPadding(CPointer)); }
            set { tguiLabelRenderer_setPadding(CPointer, value.CPointer); }
        }

        public Color TextColor
        {
            get { return tguiLabelRenderer_getTextColor(CPointer); }
            set { tguiLabelRenderer_setTextColor(CPointer, value); }
        }

        public Color BackgroundColor
        {
            get { return tguiLabelRenderer_getBackgroundColor(CPointer); }
            set { tguiLabelRenderer_setBackgroundColor(CPointer, value); }
        }

        public Color BorderColor
        {
            get { return tguiLabelRenderer_getBorderColor(CPointer); }
            set { tguiLabelRenderer_setBorderColor(CPointer, value); }
        }

        public Text.Styles TextStyle
        {
            get { return tguiLabelRenderer_getTextStyle(CPointer); }
            set { tguiLabelRenderer_setTextStyle(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiLabelRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiLabelRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabelRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiLabelRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabelRenderer_setPadding(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiLabelRenderer_getPadding(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabelRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiLabelRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabelRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiLabelRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabelRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiLabelRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabelRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Text.Styles tguiLabelRenderer_getTextStyle(IntPtr cPointer);

        #endregion
    }
}
