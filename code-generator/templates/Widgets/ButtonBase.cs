/// <summary>
/// Changes the position of the caption text
/// </summary>
/// <param name="position">Position of the text within the button (X and Y values between 0 and width/height of the button)</param>
/// <param name="origin">Origin that determines which part of the text is placed at the given position. The origin consists of x and y components with values between 0 (left/top) and 1 (right/bottom). The default is (0.5f, 0.5f).</param>
public void SetTextPositionAbs(Vector2f position, Vector2f origin)
{
    tguiButtonBase_setTextPositionAbs(CPointer, position, origin);
}

/// <summary>
/// Changes the position of the caption text
/// </summary>
/// <param name="position">Position of the text within the button, relative to the button size (i.e. X and Y values between 0 and 1)</param>
/// <param name="origin">Origin that determines which part of the text is placed at the given position. The origin consists of x and y components with values between 0 (left/top) and 1 (right/bottom). The default is (0.5f, 0.5f).</param>
public void SetTextPositionRel(Vector2f position, Vector2f origin)
{
    tguiButtonBase_setTextPositionRel(CPointer, position, origin);
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiButtonBase_setTextPositionAbs(IntPtr cPointer, Vector2f position, Vector2f origin);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiButtonBase_setTextPositionRel(IntPtr cPointer, Vector2f position, Vector2f origin);

#endregion
