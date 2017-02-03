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
	public class Tabs : Widget
	{
		public Tabs()
			: base(tguiTabs_create())
		{
		}

		protected internal Tabs(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Tabs(Tabs copy)
			: base(copy)
		{
		}

		public new TabsRenderer Renderer
		{
			get { return new TabsRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public uint Add(string text, bool selectTab = true)
		{
			return tguiTabs_add(CPointer, Util.ConvertStringForC_UTF32(text), selectTab);
		}

		public void Insert(uint index, string text, bool selectTab = true)
		{
			tguiTabs_insert(CPointer, index, Util.ConvertStringForC_UTF32(text), selectTab);
		}

		public string GetText(uint index)
		{
			return Util.GetStringFromC_UTF32(tguiTabs_getText(CPointer, index));
		}

		public bool ChangeText(uint index, string text)
		{
			return tguiTabs_changeText(CPointer, index, Util.ConvertStringForC_UTF32(text));
		}

		public void Select(string text)
		{
			tguiTabs_selectByText(CPointer, Util.ConvertStringForC_UTF32(text));
		}

		public void Select(uint index)
		{
			tguiTabs_selectByIndex(CPointer, index);
		}

		public void Deselect()
		{
			tguiTabs_deselect(CPointer);
		}

		public void Remove(string text)
		{
			tguiTabs_removeByText(CPointer, Util.ConvertStringForC_UTF32(text));
		}

		public void Remove(uint index)
		{
			tguiTabs_removeByIndex(CPointer, index);
		}

		public void RemoveAll()
		{
			tguiTabs_removeAll(CPointer);
		}

		public string GetSelected()
		{
			return Util.GetStringFromC_UTF32(tguiTabs_getSelected(CPointer));
		}

		public int GetSelectedIndex()
		{
			return tguiTabs_getSelectedIndex(CPointer);
		}

		public uint GetTabsCount()
		{
			return tguiTabs_getTabsCount(CPointer);
		}

		public uint TextSize
		{
			get { return tguiTabs_getTextSize(CPointer); }
			set { tguiTabs_setTextSize(CPointer, value); }
		}

		public float TabHeight
		{
			get { return tguiTabs_getTabHeight(CPointer); }
			set { tguiTabs_setTabHeight(CPointer, value); }
		}

		public float MaximumTabWidth
		{
			get { return tguiTabs_getMaximumTabWidth(CPointer); }
			set { tguiTabs_setMaximumTabWidth(CPointer, value); }
		}


		protected override void InitSignals()
		{
			base.InitSignals();

			IntPtr error;
			tguiWidget_connect_string(CPointer, Util.ConvertStringForC_ASCII("TabSelected"), ProcessTabSelectedSignal, out error);
			if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		private void ProcessTabSelectedSignal(IntPtr tab)
		{
			if (TabSelected != null)
				TabSelected(this, new SignalArgsString(Util.GetStringFromC_UTF32(tab)));
		}

		/// <summary>Event handler for the TabSelected signal</summary>
		public event EventHandler<SignalArgsString> TabSelected = null;


		#region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabs_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTabs_add(IntPtr cPointer, IntPtr text, bool selectTab);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_insert(IntPtr cPointer, uint index, IntPtr text, bool selectTab);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabs_getText(IntPtr cPointer, uint index);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiTabs_changeText(IntPtr cPointer, uint index, IntPtr text);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_selectByText(IntPtr cPointer, IntPtr text);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_selectByIndex(IntPtr cPointer, uint index);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_deselect(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_removeByText(IntPtr cPointer, IntPtr text);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_removeByIndex(IntPtr cPointer, uint index);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_removeAll(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabs_getSelected(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiTabs_getSelectedIndex(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTabs_getTabsCount(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setTextSize(IntPtr cPointer, uint textSize);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTabs_getTextSize(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setTabHeight(IntPtr cPointer, float tabHeight);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiTabs_getTabHeight(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setMaximumTabWidth(IntPtr cPointer, float maximumTabWidth);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiTabs_getMaximumTabWidth(IntPtr cPointer);

		#endregion
	}
}
