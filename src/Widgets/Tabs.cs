// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Tabs widget
    /// </summary>
    public class Tabs : TabsBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Tabs()
            : base(tguiTabs_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Tabs(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Tabs(Tabs copy)
            : base(copy)
        {
        }

        public bool AutoSize
        {
            get => tguiTabs_getAutoSize(CPointer) != 0;
            set => tguiTabs_setAutoSize(CPointer, value ? (byte)1 : (byte)0);
        }

        public void SetTabHeight(float height)
        {
            tguiTabs_setTabHeight(CPointer, height);
        }

        public float MaximumTabWidth
        {
            get => tguiTabs_getMaximumTabWidth(CPointer);
            set => tguiTabs_setMaximumTabWidth(CPointer, value);
        }

        public float MinimumTabWidth
        {
            get => tguiTabs_getMinimumTabWidth(CPointer);
            set => tguiTabs_setMinimumTabWidth(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTabs_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTabs_getAutoSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setAutoSize(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setTabHeight(IntPtr cPointer, float height);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTabs_getMaximumTabWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setMaximumTabWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiTabs_getMinimumTabWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTabs_setMinimumTabWidth(IntPtr cPointer, float value);

        #endregion
    }
}
