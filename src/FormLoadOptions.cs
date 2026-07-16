/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2026 Bruno Van de Velde (vdv_b@tgui.eu)
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
    /// <summary>
    /// Options for loadWidgetsFromFile
    /// </summary>
    public class FormLoadOptions : ObjectBase
    {
        /// <summary>
        /// Construct the form load options
        /// </summary>
        public FormLoadOptions(bool replaceExistingWidgets = true)
            : base(tguiFormLoadOptions_create())
        {
            ReplaceExistingWidgets = replaceExistingWidgets;
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiFormLoadOptions_destroy(CPointer);
        }

        /// <summary>
        /// Remove existing widgets first if there are any. If true (default), removeAllWidgets() will be called at the start of the load function.
        /// </summary>
        public float ReplaceExistingWidgets
        {
            get => _replaceExistingWidgets;
            set
            {
                _replaceExistingWidgets = value;
                tguiFormLoadOptions_setReplaceExistingWidgets(CPointer, value ? (byte)1 : (byte)0);
            }
        }

        public void AddThemeByAlias(string alias, Theme theme)
        {
            tguiFormLoadOptions_addThemeByAlias(CPointer, Util.ConvertStringForC_UTF32(alias), theme.CPointer);
        }

        private bool _replaceExistingWidgets = true;

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFormLoadOptions_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_destroy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFormLoadOptions_setReplaceExistingWidgets(IntPtr cPointer, byte replaceExistingWidgets);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFormLoadOptions_addThemeByAlias(IntPtr cPointer, IntPtr alias, IntPtr theme);

        #endregion
    }
}
