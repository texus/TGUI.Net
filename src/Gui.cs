/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2016 Bruno Van de Velde (vdv_b@tgui.eu)
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
    public class Gui : SFML.ObjectBase
    {
        public Gui()
            : base(tguiGui_create())
        {
        }

        public Gui(RenderWindow window)
            : this()
        {
            Target = window;
        }

        protected override void Destroy(bool disposing)
        {
            tguiGui_destroy(CPointer);
        }

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

        public View View
        {
            set { tguiGui_setView(CPointer, value.CPointer); }
        }

        public Font Font
        {
            set { tguiGui_setFont(CPointer, value.CPointer); }
        }

        public void Add(Widget widget, string widgetName = "")
        {
            tguiGui_add(CPointer, widget.CPointer, Util.ConvertStringForC_UTF32(widgetName));

            widget.ParentGui = this;
            myWidgets.Add(widget);
            myWidgetIds.Add(widgetName);
        }

        public Widget Get(string widgetName)
        {
            // Search for the widget locally
            for (var i = 0; i < myWidgetIds.Count; ++i)
            {
                if (myWidgetIds[i] == widgetName)
                    return myWidgets[i];
            }

            // If not found, it is still possible that it exists (e.g. it could have been loaded from a file inside the c++ code)
            return Util.GetWidgetFromC(tguiGui_get(CPointer, Util.ConvertStringForC_UTF32(widgetName)), this);
        }

        public List<Widget> GetWidgets()
        {
            // We can't use our myWidgets member because the c++ code may contain more widgets (e.g. it could have been loaded from a file inside the c++ code)

            unsafe
            {
                IntPtr* WidgetsPtr = tguiGui_getWidgets(CPointer, out uint Count);
                List<Widget> Widgets = new List<Widget>();
                for (uint i = 0; i < Count; ++i)
                    Widgets.Add(Util.GetWidgetFromC(WidgetsPtr[i], this));

                return Widgets;
            }
        }

        public List<string> GetWidgetNames()
        {
            // We can't use our myWidgetIds member because the c++ code may contain more widgets (e.g. it could have been loaded from a file inside the c++ code)

            unsafe
            {
                IntPtr* NamesPtr = tguiGui_getWidgetNames(CPointer, out uint Count);
                List<string> Names = new List<string>();
                for (uint i = 0; i < Count; ++i)
                    Names.Add(Util.GetStringFromC_UTF32(NamesPtr[i]));

                return Names;
            }
        }

        public void Remove(Widget widget)
        {
            tguiGui_remove(CPointer, widget.CPointer);

            var index = myWidgets.IndexOf(widget);
            if (index != -1)
            {
                myWidgets.RemoveAt(index);
                myWidgetIds.RemoveAt(index);
            }
        }

        public void RemoveAllWidgets()
        {
            tguiGui_removeAllWidgets(CPointer);

            myWidgets.Clear();
            myWidgetIds.Clear();
        }

        public bool TabKeyUsageEnabled
        {
            get { return tguiGui_isTabKeyUsageEnabled(CPointer); }
            set { tguiGui_setTabKeyUsageEnabled(CPointer, value); }
        }

        public void Draw()
        {
            tguiGui_draw(CPointer);
        }

        public float Opacity
        {
            get { return tguiGui_getOpacity(CPointer); }
            set { tguiGui_setOpacity(CPointer, value); }
        }

        public void LoadWidgetsFromFile(string filename)
        {
            if (!tguiGui_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_ASCII(filename)))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        public void SaveWidgetsToFile(string filename)
        {
            if (!tguiGui_saveWidgetsToFile(CPointer, Util.ConvertStringForC_ASCII(filename)))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
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

        private void HandleEvent(Event ev)
        {
            tguiGui_handleEvent(CPointer, ev);
        }


        private RenderWindow myRenderTarget = null;
        private List<Widget> myWidgets = new List<Widget>();
        private List<string> myWidgetIds = new List<string>();


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
        static extern private void tguiGui_handleEvent(IntPtr cPointer, Event ev);

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
