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


		protected override void InitSignals()
		{
			base.InitSignals();
			IntPtr error;

		    TextChangedCallback = new CallbackActionString(ProcessTextChangedSignal);
		    tguiWidget_connect_string(CPointer, Util.ConvertStringForC_ASCII("TextChanged"), TextChangedCallback, out error);
		    if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));

		    ReturnKeyPressedCallback = new CallbackActionString(ProcessReturnKeyPressedSignal);
		    tguiWidget_connect_string(CPointer, Util.ConvertStringForC_ASCII("ReturnKeyPressed"), ReturnKeyPressedCallback, out error);
		    if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		private void ProcessTextChangedSignal(IntPtr text)
		{
			if (TextChanged != null)
				TextChanged(this, new SignalArgsString(Util.GetStringFromC_UTF32(text)));
		}

		private void ProcessReturnKeyPressedSignal(IntPtr text)
		{
			if (ReturnKeyPressed != null)
				ReturnKeyPressed(this, new SignalArgsString(Util.GetStringFromC_UTF32(text)));
		}

		/// <summary>Event handler for the TextChanged signal</summary>
		public event EventHandler<SignalArgsString> TextChanged = null;

		/// <summary>Event handler for the ReturnKeyPressed signal</summary>
		public event EventHandler<SignalArgsString> ReturnKeyPressed = null;

	    private CallbackActionString TextChangedCallback;
	    private CallbackActionString ReturnKeyPressedCallback;


	    #region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBox_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_setText(IntPtr cPointer, IntPtr value);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBox_getText(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_setDefaultText(IntPtr cPointer, IntPtr value);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBox_getDefaultText(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_selectText(IntPtr cPointer, uint start, uint length);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBox_getSelectedText(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_setTextSize(IntPtr cPointer, uint textSize);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiEditBox_getTextSize(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_setPasswordCharacter(IntPtr cPointer, char passwordChar);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected char tguiEditBox_getPasswordCharacter(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_setMaximumCharacters(IntPtr cPointer, uint maximumCharacters);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiEditBox_getMaximumCharacters(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_setAlignment(IntPtr cPointer, HorizontalAlignment alignment);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected HorizontalAlignment tguiEditBox_getAlignment(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_limitTextWidth(IntPtr cPointer, bool limitWidth);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiEditBox_isTextWidthLimited(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_setCaretPosition(IntPtr cPointer, uint caretPosition);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiEditBox_getCaretPosition(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiEditBox_setInputValidator(IntPtr cPointer, IntPtr validator);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiEditBox_getInputValidator(IntPtr cPointer);

		#endregion
	}
}
