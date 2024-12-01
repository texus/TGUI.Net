// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for FileDialog widgets
    /// </summary>
    public class FileDialogRenderer : ChildWindowRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FileDialogRenderer()
            : base(tguiFileDialogRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal FileDialogRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public FileDialogRenderer(FileDialogRenderer copy)
            : base(tguiFileDialogRenderer_copy(copy.CPointer))
        {
        }

        public RendererData ListView
        {
            get => new RendererData(tguiFileDialogRenderer_getListView(CPointer));
            set => tguiFileDialogRenderer_setListView(CPointer, value.CPointer);
        }

        public RendererData EditBox
        {
            get => new RendererData(tguiFileDialogRenderer_getEditBox(CPointer));
            set => tguiFileDialogRenderer_setEditBox(CPointer, value.CPointer);
        }

        public RendererData FilenameLabel
        {
            get => new RendererData(tguiFileDialogRenderer_getFilenameLabel(CPointer));
            set => tguiFileDialogRenderer_setFilenameLabel(CPointer, value.CPointer);
        }

        public RendererData FileTypeComboBox
        {
            get => new RendererData(tguiFileDialogRenderer_getFileTypeComboBox(CPointer));
            set => tguiFileDialogRenderer_setFileTypeComboBox(CPointer, value.CPointer);
        }

        public RendererData Button
        {
            get => new RendererData(tguiFileDialogRenderer_getButton(CPointer));
            set => tguiFileDialogRenderer_setButton(CPointer, value.CPointer);
        }

        public RendererData BackButton
        {
            get => new RendererData(tguiFileDialogRenderer_getBackButton(CPointer));
            set => tguiFileDialogRenderer_setBackButton(CPointer, value.CPointer);
        }

        public RendererData ForwardButton
        {
            get => new RendererData(tguiFileDialogRenderer_getForwardButton(CPointer));
            set => tguiFileDialogRenderer_setForwardButton(CPointer, value.CPointer);
        }

        public RendererData UpButton
        {
            get => new RendererData(tguiFileDialogRenderer_getUpButton(CPointer));
            set => tguiFileDialogRenderer_setUpButton(CPointer, value.CPointer);
        }

        public bool ArrowsOnNavigationButtonsVisible
        {
            get => tguiFileDialogRenderer_getArrowsOnNavigationButtonsVisible(CPointer) != 0;
            set => tguiFileDialogRenderer_setArrowsOnNavigationButtonsVisible(CPointer, value ? (byte)1 : (byte)0);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_getListView(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setListView(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_getEditBox(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setEditBox(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_getFilenameLabel(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setFilenameLabel(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_getFileTypeComboBox(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setFileTypeComboBox(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_getButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setButton(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_getBackButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setBackButton(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_getForwardButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setForwardButton(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogRenderer_getUpButton(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setUpButton(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiFileDialogRenderer_getArrowsOnNavigationButtonsVisible(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogRenderer_setArrowsOnNavigationButtonsVisible(IntPtr cPointer, byte value);

        #endregion
    }
}
