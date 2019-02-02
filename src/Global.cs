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
    public static class Global
    {
        /// <summary>Name of the CTGUI library to import</summary>
#if _WINDOWS_
        public const string CTGUI = "ctgui-0.8.dll";
#elif _OSX_
        public const string CTGUI = "libctgui.dylib";
#elif _LINUX_
        public const string CTGUI = "libctgui.so";
#endif

        /// <summary>
        /// Gets or sets the default font for all new widgets
        /// </summary>
        public static Font Font
        {
            set { tgui_setGlobalFont(value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the default text size for all new widgets
        /// </summary>
        public static uint TextSize
        {
            get { return tgui_getGlobalTextSize(); }
            set { tgui_setGlobalTextSize(value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tgui_setGlobalFont(IntPtr font);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tgui_setGlobalTextSize(uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tgui_getGlobalTextSize();

        #endregion
    }
}
