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
			get { return new Outline(tguiTabsRenderer_getBorders(CPointer)); }
			set { tguiTabsRenderer_setBorders(CPointer, value.CPointer); }
		}

		public Color BackgroundColor
		{
			get { return tguiTabsRenderer_getBackgroundColor(CPointer); }
			set { tguiTabsRenderer_setBackgroundColor(CPointer, value); }
		}

        public Color BackgroundColorHover
		{
			get { return tguiTabsRenderer_getBackgroundColorHover(CPointer); }
			set { tguiTabsRenderer_setBackgroundColorHover(CPointer, value); }
		}

		public Color SelectedBackgroundColor
		{
			get { return tguiTabsRenderer_getSelectedBackgroundColor(CPointer); }
			set { tguiTabsRenderer_setSelectedBackgroundColor(CPointer, value); }
		}

		public Color SelectedBackgroundColorHover
		{
			get { return tguiTabsRenderer_getSelectedBackgroundColorHover(CPointer); }
			set { tguiTabsRenderer_setSelectedBackgroundColorHover(CPointer, value); }
		}

        public Color BackgroundColorDisabled
		{
			get { return tguiTabsRenderer_getBackgroundColorDisabled(CPointer); }
			set { tguiTabsRenderer_setBackgroundColorDisabled(CPointer, value); }
		}

		public Color TextColor
		{
			get { return tguiTabsRenderer_getTextColor(CPointer); }
			set { tguiTabsRenderer_setTextColor(CPointer, value); }
		}

		public Color TextColorHover
		{
			get { return tguiTabsRenderer_getTextColorHover(CPointer); }
			set { tguiTabsRenderer_setTextColorHover(CPointer, value); }
		}

		public Color SelectedTextColor
		{
			get { return tguiTabsRenderer_getSelectedTextColor(CPointer); }
			set { tguiTabsRenderer_setSelectedTextColor(CPointer, value); }
		}

		public Color SelectedTextColorHover
		{
			get { return tguiTabsRenderer_getSelectedTextColorHover(CPointer); }
			set { tguiTabsRenderer_setSelectedTextColorHover(CPointer, value); }
		}

		public Color TextColorDisabled
		{
			get { return tguiTabsRenderer_getTextColorDisabled(CPointer); }
			set { tguiTabsRenderer_setTextColorDisabled(CPointer, value); }
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

		public Texture TextureTabHover
		{
			set { tguiTabsRenderer_setTextureTabHover(CPointer, value.CPointer); }
		}

		public Texture TextureSelectedTab
		{
			set { tguiTabsRenderer_setTextureSelectedTab(CPointer, value.CPointer); }
		}

		public Texture TextureSelectedTabHover
		{
			set { tguiTabsRenderer_setTextureSelectedTabHover(CPointer, value.CPointer); }
		}

		public Texture TextureDisabledTab
		{
			set { tguiTabsRenderer_setTextureDisabledTab(CPointer, value.CPointer); }
		}

		public float DistanceToSide
		{
			get { return tguiTabsRenderer_getDistanceToSide(CPointer); }
			set { tguiTabsRenderer_setDistanceToSide(CPointer, value); }
		}


		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabsRenderer_create();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabsRenderer_copy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setBorders(IntPtr cPointer, IntPtr borders);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabsRenderer_getBorders(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getBackgroundColorHover(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setSelectedBackgroundColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getSelectedBackgroundColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setSelectedBackgroundColorHover(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getSelectedBackgroundColorHover(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setBackgroundColorDisabled(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getBackgroundColorDisabled(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getTextColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextColorHover(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getTextColorHover(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getSelectedTextColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setSelectedTextColorHover(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getSelectedTextColorHover(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextColorDisabled(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getTextColorDisabled(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiTabsRenderer_getBorderColor(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextureTab(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextureTabHover(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextureSelectedTab(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextureSelectedTabHover(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setTextureDisabledTab(IntPtr cPointer, IntPtr texture);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabsRenderer_setDistanceToSide(IntPtr cPointer, float distanceToSide);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiTabsRenderer_getDistanceToSide(IntPtr cPointer);

		#endregion
	}
}
