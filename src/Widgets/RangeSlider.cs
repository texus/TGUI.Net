// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// RangeSlider widget
    /// </summary>
    public class RangeSlider : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RangeSlider()
            : base(tguiRangeSlider_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal RangeSlider(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public RangeSlider(RangeSlider copy)
            : base(copy)
        {
        }

        public new RangeSliderRenderer Renderer
        {
            get => new RangeSliderRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new RangeSliderRenderer SharedRenderer => new RangeSliderRenderer(tguiWidget_getSharedRenderer(CPointer));

        public float Minimum
        {
            get => tguiRangeSlider_getMinimum(CPointer);
            set => tguiRangeSlider_setMinimum(CPointer, value);
        }

        public float Maximum
        {
            get => tguiRangeSlider_getMaximum(CPointer);
            set => tguiRangeSlider_setMaximum(CPointer, value);
        }

        public float SelectionStart
        {
            get => tguiRangeSlider_getSelectionStart(CPointer);
            set => tguiRangeSlider_setSelectionStart(CPointer, value);
        }

        public float SelectionEnd
        {
            get => tguiRangeSlider_getSelectionEnd(CPointer);
            set => tguiRangeSlider_setSelectionEnd(CPointer, value);
        }

        public float Step
        {
            get => tguiRangeSlider_getStep(CPointer);
            set => tguiRangeSlider_setStep(CPointer, value);
        }

        public class RangeChangeEventArgs : EventArgs
        {
            public RangeChangeEventArgs(float start, float end)
            {
                Start = start;
                End = end;
            }
            public float Start { get; }
            public float End { get; }
        }
        public event EventHandler<RangeChangeEventArgs> OnRangeChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackRange func = (float val1, float val2) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new RangeChangeEventArgs(val1, val2));
                };
                uint id = tguiWidget_signalRangeConnect(CPointer, Util.ConvertStringForC_UTF32("RangeChanged"), func);
                ConnectEventHandler(id, "RangeChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("RangeChanged", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRangeSlider_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiRangeSlider_getMinimum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSlider_setMinimum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiRangeSlider_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSlider_setMaximum(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiRangeSlider_getSelectionStart(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSlider_setSelectionStart(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiRangeSlider_getSelectionEnd(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSlider_setSelectionEnd(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiRangeSlider_getStep(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRangeSlider_setStep(IntPtr cPointer, float value);

        #endregion
    }
}
