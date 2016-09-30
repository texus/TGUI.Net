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
	public class TabsRenderer : WidgetRenderer
	{
		public TabsRenderer()
			: base(tguiTabsRenderer_create())
		{
		}

		protected internal TabsRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public TabsRenderer(TabsRenderer copy)
			: base(tguiTabsRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return tguiTabsRenderer_getBorders(CPointer); }
			set { tguiTabsRenderer_setBorders(CPointer, value); }
		}

		public Color BackgroundColor
		{
			get { return tguiTabsRenderer_getBackgroundColor(CPointer); }
			set { tguiTabsRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color SelectedBackgroundColor
		{
			get { return tguiTabsRenderer_getSelectedBackgroundColor(CPointer); }
			set { tguiTabsRenderer_setSelectedBackgroundColor(CPointer, value); }
		}

		public Color TextColor
		{
			get { return tguiTabsRenderer_getTextColor(CPointer); }
			set { tguiTabsRenderer_setTextColor(CPointer, value); }
		}

		public Color SelectedTextColor
		{
			get { return tguiTabsRenderer_getSelectedTextColor(CPointer); }
			set { tguiTabsRenderer_setSelectedTextColor(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiTabsRenderer_getBorderColor(CPointer); }
			set { tguiTabsRenderer_setBorderColor(CPointer, value); }
		}

		public Texture TextureTab
		{
			set { tguiTabsRenderer_setTextureTab(CPointer, value.CPointer); }
		}

		public Texture TextureSelectedTab
		{
			set { tguiTabsRenderer_setTextureSelectedTab(CPointer, value.CPointer); }
		}

		public float DistanceToSide
		{
			get { return tguiTabsRenderer_getDistanceToSide(CPointer); }
			set { tguiTabsRenderer_setDistanceToSide(CPointer, value); }
		}


		#region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabsRenderer_create();

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabsRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setBorders(IntPtr cPointer, Outline borders);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Outline tguiTabsRenderer_getBorders(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setSelectedBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getSelectedBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getTextColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getSelectedTextColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getBorderColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextureTab(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextureSelectedTab(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setDistanceToSide(IntPtr cPointer, float distanceToSide);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiTabsRenderer_getDistanceToSide(IntPtr cPointer);

		#endregion
	}
}
