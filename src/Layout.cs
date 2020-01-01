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
    /// <summary>
    /// Layout stores either a value or a string expression that is used to represent a Left, Top, Width or Height property
    /// </summary>
    public class Layout : SFML.ObjectBase
    {
        /// <summary>
        /// Construct the layout with a constant value
        /// </summary>
        /// <param name="constant">Constant value to store in the layout</param>
        public Layout(float constant)
            : base(tguiLayout_create(constant))
        {
        }

        /// <summary>
        /// Construct the layout with a string expression which will be parsed to determine the value of the layout
        /// </summary>
        /// <param name="expression">String to parse</param>
        public Layout(string expression)
            : base(tguiLayout_createFromString(Util.ConvertStringForC_ASCII(expression)))
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Layout(Layout copy)
            : base(tguiLayout_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiLayout_destroy(CPointer);
        }

        /// <summary>
        /// Gets the current value of the layout
        /// </summary>
        public float Value
        {
            get { return tguiLayout_getValue(CPointer); }
        }

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLayout_create(float constant);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLayout_createFromString(IntPtr expression);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLayout_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLayout_destroy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiLayout_getValue(IntPtr cPointer);

        #endregion
    }
}
