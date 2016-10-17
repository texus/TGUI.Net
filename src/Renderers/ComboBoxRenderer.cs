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
	public class ComboBoxRenderer : WidgetRenderer
	{
		public ComboBoxRenderer()
			: base(tguiComboBoxRenderer_create())
		{
		}

		protected internal ComboBoxRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public ComboBoxRenderer(ComboBoxRenderer copy)
			: base(tguiComboBoxRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return tguiComboBoxRenderer_getBorders(CPointer); }
			set { tguiComboBoxRenderer_setBorders(CPointer, value); }
		}

		public Outline Padding
		{
			get { return tguiComboBoxRenderer_getPadding(CPointer); }
			set { tguiComboBoxRenderer_setPadding(CPointer, value); }
		}

		public Color BackgroundColor
		{
			get { return tguiComboBoxRenderer_getBackgroundColor(CPointer); }
			set { tguiComboBoxRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color TextColor
		{
			get { return tguiComboBoxRenderer_getTextColor(CPointer); }
			set { tguiComboBoxRenderer_setTextColor(CPointer, value); }
		}

		public Color ArrowBackgroundColor
		{
			get { return tguiComboBoxRenderer_getArrowBackgroundColor(CPointer); }
			set { tguiComboBoxRenderer_setArrowBackgroundColor(CPointer, value); }
		}

		public Color ArrowBackgroundColorHover
		{
			get { return tguiComboBoxRenderer_getArrowBackgroundColorHover(CPointer); }
			set { tguiComboBoxRenderer_setArrowBackgroundColorHover(CPointer, value); }
		}

		public Color ArrowColor
		{
			get { return tguiComboBoxRenderer_getArrowColor(CPointer); }
			set { tguiComboBoxRenderer_setArrowColor(CPointer, value); }
		}

		public Color ArrowColorHover
		{
			get { return tguiComboBoxRenderer_getArrowColorHover(CPointer); }
			set { tguiComboBoxRenderer_setArrowColorHover(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiComboBoxRenderer_getBorderColor(CPointer); }
			set { tguiComboBoxRenderer_setBorderColor(CPointer, value); }
		}

		public Texture TextureBackground
		{
			set { tguiComboBoxRenderer_setTextureBackground(CPointer, value.CPointer); }
		}

		public Texture TextureArrowUp
		{
			set { tguiComboBoxRenderer_setTextureArrowUp(CPointer, value.CPointer); }
		}

		public Texture TextureArrowUpHover
		{
			set { tguiComboBoxRenderer_setTextureArrowUpHover(CPointer, value.CPointer); }
		}

		public Texture TextureArrowDown
		{
			set { tguiComboBoxRenderer_setTextureArrowDown(CPointer, value.CPointer); }
		}

		public Texture TextureArrowDownHover
		{
			set { tguiComboBoxRenderer_setTextureArrowDownHover(CPointer, value.CPointer); }
		}

		public Text.Styles TextStyle
		{
			get { return tguiComboBoxRenderer_getTextStyle(CPointer); }
			set { tguiComboBoxRenderer_setTextStyle(CPointer, value); }
		}

		public RendererData ListBox
		{
			get { return new RendererData(tguiComboBoxRenderer_getListBox(CPointer)); }
			set { tguiComboBoxRenderer_setListBox(CPointer, value.CPointer); }
		}


		#region Imports

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiComboBoxRenderer_create();

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiComboBoxRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setBorders(IntPtr cPointer, Outline borders);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Outline tguiComboBoxRenderer_getBorders(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setPadding(IntPtr cPointer, Outline borders);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Outline tguiComboBoxRenderer_getPadding(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiComboBoxRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setTextColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiComboBoxRenderer_getTextColor(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setArrowBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiComboBoxRenderer_getArrowBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setArrowBackgroundColorHover(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiComboBoxRenderer_getArrowBackgroundColorHover(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setArrowColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiComboBoxRenderer_getArrowColor(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setArrowColorHover(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiComboBoxRenderer_getArrowColorHover(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiComboBoxRenderer_getBorderColor(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setTextureArrowUp(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setTextureArrowUpHover(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setTextureArrowDown(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setTextureArrowDownHover(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Text.Styles tguiComboBoxRenderer_getTextStyle(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiComboBoxRenderer_setListBox(IntPtr cPointer, IntPtr rendererData);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiComboBoxRenderer_getListBox(IntPtr cPointer);

		#endregion
	}
}
