/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2024 Bruno Van de Velde (vdv_b@tgui.eu)
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
    public static class Cursor
    {
        public enum Type
        {
            /// <summary>Arrow cursor (default)</summary>
            Arrow,

            /// <summary>I-beam, cursor when hovering over a text field</summary>
	        Text,

	        /// <summary>Pointing hand cursor</summary>
	        Hand,

	        /// <summary>Left arrow on Linux, horizontal double arrow cursor on Windows and macOS</summary>
	        SizeLeft,

	        /// <summary>Right arrow on Linux, horizontal double arrow cursor on Windows and macOS</summary>
	        SizeRight,

	        /// <summary>Up arrow on Linux, vertical double arrow cursor on Windows and macOS</summary>
	        SizeTop,

	        /// <summary>Down arrow on Linux, vertical double arrow cursor on Windows and macOS</summary>
	        SizeBottom,

	        /// <summary>Top-left arrow on Linux, double arrow cursor going from top-left to bottom-right on Windows and macOS</summary>
	        SizeTopLeft,

	        /// <summary>Bottom-right arrow on Linux, double arrow cursor going from top-left to bottom-right on Windows and</summary>
	        SizeBottomRight,

	        /// <summary>Bottom-left arrow on Linux, double arrow cursor going from bottom-left to top-right on Windows and macOS</summary>
	        SizeBottomLeft,

	        /// <summary>Top-right arrow on Linux, double arrow cursor going from bottom-left to top-right on Windows and macOS</summary>
	        SizeTopRight,

	        /// <summary>Horizontal double arrow cursor</summary>
	        SizeHorizontal,

	        /// <summary>Vertical double arrow cursor</summary>
	        SizeVertical,

	        /// <summary>Crosshair cursor</summary>
	        Crosshair,

	        /// <summary>Help cursor</summary>
	        Help,

	        /// <summary>Action not allowed cursor</summary>
	        NotAllowed
        }

        public static void setStyle(Type type, byte[] pixels, Vector2u size, Vector2u hotspot)
        {
            unsafe
            {
                fixed (byte* pixelsPtr = pixels)
                {
                    tguiCursor_setStyle(type, pixelsPtr, size, hotspot);
                }
            }
        }

        public static void resetStyle(Type type)
        {
            tguiCursor_resetStyle(type);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void tguiCursor_setStyle(Type type, byte* pixels, Vector2u size, Vector2u hotspot);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiCursor_resetStyle(Type type);

        #endregion
    };
}
