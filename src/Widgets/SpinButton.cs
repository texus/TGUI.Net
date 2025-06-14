// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// SpinButton widget
    /// </summary>
    public class SpinButton : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SpinButton()
            : base(tguiSpinButton_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SpinButton(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public SpinButton(SpinButton copy)
            : base(copy)
        {
        }

        public float Value
        {
            get => tguiSpinButton_getValue(CPointer);
            set => tguiSpinButton_setValue(CPointer, value);
        }

        public float Step
        {
            get => tguiSpinButton_getStep(CPointer);
            set => tguiSpinButton_setStep(CPointer, value);
        }

        public new SpinButtonRenderer Renderer
        {
            get => new SpinButtonRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new SpinButtonRenderer SharedRenderer => new SpinButtonRenderer(tguiWidget_getSharedRenderer(CPointer));

        public float Minimum
        {
            get => tguiSpinButton_getMinimum(CPointer);
            set => tguiSpinButton_setMinimum(CPointer, value);
        }

        public float Maximum
        {
            get => tguiSpinButton_getMaximum(CPointer);
            set => tguiSpinButton_setMaximum(CPointer, value);
        }

        public Orientation Orientation
        {
            get => tguiSpinButton_getOrientation(CPointer);
            set => tguiSpinButton_setOrientation(CPointer, value);
        }

        public class ValueChangeEventArgs : EventArgs
        {
            public ValueChangeEventArgs(float val)
            {
                Value = val;
            }
            public float Value { get; }
        }
        public event EventHandler<ValueChangeEventArgs> OnValueChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackFloat func = (float val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ValueChangeEventArgs(val));
                };
                uint id = tguiWidget_signalFloatConnect(CPointer, Util.ConvertStringForC_UTF32("ValueChanged"), func);
                ConnectEventHandler(id, "ValueChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("ValueChanged", value);
            }
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinButton_getValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButton_setValue(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinButton_getStep(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButton_setStep(IntPtr cPointer, float value);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinButton_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinButton_getMinimum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButton_setMinimum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinButton_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButton_setMaximum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Orientation tguiSpinButton_getOrientation(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinButton_setOrientation(IntPtr cPointer, Orientation value);

        #endregion
    }
}
