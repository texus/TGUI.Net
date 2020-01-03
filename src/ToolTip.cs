/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2020 Bruno Van de Velde (vdv_b@tgui.eu)
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
using SFML.System;

namespace TGUI
{
    /// <summary>
    /// This class provides global settings for tool tips
    /// </summary>
    public static class ToolTip
    {
        /// <summary>
        /// Gets or sets the time that the mouse has to stand still before the tooltip becomes visible
        /// </summary>
        public static Time InitialDelay
        {
            get { return tguiToolTip_getInitialDelay(); }
            set { tguiToolTip_setInitialDelay(value); }
        }

        /// <summary>
        /// Gets or sets the default distance between the mouse position and the tool tip
        /// </summary>
        public static Vector2f DistanceToMouse
        {
            get { return tguiToolTip_getDistanceToMouse(); }
            set { tguiToolTip_setDistanceToMouse(value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void tguiToolTip_setInitialDelay(Time time);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Time tguiToolTip_getInitialDelay();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void tguiToolTip_setDistanceToMouse(Vector2f distance);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2f tguiToolTip_getDistanceToMouse();

        #endregion
    }
}
