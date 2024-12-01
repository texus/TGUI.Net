// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// HorizontalLayout widget
    /// </summary>
    public class HorizontalLayout : BoxLayoutRatios
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public HorizontalLayout()
            : base(tguiHorizontalLayout_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal HorizontalLayout(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public HorizontalLayout(HorizontalLayout copy)
            : base(copy)
        {
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiHorizontalLayout_create();

        #endregion
    }
}
