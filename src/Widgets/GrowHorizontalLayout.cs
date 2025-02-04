// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// GrowHorizontalLayout widget
    /// </summary>
    public class GrowHorizontalLayout : BoxLayout
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GrowHorizontalLayout()
            : base(tguiGrowHorizontalLayout_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal GrowHorizontalLayout(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public GrowHorizontalLayout(GrowHorizontalLayout copy)
            : base(copy)
        {
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGrowHorizontalLayout_create();

        #endregion
    }
}
