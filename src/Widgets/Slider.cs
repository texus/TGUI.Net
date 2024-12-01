// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Slider widget
    /// </summary>
    public class Slider : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Slider()
            : base(tguiSlider_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Slider(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Slider(Slider copy)
            : base(copy)
        {
        }

        public new SliderRenderer Renderer
        {
            get => new SliderRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new SliderRenderer SharedRenderer => new SliderRenderer(tguiWidget_getSharedRenderer(CPointer));

        public float Minimum
        {
            get => tguiSlider_getMinimum(CPointer);
            set => tguiSlider_setMinimum(CPointer, value);
        }

        public float Maximum
        {
            get => tguiSlider_getMaximum(CPointer);
            set => tguiSlider_setMaximum(CPointer, value);
        }

        public float Value
        {
            get => tguiSlider_getValue(CPointer);
            set => tguiSlider_setValue(CPointer, value);
        }

        public float Step
        {
            get => tguiSlider_getStep(CPointer);
            set => tguiSlider_setStep(CPointer, value);
        }

        public Orientation Orientation
        {
            get => tguiSlider_getOrientation(CPointer);
            set => tguiSlider_setOrientation(CPointer, value);
        }

        public bool InvertedDirection
        {
            get => tguiSlider_getInvertedDirection(CPointer) != 0;
            set => tguiSlider_setInvertedDirection(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool ChangeValueOnScroll
        {
            get => tguiSlider_getChangeValueOnScroll(CPointer) != 0;
            set => tguiSlider_setChangeValueOnScroll(CPointer, value ? (byte)1 : (byte)0);
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
        private static extern IntPtr tguiSlider_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSlider_getMinimum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSlider_setMinimum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSlider_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSlider_setMaximum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSlider_getValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSlider_setValue(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSlider_getStep(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSlider_setStep(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Orientation tguiSlider_getOrientation(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSlider_setOrientation(IntPtr cPointer, Orientation value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiSlider_getInvertedDirection(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSlider_setInvertedDirection(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiSlider_getChangeValueOnScroll(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSlider_setChangeValueOnScroll(IntPtr cPointer, byte value);

        #endregion
    }
}
