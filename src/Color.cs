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
    public struct Color
    {
        /// <summary>
        /// Construct a color from a string
        /// </summary>
        /// <param name="str">String to be deserialized as color</param>
        /// <returns>Deserialized color object, or null if deserializing failed or the string specified an empty color (e.g. "none")</returns>
        public static Color? FromString(string str)
        {
            return Util.GetColorFromC(tguiColor_fromString(Util.ConvertStringForC_ASCII(str)));
        }

        /// <summary>
        /// Construct the color from its red, green, blue and alpha components
        /// </summary>
        /// <param name="red">Red component</param>
        /// <param name="green">Green component</param>
        /// <param name="blue">Blue component</param>
        /// <param name="alpha">Alpha (transparency) component</param>
        public Color(byte red, byte green, byte blue, byte alpha = 255)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        /// <summary>
        /// Construct the color from another
        /// </summary>
        /// <param name="color">Color to copy</param>
        public Color(Color color) : this(color.R, color.G, color.B, color.A) { }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString() => $"[Color] R({R}) G({G}) B({B}) A({A})";

        /// <summary>Red component of the color</summary>
        public byte R;

        /// <summary>Green component of the color</summary>
        public byte G;

        /// <summary>Blue component of the color</summary>
        public byte B;

        /// <summary>Alpha (transparent) component of the color</summary>
        public byte A;

        /// <summary>Predefined black color</summary>
        public static readonly Color Black = new Color(0, 0, 0);

        /// <summary>Predefined white color</summary>
        public static readonly Color White = new Color(255, 255, 255);

        /// <summary>Predefined red color</summary>
        public static readonly Color Red = new Color(255, 0, 0);

        /// <summary>Predefined green color</summary>
        public static readonly Color Green = new Color(0, 255, 0);

        /// <summary>Predefined blue color</summary>
        public static readonly Color Blue = new Color(0, 0, 255);

        /// <summary>Predefined yellow color</summary>
        public static readonly Color Yellow = new Color(255, 255, 0);

        /// <summary>Predefined magenta color</summary>
        public static readonly Color Magenta = new Color(255, 0, 255);

        /// <summary>Predefined cyan color</summary>
        public static readonly Color Cyan = new Color(0, 255, 255);

        /// <summary>Predefined (black) transparent color</summary>
        public static readonly Color Transparent = new Color(0, 0, 0, 0);

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiColor_fromString(IntPtr str);

        #endregion
    };

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ColorCTGUI
    {
        public ColorCTGUI(Color? color)
        {
            if (!(color is null))
            {
                R = color.Value.R;
                G = color.Value.G;
                B = color.Value.B;
                A = color.Value.A;
                IsSet = (byte)1;
            }
            else
            {
                R = 0;
                G = 0;
                B = 0;
                A = 0;
                IsSet = (byte)0;
            }
        }

        /// <summary>Red component of the color</summary>
        public readonly byte R;

        /// <summary>Green component of the color</summary>
        public readonly byte G;

        /// <summary>Blue component of the color</summary>
        public readonly byte B;

        /// <summary>Alpha (transparent) component of the color</summary>
        public readonly byte A;

        /// <summary>Determines if the RGBA value is valid or whether this color is the equivalent to a null object</summary>
        public readonly byte IsSet;
    }
}
