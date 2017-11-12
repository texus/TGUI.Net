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
using SFML.System;

namespace TGUI
{
    public enum ScrollbarPolicy
	{
		Automatic,
		Always,
		Never
	}

	public class ScrollablePanel : Panel
	{
		public ScrollablePanel()
			: base(tguiScrollablePanel_create())
		{
		}

		protected internal ScrollablePanel(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public ScrollablePanel(ScrollablePanel copy)
			: base(copy)
		{
		}

		public new ScrollablePanelRenderer Renderer
		{
			get { return new ScrollablePanelRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public new ScrollablePanelRenderer SharedRenderer
		{
			get { return new ScrollablePanelRenderer(tguiWidget_getSharedRenderer(CPointer)); }
		}

        public Vector2f ContentSize
		{
			get { return tguiScrollablePanel_getContentSize(CPointer); }
			set { tguiScrollablePanel_setContentSize(CPointer, value); }
		}

        public float ScrollbarWidth
		{
			get { return tguiScrollablePanel_getScrollbarWidth(CPointer); }
			set { tguiScrollablePanel_setScrollbarWidth(CPointer, value); }
		}

        public ScrollbarPolicy VerticalScrollbarPolicy
		{
			get { return tguiScrollablePanel_getVerticalScrollbarPolicy(CPointer); }
			set { tguiScrollablePanel_setVerticalScrollbarPolicy(CPointer, value); }
		}

        public ScrollbarPolicy HorizontalScrollbarPolicy
		{
			get { return tguiScrollablePanel_getHorizontalScrollbarPolicy(CPointer); }
			set { tguiScrollablePanel_setHorizontalScrollbarPolicy(CPointer, value); }
		}

		public Vector2f ContentOffset
		{
			get { return tguiScrollablePanel_getContentOffset(CPointer); }
		}


	    #region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiScrollablePanel_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollablePanel_setContentSize(IntPtr cPointer, Vector2f contentSize);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiScrollablePanel_getContentSize(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollablePanel_setScrollbarWidth(IntPtr cPointer, float width);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiScrollablePanel_getScrollbarWidth(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollablePanel_setVerticalScrollbarPolicy(IntPtr cPointer, ScrollbarPolicy policy);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected ScrollbarPolicy tguiScrollablePanel_getVerticalScrollbarPolicy(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollablePanel_setHorizontalScrollbarPolicy(IntPtr cPointer, ScrollbarPolicy policy);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected ScrollbarPolicy tguiScrollablePanel_getHorizontalScrollbarPolicy(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiScrollablePanel_getContentOffset(IntPtr cPointer);

		#endregion
	}
}
