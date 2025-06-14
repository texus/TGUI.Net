// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// RadioButton widget
    /// </summary>
    public class RadioButton : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RadioButton()
            : base(tguiRadioButton_create())
        {
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

        public new RadioButtonRenderer Renderer
        {
            get => new RadioButtonRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new RadioButtonRenderer SharedRenderer => new RadioButtonRenderer(tguiWidget_getSharedRenderer(CPointer));

        public bool Checked
        {
            get => tguiRadioButton_isChecked(CPointer) != 0;
            set => tguiRadioButton_setChecked(CPointer, value ? (byte)1 : (byte)0);
        }

        public string Text
        {
            get => Util.GetStringFromC_UTF32(tguiRadioButton_getText(CPointer));
            set => tguiRadioButton_setText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public bool TextClickable
        {
            get => tguiRadioButton_isTextClickable(CPointer) != 0;
            set => tguiRadioButton_setTextClickable(CPointer, value ? (byte)1 : (byte)0);
        }

        public float MaxWidth
        {
            get => tguiRadioButton_getMaxWidth(CPointer);
            set => tguiRadioButton_setMaxWidth(CPointer, value);
        }

        public class CheckEventArgs : EventArgs
        {
            public CheckEventArgs(bool isChecked)
            {
                Checked = isChecked;
            }
            public bool Checked { get; }
        }
        public event EventHandler<CheckEventArgs> OnCheck
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackBool func = (byte val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new CheckEventArgs(val != 0));
                };
                uint id = tguiWidget_signalBoolConnect(CPointer, Util.ConvertStringForC_UTF32("Checked"), func);
                ConnectEventHandler(id, "Checked", value, func);
            }
            remove
            {
                DisconnectEventHandler("Checked", value);
            }
        }

        public class UncheckEventArgs : EventArgs
        {
            public UncheckEventArgs(bool isChecked)
            {
                Checked = isChecked;
            }
            public bool Checked { get; }
        }
        public event EventHandler<UncheckEventArgs> OnUncheck
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackBool func = (byte val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new UncheckEventArgs(val != 0));
                };
                uint id = tguiWidget_signalBoolConnect(CPointer, Util.ConvertStringForC_UTF32("Unchecked"), func);
                ConnectEventHandler(id, "Unchecked", value, func);
            }
            remove
            {
                DisconnectEventHandler("Unchecked", value);
            }
        }

        public class ChangeEventArgs : EventArgs
        {
            public ChangeEventArgs(bool isChecked)
            {
                Checked = isChecked;
            }
            public bool Checked { get; }
        }
        public event EventHandler<ChangeEventArgs> OnChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackBool func = (byte val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ChangeEventArgs(val != 0));
                };
                uint id = tguiWidget_signalBoolConnect(CPointer, Util.ConvertStringForC_UTF32("Changed"), func);
                ConnectEventHandler(id, "Changed", value, func);
            }
            remove
            {
                DisconnectEventHandler("Changed", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButton_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiRadioButton_isChecked(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButton_setChecked(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButton_getText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButton_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiRadioButton_isTextClickable(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButton_setTextClickable(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiRadioButton_getMaxWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButton_setMaxWidth(IntPtr cPointer, float value);

        #endregion
    }
}
