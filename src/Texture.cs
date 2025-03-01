// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class Texture : ObjectBase
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Texture(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Texture()
            : base(tguiTexture_createNull())
        {
        }

        /// <summary>
        /// Construct the texture from a filename
        /// </summary>
        /// <param name="filename">Id for the the image to load (for the default loader, the id is the filename)</param>
        /// <param name="partRect">Load only part of the image. Pass an empty rectangle if you want to load the full image</param>
        /// <param name="middleRect">Choose the middle part of the image for 9-slice scaling (relative to the part defined by partRect)</param>
        public Texture(string filename, UIntRect partRect = default, UIntRect middleRect = default)
            : base(CreateTextureFromFileImpl(filename, partRect, middleRect))
        {
        }

        /// <summary>
        /// Construct the texture from a filename
        /// </summary>
        /// <param name="filename">Id for the the image to load (for the default loader, the id is the filename)</param>
        /// <param name="partRect">Load only part of the image. Pass an empty rectangle if you want to load the full image</param>
        /// <param name="middleRect">Choose the middle part of the image for 9-slice scaling (relative to the part defined by partRect)</param>
        /// <param name="smooth">Enable smoothing on the texture</param>
        public Texture(string filename, UIntRect partRect, UIntRect middleRect, bool smooth)
            : base(CreateTextureFromFileExImpl(filename, partRect, middleRect, smooth))
        {
        }

        /// <summary>
        /// Construct the texture from a file in memory
        /// </summary>
        /// <param name="bytes">Array of bytes containing the file in memory</param>
        /// <param name="partRect">Load only part of the image. Pass an empty rectangle if you want to load the full image</param>
        /// <param name="middleRect">Choose the middle part of the image for 9-slice scaling (relative to the part defined by partRect)</param>
        public Texture(ReadOnlySpan<byte> bytes, UIntRect partRect = default, UIntRect middleRect = default)
            : base(CreateTextureFromMemoryImpl(bytes, partRect, middleRect))
        {
        }

        /// <summary>
        /// Construct the texture from a file in memory
        /// </summary>
        /// <param name="bytes">Array of bytes containing the file in memory</param>
        /// <param name="partRect">Load only part of the image. Pass an empty rectangle if you want to load the full image</param>
        /// <param name="middleRect">Choose the middle part of the image for 9-slice scaling (relative to the part defined by partRect)</param>
        /// <param name="smooth">Enable smoothing on the texture</param>
        public Texture(ReadOnlySpan<byte> bytes, UIntRect partRect, UIntRect middleRect, bool smooth)
            : base(CreateTextureFromMemoryExImpl(bytes, partRect, middleRect, smooth))
        {
        }

        /// <summary>
        /// Construct the texture from pixel data
        /// </summary>
        /// <param name="size">Width and height of the image to create</param>
        /// <param name="pixels">Array of size.X * size.Y * 4 bytes with RGBA pixels</param>
        /// <param name="partRect">Load only part of the image. Pass an empty rectangle if you want to load the full image</param>
        /// <param name="middleRect">Choose the middle part of the image for 9-slice scaling (relative to the part defined by partRect)</param>
        /// <exception cref="Exception">pixels.Length must equal 4 * size.X * size.Y</exception>
        public Texture(Vector2u size, ReadOnlySpan<byte> pixels, UIntRect partRect = default, UIntRect middleRect = default)
            : base(CreateTextureFromPixelDataImpl(size, pixels, partRect, middleRect))
        {
        }

        /// <summary>
        /// Construct the texture from pixel data
        /// </summary>
        /// <param name="size">Width and height of the image to create</param>
        /// <param name="pixels">Array of size.X * size.Y * 4 bytes with RGBA pixels</param>
        /// <param name="partRect">Load only part of the image. Pass an empty rectangle if you want to load the full image</param>
        /// <param name="middleRect">Choose the middle part of the image for 9-slice scaling (relative to the part defined by partRect)</param>
        /// <param name="smooth">Enable smoothing on the texture</param>
        /// <exception cref="Exception">pixels.Length must equal 4 * size.X * size.Y</exception>
        public Texture(Vector2u size, ReadOnlySpan<byte> pixels, UIntRect partRect, UIntRect middleRect, bool smooth)
            : base(CreateTextureFromPixelDataExImpl(size, pixels, partRect, middleRect, smooth))
        {
        }

        private static IntPtr CreateTextureFromFileImpl(string filename, UIntRect partRect, UIntRect middleRect)
        {
            IntPtr cPtr = tguiTexture_createFromFile(Util.ConvertStringForC_UTF32(filename), partRect, middleRect);
            if (cPtr == IntPtr.Zero)
                throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
            else
                return cPtr;
        }

        private static IntPtr CreateTextureFromFileExImpl(string filename, UIntRect partRect, UIntRect middleRect, bool smooth)
        {
            IntPtr cPtr = tguiTexture_createFromFileEx(Util.ConvertStringForC_UTF32(filename), partRect, middleRect, smooth ? (byte)1 : (byte)0);
            if (cPtr == IntPtr.Zero)
                throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
            else
                return cPtr;
        }

        private static unsafe IntPtr CreateTextureFromMemoryImpl(ReadOnlySpan<byte> bytes, UIntRect partRect, UIntRect middleRect)
        {
            fixed (byte* ptr = bytes)
            {
                IntPtr cPtr = tguiTexture_createFromMemory(ptr, (UIntPtr)bytes.Length, partRect, middleRect);
                if (cPtr == IntPtr.Zero)
                    throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
                else
                    return cPtr;
            }
        }

        private static unsafe IntPtr CreateTextureFromMemoryExImpl(ReadOnlySpan<byte> bytes, UIntRect partRect, UIntRect middleRect, bool smooth)
        {
            fixed (byte* ptr = bytes)
            {
                IntPtr cPtr = tguiTexture_createFromMemoryEx(ptr, (UIntPtr)bytes.Length, partRect, middleRect, smooth ? (byte)1 : (byte)0);
                if (cPtr == IntPtr.Zero)
                    throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
                else
                    return cPtr;
            }
        }

        private static unsafe IntPtr CreateTextureFromPixelDataImpl(Vector2u size, ReadOnlySpan<byte> pixels, UIntRect partRect, UIntRect middleRect)
        {
            if (pixels.Length != 4 * size.X * size.Y)
                throw new Exception("Pixel data passed to Texture constructor must be RGBA (pixels.Length must equal 4 * size.X * size.Y)");

            fixed (byte* ptr = pixels)
            {
                IntPtr cPtr = tguiTexture_createFromPixelData(size, ptr, partRect, middleRect);
                if (cPtr == IntPtr.Zero)
                    throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
                else
                    return cPtr;
            }
        }

        private static unsafe IntPtr CreateTextureFromPixelDataExImpl(Vector2u size, ReadOnlySpan<byte> pixels, UIntRect partRect, UIntRect middleRect, bool smooth)
        {
            if (pixels.Length != 4 * size.X * size.Y)
                throw new Exception("Pixel data passed to Texture constructor must be RGBA (pixels.Length must equal 4 * size.X * size.Y)");

            fixed (byte* ptr = pixels)
            {
                IntPtr cPtr = tguiTexture_createFromPixelDataEx(size, ptr, partRect, middleRect, smooth ? (byte)1 : (byte)0);
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
            tguiTexture_destroy(CPointer);
        }

        public Color? Color
        {
            get => Util.GetColorFromC(tguiTexture_getColor(CPointer));
            set => tguiTexture_setColor(CPointer, Util.ConvertColorForC(value));
        }

        public UIntRect MiddleRect
        {
            get => tguiTexture_getMiddleRect(CPointer);
            set => tguiTexture_setMiddleRect(CPointer, value);
        }

        public string GetId()
        {
            return Util.GetStringFromC_UTF32(tguiTexture_getId(CPointer));
        }

        public Vector2u GetImageSize()
        {
            return tguiTexture_getImageSize(CPointer);
        }

        public UIntRect GetPartRect()
        {
            return tguiTexture_getPartRect(CPointer);
        }

        public bool IsSmooth()
        {
            return tguiTexture_isSmooth(CPointer) != 0;
        }

        public bool IsTransparentPixel(Vector2u pos)
        {
            return tguiTexture_isTransparentPixel(CPointer, pos) != 0;
        }

        public static void SetDefaultSmooth(bool smooth)
        {
            tguiTexture_setDefaultSmooth(smooth ? (byte)1 : (byte)0);
        }

        public static bool GetDefaultSmooth()
        {
            return tguiTexture_getDefaultSmooth() != 0;
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr tgui_getLastError();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTexture_createNull();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTexture_createFromFile(IntPtr filename, UIntRect partRect, UIntRect middleRect);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTexture_createFromFileEx(IntPtr filename, UIntRect partRect, UIntRect middleRect, byte smoothing);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe private static extern IntPtr tguiTexture_createFromMemory(byte* data, UIntPtr dataSize, UIntRect partRect, UIntRect middleRect);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe private static extern IntPtr tguiTexture_createFromMemoryEx(byte* data, UIntPtr dataSize, UIntRect partRect, UIntRect middleRect, byte smoothing);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe private static extern IntPtr tguiTexture_createFromPixelData(Vector2u size, byte* pixels, UIntRect partRect, UIntRect middleRect);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe private static extern IntPtr tguiTexture_createFromPixelDataEx(Vector2u size, byte* pixels, UIntRect partRect, UIntRect middleRect, byte smoothing);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTexture_destroy(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiTexture_getColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTexture_setColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntRect tguiTexture_getMiddleRect(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTexture_setMiddleRect(IntPtr cPointer, UIntRect value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTexture_getId(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2u tguiTexture_getImageSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntRect tguiTexture_getPartRect(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTexture_isSmooth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTexture_isTransparentPixel(IntPtr cPointer, Vector2u pos);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTexture_setDefaultSmooth(byte smooth);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTexture_getDefaultSmooth();

        #endregion
    }
}
