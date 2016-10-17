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
	public abstract class Container : Widget
	{
		protected Container(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Container(Container copy)
			: base(copy)
		{
		}

		public void Add(Widget widget, string widgetName = "")
		{
			tguiContainer_add(CPointer, widget.CPointer, Util.ConvertStringForC_UTF32(widgetName));
		}

		public Widget Get(string widgetName)
		{
			IntPtr WidgetCPointer = tguiContainer_get(CPointer, Util.ConvertStringForC_UTF32(widgetName));
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
				IntPtr* WidgetsPtr = tguiContainer_getWidgets(CPointer, out Count);
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
				IntPtr* NamesPtr = tguiContainer_getWidgetNames(CPointer, out Count);
				List<string> Names = new List<string>();
				for (uint i = 0; i < Count; ++i)
					Names.Add(Util.GetStringFromC_UTF32(NamesPtr[i]));

				return Names;
			}
		}

		public void Remove(Widget widget)
		{
			tguiContainer_remove(CPointer, widget.CPointer);
		}

		public void RemoveAllWidgets()
		{
			tguiContainer_removeAllWidgets(CPointer);
		}

		public Vector2f ChildWidgetsOffset
		{
			get { return tguiContainer_getChildWidgetsOffset(CPointer); }
		}

		public void LoadWidgetsFromFile(string filename)
		{
			IntPtr error;
			tguiContainer_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_ASCII(filename), out error);
			if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		public void SaveWidgetsToFile(string filename)
		{
			IntPtr error;
			tguiContainer_saveWidgetsToFile(CPointer, Util.ConvertStringForC_ASCII(filename), out error);
			if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}


		#region Imports

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiContainer_add(IntPtr cPointer, IntPtr cPointerWidget, IntPtr widgetName);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiContainer_get(IntPtr cPointer, IntPtr widgetName);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		unsafe static extern protected IntPtr* tguiContainer_getWidgets(IntPtr cPointer, out uint count);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		unsafe static extern protected IntPtr* tguiContainer_getWidgetNames(IntPtr cPointer, out uint count);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiContainer_remove(IntPtr cPointer, IntPtr cPointerWidget);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiContainer_removeAllWidgets(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiContainer_getChildWidgetsOffset(IntPtr cPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiContainer_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename, out IntPtr error);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiContainer_saveWidgetsToFile(IntPtr cPointer, IntPtr filename, out IntPtr error);

		#endregion
	}
}
