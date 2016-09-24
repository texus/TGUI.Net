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

namespace TGUI
{
	public class RadioButton : ClickableWidget
	{
		public RadioButton(string text = "")
			: base(tguiRadioButton_create())
		{
			if (text.Length > 0)
				Text = text;
		}

		protected internal RadioButton(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public RadioButton(RadioButton copy)
			: base(copy)
		{
		}

		public new RadioButtonRenderer Renderer
		{
			get { return new RadioButtonRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public bool Checked
		{
			get { return tguiRadioButton_isChecked(CPointer); }
			set
			{
				if (value)
					tguiRadioButton_check(CPointer);
				else
					tguiRadioButton_uncheck(CPointer);
			}
		}

		public string Text
		{
			get { return Util.GetStringFromC_UTF32(tguiRadioButton_getText(CPointer)); }
			set { tguiRadioButton_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
		}

		public uint TextSize
		{
			get { return tguiRadioButton_getTextSize(CPointer); }
			set { tguiRadioButton_setTextSize(CPointer, value); }
		}

		public bool TextClickable
		{
			get { return tguiRadioButton_isTextClickable(CPointer); }
			set { tguiRadioButton_setTextClickable(CPointer, value); }
		}

		#region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiRadioButton_create();

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiWidget_getRenderer(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiRadioButton_check(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiRadioButton_uncheck(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern bool tguiRadioButton_isChecked(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiRadioButton_setText(IntPtr cPointer, IntPtr value);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiRadioButton_getText(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiRadioButton_setTextSize(IntPtr cPointer, uint textSize);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern uint tguiRadioButton_getTextSize(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiRadioButton_setTextClickable(IntPtr cPointer, bool clickable);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern bool tguiRadioButton_isTextClickable(IntPtr cPointer);

		#endregion
	}
}
