RadioButton? CheckedRadioButton => (RadioButton?)Util.GetWidgetFromC(tguiRadioButtonGroup_getCheckedRadioButton(CPointer));

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiRadioButtonGroup_getCheckedRadioButton(IntPtr cPointer);

#endregion
