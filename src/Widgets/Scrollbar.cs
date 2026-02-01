// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public enum ScrollbarPolicy
    {
        Automatic,
        Always,
        Never,
    }

    /// <summary>
    /// Scrollbar widget
    /// </summary>
    public class Scrollbar : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Scrollbar()
            : base(tguiScrollbar_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Scrollbar(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Scrollbar(Scrollbar copy)
            : base(copy)
        {
        }

        public new ScrollbarRenderer Renderer
        {
            get => new ScrollbarRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new ScrollbarRenderer SharedRenderer => new ScrollbarRenderer(tguiWidget_getSharedRenderer(CPointer));

        public int ViewportSize
        {
            get => (int)tguiScrollbar_getViewportSize(CPointer);
            set => tguiScrollbar_setViewportSize(CPointer, (uint)value);
        }

        public int Maximum
        {
            get => (int)tguiScrollbar_getMaximum(CPointer);
            set => tguiScrollbar_setMaximum(CPointer, (uint)value);
        }

        public int Value
        {
            get => (int)tguiScrollbar_getValue(CPointer);
            set => tguiScrollbar_setValue(CPointer, (uint)value);
        }

        public int ScrollAmount
        {
            get => (int)tguiScrollbar_getScrollAmount(CPointer);
            set => tguiScrollbar_setScrollAmount(CPointer, (uint)value);
        }

        public ScrollbarPolicy Policy
        {
            get => tguiScrollbar_getPolicy(CPointer);
            set => tguiScrollbar_setPolicy(CPointer, value);
        }

        public Orientation Orientation
        {
            get => tguiScrollbar_getOrientation(CPointer);
            set => tguiScrollbar_setOrientation(CPointer, value);
        }

        public bool IsShown()
        {
            return tguiScrollbar_isShown(CPointer) != 0;
        }

        public int GetMaxValue()
        {
            return (int)tguiScrollbar_getMaxValue(CPointer);
        }

        public float GetDefaultWidth()
        {
            return tguiScrollbar_getDefaultWidth(CPointer);
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

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbar_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbar_getViewportSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbar_setViewportSize(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbar_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbar_setMaximum(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbar_getValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbar_setValue(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbar_getScrollAmount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbar_setScrollAmount(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ScrollbarPolicy tguiScrollbar_getPolicy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbar_setPolicy(IntPtr cPointer, ScrollbarPolicy value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Orientation tguiScrollbar_getOrientation(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbar_setOrientation(IntPtr cPointer, Orientation value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiScrollbar_isShown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbar_getMaxValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiScrollbar_getDefaultWidth(IntPtr cPointer);

        #endregion
    }
}
