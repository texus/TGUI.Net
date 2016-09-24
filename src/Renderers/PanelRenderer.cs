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
	public class PanelRenderer : WidgetRenderer
	{
		public PanelRenderer()
			: base(tguiPanelRenderer_create())
		{
		}

		internal PanelRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public PanelRenderer(PanelRenderer copy)
			: base(tguiPanelRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return tguiPanelRenderer_getBorders(CPointer); }
			set { tguiPanelRenderer_setBorders(CPointer, value); }
		}

		public Color BackgroundColor
		{
			get { return tguiPanelRenderer_getBackgroundColor(CPointer); }
			set { tguiPanelRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiPanelRenderer_getBorderColor(CPointer); }
			set { tguiPanelRenderer_setBorderColor(CPointer, value); }
		}

		#region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiPanelRenderer_create();

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiPanelRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiPanelRenderer_setBorders(IntPtr cPointer, Outline borders);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Outline tguiPanelRenderer_getBorders(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiPanelRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Color tguiPanelRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiPanelRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Color tguiPanelRenderer_getBorderColor(IntPtr cPointer);

		#endregion
	}
}
