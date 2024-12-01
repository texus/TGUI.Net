/// <summary>
/// Default constructor
/// </summary>
public Sprite()
    : base(tguiSprite_createNull())
{
}

/// <summary>
/// Construct from a texture
/// </summary>
public Sprite(Texture texture)
    : base(tguiSprite_createFromTexture(texture.CPointer))
{
}

/// <summary>
/// Destroy the object
/// </summary>
/// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
protected override void Destroy(bool disposing)
{
    tguiSprite_destroy(CPointer);
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiSprite_createNull();

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiSprite_createFromTexture(IntPtr texture);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiSprite_destroy(IntPtr cPointer);

#endregion
