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

        public new RadioButtonRenderer SharedRenderer
		{
			get { return new RadioButtonRenderer(tguiWidget_getSharedRenderer(CPointer)); }
		}

		public bool Checked
		{
			get { return tguiRadioButton_isChecked(CPointer); }
			set { tguiRadioButton_setChecked(CPointer, value); }
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

		protected override void InitSignals()
		{
			base.InitSignals();

            ToggledCallback = new CallbackActionInt(ProcessToggledSignal);
		    if (tguiWidget_connectInt(CPointer, Util.ConvertStringForC_ASCII("Checked"), ToggledCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
		    if (tguiWidget_connectInt(CPointer, Util.ConvertStringForC_ASCII("Unchecked"), ToggledCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
		}

		private void ProcessToggledSignal(int value)
		{
            Toggled?.Invoke(this, new SignalArgsBool(value != 0));
        }

		/// <summary>Event handler for the Checked/Unchecked signal</summary>
		public event EventHandler<SignalArgsBool> Toggled = null;

	    private CallbackActionInt ToggledCallback;

		#region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiRadioButton_create();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRadioButton_setChecked(IntPtr cPointer, bool check);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiRadioButton_isChecked(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRadioButton_setText(IntPtr cPointer, IntPtr value);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiRadioButton_getText(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRadioButton_setTextSize(IntPtr cPointer, uint textSize);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiRadioButton_getTextSize(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRadioButton_setTextClickable(IntPtr cPointer, bool clickable);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiRadioButton_isTextClickable(IntPtr cPointer);

		#endregion
	}
}
