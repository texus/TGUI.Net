using System.Collections.Generic;

public struct Filter
{
    public string name;
    public List<string> expressions;
};

public string Path
{
    get => Util.GetStringFromC_UTF32(tguiFileDialog_getPath(CPointer));
    set => tguiFileDialog_setPath(CPointer, Util.ConvertStringForC_UTF32(value));
}

public unsafe IReadOnlyList<string> SelectedPaths
{
    get
    {
        IntPtr* returnStringsC = tguiFileDialog_getSelectedPaths(CPointer, out UIntPtr returnCount);
        string[] returnStrings = new string[(int)returnCount];
        for (int i = 0; i < (int)returnCount; ++i)
            returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

        return returnStrings;
    }
}

public unsafe IReadOnlyList<Filter> FileTypeFilters
{
    get
    {
        IntPtr* filtersC = tguiFileDialog_getFileTypeFilters(CPointer, out UIntPtr filtersCount);

        Filter[] filters = new Filter[(int)filtersCount];
        for (int i = 0; i < (int)filtersCount; ++i)
        {
            filters[i].name = Util.GetStringFromC_UTF32(tguiFileDialogFilter_getName(filtersC[i]));

            IntPtr* expressionsC = tguiFileDialogFilter_getExpressions(filtersC[i], out UIntPtr expressionsCount);
            filters[i].expressions = new List<string>();
            for (int j = 0; j < (int)expressionsCount; ++j)
                filters[i].expressions.Add(Util.GetStringFromC_UTF32(expressionsC[i]));

            tguiFileDialogFilter_destroy(filtersC[i]);
        }

        return filters;
    }

    set
    {
        IntPtr[] filtersC = new IntPtr[value.Count];
        for (int i = 0; i < (int)value.Count; ++i)
        {
            filtersC[i] = tguiFileDialogFilter_create(Util.ConvertStringForC_UTF32(value[i].name));
            if (!(value[i].expressions is null))
            {
                foreach (string expression in value[i].expressions)
                    tguiFileDialogFilter_addExpression(filtersC[i], Util.ConvertStringForC_UTF32(expression));
            }
        }

        tguiFileDialog_setFileTypeFilters(CPointer, filtersC, (UIntPtr)filtersC.Length, (UIntPtr)0);

        for (int i = 0; i < (int)value.Count; ++i)
            tguiFileDialogFilter_destroy(filtersC[i]);
    }
}

public unsafe int DefaultFilterIndex
{
    get => (int)tguiFileDialog_getFileTypeFiltersIndex(CPointer);
    set
    {
        IntPtr* filters = tguiFileDialog_getFileTypeFilters(CPointer, out UIntPtr filtersCount);
        IntPtr[] filtersArray = new IntPtr[(int)filtersCount];
        for (int i = 0; i < (int)filtersCount; ++i)
            filtersArray[i] = filters[i];
        tguiFileDialog_setFileTypeFilters(CPointer, filtersArray, filtersCount, (UIntPtr)value);
        for (int i = 0; i < (int)filtersCount; ++i)
            tguiFileDialogFilter_destroy(filters[i]);
    }
}

public (string name, string size, string modified) ColumnCaptions
{
    get => (Util.GetStringFromC_UTF32(tguiFileDialog_getListViewColumnCaptionsName(CPointer)),
            Util.GetStringFromC_UTF32(tguiFileDialog_getListViewColumnCaptionsSize(CPointer)),
            Util.GetStringFromC_UTF32(tguiFileDialog_getListViewColumnCaptionsModified(CPointer)));
    set => tguiFileDialog_setListViewColumnCaptions(CPointer,
                                                    Util.ConvertStringForC_UTF32(value.name),
                                                    Util.ConvertStringForC_UTF32(value.size),
                                                    Util.ConvertStringForC_UTF32(value.modified));
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiFileDialogFilter_create(IntPtr name);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiFileDialogFilter_destroy(IntPtr filter);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiFileDialogFilter_getName(IntPtr filter);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiFileDialogFilter_addExpression(IntPtr filter, IntPtr expression);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe IntPtr* tguiFileDialogFilter_getExpressions(IntPtr filter, out UIntPtr count);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiFileDialog_setPath(IntPtr cPointer, IntPtr path);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiFileDialog_getPath(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe IntPtr* tguiFileDialog_getSelectedPaths(IntPtr cPointer, out UIntPtr count);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe void tguiFileDialog_setFileTypeFilters(IntPtr cPointer, IntPtr[] filters, UIntPtr filterCount, UIntPtr defaultFilterIndex);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe IntPtr* tguiFileDialog_getFileTypeFilters(IntPtr cPointer, out UIntPtr count);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern UIntPtr tguiFileDialog_getFileTypeFiltersIndex(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiFileDialog_setListViewColumnCaptions(IntPtr cPointer, IntPtr nameColumnText, IntPtr sizeColumnText, IntPtr modifiedColumnText);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiFileDialog_getListViewColumnCaptionsName(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiFileDialog_getListViewColumnCaptionsSize(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiFileDialog_getListViewColumnCaptionsModified(IntPtr cPointer);

#endregion
