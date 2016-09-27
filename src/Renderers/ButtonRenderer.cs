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
	public class ButtonRenderer : WidgetRenderer
	{
		public ButtonRenderer()
			: base(tguiButtonRenderer_create())
		{
		}

		protected internal ButtonRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public ButtonRenderer(ButtonRenderer copy)
			: base(tguiButtonRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return tguiButtonRenderer_getBorders(CPointer); }
			set { tguiButtonRenderer_setBorders(CPointer, value); }
		}

		public Color TextColor
		{
			get { return tguiButtonRenderer_getTextColor(CPointer); }
			set { tguiButtonRenderer_setTextColor(CPointer, value); }
		}

		public Color TextColorHover
		{
			get { return tguiButtonRenderer_getTextColorHover(CPointer); }
			set { tguiButtonRenderer_setTextColorHover(CPointer, value); }
		}

		public Color TextColorDown
		{
			get { return tguiButtonRenderer_getTextColorDown(CPointer); }
			set { tguiButtonRenderer_setTextColorDown(CPointer, value); }
		}

		public Color TextColorDisabled
		{
			get { return tguiButtonRenderer_getTextColorDisabled(CPointer); }
			set { tguiButtonRenderer_setTextColorDisabled(CPointer, value); }
		}

		public Color BackgroundColor
		{
			get { return tguiButtonRenderer_getBackgroundColor(CPointer); }
			set { tguiButtonRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color BackgroundColorHover
		{
			get { return tguiButtonRenderer_getBackgroundColorHover(CPointer); }
			set { tguiButtonRenderer_setBackgroundColorHover(CPointer, value); }
		}

		public Color BackgroundColorDown
		{
			get { return tguiButtonRenderer_getBackgroundColorDown(CPointer); }
			set { tguiButtonRenderer_setBackgroundColorDown(CPointer, value); }
		}

		public Color BackgroundColorDisabled
		{
			get { return tguiButtonRenderer_getBackgroundColorDisabled(CPointer); }
			set { tguiButtonRenderer_setBackgroundColorDisabled(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiButtonRenderer_getBorderColor(CPointer); }
			set { tguiButtonRenderer_setBorderColor(CPointer, value); }
		}

		public Color BorderColorHover
		{
			get { return tguiButtonRenderer_getBorderColorHover(CPointer); }
			set { tguiButtonRenderer_setBorderColorHover(CPointer, value); }
		}

		public Color BorderColorDown
		{
			get { return tguiButtonRenderer_getBorderColorDown(CPointer); }
			set { tguiButtonRenderer_setBorderColorDown(CPointer, value); }
		}

		public Color BorderColorDisabled
		{
			get { return tguiButtonRenderer_getBorderColorDisabled(CPointer); }
			set { tguiButtonRenderer_setBorderColorDisabled(CPointer, value); }
		}

		public Texture Texture
		{
			set { tguiButtonRenderer_setTexture(CPointer, value.CPointer); }
		}

		public Texture TextureHover
		{
			set { tguiButtonRenderer_setTextureHover(CPointer, value.CPointer); }
		}

		public Texture TextureDown
		{
			set { tguiButtonRenderer_setTextureDown(CPointer, value.CPointer); }
		}

		public Texture TextureDisabled
		{
			set { tguiButtonRenderer_setTextureDisabled(CPointer, value.CPointer); }
		}

		public Text.Styles TextStyle
		{
			get { return tguiButtonRenderer_getTextStyle(CPointer); }
			set { tguiButtonRenderer_setTextStyle(CPointer, value); }
		}

		public Text.Styles TextStyleHover
		{
			get { return tguiButtonRenderer_getTextStyleHover(CPointer); }
			set { tguiButtonRenderer_setTextStyleHover(CPointer, value); }
		}

		public Text.Styles TextStyleDown
		{
			get { return tguiButtonRenderer_getTextStyleDown(CPointer); }
			set { tguiButtonRenderer_setTextStyleDown(CPointer, value); }
		}

		public Text.Styles TextStyleDisabled
		{
			get { return tguiButtonRenderer_getTextStyleDisabled(CPointer); }
			set { tguiButtonRenderer_setTextStyleDisabled(CPointer, value); }
		}

		#region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiButtonRenderer_create();

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiButtonRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBorders(IntPtr cPointer, Outline borders);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Outline tguiButtonRenderer_getBorders(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getTextColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextColorHover(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getTextColorHover(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextColorDown(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getTextColorDown(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextColorDisabled(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getTextColorDisabled(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getBackgroundColorHover(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBackgroundColorDown(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getBackgroundColorDown(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBackgroundColorDisabled(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getBackgroundColorDisabled(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getBorderColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBorderColorHover(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getBorderColorHover(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBorderColorDown(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getBorderColorDown(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setBorderColorDisabled(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiButtonRenderer_getBorderColorDisabled(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTexture(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextureHover(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextureDown(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextureDisabled(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Text.Styles tguiButtonRenderer_getTextStyle(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextStyleHover(IntPtr cPointer, Text.Styles style);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Text.Styles tguiButtonRenderer_getTextStyleHover(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextStyleDown(IntPtr cPointer, Text.Styles style);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Text.Styles tguiButtonRenderer_getTextStyleDown(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiButtonRenderer_setTextStyleDisabled(IntPtr cPointer, Text.Styles style);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Text.Styles tguiButtonRenderer_getTextStyleDisabled(IntPtr cPointer);

		#endregion
	}
}
