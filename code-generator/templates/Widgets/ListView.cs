public void Sort(int columnIndex, Func<string, string, bool> compareFunc)
{
    UnmanagedCallbackSort func = (IntPtr a, IntPtr b) => compareFunc(Util.GetStringFromC_UTF32(a), Util.GetStringFromC_UTF32(b)) ? (byte)1 : (byte)0;
    tguiListView_sort(CPointer, (UIntPtr)columnIndex, func);
}

private delegate byte UnmanagedCallbackSort(IntPtr a, IntPtr b);

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiListView_sort(IntPtr cPointer, UIntPtr index, UnmanagedCallbackSort func);

#endregion
