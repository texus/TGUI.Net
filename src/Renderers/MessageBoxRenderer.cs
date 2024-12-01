// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for MessageBox widgets
    /// </summary>
    public class MessageBoxRenderer : ChildWindowRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageBoxRenderer()
            : base(tguiMessageBoxRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal MessageBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public MessageBoxRenderer(MessageBoxRenderer copy)
            : base(tguiMessageBoxRenderer_copy(copy.CPointer))
        {
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiMessageBoxRenderer_getTextColor(CPointer));
            set => tguiMessageBoxRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public RendererData Button
        {
            get => new RendererData(tguiMessageBoxRenderer_getButton(CPointer));
            set => tguiMessageBoxRenderer_setButton(CPointer, value.CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMessageBoxRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMessageBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMessageBoxRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMessageBoxRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMessageBoxRenderer_getButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMessageBoxRenderer_setButton(IntPtr cPointer, IntPtr value);

        #endregion
    }
}
