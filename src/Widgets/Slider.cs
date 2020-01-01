/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2020 Bruno Van de Velde (vdv_b@tgui.eu)
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

        public Slider(float min, float max)
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

        public float Minimum
        {
            get { return tguiSlider_getMinimum(CPointer); }
            set { tguiSlider_setMinimum(CPointer, value); }
        }

        public float Maximum
        {
            get { return tguiSlider_getMaximum(CPointer); }
            set { tguiSlider_setMaximum(CPointer, value); }
        }

        public float Value
        {
            get { return tguiSlider_getValue(CPointer); }
            set { tguiSlider_setValue(CPointer, value); }
        }

        public float Step
        {
            get { return tguiSlider_getStep(CPointer); }
            set { tguiSlider_setStep(CPointer, value); }
        }

        public bool InvertedDirection
        {
            get { return tguiSlider_getInvertedDirection(CPointer); }
            set { tguiSlider_setInvertedDirection(CPointer, value); }
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
        static extern private IntPtr tguiSlider_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSlider_setMinimum(IntPtr cPointer, float minimum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiSlider_getMinimum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSlider_setMaximum(IntPtr cPointer, float maximum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiSlider_getMaximum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSlider_setValue(IntPtr cPointer, float value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiSlider_getValue(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSlider_setStep(IntPtr cPointer, float step);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiSlider_getStep(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSlider_setInvertedDirection(IntPtr cPointer, bool invertedDirection);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiSlider_getInvertedDirection(IntPtr cPointer);

        #endregion
    }
}
