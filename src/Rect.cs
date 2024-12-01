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
    [StructLayout(LayoutKind.Sequential)]
    public struct IntRect : IEquatable<IntRect>
    {
        /// <summary>
        /// Constructs the rectangle from its position and size
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public IntRect(int left, int top, int width, int height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Constructs the rectangle from its position and size
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle</param>
        /// <param name="size">Size of the rectangle</param>
        public IntRect(Vector2i position, Vector2i size)
            : this(position.X, position.Y, size.X, size.Y)
        {
        }

        /// <summary>
        /// Position of the rectangle's top-left corner
        /// </summary>
        public Vector2i Position
        {
            get => new Vector2i(Left, Top);
            set { Left = value.X; Top = value.Y; }
        }

        /// <summary>
        /// Size of the rectangle
        /// </summary>
        public Vector2i Size
        {
            get => new Vector2i(Width, Height);
            set { Width = value.X; Height = value.Y; }
        }

        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="x">X coordinate of the point to test</param>
        /// <param name="y">Y coordinate of the point to test</param>
        /// <returns>True if the point is inside</returns>
        public bool Contains(int x, int y)
        {
            var minX = Math.Min(Left, Left + Width);
            var maxX = Math.Max(Left, Left + Width);
            var minY = Math.Min(Top, Top + Height);
            var maxY = Math.Max(Top, Top + Height);

            return (x >= minX) && (x < maxX) && (y >= minY) && (y < maxY);
        }

        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Vector2 position of the point to test</param>
        /// <returns>True if the point is inside</returns>
        public bool Contains(Vector2i point) => Contains(point.X, point.Y);

        /// <summary>
        /// Check intersection between two rectangles
        /// </summary>
        /// <param name="rect"> Rectangle to test</param>
        /// <returns>True if rectangles overlap</returns>
        public bool Intersects(IntRect rect)
        {
            // Compute the intersection boundaries
            var interLeft = Math.Max(Left, rect.Left);
            var interTop = Math.Max(Top, rect.Top);
            var interRight = Math.Min(Left + Width, rect.Left + rect.Width);
            var interBottom = Math.Min(Top + Height, rect.Top + rect.Height);

            // If the intersection is valid (positive non zero area), then there is an intersection
            return (interLeft < interRight) && (interTop < interBottom);
        }

        /// <summary>
        /// Deconstructs an IntRect into a tuple of ints
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public void Deconstruct(out int left, out int top, out int width, out int height)
        {
            left = Left;
            top = Top;
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString() => $"[IntRect] Left({Left}) Top({Top}) Width({Width}) Height({Height})";

        /// <summary>
        /// Compare rectangle and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and rectangle are equal</returns>
        public override bool Equals(object? obj) => (obj is IntRect) && Equals((IntRect)obj);

        /// <summary>
        /// Compare two rectangles and checks if they are equal
        /// </summary>
        /// <param name="other">Rectangle to check</param>
        /// <returns>Rectangles are equal</returns>
        public bool Equals(IntRect other) => (Left == other.Left) && (Top == other.Top) && (Width == other.Width) && (Height == other.Height);

        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode() => unchecked((int)((uint)Left
                                                           ^ (((uint)Top << 13) | ((uint)Top >> 19))
                                                           ^ (((uint)Width << 26) | ((uint)Width >> 6))
                                                           ^ (((uint)Height << 7) | ((uint)Height >> 25))));

        /// <summary>
        /// Operator == overload ; check rect equality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 == r2</returns>
        public static bool operator ==(IntRect r1, IntRect r2) => r1.Equals(r2);

        /// <summary>
        /// Operator != overload ; check rect inequality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 != r2</returns>
        public static bool operator !=(IntRect r1, IntRect r2) => !r1.Equals(r2);

        /// <summary>
        /// Converts a tuple of ints to an IntRect
        /// </summary>
        /// <param name="tuple">The tuple to convert</param>
        public static implicit operator IntRect((int Left, int Top, int Width, int Height) tuple) => new IntRect(tuple.Left, tuple.Top, tuple.Width, tuple.Height);

        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        public static explicit operator UIntRect(IntRect r) => new UIntRect((uint)r.Left, (uint)r.Top, (uint)r.Width, (uint)r.Height);

        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        public static explicit operator FloatRect(IntRect r) => new FloatRect((float)r.Left, (float)r.Top, (float)r.Width, (float)r.Height);

        /// <summary>Left coordinate of the rectangle</summary>
        public int Left;

        /// <summary>Top coordinate of the rectangle</summary>
        public int Top;

        /// <summary>Width of the rectangle</summary>
        public int Width;

        /// <summary>Height of the rectangle</summary>
        public int Height;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct UIntRect : IEquatable<UIntRect>
    {
        /// <summary>
        /// Constructs the rectangle from its position and size
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public UIntRect(uint left, uint top, uint width, uint height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Constructs the rectangle from its position and size
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle</param>
        /// <param name="size">Size of the rectangle</param>
        public UIntRect(Vector2u position, Vector2u size)
            : this(position.X, position.Y, size.X, size.Y)
        {
        }

        /// <summary>
        /// Position of the rectangle's top-left corner
        /// </summary>
        public Vector2u Position
        {
            get => new Vector2u(Left, Top);
            set { Left = value.X; Top = value.Y; }
        }

        /// <summary>
        /// Size of the rectangle
        /// </summary>
        public Vector2u Size
        {
            get => new Vector2u(Width, Height);
            set { Width = value.X; Height = value.Y; }
        }

        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="x">X coordinate of the point to test</param>
        /// <param name="y">Y coordinate of the point to test</param>
        /// <returns>True if the point is inside</returns>
        public bool Contains(uint x, uint y)
        {
            var minX = Math.Min(Left, Left + Width);
            var maxX = Math.Max(Left, Left + Width);
            var minY = Math.Min(Top, Top + Height);
            var maxY = Math.Max(Top, Top + Height);

            return (x >= minX) && (x < maxX) && (y >= minY) && (y < maxY);
        }

        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Vector2 position of the point to test</param>
        /// <returns>True if the point is inside</returns>
        public bool Contains(Vector2u point) => Contains(point.X, point.Y);

        /// <summary>
        /// Check intersection between two rectangles
        /// </summary>
        /// <param name="rect"> Rectangle to test</param>
        /// <returns>True if rectangles overlap</returns>
        public bool Intersects(UIntRect rect)
        {
            // Compute the intersection boundaries
            var interLeft = Math.Max(Left, rect.Left);
            var interTop = Math.Max(Top, rect.Top);
            var interRight = Math.Min(Left + Width, rect.Left + rect.Width);
            var interBottom = Math.Min(Top + Height, rect.Top + rect.Height);

            // If the intersection is valid (positive non zero area), then there is an intersection
            return (interLeft < interRight) && (interTop < interBottom);
        }

        /// <summary>
        /// Deconstructs an UIntRect into a tuple of uints
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public void Deconstruct(out uint left, out uint top, out uint width, out uint height)
        {
            left = Left;
            top = Top;
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString() => $"[UIntRect] Left({Left}) Top({Top}) Width({Width}) Height({Height})";

        /// <summary>
        /// Compare rectangle and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and rectangle are equal</returns>
        public override bool Equals(object? obj) => (obj is UIntRect) && Equals((UIntRect)obj);

        /// <summary>
        /// Compare two rectangles and checks if they are equal
        /// </summary>
        /// <param name="other">Rectangle to check</param>
        /// <returns>Rectangles are equal</returns>
        public bool Equals(UIntRect other) => (Left == other.Left) && (Top == other.Top) && (Width == other.Width) && (Height == other.Height);

        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode() => unchecked((int)(Left
                                                           ^ ((Top << 13) | (Top >> 19))
                                                           ^ ((Width << 26) | (Width >> 6))
                                                           ^ ((Height << 7) | (Height >> 25))));

        /// <summary>
        /// Operator == overload ; check rect equality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 == r2</returns>
        public static bool operator ==(UIntRect r1, UIntRect r2) => r1.Equals(r2);

        /// <summary>
        /// Operator != overload ; check rect inequality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 != r2</returns>
        public static bool operator !=(UIntRect r1, UIntRect r2) => !r1.Equals(r2);

        /// <summary>
        /// Converts a tuple of uints to an UIntRect
        /// </summary>
        /// <param name="tuple">The tuple to convert</param>
        public static implicit operator UIntRect((uint Left, uint Top, uint Width, uint Height) tuple) => new UIntRect(tuple.Left, tuple.Top, tuple.Width, tuple.Height);

        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        public static explicit operator IntRect(UIntRect r) => new IntRect((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);

        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        public static explicit operator FloatRect(UIntRect r) => new FloatRect((float)r.Left, (float)r.Top, (float)r.Width, (float)r.Height);

        /// <summary>Left coordinate of the rectangle</summary>
        public uint Left;

        /// <summary>Top coordinate of the rectangle</summary>
        public uint Top;

        /// <summary>Width of the rectangle</summary>
        public uint Width;

        /// <summary>Height of the rectangle</summary>
        public uint Height;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct FloatRect : IEquatable<FloatRect>
    {
        /// <summary>
        /// Constructs the rectangle from its position and size
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public FloatRect(float left, float top, float width, float height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Constructs the rectangle from its position and size
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle</param>
        /// <param name="size">Size of the rectangle</param>
        public FloatRect(Vector2f position, Vector2f size)
            : this(position.X, position.Y, size.X, size.Y)
        {
        }

        /// <summary>
        /// Position of the rectangle's top-left corner
        /// </summary>
        public Vector2f Position
        {
            get => new Vector2f(Left, Top);
            set { Left = value.X; Top = value.Y; }
        }

        /// <summary>
        /// Size of the rectangle
        /// </summary>
        public Vector2f Size
        {
            get => new Vector2f(Width, Height);
            set { Width = value.X; Height = value.Y; }
        }

        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="x">X coordinate of the point to test</param>
        /// <param name="y">Y coordinate of the point to test</param>
        /// <returns>True if the point is inside</returns>
        public bool Contains(float x, float y)
        {
            var minX = Math.Min(Left, Left + Width);
            var maxX = Math.Max(Left, Left + Width);
            var minY = Math.Min(Top, Top + Height);
            var maxY = Math.Max(Top, Top + Height);

            return (x >= minX) && (x < maxX) && (y >= minY) && (y < maxY);
        }

        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Vector2 position of the point to test</param>
        /// <returns>True if the point is inside</returns>
        public bool Contains(Vector2f point) => Contains(point.X, point.Y);

        /// <summary>
        /// Check intersection between two rectangles
        /// </summary>
        /// <param name="rect"> Rectangle to test</param>
        /// <returns>True if rectangles overlap</returns>
        public bool Intersects(FloatRect rect)
        {
            // Compute the intersection boundaries
            var interLeft = Math.Max(Left, rect.Left);
            var interTop = Math.Max(Top, rect.Top);
            var interRight = Math.Min(Left + Width, rect.Left + rect.Width);
            var interBottom = Math.Min(Top + Height, rect.Top + rect.Height);

            // If the intersection is valid (positive non zero area), then there is an intersection
            return (interLeft < interRight) && (interTop < interBottom);
        }

        /// <summary>
        /// Deconstructs a FloatRect into a tuple of floats
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public void Deconstruct(out float left, out float top, out float width, out float height)
        {
            left = Left;
            top = Top;
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString() => $"[FloatRect] Left({Left}) Top({Top}) Width({Width}) Height({Height})";

        /// <summary>
        /// Compare rectangle and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and rectangle are equal</returns>
        public override bool Equals(object? obj) => (obj is FloatRect) && Equals((FloatRect)obj);

        /// <summary>
        /// Compare two rectangles and checks if they are equal
        /// </summary>
        /// <param name="other">Rectangle to check</param>
        /// <returns>Rectangles are equal</returns>
        public bool Equals(FloatRect other) => (Left == other.Left) && (Top == other.Top) && (Width == other.Width) && (Height == other.Height);

        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode() => unchecked((int)((uint)Left
                                                           ^ (((uint)Top << 13) | ((uint)Top >> 19))
                                                           ^ (((uint)Width << 26) | ((uint)Width >> 6))
                                                           ^ (((uint)Height << 7) | ((uint)Height >> 25))));

        /// <summary>
        /// Operator == overload ; check rect equality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 == r2</returns>
        public static bool operator ==(FloatRect r1, FloatRect r2) => r1.Equals(r2);

        /// <summary>
        /// Operator != overload ; check rect inequality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 != r2</returns>
        public static bool operator !=(FloatRect r1, FloatRect r2) => !r1.Equals(r2);

        /// <summary>
        /// Converts a tuple of floats to a FloatRect
        /// </summary>
        /// <param name="tuple">The tuple to convert</param>
        public static implicit operator FloatRect((float Left, float Top, float Width, float Height) tuple) => new FloatRect(tuple.Left, tuple.Top, tuple.Width, tuple.Height);

        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        public static explicit operator IntRect(FloatRect r) => new IntRect((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);

        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        public static explicit operator UIntRect(FloatRect r) => new UIntRect((uint)r.Left, (uint)r.Top, (uint)r.Width, (uint)r.Height);

        /// <summary>Left coordinate of the rectangle</summary>
        public float Left;

        /// <summary>Top coordinate of the rectangle</summary>
        public float Top;

        /// <summary>Width of the rectangle</summary>
        public float Width;

        /// <summary>Height of the rectangle</summary>
        public float Height;
    }
}
