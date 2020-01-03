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
    /// Renderer for button widgets
    /// </summary>
    public class ButtonRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ButtonRenderer()
            : base(tguiButtonRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ButtonRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ButtonRenderer(ButtonRenderer copy)
            : base(tguiButtonRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Gets or sets the size of the borders
        /// </summary>
        public Outline Borders
        {
            get { return new Outline(tguiButtonRenderer_getBorders(CPointer)); }
            set { tguiButtonRenderer_setBorders(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the text color
        /// </summary>
        public Color TextColor
        {
            get { return tguiButtonRenderer_getTextColor(CPointer); }
            set { tguiButtonRenderer_setTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color when the button is in hover state
        /// </summary>
        public Color TextColorHover
        {
            get { return tguiButtonRenderer_getTextColorHover(CPointer); }
            set { tguiButtonRenderer_setTextColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color when the button is in down state
        /// </summary>
        public Color TextColorDown
        {
            get { return tguiButtonRenderer_getTextColorDown(CPointer); }
            set { tguiButtonRenderer_setTextColorDown(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color when the button is in focused state
        /// </summary>
        public Color TextColorFocused
        {
            get { return tguiButtonRenderer_getTextColorFocused(CPointer); }
            set { tguiButtonRenderer_setTextColorFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color when the button is in disabled state
        /// </summary>
        public Color TextColorDisabled
        {
            get { return tguiButtonRenderer_getTextColorDisabled(CPointer); }
            set { tguiButtonRenderer_setTextColorDisabled(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return tguiButtonRenderer_getBackgroundColor(CPointer); }
            set { tguiButtonRenderer_setBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color when the button is in hover state
        /// </summary>
        public Color BackgroundColorHover
        {
            get { return tguiButtonRenderer_getBackgroundColorHover(CPointer); }
            set { tguiButtonRenderer_setBackgroundColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color when the button is in down state
        /// </summary>
        public Color BackgroundColorDown
        {
            get { return tguiButtonRenderer_getBackgroundColorDown(CPointer); }
            set { tguiButtonRenderer_setBackgroundColorDown(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color when the button is in focused state
        /// </summary>
        public Color BackgroundColorFocused
        {
            get { return tguiButtonRenderer_getBackgroundColorFocused(CPointer); }
            set { tguiButtonRenderer_setBackgroundColorFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color when the button is in disabled state
        /// </summary>
        public Color BackgroundColorDisabled
        {
            get { return tguiButtonRenderer_getBackgroundColorDisabled(CPointer); }
            set { tguiButtonRenderer_setBackgroundColorDisabled(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the outline color of the text
        /// </summary>
        public Color TextOutlineColor
        {
            get { return tguiButtonRenderer_getTextOutlineColor(CPointer); }
            set { tguiButtonRenderer_setTextOutlineColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the outline thickness of the text
        /// </summary>
        public float TextOutlineThickness
        {
            get { return tguiButtonRenderer_getTextOutlineThickness(CPointer); }
            set { tguiButtonRenderer_setTextOutlineThickness(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders
        /// </summary>
        public Color BorderColor
        {
            get { return tguiButtonRenderer_getBorderColor(CPointer); }
            set { tguiButtonRenderer_setBorderColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders when the button is in hover state
        /// </summary>
        public Color BorderColorHover
        {
            get { return tguiButtonRenderer_getBorderColorHover(CPointer); }
            set { tguiButtonRenderer_setBorderColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders when the button is in down state
        /// </summary>
        public Color BorderColorDown
        {
            get { return tguiButtonRenderer_getBorderColorDown(CPointer); }
            set { tguiButtonRenderer_setBorderColorDown(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders when the button is in focused state
        /// </summary>
        public Color BorderColorFocused
        {
            get { return tguiButtonRenderer_getBorderColorFocused(CPointer); }
            set { tguiButtonRenderer_setBorderColorFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders when the button is in disabled state
        /// </summary>
        public Color BorderColorDisabled
        {
            get { return tguiButtonRenderer_getBorderColorDisabled(CPointer); }
            set { tguiButtonRenderer_setBorderColorDisabled(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background texture
        /// </summary>
        public Texture Texture
        {
            set { tguiButtonRenderer_setTexture(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background texture when the button is in hover state
        /// </summary>
        public Texture TextureHover
        {
            set { tguiButtonRenderer_setTextureHover(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background texture when the button is in down state
        /// </summary>
        public Texture TextureDown
        {
            set { tguiButtonRenderer_setTextureDown(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background texture when the button is in focused state
        /// </summary>
        public Texture TextureFocused
        {
            set { tguiButtonRenderer_setTextureFocused(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background texture when the button is in disabled state
        /// </summary>
        public Texture TextureDisabled
        {
            set { tguiButtonRenderer_setTextureDisabled(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the text style
        /// </summary>
        public Text.Styles TextStyle
        {
            get { return tguiButtonRenderer_getTextStyle(CPointer); }
            set { tguiButtonRenderer_setTextStyle(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text style when the button is in hover state
        /// </summary>
        public Text.Styles TextStyleHover
        {
            get { return tguiButtonRenderer_getTextStyleHover(CPointer); }
            set { tguiButtonRenderer_setTextStyleHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text style when the button is in down state
        /// </summary>
        public Text.Styles TextStyleDown
        {
            get { return tguiButtonRenderer_getTextStyleDown(CPointer); }
            set { tguiButtonRenderer_setTextStyleDown(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text style when the button is in focused state
        /// </summary>
        public Text.Styles TextStyleFocused
        {
            get { return tguiButtonRenderer_getTextStyleFocused(CPointer); }
            set { tguiButtonRenderer_setTextStyleFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text style when the button is in disabled state
        /// </summary>
        public Text.Styles TextStyleDisabled
        {
            get { return tguiButtonRenderer_getTextStyleDisabled(CPointer); }
            set { tguiButtonRenderer_setTextStyleDisabled(CPointer, value); }
        }

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiButtonRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiButtonRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiButtonRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextColorDown(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getTextColorDown(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getTextColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBackgroundColorDown(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBackgroundColorDown(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBackgroundColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBackgroundColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBackgroundColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBorderColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBorderColorDown(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBorderColorDown(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBorderColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBorderColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setBorderColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getBorderColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextOutlineColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiButtonRenderer_getTextOutlineColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextOutlineThickness(IntPtr cPointer, float thickness);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiButtonRenderer_getTextOutlineThickness(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTexture(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextureHover(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextureDown(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextureFocused(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextureDisabled(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiButtonRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextStyleHover(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiButtonRenderer_getTextStyleHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextStyleDown(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiButtonRenderer_getTextStyleDown(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextStyleFocused(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiButtonRenderer_getTextStyleFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiButtonRenderer_setTextStyleDisabled(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiButtonRenderer_getTextStyleDisabled(IntPtr cPointer);

        #endregion
    }
}
