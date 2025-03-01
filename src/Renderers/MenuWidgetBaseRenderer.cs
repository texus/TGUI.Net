// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for MenuWidgetBase widgets
    /// </summary>
    public class MenuWidgetBaseRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuWidgetBaseRenderer()
            : base(tguiMenuWidgetBaseRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal MenuWidgetBaseRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public MenuWidgetBaseRenderer(MenuWidgetBaseRenderer copy)
            : base(tguiMenuWidgetBaseRenderer_copy(copy.CPointer))
        {
        }

        public Color? BackgroundColor
        {
            get => Util.GetColorFromC(tguiMenuWidgetBaseRenderer_getBackgroundColor(CPointer));
            set => tguiMenuWidgetBaseRenderer_setBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedBackgroundColor
        {
            get => Util.GetColorFromC(tguiMenuWidgetBaseRenderer_getSelectedBackgroundColor(CPointer));
            set => tguiMenuWidgetBaseRenderer_setSelectedBackgroundColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiMenuWidgetBaseRenderer_getTextColor(CPointer));
            set => tguiMenuWidgetBaseRenderer_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SelectedTextColor
        {
            get => Util.GetColorFromC(tguiMenuWidgetBaseRenderer_getSelectedTextColor(CPointer));
            set => tguiMenuWidgetBaseRenderer_setSelectedTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public Color? TextColorDisabled
        {
            get => Util.GetColorFromC(tguiMenuWidgetBaseRenderer_getTextColorDisabled(CPointer));
            set => tguiMenuWidgetBaseRenderer_setTextColorDisabled(CPointer, Util.ConvertColorForC(value));
        }

        public Color? SeparatorColor
        {
            get => Util.GetColorFromC(tguiMenuWidgetBaseRenderer_getSeparatorColor(CPointer));
            set => tguiMenuWidgetBaseRenderer_setSeparatorColor(CPointer, Util.ConvertColorForC(value));
        }

        public Texture TextureItemBackground
        {
            get => new Texture(tguiMenuWidgetBaseRenderer_getTextureItemBackground(CPointer));
            set => tguiMenuWidgetBaseRenderer_setTextureItemBackground(CPointer, value.CPointer);
        }

        public Texture TextureSelectedItemBackground
        {
            get => new Texture(tguiMenuWidgetBaseRenderer_getTextureSelectedItemBackground(CPointer));
            set => tguiMenuWidgetBaseRenderer_setTextureSelectedItemBackground(CPointer, value.CPointer);
        }

        public float DistanceToSide
        {
            get => tguiMenuWidgetBaseRenderer_getDistanceToSide(CPointer);
            set => tguiMenuWidgetBaseRenderer_setDistanceToSide(CPointer, value);
        }

        public float SeparatorThickness
        {
            get => tguiMenuWidgetBaseRenderer_getSeparatorThickness(CPointer);
            set => tguiMenuWidgetBaseRenderer_setSeparatorThickness(CPointer, value);
        }

        public float SeparatorVerticalPadding
        {
            get => tguiMenuWidgetBaseRenderer_getSeparatorVerticalPadding(CPointer);
            set => tguiMenuWidgetBaseRenderer_setSeparatorVerticalPadding(CPointer, value);
        }

        public float SeparatorSidePadding
        {
            get => tguiMenuWidgetBaseRenderer_getSeparatorSidePadding(CPointer);
            set => tguiMenuWidgetBaseRenderer_setSeparatorSidePadding(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuWidgetBaseRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuWidgetBaseRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuWidgetBaseRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuWidgetBaseRenderer_getSelectedBackgroundColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setSelectedBackgroundColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuWidgetBaseRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuWidgetBaseRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setSelectedTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuWidgetBaseRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setTextColorDisabled(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiMenuWidgetBaseRenderer_getSeparatorColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setSeparatorColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuWidgetBaseRenderer_getTextureItemBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setTextureItemBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuWidgetBaseRenderer_getTextureSelectedItemBackground(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setTextureSelectedItemBackground(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuWidgetBaseRenderer_getDistanceToSide(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setDistanceToSide(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuWidgetBaseRenderer_getSeparatorThickness(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setSeparatorThickness(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuWidgetBaseRenderer_getSeparatorVerticalPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setSeparatorVerticalPadding(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuWidgetBaseRenderer_getSeparatorSidePadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuWidgetBaseRenderer_setSeparatorSidePadding(IntPtr cPointer, float value);

        #endregion
    }
}
