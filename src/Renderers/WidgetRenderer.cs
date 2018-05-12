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
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using SFML.Graphics;

namespace TGUI
{
	public class WidgetRenderer : SFML.ObjectBase
	{
		public WidgetRenderer()
			: base(tguiWidgetRenderer_create())
		{
		}

		protected internal WidgetRenderer(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public WidgetRenderer(WidgetRenderer copy)
			: base(tguiWidgetRenderer_copy(copy.CPointer))
		{
		}

		protected override void Destroy(bool disposing)
		{
			tguiWidgetRenderer_destroy(CPointer);
		}

		public float Opacity
		{
			get { return tguiWidgetRenderer_getOpacity(CPointer); }
			set { tguiWidgetRenderer_setOpacity(CPointer, value); }
		}

		public Font Font
		{
			set { tguiWidgetRenderer_setFont(CPointer, value.CPointer); }
		}

		public bool TransparentTexture
		{
			get { return tguiWidgetRenderer_getTransparentTexture(CPointer); }
			set { tguiWidgetRenderer_setTransparentTexture(CPointer, value); }
		}

		public RendererData Data
		{
			get { return new RendererData(tguiWidgetRenderer_getData(CPointer)); }
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Provide a string describing the object
		/// </summary>
		/// <returns>String description of the object</returns>
		////////////////////////////////////////////////////////////
		public override string ToString()
		{
			return "[WidgetRenderer]";
		}


		#region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidgetRenderer_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidgetRenderer_copy(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidgetRenderer_destroy(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidgetRenderer_setOpacity(IntPtr cPointer, float opacity);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiWidgetRenderer_getOpacity(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidgetRenderer_setFont(IntPtr cPointer, IntPtr font);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidgetRenderer_setTransparentTexture(IntPtr cPointer, bool ignoreTransparentParts);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiWidgetRenderer_getTransparentTexture(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidgetRenderer_getData(IntPtr cPointer);

		#endregion
	}
}
