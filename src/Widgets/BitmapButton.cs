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
using System.Security;
using System.Runtime.InteropServices;
using SFML.Graphics;

namespace TGUI
{
    /// <summary>
    /// Bitmap button widget
    /// </summary>
    public class BitmapButton : Button
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="text">Caption of the button</param>
        public BitmapButton(string text = "")
            : base(tguiBitmapButton_create())
        {
            if (text.Length > 0)
                Text = text;
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal BitmapButton(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public BitmapButton(BitmapButton copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets or sets the image that should be displayed next to the text
        /// </summary>
        public Texture Image
        {
            set { tguiBitmapButton_setImage(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the relative size of the image to display next to the text
        /// </summary>
        /// <remarks>
        /// The value lies between 0 and 1 and determines the height of the image compared to the button height.
        /// If set to 0 (default), the image will always have its original size, no matter what the button size is.
        /// </remarks>
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
