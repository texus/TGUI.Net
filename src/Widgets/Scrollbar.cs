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
	public class Scrollbar : Widget
	{
		public Scrollbar()
			: base(tguiScrollbar_create())
		{
		}

		protected internal Scrollbar(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Scrollbar(Scrollbar copy)
			: base(copy)
		{
		}

		public new ScrollbarRenderer Renderer
		{
			get { return new ScrollbarRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public uint LowValue
		{
			get { return tguiScrollbar_getLowValue(CPointer); }
			set { tguiScrollbar_setLowValue(CPointer, value); }
		}

		public uint Maximum
		{
			get { return tguiScrollbar_getMaximum(CPointer); }
			set { tguiScrollbar_setMaximum(CPointer, value); }
		}

		public uint Value
		{
			get { return tguiScrollbar_getValue(CPointer); }
			set { tguiScrollbar_setValue(CPointer, value); }
		}

		public uint ScrollAmount
		{
			get { return tguiScrollbar_getScrollAmount(CPointer); }
			set { tguiScrollbar_setScrollAmount(CPointer, value); }
		}

		public bool AutoHide
		{
			get { return tguiScrollbar_getAutoHide(CPointer); }
			set { tguiScrollbar_setAutoHide(CPointer, value); }
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

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiScrollbar_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollbar_setLowValue(IntPtr cPointer, uint lowValue);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiScrollbar_getLowValue(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollbar_setMaximum(IntPtr cPointer, uint maximum);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiScrollbar_getMaximum(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollbar_setValue(IntPtr cPointer, uint value);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiScrollbar_getValue(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollbar_setScrollAmount(IntPtr cPointer, uint scrollAmount);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiScrollbar_getScrollAmount(IntPtr cPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiScrollbar_setAutoHide(IntPtr cPointer, bool autoHide);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiScrollbar_getAutoHide(IntPtr cPointer);

		#endregion
	}
}
