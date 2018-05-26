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
	public class Group : Container
	{
		public Group()
			: base(tguiGroup_create())
		{
		}

		public Group(Vector2f size)
			: this()
		{
			Size = size;
		}

		public Group(float width, float height)
			: this(new Vector2f(width, height))
		{
		}

		protected internal Group(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Group(Group copy)
			: base(copy)
		{
		}

		public new GroupRenderer Renderer
		{
			get { return new GroupRenderer(tguiWidget_getRenderer(CPointer)); }
		}

        public new GroupRenderer SharedRenderer
		{
			get { return new GroupRenderer(tguiWidget_getSharedRenderer(CPointer)); }
		}


	    #region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiGroup_create();

		#endregion
	}
}
