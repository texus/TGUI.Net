// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// GrowVerticalLayout widget
    /// </summary>
    public class GrowVerticalLayout : BoxLayout
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GrowVerticalLayout()
            : base(tguiGrowVerticalLayout_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal GrowVerticalLayout(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public GrowVerticalLayout(GrowVerticalLayout copy)
            : base(copy)
        {
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGrowVerticalLayout_create();

        #endregion
    }
}
