// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Knob widget
    /// </summary>
    public class Knob : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Knob()
            : base(tguiKnob_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Knob(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Knob(Knob copy)
            : base(copy)
        {
        }

        public new KnobRenderer Renderer
        {
            get => new KnobRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new KnobRenderer SharedRenderer => new KnobRenderer(tguiWidget_getSharedRenderer(CPointer));

        public float StartRotation
        {
            get => tguiKnob_getStartRotation(CPointer);
            set => tguiKnob_setStartRotation(CPointer, value);
        }

        public float EndRotation
        {
            get => tguiKnob_getEndRotation(CPointer);
            set => tguiKnob_setEndRotation(CPointer, value);
        }

        public float Minimum
        {
            get => tguiKnob_getMinimum(CPointer);
            set => tguiKnob_setMinimum(CPointer, value);
        }

        public float Maximum
        {
            get => tguiKnob_getMaximum(CPointer);
            set => tguiKnob_setMaximum(CPointer, value);
        }

        public float Value
        {
            get => tguiKnob_getValue(CPointer);
            set => tguiKnob_setValue(CPointer, value);
        }

        public bool ClockwiseTurning
        {
            get => tguiKnob_getClockwiseTurning(CPointer) != 0;
            set => tguiKnob_setClockwiseTurning(CPointer, value ? (byte)1 : (byte)0);
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
        private static extern IntPtr tguiKnob_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiKnob_getStartRotation(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiKnob_setStartRotation(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiKnob_getEndRotation(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiKnob_setEndRotation(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiKnob_getMinimum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiKnob_setMinimum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiKnob_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiKnob_setMaximum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiKnob_getValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiKnob_setValue(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiKnob_getClockwiseTurning(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiKnob_setClockwiseTurning(IntPtr cPointer, byte value);

        #endregion
    }
}
