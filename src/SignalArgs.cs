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
using SFML.System;

namespace TGUI
{
	public class SignalArgsVector2f : EventArgs
	{
		public SignalArgsVector2f(Vector2f value)
		{
			Value = value;
		}

		public Vector2f Value;
	}

	public class SignalArgsString : EventArgs
	{
		public SignalArgsString(string value)
		{
			Value = value;
		}

		public string Value;
	}

	public class SignalArgsInt : EventArgs
	{
		public SignalArgsInt(int value)
		{
			Value = value;
		}

		public int Value;
	}

    public class SignalArgsUInt : EventArgs
	{
		public SignalArgsUInt(uint value)
		{
			Value = value;
		}

		public uint Value;
	}

	public class SignalArgsFloat : EventArgs
	{
		public SignalArgsFloat(float value)
		{
			Value = value;
		}

		public float Value;
	}

    public class SignalArgsRange : EventArgs
	{
		public SignalArgsRange(float start, float end)
		{
			Start = start;
			End = end;
		}

		public float Start;
		public float End;
	}

	public class SignalArgsBool : EventArgs
	{
		public SignalArgsBool(bool value)
		{
			Value = value;
		}

		public bool Value;
	}

	public class SignalArgsItem : EventArgs
	{
		public SignalArgsItem(string item, string id)
		{
			Item = item;
			Id = id;
		}

		public string Item;
		public string Id;
	}
}
