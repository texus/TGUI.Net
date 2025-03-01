// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// EditBox widget
    /// </summary>
    public class EditBox : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EditBox()
            : base(tguiEditBox_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal EditBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public EditBox(EditBox copy)
            : base(copy)
        {
        }

        public new EditBoxRenderer Renderer
        {
            get => new EditBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new EditBoxRenderer SharedRenderer => new EditBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        public string Text
        {
            get => Util.GetStringFromC_UTF32(tguiEditBox_getText(CPointer));
            set => tguiEditBox_setText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public string DefaultText
        {
            get => Util.GetStringFromC_UTF32(tguiEditBox_getDefaultText(CPointer));
            set => tguiEditBox_setDefaultText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public string PasswordCharacter
        {
            get => Char.ConvertFromUtf32((int)tguiEditBox_getPasswordCharacter(CPointer));
            set => tguiEditBox_setPasswordCharacter(CPointer, (uint)Char.ConvertToUtf32(value, 0));
        }

        public int MaximumCharacters
        {
            get => (int)tguiEditBox_getMaximumCharacters(CPointer);
            set => tguiEditBox_setMaximumCharacters(CPointer, (uint)value);
        }

        public HorizontalAlignment Alignment
        {
            get => tguiEditBox_getAlignment(CPointer);
            set => tguiEditBox_setAlignment(CPointer, value);
        }

        public bool TextWidthLimited
        {
            get => tguiEditBox_isTextWidthLimited(CPointer) != 0;
            set => tguiEditBox_setTextWidthLimited(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool ReadOnly
        {
            get => tguiEditBox_isReadOnly(CPointer) != 0;
            set => tguiEditBox_setReadOnly(CPointer, value ? (byte)1 : (byte)0);
        }

        public int CaretPosition
        {
            get => (int)tguiEditBox_getCaretPosition(CPointer);
            set => tguiEditBox_setCaretPosition(CPointer, (UIntPtr)value);
        }

        public string Suffix
        {
            get => Util.GetStringFromC_UTF32(tguiEditBox_getSuffix(CPointer));
            set => tguiEditBox_setSuffix(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public bool SetInputValidator(string regex = ".*")
        {
            return tguiEditBox_setInputValidator(CPointer, Util.ConvertStringForC_UTF32(regex)) != 0;
        }

        public string GetInputValidator()
        {
            return Util.GetStringFromC_UTF32(tguiEditBox_getInputValidator(CPointer));
        }

        public void SelectText(int start, int length)
        {
            tguiEditBox_selectText(CPointer, (UIntPtr)start, (UIntPtr)length);
        }

        public string GetSelectedText()
        {
            return Util.GetStringFromC_UTF32(tguiEditBox_getSelectedText(CPointer));
        }

        public class TextChangeEventArgs : EventArgs
        {
            public TextChangeEventArgs(string text)
            {
                Text = text;
            }
            public string Text { get; }
        }
        public event EventHandler<TextChangeEventArgs> OnTextChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new TextChangeEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("TextChanged"), func);
                ConnectEventHandler(id, "TextChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("TextChanged", value);
            }
        }

        public class ReturnKeyPressEventArgs : EventArgs
        {
            public ReturnKeyPressEventArgs(string text)
            {
                Text = text;
            }
            public string Text { get; }
        }
        public event EventHandler<ReturnKeyPressEventArgs> OnReturnKeyPress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ReturnKeyPressEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("ReturnKeyPressed"), func);
                ConnectEventHandler(id, "ReturnKeyPressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("ReturnKeyPressed", value);
            }
        }

        public class ReturnOrUnfocusEventArgs : EventArgs
        {
            public ReturnOrUnfocusEventArgs(string text)
            {
                Text = text;
            }
            public string Text { get; }
        }
        public event EventHandler<ReturnOrUnfocusEventArgs> OnReturnOrUnfocus
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ReturnOrUnfocusEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("ReturnOrUnfocused"), func);
                ConnectEventHandler(id, "ReturnOrUnfocused", value, func);
            }
            remove
            {
                DisconnectEventHandler("ReturnOrUnfocused", value);
            }
        }

        public class CaretPositionChangeEventArgs : EventArgs
        {
            public CaretPositionChangeEventArgs(UIntPtr caretPos)
            {
                CaretPos = caretPos;
            }
            public UIntPtr CaretPos { get; }
        }
        public event EventHandler<CaretPositionChangeEventArgs> OnCaretPositionChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackNUInt func = (UIntPtr val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new CaretPositionChangeEventArgs(val));
                };
                uint id = tguiWidget_signalSizeTConnect(CPointer, Util.ConvertStringForC_UTF32("CaretPositionChanged"), func);
                ConnectEventHandler(id, "CaretPositionChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("CaretPositionChanged", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBox_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBox_getText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBox_getDefaultText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setDefaultText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiEditBox_getPasswordCharacter(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setPasswordCharacter(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiEditBox_getMaximumCharacters(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setMaximumCharacters(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern HorizontalAlignment tguiEditBox_getAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setAlignment(IntPtr cPointer, HorizontalAlignment value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiEditBox_isTextWidthLimited(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setTextWidthLimited(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiEditBox_isReadOnly(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setReadOnly(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiEditBox_getCaretPosition(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setCaretPosition(IntPtr cPointer, UIntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBox_getSuffix(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_setSuffix(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiEditBox_setInputValidator(IntPtr cPointer, IntPtr regex);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBox_getInputValidator(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBox_selectText(IntPtr cPointer, UIntPtr start, UIntPtr length);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBox_getSelectedText(IntPtr cPointer);

        #endregion
    }
}
