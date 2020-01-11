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
    /// Radio button widget
    /// </summary>
    public class RadioButton : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="text">The text to display next to the radio button</param>
        public RadioButton(string text = "")
            : base(tguiRadioButton_create())
        {
            if (text.Length > 0)
                Text = text;
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal RadioButton(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public RadioButton(RadioButton copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets or sets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After retrieving the renderer, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new RadioButtonRenderer Renderer
        {
            get { return new RadioButtonRenderer(tguiWidget_getRenderer(CPointer)); }
            set { SetRenderer(value.Data); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new RadioButtonRenderer SharedRenderer
        {
            get { return new RadioButtonRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets whether the radio button is checked
        /// </summary>
        /// <remarks>
        /// When checking a radio button, all other radio buttons that have the same parent will be unchecked.
        /// </remarks>
        public bool Checked
        {
            get { return tguiRadioButton_isChecked(CPointer); }
            set { tguiRadioButton_setChecked(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text to display next to the radio button
        /// </summary>
        public string Text
        {
            get { return Util.GetStringFromC_UTF32(tguiRadioButton_getText(CPointer)); }
            set { tguiRadioButton_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        /// <summary>
        /// Gets or sets the character size of the text
        /// </summary>
        public uint TextSize
        {
            get { return tguiRadioButton_getTextSize(CPointer); }
            set { tguiRadioButton_setTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the radio button is be checked by clicking on the text next to it
        /// </summary>
        public bool TextClickable
        {
            get { return tguiRadioButton_isTextClickable(CPointer); }
            set { tguiRadioButton_setTextClickable(CPointer, value); }
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            ToggledCallback = new CallbackActionInt((val) => SendSignal(myToggledEventKey, new SignalArgsBool(val != 0)));
            AddInternalSignal(tguiWidget_connectInt(CPointer, Util.ConvertStringForC_ASCII("Changed"), ToggledCallback));
        }

        /// <summary>Event handler for the Checked/Unchecked signal</summary>
        public event EventHandler<SignalArgsBool> Toggled
        {
            add { myEventHandlerList.AddHandler(myToggledEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myToggledEventKey, value); }
        }

        private CallbackActionInt ToggledCallback;
        static readonly object myToggledEventKey = new object();

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiRadioButton_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRadioButton_setChecked(IntPtr cPointer, bool check);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiRadioButton_isChecked(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRadioButton_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiRadioButton_getText(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRadioButton_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiRadioButton_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiRadioButton_setTextClickable(IntPtr cPointer, bool clickable);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiRadioButton_isTextClickable(IntPtr cPointer);

        #endregion
    }
}
