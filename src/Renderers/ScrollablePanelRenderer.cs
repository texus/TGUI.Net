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
	public class ScrollablePanelRenderer : PanelRenderer
	{
		public ScrollablePanelRenderer()
			: base(tguiScrollablePanelRenderer_create())
		{
		}

		protected internal ScrollablePanelRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public ScrollablePanelRenderer(ScrollablePanelRenderer copy)
			: base(tguiScrollablePanelRenderer_copy(copy.CPointer))
		{
		}

		public RendererData Scrollbar
		{
			get { return new RendererData(tguiScrollablePanelRenderer_getScrollbar(CPointer)); }
			set { tguiScrollablePanelRenderer_setScrollbar(CPointer, value.CPointer); }
		}

		public float ScrollbarWidth
		{
			get { return tguiScrollablePanelRenderer_getScrollbarWidth(CPointer); }
			set { tguiScrollablePanelRenderer_setScrollbarWidth(CPointer, value); }
		}


		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiScrollablePanelRenderer_create();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiScrollablePanelRenderer_copy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollablePanelRenderer_setScrollbar(IntPtr cPointer, IntPtr rendererData);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiScrollablePanelRenderer_getScrollbar(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollablePanelRenderer_setScrollbarWidth(IntPtr cPointer, float width);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiScrollablePanelRenderer_getScrollbarWidth(IntPtr cPointer);

		#endregion
	}
}
