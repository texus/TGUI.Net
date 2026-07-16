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
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2f
    {
        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Construct the vector from a Vector2i
        /// </summary>
        /// <param name="vec">Vector to copy</param>
        public Vector2f(Vector2i vec) : this((float)vec.X, (float)vec.Y)
        {
        }

        /// <summary>
        /// Construct the vector from a Vector2u
        /// </summary>
        /// <param name="vec">Vector to copy</param>
        public Vector2f(Vector2u vec) : this((float)vec.X, (float)vec.Y)
        {
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString() => $"[Vector2f] X({X}) Y({Y})";

        /// <summary>X (horizontal) component of the vector</summary>
        public float X;

        /// <summary>Y (vertical) component of the vector</summary>
        public float Y;
    };

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2i
    {
        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Construct the vector from a Vector2f
        /// </summary>
        /// <param name="vec">Vector to copy</param>
        public Vector2i(Vector2f vec) : this((int)vec.X, (int)vec.Y)
        {
        }

        /// <summary>
        /// Construct the vector from a Vector2u
        /// </summary>
        /// <param name="vec">Vector to copy</param>
        public Vector2i(Vector2u vec) : this((int)vec.X, (int)vec.Y)
        {
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString() => $"[Vector2i] X({X}) Y({Y})";

        /// <summary>X (horizontal) component of the vector</summary>
        public int X;

        /// <summary>Y (vertical) component of the vector</summary>
        public int Y;
    };

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2u
    {
        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Vector2u(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Construct the vector from a Vector2f
        /// </summary>
        /// <param name="vec">Vector to copy</param>
        public Vector2u(Vector2f vec) : this((uint)vec.X, (uint)vec.Y)
        {
        }

        /// <summary>
        /// Construct the vector from a Vector2i
        /// </summary>
        /// <param name="vec">Vector to copy</param>
        public Vector2u(Vector2i vec) : this((uint)vec.X, (uint)vec.Y)
        {
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString() => $"[Vector2u] X({X}) Y({Y})";

        /// <summary>X (horizontal) component of the vector</summary>
        public uint X;

        /// <summary>Y (vertical) component of the vector</summary>
        public uint Y;
    };
}
