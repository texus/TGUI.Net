// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for PanelListBox widgets
    /// </summary>
    public class PanelListBoxRenderer : ScrollablePanelRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PanelListBoxRenderer()
            : base(tguiPanelListBoxRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal PanelListBoxRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public PanelListBoxRenderer(PanelListBoxRenderer copy)
            : base(tguiPanelListBoxRenderer_copy(copy.CPointer))
        {
        }

        public Color? ItemsBackgroundColor
        {
            get => Util.GetColorFromC(tguiPanelListBoxRenderer_getItemsBackgroundColor(CPointer));
            set => tguiPanelListBoxRenderer_setItemsBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? ItemsBackgroundColorHover
        {
            get => Util.GetColorFromC(tguiPanelListBoxRenderer_getItemsBackgroundColorHover(CPointer));
            set => tguiPanelListBoxRenderer_setItemsBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedItemsBackgroundColor
        {
            get => Util.GetColorFromC(tguiPanelListBoxRenderer_getSelectedItemsBackgroundColor(CPointer));
            set => tguiPanelListBoxRenderer_setSelectedItemsBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedItemsBackgroundColorHover
        {
            get => Util.GetColorFromC(tguiPanelListBoxRenderer_getSelectedItemsBackgroundColorHover(CPointer));
            set => tguiPanelListBoxRenderer_setSelectedItemsBackgroundColorHover(CPointer, Util.ConvertColorForC(value));
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBoxRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanelListBoxRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiPanelListBoxRenderer_getItemsBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanelListBoxRenderer_setItemsBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiPanelListBoxRenderer_getItemsBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanelListBoxRenderer_setItemsBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiPanelListBoxRenderer_getSelectedItemsBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanelListBoxRenderer_setSelectedItemsBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiPanelListBoxRenderer_getSelectedItemsBackgroundColorHover(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanelListBoxRenderer_setSelectedItemsBackgroundColorHover(IntPtr cPointer, ColorCTGUI value);

        #endregion
    }
}
