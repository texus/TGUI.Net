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
	public abstract class BoxLayout : Panel
	{
		protected internal BoxLayout(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public BoxLayout(BoxLayout copy)
			: base(copy)
		{
		}

		public bool Insert(uint index, Widget widget, string widgetName = "")
		{
			return tguiBoxLayout_insert(CPointer, index, widget.CPointer, Util.ConvertStringForC_UTF32(widgetName));
		}

		public void AddSpace(float ratio = 1)
		{
			tguiBoxLayout_addSpace(CPointer, ratio);
		}

		public bool InsertSpace(uint index, float ratio = 1)
		{
			return tguiBoxLayout_insertSpace(CPointer, index, ratio);
		}

		public bool Remove(uint index)
		{
			return tguiBoxLayout_removeAtIndex(CPointer, index);
		}

		public Widget Get(uint index)
		{
			IntPtr WidgetCPointer = tguiBoxLayout_getAtIndex(CPointer, index);
			if (WidgetCPointer == IntPtr.Zero)
				return null;

			Type type = Type.GetType("TGUI." + Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(WidgetCPointer)));
			return (Widget)Activator.CreateInstance(type, new object[]{ WidgetCPointer });
		}

		public bool SetRatio(Widget widget, float ratio)
		{
			return tguiBoxLayout_setRatio(CPointer, widget.CPointer, ratio);
		}

		public bool SetRatio(uint index, float ratio)
		{
			return tguiBoxLayout_setRatioAtIndex(CPointer, index, ratio);
		}

		public float GetRatio(Widget widget)
		{
			return tguiBoxLayout_getRatio(CPointer, widget.CPointer);
		}

		public float GetRatio(uint index)
		{
			return tguiBoxLayout_getRatioAtIndex(CPointer, index);
		}

		public bool SetFixedSize(Widget widget, float size)
		{
			return tguiBoxLayout_setFixedSize(CPointer, widget.CPointer, size);
		}

		public bool SetFixedSize(uint index, float size)
		{
			return tguiBoxLayout_setFixedSizeAtIndex(CPointer, index, size);
		}

		public float GetFixedSize(Widget widget)
		{
			return tguiBoxLayout_getFixedSize(CPointer, widget.CPointer);
		}

		public float GetFixedSize(uint index)
		{
			return tguiBoxLayout_getFixedSizeAtIndex(CPointer, index);
		}

		#region Imports

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiBoxLayout_insert(IntPtr cPointer, uint index, IntPtr widgetCPointer, IntPtr widgetName);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiBoxLayout_addSpace(IntPtr cPointer, float ratio);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiBoxLayout_insertSpace(IntPtr cPointer, uint index, float ratio);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiBoxLayout_removeAtIndex(IntPtr cPointer, uint index);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiBoxLayout_getAtIndex(IntPtr cPointer, uint index);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiBoxLayout_setRatio(IntPtr cPointer, IntPtr widgetCPointer, float ratio);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiBoxLayout_setRatioAtIndex(IntPtr cPointer, uint index, float ratio);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiBoxLayout_getRatio(IntPtr cPointer, IntPtr widgetCPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiBoxLayout_getRatioAtIndex(IntPtr cPointer, uint index);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiBoxLayout_setFixedSize(IntPtr cPointer, IntPtr widgetCPointer, float size);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiBoxLayout_setFixedSizeAtIndex(IntPtr cPointer, uint index, float size);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiBoxLayout_getFixedSize(IntPtr cPointer, IntPtr widgetCPointer);

		[DllImport("ctgui-0.8", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected float tguiBoxLayout_getFixedSizeAtIndex(IntPtr cPointer, uint index);

		#endregion
	}
}
