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
using System.Runtime.InteropServices;

namespace TGUI
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Outline : IEquatable<Outline>
	{
		////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="size">Width and height of the outline in all directions</param>
		////////////////////////////////////////////////////////////
		public Outline(float size = 0)
		{
			Left = size;
			Top = size;
			Right = size;
			Bottom = size;
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="width">Width of the left and right outline</param>
		/// <param name="height">Height of the top and bottom outline</param>
		////////////////////////////////////////////////////////////
		public Outline(float width, float height)
		{
			Left = width;
			Top = height;
			Right = width;
			Bottom = height;
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Construct the outline
		/// </summary>
		/// <param name="left">Width of the left outline</param>
		/// <param name="top">Height of the top outline</param>
		/// <param name="right">Width of the right outline</param>
		/// <param name="bottom">Height of the bottom outline</param>
		////////////////////////////////////////////////////////////
		public Outline(float left, float top, float right, float bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Operator == overload ; check outline equality
		/// </summary>
		/// <param name="o1">First outline</param>
		/// <param name="o2">Second outline</param>
		/// <returns>o1 == o2</returns>
		////////////////////////////////////////////////////////////
		public static bool operator ==(Outline o1, Outline o2)
		{
			return o1.Equals(o2);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Operator != overload ; check outline inequality
		/// </summary>
		/// <param name="o1">First outline</param>
		/// <param name="o2">Second outline</param>
		/// <returns>o1 != o2</returns>
		////////////////////////////////////////////////////////////
		public static bool operator !=(Outline o1, Outline o2)
		{
			return !o1.Equals(o2);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Provide a string describing the object
		/// </summary>
		/// <returns>String description of the object</returns>
		////////////////////////////////////////////////////////////
		public override string ToString()
		{
			return "[Outline]" +
			" Left(" + Left + ")" +
			" Top(" + Top + ")" +
			" Right(" + Right + ")" +
			" Bottom(" + Bottom + ")";
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Compare outline and object and checks if they are equal
		/// </summary>
		/// <param name="obj">Object to check</param>
		/// <returns>Object and outline are equal</returns>
		////////////////////////////////////////////////////////////
		public override bool Equals(object obj)
		{
			return (obj is Outline) && Equals((Outline)obj);
		}

		///////////////////////////////////////////////////////////
		/// <summary>
		/// Compare two outlines and checks if they are equal
		/// </summary>
		/// <param name="other">Outline to check</param>
		/// <returns>Outlines are equal</returns>
		////////////////////////////////////////////////////////////
		public bool Equals(Outline other)
		{
			return (Left == other.Left) &&
			(Top == other.Top) &&
			(Right == other.Right) &&
			(Bottom == other.Bottom);
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Provide a integer describing the object
		/// </summary>
		/// <returns>Integer description of the object</returns>
		////////////////////////////////////////////////////////////
		public override int GetHashCode()
		{
			return Left.GetHashCode() ^
			Top.GetHashCode() ^
			Right.GetHashCode() ^
			Bottom.GetHashCode();
		}

		/// <summary>Outline at the left of the object</summary>
		public float Left;

		/// <summary>Outline at the top of the object</summary>
		public float Top;

		/// <summary>Outline at the right of the object</summary>
		public float Right;

		/// <summary>Outline at the bottom of the object</summary>
		public float Bottom;
	}
}