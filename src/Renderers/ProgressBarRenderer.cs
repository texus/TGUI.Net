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
	public class ProgressBarRenderer : WidgetRenderer
	{
		public ProgressBarRenderer()
			: base(tguiProgressBarRenderer_create())
		{
		}

		protected internal ProgressBarRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public ProgressBarRenderer(ProgressBarRenderer copy)
			: base(tguiProgressBarRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return new Outline(tguiProgressBarRenderer_getBorders(CPointer)); }
			set { tguiProgressBarRenderer_setBorders(CPointer, value.CPointer); }
		}

		public Color TextColor
		{
			get { return tguiProgressBarRenderer_getTextColor(CPointer); }
			set { tguiProgressBarRenderer_setTextColor(CPointer, value); }
		}

		public Color TextColorFilled
		{
			get { return tguiProgressBarRenderer_getTextColorFilled(CPointer); }
			set { tguiProgressBarRenderer_setTextColorFilled(CPointer, value); }
		}

		public Color BackgroundColor
		{
			get { return tguiProgressBarRenderer_getBackgroundColor(CPointer); }
			set { tguiProgressBarRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color FillColor
		{
			get { return tguiProgressBarRenderer_getFillColor(CPointer); }
			set { tguiProgressBarRenderer_setFillColor(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiProgressBarRenderer_getBorderColor(CPointer); }
			set { tguiProgressBarRenderer_setBorderColor(CPointer, value); }
		}

		public Texture TextureBackground
		{
			set { tguiProgressBarRenderer_setTextureBackground(CPointer, value.CPointer); }
		}

		public Texture TextureFill
		{
			set { tguiProgressBarRenderer_setTextureFill(CPointer, value.CPointer); }
		}

		public Text.Styles TextStyle
		{
			get { return tguiProgressBarRenderer_getTextStyle(CPointer); }
			set { tguiProgressBarRenderer_setTextStyle(CPointer, value); }
		}


		#region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiProgressBarRenderer_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiProgressBarRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setBorders(IntPtr cPointer, IntPtr borders);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiProgressBarRenderer_getBorders(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setTextColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiProgressBarRenderer_getTextColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setTextColorFilled(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiProgressBarRenderer_getTextColorFilled(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiProgressBarRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setFillColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiProgressBarRenderer_getFillColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiProgressBarRenderer_getBorderColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setTextureFill(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiProgressBarRenderer_setTextStyle(IntPtr cPointer, Text.Styles style);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Text.Styles tguiProgressBarRenderer_getTextStyle(IntPtr cPointer);

		#endregion
	}
}
