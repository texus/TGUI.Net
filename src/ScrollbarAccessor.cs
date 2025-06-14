// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class ScrollbarAccessor : ObjectBase
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ScrollbarAccessor(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiScrollbarAccessor_destroy(CPointer);
        }

        public int Value
        {
            get => (int)tguiScrollbarAccessor_getValue(CPointer);
            set => tguiScrollbarAccessor_setValue(CPointer, (uint)value);
        }

        public int ScrollAmount
        {
            get => (int)tguiScrollbarAccessor_getScrollAmount(CPointer);
            set => tguiScrollbarAccessor_setScrollAmount(CPointer, (uint)value);
        }

        public ScrollbarPolicy Policy
        {
            get => tguiScrollbarAccessor_getPolicy(CPointer);
            set => tguiScrollbarAccessor_setPolicy(CPointer, value);
        }

        public int Maximum
        {
            get => (int)tguiScrollbarAccessor_getMaximum(CPointer);
        }

        public int ViewportSize
        {
            get => (int)tguiScrollbarAccessor_getViewportSize(CPointer);
        }

        public int MaxValue
        {
            get => (int)tguiScrollbarAccessor_getMaxValue(CPointer);
        }

        public bool Shown
        {
            get => tguiScrollbarAccessor_isShown(CPointer) != 0;
        }

        public float Width
        {
            get => tguiScrollbarAccessor_getWidth(CPointer);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarAccessor_destroy(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbarAccessor_getValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarAccessor_setValue(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbarAccessor_getScrollAmount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarAccessor_setScrollAmount(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ScrollbarPolicy tguiScrollbarAccessor_getPolicy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiScrollbarAccessor_setPolicy(IntPtr cPointer, ScrollbarPolicy value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbarAccessor_getMaximum(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbarAccessor_getViewportSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiScrollbarAccessor_getMaxValue(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiScrollbarAccessor_isShown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiScrollbarAccessor_getWidth(IntPtr cPointer);

        #endregion
    }
}
