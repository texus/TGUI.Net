// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for MenuBar widgets
    /// </summary>
    public class MenuBarRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuBarRenderer()
            : base(tguiMenuBarRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal MenuBarRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public MenuBarRenderer(MenuBarRenderer copy)
            : base(tguiMenuBarRenderer_copy(copy.CPointer))
        {
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiMenuBarRenderer_getBackgroundColor(CPointer));
            set => tguiMenuBarRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBackgroundColor
        {
            get => Util.GetColorFromC(tguiMenuBarRenderer_getSelectedBackgroundColor(CPointer));
            set => tguiMenuBarRenderer_setSelectedBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiMenuBarRenderer_getTextColor(CPointer));
            set => tguiMenuBarRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColor
        {
            get => Util.GetColorFromC(tguiMenuBarRenderer_getSelectedTextColor(CPointer));
            set => tguiMenuBarRenderer_setSelectedTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDisabled
        {
            get => Util.GetColorFromC(tguiMenuBarRenderer_getTextColorDisabled(CPointer));
            set => tguiMenuBarRenderer_setTextColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SeparatorColor
        {
            get => Util.GetColorFromC(tguiMenuBarRenderer_getSeparatorColor(CPointer));
            set => tguiMenuBarRenderer_setSeparatorColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureBackground
        {
            get => new Texture(tguiMenuBarRenderer_getTextureBackground(CPointer));
            set => tguiMenuBarRenderer_setTextureBackground(CPointer, value.CPointer);
        }

        public Texture TextureItemBackground
        {
            get => new Texture(tguiMenuBarRenderer_getTextureItemBackground(CPointer));
            set => tguiMenuBarRenderer_setTextureItemBackground(CPointer, value.CPointer);
        }

        public Texture TextureSelectedItemBackground
        {
            get => new Texture(tguiMenuBarRenderer_getTextureSelectedItemBackground(CPointer));
            set => tguiMenuBarRenderer_setTextureSelectedItemBackground(CPointer, value.CPointer);
        }

        public float DistanceToSide
        {
            get => tguiMenuBarRenderer_getDistanceToSide(CPointer);
            set => tguiMenuBarRenderer_setDistanceToSide(CPointer, value);
        }

        public float SeparatorThickness
        {
            get => tguiMenuBarRenderer_getSeparatorThickness(CPointer);
            set => tguiMenuBarRenderer_setSeparatorThickness(CPointer, value);
        }

        public float SeparatorVerticalPadding
        {
            get => tguiMenuBarRenderer_getSeparatorVerticalPadding(CPointer);
            set => tguiMenuBarRenderer_setSeparatorVerticalPadding(CPointer, value);
        }

        public float SeparatorSidePadding
        {
            get => tguiMenuBarRenderer_getSeparatorSidePadding(CPointer);
            set => tguiMenuBarRenderer_setSeparatorSidePadding(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuBarRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuBarRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuBarRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuBarRenderer_getSelectedBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setSelectedBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuBarRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuBarRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setSelectedTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuBarRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setTextColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuBarRenderer_getSeparatorColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setSeparatorColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuBarRenderer_getTextureBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setTextureBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuBarRenderer_getTextureItemBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setTextureItemBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuBarRenderer_getTextureSelectedItemBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setTextureSelectedItemBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuBarRenderer_getDistanceToSide(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setDistanceToSide(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuBarRenderer_getSeparatorThickness(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setSeparatorThickness(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuBarRenderer_getSeparatorVerticalPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setSeparatorVerticalPadding(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuBarRenderer_getSeparatorSidePadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBarRenderer_setSeparatorSidePadding(IntPtr cPointer, float value);

        #endregion
    }
}
