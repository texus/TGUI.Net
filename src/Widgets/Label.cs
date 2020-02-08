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
    /// Label widget
    /// </summary>
    public class Label : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="text">Text of the label</param>
        public Label(string text = "")
            : base(tguiLabel_create())
        {
            if (text.Length > 0)
                Text = text;
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Label(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Label(Button copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets or sets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After retrieving the renderer, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new LabelRenderer Renderer
        {
            get { return new LabelRenderer(tguiWidget_getRenderer(CPointer)); }
            set { SetRenderer(value.Data); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new LabelRenderer SharedRenderer
        {
            get { return new LabelRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets the text of the label
        /// </summary>
        /// <remarks>
        /// When the text is auto-sized (default), then the size of the label will be changed to fit the whole text.
        /// </remarks>
        public string Text
        {
            get { return Util.GetStringFromC_UTF32(tguiLabel_getText(CPointer)); }
            set { tguiLabel_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        /// <summary>
        /// Gets or sets the horizontal text alignment
        /// </summary>
        /// <remarks>
        /// By default the text is aligned to the left.
        /// </remarks>
        public HorizontalAlignment HorizontalAlignment
        {
            get { return tguiLabel_getHorizontalAlignment(CPointer); }
            set { tguiLabel_setHorizontalAlignment(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the vertical text alignment
        /// </summary>
        /// <remarks>
        /// By default the text is aligned to the top.
        /// </remarks>
        public VerticalAlignment VerticalAlignmentAlignment
        {
            get { return tguiLabel_getVerticalAlignment(CPointer); }
            set { tguiLabel_setVerticalAlignment(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the label is auto-sized or not
        /// </summary>
        /// <remarks>
        /// When the label is in auto-size mode, the width and height of the label will be changed to fit the text.
        /// Otherwise, only the part defined by the size will be visible.
        ///
        /// The label is auto-sized by default.
        /// </remarks>
        public bool AutoSize
        {
            get { return tguiLabel_getAutoSize(CPointer); }
            set { tguiLabel_setAutoSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the maximum width that the text will have when auto-sizing
        /// </summary>
        /// <remarks>
        /// This property is ignored when an exact size has been given.
        /// Pass 0 to this function to disable the maximum.
        ///
        /// When the text is auto-sizing then the text will be split over several lines when its width would exceed the
        /// value passed to this function.
        /// </remarks>
        public float MaximumTextWidth
        {
            get { return tguiLabel_getMaximumTextWidth(CPointer); }
            set { tguiLabel_setMaximumTextWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the widget should completely ignore mouse events and let them pass to the widgets behind it
        /// </summary>
        public bool IgnoreMouseEvents
        {
            get { return tguiLabel_isIgnoringMouseEvents(CPointer); }
            set { tguiLabel_ignoreMouseEvents(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets when the vertical scrollbar should be displayed
        /// </summary>
        public Scrollbar.Policy ScrollbarPolicy
        {
            get { return tguiLabel_getScrollbarPolicy(CPointer); }
            set { tguiLabel_setScrollbarPolicy(CPointer, value); }
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            DoubleClickedCallback = new CallbackActionString((text) => SendSignal(myDoubleClickedEventKey, new SignalArgsString(Util.GetStringFromC_UTF32(text))));
            AddInternalSignal(tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("DoubleClicked"), DoubleClickedCallback));
        }

        /// <summary>Event handler for the DoubleClicked signal</summary>
        public event EventHandler<SignalArgsString> DoubleClicked
        {
            add { myEventHandlerList.AddHandler(myDoubleClickedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myDoubleClickedEventKey, value); }
        }

        private CallbackActionString DoubleClickedCallback;
        static readonly object myDoubleClickedEventKey = new object();

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLabel_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabel_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiLabel_getText(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabel_setHorizontalAlignment(IntPtr cPointer, HorizontalAlignment alignment);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private HorizontalAlignment tguiLabel_getHorizontalAlignment(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabel_setVerticalAlignment(IntPtr cPointer, VerticalAlignment alignment);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private VerticalAlignment tguiLabel_getVerticalAlignment(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabel_setAutoSize(IntPtr cPointer, bool autoSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiLabel_getAutoSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabel_setMaximumTextWidth(IntPtr cPointer, float maximumTextWidth);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiLabel_getMaximumTextWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabel_ignoreMouseEvents(IntPtr cPointer, bool ignore);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiLabel_isIgnoringMouseEvents(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiLabel_setScrollbarPolicy(IntPtr cPointer, Scrollbar.Policy policy);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Scrollbar.Policy tguiLabel_getScrollbarPolicy(IntPtr cPointer);

        #endregion
    }
}
