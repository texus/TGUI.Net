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
    : base(tguiTexture_createFromFile(Util.ConvertStringForC_UTF32(filename), partRect, middleRect))
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
    : base(tguiTexture_createFromFileEx(Util.ConvertStringForC_UTF32(filename), partRect, middleRect, smooth ? (byte)1 : (byte)0))
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

private static unsafe IntPtr CreateTextureFromMemoryImpl(ReadOnlySpan<byte> bytes, UIntRect partRect, UIntRect middleRect)
{
    fixed (byte* ptr = bytes)
    {
        return tguiTexture_createFromMemory(ptr, (UIntPtr)bytes.Length, partRect, middleRect);
    }
}

private static unsafe IntPtr CreateTextureFromMemoryExImpl(ReadOnlySpan<byte> bytes, UIntRect partRect, UIntRect middleRect, bool smooth)
{
    fixed (byte* ptr = bytes)
    {
        return tguiTexture_createFromMemoryEx(ptr, (UIntPtr)bytes.Length, partRect, middleRect, smooth ? (byte)1 : (byte)0);
    }
}

private static unsafe IntPtr CreateTextureFromPixelDataImpl(Vector2u size, ReadOnlySpan<byte> pixels, UIntRect partRect, UIntRect middleRect)
{
    if (pixels.Length != 4 * size.X * size.Y)
        throw new Exception("Pixel data passed to Texture constructor must be RGBA (pixels.Length must equal 4 * size.X * size.Y)");

    fixed (byte* ptr = pixels)
    {
        return tguiTexture_createFromPixelData(size, ptr, partRect, middleRect);
    }
}

private static unsafe IntPtr CreateTextureFromPixelDataExImpl(Vector2u size, ReadOnlySpan<byte> pixels, UIntRect partRect, UIntRect middleRect, bool smooth)
{
    if (pixels.Length != 4 * size.X * size.Y)
        throw new Exception("Pixel data passed to Texture constructor must be RGBA (pixels.Length must equal 4 * size.X * size.Y)");

    fixed (byte* ptr = pixels)
    {
        return tguiTexture_createFromPixelDataEx(size, ptr, partRect, middleRect, smooth ? (byte)1 : (byte)0);
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

#region Imports

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
