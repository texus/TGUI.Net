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
    public static class Global
    {
        /// <summary>
        /// Gets or sets the default text size for all new widgets
        /// </summary>
        public static uint TextSize
        {
            get { return tgui_getGlobalTextSize(); }
            set { tgui_setGlobalTextSize(value); }
        }

        /// <summary>
        /// Gets or sets the double-click time for the mouse
        /// </summary>
        public static Duration DoubleClickTime
        {
            get { return tgui_getDoubleClickTime(); }
            set { tgui_setDoubleClickTime(value); }
        }

        /// <summary>
        /// Gets or sets the resource path
        /// </summary>
        /// <remarks>This pathname is placed in front of every filename that is used to load a resource.</remarks>
        public static string ResourcePath
        {
            get { return Util.GetStringFromC_UTF32(tgui_getResourcePath()); }
            set { tgui_setResourcePath(Util.ConvertStringForC_UTF32(value)); }
        }

        /// <summary>
        /// Gets or sets the blink rate of the cursor in edit fields such as EditBox and TextArea
        /// </summary>
        public static Duration EditCursorBlinkRate
        {
            get { return tgui_getEditCursorBlinkRate(); }
            set { tgui_setEditCursorBlinkRate(value); }
        }


        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tgui_setGlobalTextSize(uint textSize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tgui_getGlobalTextSize();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tgui_setEditCursorBlinkRate(Duration blinkRate);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Duration tgui_getEditCursorBlinkRate();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tgui_setDoubleClickTime(Duration duration);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Duration tgui_getDoubleClickTime();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tgui_setResourcePath(IntPtr path);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tgui_getResourcePath();

        #endregion
    }
}
