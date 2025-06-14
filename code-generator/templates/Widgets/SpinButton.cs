public float Value
{
    get => tguiSpinButton_getValue(CPointer);
    set => tguiSpinButton_setValue(CPointer, value);
}

public float Step
{
    get => tguiSpinButton_getStep(CPointer);
    set => tguiSpinButton_setStep(CPointer, value);
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern float tguiSpinButton_getValue(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiSpinButton_setValue(IntPtr cPointer, float value);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern float tguiSpinButton_getStep(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiSpinButton_setStep(IntPtr cPointer, float value);

#endregion
