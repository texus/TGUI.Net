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
    /// Scrollbar widget
    /// </summary>
    public class Scrollbar : Widget
    {
        /// <summary>
        /// Defines when the scrollbar shows up
        /// </summary>
        public enum Policy
        {
            /// <summary>Show the scrollbar only when needed (default)</summary>
            Automatic,

            /// <summary>Always show the scrollbar, even when the contents fits</summary>
            Always,

            /// <summary>Never show the scrollbar, even if the contents does not fit</summary>
            Never
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Scrollbar()
            : base(tguiScrollbar_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Scrollbar(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Scrollbar(Scrollbar copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets or sets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After retrieving the renderer, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new ScrollbarRenderer Renderer
        {
            get { return new ScrollbarRenderer(tguiWidget_getRenderer(CPointer)); }
            set { SetRenderer(value.Data); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new ScrollbarRenderer SharedRenderer
        {
            get { return new ScrollbarRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets the viewport size
        /// </summary>
        /// <remarks>
        /// If the contents through which the scrollbar can scroll is 600 pixels of which only 200 pixels are visible on the
        /// screen then the viewport size should be set to 200 and the maximum should be set to 600. The thumb will occupy
        /// one third of the scrollbar track in this case. The possible scrollbar values are in the range [0, 400] in this case.
        ///
        /// Until the maximum is bigger than this value, no scrollbar will be drawn.
        /// You can however choose to always draw the scrollbar by setting AutoHide to false.
        /// </remarks>
        public uint ViewportSize
        {
            get { return tguiScrollbar_getViewportSize(CPointer); }
            set { tguiScrollbar_setViewportSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the maximum value
        /// </summary>
        /// <remarks>
        /// When the value is bigger than Maximum - ViewportSize then the value is set to Maximum - ViewportSize.
        /// The default maximum value is 10.
        /// </remarks>
        public uint Maximum
        {
            get { return tguiScrollbar_getMaximum(CPointer); }
            set { tguiScrollbar_setMaximum(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the value of the scrollbar
        /// </summary>
        /// <remarks>
        /// The value has to be smaller than Maximum - ViewportSize.
        /// </remarks>
        public uint Value
        {
            get { return tguiScrollbar_getValue(CPointer); }
            set { tguiScrollbar_setValue(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets how much the value changes when scrolling or pressing one of the arrows of the scrollbar
        /// </summary>
        public uint ScrollAmount
        {
            get { return tguiScrollbar_getScrollAmount(CPointer); }
            set { tguiScrollbar_setScrollAmount(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the scrollbar should hide automatically or not
        /// </summary>
        /// <remarks>
        /// When true (default), the scrollbar will not be drawn when the maximum is smaller than the viewportSize.
        /// </remarks>
        public bool AutoHide
        {
            get { return tguiScrollbar_getAutoHide(CPointer); }
            set { tguiScrollbar_setAutoHide(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the scrollbar lies horizontally or vertically
        /// </summary>
        /// <remarks>
        /// This function will swap the width and height of the scrollbar if it didn't lie in the wanted direction.
        /// </remarks>
        public bool VerticalScroll
        {
            get { return tguiScrollbar_getVerticalScroll(CPointer); }
            set { tguiScrollbar_setVerticalScroll(CPointer, value); }
        }

        /// <summary>
        /// Gets the default width of the scrollbar
        /// </summary>
        /// <remarks>
        /// The default width is the value the scrollbar has on construction or the size of the texture once a texture is set.
        /// </remarks>
        public float DefaultWidth
        {
            get { return tguiScrollbar_getDefaultWidth(CPointer); }
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            ValueChangedCallback = new CallbackActionUInt((val) => SendSignal(myValueChangedEventKey, new SignalArgsUInt(val)));
            AddInternalSignal(tguiWidget_connectUInt(CPointer, Util.ConvertStringForC_ASCII("ValueChanged"), ValueChangedCallback));
        }

        /// <summary>Event handler for the ValueChanged signal</summary>
        public event EventHandler<SignalArgsUInt> ValueChanged
        {
            add { myEventHandlerList.AddHandler(myValueChangedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myValueChangedEventKey, value); }
        }

        private CallbackActionUInt ValueChangedCallback;
        static readonly object myValueChangedEventKey = new object();

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiScrollbar_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbar_setViewportSize(IntPtr cPointer, uint viewport);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiScrollbar_getViewportSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbar_setMaximum(IntPtr cPointer, uint maximum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiScrollbar_getMaximum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbar_setValue(IntPtr cPointer, uint value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiScrollbar_getValue(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbar_setScrollAmount(IntPtr cPointer, uint scrollAmount);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiScrollbar_getScrollAmount(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbar_setAutoHide(IntPtr cPointer, bool autoHide);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiScrollbar_getAutoHide(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollbar_setVerticalScroll(IntPtr cPointer, bool vertical);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiScrollbar_getVerticalScroll(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiScrollbar_getDefaultWidth(IntPtr cPointer);

        #endregion
    }
}
