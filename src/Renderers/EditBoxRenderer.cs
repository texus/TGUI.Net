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
	public class EditBoxRenderer : WidgetRenderer
	{
		public EditBoxRenderer()
			: base(tguiEditBoxRenderer_create())
		{
		}

		protected internal EditBoxRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public EditBoxRenderer(EditBoxRenderer copy)
			: base(tguiEditBoxRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return new Outline(tguiEditBoxRenderer_getBorders(CPointer)); }
			set { tguiEditBoxRenderer_setBorders(CPointer, value.CPointer); }
		}

		public Outline Padding
		{
			get { return new Outline(tguiEditBoxRenderer_getPadding(CPointer)); }
			set { tguiEditBoxRenderer_setPadding(CPointer, value.CPointer); }
		}

		public float CaretWidth
		{
			get { return tguiEditBoxRenderer_getCaretWidth(CPointer); }
			set { tguiEditBoxRenderer_setCaretWidth(CPointer, value); }
		}

		public Color TextColor
		{
			get { return tguiEditBoxRenderer_getTextColor(CPointer); }
			set { tguiEditBoxRenderer_setTextColor(CPointer, value); }
		}

		public Color DefaultTextColor
		{
			get { return tguiEditBoxRenderer_getDefaultTextColor(CPointer); }
			set { tguiEditBoxRenderer_setDefaultTextColor(CPointer, value); }
		}

		public Color TextColorFocused
		{
			get { return tguiEditBoxRenderer_getTextColorFocused(CPointer); }
			set { tguiEditBoxRenderer_setTextColorFocused(CPointer, value); }
		}

		public Color TextColorDisabled
		{
			get { return tguiEditBoxRenderer_getTextColorDisabled(CPointer); }
			set { tguiEditBoxRenderer_setTextColorDisabled(CPointer, value); }
		}

		public Color SelectedTextColor
		{
			get { return tguiEditBoxRenderer_getSelectedTextColor(CPointer); }
			set { tguiEditBoxRenderer_setSelectedTextColor(CPointer, value); }
		}

		public Color SelectedTextBackgroundColor
		{
			get { return tguiEditBoxRenderer_getSelectedTextBackgroundColor(CPointer); }
			set { tguiEditBoxRenderer_setSelectedTextBackgroundColor(CPointer, value); }
		}

		public Color BackgroundColor
		{
			get { return tguiEditBoxRenderer_getBackgroundColor(CPointer); }
			set { tguiEditBoxRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color BackgroundColorHover
		{
			get { return tguiEditBoxRenderer_getBackgroundColorHover(CPointer); }
			set { tguiEditBoxRenderer_setBackgroundColorHover(CPointer, value); }
		}

		public Color BackgroundColorFocused
		{
			get { return tguiEditBoxRenderer_getBackgroundColorFocused(CPointer); }
			set { tguiEditBoxRenderer_setBackgroundColorFocused(CPointer, value); }
		}

		public Color BackgroundColorDisabled
		{
			get { return tguiEditBoxRenderer_getBackgroundColorDisabled(CPointer); }
			set { tguiEditBoxRenderer_setBackgroundColorDisabled(CPointer, value); }
		}

		public Color CaretColor
		{
			get { return tguiEditBoxRenderer_getCaretColor(CPointer); }
			set { tguiEditBoxRenderer_setCaretColor(CPointer, value); }
		}

		public Color CaretColorHover
		{
			get { return tguiEditBoxRenderer_getCaretColorHover(CPointer); }
			set { tguiEditBoxRenderer_setCaretColorHover(CPointer, value); }
		}

		public Color CaretColorFocused
		{
			get { return tguiEditBoxRenderer_getCaretColorFocused(CPointer); }
			set { tguiEditBoxRenderer_setCaretColorFocused(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiEditBoxRenderer_getBorderColor(CPointer); }
			set { tguiEditBoxRenderer_setBorderColor(CPointer, value); }
		}

		public Color BorderColorHover
		{
			get { return tguiEditBoxRenderer_getBorderColorHover(CPointer); }
			set { tguiEditBoxRenderer_setBorderColorHover(CPointer, value); }
		}

		public Color BorderColorFocused
		{
			get { return tguiEditBoxRenderer_getBorderColorFocused(CPointer); }
			set { tguiEditBoxRenderer_setBorderColorFocused(CPointer, value); }
		}

		public Color BorderColorDisabled
		{
			get { return tguiEditBoxRenderer_getBorderColorDisabled(CPointer); }
			set { tguiEditBoxRenderer_setBorderColorDisabled(CPointer, value); }
		}

		public Texture Texture
		{
			set { tguiEditBoxRenderer_setTexture(CPointer, value.CPointer); }
		}

		public Texture TextureHover
		{
			set { tguiEditBoxRenderer_setTextureHover(CPointer, value.CPointer); }
		}

		public Texture TextureFocused
		{
			set { tguiEditBoxRenderer_setTextureFocused(CPointer, value.CPointer); }
		}

		public Texture TextureDisabled
		{
			set { tguiEditBoxRenderer_setTextureDisabled(CPointer, value.CPointer); }
		}

		public Text.Styles TextStyle
		{
			get { return tguiEditBoxRenderer_getTextStyle(CPointer); }
			set { tguiEditBoxRenderer_setTextStyle(CPointer, value); }
		}

		public Text.Styles DefaultTextStyle
		{
			get { return tguiEditBoxRenderer_getDefaultTextStyle(CPointer); }
			set { tguiEditBoxRenderer_setDefaultTextStyle(CPointer, value); }
		}


		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBoxRenderer_create();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBoxRenderer_copy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBorders(IntPtr cPointer, IntPtr borders);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBoxRenderer_getBorders(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setPadding(IntPtr cPointer, IntPtr padding);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBoxRenderer_getPadding(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setCaretWidth(IntPtr cPointer, float width);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiEditBoxRenderer_getCaretWidth(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setTextColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getTextColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setDefaultTextColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getDefaultTextColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setTextColorFocused(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getTextColorFocused(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setTextColorDisabled(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getTextColorDisabled(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getSelectedTextColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setSelectedTextBackgroundColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getSelectedTextBackgroundColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getBackgroundColorHover(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBackgroundColorFocused(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getBackgroundColorFocused(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBackgroundColorDisabled(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getBackgroundColorDisabled(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setCaretColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getCaretColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setCaretColorHover(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getCaretColorHover(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setCaretColorFocused(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getCaretColorFocused(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getBorderColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBorderColorHover(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getBorderColorHover(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBorderColorFocused(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getBorderColorFocused(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setBorderColorDisabled(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiEditBoxRenderer_getBorderColorDisabled(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setTexture(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setTextureHover(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setTextureFocused(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setTextureDisabled(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Text.Styles tguiEditBoxRenderer_getTextStyle(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBoxRenderer_setDefaultTextStyle(IntPtr cPointer, Text.Styles style);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Text.Styles tguiEditBoxRenderer_getDefaultTextStyle(IntPtr cPointer);

		#endregion
	}
}
