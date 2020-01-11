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

            MousePressedCallback = new CallbackActionVector2f((pos) => SendSignal(myMousePressedEventKey, new SignalArgsVector2f(pos)));
            AddInternalSignal(tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("MousePressed"), MousePressedCallback));

            MouseReleasedCallback = new CallbackActionVector2f((pos) => SendSignal(myMouseReleasedEventKey, new SignalArgsVector2f(pos)));
            AddInternalSignal(tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("MouseReleased"), MouseReleasedCallback));

            ClickedCallback = new CallbackActionVector2f((pos) => SendSignal(myClickedEventKey, new SignalArgsVector2f(pos)));
            AddInternalSignal(tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("Clicked"), ClickedCallback));

            RightMousePressedCallback = new CallbackActionVector2f((pos) => SendSignal(myRightMousePressedEventKey, new SignalArgsVector2f(pos)));
            AddInternalSignal(tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("RightMousePressed"), RightMousePressedCallback));

            RightMouseReleasedCallback = new CallbackActionVector2f((pos) => SendSignal(myRightMouseReleasedEventKey, new SignalArgsVector2f(pos)));
            AddInternalSignal(tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("RightMouseReleased"), RightMouseReleasedCallback));

            RightClickedCallback = new CallbackActionVector2f((pos) => SendSignal(myRightClickedEventKey, new SignalArgsVector2f(pos)));
            AddInternalSignal(tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("RightClicked"), RightClickedCallback));
        }

        /// <summary>Event handler for the MousePressed signal</summary>
        public event EventHandler<SignalArgsVector2f> MousePressed
        {
            add { myEventHandlerList.AddHandler(myMousePressedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myMousePressedEventKey, value); }
        }

        /// <summary>Event handler for the MouseReleased signal</summary>
        public event EventHandler<SignalArgsVector2f> MouseReleased
        {
            add { myEventHandlerList.AddHandler(myMouseReleasedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myMouseReleasedEventKey, value); }
        }

        /// <summary>Event handler for the Clicked signal</summary>
        public event EventHandler<SignalArgsVector2f> Clicked
        {
            add { myEventHandlerList.AddHandler(myClickedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myClickedEventKey, value); }
        }

        /// <summary>Event handler for the RightMousePressed signal</summary>
        public event EventHandler<SignalArgsVector2f> RightMousePressed
        {
            add { myEventHandlerList.AddHandler(myRightMousePressedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myRightMousePressedEventKey, value); }
        }

        /// <summary>Event handler for the RightMouseReleased signal</summary>
        public event EventHandler<SignalArgsVector2f> RightMouseReleased
        {
            add { myEventHandlerList.AddHandler(myRightMouseReleasedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myRightMouseReleasedEventKey, value); }
        }

        /// <summary>Event handler for the RightClicked signal</summary>
        public event EventHandler<SignalArgsVector2f> RightClicked
        {
            add { myEventHandlerList.AddHandler(myRightClickedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myRightClickedEventKey, value); }
        }

        private CallbackActionVector2f MousePressedCallback;
        private CallbackActionVector2f MouseReleasedCallback;
        private CallbackActionVector2f ClickedCallback;
        private CallbackActionVector2f RightMousePressedCallback;
        private CallbackActionVector2f RightMouseReleasedCallback;
        private CallbackActionVector2f RightClickedCallback;

        static readonly object myMousePressedEventKey = new object();
        static readonly object myMouseReleasedEventKey = new object();
        static readonly object myClickedEventKey = new object();
        static readonly object myRightMousePressedEventKey = new object();
        static readonly object myRightMouseReleasedEventKey = new object();
        static readonly object myRightClickedEventKey = new object();

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiClickableWidget_create();

        #endregion
    }
}
