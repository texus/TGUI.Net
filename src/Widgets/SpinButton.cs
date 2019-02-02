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
    public class SpinButton : Widget
    {
        public SpinButton()
            : base(tguiSpinButton_create())
        {
        }

        public SpinButton(float min, float max)
            : this()
        {
            Minimum = min;
            Maximum = max;
        }

        protected internal SpinButton(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public SpinButton(SpinButton copy)
            : base(copy)
        {
        }

        public new SpinButtonRenderer Renderer
        {
            get { return new SpinButtonRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        public new SpinButtonRenderer SharedRenderer
        {
            get { return new SpinButtonRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        public float Minimum
        {
            get { return tguiSpinButton_getMinimum(CPointer); }
            set { tguiSpinButton_setMinimum(CPointer, value); }
        }

        public float Maximum
        {
            get { return tguiSpinButton_getMaximum(CPointer); }
            set { tguiSpinButton_setMaximum(CPointer, value); }
        }

        public float Value
        {
            get { return tguiSpinButton_getValue(CPointer); }
            set { tguiSpinButton_setValue(CPointer, value); }
        }

        public float Step
        {
            get { return tguiSpinButton_getStep(CPointer); }
            set { tguiSpinButton_setStep(CPointer, value); }
        }

        protected override void InitSignals()
        {
            base.InitSignals();

            ValueChangedCallback = new CallbackActionFloat(ProcessValueChangedSignal);
            if (tguiWidget_connectFloat(CPointer, Util.ConvertStringForC_ASCII("ValueChanged"), ValueChangedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessValueChangedSignal(float value)
        {
            ValueChanged?.Invoke(this, new SignalArgsFloat(value));
        }

        /// <summary>Event handler for the ValueChanged signal</summary>
        public event EventHandler<SignalArgsFloat> ValueChanged = null;

        private CallbackActionFloat ValueChangedCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiSpinButton_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSpinButton_setMinimum(IntPtr cPointer, float minimum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiSpinButton_getMinimum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSpinButton_setMaximum(IntPtr cPointer, float maximum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiSpinButton_getMaximum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSpinButton_setValue(IntPtr cPointer, float value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiSpinButton_getValue(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSpinButton_setStep(IntPtr cPointer, float step);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiSpinButton_getStep(IntPtr cPointer);

        #endregion
    }
}
