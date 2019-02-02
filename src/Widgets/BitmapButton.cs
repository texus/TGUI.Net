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
    public class BitmapButton : Button
    {
        public BitmapButton()
            : base(tguiBitmapButton_create())
        {
        }

        protected internal BitmapButton(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public BitmapButton(BitmapButton copy)
            : base(copy)
        {
        }

        public Texture Image
        {
            set { tguiBitmapButton_setImage(CPointer, value.CPointer); }
        }

        public float ImageScaling
        {
            get { return tguiBitmapButton_getImageScaling(CPointer); }
            set { tguiBitmapButton_setImageScaling(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiBitmapButton_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiBitmapButton_setImage(IntPtr cPointer, IntPtr image);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiBitmapButton_setImageScaling(IntPtr cPointer, float imageScaling);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiBitmapButton_getImageScaling(IntPtr cPointer);

        #endregion
    }
}
