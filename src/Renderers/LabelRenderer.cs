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
    /// <summary>
    /// Renderer for label widgets
    /// </summary>
    public class LabelRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LabelRenderer()
            : base(tguiLabelRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal LabelRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public LabelRenderer(LabelRenderer copy)
            : base(tguiLabelRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Gets or sets the size of the borders
        /// </summary>
        public Outline Borders
        {
            get { return new Outline(tguiLabelRenderer_getBorders(CPointer)); }
            set { tguiLabelRenderer_setBorders(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the size of the padding
        /// </summary>
        public Outline Padding
        {
            get { return new Outline(tguiLabelRenderer_getPadding(CPointer)); }
            set { tguiLabelRenderer_setPadding(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the text color
        /// </summary>
        public Color TextColor
        {
            get { return tguiLabelRenderer_getTextColor(CPointer); }
            set { tguiLabelRenderer_setTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the outline color of the text
        /// </summary>
        public Color TextOutlineColor
        {
            get { return tguiLabelRenderer_getTextOutlineColor(CPointer); }
            set { tguiLabelRenderer_setTextOutlineColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the outline thickness of the text
        /// </summary>
        public float TextOutlineThickness
        {
            get { return tguiLabelRenderer_getTextOutlineThickness(CPointer); }
            set { tguiLabelRenderer_setTextOutlineThickness(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return tguiLabelRenderer_getBackgroundColor(CPointer); }
            set { tguiLabelRenderer_setBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders
        /// </summary>
        public Color BorderColor
        {
            get { return tguiLabelRenderer_getBorderColor(CPointer); }
            set { tguiLabelRenderer_setBorderColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text style
        /// </summary>
        public Text.Styles TextStyle
        {
            get { return tguiLabelRenderer_getTextStyle(CPointer); }
            set { tguiLabelRenderer_setTextStyle(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the renderer data of the scrollbar
        /// </summary>
        public RendererData Scrollbar
        {
            get { return new RendererData(tguiLabelRenderer_getScrollbar(CPointer)); }
            set { tguiLabelRenderer_setScrollbar(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the width of the scrollbar
        /// </summary>
        public float ScrollbarWidth
        {
            get { return tguiLabelRenderer_getScrollbarWidth(CPointer); }
            set { tguiLabelRenderer_setScrollbarWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background texture
        /// </summary>
        public Texture TextureBackground
        {
            set { tguiLabelRenderer_setTextureBackground(CPointer, value.CPointer); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLabelRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLabelRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLabelRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setPadding(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLabelRenderer_getPadding(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiLabelRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setTextOutlineColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiLabelRenderer_getTextOutlineColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setTextOutlineThickness(IntPtr cPointer, float thickness);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiLabelRenderer_getTextOutlineThickness(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiLabelRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiLabelRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiLabelRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setScrollbar(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLabelRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setScrollbarWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiLabelRenderer_getScrollbarWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabelRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

        #endregion
    }
}
