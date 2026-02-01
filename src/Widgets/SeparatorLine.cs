// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// SeparatorLine widget
    /// </summary>
    public class SeparatorLine : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SeparatorLine()
            : base(tguiSeparatorLine_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SeparatorLine(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public SeparatorLine(SeparatorLine copy)
            : base(copy)
        {
        }

        public new SeparatorLineRenderer Renderer
        {
            get => new SeparatorLineRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new SeparatorLineRenderer SharedRenderer => new SeparatorLineRenderer(tguiWidget_getSharedRenderer(CPointer));

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSeparatorLine_create();

        #endregion
    }
}
