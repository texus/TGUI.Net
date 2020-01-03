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
    public class RangeSlider : Widget
    {
        public RangeSlider()
            : base(tguiRangeSlider_create())
        {
        }

        public RangeSlider(float min, float max)
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
            set { SetRenderer(value.Data); }
        }

        public new RangeSliderRenderer SharedRenderer
        {
            get { return new RangeSliderRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        public float Minimum
        {
            get { return tguiRangeSlider_getMinimum(CPointer); }
            set { tguiRangeSlider_setMinimum(CPointer, value); }
        }

        public float Maximum
        {
            get { return tguiRangeSlider_getMaximum(CPointer); }
            set { tguiRangeSlider_setMaximum(CPointer, value); }
        }

        public float SelectionStart
        {
            get { return tguiRangeSlider_getSelectionStart(CPointer); }
            set { tguiRangeSlider_setSelectionStart(CPointer, value); }
        }

        public float SelectionEnd
        {
            get { return tguiRangeSlider_getSelectionEnd(CPointer); }
            set { tguiRangeSlider_setSelectionEnd(CPointer, value); }
        }

        public float Step
        {
            get { return tguiRangeSlider_getStep(CPointer); }
            set { tguiRangeSlider_setStep(CPointer, value); }
        }

        protected override void InitSignals()
        {
            base.InitSignals();

            RangeChangedCallback = new CallbackActionRange(ProcessRangeChangedSignal);
            if (tguiWidget_connectRange(CPointer, Util.ConvertStringForC_ASCII("RangeChanged"), RangeChangedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessRangeChangedSignal(float start, float end)
        {
            RangeChanged?.Invoke(this, new SignalArgsRange(start, end));
        }

        /// <summary>Event handler for the RangeChanged signal</summary>
        public event EventHandler<SignalArgsRange> RangeChanged = null;

        private CallbackActionRange RangeChangedCallback;

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiRangeSlider_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSlider_setMinimum(IntPtr cPointer, float minimum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiRangeSlider_getMinimum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSlider_setMaximum(IntPtr cPointer, float maximum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiRangeSlider_getMaximum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSlider_setSelectionStart(IntPtr cPointer, float start);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiRangeSlider_getSelectionStart(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSlider_setSelectionEnd(IntPtr cPointer, float end);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiRangeSlider_getSelectionEnd(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRangeSlider_setStep(IntPtr cPointer, float step);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiRangeSlider_getStep(IntPtr cPointer);

        #endregion
    }
}
