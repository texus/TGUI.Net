// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Renderer for Widget widgets
    /// </summary>
    public class WidgetRenderer : ObjectBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public WidgetRenderer()
            : base(tguiWidgetRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal WidgetRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Renderer object to copy</param>
        public WidgetRenderer(WidgetRenderer copy)
            : base(tguiWidgetRenderer_copy(copy.CPointer))
        {
        }

        public float Opacity
        {
            get => tguiWidgetRenderer_getOpacity(CPointer);
            set => tguiWidgetRenderer_setOpacity(CPointer, value);
        }

        public float OpacityDisabled
        {
            get => tguiWidgetRenderer_getOpacityDisabled(CPointer);
            set => tguiWidgetRenderer_setOpacityDisabled(CPointer, value);
        }

        public Font Font
        {
            get => new Font(tguiWidgetRenderer_getFont(CPointer));
            set => tguiWidgetRenderer_setFont(CPointer, value.CPointer);
        }

        public int TextSize
        {
            get => (int)tguiWidgetRenderer_getTextSize(CPointer);
            set => tguiWidgetRenderer_setTextSize(CPointer, (uint)value);
        }

        public bool TransparentTexture
        {
            get => tguiWidgetRenderer_getTransparentTexture(CPointer) != 0;
            set => tguiWidgetRenderer_setTransparentTexture(CPointer, value ? (byte)1 : (byte)0);
        }

        public RendererData Data
        {
            get => new RendererData(tguiWidgetRenderer_getData(CPointer));
            set => tguiWidgetRenderer_setData(CPointer, value.CPointer);
        }

        public void SetProperty(string property, bool value)
        {
            tguiWidgetRenderer_setPropertyBool(CPointer, Util.ConvertStringForC_UTF32(property), value ? (byte)1 : (byte)0);
        }

        public void SetProperty(string property, Font value)
        {
            tguiWidgetRenderer_setPropertyFont(CPointer, Util.ConvertStringForC_UTF32(property), value.CPointer);
        }

        public void SetProperty(string property, Color? value)
        {
            tguiWidgetRenderer_setPropertyColor(CPointer, Util.ConvertStringForC_UTF32(property), Util.ConvertColorForC(value));
        }

        public void SetProperty(string property, string value)
        {
            tguiWidgetRenderer_setPropertyString(CPointer, Util.ConvertStringForC_UTF32(property), Util.ConvertStringForC_UTF32(value));
        }

        public void SetProperty(string property, float value)
        {
            tguiWidgetRenderer_setPropertyNumber(CPointer, Util.ConvertStringForC_UTF32(property), value);
        }

        public void SetProperty(string property, Outline value)
        {
            tguiWidgetRenderer_setPropertyOutline(CPointer, Util.ConvertStringForC_UTF32(property), value.CPointer);
        }

        public void SetProperty(string property, Texture value)
        {
            tguiWidgetRenderer_setPropertyTexture(CPointer, Util.ConvertStringForC_UTF32(property), value.CPointer);
        }

        public void SetProperty(string property, TextStyles value)
        {
            tguiWidgetRenderer_setPropertyTextStyle(CPointer, Util.ConvertStringForC_UTF32(property), value);
        }

        public void SetProperty(string property, RendererData value)
        {
            tguiWidgetRenderer_setPropertyRendererData(CPointer, Util.ConvertStringForC_UTF32(property), value.CPointer);
        }

        public bool hasProperty(string property)
        {
            return tguiWidgetRenderer_hasProperty(CPointer, Util.ConvertStringForC_UTF32(property)) != 0;
        }

        public bool getPropertyBool(string property)
        {
            return tguiWidgetRenderer_getPropertyBool(CPointer, Util.ConvertStringForC_UTF32(property)) != 0;
        }

        public Font getPropertyFont(string property)
        {
            return new Font(tguiWidgetRenderer_getPropertyFont(CPointer, Util.ConvertStringForC_UTF32(property)));
        }

        public Color? getPropertyColor(string property)
        {
            return Util.GetColorFromC(tguiWidgetRenderer_getPropertyColor(CPointer, Util.ConvertStringForC_UTF32(property)));
        }

        public string getPropertyString(string property)
        {
            return Util.GetStringFromC_UTF32(tguiWidgetRenderer_getPropertyString(CPointer, Util.ConvertStringForC_UTF32(property)));
        }

        public float getPropertyNumber(string property)
        {
            return tguiWidgetRenderer_getPropertyNumber(CPointer, Util.ConvertStringForC_UTF32(property));
        }

        public Outline getPropertyOutline(string property)
        {
            return new Outline(tguiWidgetRenderer_getPropertyOutline(CPointer, Util.ConvertStringForC_UTF32(property)));
        }

        public Texture getPropertyTexture(string property)
        {
            return new Texture(tguiWidgetRenderer_getPropertyTexture(CPointer, Util.ConvertStringForC_UTF32(property)));
        }

        public TextStyles getPropertyTextStyle(string property)
        {
            return tguiWidgetRenderer_getPropertyTextStyle(CPointer, Util.ConvertStringForC_UTF32(property));
        }

        public RendererData getPropertyRendererData(string property)
        {
            return new RendererData(tguiWidgetRenderer_getPropertyRendererData(CPointer, Util.ConvertStringForC_UTF32(property)));
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        ///<param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiWidgetRenderer_destroy(CPointer);
        }

        #region CustomImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private byte tguiWidgetRenderer_hasProperty(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private byte tguiWidgetRenderer_getPropertyBool(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidgetRenderer_getPropertyFont(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private ColorCTGUI tguiWidgetRenderer_getPropertyColor(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidgetRenderer_getPropertyString(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiWidgetRenderer_getPropertyNumber(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidgetRenderer_getPropertyOutline(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidgetRenderer_getPropertyTexture(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private TextStyles tguiWidgetRenderer_getPropertyTextStyle(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidgetRenderer_getPropertyRendererData(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidgetRenderer_destroy(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidgetRenderer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidgetRenderer_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiWidgetRenderer_getOpacity(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setOpacity(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiWidgetRenderer_getOpacityDisabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setOpacityDisabled(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidgetRenderer_getFont(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setFont(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiWidgetRenderer_getTextSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setTextSize(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidgetRenderer_getTransparentTexture(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setTransparentTexture(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidgetRenderer_getData(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setData(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyBool(IntPtr cPointer, IntPtr property, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyFont(IntPtr cPointer, IntPtr property, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyColor(IntPtr cPointer, IntPtr property, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyString(IntPtr cPointer, IntPtr property, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyNumber(IntPtr cPointer, IntPtr property, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyOutline(IntPtr cPointer, IntPtr property, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyTexture(IntPtr cPointer, IntPtr property, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyTextStyle(IntPtr cPointer, IntPtr property, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidgetRenderer_setPropertyRendererData(IntPtr cPointer, IntPtr property, IntPtr value);

        #endregion
    }
}
