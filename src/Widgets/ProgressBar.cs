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

namespace TGUI
{
    public class ProgressBar : Widget
    {
        public enum Direction
        {
            LeftToRight,
            RightToLeft,
            TopToBottom,
            BottomToTop
        }


        public ProgressBar()
            : base(tguiProgressBar_create())
        {
        }

        protected internal ProgressBar(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public ProgressBar(ProgressBar copy)
            : base(copy)
        {
        }

        public new ProgressBarRenderer Renderer
        {
            get { return new ProgressBarRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        public new ProgressBarRenderer SharedRenderer
        {
            get { return new ProgressBarRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        public uint Minimum
        {
            get { return tguiProgressBar_getMinimum(CPointer); }
            set { tguiProgressBar_setMinimum(CPointer, value); }
        }

        public uint Maximum
        {
            get { return tguiProgressBar_getMaximum(CPointer); }
            set { tguiProgressBar_setMaximum(CPointer, value); }
        }

        public uint Value
        {
            get { return tguiProgressBar_getValue(CPointer); }
            set { tguiProgressBar_setValue(CPointer, value); }
        }

        public uint IncrementValue()
        {
            return tguiProgressBar_incrementValue(CPointer);
        }

        public string Text
        {
            get { return Util.GetStringFromC_UTF32(tguiProgressBar_getText(CPointer)); }
            set { tguiProgressBar_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        public uint TextSize
        {
            get { return tguiProgressBar_getTextSize(CPointer); }
            set { tguiProgressBar_setTextSize(CPointer, value); }
        }

        public Direction FillDirection
        {
            get { return tguiProgressBar_getFillDirection(CPointer); }
            set { tguiProgressBar_setFillDirection(CPointer, value); }
        }

        protected override void InitSignals()
        {
            base.InitSignals();

            ValueChangedCallback = new CallbackActionUInt(ProcessValueChangedSignal);
            if (tguiWidget_connectUInt(CPointer, Util.ConvertStringForC_ASCII("ValueChanged"), ValueChangedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            FullCallback = new CallbackAction(ProcessFullSignal);
            if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("Full"), FullCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessValueChangedSignal(uint value)
        {
            ValueChanged?.Invoke(this, new SignalArgsUInt(value));
        }

        private void ProcessFullSignal()
        {
            Full?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Event handler for the ValueChanged signal</summary>
        public event EventHandler<SignalArgsUInt> ValueChanged = null;

        /// <summary>Event handler for the Full signal</summary>
        public event EventHandler Full = null;

        private CallbackActionUInt ValueChangedCallback;
        private CallbackAction FullCallback;

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiProgressBar_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiProgressBar_setMinimum(IntPtr cPointer, uint minimum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiProgressBar_getMinimum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiProgressBar_setMaximum(IntPtr cPointer, uint maximum);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiProgressBar_getMaximum(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiProgressBar_setValue(IntPtr cPointer, uint value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiProgressBar_getValue(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiProgressBar_incrementValue(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiProgressBar_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiProgressBar_getText(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiProgressBar_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiProgressBar_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiProgressBar_setFillDirection(IntPtr cPointer, Direction fillDirection);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Direction tguiProgressBar_getFillDirection(IntPtr cPointer);

        #endregion
    }
}
