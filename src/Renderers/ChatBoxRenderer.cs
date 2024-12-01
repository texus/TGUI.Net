// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for ChatBox widgets
    /// </summary>
    public class ChatBoxRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatBoxRenderer()
            : base(tguiChatBoxRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ChatBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public ChatBoxRenderer(ChatBoxRenderer copy)
            : base(tguiChatBoxRenderer_copy(copy.CPointer))
        {
        }

        public Outline Borders
        {
            get => new Outline(tguiChatBoxRenderer_getBorders(CPointer));
            set => tguiChatBoxRenderer_setBorders(CPointer, value.CPointer);
        }

        public Outline Padding
        {
            get => new Outline(tguiChatBoxRenderer_getPadding(CPointer));
            set => tguiChatBoxRenderer_setPadding(CPointer, value.CPointer);
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiChatBoxRenderer_getBackgroundColor(CPointer));
            set => tguiChatBoxRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? BorderColor
        {
            get => Util.GetColorFromC(tguiChatBoxRenderer_getBorderColor(CPointer));
            set => tguiChatBoxRenderer_setBorderColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiChatBoxRenderer_getTextureBackground(CPointer));
            set => tguiChatBoxRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        public RendererData Scrollbar
        {
            get => new RendererData(tguiChatBoxRenderer_getScrollbar(CPointer));
            set => tguiChatBoxRenderer_setScrollbar(CPointer, value.CPointer);
        }

        public float ScrollbarWidth
        {
            get => tguiChatBoxRenderer_getScrollbarWidth(CPointer);
            set => tguiChatBoxRenderer_setScrollbarWidth(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChatBoxRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChatBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChatBoxRenderer_getBorders(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBoxRenderer_setBorders(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChatBoxRenderer_getPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBoxRenderer_setPadding(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChatBoxRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBoxRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChatBoxRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBoxRenderer_setBorderColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChatBoxRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBoxRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChatBoxRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBoxRenderer_setScrollbar(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiChatBoxRenderer_getScrollbarWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBoxRenderer_setScrollbarWidth(IntPtr cPointer, float value);

        #endregion
    }
}
