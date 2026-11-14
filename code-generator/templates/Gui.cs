using SFML.Graphics;
using SFML.Window;

public event EventHandler<FloatRect>? OnViewChange;
public event EventHandler? OnWindowFocus;
public event EventHandler? OnWindowUnfocus;

/// <summary>
/// Creates the gui without a window
/// </summary>
/// <remarks>SetWindow has to be called before the gui can be drawn</remarks>
public Gui() :
    base(tguiGuiCSFMLGraphics_create(IntPtr.Zero))
{
    Util.Guis.Add(CPointer, this);
    Util.UnmanagedWidgetCleanupCallbackFuncPtr = Util.UnmanagedWidgetCleanupCallback;

    _unmanagedCallbackOnViewChangeFuncPtr = UnmanagedOnViewChangeCallback;
    _unmanagedCallbackOnWindowFocusFuncPtr = UnmanagedOnWindowFocusCallback;
    _unmanagedCallbackOnWindowUnfocusFuncPtr = UnmanagedOnWindowUnfocusCallback;

    tgui_setBindingWidgetCleanupCallback(Util.UnmanagedWidgetCleanupCallbackFuncPtr);
    tguiGui_setViewChangeCallback(CPointer, _unmanagedCallbackOnViewChangeFuncPtr);
    tguiGui_setWindowFocusCallback(CPointer, _unmanagedCallbackOnWindowFocusFuncPtr);
    tguiGui_setWindowUnfocusCallback(CPointer, _unmanagedCallbackOnWindowUnfocusFuncPtr);
}

/// <summary>
/// Creates the gui for a particular window
/// </summary>
/// <param name="window">Window that will provide the events and will be drawn to</param>
public Gui(RenderWindow window) :
    this()
{
    SetWindow(window);
}

/// <summary>
/// Sets the window on which the gui should be drawn and from which it receives events
/// </summary>
/// <param name="window">Window that will provide the events and will be drawn to</param>
/// <remarks>
/// This can be used to keep the gui when the window has to be recreated (e.g. to switch to fullscreen).
/// </remarks>
public void SetWindow(RenderWindow window)
{
    if (window is null)
        throw new ArgumentNullException(nameof(window));

    if (_window != null)
        DisconnectWindowEvents(_window);

    tguiGuiCSFMLGraphics_setWindow(CPointer, window.CPointer);
    _window = window;
    ConnectWindowEvents(window);
}

/// <summary>
/// Returns the window on which the gui is being drawn, or null if no window was set yet
/// </summary>
public RenderWindow? Window => _window;

/// <summary>
/// Destroys the mouse cursors that were intentionally leaked when guis were destroyed
/// </summary>
/// <remarks>
/// SFML requires a mouse cursor to outlive the window that uses it, so destroying a gui may leak a cursor.
/// This function should only be called after both the gui and the window have been destroyed.
/// </remarks>
public static void CleanupLeakedCursors() => tguiBackendCSFMLGraphics_cleanupLeakedCursors();

/// <summary>
/// Destroy the object
/// </summary>
/// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
protected override void Destroy(bool disposing)
{
    Util.Guis.Remove(CPointer);

    // Events from the window would otherwise still be forwarded to the destroyed gui
    if (disposing && (_window != null))
    {
        DisconnectWindowEvents(_window);
        _window = null;
    }

    // It is possible for resources to outlive the gui because there is no fixed order in which
    // objects would be destructed. If an object outlives the gui then it would crash on exit
    // as destruction will fail after the backend is already gone. So when the gui is destroyed,
    // we destroy all objects internally here. Any attempt to still use an object afterwards will
    // trigger an exception, but there won't be any problem if all that happens is finializing the objects.
    if (Util.Guis.Count == 0)
        Util.DisposeAllRemainingObjects();

    tguiGuiCSFMLGraphics_destroy(CPointer);
}

/// <summary>
/// Draws all the widgets that were added to the gui
/// </summary>
/// <exception cref="InvalidOperationException">No window was set yet</exception>
public void Draw()
{
    if (_window is null)
        throw new InvalidOperationException("Gui.SetWindow has to be called before Gui.Draw");

    tguiGui_draw(CPointer);
}

