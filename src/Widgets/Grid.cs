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
using System.Security;
using System.Runtime.InteropServices;

namespace TGUI
{
	public class Grid : Container
	{
		public Grid()
			: base(tguiGrid_create())
		{
		}

		protected internal Grid(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Grid(Grid copy)
			: base(copy)
		{
		}

		public void AddWidget(Widget widget, uint row, uint col)
		{
			tguiGrid_addWidget(CPointer, widget.CPointer, row, col, (new Outline()).CPointer, Alignment.Center);
		}

		public void AddWidget(Widget widget, uint row, uint col, Outline borders, Alignment alignment = Alignment.Center)
		{
			tguiGrid_addWidget(CPointer, widget.CPointer, row, col, borders.CPointer, alignment);
		}

		public Widget GetWidget(uint row, uint col)
		{
			IntPtr WidgetCPointer = tguiGrid_getWidget(CPointer, row, col);
			if (WidgetCPointer == IntPtr.Zero)
				return null;

			Type type = Type.GetType("TGUI." + Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(WidgetCPointer)));
			return (Widget)Activator.CreateInstance(type, new object[]{ WidgetCPointer });
		}

		public void SetWidgetBorders(Widget widget, Outline borders)
		{
			tguiGrid_setWidgetBorders(CPointer, widget.CPointer, borders.CPointer);
		}

		public void SetWidgetBorders(uint row, uint col, Outline borders)
		{
			tguiGrid_setWidgetBordersByCell(CPointer, row, col, borders.CPointer);
		}

		public Outline GetWidgetBorders(Widget widget)
		{
			return new Outline(tguiGrid_getWidgetBorders(CPointer, widget.CPointer));
		}

		public Outline GetWidgetBorders(uint row, uint col)
		{
			return new Outline(tguiGrid_getWidgetBordersByCell(CPointer, row, col));
		}

		public void SetWidgetAlignment(Widget widget, Alignment alignment)
		{
			tguiGrid_setWidgetAlignment(CPointer, widget.CPointer, alignment);
		}

		public void SetWidgetAlignment(uint row, uint col, Alignment alignment)
		{
			tguiGrid_setWidgetAlignmentByCell(CPointer, row, col, alignment);
		}

		public Alignment GetWidgetAlignment(Widget widget)
		{
			return tguiGrid_getWidgetAlignment(CPointer, widget.CPointer);
		}

		public Alignment GetWidgetAlignment(uint row, uint col)
		{
			return tguiGrid_getWidgetAlignmentByCell(CPointer, row, col);
		}


		#region Imports

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiGrid_create();

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGrid_addWidget(IntPtr cPointer, IntPtr widgetCPointer, uint row, uint col, IntPtr bordersCPointer, Alignment alignment);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiGrid_getWidget(IntPtr cPointer, uint row, uint col);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGrid_setWidgetBorders(IntPtr cPointer, IntPtr widgetCPointer, IntPtr bordersCPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGrid_setWidgetBordersByCell(IntPtr cPointer, uint row, uint col, IntPtr bordersCPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiGrid_getWidgetBorders(IntPtr cPointer, IntPtr widgetCPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiGrid_getWidgetBordersByCell(IntPtr cPointer, uint row, uint col);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGrid_setWidgetAlignment(IntPtr cPointer, IntPtr widgetCPointer, Alignment alignment);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiGrid_setWidgetAlignmentByCell(IntPtr cPointer, uint row, uint col, Alignment alignment);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Alignment tguiGrid_getWidgetAlignment(IntPtr cPointer, IntPtr widgetCPointer);

		[DllImport("ctgui-0.8.dll", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Alignment tguiGrid_getWidgetAlignmentByCell(IntPtr cPointer, uint row, uint col);

		#endregion
	}
}
