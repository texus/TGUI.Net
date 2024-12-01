// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Group widget
    /// </summary>
    public class Group : Container
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Group()
            : base(tguiGroup_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Group(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Group(Group copy)
            : base(copy)
        {
        }

        public new GroupRenderer Renderer
        {
            get => new GroupRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new GroupRenderer SharedRenderer => new GroupRenderer(tguiWidget_getSharedRenderer(CPointer));

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGroup_create();

        #endregion
    }
}
