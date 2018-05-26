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

namespace TGUI
{
	public class Outline : SFML.ObjectBase
	{
		////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="size">Width and height of the outline in all directions</param>
		////////////////////////////////////////////////////////////
		public Outline(float size = 0)
			: base(tguiOutline_create(size, size, size, size))
		{
		}

        ////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="size">Width and height of the outline in all directions</param>
		////////////////////////////////////////////////////////////
		public Outline(string size)
			: base(tguiOutline_createFromStrings(Util.ConvertStringForC_ASCII(size),
                                                 Util.ConvertStringForC_ASCII(size),
			                                     Util.ConvertStringForC_ASCII(size),
			                                     Util.ConvertStringForC_ASCII(size)))
		{
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="width">Width of the left and right outline</param>
		/// <param name="height">Height of the top and bottom outline</param>
		////////////////////////////////////////////////////////////
		public Outline(float width, float height)
			: base(tguiOutline_create(width, height, width, height))
		{
		}

        ////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="width">Width of the left and right outline</param>
		/// <param name="height">Height of the top and bottom outline</param>
		////////////////////////////////////////////////////////////
		public Outline(string width, string height)
			: base(tguiOutline_createFromStrings(Util.ConvertStringForC_ASCII(width),
			                                     Util.ConvertStringForC_ASCII(height),
			                                     Util.ConvertStringForC_ASCII(width),
			                                     Util.ConvertStringForC_ASCII(height)))
		{
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="left">Width of the left outline</param>
		/// <param name="top">Height of the top outline</param>
		/// <param name="right">Width of the right outline</param>
		/// <param name="bottom">Height of the bottom outline</param>
		////////////////////////////////////////////////////////////
		public Outline(float left, float top, float right, float bottom)
			: base(tguiOutline_create(left, top, right, bottom))
		{
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="left">Width of the left outline</param>
		/// <param name="top">Height of the top outline</param>
		/// <param name="right">Width of the right outline</param>
		/// <param name="bottom">Height of the bottom outline</param>
		////////////////////////////////////////////////////////////
		public Outline(string left, string top, string right, string bottom)
			: base(tguiOutline_createFromStrings(Util.ConvertStringForC_ASCII(left),
			                                     Util.ConvertStringForC_ASCII(top),
			                                     Util.ConvertStringForC_ASCII(right),
			                                     Util.ConvertStringForC_ASCII(bottom)))
		{
		}

        protected internal Outline(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Outline(Outline copy)
			: base(tguiOutline_copy(copy.CPointer))
		{
		}

        protected override void Destroy(bool disposing)
		{
			tguiOutline_destroy(CPointer);
		}

        public float Left
		{
			get { return tguiOutline_getLeft(CPointer); }
		}

        public float Top
		{
			get { return tguiOutline_getTop(CPointer); }
		}

        public float Right
		{
			get { return tguiOutline_getRight(CPointer); }
		}

        public float Bottom
		{
			get { return tguiOutline_getBottom(CPointer); }
		}


		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiOutline_create(float left, float top, float right, float bottom);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiOutline_createFromStrings(IntPtr left, IntPtr top, IntPtr right, IntPtr bottom);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiOutline_copy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiOutline_destroy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiOutline_getLeft(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiOutline_getTop(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiOutline_getRight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiOutline_getBottom(IntPtr cPointer);

		#endregion
	}
}
