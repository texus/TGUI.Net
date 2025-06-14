using System.Collections.Generic;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallback();

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackInt(int val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackUInt(uint val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackNUInt(UIntPtr val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackBool(byte val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackFloat(float val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackColor(ColorCTGUI val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackString(IntPtr val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackVector2f(Vector2f val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackFloatRect(FloatRect val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected unsafe delegate void UnmanagedCallbackBoolPtr(byte* val);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackRange(float val1, float val2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected unsafe delegate void UnmanagedCallbackTabSelectionChanging(int index, byte* vetoed);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackChildWindow(IntPtr windowCPointer);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackItem(int item);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackPanelListBoxItem(int item);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected unsafe delegate void UnmanagedCallbackFileDialogPaths(UIntPtr count, IntPtr* paths);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackShowEffect(ShowEffectType type, byte show);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected delegate void UnmanagedCallbackAnimationType(AnimationType type);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
protected unsafe delegate void UnmanagedCallbackItemHierarchy(UIntPtr count, IntPtr* hierarchy);

/// <summary>
/// Copy constructor
/// </summary>
public Widget(Widget copy)
    : base(tguiWidget_copy(copy.CPointer))
{
}

/// <summary>
/// Destroy the object
/// </summary>
/// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
protected override void Destroy(bool disposing)
{
    tguiWidget_destroy(CPointer);
}

public Vector2f Position
{
    get => tguiWidget_getPosition(CPointer);
    set => tguiWidget_setPosition(CPointer, value);
}

public Layout2d PositionLayout
{
    get => new Layout2d(tguiWidget_getPositionLayout(CPointer));
    set => tguiWidget_setPositionLayout(CPointer, value.CPointer);
}

public Vector2f Size
{
    get => tguiWidget_getSize(CPointer);
    set => tguiWidget_setSize(CPointer, value);
}

public Layout2d SizeLayout
{
    get => new Layout2d(tguiWidget_getSizeLayout(CPointer));
    set => tguiWidget_setSizeLayout(CPointer, value.CPointer);
}

public AutoLayout AutoLayout
{
    get => tguiWidget_getAutoLayout(CPointer);
    set => tguiWidget_setAutoLayout(CPointer, value);
}

public uint Connect(string signalName, Action callbackFunc)
{
    UnmanagedCallback func = () => { callbackFunc(); };
    uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32(signalName), func);
    ConnectEventHandler(id, signalName, callbackFunc, func);
    return id;
}

public bool Disconnect(string signalName, uint id)
{
    if (Util.Callbacks.ContainsKey(CPointer) && Util.Callbacks[CPointer].ContainsKey(signalName))
    {
        // List<UnmanagedCallbackInfo>
        var callbacks = Util.Callbacks[CPointer][signalName];
        foreach (Util.UnmanagedCallbackInfo callbackInfo in callbacks)
        {
            if (callbackInfo.id == id)
            {
                callbacks.Remove(callbackInfo);
                break;
            }
        }

        if (callbacks.Count == 0)
            Util.Callbacks[CPointer].Remove(signalName);
        if (Util.Callbacks[CPointer].Count == 0)
            Util.Callbacks.Remove(CPointer);
    }

    return tguiWidget_signalDisconnect(CPointer, Util.ConvertStringForC_UTF32(signalName), id) != 0;
}

public void DisconnectAll(string signalName)
{
    if (Util.Callbacks.ContainsKey(CPointer))
    {
        Util.Callbacks[CPointer].Remove(signalName);
        if (Util.Callbacks[CPointer].Count == 0)
            Util.Callbacks.Remove(CPointer);
    }

    tguiWidget_signalDisconnectAll(CPointer, Util.ConvertStringForC_UTF32(signalName));
}

public bool SetSignalEnabled(string signalName, bool enabled)
{
    return tguiWidget_setSignalEnabled(CPointer, Util.ConvertStringForC_UTF32(signalName), enabled ? (byte)1 : (byte)0) != 0;
}

public bool IsSignalEnabled(string signalName)
{
    return tguiWidget_isSignalEnabled(CPointer, Util.ConvertStringForC_UTF32(signalName)) != 0;
}

public WidgetRenderer Renderer
{
    get => new WidgetRenderer(tguiWidget_getRenderer(CPointer));
    set => SetRenderer(value.Data);
}

public WidgetRenderer SharedRenderer => new WidgetRenderer(tguiWidget_getSharedRenderer(CPointer));

/// <summary>
/// Sets new renderer data for the widget. The renderer determines how the widget looks.
/// </summary>
/// <param name="rendererData">new renderer data</param>
/// <remarks>
/// The renderer data is shared with this widget. When the data is changed, this widget will be updated as well.
/// </remarks>
public void SetRenderer(RendererData rendererData)
{
    if (tguiWidget_setRenderer(CPointer, rendererData.CPointer) == 0)
        throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
}

public void ShowWithEffect(ShowEffectType type, Duration duration)
{
    tguiWidget_showWithEffect(CPointer, type, duration);
}

public void HideWithEffect(ShowEffectType type, Duration duration)
{
    tguiWidget_hideWithEffect(CPointer, type, duration);
}

public void MoveWithAnimation(Vector2f position, Duration duration)
{
    tguiWidget_moveWithAnimation(CPointer, position, duration);
}

public void ResizeWithAnimation(Vector2f size, Duration duration)
{
    tguiWidget_resizeWithAnimation(CPointer, size, duration);
}

public Widget? ToolTip
{
    get => Util.GetWidgetFromC(tguiWidget_getToolTip(CPointer));
    set => tguiWidget_setToolTip(CPointer, value is null ? IntPtr.Zero : value.CPointer);
}

public Widget? Parent
{
    get => Util.GetWidgetFromC(tguiWidget_getParent(CPointer));
}

public Gui? ParentGui
{
    get 
    {
        IntPtr guiCPointer = tguiWidget_getParentGui(CPointer);
        try
        {
            return Util.Guis[guiCPointer];
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}

public void UpdateTime(Duration duration)
{
    tguiWidget_updateTime(CPointer, duration);
}

protected void ConnectEventHandler(uint id, string signalName, Delegate handler, Delegate helperFunc)
{
    if (id == 0)
        throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));

    if (!Util.Callbacks.ContainsKey(CPointer))
        Util.Callbacks.Add(CPointer, new Dictionary<string, List<Util.UnmanagedCallbackInfo>>());
    if (!Util.Callbacks[CPointer].ContainsKey(signalName))
        Util.Callbacks[CPointer].Add(signalName, new List<Util.UnmanagedCallbackInfo>());

    Util.Callbacks[CPointer][signalName].Add(new Util.UnmanagedCallbackInfo(id, handler, helperFunc));
}

protected void DisconnectEventHandler(string signalName, Delegate handler)
{
    if (!Util.Callbacks.ContainsKey(CPointer) || !Util.Callbacks[CPointer].ContainsKey(signalName))
        return;

    uint id = 0;
    foreach (Util.UnmanagedCallbackInfo info in Util.Callbacks[CPointer][signalName])
    {
        if (info.callbackFunc.Equals(handler))
        {
            id = info.id;
            break;
        }
    }

    if (id != 0)
        Disconnect(signalName, id);
}

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiWidget_copy(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiWidget_destroy(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
public static extern IntPtr tguiWidget_addPointerReference(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiWidget_setPosition(IntPtr cPointer, Vector2f position);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern Vector2f tguiWidget_getPosition(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiWidget_setPositionLayout(IntPtr cPointer, IntPtr layout);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiWidget_getPositionLayout(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiWidget_setSize(IntPtr cPointer, Vector2f size);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern Vector2f tguiWidget_getSize(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiWidget_setSizeLayout(IntPtr cPointer, IntPtr layout);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiWidget_getSizeLayout(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiWidget_setRenderer(IntPtr cPointer, IntPtr renderer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern IntPtr tguiWidget_getRenderer(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern IntPtr tguiWidget_getSharedRenderer(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern IntPtr tgui_getLastError();

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern void tguiWidget_setAutoLayout(IntPtr cPointer, AutoLayout layout);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern AutoLayout tguiWidget_getAutoLayout(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallback function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalIntConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackInt function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalUIntConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackUInt function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalSizeTConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackNUInt function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalBoolConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackBool function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalFloatConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackFloat function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalColorConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackColor function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalStringConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackString function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalVector2fConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackVector2f function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalFloatRectConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackFloatRect function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalBoolPtrConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackBoolPtr function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalRangeConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackRange function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalTabSelectionChangingConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackTabSelectionChanging function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalChildWindowConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackChildWindow function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalItemConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackItem function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalPanelListBoxItemConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackPanelListBoxItem function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalFileDialogPathsConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackFileDialogPaths function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalShowEffectConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackShowEffect function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalAnimationTypeConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackAnimationType function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern uint tguiWidget_signalItemHierarchyConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackItemHierarchy function);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern byte tguiWidget_signalDisconnect(IntPtr cPointer, IntPtr signalName, uint id);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern void tguiWidget_signalDisconnectAll(IntPtr cPointer, IntPtr signalName);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern byte tguiWidget_setSignalEnabled(IntPtr cPointer, IntPtr signalName, byte enabled);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern byte tguiWidget_isSignalEnabled(IntPtr cPointer, IntPtr signalName);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern void tguiWidget_showWithEffect(IntPtr cPointer, ShowEffectType type, Duration duration);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern void tguiWidget_hideWithEffect(IntPtr cPointer, ShowEffectType type, Duration duration);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern void tguiWidget_moveWithAnimation(IntPtr cPointer, Vector2f position, Duration duration);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern void tguiWidget_resizeWithAnimation(IntPtr cPointer, Vector2f size, Duration duration);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern void tguiWidget_setToolTip(IntPtr cPointer, IntPtr toolTip);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern IntPtr tguiWidget_getToolTip(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern IntPtr tguiWidget_getParent(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern IntPtr tguiWidget_getParentGui(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern void tguiWidget_updateTime(IntPtr cPointer, Duration duration);

#endregion
