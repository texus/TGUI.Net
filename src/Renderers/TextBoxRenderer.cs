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
    public class TextBoxRenderer : WidgetRenderer
    {
        public TextBoxRenderer()
            : base(tguiTextBoxRenderer_create())
        {
        }

        protected internal TextBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public TextBoxRenderer(TextBoxRenderer copy)
            : base(tguiTextBoxRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get { return new Outline(tguiTextBoxRenderer_getBorders(CPointer)); }
            set { tguiTextBoxRenderer_setBorders(CPointer, value.CPointer); }
        }

        public Outline Padding
        {
            get { return new Outline(tguiTextBoxRenderer_getPadding(CPointer)); }
            set { tguiTextBoxRenderer_setPadding(CPointer, value.CPointer); }
        }

        public Color BackgroundColor
        {
            get { return tguiTextBoxRenderer_getBackgroundColor(CPointer); }
            set { tguiTextBoxRenderer_setBackgroundColor(CPointer, value); }
        }

        public Color TextColor
        {
            get { return tguiTextBoxRenderer_getTextColor(CPointer); }
            set { tguiTextBoxRenderer_setTextColor(CPointer, value); }
        }

        public Color SelectedTextColor
        {
            get { return tguiTextBoxRenderer_getSelectedTextColor(CPointer); }
            set { tguiTextBoxRenderer_setSelectedTextColor(CPointer, value); }
        }

        public Color SelectedTextBackgroundColor
        {
            get { return tguiTextBoxRenderer_getSelectedTextBackgroundColor(CPointer); }
            set { tguiTextBoxRenderer_setSelectedTextBackgroundColor(CPointer, value); }
        }

        public Color BorderColor
        {
            get { return tguiTextBoxRenderer_getBorderColor(CPointer); }
            set { tguiTextBoxRenderer_setBorderColor(CPointer, value); }
        }

        public Color CaretColor
        {
            get { return tguiTextBoxRenderer_getCaretColor(CPointer); }
            set { tguiTextBoxRenderer_setCaretColor(CPointer, value); }
        }

        public float CaretWidth
        {
            get { return tguiTextBoxRenderer_getCaretWidth(CPointer); }
            set { tguiTextBoxRenderer_setCaretWidth(CPointer, value); }
        }

        public Texture TextureBackground
        {
            set { tguiTextBoxRenderer_setTextureBackground(CPointer, value.CPointer); }
        }

        public RendererData Scrollbar
        {
            get { return new RendererData(tguiTextBoxRenderer_getScrollbar(CPointer)); }
            set { tguiTextBoxRenderer_setScrollbar(CPointer, value.CPointer); }
        }

        public float ScrollbarWidth
        {
            get { return tguiTextBoxRenderer_getScrollbarWidth(CPointer); }
            set { tguiTextBoxRenderer_setScrollbarWidth(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTextBoxRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTextBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTextBoxRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setPadding(IntPtr cPointer, IntPtr padding);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTextBoxRenderer_getPadding(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setCaretWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiTextBoxRenderer_getCaretWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiTextBoxRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiTextBoxRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setSelectedTextBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiTextBoxRenderer_getSelectedTextBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiTextBoxRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setCaretColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiTextBoxRenderer_getCaretColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiTextBoxRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setScrollbar(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTextBoxRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTextBoxRenderer_setScrollbarWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiTextBoxRenderer_getScrollbarWidth(IntPtr cPointer);

        #endregion
    }
}
