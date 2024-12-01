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
