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
	public class EditBox : ClickableWidget
	{
		public const string ValidatorAll = ".*";
		public const string ValidatorInt = "[+-]?[0-9]*";
		public const string ValidatorUInt = "[0-9]*";
		public const string ValidatorFloat = "[+-]?[0-9]*\\.?[0-9]*";

		public EditBox()
			: base(tguiEditBox_create())
		{
		}

		protected internal EditBox(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public EditBox(EditBox copy)
			: base(copy)
		{
		}

		public new EditBoxRenderer Renderer
		{
			get { return new EditBoxRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public string Text
		{
			get { return Util.GetStringFromC_UTF32(tguiEditBox_getText(CPointer)); }
			set { tguiEditBox_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
		}

		public string DefaultText
		{
			get { return Util.GetStringFromC_UTF32(tguiEditBox_getDefaultText(CPointer)); }
			set { tguiEditBox_setDefaultText(CPointer, Util.ConvertStringForC_UTF32(value)); }
		}

		public void SetSelectedText(uint start, uint length = uint.MaxValue)
		{
			tguiEditBox_selectText(CPointer, start, length);
		}

		public string SelectedText
		{
			get { return Util.GetStringFromC_UTF32(tguiEditBox_getSelectedText(CPointer)); }
		}

		public uint TextSize
		{
			get { return tguiEditBox_getTextSize(CPointer); }
			set { tguiEditBox_setTextSize(CPointer, value); }
		}

		public char PasswordCharacter
		{
			get { return tguiEditBox_getPasswordCharacter(CPointer); }
			set { tguiEditBox_setPasswordCharacter(CPointer, value); }
		}

		public uint MaximumCharacters
		{
			get { return tguiEditBox_getMaximumCharacters(CPointer); }
			set { tguiEditBox_setMaximumCharacters(CPointer, value); }
		}

		public HorizontalAlignment Alignment
		{
			get { return tguiEditBox_getAlignment(CPointer); }
			set { tguiEditBox_setAlignment(CPointer, value); }
		}

		public bool LimitTextWidth
		{
			get { return tguiEditBox_isTextWidthLimited(CPointer); }
			set { tguiEditBox_limitTextWidth(CPointer, value); }
		}

		public uint CaretPosition
		{
			get { return tguiEditBox_getCaretPosition(CPointer); }
			set { tguiEditBox_setCaretPosition(CPointer, value); }
		}

		public string InputValidator
		{
			get { return Util.GetStringFromC_ASCII(tguiEditBox_getInputValidator(CPointer)); }
			set { tguiEditBox_setInputValidator(CPointer, Util.ConvertStringForC_ASCII(value)); }
		}

		#region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiEditBox_create();

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiWidget_getRenderer(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_setText(IntPtr cPointer, IntPtr value);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiEditBox_getText(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_setDefaultText(IntPtr cPointer, IntPtr value);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiEditBox_getDefaultText(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_selectText(IntPtr cPointer, uint start, uint length);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiEditBox_getSelectedText(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_setTextSize(IntPtr cPointer, uint textSize);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern uint tguiEditBox_getTextSize(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_setPasswordCharacter(IntPtr cPointer, char passwordChar);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern char tguiEditBox_getPasswordCharacter(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_setMaximumCharacters(IntPtr cPointer, uint maximumCharacters);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern uint tguiEditBox_getMaximumCharacters(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_setAlignment(IntPtr cPointer, HorizontalAlignment alignment);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern HorizontalAlignment tguiEditBox_getAlignment(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_limitTextWidth(IntPtr cPointer, bool limitWidth);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern bool tguiEditBox_isTextWidthLimited(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_setCaretPosition(IntPtr cPointer, uint caretPosition);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern uint tguiEditBox_getCaretPosition(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiEditBox_setInputValidator(IntPtr cPointer, IntPtr validator);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiEditBox_getInputValidator(IntPtr cPointer);

		#endregion
	}
}
