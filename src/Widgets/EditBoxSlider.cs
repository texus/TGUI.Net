// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// EditBoxSlider widget
    /// </summary>
    public class EditBoxSlider : SubwidgetContainer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EditBoxSlider()
            : base(tguiEditBoxSlider_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal EditBoxSlider(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public EditBoxSlider(EditBoxSlider copy)
            : base(copy)
        {
        }

        public EditBoxRenderer EditBoxRenderer
        {
            get => new EditBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  EditBoxRenderer EditBoxSharedRenderer => new EditBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        public SliderRenderer SliderRenderer
        {
            get => new SliderRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  SliderRenderer SliderSharedRenderer => new SliderRenderer(tguiWidget_getSharedRenderer(CPointer));

        public bool SetValue(float value)
        {
            return tguiEditBoxSlider_setValue(CPointer, value) != 0;
        }

        public float GetValue()
        {
            return tguiEditBoxSlider_getValue(CPointer);
        }

        public float Minimum
        {
            get => tguiEditBoxSlider_getMinimum(CPointer);
            set => tguiEditBoxSlider_setMinimum(CPointer, value);
        }

        public float Maximum
        {
            get => tguiEditBoxSlider_getMaximum(CPointer);
            set => tguiEditBoxSlider_setMaximum(CPointer, value);
        }

        public float Step
        {
            get => tguiEditBoxSlider_getStep(CPointer);
            set => tguiEditBoxSlider_setStep(CPointer, value);
        }

        public int DecimalPlaces
        {
            get => (int)tguiEditBoxSlider_getDecimalPlaces(CPointer);
            set => tguiEditBoxSlider_setDecimalPlaces(CPointer, (uint)value);
        }

        public HorizontalAlignment TextAlignment
        {
            get => tguiEditBoxSlider_getTextAlignment(CPointer);
            set => tguiEditBoxSlider_setTextAlignment(CPointer, value);
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

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiEditBoxSlider_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiEditBoxSlider_setValue(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiEditBoxSlider_getValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiEditBoxSlider_getMinimum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxSlider_setMinimum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiEditBoxSlider_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxSlider_setMaximum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiEditBoxSlider_getStep(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxSlider_setStep(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiEditBoxSlider_getDecimalPlaces(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxSlider_setDecimalPlaces(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern HorizontalAlignment tguiEditBoxSlider_getTextAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiEditBoxSlider_setTextAlignment(IntPtr cPointer, HorizontalAlignment value);

        #endregion
    }
}
