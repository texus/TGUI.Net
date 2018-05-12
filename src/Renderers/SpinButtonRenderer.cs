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
	public class SpinButtonRenderer : WidgetRenderer
	{
		public SpinButtonRenderer()
			: base(tguiSpinButtonRenderer_create())
		{
		}

		protected internal SpinButtonRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public SpinButtonRenderer(SpinButtonRenderer copy)
			: base(tguiSpinButtonRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return new Outline(tguiSpinButtonRenderer_getBorders(CPointer)); }
			set { tguiSpinButtonRenderer_setBorders(CPointer, value.CPointer); }
		}

		public float BorderBetweenArrows
		{
			get { return tguiSpinButtonRenderer_getBorderBetweenArrows(CPointer); }
			set { tguiSpinButtonRenderer_setBorderBetweenArrows(CPointer, value); }
		}

		public Color BackgroundColor
		{
			get { return tguiSpinButtonRenderer_getBackgroundColor(CPointer); }
			set { tguiSpinButtonRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color BackgroundColorHover
		{
			get { return tguiSpinButtonRenderer_getBackgroundColorHover(CPointer); }
			set { tguiSpinButtonRenderer_setBackgroundColorHover(CPointer, value); }
		}

		public Color ArrowColor
		{
			get { return tguiSpinButtonRenderer_getArrowColor(CPointer); }
			set { tguiSpinButtonRenderer_setArrowColor(CPointer, value); }
		}

		public Color ArrowColorHover
		{
			get { return tguiSpinButtonRenderer_getArrowColorHover(CPointer); }
			set { tguiSpinButtonRenderer_setArrowColorHover(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiSpinButtonRenderer_getBorderColor(CPointer); }
			set { tguiSpinButtonRenderer_setBorderColor(CPointer, value); }
		}

		public Texture TextureArrowUp
		{
			set { tguiSpinButtonRenderer_setTextureArrowUp(CPointer, value.CPointer); }
		}

		public Texture TextureArrowUpHover
		{
			set { tguiSpinButtonRenderer_setTextureArrowUpHover(CPointer, value.CPointer); }
		}

		public Texture TextureArrowDown
		{
			set { tguiSpinButtonRenderer_setTextureArrowDown(CPointer, value.CPointer); }
		}

		public Texture TextureArrowDownHover
		{
			set { tguiSpinButtonRenderer_setTextureArrowDownHover(CPointer, value.CPointer); }
		}

		#region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiSpinButtonRenderer_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiSpinButtonRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setBorders(IntPtr cPointer, IntPtr borders);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiSpinButtonRenderer_getBorders(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setBorderBetweenArrows(IntPtr cPointer, float border);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiSpinButtonRenderer_getBorderBetweenArrows(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiSpinButtonRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiSpinButtonRenderer_getBackgroundColorHover(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setArrowColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiSpinButtonRenderer_getArrowColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setArrowColorHover(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiSpinButtonRenderer_getArrowColorHover(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiSpinButtonRenderer_getBorderColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setTextureArrowUp(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setTextureArrowUpHover(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setTextureArrowDown(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSpinButtonRenderer_setTextureArrowDownHover(IntPtr cPointer, IntPtr texture);

		#endregion
	}
}
