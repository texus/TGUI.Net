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
    /// Renderer for text box widgets
    /// </summary>
    public class TextBoxRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TextBoxRenderer()
            : base(tguiTextBoxRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal TextBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public TextBoxRenderer(TextBoxRenderer copy)
            : base(tguiTextBoxRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Gets or sets the size of the borders
        /// </summary>
        public Outline Borders
        {
            get { return new Outline(tguiTextBoxRenderer_getBorders(CPointer)); }
            set { tguiTextBoxRenderer_setBorders(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the size of the padding
        /// </summary>
        public Outline Padding
        {
            get { return new Outline(tguiTextBoxRenderer_getPadding(CPointer)); }
            set { tguiTextBoxRenderer_setPadding(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return tguiTextBoxRenderer_getBackgroundColor(CPointer); }
            set { tguiTextBoxRenderer_setBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color
        /// </summary>
        public Color TextColor
        {
            get { return tguiTextBoxRenderer_getTextColor(CPointer); }
            set { tguiTextBoxRenderer_setTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the default text that can optionally be displayed when the text box is empty
        /// </summary>
        public Color DefaultTextColor
        {
            get { return tguiTextBoxRenderer_getDefaultTextColor(CPointer); }
            set { tguiTextBoxRenderer_setDefaultTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the selected text that will be used inside the text box
        /// </summary>
        public Color SelectedTextColor
        {
            get { return tguiTextBoxRenderer_getSelectedTextColor(CPointer); }
            set { tguiTextBoxRenderer_setSelectedTextColor(CPointer, value); }
        }


        /// <summary>
        /// Gets or sets the background color of the selected text that will be used inside the text box
        /// </summary>
        public Color SelectedTextBackgroundColor
        {
            get { return tguiTextBoxRenderer_getSelectedTextBackgroundColor(CPointer); }
            set { tguiTextBoxRenderer_setSelectedTextBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders
        /// </summary>
        public Color BorderColor
        {
            get { return tguiTextBoxRenderer_getBorderColor(CPointer); }
            set { tguiTextBoxRenderer_setBorderColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color that will be used inside the text box for the blinking caret
        /// </summary>
        public Color CaretColor
        {
            get { return tguiTextBoxRenderer_getCaretColor(CPointer); }
            set { tguiTextBoxRenderer_setCaretColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the width of the caret
        /// </summary>
        public float CaretWidth
        {
            get { return tguiTextBoxRenderer_getCaretWidth(CPointer); }
            set { tguiTextBoxRenderer_setCaretWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background image that is displayed
        /// </summary>
        public Texture TextureBackground
        {
            set { tguiTextBoxRenderer_setTextureBackground(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the renderer data of the scrollbar
        /// </summary>
        public RendererData Scrollbar
        {
            get { return new RendererData(tguiTextBoxRenderer_getScrollbar(CPointer)); }
            set { tguiTextBoxRenderer_setScrollbar(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the wanted width scrollbar (0 to use the default width, which is the texture width when using textures)
        /// </summary>
        public float ScrollbarWidth
        {
            get { return tguiTextBoxRenderer_getScrollbarWidth(CPointer); }
            set { tguiTextBoxRenderer_setScrollbarWidth(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTextBoxRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTextBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTextBoxRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setPadding(IntPtr cPointer, IntPtr padding);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTextBoxRenderer_getPadding(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setCaretWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiTextBoxRenderer_getCaretWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTextBoxRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setDefaultTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTextBoxRenderer_getDefaultTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTextBoxRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setSelectedTextBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTextBoxRenderer_getSelectedTextBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTextBoxRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setCaretColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTextBoxRenderer_getCaretColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTextBoxRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setScrollbar(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTextBoxRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTextBoxRenderer_setScrollbarWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiTextBoxRenderer_getScrollbarWidth(IntPtr cPointer);

        #endregion
    }
}
