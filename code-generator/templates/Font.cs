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
    if (Util.Guis.Count == 0)
        throw new Exception("Gui must be created before creating a Font!");

    IntPtr cPtr = tguiFont_createFromFile(Util.ConvertStringForC_UTF32(filename));
    if (cPtr == IntPtr.Zero)
        throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
    else
        return cPtr;
}

private static unsafe IntPtr CreateFontFromMemoryImpl(ReadOnlySpan<byte> bytes)
{
    if (Util.Guis.Count == 0)
        throw new Exception("Gui must be created before creating a Font!");

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
