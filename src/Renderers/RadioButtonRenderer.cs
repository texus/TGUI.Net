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
    public class RadioButtonRenderer : WidgetRenderer
    {
        public RadioButtonRenderer()
            : base(tguiRadioButtonRenderer_create())
        {
        }

        protected internal RadioButtonRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public RadioButtonRenderer(RadioButtonRenderer copy)
            : base(tguiRadioButtonRenderer_copy(copy.CPointer))
        {
        }

        public float TextDistanceRatio
        {
            get { return tguiRadioButtonRenderer_getTextDistanceRatio(CPointer); }
            set { tguiRadioButtonRenderer_setTextDistanceRatio(CPointer, value); }
        }

        public Outline Borders
        {
            get { return new Outline(tguiRadioButtonRenderer_getBorders(CPointer)); }
            set { tguiRadioButtonRenderer_setBorders(CPointer, value.CPointer); }
        }

        public Color TextColor
        {
            get { return tguiRadioButtonRenderer_getTextColor(CPointer); }
            set { tguiRadioButtonRenderer_setTextColor(CPointer, value); }
        }

        public Color TextColorHover
        {
            get { return tguiRadioButtonRenderer_getTextColorHover(CPointer); }
            set { tguiRadioButtonRenderer_setTextColorHover(CPointer, value); }
        }

        public Color TextColorDisabled
        {
            get { return tguiRadioButtonRenderer_getTextColorDisabled(CPointer); }
            set { tguiRadioButtonRenderer_setTextColorDisabled(CPointer, value); }
        }

        public Color TextColorChecked
        {
            get { return tguiRadioButtonRenderer_getTextColorChecked(CPointer); }
            set { tguiRadioButtonRenderer_setTextColorChecked(CPointer, value); }
        }

        public Color TextColorCheckedHover
        {
            get { return tguiRadioButtonRenderer_getTextColorCheckedHover(CPointer); }
            set { tguiRadioButtonRenderer_setTextColorCheckedHover(CPointer, value); }
        }

        public Color TextColorCheckedDisabled
        {
            get { return tguiRadioButtonRenderer_getTextColorCheckedDisabled(CPointer); }
            set { tguiRadioButtonRenderer_setTextColorCheckedDisabled(CPointer, value); }
        }

        public Color BackgroundColor
        {
            get { return tguiRadioButtonRenderer_getBackgroundColor(CPointer); }
            set { tguiRadioButtonRenderer_setBackgroundColor(CPointer, value); }
        }

        public Color BackgroundColorHover
        {
            get { return tguiRadioButtonRenderer_getBackgroundColorHover(CPointer); }
            set { tguiRadioButtonRenderer_setBackgroundColorHover(CPointer, value); }
        }

        public Color BackgroundColorDisabled
        {
            get { return tguiRadioButtonRenderer_getBackgroundColorDisabled(CPointer); }
            set { tguiRadioButtonRenderer_setBackgroundColorDisabled(CPointer, value); }
        }

        public Color BackgroundColorChecked
        {
            get { return tguiRadioButtonRenderer_getBackgroundColorChecked(CPointer); }
            set { tguiRadioButtonRenderer_setBackgroundColorChecked(CPointer, value); }
        }

        public Color BackgroundColorCheckedHover
        {
            get { return tguiRadioButtonRenderer_getBackgroundColorCheckedHover(CPointer); }
            set { tguiRadioButtonRenderer_setBackgroundColorCheckedHover(CPointer, value); }
        }

        public Color BackgroundColorCheckedDisabled
        {
            get { return tguiRadioButtonRenderer_getBackgroundColorCheckedDisabled(CPointer); }
            set { tguiRadioButtonRenderer_setBackgroundColorCheckedDisabled(CPointer, value); }
        }

        public Color BorderColor
        {
            get { return tguiRadioButtonRenderer_getBorderColor(CPointer); }
            set { tguiRadioButtonRenderer_setBorderColor(CPointer, value); }
        }

        public Color BorderColorHover
        {
            get { return tguiRadioButtonRenderer_getBorderColorHover(CPointer); }
            set { tguiRadioButtonRenderer_setBorderColorHover(CPointer, value); }
        }

        public Color BorderColorFocused
        {
            get { return tguiRadioButtonRenderer_getBorderColorFocused(CPointer); }
            set { tguiRadioButtonRenderer_setBorderColorFocused(CPointer, value); }
        }

        public Color BorderColorDisabled
        {
            get { return tguiRadioButtonRenderer_getBorderColorDisabled(CPointer); }
            set { tguiRadioButtonRenderer_setBorderColorDisabled(CPointer, value); }
        }

        public Color BorderColorChecked
        {
            get { return tguiRadioButtonRenderer_getBorderColorChecked(CPointer); }
            set { tguiRadioButtonRenderer_setBorderColorChecked(CPointer, value); }
        }

        public Color BorderColorHoverChecked
        {
            get { return tguiRadioButtonRenderer_getBorderColorCheckedHover(CPointer); }
            set { tguiRadioButtonRenderer_setBorderColorCheckedHover(CPointer, value); }
        }

        public Color BorderColorFocusedChecked
        {
            get { return tguiRadioButtonRenderer_getBorderColorCheckedFocused(CPointer); }
            set { tguiRadioButtonRenderer_setBorderColorCheckedFocused(CPointer, value); }
        }

        public Color BorderColorDisabledChecked
        {
            get { return tguiRadioButtonRenderer_getBorderColorCheckedDisabled(CPointer); }
            set { tguiRadioButtonRenderer_setBorderColorCheckedDisabled(CPointer, value); }
        }

        public Color CheckColor
        {
            get { return tguiRadioButtonRenderer_getCheckColor(CPointer); }
            set { tguiRadioButtonRenderer_setCheckColor(CPointer, value); }
        }

        public Color CheckColorHover
        {
            get { return tguiRadioButtonRenderer_getCheckColorHover(CPointer); }
            set { tguiRadioButtonRenderer_setCheckColorHover(CPointer, value); }
        }

        public Color CheckColorDisabled
        {
            get { return tguiRadioButtonRenderer_getCheckColorDisabled(CPointer); }
            set { tguiRadioButtonRenderer_setCheckColorDisabled(CPointer, value); }
        }

        public Texture TextureUnchecked
        {
            set { tguiRadioButtonRenderer_setTextureUnchecked(CPointer, value.CPointer); }
        }

        public Texture TextureChecked
        {
            set { tguiRadioButtonRenderer_setTextureChecked(CPointer, value.CPointer); }
        }

        public Texture TextureUncheckedHover
        {
            set { tguiRadioButtonRenderer_setTextureUncheckedHover(CPointer, value.CPointer); }
        }

        public Texture TextureCheckedHover
        {
            set { tguiRadioButtonRenderer_setTextureCheckedHover(CPointer, value.CPointer); }
        }

        public Texture TextureUncheckedFocused
        {
            set { tguiRadioButtonRenderer_setTextureUncheckedFocused(CPointer, value.CPointer); }
        }

        public Texture TextureCheckedFocused
        {
            set { tguiRadioButtonRenderer_setTextureCheckedFocused(CPointer, value.CPointer); }
        }

        public Texture TextureUncheckedDisabled
        {
            set { tguiRadioButtonRenderer_setTextureUncheckedDisabled(CPointer, value.CPointer); }
        }

        public Texture TextureCheckedDisabled
        {
            set { tguiRadioButtonRenderer_setTextureCheckedDisabled(CPointer, value.CPointer); }
        }

        public Text.Styles TextStyle
        {
            get { return tguiRadioButtonRenderer_getTextStyle(CPointer); }
            set { tguiRadioButtonRenderer_setTextStyle(CPointer, value); }
        }

        public Text.Styles TextStyleChecked
        {
            get { return tguiRadioButtonRenderer_getTextStyleChecked(CPointer); }
            set { tguiRadioButtonRenderer_setTextStyleChecked(CPointer, value); }
        }

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiRadioButtonRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiRadioButtonRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextDistanceRatio(IntPtr cPointer, float ratio);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiRadioButtonRenderer_getTextDistanceRatio(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiRadioButtonRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextColorChecked(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getTextColorChecked(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextColorCheckedHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getTextColorCheckedHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextColorCheckedDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getTextColorCheckedDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBackgroundColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBackgroundColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBackgroundColorChecked(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBackgroundColorChecked(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBackgroundColorCheckedHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBackgroundColorCheckedHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBackgroundColorCheckedDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBackgroundColorCheckedDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorderColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBorderColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorderColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBorderColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorderColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBorderColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorderColorChecked(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBorderColorChecked(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorderColorCheckedHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBorderColorCheckedHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorderColorCheckedFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBorderColorCheckedFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setBorderColorCheckedDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getBorderColorCheckedDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setCheckColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getCheckColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setCheckColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getCheckColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setCheckColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiRadioButtonRenderer_getCheckColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextureUnchecked(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextureChecked(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextureUncheckedHover(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextureCheckedHover(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextureUncheckedFocused(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextureCheckedFocused(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextureUncheckedDisabled(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextureCheckedDisabled(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Text.Styles tguiRadioButtonRenderer_getTextStyle(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiRadioButtonRenderer_setTextStyleChecked(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Text.Styles tguiRadioButtonRenderer_getTextStyleChecked(IntPtr cPointer);

        #endregion
    }
}
