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
using SFML.System;

namespace TGUI
{
	public class Layout2d : SFML.ObjectBase
	{
        public Layout2d()
			: base(tguiLayout2d_create(new Vector2f(0, 0)))
		{
		}

		public Layout2d(Vector2f constant)
			: base(tguiLayout2d_create(constant))
		{
		}

        public Layout2d(float x, float y)
			: base(tguiLayout2d_create(new Vector2f(x, y)))
		{
		}

		public Layout2d(Layout x, Layout y)
			: base(tguiLayout2d_createFromLayouts(x.CPointer, y.CPointer))
		{
		}

		public Layout2d(string xExpression, string yExpression)
			: this(new Layout(xExpression), new Layout(yExpression))
		{
		}

        public Layout2d(string expression)
			: base(tguiLayout2d_createFromString(Util.ConvertStringForC_ASCII(expression)))
		{
		}

		public Layout2d(Layout2d copy)
			: base(tguiLayout2d_copy(copy.CPointer))
		{
		}

		protected override void Destroy(bool disposing)
		{
			tguiLayout2d_destroy(CPointer);
		}

		public Vector2f Value
		{
			get { return tguiLayout2d_getValue(CPointer); }
		}

		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiLayout2d_create(Vector2f constant);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiLayout2d_createFromLayouts(IntPtr x, IntPtr y);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiLayout2d_createFromString(IntPtr expression);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiLayout2d_copy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiLayout2d_destroy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiLayout2d_getValue(IntPtr cPointer);

		#endregion
	}
}
