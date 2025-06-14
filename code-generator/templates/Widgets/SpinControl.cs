public float Value
{
    get => tguiSpinControl_getValue(CPointer);
    set => tguiSpinControl_setValue(CPointer, value);
}

public float Step
{
    get => tguiSpinControl_getStep(CPointer);
    set => tguiSpinControl_setStep(CPointer, value);
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern float tguiSpinControl_getValue(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiSpinControl_setValue(IntPtr cPointer, float value);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern float tguiSpinControl_getStep(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiSpinControl_setStep(IntPtr cPointer, float value);

#endregion
