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
    /// Renderer for edit box widgets
    /// </summary>
    public class EditBoxRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EditBoxRenderer()
            : base(tguiEditBoxRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal EditBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public EditBoxRenderer(EditBoxRenderer copy)
            : base(tguiEditBoxRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Gets or sets the size of the borders
        /// </summary>
        public Outline Borders
        {
            get { return new Outline(tguiEditBoxRenderer_getBorders(CPointer)); }
            set { tguiEditBoxRenderer_setBorders(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the size of the padding
        /// </summary>
        public Outline Padding
        {
            get { return new Outline(tguiEditBoxRenderer_getPadding(CPointer)); }
            set { tguiEditBoxRenderer_setPadding(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the width of the caret
        /// </summary>
        public float CaretWidth
        {
            get { return tguiEditBoxRenderer_getCaretWidth(CPointer); }
            set { tguiEditBoxRenderer_setCaretWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color
        /// </summary>
        public Color TextColor
        {
            get { return tguiEditBoxRenderer_getTextColor(CPointer); }
            set { tguiEditBoxRenderer_setTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the default text that can optionally be displayed when the edit box is empty
        /// </summary>
        public Color DefaultTextColor
        {
            get { return tguiEditBoxRenderer_getDefaultTextColor(CPointer); }
            set { tguiEditBoxRenderer_setDefaultTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color that will be used inside the edit box when the edit box is focused
        /// </summary>
        public Color TextColorFocused
        {
            get { return tguiEditBoxRenderer_getTextColorFocused(CPointer); }
            set { tguiEditBoxRenderer_setTextColorFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color that will be used inside the edit box when the edit box is disabled
        /// </summary>
        public Color TextColorDisabled
        {
            get { return tguiEditBoxRenderer_getTextColorDisabled(CPointer); }
            set { tguiEditBoxRenderer_setTextColorDisabled(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the selected text that will be used inside the edit box
        /// </summary>
        public Color SelectedTextColor
        {
            get { return tguiEditBoxRenderer_getSelectedTextColor(CPointer); }
            set { tguiEditBoxRenderer_setSelectedTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color of the selected text that will be used inside the edit box
        /// </summary>
        public Color SelectedTextBackgroundColor
        {
            get { return tguiEditBoxRenderer_getSelectedTextBackgroundColor(CPointer); }
            set { tguiEditBoxRenderer_setSelectedTextBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return tguiEditBoxRenderer_getBackgroundColor(CPointer); }
            set { tguiEditBoxRenderer_setBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color in the hover state
        /// </summary>
        public Color BackgroundColorHover
        {
            get { return tguiEditBoxRenderer_getBackgroundColorHover(CPointer); }
            set { tguiEditBoxRenderer_setBackgroundColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color when the edit box is focused
        /// </summary>
        public Color BackgroundColorFocused
        {
            get { return tguiEditBoxRenderer_getBackgroundColorFocused(CPointer); }
            set { tguiEditBoxRenderer_setBackgroundColorFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color when the edit box is disabled
        /// </summary>
        public Color BackgroundColorDisabled
        {
            get { return tguiEditBoxRenderer_getBackgroundColorDisabled(CPointer); }
            set { tguiEditBoxRenderer_setBackgroundColorDisabled(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color that will be used inside the edit box for the blinking caret
        /// </summary>
        public Color CaretColor
        {
            get { return tguiEditBoxRenderer_getCaretColor(CPointer); }
            set { tguiEditBoxRenderer_setCaretColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color that will be used inside the edit box for the blinking caret when the mouse is on top of the edit box
        /// </summary>
        public Color CaretColorHover
        {
            get { return tguiEditBoxRenderer_getCaretColorHover(CPointer); }
            set { tguiEditBoxRenderer_setCaretColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color that will be used inside the edit box for the blinking caret when the edit box is focused
        /// </summary>
        public Color CaretColorFocused
        {
            get { return tguiEditBoxRenderer_getCaretColorFocused(CPointer); }
            set { tguiEditBoxRenderer_setCaretColorFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders
        /// </summary>
        public Color BorderColor
        {
            get { return tguiEditBoxRenderer_getBorderColor(CPointer); }
            set { tguiEditBoxRenderer_setBorderColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders when the mouse is on top of the edit box
        /// </summary>
        public Color BorderColorHover
        {
            get { return tguiEditBoxRenderer_getBorderColorHover(CPointer); }
            set { tguiEditBoxRenderer_setBorderColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders when the edit box is focused
        /// </summary>
        public Color BorderColorFocused
        {
            get { return tguiEditBoxRenderer_getBorderColorFocused(CPointer); }
            set { tguiEditBoxRenderer_setBorderColorFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders when the edit box is disabled
        /// </summary>
        public Color BorderColorDisabled
        {
            get { return tguiEditBoxRenderer_getBorderColorDisabled(CPointer); }
            set { tguiEditBoxRenderer_setBorderColorDisabled(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background image that is displayed
        /// </summary>
        public Texture Texture
        {
            set { tguiEditBoxRenderer_setTexture(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background image that is displayed when the mouse is on top of the edit box
        /// </summary>
        public Texture TextureHover
        {
            set { tguiEditBoxRenderer_setTextureHover(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background image that is displayed when the edit box is focused
        /// </summary>
        public Texture TextureFocused
        {
            set { tguiEditBoxRenderer_setTextureFocused(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background image that is displayed when the edit box is disabled
        /// </summary>
        public Texture TextureDisabled
        {
            set { tguiEditBoxRenderer_setTextureDisabled(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the text style
        /// </summary>
        public Text.Styles TextStyle
        {
            get { return tguiEditBoxRenderer_getTextStyle(CPointer); }
            set { tguiEditBoxRenderer_setTextStyle(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text style of the default text (the text drawn when the edit box is empty)
        /// </summary>
        public Text.Styles DefaultTextStyle
        {
            get { return tguiEditBoxRenderer_getDefaultTextStyle(CPointer); }
            set { tguiEditBoxRenderer_setDefaultTextStyle(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBoxRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBoxRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setPadding(IntPtr cPointer, IntPtr padding);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBoxRenderer_getPadding(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setCaretWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiEditBoxRenderer_getCaretWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setDefaultTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getDefaultTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setTextColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getTextColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setTextColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setSelectedTextBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getSelectedTextBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBackgroundColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getBackgroundColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBackgroundColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setCaretColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getCaretColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setCaretColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getCaretColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setCaretColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getCaretColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBorderColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBorderColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getBorderColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setBorderColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiEditBoxRenderer_getBorderColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setTexture(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setTextureHover(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setTextureFocused(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setTextureDisabled(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiEditBoxRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBoxRenderer_setDefaultTextStyle(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiEditBoxRenderer_getDefaultTextStyle(IntPtr cPointer);

        #endregion
    }
}
