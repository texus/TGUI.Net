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
	public class Knob : Widget
	{
		public Knob()
			: base(tguiKnob_create())
		{
		}

		public Knob(int min, int max)
			: this()
		{
			Minimum = min;
			Maximum = max;
		}

		protected internal Knob(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Knob(Knob copy)
			: base(copy)
		{
		}

		public new KnobRenderer Renderer
		{
			get { return new KnobRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public float StartRotation
		{
			get { return tguiKnob_getStartRotation(CPointer); }
			set { tguiKnob_setStartRotation(CPointer, value); }
		}

		public float EndRotation
		{
			get { return tguiKnob_getEndRotation(CPointer); }
			set { tguiKnob_setEndRotation(CPointer, value); }
		}

		public int Minimum
		{
			get { return tguiKnob_getMinimum(CPointer); }
			set { tguiKnob_setMinimum(CPointer, value); }
		}

		public int Maximum
		{
			get { return tguiKnob_getMaximum(CPointer); }
			set { tguiKnob_setMaximum(CPointer, value); }
		}

		public int Value
		{
			get { return tguiKnob_getValue(CPointer); }
			set { tguiKnob_setValue(CPointer, value); }
		}

		public bool ClockwiseTurning
		{
			get { return tguiKnob_getClockwiseTurning(CPointer); }
			set { tguiKnob_setClockwiseTurning(CPointer, value); }
		}


		protected override void InitSignals()
		{
			base.InitSignals();

			IntPtr error;
			tguiWidget_connect_int(CPointer, Util.ConvertStringForC_ASCII("ValueChanged"), ProcessValueChangedSignal, out error);
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


		#region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiKnob_create();

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnob_setStartRotation(IntPtr cPointer, float startRotation);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiKnob_getStartRotation(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnob_setEndRotation(IntPtr cPointer, float endRotation);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiKnob_getEndRotation(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnob_setMinimum(IntPtr cPointer, int minimum);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiKnob_getMinimum(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnob_setMaximum(IntPtr cPointer, int maximum);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiKnob_getMaximum(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnob_setValue(IntPtr cPointer, int value);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected int tguiKnob_getValue(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiKnob_setClockwiseTurning(IntPtr cPointer, bool clockwise);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiKnob_getClockwiseTurning(IntPtr cPointer);

		#endregion
	}
}
