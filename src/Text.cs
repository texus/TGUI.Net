// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class Text : ObjectBase
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Text(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Text()
            : base(tguiText_create())
        {
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiText_destroy(CPointer);
        }

        public static float GetExtraHorizontalPadding(Font font, int characterSize)
        {
            return tguiText_getStaticExtraHorizontalPadding(font.CPointer, (uint)characterSize);
        }

        public static float GetExtraHorizontalOffset(Font font, int characterSize)
        {
            return tguiText_getStaticExtraHorizontalOffset(font.CPointer, (uint)characterSize);
        }

        public static float GetExtraVerticalPadding(int characterSize)
        {
            return tguiText_getStaticExtraVerticalPadding((uint)characterSize);
        }

        public static float GetLineHeight(Font font, int characterSize)
        {
            return tguiText_getStaticLineHeight(font.CPointer, (uint)characterSize);
        }

        public static float GetLineWidth(string text, Font font, int characterSize, TextStyles style)
        {
            return tguiText_getStaticLineWidth(Util.ConvertStringForC_UTF32(text), font.CPointer, (uint)characterSize, style);
        }

        public static int FindBestTextSize(Font font, float height, int fit = 0)
        {
            return (int)tguiText_findBestTextSize(font.CPointer, height, fit);
        }

        public static string WordWrap(float maxWidth, string text, Font font, int textSize, bool bold)
        {
            return Util.GetStringFromC_UTF32(tguiText_wordWrap(maxWidth, Util.ConvertStringForC_UTF32(text), font.CPointer, (uint)textSize, bold ? (byte)1 : (byte)0));
        }

        public Vector2f GetSize()
        {
            return tguiText_getSize(CPointer);
        }

        public Vector2f Position
        {
            get => tguiText_getPosition(CPointer);
            set => tguiText_setPosition(CPointer, value);
        }

        public string String
        {
            get => Util.GetStringFromC_UTF32(tguiText_getString(CPointer));
            set => tguiText_setString(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public int CharacterSize
        {
            get => (int)tguiText_getCharacterSize(CPointer);
            set => tguiText_setCharacterSize(CPointer, (uint)value);
        }

        public Color? Color
        {
            get => Util.GetColorFromC(tguiText_getColor(CPointer));
            set => tguiText_setColor(CPointer, Util.ConvertColorForC(value));
        }

        public float Opacity
        {
            get => tguiText_getOpacity(CPointer);
            set => tguiText_setOpacity(CPointer, value);
        }

        public Font Font
        {
            get => new Font(tguiText_getFont(CPointer));
            set => tguiText_setFont(CPointer, value.CPointer);
        }

        public TextStyles Style
        {
            get => tguiText_getStyle(CPointer);
            set => tguiText_setStyle(CPointer, value);
        }

        public Color? OutlineColor
        {
            get => Util.GetColorFromC(tguiText_getOutlineColor(CPointer));
            set => tguiText_setOutlineColor(CPointer, Util.ConvertColorForC(value));
        }

        public float OutlineThickness
        {
            get => tguiText_getOutlineThickness(CPointer);
            set => tguiText_setOutlineThickness(CPointer, value);
        }

        public Vector2f FindCharacterPos(int index)
        {
            return tguiText_findCharacterPos(CPointer, (UIntPtr)index);
        }

        public float GetExtraHorizontalPadding()
        {
            return tguiText_getExtraHorizontalPadding(CPointer);
        }

        public float GetExtraHorizontalOffset()
        {
            return tguiText_getExtraHorizontalOffset(CPointer);
        }

        public float GetLineHeight()
        {
            return tguiText_getLineHeight(CPointer);
        }

        public float GetLineWidth()
        {
            return tguiText_getLineWidth(CPointer);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiText_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_destroy(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getStaticExtraHorizontalPadding(IntPtr font, uint characterSize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getStaticExtraHorizontalOffset(IntPtr font, uint characterSize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getStaticExtraVerticalPadding(uint characterSize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getStaticLineHeight(IntPtr font, uint characterSize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getStaticLineWidth(IntPtr text, IntPtr font, uint characterSize, TextStyles style);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiText_findBestTextSize(IntPtr font, float height, int fit);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiText_wordWrap(float maxWidth, IntPtr text, IntPtr font, uint textSize, byte bold);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiText_getSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiText_getPosition(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setPosition(IntPtr cPointer, Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiText_getString(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setString(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiText_getCharacterSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setCharacterSize(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiText_getColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getOpacity(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setOpacity(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiText_getFont(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setFont(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiText_getStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiText_getOutlineColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setOutlineColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getOutlineThickness(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiText_setOutlineThickness(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiText_findCharacterPos(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getExtraHorizontalPadding(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getExtraHorizontalOffset(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getLineHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiText_getLineWidth(IntPtr cPointer);

        #endregion
    }
}
