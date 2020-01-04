/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2020 Bruno Van de Velde (vdv_b@tgui.eu)
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented;
//    you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment
//    in the product documentation would be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such,
//    and must not be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Security;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    /// <summary>
    /// Gui class that acts as the root container
    /// </summary>
    public class Gui : SFML.ObjectBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks>
        /// You will still need to set the Target property before using the Gui
        /// </remarks>
        public Gui()
            : base(tguiGui_create())
        {
        }

        /// <summary>
        /// Default constructor that sets the window on which the gui should be drawn
        /// </summary>
        /// <param name="window">Window to draw the gui on</param>
        public Gui(RenderWindow window)
            : this()
        {
            Target = window;
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiGui_destroy(CPointer);
        }

        /// <summary>
        /// Gets or sets the window on which the gui should be drawn
        /// </summary>
        public RenderWindow Target
        {
            get { return myRenderTarget; }
            set
            {
                // RenderWindow instead of RenderTarget because we need the events
                myRenderTarget = value;
                tguiGui_setTargetRenderWindow(CPointer, value.CPointer);

                value.MouseMoved += new EventHandler<MouseMoveEventArgs>(OnMouseMoved);
                value.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMousePressed);
                value.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(OnMouseReleased);
                value.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
                value.KeyReleased += new EventHandler<KeyEventArgs>(OnKeyReleased);
                value.TextEntered += new EventHandler<TextEventArgs>(OnTextEntered);
                value.MouseWheelScrolled += new EventHandler<MouseWheelScrollEventArgs>(OnMouseWheelScrolled);
                value.LostFocus += OnLostFocus;
                value.GainedFocus += OnGainedFocus;
            }
        }

        /// <summary>
        /// Sets the view that is used to render the gui
        /// </summary>
        public View View
        {
            set { tguiGui_setView(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Sets the font that should be used by all widgets added to this gui
        /// </summary>
        public Font Font
        {
            set { tguiGui_setFont(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Adds a widget to the gui
        /// </summary>
        /// <param name="widget">The widget you would like to add</param>
        /// <param name="widgetName">You can give the widget a unique name to retrieve it from the gui later</param>
        /// <remarks>
        /// The widget name should not contain whitespace
        /// </remarks>
        public void Add(Widget widget, string widgetName = "")
        {
            tguiGui_add(CPointer, widget.CPointer, Util.ConvertStringForC_UTF32(widgetName));

            widget.ParentGui = this;
        }

        /// <summary>
        /// Returns a widget that was added earlier
        /// </summary>
        /// <param name="widgetName">The name that was given to the widget when it was added to the gui</param>
        /// <returns>
        /// The earlier added widget
        /// </returns>
        /// <remarks>
        /// The gui will first search for widgets that are direct children of it, but when none of the child widgets match
        /// the given name, a recursive search will be performed.
        /// The function returns null when an unknown widget name was passed.
        /// </remarks>
        public Widget Get(string widgetName)
        {
            return Util.GetWidgetFromC(tguiGui_get(CPointer, Util.ConvertStringForC_UTF32(widgetName)), this);
        }

        /// <summary>
        /// Returns a list of all the widgets in this gui
        /// </summary>
        /// <returns>
        /// List of widgets that have been added to the gui
        /// </returns>
        public List<Widget> GetWidgets()
        {
            unsafe
            {
                IntPtr* WidgetsPtr = tguiGui_getWidgets(CPointer, out uint Count);
                List<Widget> Widgets = new List<Widget>();
                for (uint i = 0; i < Count; ++i)
                    Widgets.Add(Util.GetWidgetFromC(WidgetsPtr[i], this));

                return Widgets;
            }
        }

        /// <summary>
        /// Returns a list of the names of all the widgets in this gui
        /// </summary>
        /// <returns>
        /// List of widget names belonging to the widgets that were added to the gui
        /// </returns>
        public List<string> GetWidgetNames()
        {
            unsafe
            {
                IntPtr* NamesPtr = tguiGui_getWidgetNames(CPointer, out uint Count);
                List<string> Names = new List<string>();
                for (uint i = 0; i < Count; ++i)
                    Names.Add(Util.GetStringFromC_UTF32(NamesPtr[i]));

                return Names;
            }
        }

        /// <summary>
        /// Removes a single widget that was added to the gui
        /// </summary>
        /// <param name="widget">Widget to remove</param>
        /// <returns>
        /// True when widget is removed, false when widget was not found
        /// </returns>
        public void Remove(Widget widget)
        {
            tguiGui_remove(CPointer, widget.CPointer);
        }

        /// <summary>
        /// Removes all widgets that were added to the container
        /// </summary>
        public void RemoveAllWidgets()
        {
            tguiGui_removeAllWidgets(CPointer);
        }

        /// <summary>
        /// Gets or sets whether pressing tab will focus another widget
        /// </summary>
        public bool TabKeyUsageEnabled
        {
            get { return tguiGui_isTabKeyUsageEnabled(CPointer); }
            set { tguiGui_setTabKeyUsageEnabled(CPointer, value); }
        }

        /// <summary>
        /// Draws all the widgets that were added to the gui
        /// </summary>
        public void Draw()
        {
            tguiGui_draw(CPointer);
        }

        /// <summary>
        /// Gets or sets the opacity of all widgets that are added to the gui
        /// </summary>
        /// <remarks>
        /// 0 means completely transparent, while 1 (default) means fully opaque
        /// </remarks>
        public float Opacity
        {
            get { return tguiGui_getOpacity(CPointer); }
            set { tguiGui_setOpacity(CPointer, value); }
        }

        /// <summary>
        /// Loads the child widgets from a text file
        /// </summary>
        /// <param name="filename">Filename of the widget file</param>
        /// <exception cref="TGUIException">Thrown when file could not be opened or parsing failed</exception>
        public void LoadWidgetsFromFile(string filename)
        {
            if (!tguiGui_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_ASCII(filename)))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        /// <summary>
        /// Saves the child widgets to a text file
        /// </summary>
        /// <param name="filename">Filename of the widget file</param>
        /// <exception cref="TGUIException">Thrown when file could not be opened for writing</exception>
        public void SaveWidgetsToFile(string filename)
        {
            if (!tguiGui_saveWidgetsToFile(CPointer, Util.ConvertStringForC_ASCII(filename)))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        /// <summary>
        /// Gets or sets the filter function that determines whether the gui should handle the event
        /// </summary>
        /// <remarks>
        /// By default the event filter is null and the gui will handle all events.
        /// You can set a function here that takes an event as parameter and returns whether or not
        /// the gui should handle this event.
        /// </remarks>
        public Func<Event, bool> EventFilter
        {
            get { return myEventFilter; }
            set { myEventFilter = value; }
        }

        /// <summary>
        /// Passes the event to the widgets
        /// </summary>
        /// <param name="ev">The event that was polled from the window</param>
        /// <returns>
        /// Has the event been consumed?
        /// When this function returns false, then the event was ignored by all widgets.
        /// </returns>
        protected bool HandleEvent(Event ev)
        {
            if ((myEventFilter != null) && !myEventFilter(ev))
                return false;

            bool processed = tguiGui_handleEvent(CPointer, ev);
            EventProcessed?.Invoke(this, new SignalArgsEventProcessed(ev, processed));
            return processed;
        }

        private void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.MouseMoved
            };
            ev.MouseMove.X = e.X;
            ev.MouseMove.Y = e.Y;
            HandleEvent(ev);
        }

        private void OnTouchMoved(object sender, TouchEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.TouchMoved
            };
            ev.Touch.X = e.X;
            ev.Touch.Y = e.Y;
            ev.Touch.Finger = e.Finger;
            HandleEvent(ev);
        }

        private void OnMousePressed(object sender, MouseButtonEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.MouseButtonPressed
            };
            ev.MouseButton.X = e.X;
            ev.MouseButton.Y = e.Y;
            ev.MouseButton.Button = e.Button;
            HandleEvent(ev);
        }

        private void OnTouchBegan(object sender, TouchEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.TouchBegan
            };
            ev.Touch.X = e.X;
            ev.Touch.Y = e.Y;
            ev.Touch.Finger = e.Finger;
            HandleEvent(ev);
        }

        private void OnMouseReleased(object sender, MouseButtonEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.MouseButtonReleased
            };
            ev.MouseButton.X = e.X;
            ev.MouseButton.Y = e.Y;
            ev.MouseButton.Button = e.Button;
            HandleEvent(ev);
        }

        private void OnTouchEnded(object sender, TouchEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.TouchEnded
            };
            ev.Touch.X = e.X;
            ev.Touch.Y = e.Y;
            ev.Touch.Finger = e.Finger;
            HandleEvent(ev);
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.KeyPressed
            };
            ev.Key.Code = e.Code;
            ev.Key.Control = Convert.ToInt32(e.Control);
            ev.Key.Shift = Convert.ToInt32(e.Shift);
            ev.Key.Alt = Convert.ToInt32(e.Alt);
            ev.Key.System = Convert.ToInt32(e.System);
            HandleEvent(ev);
        }

        private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.KeyReleased
            };
            ev.Key.Code = e.Code;
            ev.Key.Control = Convert.ToInt32(e.Control);
            ev.Key.Shift = Convert.ToInt32(e.Shift);
            ev.Key.Alt = Convert.ToInt32(e.Alt);
            ev.Key.System = Convert.ToInt32(e.System);
            HandleEvent(ev);
        }

        private void OnTextEntered(object sender, TextEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.TextEntered
            };
            ev.Text.Unicode = (uint)Char.ConvertToUtf32(e.Unicode, 0);
            HandleEvent(ev);
        }

        private void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.MouseWheelScrolled
            };
            ev.MouseWheelScroll.Wheel = e.Wheel;
            ev.MouseWheelScroll.Delta = e.Delta;
            ev.MouseWheelScroll.X = e.X;
            ev.MouseWheelScroll.Y = e.Y;
            HandleEvent(ev);
        }

        private void OnLostFocus(object sender, EventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.LostFocus
            };
            HandleEvent(ev);
        }

        private void OnGainedFocus(object sender, EventArgs e)
        {
            Event ev = new Event
            {
                Type = EventType.GainedFocus
            };
            HandleEvent(ev);
        }

        /// <summary>Event handler that provides a callback for each event processed by the gui</summary>
        public event EventHandler<SignalArgsEventProcessed> EventProcessed = null;

        private RenderWindow myRenderTarget = null;
        private Func<Event, bool> myEventFilter = null;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tgui_getLastError();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiGui_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_destroy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_setTargetRenderWindow(IntPtr cPointer, IntPtr cPointerRenderWindow);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_setView(IntPtr cPointer, IntPtr cPointerView);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiGui_handleEvent(IntPtr cPointer, Event ev);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_add(IntPtr cPointer, IntPtr cPointerWidget, IntPtr widgetName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiGui_get(IntPtr cPointer, IntPtr widgetName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiGui_getWidgets(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiGui_getWidgetNames(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_remove(IntPtr cPointer, IntPtr cPointerWidget);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_removeAllWidgets(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_setTabKeyUsageEnabled(IntPtr cPointer, bool enabled);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiGui_isTabKeyUsageEnabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_draw(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_setFont(IntPtr cPointer, IntPtr font);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiGui_setOpacity(IntPtr cPointer, float opacity);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiGui_getOpacity(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiGui_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiGui_saveWidgetsToFile(IntPtr cPointer, IntPtr filename);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidget_getWidgetType(IntPtr cPointer);

        #endregion
    }
}
