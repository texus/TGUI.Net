/// <summary>
/// Copy constructor
/// </summary>
public Container(Container copy)
    : base(copy)
{
}

/// <summary>
/// Loads the child widgets from a text file
/// </summary>
/// <param name="filename">Filename of the widget file</param>
/// <param name="replaceExisting">Remove existing widgets first if there are any file</param>
public bool LoadWidgetsFromFile(string filename, bool replaceExisting = true)
{
    return tguiContainer_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_UTF32(filename), replaceExisting ? (byte)1 : (byte)0) != 0;
}

/// <summary>
/// Saves the child widgets to a text file
/// </summary>
/// <param name="filename">Filename of the widget file</param>
public bool SaveWidgetsToFile(string filename)
{
    return tguiContainer_saveWidgetsToFile(CPointer, Util.ConvertStringForC_UTF32(filename)) != 0;
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiContainer_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename, byte replaceExisting);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiContainer_saveWidgetsToFile(IntPtr cPointer, IntPtr filename);

#endregion
