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
using SFML.System;

namespace TGUI
{
    /// <summary>
    /// Clickable widget
    /// </summary>
    public class ClickableWidget : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ClickableWidget()
            : base(tguiClickableWidget_create())
        {
        }

        /// <summary>
        /// Constructor to create the widget with a given size
        /// </summary>
        /// <param name="size">Initial size of the widget</param>
        public ClickableWidget(Vector2f size)
            : this()
        {
            Size = size;
        }

        /// <summary>
        /// Constructor to create the widget with a given size
        /// </summary>
        /// <param name="width">Initial width of the widget</param>
        /// <param name="height">Initial height of the widget</param>
        public ClickableWidget(float width, float height)
            : this(new Vector2f(width, height))
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ClickableWidget(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ClickableWidget(ClickableWidget copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            MousePressedCallback = new CallbackActionVector2f(ProcessMousePressedSignal);
            if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("MousePressed"), MousePressedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            MouseReleasedCallback = new CallbackActionVector2f(ProcessMouseReleasedSignal);
            if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("MouseReleased"), MouseReleasedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            ClickedCallback = new CallbackActionVector2f(ProcessClickedSignal);
            if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("Clicked"), ClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            RightMousePressedCallback = new CallbackActionVector2f(ProcessRightMousePressedSignal);
            if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("RightMousePressed"), RightMousePressedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            RightMouseReleasedCallback = new CallbackActionVector2f(ProcessRightMouseReleasedSignal);
            if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("RightMouseReleased"), RightMouseReleasedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            RightClickedCallback = new CallbackActionVector2f(ProcessRightClickedSignal);
            if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("RightClicked"), RightClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessMousePressedSignal(Vector2f pos)
        {
            MousePressed?.Invoke(this, new SignalArgsVector2f(pos));
        }

        private void ProcessMouseReleasedSignal(Vector2f pos)
        {
            MouseReleased?.Invoke(this, new SignalArgsVector2f(pos));
        }

        private void ProcessClickedSignal(Vector2f pos)
        {
            Clicked?.Invoke(this, new SignalArgsVector2f(pos));
        }

        private void ProcessRightMousePressedSignal(Vector2f pos)
        {
            RightMousePressed?.Invoke(this, new SignalArgsVector2f(pos));
        }

        private void ProcessRightMouseReleasedSignal(Vector2f pos)
        {
            RightMouseReleased?.Invoke(this, new SignalArgsVector2f(pos));
        }

        private void ProcessRightClickedSignal(Vector2f pos)
        {
            RightClicked?.Invoke(this, new SignalArgsVector2f(pos));
        }

        /// <summary>Event handler for the MousePressed signal</summary>
        public event EventHandler<SignalArgsVector2f> MousePressed = null;

        /// <summary>Event handler for the MouseReleased signal</summary>
        public event EventHandler<SignalArgsVector2f> MouseReleased = null;

        /// <summary>Event handler for the Clicked signal</summary>
        public event EventHandler<SignalArgsVector2f> Clicked = null;

        /// <summary>Event handler for the RightMousePressed signal</summary>
        public event EventHandler<SignalArgsVector2f> RightMousePressed = null;

        /// <summary>Event handler for the RightMouseReleased signal</summary>
        public event EventHandler<SignalArgsVector2f> RightMouseReleased = null;

        /// <summary>Event handler for the RightClicked signal</summary>
        public event EventHandler<SignalArgsVector2f> RightClicked = null;

        private CallbackActionVector2f MousePressedCallback;
        private CallbackActionVector2f MouseReleasedCallback;
        private CallbackActionVector2f ClickedCallback;
        private CallbackActionVector2f RightMousePressedCallback;
        private CallbackActionVector2f RightMouseReleasedCallback;
        private CallbackActionVector2f RightClickedCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiClickableWidget_create();

        #endregion
    }
}
