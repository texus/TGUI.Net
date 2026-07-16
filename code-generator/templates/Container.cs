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
/// <param name="loadOptions">Settings to use for loading</param>
/// <exception cref="Exception">The file could not be loaded</exception>
public void LoadWidgetsFromFile(string filename, FormLoadOptions? loadOptions = null)
{
    loadOptions ??= new FormLoadOptions();
    if (tguiContainer_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_UTF32(filename), loadOptions.CPointer) == 0)
        throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
}

/// <summary>
/// Saves the child widgets to a text file
/// </summary>
/// <param name="filename">Filename of the widget file</param>
/// <exception cref="Exception">The file could not be saved</exception>
public void SaveWidgetsToFile(string filename)
{
    if (tguiContainer_saveWidgetsToFile(CPointer, Util.ConvertStringForC_UTF32(filename)) == 0)
        throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiContainer_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename, IntPtr loadOptions);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiContainer_saveWidgetsToFile(IntPtr cPointer, IntPtr filename);

#endregion
