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
	public class TextBox : Widget
	{
		public const string ValidatorAll = ".*";
		public const string ValidatorInt = "[+-]?[0-9]*";
		public const string ValidatorUInt = "[0-9]*";
		public const string ValidatorFloat = "[+-]?[0-9]*\\.?[0-9]*";

		public TextBox()
			: base(tguiTextBox_create())
		{
		}

		protected internal TextBox(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public TextBox(TextBox copy)
			: base(copy)
		{
		}

		public new TextBoxRenderer Renderer
		{
			get { return new TextBoxRenderer(tguiWidget_getRenderer(CPointer)); }
		}

        public new TextBoxRenderer SharedRenderer
		{
			get { return new TextBoxRenderer(tguiWidget_getSharedRenderer(CPointer)); }
		}

		public string Text
		{
			get { return Util.GetStringFromC_UTF32(tguiTextBox_getText(CPointer)); }
			set { tguiTextBox_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
		}

		public void AddText(string text)
		{
			tguiTextBox_addText(CPointer, Util.ConvertStringForC_UTF32(text));
		}

		public string SelectedText
		{
			get { return Util.GetStringFromC_UTF32(tguiTextBox_getSelectedText(CPointer)); }
		}

		public uint TextSize
		{
			get { return tguiTextBox_getTextSize(CPointer); }
			set { tguiTextBox_setTextSize(CPointer, value); }
		}

		public uint MaximumCharacters
		{
			get { return tguiTextBox_getMaximumCharacters(CPointer); }
			set { tguiTextBox_setMaximumCharacters(CPointer, value); }
		}

		public uint CaretPosition {
			get { return tguiTextBox_getCaretPosition (CPointer); }
			set { tguiTextBox_setCaretPosition (CPointer, value); }
		}

		public bool ReadOnly
		{
			get { return tguiTextBox_isReadOnly(CPointer); }
			set { tguiTextBox_setReadOnly(CPointer, value); }
		}

		public bool VerticalScrollbarPresent
		{
			get { return tguiTextBox_isVerticalScrollbarPresent(CPointer); }
			set { tguiTextBox_setVerticalScrollbarPresent(CPointer, value); }
		}

		public uint LinesCount {
			get { return tguiTextBox_getLinesCount (CPointer); }
		}


		protected override void InitSignals()
		{
			base.InitSignals();

            TextChangedCallback = new CallbackActionString(ProcessTextChangedSignal);
            tguiTextBox_connect_onTextChange(CPointer, TextChangedCallback, out IntPtr error);
		    if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		private void ProcessTextChangedSignal(IntPtr text)
		{
            TextChanged?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(text)));
        }

		/// <summary>Event handler for the TextChanged signal</summary>
		public event EventHandler<SignalArgsString> TextChanged = null;

	    private CallbackActionString TextChangedCallback;


	    #region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTextBox_create();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTextBox_setText(IntPtr cPointer, IntPtr value);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTextBox_addText(IntPtr cPointer, IntPtr value);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTextBox_getText(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiTextBox_getSelectedText(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTextBox_setTextSize(IntPtr cPointer, uint textSize);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTextBox_getTextSize(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTextBox_setMaximumCharacters(IntPtr cPointer, uint maximumCharacters);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTextBox_getMaximumCharacters(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTextBox_setCaretPosition (IntPtr cPointer, uint charactersBeforeCaret);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTextBox_getCaretPosition (IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTextBox_setReadOnly(IntPtr cPointer, bool readOnly);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiTextBox_isReadOnly(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTextBox_setVerticalScrollbarPresent(IntPtr cPointer, bool present);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiTextBox_isVerticalScrollbarPresent(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiTextBox_getLinesCount (IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiTextBox_connect_onTextChange(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionString func, out IntPtr error);

		#endregion
	}
}
