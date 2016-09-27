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
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
	public class Gui : ObjectBase
	{
		public Gui()
			: base(tguiGui_create())
		{
		}

		public Gui(RenderWindow window)
			: base(tguiGui_create_fromWindow(window.CPointer))
		{
			Window = window;
		}

		protected override void Destroy(bool disposing)
		{
			tguiGui_destroy(CPointer);
		}

		public RenderWindow Window
		{
			set
			{
				tguiGui_setWindow(CPointer, value.CPointer);

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
		}

		public Widget Get(string widgetName)
		{
			IntPtr WidgetCPointer = tguiGui_get(CPointer, Util.ConvertStringForC_UTF32(widgetName));
			if (WidgetCPointer == IntPtr.Zero)
				return null;

			Type type = Type.GetType("TGUI." + Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(WidgetCPointer)));
			return (Widget)Activator.CreateInstance(type, new object[]{ WidgetCPointer });
		}

		public List<Widget> GetWidgets()
		{
			unsafe
			{
				uint Count;
				IntPtr* WidgetsPtr = tguiGui_getWidgets(CPointer, out Count);
				List<Widget> Widgets = new List<Widget>();
				for (uint i = 0; i < Count; ++i)
				{
					IntPtr WidgetCPointer = WidgetsPtr[i];
					Type type = Type.GetType("TGUI." + Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(WidgetCPointer)));
					Widgets.Add((Widget)Activator.CreateInstance(type, new object[]{ WidgetCPointer }));
				}

				return Widgets;
			}
		}

		public List<string> GetWidgetNames()
		{
			unsafe
			{
				uint Count;
				IntPtr* NamesPtr = tguiGui_getWidgetNames(CPointer, out Count);
				List<string> Names = new List<string>();
				for (uint i = 0; i < Count; ++i)
					Names.Add(Util.GetStringFromC_UTF32(NamesPtr[i]));

				return Names;
			}
		}

		public void Remove(Widget widget)
		{
			tguiGui_remove(CPointer, widget.CPointer);
		}

		public void RemoveAllWidgets()
		{
			tguiGui_removeAllWidgets(CPointer);
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
			IntPtr error;
			tguiGui_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_ASCII(filename), out error);
			if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		public void SaveWidgetsToFile(string filename)
		{
			IntPtr error;
			tguiGui_saveWidgetsToFile(CPointer, Util.ConvertStringForC_ASCII(filename), out error);
			if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		private void OnMouseMoved(object sender, MouseMoveEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.MouseMoved;
			ev.MouseMove.X = e.X;
			ev.MouseMove.Y = e.Y;
			HandleEvent(ev);
		}

		private void OnTouchMoved(object sender, TouchEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.TouchMoved;
			ev.Touch.X = e.X;
			ev.Touch.Y = e.Y;
			ev.Touch.Finger = e.Finger;
			HandleEvent(ev);
		}

		private void OnMousePressed(object sender, MouseButtonEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.MouseButtonPressed;
			ev.MouseButton.X = e.X;
			ev.MouseButton.Y = e.Y;
			ev.MouseButton.Button = e.Button;
			HandleEvent(ev);
		}

		private void OnTouchBegan(object sender, TouchEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.TouchBegan;
			ev.Touch.X = e.X;
			ev.Touch.Y = e.Y;
			ev.Touch.Finger = e.Finger;
			HandleEvent(ev);
		}

		private void OnMouseReleased(object sender, MouseButtonEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.MouseButtonReleased;
			ev.MouseButton.X = e.X;
			ev.MouseButton.Y = e.Y;
			ev.MouseButton.Button = e.Button;
			HandleEvent(ev);
		}

		private void OnTouchEnded(object sender, TouchEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.TouchEnded;
			ev.Touch.X = e.X;
			ev.Touch.Y = e.Y;
			ev.Touch.Finger = e.Finger;
			HandleEvent(ev);
		}

		private void OnKeyPressed(object sender, KeyEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.KeyPressed;
			ev.Key.Code = e.Code;
			ev.Key.Control = Convert.ToInt32(e.Control);
			ev.Key.Shift = Convert.ToInt32(e.Shift);
			ev.Key.Alt = Convert.ToInt32(e.Alt);
			ev.Key.System = Convert.ToInt32(e.System);
			HandleEvent(ev);
		}

		private void OnKeyReleased(object sender, KeyEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.KeyReleased;
			ev.Key.Code = e.Code;
			ev.Key.Control = Convert.ToInt32(e.Control);
			ev.Key.Shift = Convert.ToInt32(e.Shift);
			ev.Key.Alt = Convert.ToInt32(e.Alt);
			ev.Key.System = Convert.ToInt32(e.System);
			HandleEvent(ev);
		}

		private void OnTextEntered(object sender, TextEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.TextEntered;
			ev.Text.Unicode = (uint)Char.ConvertToUtf32(e.Unicode, 0);
			HandleEvent(ev);
		}

		private void OnMouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.MouseWheelScrolled;
			ev.MouseWheelScroll.Wheel = e.Wheel;
			ev.MouseWheelScroll.Delta = e.Delta;
			ev.MouseWheelScroll.X = e.X;
			ev.MouseWheelScroll.Y = e.Y;
			HandleEvent(ev);
		}

		private void OnLostFocus(object sender, EventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.LostFocus;
			HandleEvent(ev);
		}

		private void OnGainedFocus(object sender, EventArgs e)
		{
			Event ev = new Event();
			ev.Type = EventType.GainedFocus;
			HandleEvent(ev);
		}

		private void HandleEvent(Event ev)
		{
			tguiGui_handleEvent(CPointer, ev);
		}


		#region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiGui_create();

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiGui_create_fromWindow(IntPtr cPointerRenderWindow);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_destroy(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_setWindow(IntPtr cPointer, IntPtr cPointerRenderWindow);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_setView(IntPtr cPointer, IntPtr cPointerView);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_handleEvent(IntPtr cPointer, Event ev);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_add(IntPtr cPointer, IntPtr cPointerWidget, IntPtr widgetName);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiGui_get(IntPtr cPointer, IntPtr widgetName);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		unsafe static extern protected IntPtr* tguiGui_getWidgets(IntPtr cPointer, out uint count);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		unsafe static extern protected IntPtr* tguiGui_getWidgetNames(IntPtr cPointer, out uint count);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_remove(IntPtr cPointer, IntPtr cPointerWidget);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_removeAllWidgets(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_draw(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_setFont(IntPtr cPointer, IntPtr font);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_setOpacity(IntPtr cPointer, float opacity);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiGui_getOpacity(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename, out IntPtr error);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGui_saveWidgetsToFile(IntPtr cPointer, IntPtr filename, out IntPtr error);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidget_getWidgetType(IntPtr cPointer);

		#endregion
	}
}
