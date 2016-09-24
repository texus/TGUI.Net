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
using System.Runtime.InteropServices;

namespace TGUI
{
	////////////////////////////////////////////////////////////
	/// <summary>
	/// The ObjectBase class is an abstract base for every
	/// TGUI object. It's meant for internal use only
	/// </summary>
	////////////////////////////////////////////////////////////
	public abstract class ObjectBase : IDisposable
	{
		////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the object from a pointer to the C library object
		/// </summary>
		/// <param name="cPointer">Internal pointer to the object in the C libraries</param>
		////////////////////////////////////////////////////////////
		public ObjectBase(IntPtr cPointer)
		{
			myCPointer = cPointer;
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Dispose the object
		/// </summary>
		////////////////////////////////////////////////////////////
		~ObjectBase()
		{
			Dispose(false);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Access to the internal pointer of the object.
		/// For internal use only
		/// </summary>
		////////////////////////////////////////////////////////////
		public IntPtr CPointer
		{
			get { return myCPointer; }
			protected set { myCPointer = value; }
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Explicitly dispose the object
		/// </summary>
		////////////////////////////////////////////////////////////
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Destroy the object
		/// </summary>
		/// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
		////////////////////////////////////////////////////////////
		private void Dispose(bool disposing)
		{
			if (myCPointer != IntPtr.Zero)
			{
				Destroy(disposing);
				myCPointer = IntPtr.Zero;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Destroy the object (implementation is left to each derived class)
		/// </summary>
		/// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
		////////////////////////////////////////////////////////////
		protected abstract void Destroy(bool disposing);

		private IntPtr myCPointer = IntPtr.Zero;
	}
}
