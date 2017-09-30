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
	public class Slider : Widget
	{
		public Slider()
			: base(tguiSlider_create())
		{
		}

		public Slider(int min, int max)
			: this()
		{
			Minimum = min;
			Maximum = max;
		}

		protected internal Slider(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Slider(Slider copy)
			: base(copy)
		{
		}

		public new SliderRenderer Renderer
		{
			get { return new SliderRenderer(tguiWidget_getRenderer(CPointer)); }
		}

        public new SliderRenderer SharedRenderer
		{
			get { return new SliderRenderer(tguiWidget_getSharedRenderer(CPointer)); }
		}

		public int Minimum
		{
			get { return tguiSlider_getMinimum(CPointer); }
			set { tguiSlider_setMinimum(CPointer, value); }
		}

		public int Maximum
		{
			get { return tguiSlider_getMaximum(CPointer); }
			set { tguiSlider_setMaximum(CPointer, value); }
		}

		public int Value
		{
			get { return tguiSlider_getValue(CPointer); }
			set { tguiSlider_setValue(CPointer, value); }
		}


		protected override void InitSignals()
		{
			base.InitSignals();

			IntPtr error;
		    ValueChangedCallback = new CallbackActionInt(ProcessValueChangedSignal);
		    tguiSlider_connect_onValueChange(CPointer, ValueChangedCallback, out error);
		    if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		private void ProcessValueChangedSignal(int value)
		{
			if (ValueChanged != null)
				ValueChanged(this, new SignalArgsInt(value));
		}

		/// <summary>Event handler for the ValueChanged signal</summary>
		public event EventHandler<SignalArgsInt> ValueChanged = null;

	    private CallbackActionInt ValueChangedCallback;

	    #region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiSlider_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSlider_setMinimum(IntPtr cPointer, int minimum);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiSlider_getMinimum(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSlider_setMaximum(IntPtr cPointer, int maximum);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiSlider_getMaximum(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSlider_setValue(IntPtr cPointer, int value);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiSlider_getValue(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiSlider_connect_onValueChange(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionInt func, out IntPtr error);

		#endregion
	}
}
