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
	public class KnobRenderer : WidgetRenderer
	{
		public KnobRenderer()
			: base(tguiKnobRenderer_create())
		{
		}

		protected internal KnobRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public KnobRenderer(KnobRenderer copy)
			: base(tguiKnobRenderer_copy(copy.CPointer))
		{
		}

		public Outline Borders
		{
			get { return tguiKnobRenderer_getBorders(CPointer); }
			set { tguiKnobRenderer_setBorders(CPointer, value); }
		}

		public Color BackgroundColor
		{
			get { return tguiKnobRenderer_getBackgroundColor(CPointer); }
			set { tguiKnobRenderer_setBackgroundColor(CPointer, value); }
		}

		public Color ThumbColor
		{
			get { return tguiKnobRenderer_getThumbColor(CPointer); }
			set { tguiKnobRenderer_setThumbColor(CPointer, value); }
		}

		public Color BorderColor
		{
			get { return tguiKnobRenderer_getBorderColor(CPointer); }
			set { tguiKnobRenderer_setBorderColor(CPointer, value); }
		}

		public Texture TextureBackground
		{
			set { tguiKnobRenderer_setTextureBackground(CPointer, value.CPointer); }
		}

		public Texture TextureForeground
		{
			set { tguiKnobRenderer_setTextureForeground(CPointer, value.CPointer); }
		}

		public float ImageRotation
		{
			get { return tguiKnobRenderer_getImageRotation(CPointer); }
			set { tguiKnobRenderer_setImageRotation(CPointer, value); }
		}


		#region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiKnobRenderer_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiKnobRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnobRenderer_setBorders(IntPtr cPointer, Outline borders);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Outline tguiKnobRenderer_getBorders(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnobRenderer_setBackgroundColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiKnobRenderer_getBackgroundColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnobRenderer_setThumbColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiKnobRenderer_getThumbColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnobRenderer_setBorderColor(IntPtr cPointer, Color color);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Color tguiKnobRenderer_getBorderColor(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnobRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnobRenderer_setTextureForeground(IntPtr cPointer, IntPtr texture);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnobRenderer_setImageRotation(IntPtr cPointer, float rotation);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiKnobRenderer_getImageRotation(IntPtr cPointer);

		#endregion
	}
}
