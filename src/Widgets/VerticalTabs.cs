// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// VerticalTabs widget
    /// </summary>
    public class VerticalTabs : TabsBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public VerticalTabs()
            : base(tguiVerticalTabs_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal VerticalTabs(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public VerticalTabs(VerticalTabs copy)
            : base(copy)
        {
        }

        public void SetTabWidth(float width)
        {
            tguiVerticalTabs_setTabWidth(CPointer, width);
        }

        public float TabHeight
        {
            get => tguiVerticalTabs_getTabHeight(CPointer);
            set => tguiVerticalTabs_setTabHeight(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiVerticalTabs_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiVerticalTabs_setTabWidth(IntPtr cPointer, float width);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiVerticalTabs_getTabHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiVerticalTabs_setTabHeight(IntPtr cPointer, float value);

        #endregion
    }
}
