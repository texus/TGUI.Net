public Panel? addItem(string id = "", int index = -1)
{
    IntPtr cPointer;
    if (index >= 0)
        cPointer = tguiPanelListBox_addItemAtIndex(CPointer, Util.ConvertStringForC_UTF32(id), (UIntPtr)index);
    else
        cPointer = tguiPanelListBox_addItem(CPointer, Util.ConvertStringForC_UTF32(id));

    if (cPointer != IntPtr.Zero)
        return new Panel(cPointer);
    else
        return null;
}

public Panel getPanelTemplate()
{
    return new Panel(tguiPanelListBox_getPanelTemplate(CPointer));
}

public bool SetSelectedItem(Panel panel)
{
    return tguiPanelListBox_setSelectedItem(CPointer, panel.CPointer) != 0;
}

public Panel? GetSelectedItem()
{
    IntPtr cPointer = tguiPanelListBox_getSelectedItem(CPointer);
    if (cPointer != IntPtr.Zero)
        return new Panel(cPointer);
    else
        return null;
}

public bool RemoveItem(Panel panel)
{
    return tguiPanelListBox_removeItem(CPointer, panel.CPointer) != 0;
}

public Panel? GetItemById(string id)
{
    IntPtr cPointer = tguiPanelListBox_getItemById(CPointer, Util.ConvertStringForC_UTF32(id));
    if (cPointer != IntPtr.Zero)
        return new Panel(cPointer);
    else
        return null;
}

public Panel? GetItemByIndex(int index)
{
    IntPtr cPointer = tguiPanelListBox_getItemByIndex(CPointer, (UIntPtr)index);
    if (cPointer != IntPtr.Zero)
        return new Panel(cPointer);
    else
        return null;
}

public int GetIndexById(string id)
{
    return tguiPanelListBox_getIndexById(CPointer, Util.ConvertStringForC_UTF32(id));
}

public int GetIndexByItem(Panel panel)
{
    return tguiPanelListBox_getIndexByItem(CPointer, panel.CPointer);
}

public string GetIdByIndex(int index)
{
    return Util.GetStringFromC_UTF32(tguiPanelListBox_getIdByIndex(CPointer, (UIntPtr)index));
}

public unsafe IReadOnlyList<Widget> GetItems()
{
    IntPtr* returnWidgetsC = tguiPanelListBox_getItems(CPointer, out UIntPtr returnCount);
    Panel[] returnWidgets = new Panel[(int)returnCount];
    for (int i = 0; i < (int)returnCount; ++i)
        returnWidgets[i] = (Panel?)Util.GetWidgetFromC(returnWidgetsC[i]) ?? throw new ArgumentNullException();

    return returnWidgets;
}

public bool Contains(Panel panel)
{
    return tguiPanelListBox_contains(CPointer, panel.CPointer) != 0;
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiPanelListBox_addItem(IntPtr cPointer, IntPtr id);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiPanelListBox_addItemAtIndex(IntPtr cPointer, IntPtr id, UIntPtr index);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiPanelListBox_getPanelTemplate(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiPanelListBox_setSelectedItem(IntPtr cPointer, IntPtr panel);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiPanelListBox_getSelectedItem(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiPanelListBox_removeItem(IntPtr cPointer, IntPtr panel);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiPanelListBox_getItemById(IntPtr cPointer, IntPtr id);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiPanelListBox_getItemByIndex(IntPtr cPointer, UIntPtr index);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern int tguiPanelListBox_getIndexById(IntPtr cPointer, IntPtr id);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern int tguiPanelListBox_getIndexByItem(IntPtr cPointer, IntPtr panel);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiPanelListBox_getIdByIndex(IntPtr cPointer, UIntPtr index);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe IntPtr* tguiPanelListBox_getItems(IntPtr cPointer, out UIntPtr count);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiPanelListBox_contains(IntPtr cPointer, IntPtr panel);

#endregion
