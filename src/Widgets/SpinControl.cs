// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// SpinControl widget
    /// </summary>
    public class SpinControl : SubwidgetContainer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SpinControl()
            : base(tguiSpinControl_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SpinControl(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public SpinControl(SpinControl copy)
            : base(copy)
        {
        }

        public float Value
        {
            get => tguiSpinControl_getValue(CPointer);
            set => tguiSpinControl_setValue(CPointer, value);
        }

        public float Step
        {
            get => tguiSpinControl_getStep(CPointer);
            set => tguiSpinControl_setStep(CPointer, value);
        }

        public SpinButtonRenderer SpinButtonRenderer
        {
            get => new SpinButtonRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public SpinButtonRenderer SpinButtonSharedRenderer => new SpinButtonRenderer(tguiWidget_getSharedRenderer(CPointer));

        public EditBoxRenderer SpinTextRenderer
        {
            get => new EditBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public EditBoxRenderer SpinTextSharedRenderer => new EditBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        public float Minimum
        {
            get => tguiSpinControl_getMinimum(CPointer);
            set => tguiSpinControl_setMinimum(CPointer, value);
        }

        public float Maximum
        {
            get => tguiSpinControl_getMaximum(CPointer);
            set => tguiSpinControl_setMaximum(CPointer, value);
        }

        public int DecimalPlaces
        {
            get => (int)tguiSpinControl_getDecimalPlaces(CPointer);
            set => tguiSpinControl_setDecimalPlaces(CPointer, (uint)value);
        }

        public float SpinButtonWidth
        {
            get => tguiSpinControl_getSpinButtonWidth(CPointer);
            set => tguiSpinControl_setSpinButtonWidth(CPointer, value);
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
        private static extern float tguiSpinControl_getValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinControl_setValue(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinControl_getStep(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinControl_setStep(IntPtr cPointer, float value);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSpinControl_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinControl_getMinimum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinControl_setMinimum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinControl_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinControl_setMaximum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiSpinControl_getDecimalPlaces(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinControl_setDecimalPlaces(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSpinControl_getSpinButtonWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSpinControl_setSpinButtonWidth(IntPtr cPointer, float value);

        #endregion
    }
}
