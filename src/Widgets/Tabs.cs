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

        public new TabsRenderer SharedRenderer
		{
			get { return new TabsRenderer(tguiWidget_getSharedRenderer(CPointer)); }
		}

        public bool AutoSize
		{
			get { return tguiTabs_getAutoSize(CPointer); }
			set { tguiTabs_setAutoSize(CPointer, value); }
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

		public void SetTabVisible(uint index, bool visible)
		{
			tguiTabs_setTabVisible(CPointer, index, visible);
		}

		public bool GetTabVisible(uint index)
		{
			return tguiTabs_getTabVisible(CPointer, index);
		}

		public void SetTabEnabled(uint index, bool enabled)
		{
			tguiTabs_setTabEnabled(CPointer, index, enabled);
		}

		public bool GetTabEnabled(uint index)
		{
			return tguiTabs_getTabEnabled(CPointer, index);
		}

		public uint TextSize
		{
			get { return tguiTabs_getTextSize(CPointer); }
			set { tguiTabs_setTextSize(CPointer, value); }
		}

		public float MaximumTabWidth
		{
			get { return tguiTabs_getMaximumTabWidth(CPointer); }
			set { tguiTabs_setMaximumTabWidth(CPointer, value); }
		}

		public float MinimumTabWidth
		{
			get { return tguiTabs_getMinimumTabWidth(CPointer); }
			set { tguiTabs_setMinimumTabWidth(CPointer, value); }
		}

		protected override void InitSignals()
		{
			base.InitSignals();

            TabSelectedCallback = new CallbackActionString(ProcessTabSelectedSignal);
		    if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("TabSelected"), TabSelectedCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
		}

		private void ProcessTabSelectedSignal(IntPtr tab)
		{
            TabSelected?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(tab)));
        }

		/// <summary>Event handler for the TabSelected signal</summary>
		public event EventHandler<SignalArgsString> TabSelected = null;

	    private CallbackActionString TabSelectedCallback;

		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabs_create();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setAutoSize(IntPtr cPointer, bool autoSize);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiTabs_getAutoSize(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTabs_add(IntPtr cPointer, IntPtr text, bool selectTab);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_insert(IntPtr cPointer, uint index, IntPtr text, bool selectTab);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabs_getText(IntPtr cPointer, uint index);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiTabs_changeText(IntPtr cPointer, uint index, IntPtr text);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_selectByText(IntPtr cPointer, IntPtr text);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_selectByIndex(IntPtr cPointer, uint index);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_deselect(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_removeByText(IntPtr cPointer, IntPtr text);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_removeByIndex(IntPtr cPointer, uint index);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_removeAll(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTabs_getSelected(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiTabs_getSelectedIndex(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTabs_getTabsCount(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setTabVisible(IntPtr cPointer, uint index, bool visible);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiTabs_getTabVisible(IntPtr cPointer, uint index);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setTabEnabled(IntPtr cPointer, uint index, bool enabled);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiTabs_getTabEnabled(IntPtr cPointer, uint index);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setTextSize(IntPtr cPointer, uint textSize);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTabs_getTextSize(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setMaximumTabWidth(IntPtr cPointer, float maximumTabWidth);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiTabs_getMaximumTabWidth(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTabs_setMinimumTabWidth(IntPtr cPointer, float minimumTabWidth);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiTabs_getMinimumTabWidth(IntPtr cPointer);

		#endregion
	}
}
