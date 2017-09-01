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
	public class RangeSlider : Widget
	{
		public RangeSlider()
			: base(tguiRangeSlider_create())
		{
		}

		public RangeSlider(int min, int max)
			: this()
		{
			Minimum = min;
			Maximum = max;
		}

		protected internal RangeSlider(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public RangeSlider(RangeSlider copy)
			: base(copy)
		{
		}

		public new RangeSliderRenderer Renderer
		{
			get { return new RangeSliderRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public int Minimum
		{
			get { return tguiRangeSlider_getMinimum(CPointer); }
			set { tguiRangeSlider_setMinimum(CPointer, value); }
		}

		public int Maximum
		{
			get { return tguiRangeSlider_getMaximum(CPointer); }
			set { tguiRangeSlider_setMaximum(CPointer, value); }
		}

		public int SelectionStart
		{
			get { return tguiRangeSlider_getSelectionStart(CPointer); }
			set { tguiRangeSlider_setSelectionStart(CPointer, value); }
		}

		public int SelectionEnd
		{
			get { return tguiRangeSlider_getSelectionEnd(CPointer); }
			set { tguiRangeSlider_setSelectionEnd(CPointer, value); }
		}


		protected override void InitSignals()
		{
			base.InitSignals();

			IntPtr error;
		    RangeChangedCallback = new CallbackActionRange(ProcessRangeChangedSignal);
		    tguiRangeSlider_connect_onRangeChange(CPointer, RangeChangedCallback, out error);
		    if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		private void ProcessRangeChangedSignal(int start, int end)
		{
			if (RangeChanged != null)
				RangeChanged(this, new SignalArgsRange(start, end));
		}

		/// <summary>Event handler for the RangeChanged signal</summary>
		public event EventHandler<SignalArgsRange> RangeChanged = null;

	    private CallbackActionRange RangeChangedCallback;

	    #region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiRangeSlider_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRangeSlider_setMinimum(IntPtr cPointer, int minimum);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiRangeSlider_getMinimum(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRangeSlider_setMaximum(IntPtr cPointer, int maximum);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiRangeSlider_getMaximum(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRangeSlider_setSelectionStart(IntPtr cPointer, int start);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiRangeSlider_getSelectionStart(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRangeSlider_setSelectionEnd(IntPtr cPointer, int end);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiRangeSlider_getSelectionEnd(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiRangeSlider_connect_onRangeChange(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionRange func, out IntPtr error);

		#endregion
	}
}