/// <summary>
/// Processes a LostFocus event
/// </summary>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventLostFocus()
{
    Event e = new Event();
    e.Type = EventType.LostFocus;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a GainedFocus event
/// </summary>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventGainedFocus()
{
    Event e = new Event();
    e.Type = EventType.GainedFocus;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a TextEntered event
/// </summary>
/// <param name="unicode">UTF-16 value of the character</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventTextEntered(string unicode)
{
    Event e = new Event();
    e.Type = EventType.TextEntered;
    e.Text.Unicode = (uint)Char.ConvertToUtf32(unicode, 0);
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a KeyPressed event
/// </summary>
/// <param name="code">Code of the key</param>
/// <param name="scancode">Physical code of the key</param>
/// <param name="alt">Is the Alt modifier pressed?</param>
/// <param name="control">Is the Control modifier pressed?</param>
/// <param name="shift">Is the Shift modifier pressed?</param>
/// <param name="system">Is the System modifier pressed?</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventKeyPressed(Keyboard.Key code, Keyboard.Scancode scancode, bool alt, bool control, bool shift, bool system)
{
    Event e = new Event();
    e.Type = EventType.KeyPressed;
    e.Key.Code = code;
    e.Key.Scancode = scancode;
    e.Key.Alt = alt;
    e.Key.Control = control;
    e.Key.Shift = shift;
    e.Key.System = system;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a KeyReleased event
/// </summary>
/// <param name="code">Code of the key</param>
/// <param name="scancode">Physical code of the key</param>
/// <param name="alt">Is the Alt modifier pressed?</param>
/// <param name="control">Is the Control modifier pressed?</param>
/// <param name="shift">Is the Shift modifier pressed?</param>
/// <param name="system">Is the System modifier pressed?</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventKeyReleased(Keyboard.Key code, Keyboard.Scancode scancode, bool alt, bool control, bool shift, bool system)
{
    Event e = new Event();
    e.Type = EventType.KeyReleased;
    e.Key.Code = code;
    e.Key.Scancode = scancode;
    e.Key.Alt = alt;
    e.Key.Control = control;
    e.Key.Shift = shift;
    e.Key.System = system;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a MouseWheelScrolled event
/// </summary>
/// <param name="wheel">Mouse Wheel which triggered the event</param>
/// <param name="delta">Scroll amount</param>
/// <param name="x">X coordinate of the mouse cursor</param>
/// <param name="y">Y coordinate of the mouse cursor</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventMouseWheelScrolled(Mouse.Wheel wheel, float delta, int x, int y)
{
    Event e = new Event();
    e.Type = EventType.MouseWheelScrolled;
    e.MouseWheelScroll.Wheel = wheel;
    e.MouseWheelScroll.Delta = delta;
    e.MouseWheelScroll.Position.X = x;
    e.MouseWheelScroll.Position.Y = y;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a MouseButtonPressed event
/// </summary>
/// <param name="button">Code of the button</param>
/// <param name="x">X coordinate of the mouse cursor</param>
/// <param name="y">Y coordinate of the mouse cursor</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventMouseButtonPressed(Mouse.Button button, int x, int y)
{
    Event e = new Event();
    e.Type = EventType.MouseButtonPressed;
    e.MouseButton.Button = button;
    e.MouseButton.Position.X = x;
    e.MouseButton.Position.Y = y;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a MouseButtonReleased event
/// </summary>
/// <param name="button">Code of the button</param>
/// <param name="x">X coordinate of the mouse cursor</param>
/// <param name="y">Y coordinate of the mouse cursor</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventMouseButtonReleased(Mouse.Button button, int x, int y)
{
    Event e = new Event();
    e.Type = EventType.MouseButtonReleased;
    e.MouseButton.Button = button;
    e.MouseButton.Position.X = x;
    e.MouseButton.Position.Y = y;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a MouseMoved event
/// </summary>
/// <param name="x">X coordinate of the mouse cursor</param>
/// <param name="y">Y coordinate of the mouse cursor</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventMouseMoved(int x, int y)
{
    Event e = new Event();
    e.Type = EventType.MouseMoved;
    e.MouseMove.Position.X = x;
    e.MouseMove.Position.Y = y;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a TouchBegan event
/// </summary>
/// <param name="finger">Index of the finger in case of multi-touch events</param>
/// <param name="x">X position of the touch, relative to the left of the owner window</param>
/// <param name="y">Y position of the touch, relative to the top of the owner window</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventTouchBegan(uint finger, int x, int y)
{
    Event e = new Event();
    e.Type = EventType.TouchBegan;
    e.Touch.Finger = finger;
    e.Touch.Position.X = x;
    e.Touch.Position.Y = y;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a TouchMoved event
/// </summary>
/// <param name="finger">Index of the finger in case of multi-touch events</param>
/// <param name="x">X position of the touch, relative to the left of the owner window</param>
/// <param name="y">Y position of the touch, relative to the top of the owner window</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventTouchMoved(uint finger, int x, int y)
{
    Event e = new Event();
    e.Type = EventType.TouchMoved;
    e.Touch.Finger = finger;
    e.Touch.Position.X = x;
    e.Touch.Position.Y = y;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a TouchEnded event
/// </summary>
/// <param name="finger">Index of the finger in case of multi-touch events</param>
/// <param name="x">X position of the touch, relative to the left of the owner window</param>
/// <param name="y">Y position of the touch, relative to the top of the owner window</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventTouchEnded(uint finger, int x, int y)
{
    Event e = new Event();
    e.Type = EventType.TouchEnded;
    e.Touch.Finger = finger;
    e.Touch.Position.X = x;
    e.Touch.Position.Y = y;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a MouseEntered event
/// </summary>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventMouseEntered()
{
    Event e = new Event();
    e.Type = EventType.MouseEntered;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a MouseLeft event
/// </summary>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventMouseLeft()
{
    Event e = new Event();
    e.Type = EventType.MouseLeft;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a Resized event
/// </summary>
/// <param name="width">New width of the window</param>
/// <param name="height">New height of the window</param>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventResized(uint width, uint height)
{
    Event e = new Event();
    e.Type = EventType.Resized;
    e.Size.Size.X = width;
    e.Size.Size.Y = height;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

/// <summary>
/// Processes a Closed event
/// </summary>
/// <remarks>This function is automatically called for events from the window. You shouldn't call it unless you want to insert a fake event.</remarks>
public void HandleEventClosed()
{
    Event e = new Event();
    e.Type = EventType.Closed;
    tguiGuiCSFMLGraphics_handleEvent(CPointer, in e);
}

private static void UnmanagedOnViewChangeCallback(IntPtr guiCPointer)
{
    try
    {
        Gui gui = Util.Guis[guiCPointer];
        if (gui.OnViewChange is null)
            return;

        FloatRect viewRect = gui.GetView();
        gui.OnViewChange.Invoke(gui, viewRect);
    }
    catch (KeyNotFoundException)
    {
        return;
    }
}

private static void UnmanagedOnWindowFocusCallback(IntPtr guiCPointer)
{
    try
    {
        Gui gui = Util.Guis[guiCPointer];
        gui.OnWindowFocus?.Invoke(gui, EventArgs.Empty);
    }
    catch (KeyNotFoundException)
    {
        return;
    }
}

private static void UnmanagedOnWindowUnfocusCallback(IntPtr guiCPointer)
{
    try
    {
        Gui gui = Util.Guis[guiCPointer];
        gui.OnWindowUnfocus?.Invoke(gui, EventArgs.Empty);
    }
    catch (KeyNotFoundException)
    {
        return;
    }
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
private delegate void UnmanagedCallbackOnViewChange(IntPtr guiCPointer);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
private delegate void UnmanagedCallbackOnWindowFocus(IntPtr guiCPointer);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
private delegate void UnmanagedCallbackOnWindowUnfocus(IntPtr guiCPointer);

private UnmanagedCallbackOnViewChange _unmanagedCallbackOnViewChangeFuncPtr;
private UnmanagedCallbackOnWindowFocus _unmanagedCallbackOnWindowFocusFuncPtr;
private UnmanagedCallbackOnWindowUnfocus _unmanagedCallbackOnWindowUnfocusFuncPtr;

private void ConnectWindowEvents(RenderWindow window)
{
    window.Closed += OnWindowClosed;
    window.GainedFocus += OnWindowGainedFocus;
    window.KeyPressed += OnWindowKeyPressed;
    window.KeyReleased += OnWindowKeyReleased;
    window.LostFocus += OnWindowLostFocus;
    window.MouseButtonPressed += OnWindowMouseButtonPressed;
    window.MouseButtonReleased += OnWindowMouseButtonReleased;
    window.MouseEntered += OnWindowMouseEntered;
    window.MouseLeft += OnWindowMouseLeft;
    window.MouseMoved += OnWindowMouseMoved;
    window.MouseWheelScrolled += OnWindowMouseWheelScrolled;
    window.Resized += OnWindowResized;
    window.TextEntered += OnWindowTextEntered;
    window.TouchBegan += OnWindowTouchBegan;
    window.TouchMoved += OnWindowTouchMoved;
    window.TouchEnded += OnWindowTouchEnded;
}

private void DisconnectWindowEvents(RenderWindow window)
{
    window.Closed -= OnWindowClosed;
    window.GainedFocus -= OnWindowGainedFocus;
    window.KeyPressed -= OnWindowKeyPressed;
    window.KeyReleased -= OnWindowKeyReleased;
    window.LostFocus -= OnWindowLostFocus;
    window.MouseButtonPressed -= OnWindowMouseButtonPressed;
    window.MouseButtonReleased -= OnWindowMouseButtonReleased;
    window.MouseEntered -= OnWindowMouseEntered;
    window.MouseLeft -= OnWindowMouseLeft;
    window.MouseMoved -= OnWindowMouseMoved;
    window.MouseWheelScrolled -= OnWindowMouseWheelScrolled;
    window.Resized -= OnWindowResized;
    window.TextEntered -= OnWindowTextEntered;
    window.TouchBegan -= OnWindowTouchBegan;
    window.TouchMoved -= OnWindowTouchMoved;
    window.TouchEnded -= OnWindowTouchEnded;
}

private void OnWindowClosed(object? sender, EventArgs e) => HandleEventClosed();
private void OnWindowGainedFocus(object? sender, EventArgs e) => HandleEventGainedFocus();
private void OnWindowKeyPressed(object? sender, KeyEventArgs args) => HandleEventKeyPressed(args.Code, args.Scancode, args.Alt, args.Control, args.Shift, args.System);
private void OnWindowKeyReleased(object? sender, KeyEventArgs args) => HandleEventKeyReleased(args.Code, args.Scancode, args.Alt, args.Control, args.Shift, args.System);
private void OnWindowLostFocus(object? sender, EventArgs e) => HandleEventLostFocus();
private void OnWindowMouseButtonPressed(object? sender, MouseButtonEventArgs args) => HandleEventMouseButtonPressed(args.Button, args.Position.X, args.Position.Y);
private void OnWindowMouseButtonReleased(object? sender, MouseButtonEventArgs args) => HandleEventMouseButtonReleased(args.Button, args.Position.X, args.Position.Y);
private void OnWindowMouseEntered(object? sender, EventArgs e) => HandleEventMouseEntered();
private void OnWindowMouseLeft(object? sender, EventArgs e) => HandleEventMouseLeft();
private void OnWindowMouseMoved(object? sender, MouseMoveEventArgs args) => HandleEventMouseMoved(args.Position.X, args.Position.Y);
private void OnWindowMouseWheelScrolled(object? sender, MouseWheelScrollEventArgs args) => HandleEventMouseWheelScrolled(args.Wheel, args.Delta, args.Position.X, args.Position.Y);
private void OnWindowResized(object? sender, SizeEventArgs args) => HandleEventResized(args.Size.X, args.Size.Y);
private void OnWindowTextEntered(object? sender, TextEventArgs args) => HandleEventTextEntered(args.Unicode);
private void OnWindowTouchBegan(object? sender, TouchEventArgs args) => HandleEventTouchBegan(args.Finger, args.Position.X, args.Position.Y);
private void OnWindowTouchMoved(object? sender, TouchEventArgs args) => HandleEventTouchMoved(args.Finger, args.Position.X, args.Position.Y);
private void OnWindowTouchEnded(object? sender, TouchEventArgs args) => HandleEventTouchEnded(args.Finger, args.Position.X, args.Position.Y);

private RenderWindow? _window;

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
unsafe private static extern void tgui_setBindingWidgetCleanupCallback([MarshalAs(UnmanagedType.FunctionPtr)] Util.UnmanagedCallbackWidgetCleanup callback);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
unsafe private static extern void tguiGui_setViewChangeCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackOnViewChange callback);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
unsafe private static extern void tguiGui_setWindowFocusCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackOnWindowFocus callback);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
unsafe private static extern void tguiGui_setWindowUnfocusCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackOnWindowUnfocus callback);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern IntPtr tguiGuiCSFMLGraphics_create(IntPtr window);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiGuiCSFMLGraphics_destroy(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern byte tguiGuiCSFMLGraphics_handleEvent(IntPtr cPointer, in Event e);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiGuiCSFMLGraphics_setWindow(IntPtr cPointer, IntPtr window);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiBackendCSFMLGraphics_cleanupLeakedCursors();

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern void tguiGui_draw(IntPtr cPointer);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
protected static extern IntPtr tgui_getLastError();

#endregion
