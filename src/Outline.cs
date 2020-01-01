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

namespace TGUI
{
    /// <summary>
    /// Outline is used to define padding and borders
    /// </summary>
    public class Outline : SFML.ObjectBase
    {
        /// <summary>
        /// Construct the outline
        /// </summary>
        /// <param name="size">Width and height of the outline in all directions</param>
        public Outline(float size = 0)
            : base(tguiOutline_create(size, size, size, size))
        {
        }

        /// <summary>
        /// Construct the outline
        /// </summary>
        /// <param name="size">Width and height of the outline in all directions</param>
        public Outline(string size)
            : base(tguiOutline_createFromStrings(Util.ConvertStringForC_ASCII(size),
                                                 Util.ConvertStringForC_ASCII(size),
                                                 Util.ConvertStringForC_ASCII(size),
                                                 Util.ConvertStringForC_ASCII(size)))
        {
        }

        /// <summary>
        /// Construct the outline
        /// </summary>
        /// <param name="width">Width of the left and right outline</param>
        /// <param name="height">Height of the top and bottom outline</param>
        public Outline(float width, float height)
            : base(tguiOutline_create(width, height, width, height))
        {
        }

        /// <summary>
        /// Construct the outline
        /// </summary>
        /// <param name="width">Width of the left and right outline</param>
        /// <param name="height">Height of the top and bottom outline</param>
        public Outline(string width, string height)
            : base(tguiOutline_createFromStrings(Util.ConvertStringForC_ASCII(width),
                                                 Util.ConvertStringForC_ASCII(height),
                                                 Util.ConvertStringForC_ASCII(width),
                                                 Util.ConvertStringForC_ASCII(height)))
        {
        }

        /// <summary>
        /// Construct the outline
        /// </summary>
        /// <param name="left">Width of the left outline</param>
        /// <param name="top">Height of the top outline</param>
        /// <param name="right">Width of the right outline</param>
        /// <param name="bottom">Height of the bottom outline</param>
        public Outline(float left, float top, float right, float bottom)
            : base(tguiOutline_create(left, top, right, bottom))
        {
        }

        /// <summary>
        /// Construct the outline
        /// </summary>
        /// <param name="left">Width of the left outline</param>
        /// <param name="top">Height of the top outline</param>
        /// <param name="right">Width of the right outline</param>
        /// <param name="bottom">Height of the bottom outline</param>
        public Outline(string left, string top, string right, string bottom)
            : base(tguiOutline_createFromStrings(Util.ConvertStringForC_ASCII(left),
                                                 Util.ConvertStringForC_ASCII(top),
                                                 Util.ConvertStringForC_ASCII(right),
                                                 Util.ConvertStringForC_ASCII(bottom)))
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Outline(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Outline(Outline copy)
            : base(tguiOutline_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiOutline_destroy(CPointer);
        }

        /// <summary>
        /// Gets the width of the left outline
        /// </summary>
        public float Left
        {
            get { return tguiOutline_getLeft(CPointer); }
        }

        /// <summary>
        /// Gets the height of the top outline
        /// </summary>
        public float Top
        {
            get { return tguiOutline_getTop(CPointer); }
        }

        /// <summary>
        /// Gets the width of the right outline
        /// </summary>
        public float Right
        {
            get { return tguiOutline_getRight(CPointer); }
        }

        /// <summary>
        /// Gets the height of the bottom outline
        /// </summary>
        public float Bottom
        {
            get { return tguiOutline_getBottom(CPointer); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiOutline_create(float left, float top, float right, float bottom);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiOutline_createFromStrings(IntPtr left, IntPtr top, IntPtr right, IntPtr bottom);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiOutline_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiOutline_destroy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiOutline_getLeft(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiOutline_getTop(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiOutline_getRight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiOutline_getBottom(IntPtr cPointer);

        #endregion
    }
}
