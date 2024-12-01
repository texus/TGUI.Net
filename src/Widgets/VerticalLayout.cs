// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// VerticalLayout widget
    /// </summary>
    public class VerticalLayout : BoxLayoutRatios
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public VerticalLayout()
            : base(tguiVerticalLayout_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal VerticalLayout(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public VerticalLayout(VerticalLayout copy)
            : base(copy)
        {
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiVerticalLayout_create();

        #endregion
    }
}
