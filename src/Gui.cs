// This file is generated, it should not be edited directly.

using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class Gui : ObjectBase
    {
        public event EventHandler<FloatRect>? OnViewChange;
        public event EventHandler? OnWindowFocus;
        public event EventHandler? OnWindowUnfocus;

        /// <summary>
        /// Creates the gui for a particular window
        /// </summary>
        /// <param name="window">Window that will provide the events and will be drawn to</param>
        public Gui(RenderWindow window) :
            base(tguiGuiCSFMLGraphics_create(window.CPointer))
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

            window.Closed += (s, e) => HandleEventClosed();
            window.GainedFocus += (s, e) => HandleEventGainedFocus();
            window.KeyPressed += (s, args) => HandleEventKeyPressed(args.Code, args.Scancode, args.Alt, args.Control, args.Shift, args.System);
            window.KeyReleased += (s, args) => HandleEventKeyReleased(args.Code, args.Scancode, args.Alt, args.Control, args.Shift, args.System);
            window.LostFocus += (s, e) => HandleEventLostFocus();
            window.MouseButtonPressed += (s, args) => HandleEventMouseButtonPressed(args.Button, args.X, args.Y);
            window.MouseButtonReleased += (s, args) => HandleEventMouseButtonReleased(args.Button, args.X, args.Y);
            window.MouseEntered += (s, e) => HandleEventMouseEntered();
            window.MouseLeft += (s, e) => HandleEventMouseLeft();
            window.MouseMoved += (s, args) => HandleEventMouseMoved(args.X, args.Y);
            window.MouseWheelScrolled += (s, args) => HandleEventMouseWheelScrolled(args.Wheel, args.Delta, args.X, args.Y);
            window.Resized += (s, args) => HandleEventResized(args.Width, args.Height);
            window.TextEntered += (s, args) => HandleEventTextEntered(args.Unicode);
            window.TouchBegan += (s, args) => HandleEventTouchBegan(args.Finger, args.X, args.Y);
            window.TouchMoved += (s, args) => HandleEventTouchMoved(args.Finger, args.X, args.Y);
            window.TouchEnded += (s, args) => HandleEventTouchEnded(args.Finger, args.X, args.Y);
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            Util.Guis.Remove(CPointer);

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
        public void Draw() => tguiGui_draw(CPointer);

        /// <summary>
        /// Loads the child widgets from a text file
        /// </summary>
        /// <param name="filename">Filename of the widget file</param>
        /// <param name="replaceExisting">Remove existing widgets first if there are any file</param>
        public bool LoadWidgetsFromFile(string filename, bool replaceExisting = true)
        {
            return tguiGui_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_UTF32(filename), replaceExisting ? (byte)1 : (byte)0) != 0;
        }

        /// <summary>
        /// Saves the child widgets to a text file
        /// </summary>
        /// <param name="filename">Filename of the widget file</param>
        public bool SaveWidgetsToFile(string filename)
        {
            return tguiGui_saveWidgetsToFile(CPointer, Util.ConvertStringForC_UTF32(filename)) != 0;
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
            e.Key.Alt = alt ? 1 : 0;
            e.Key.Control = control? 1 : 0;
            e.Key.Shift = shift? 1 : 0;
            e.Key.System = system? 1 : 0;
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
            e.Key.Alt = alt ? 1 : 0;
            e.Key.Control = control? 1 : 0;
            e.Key.Shift = shift? 1 : 0;
            e.Key.System = system? 1 : 0;
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
            e.MouseWheelScroll.X = x;
            e.MouseWheelScroll.Y = y;
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
            e.MouseButton.X = x;
            e.MouseButton.Y = y;
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
            e.MouseButton.X = x;
            e.MouseButton.Y = y;
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
            e.MouseMove.X = x;
            e.MouseMove.Y = y;
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
            e.Touch.X = x;
            e.Touch.Y = y;
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
            e.Touch.X = x;
            e.Touch.Y = y;
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
            e.Touch.X = x;
            e.Touch.Y = y;
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
            e.Size.Width = width;
            e.Size.Height = height;
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

        public void SetAbsoluteViewport(FloatRect viewport)
        {
            tguiGui_setAbsoluteViewport(CPointer, viewport);
        }

        public void SetRelativeViewport(FloatRect viewport)
        {
            tguiGui_setRelativeViewport(CPointer, viewport);
        }

        public FloatRect GetViewport()
        {
            return tguiGui_getViewport(CPointer);
        }

        public void SetAbsoluteView(FloatRect view)
        {
            tguiGui_setAbsoluteView(CPointer, view);
        }

        public void SetRelativeView(FloatRect view)
        {
            tguiGui_setRelativeView(CPointer, view);
        }

        public FloatRect GetView()
        {
            return tguiGui_getView(CPointer);
        }

        public bool TabKeyUsageEnabled
        {
            get => tguiGui_isTabKeyUsageEnabled(CPointer) != 0;
            set => tguiGui_setTabKeyUsageEnabled(CPointer, value ? (byte)1 : (byte)0);
        }

        public Font Font
        {
            get => new Font(tguiGui_getFont(CPointer));
            set => tguiGui_setFont(CPointer, value.CPointer);
        }

        public void Add(Widget? widget, string widgetName)
        {
            tguiGui_add(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, Util.ConvertStringForC_UTF32(widgetName));
        }

        public Widget? Get(string widgetName)
        {
            return Util.GetWidgetFromC(tguiGui_get(CPointer, Util.ConvertStringForC_UTF32(widgetName)));
        }

        public IReadOnlyList<Widget> GetWidgets()
        {
            unsafe
            {
                IntPtr* returnWidgetsC = tguiGui_getWidgets(CPointer, out UIntPtr returnCount);
                Widget[] returnWidgets = new Widget[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnWidgets[i] = Util.GetWidgetFromC(returnWidgetsC[i]) ?? throw new ArgumentNullException();

                return returnWidgets;
            }
        }

        public bool Remove(Widget? widget)
        {
            return tguiGui_remove(CPointer, widget is null ? IntPtr.Zero : widget.CPointer) != 0;
        }

        public void RemoveAllWidgets()
        {
            tguiGui_removeAllWidgets(CPointer);
        }

        public Widget? GetFocusedChild()
        {
            return Util.GetWidgetFromC(tguiGui_getFocusedChild(CPointer));
        }

        public Widget? GetFocusedLeaf()
        {
            return Util.GetWidgetFromC(tguiGui_getFocusedLeaf(CPointer));
        }

        public Widget? GetWidgetAtPos(Vector2f pos, bool recursive)
        {
            return Util.GetWidgetFromC(tguiGui_getWidgetAtPos(CPointer, pos, recursive ? (byte)1 : (byte)0));
        }

        public Widget? GetWidgetBelowMouseCursor(Vector2i mousePos, bool recursive)
        {
            return Util.GetWidgetFromC(tguiGui_getWidgetBelowMouseCursor(CPointer, mousePos, recursive ? (byte)1 : (byte)0));
        }

        public bool FocusNextWidget(bool recursive)
        {
            return tguiGui_focusNextWidget(CPointer, recursive ? (byte)1 : (byte)0) != 0;
        }

        public bool FocusPreviousWidget(bool recursive)
        {
            return tguiGui_focusPreviousWidget(CPointer, recursive ? (byte)1 : (byte)0) != 0;
        }

        public void UnfocusAllWidgets()
        {
            tguiGui_unfocusAllWidgets(CPointer);
        }

        public void MoveWidgetToFront(Widget? widget)
        {
            tguiGui_moveWidgetToFront(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public void MoveWidgetToBack(Widget? widget)
        {
            tguiGui_moveWidgetToBack(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public int MoveWidgetForward(Widget? widget)
        {
            return (int)tguiGui_moveWidgetForward(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public int MoveWidgetBackward(Widget? widget)
        {
            return (int)tguiGui_moveWidgetBackward(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public bool SetWidgetIndex(Widget? widget, int index)
        {
            return tguiGui_setWidgetIndex(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, (UIntPtr)index) != 0;
        }

        public int GetWidgetIndex(Widget? widget)
        {
            return tguiGui_getWidgetIndex(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public float Opacity
        {
            get => tguiGui_getOpacity(CPointer);
            set => tguiGui_setOpacity(CPointer, value);
        }

        public int TextSize
        {
            get => (int)tguiGui_getTextSize(CPointer);
            set => tguiGui_setTextSize(CPointer, (uint)value);
        }

        public void SetDrawingUpdatesTime(bool drawUpdatesTime)
        {
            tguiGui_setDrawingUpdatesTime(CPointer, drawUpdatesTime ? (byte)1 : (byte)0);
        }

        public bool UpdateTime()
        {
            return tguiGui_updateTime(CPointer) != 0;
        }

        public void SetOverrideMouseCursor(Cursor.Type type)
        {
            tguiGui_setOverrideMouseCursor(CPointer, type);
        }

        public void RestoreOverrideMouseCursor()
        {
            tguiGui_restoreOverrideMouseCursor(CPointer);
        }

        public void RequestMouseCursor(Cursor.Type type)
        {
            tguiGui_requestMouseCursor(CPointer, type);
        }

        public Vector2f MapPixelToCoords(Vector2i pixel)
        {
            return tguiGui_mapPixelToCoords(CPointer, pixel);
        }

        public Vector2f MapCoordsToPixel(Vector2f coord)
        {
            return tguiGui_mapCoordsToPixel(CPointer, coord);
        }

        public bool KeyboardNavigationEnabled
        {
            get => tguiGui_isKeyboardNavigationEnabled(CPointer) != 0;
            set => tguiGui_setKeyboardNavigationEnabled(CPointer, value ? (byte)1 : (byte)0);
        }

        public void MainLoop(Color? clearColor)
        {
            tguiGui_mainLoop(CPointer, Util.ConvertColorForC(clearColor));
        }

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
        private static extern void tguiGui_draw(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename, byte replaceExisting);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_saveWidgetsToFile(IntPtr cPointer, IntPtr filename);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setAbsoluteViewport(IntPtr cPointer, FloatRect viewport);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setRelativeViewport(IntPtr cPointer, FloatRect viewport);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FloatRect tguiGui_getViewport(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setAbsoluteView(IntPtr cPointer, FloatRect view);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setRelativeView(IntPtr cPointer, FloatRect view);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FloatRect tguiGui_getView(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_isTabKeyUsageEnabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setTabKeyUsageEnabled(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGui_getFont(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setFont(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_add(IntPtr cPointer, IntPtr widget, IntPtr widgetName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGui_get(IntPtr cPointer, IntPtr widgetName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe private static extern IntPtr* tguiGui_getWidgets(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_remove(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_removeAllWidgets(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGui_getFocusedChild(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGui_getFocusedLeaf(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGui_getWidgetAtPos(IntPtr cPointer, Vector2f pos, byte recursive);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGui_getWidgetBelowMouseCursor(IntPtr cPointer, Vector2i mousePos, byte recursive);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_focusNextWidget(IntPtr cPointer, byte recursive);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_focusPreviousWidget(IntPtr cPointer, byte recursive);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_unfocusAllWidgets(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_moveWidgetToFront(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_moveWidgetToBack(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiGui_moveWidgetForward(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiGui_moveWidgetBackward(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_setWidgetIndex(IntPtr cPointer, IntPtr widget, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiGui_getWidgetIndex(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiGui_getOpacity(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setOpacity(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiGui_getTextSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setTextSize(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setDrawingUpdatesTime(IntPtr cPointer, byte drawUpdatesTime);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_updateTime(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setOverrideMouseCursor(IntPtr cPointer, Cursor.Type type);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_restoreOverrideMouseCursor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_requestMouseCursor(IntPtr cPointer, Cursor.Type type);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiGui_mapPixelToCoords(IntPtr cPointer, Vector2i pixel);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiGui_mapCoordsToPixel(IntPtr cPointer, Vector2f coord);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGui_isKeyboardNavigationEnabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_setKeyboardNavigationEnabled(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGui_mainLoop(IntPtr cPointer, ColorCTGUI clearColor);

        #endregion
    }
}
