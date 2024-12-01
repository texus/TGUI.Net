// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// MessageBox widget
    /// </summary>
    public class MessageBox : ChildWindow
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageBox()
            : base(tguiMessageBox_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal MessageBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public MessageBox(MessageBox copy)
            : base(copy)
        {
        }

        public new MessageBoxRenderer Renderer
        {
            get => new MessageBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new MessageBoxRenderer SharedRenderer => new MessageBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        public string Text
        {
            get => Util.GetStringFromC_UTF32(tguiMessageBox_getText(CPointer));
            set => tguiMessageBox_setText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public void AddButton(string text)
        {
            tguiMessageBox_addButton(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        public void ChangeButtons(ReadOnlySpan<string> buttonCaptions)
        {
            IntPtr[] buttonCaptionsForC = new IntPtr[buttonCaptions.Length];
            for (int i = 0; i < buttonCaptions.Length; ++i)
                buttonCaptionsForC[i] = Util.ConvertStringForC_UTF32(buttonCaptions[i]);

            tguiMessageBox_changeButtons(CPointer, buttonCaptionsForC, (UIntPtr)buttonCaptionsForC.Length);
        }

        public IReadOnlyList<string> GetButtons()
        {
            unsafe
            {
                IntPtr* returnStringsC = tguiMessageBox_getButtons(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public HorizontalAlignment LabelAlignment
        {
            get => tguiMessageBox_getLabelAlignment(CPointer);
            set => tguiMessageBox_setLabelAlignment(CPointer, value);
        }

        public HorizontalAlignment ButtonAlignment
        {
            get => tguiMessageBox_getButtonAlignment(CPointer);
            set => tguiMessageBox_setButtonAlignment(CPointer, value);
        }

        public class ButtonPressEventArgs : EventArgs
        {
            public ButtonPressEventArgs(string buttonText)
            {
                ButtonText = buttonText;
            }
            public string ButtonText { get; }
        }
        public event EventHandler<ButtonPressEventArgs> OnButtonPress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ButtonPressEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("ButtonPressed"), func);
                ConnectEventHandler(id, "ButtonPressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("ButtonPressed", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMessageBox_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMessageBox_getText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMessageBox_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMessageBox_addButton(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMessageBox_changeButtons(IntPtr cPointer, IntPtr[] buttonCaptions, UIntPtr buttonCaptionsLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiMessageBox_getButtons(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern HorizontalAlignment tguiMessageBox_getLabelAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMessageBox_setLabelAlignment(IntPtr cPointer, HorizontalAlignment value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern HorizontalAlignment tguiMessageBox_getButtonAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMessageBox_setButtonAlignment(IntPtr cPointer, HorizontalAlignment value);

        #endregion
    }
}
