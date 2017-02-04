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
using System.Collections.Generic;

namespace TGUI
{
	public class MenuBar : Widget
	{
		public MenuBar()
			: base(tguiMenuBar_create())
		{
		}

		protected internal MenuBar(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public MenuBar(MenuBar copy)
			: base(copy)
		{
		}

		public new MenuBarRenderer Renderer
		{
			get { return new MenuBarRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public void AddMenu(string text)
		{
			tguiMenuBar_addMenu(CPointer, Util.ConvertStringForC_UTF32(text));
		}

		public bool AddMenuItem(string menu, string text)
		{
			return tguiMenuBar_addMenuItem(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(text));
		}

		public bool AddMenuItem(string text)
		{
			return tguiMenuBar_addMenuItemToLastMenu(CPointer, Util.ConvertStringForC_UTF32(text));
		}

		public bool RemoveMenu(string menu)
		{
			return tguiMenuBar_removeMenu(CPointer, Util.ConvertStringForC_UTF32(menu));
		}

		public void RemoveAllMenus()
		{
			tguiMenuBar_removeAllMenus(CPointer);
		}

		public bool RemoveMenuItem(string menu, string menuItem)
		{
			return tguiMenuBar_removeMenuItem(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(menuItem));
		}

		public uint TextSize
		{
			get { return tguiMenuBar_getTextSize(CPointer); }
			set { tguiMenuBar_setTextSize(CPointer, value); }
		}

		public float MinimumSubMenuWidth
		{
			get { return tguiMenuBar_getMinimumSubMenuWidth(CPointer); }
			set { tguiMenuBar_setMinimumSubMenuWidth(CPointer, value); }
		}

		public bool InvertedMenuDirection {
			get { return tguiMenuBar_getInvertedMenuDirection (CPointer); }
			set { tguiMenuBar_setInvertedMenuDirection (CPointer, value); }
		}

		protected override void InitSignals()
		{
			base.InitSignals();

			IntPtr error;

		    MenuItemClickedCallback = new CallbackActionString(ProcessMenuItemClickedSignal);
		    tguiWidget_connect_string(CPointer, Util.ConvertStringForC_ASCII("MenuItemClicked"), MenuItemClickedCallback, out error);
		    if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		private void ProcessMenuItemClickedSignal(IntPtr menuItem)
		{
			if (MenuItemClicked != null)
				MenuItemClicked(this, new SignalArgsString(Util.GetStringFromC_UTF32(menuItem)));
		}

		/// <summary>Event handler for the ItemSelected signal</summary>
		public event EventHandler<SignalArgsString> MenuItemClicked = null;

	    private CallbackActionString MenuItemClickedCallback;


	    #region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiMenuBar_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBar_addMenu(IntPtr cPointer, IntPtr text);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiMenuBar_addMenuItem(IntPtr cPointer, IntPtr menu, IntPtr text);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiMenuBar_addMenuItemToLastMenu(IntPtr cPointer, IntPtr text);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiMenuBar_removeMenu(IntPtr cPointer, IntPtr menu);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBar_removeAllMenus(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiMenuBar_removeMenuItem(IntPtr cPointer, IntPtr menu, IntPtr item);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBar_setTextSize(IntPtr cPointer, uint textSize);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiMenuBar_getTextSize(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBar_setMinimumSubMenuWidth(IntPtr cPointer, float minimumSubMenuWidth);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiMenuBar_getMinimumSubMenuWidth(IntPtr cPointer);

		[DllImport ("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiMenuBar_setInvertedMenuDirection (IntPtr cPointer, bool invertDirection);

		[DllImport ("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiMenuBar_getInvertedMenuDirection (IntPtr cPointer);

		#endregion
	}
}
