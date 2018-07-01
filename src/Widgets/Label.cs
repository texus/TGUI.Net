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
using SFML.System;

namespace TGUI
{
    public class Label : ClickableWidget
    {
        public Label(string text = "")
            : base(tguiLabel_create())
        {
            if (text.Length > 0)
                Text = text;
        }

        protected internal Label(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public Label(Button copy)
            : base(copy)
        {
        }

        public new LabelRenderer Renderer
        {
            get { return new LabelRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        public new LabelRenderer SharedRenderer
        {
            get { return new LabelRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        public string Text
        {
            get { return Util.GetStringFromC_UTF32(tguiLabel_getText(CPointer)); }
            set { tguiLabel_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        public uint TextSize
        {
            get { return tguiLabel_getTextSize(CPointer); }
            set { tguiLabel_setTextSize(CPointer, value); }
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return tguiLabel_getHorizontalAlignment(CPointer); }
            set { tguiLabel_setHorizontalAlignment(CPointer, value); }
        }

        public VerticalAlignment VerticalAlignmentAlignment
        {
            get { return tguiLabel_getVerticalAlignment(CPointer); }
            set { tguiLabel_setVerticalAlignment(CPointer, value); }
        }

        public bool AutoSize
        {
            get { return tguiLabel_getAutoSize(CPointer); }
            set { tguiLabel_setAutoSize(CPointer, value); }
        }

        public float MaximumTextWidth
        {
            get { return tguiLabel_getMaximumTextWidth(CPointer); }
            set { tguiLabel_setMaximumTextWidth(CPointer, value); }
        }

        public bool IgnoreMouseEvents
        {
            get { return tguiLabel_isIgnoringMouseEvents(CPointer); }
            set { tguiLabel_ignoreMouseEvents(CPointer, value); }
        }

        protected override void InitSignals()
        {
            base.InitSignals();

            DoubleClickedCallback = new CallbackActionString(ProcessDoubleClickedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("DoubleClicked"), DoubleClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessDoubleClickedSignal(IntPtr text)
        {
            DoubleClicked?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(text)));
        }

        /// <summary>Event handler for the DoubleClicked signal</summary>
        public event EventHandler<SignalArgsString> DoubleClicked = null;

        private CallbackActionString DoubleClickedCallback;

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiLabel_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabel_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiLabel_getText(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabel_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiLabel_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabel_setHorizontalAlignment(IntPtr cPointer, HorizontalAlignment alignment);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected HorizontalAlignment tguiLabel_getHorizontalAlignment(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabel_setVerticalAlignment(IntPtr cPointer, VerticalAlignment alignment);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected VerticalAlignment tguiLabel_getVerticalAlignment(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabel_setAutoSize(IntPtr cPointer, bool autoSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiLabel_getAutoSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabel_setMaximumTextWidth(IntPtr cPointer, float maximumTextWidth);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected float tguiLabel_getMaximumTextWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiLabel_ignoreMouseEvents(IntPtr cPointer, bool ignore);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiLabel_isIgnoringMouseEvents(IntPtr cPointer);

        #endregion
    }
}
