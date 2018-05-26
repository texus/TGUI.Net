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
	public class MenuBarRenderer : WidgetRenderer
	{
		public MenuBarRenderer()
			: base(tguiMenuBarRenderer_create())
		{
		}

		protected internal MenuBarRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public MenuBarRenderer(MenuBarRenderer copy)
			: base(tguiMenuBarRenderer_copy(copy.CPointer))
		{
		}

		public Color BackgroundColor
		{
			get { return tguiMenuBarRenderer_getBackgroundColor(CPointer); }
			set { tguiMenuBarRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color SelectedBackgroundColor
		{
			get { return tguiMenuBarRenderer_getSelectedBackgroundColor(CPointer); }
			set { tguiMenuBarRenderer_setSelectedBackgroundColor(CPointer, value); }
		}

		public Color TextColor
		{
			get { return tguiMenuBarRenderer_getTextColor(CPointer); }
			set { tguiMenuBarRenderer_setTextColor(CPointer, value); }
		}

		public Color SelectedTextColor
		{
			get { return tguiMenuBarRenderer_getSelectedTextColor(CPointer); }
			set { tguiMenuBarRenderer_setSelectedTextColor(CPointer, value); }
		}

		public Texture TextureBackground
		{
			set { tguiMenuBarRenderer_setTextureBackground(CPointer, value.CPointer); }
		}

		public Texture TextureItemBackground
		{
			set { tguiMenuBarRenderer_setTextureItemBackground(CPointer, value.CPointer); }
		}

		public Texture TextureSelectedItemBackground
		{
			set { tguiMenuBarRenderer_setTextureSelectedItemBackground(CPointer, value.CPointer); }
		}

		public float DistanceToSide
		{
			get { return tguiMenuBarRenderer_getDistanceToSide(CPointer); }
			set { tguiMenuBarRenderer_setDistanceToSide(CPointer, value); }
		}


		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiMenuBarRenderer_create();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiMenuBarRenderer_copy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBarRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiMenuBarRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBarRenderer_setSelectedBackgroundColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiMenuBarRenderer_getSelectedBackgroundColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBarRenderer_setTextColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiMenuBarRenderer_getTextColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBarRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiMenuBarRenderer_getSelectedTextColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBarRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBarRenderer_setTextureItemBackground(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBarRenderer_setTextureSelectedItemBackground(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBarRenderer_setDistanceToSide(IntPtr cPointer, float distanceToSide);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiMenuBarRenderer_getDistanceToSide(IntPtr cPointer);

		#endregion
	}
}
