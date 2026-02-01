// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public enum ProgressBarFillDirection
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop,
    }

    /// <summary>
    /// ProgressBar widget
    /// </summary>
    public class ProgressBar : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ProgressBar()
            : base(tguiProgressBar_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ProgressBar(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ProgressBar(ProgressBar copy)
            : base(copy)
        {
        }

        public new ProgressBarRenderer Renderer
        {
            get => new ProgressBarRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new ProgressBarRenderer SharedRenderer => new ProgressBarRenderer(tguiWidget_getSharedRenderer(CPointer));

        public int Minimum
        {
            get => (int)tguiProgressBar_getMinimum(CPointer);
            set => tguiProgressBar_setMinimum(CPointer, (uint)value);
        }

        public int Maximum
        {
            get => (int)tguiProgressBar_getMaximum(CPointer);
            set => tguiProgressBar_setMaximum(CPointer, (uint)value);
        }

        public int Value
        {
            get => (int)tguiProgressBar_getValue(CPointer);
            set => tguiProgressBar_setValue(CPointer, (uint)value);
        }

        public int IncrementValue()
        {
            return (int)tguiProgressBar_incrementValue(CPointer);
        }

        public string Text
        {
            get => Util.GetStringFromC_UTF32(tguiProgressBar_getText(CPointer));
            set => tguiProgressBar_setText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public ProgressBarFillDirection FillDirection
        {
            get => tguiProgressBar_getFillDirection(CPointer);
            set => tguiProgressBar_setFillDirection(CPointer, value);
        }

        public class ValueChangeEventArgs : EventArgs
        {
            public ValueChangeEventArgs(uint val)
            {
                Value = val;
            }
            public uint Value { get; }
        }
        public event EventHandler<ValueChangeEventArgs> OnValueChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackUInt func = (uint val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ValueChangeEventArgs(val));
                };
                uint id = tguiWidget_signalUIntConnect(CPointer, Util.ConvertStringForC_UTF32("ValueChanged"), func);
                ConnectEventHandler(id, "ValueChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("ValueChanged", value);
            }
        }

        public event EventHandler OnFull
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("Full"), func);
                ConnectEventHandler(id, "Full", value, func);
            }
            remove
            {
                DisconnectEventHandler("Full", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiProgressBar_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiProgressBar_getMinimum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBar_setMinimum(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiProgressBar_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBar_setMaximum(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiProgressBar_getValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBar_setValue(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiProgressBar_incrementValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiProgressBar_getText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBar_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ProgressBarFillDirection tguiProgressBar_getFillDirection(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiProgressBar_setFillDirection(IntPtr cPointer, ProgressBarFillDirection value);

        #endregion
    }
}
