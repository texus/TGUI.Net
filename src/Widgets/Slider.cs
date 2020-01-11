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
    /// Slider widget
    /// </summary>
    public class Slider : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Slider()
            : base(tguiSlider_create())
        {
        }

        /// <summary>
        /// Constructor that sets the minimum and maximum properties
        /// </summary>
        /// <param name="min">Minimum slider value</param>
        /// <param name="max">Maximum slider value</param>
        public Slider(float min, float max)
            : this()
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Slider(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Slider(Slider copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets or sets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After retrieving the renderer, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new SliderRenderer Renderer
        {
            get { return new SliderRenderer(tguiWidget_getRenderer(CPointer)); }
            set { SetRenderer(value.Data); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new SliderRenderer SharedRenderer
        {
            get { return new SliderRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets the minimum value of the slider
        /// </summary>
        public float Minimum
        {
            get { return tguiSlider_getMinimum(CPointer); }
            set { tguiSlider_setMinimum(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the maximum value of the slider
        /// </summary>
        public float Maximum
        {
            get { return tguiSlider_getMaximum(CPointer); }
            set { tguiSlider_setMaximum(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the current value of the slider
        /// </summary>
        public float Value
        {
            get { return tguiSlider_getValue(CPointer); }
            set { tguiSlider_setValue(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the number of positions the thumb advances with each move
        /// </summary>
        public float Step
        {
            get { return tguiSlider_getStep(CPointer); }
            set { tguiSlider_setStep(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the scrollbar lies horizontally or vertically
        /// </summary>
        /// <remarks>
        /// This function will swap the width and height of the scrollbar if it didn't lie in the wanted direction.
        /// </remarks>
        public bool VerticalScroll
        {
            get { return tguiSlider_getVerticalScroll(CPointer); }
            set { tguiSlider_setVerticalScroll(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the sides of the slider that represents the minimum and maximum are inverted
        /// </summary>
        public bool InvertedDirection
        {
            get { return tguiSlider_getInvertedDirection(CPointer); }
            set { tguiSlider_setInvertedDirection(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the mouse wheel can be used to change the value of the slider
        /// </summary>
        public bool ChangeValueOnScroll
        {
            get { return tguiSlider_getChangeValueOnScroll(CPointer); }
            set { tguiSlider_setChangeValueOnScroll(CPointer, value); }
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            ValueChangedCallback = new CallbackActionFloat((val) => SendSignal(myValueChangedEventKey, new SignalArgsFloat(val)));
            AddInternalSignal(tguiWidget_connectFloat(CPointer, Util.ConvertStringForC_ASCII("ValueChanged"), ValueChangedCallback));
        }

        /// <summary>Event handler for the ValueChanged signal</summary>
        public event EventHandler<SignalArgsFloat> ValueChanged
        {
            add { myEventHandlerList.AddHandler(myValueChangedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myValueChangedEventKey, value); }
        }

        private CallbackActionFloat ValueChangedCallback;
        static readonly object myValueChangedEventKey = new object();

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
        static extern private void tguiSlider_setVerticalScroll(IntPtr cPointer, bool vertical);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiSlider_getVerticalScroll(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSlider_setInvertedDirection(IntPtr cPointer, bool invertedDirection);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiSlider_getInvertedDirection(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiSlider_setChangeValueOnScroll(IntPtr cPointer, bool changeValueOnScroll);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiSlider_getChangeValueOnScroll(IntPtr cPointer);

        #endregion
    }
}
