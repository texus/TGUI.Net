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
    /// <summary>
    /// Spin button widget
    /// </summary>
    public class SpinButton : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SpinButton()
            : base(tguiSpinButton_create())
        {
        }

        /// <summary>
        /// Constructor that sets the minimum and maximum properties
        /// </summary>
        /// <param name="min">Minimum spin button value</param>
        /// <param name="max">Maximum spin button value</param>
        public SpinButton(float min, float max)
            : this()
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SpinButton(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public SpinButton(SpinButton copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets or sets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After retrieving the renderer, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new SpinButtonRenderer Renderer
        {
            get { return new SpinButtonRenderer(tguiWidget_getRenderer(CPointer)); }
            set { SetRenderer(value.Data); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new SpinButtonRenderer SharedRenderer
        {
            get { return new SpinButtonRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets the minimum value of the spin button
        /// </summary>
        public float Minimum
        {
            get { return tguiSpinButton_getMinimum(CPointer); }
            set { tguiSpinButton_setMinimum(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the maximum value of the spin button
        /// </summary>
        public float Maximum
        {
            get { return tguiSpinButton_getMaximum(CPointer); }
            set { tguiSpinButton_setMaximum(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the current value of the spin button
        /// </summary>
        public float Value
        {
            get { return tguiSpinButton_getValue(CPointer); }
            set { tguiSpinButton_setValue(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the amount the value changes when clicking on the spin button
        /// </summary>
        public float Step
        {
            get { return tguiSpinButton_getStep(CPointer); }
            set { tguiSpinButton_setStep(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the spin button lies horizontally or vertically
        /// </summary>
        /// <remarks>
        /// This function will swap the width and height of the scrollbar if it didn't lie in the wanted direction.
        /// </remarks>
        public bool VerticalScroll
        {
            get { return tguiSpinButton_getVerticalScroll(CPointer); }
            set { tguiSpinButton_setVerticalScroll(CPointer, value); }
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
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

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSpinButton_setVerticalScroll(IntPtr cPointer, bool vertical);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiSpinButton_getVerticalScroll(IntPtr cPointer);

        #endregion
    }
}
