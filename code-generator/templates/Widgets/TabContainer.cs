public Panel AddTab(string name, bool select = true)
{
    return new Panel(tguiTabContainer_addTab(CPointer, Util.ConvertStringForC_UTF32(name), select ? (byte)1 : (byte)0));
}

public Panel InsertTab(int index, string name, bool select = true)
{
    return new Panel(tguiTabContainer_insertTab(CPointer, (UIntPtr)index, Util.ConvertStringForC_UTF32(name), select ? (byte)1 : (byte)0));
}

public int GetIndex(Panel panel)
{
    return tguiTabContainer_getIndex(CPointer, panel.CPointer);
}

public Panel? GetSelected()
{
    IntPtr cPointer = tguiTabContainer_getSelected(CPointer);
    if (cPointer != IntPtr.Zero)
        return new Panel(cPointer);
    else
        return null;
}

public Panel? GetPanel(int index)
{
    IntPtr cPointer = tguiTabContainer_getPanel(CPointer, index);
    if (cPointer != IntPtr.Zero)
        return new Panel(cPointer);
    else
        return null;
}

public Tabs GetTabs()
{
    return new Tabs(tguiTabContainer_getTabs(CPointer));
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiTabContainer_addTab(IntPtr cPointer, IntPtr name, byte select);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiTabContainer_insertTab(IntPtr cPointer, UIntPtr index, IntPtr name, byte select);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern int tguiTabContainer_getIndex(IntPtr cPointer, IntPtr panel);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiTabContainer_getSelected(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiTabContainer_getPanel(IntPtr cPointer, int index);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiTabContainer_getTabs(IntPtr cPointer);

#endregion
