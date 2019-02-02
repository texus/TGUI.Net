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
using System.Collections.Generic;

namespace TGUI
{
    public class MessageBox : ChildWindow
    {
        public MessageBox()
            : base(tguiMessageBox_create())
        {
        }

        public MessageBox(string title, string text = "", List<string> buttons = null)
            : base(tguiMessageBox_create())
        {
            buttons = buttons ?? new List<string>();

            Title = title;
            Text = text;
            foreach (var button in buttons)
                AddButton(button);
        }

        protected internal MessageBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public MessageBox(MessageBox copy)
            : base(copy)
        {
        }

        public new MessageBoxRenderer Renderer
        {
            get { return new MessageBoxRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        public new MessageBoxRenderer SharedRenderer
        {
            get { return new MessageBoxRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        public string Text
        {
            get { return Util.GetStringFromC_UTF32(tguiMessageBox_getText(CPointer)); }
            set { tguiMessageBox_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        public uint TextSize
        {
            get { return tguiMessageBox_getTextSize(CPointer); }
            set { tguiMessageBox_setTextSize(CPointer, value); }
        }

        public void AddButton(string text)
        {
            tguiMessageBox_addButton(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        protected override void InitSignals()
        {
            base.InitSignals();

            ButtonPressedCallback = new CallbackActionString(ProcessButtonPressedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("ButtonPressed"), ButtonPressedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessButtonPressedSignal(IntPtr text)
        {
            ButtonPressed?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(text)));
        }

        /// <summary>Event handler for the ButtonPressed signal</summary>
        public event EventHandler<SignalArgsString> ButtonPressed = null;

        private CallbackActionString ButtonPressedCallback;

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiMessageBox_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMessageBox_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiMessageBox_getText(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMessageBox_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiMessageBox_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMessageBox_addButton(IntPtr cPointer, IntPtr text);

        #endregion
    }
}
