using System.Collections.Generic;

public unsafe Dictionary<Widget, (int row, int column)> WidgetLocations
{
    get
    {
        var dict = new Dictionary<Widget, (int row, int column)>();

        WidgetLocationImpl* locationList = tguiGrid_getWidgetLocations(CPointer, out UIntPtr count);
        for (int i = 0; i < (int)count; ++i)
        {
            Widget? widget = Util.GetWidgetFromC(tguiWidget_addPointerReference(locationList[i].widget));
            if (!(widget is null))
                dict[widget] = ((int)locationList[i].row, (int)locationList[i].column);
        }

        tguiGridWidgetLocation_destroy(locationList, count);
        return dict;
    }
}

[StructLayout(LayoutKind.Sequential)]
private struct WidgetLocationImpl
{
    public IntPtr widget;
    public UIntPtr row;
    public UIntPtr column;
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe void tguiGridWidgetLocation_destroy(WidgetLocationImpl* locationList, UIntPtr count);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe WidgetLocationImpl* tguiGrid_getWidgetLocations(IntPtr cPointer, out UIntPtr count);

#endregion
