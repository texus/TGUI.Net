// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class Font : ObjectBase
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Font(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Construct the font from a filename
        /// </summary>
        public Font(string filename)
            : base(CreateFontFromFileImpl(filename))
        {
        }

        /// <summary>
        /// Construct the font from a file in memory
        /// </summary>
        public Font(ReadOnlySpan<byte> bytes)
            : base(CreateFontFromMemoryImpl(bytes))
        {
        }

        private static IntPtr CreateFontFromFileImpl(string filename)
        {
            IntPtr cPtr = tguiFont_createFromFile(Util.ConvertStringForC_UTF32(filename));
            if (cPtr == IntPtr.Zero)
                throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
            else
                return cPtr;
        }

        private static unsafe IntPtr CreateFontFromMemoryImpl(ReadOnlySpan<byte> bytes)
        {
            fixed (byte* ptr = bytes)
            {
                IntPtr cPtr = tguiFont_createFromMemory(ptr, (UIntPtr)bytes.Length);
                if (cPtr == IntPtr.Zero)
                    throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
                else
                    return cPtr;
            }
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiFont_destroy(CPointer);
        }

        /// <summary>
        /// Retrieve a glyph of the font
        /// </summary>
        /// <param name="codePoint">Unicode code point of the character to get</param>
        /// <param name="characterSize">Reference character size</param>
        /// <param name="bold">Retrieve the bold version or the regular one?</param>
        /// <param name="outlineThickness">Thickness of outline (when != 0 the glyph will not be filled)</param>
        /// <returns>The glyph corresponding to codePoint and characterSize</returns>
        /// <remarks>
        /// If the font is a bitmap font, not all character sizes might be available. If the glyph is not available at the
        /// requested size, an empty glyph is returned.
        /// </remarks>
        public FontGlyph getGlyph(uint codePoint, uint characterSize, bool bold, float outlineThickness = 0)
        {
            return tguiFont_getGlyph(CPointer, codePoint, characterSize, bold ? (byte)1 : (byte)0, outlineThickness);
        }

        public static Font GlobalFont
        {
            get => new Font(tguiFont_getGlobalFont());
            set => tguiFont_setGlobalFont(value.CPointer);
        }

        public string GetId()
        {
            return Util.GetStringFromC_UTF32(tguiFont_getId(CPointer));
        }

        public float GetKerning(string first, string second, int characterSize, bool bold = false)
        {
            return tguiFont_getKerning(CPointer, (uint)Char.ConvertToUtf32(first, 0), (uint)Char.ConvertToUtf32(second, 0), (uint)characterSize, bold ? (byte)1 : (byte)0);
        }

        public float GetLineSpacing(int characterSize)
        {
            return tguiFont_getLineSpacing(CPointer, (uint)characterSize);
        }

        public float GetFontHeight(int characterSize)
        {
            return tguiFont_getFontHeight(CPointer, (uint)characterSize);
        }

        public bool Smooth
        {
            get => tguiFont_isSmooth(CPointer) != 0;
            set => tguiFont_setSmooth(CPointer, value ? (byte)1 : (byte)0);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr tgui_getLastError();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFont_createFromFile(IntPtr filename);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe private static extern IntPtr tguiFont_createFromMemory(byte* data, UIntPtr dataSize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFont_destroy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FontGlyph tguiFont_getGlyph(IntPtr cPointer, uint codePoint, uint characterSize, byte bold, float outlineThickness);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFont_getGlobalFont();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFont_setGlobalFont(IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFont_getId(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiFont_getKerning(IntPtr cPointer, uint first, uint second, uint characterSize, byte bold);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiFont_getLineSpacing(IntPtr cPointer, uint characterSize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiFont_getFontHeight(IntPtr cPointer, uint characterSize);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiFont_isSmooth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFont_setSmooth(IntPtr cPointer, byte value);

        #endregion
    }
}
