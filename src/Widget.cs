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
using SFML.System;

namespace TGUI
{
	public class Widget : ObjectBase
	{
		protected Widget(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Widget(Widget copy)
			: base(tguiWidget_copy(copy.CPointer))
		{
		}

		protected override void Destroy(bool disposing)
		{
			tguiWidget_destroy(CPointer);
		}

		public Vector2f Position
		{
			get { return tguiWidget_getPosition(CPointer); }
			set { tguiWidget_setPosition(CPointer, value); }
		}

		public void SetPosition(Layout2d layout)
		{
			tguiWidget_setPosition_fromLayout(CPointer, layout.CPointer);
		}

		public Vector2f AbsolultePosition
		{
			get { return tguiWidget_getAbsolutePosition(CPointer); }
		}

		public Vector2f Size
		{
			get { return tguiWidget_getSize(CPointer); }
			set { tguiWidget_setSize(CPointer, value); }
		}

		public void SetSize(Layout2d layout)
		{
			tguiWidget_setSize_fromLayout(CPointer, layout.CPointer);
		}

		public Vector2f FullSize
		{
			get { return tguiWidget_getFullSize(CPointer); }
		}

		public Vector2f WidgetOffset
		{
			get { return tguiWidget_getWidgetOffset(CPointer); }
		}

		public void Connect(string signalName, Action func)
		{
			IntPtr error;
			tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII(signalName), func, out error);
			if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		public WidgetRenderer Renderer
		{
			get { return new WidgetRenderer(tguiWidget_getRenderer(CPointer)); }
		}

		public void SetRenderer(RendererData renderer)
		{
			IntPtr error;
			tguiWidget_setRenderer(CPointer, renderer.CPointer, out error);
			if (error != IntPtr.Zero)
				throw new TGUIException(Util.GetStringFromC_ASCII(error));
		}

		public bool Visible
		{
			get { return tguiWidget_isVisible(CPointer); }
			set
			{
				if (value == true)
					tguiWidget_show(CPointer);
				else
					tguiWidget_hide(CPointer);
			}
		}

		public void ShowWithEffect(ShowAnimationType type, Time duration)
		{
			tguiWidget_showWithEffect(CPointer, type, duration);
		}

		public void HideWithEffect(ShowAnimationType type, Time duration)
		{
			tguiWidget_hideWithEffect(CPointer, type, duration);
		}

		public bool Enabled
		{
			get { return tguiWidget_isEnabled(CPointer); }
			set
			{
				if (value == true)
					tguiWidget_enable(CPointer);
				else
					tguiWidget_disable(CPointer);
			}
		}

		public string WidgetType
		{
			get { return Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(CPointer)); }
		}

		public Container Parent
		{
			get
			{
				IntPtr ParentCPointer = tguiWidget_getParent(CPointer);
				if (ParentCPointer == IntPtr.Zero)
					return null;

				Type type = Type.GetType("TGUI." + Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(ParentCPointer)));
				return (Container)Activator.CreateInstance(type, new object[]{ ParentCPointer });
			}
		}

		public void MoveToFront()
		{
			tguiWidget_moveToFront(CPointer);
		}

		public void MoveToBack()
		{
			tguiWidget_moveToBack(CPointer);
		}

		// TODO: ToolTip

		#region Imports

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiWidget_copy(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_destroy(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_setPosition(IntPtr cPointer, Vector2f pos);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_setPosition_fromLayout(IntPtr cPointer, IntPtr layout2d);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Vector2f tguiWidget_getPosition(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Vector2f tguiWidget_getAbsolutePosition(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Vector2f tguiWidget_getWidgetOffset(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_setSize(IntPtr cPointer, Vector2f size);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_setSize_fromLayout(IntPtr cPointer, IntPtr layout2d);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Vector2f tguiWidget_getSize(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern Vector2f tguiWidget_getFullSize(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_connect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] Action func, out IntPtr error);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_setRenderer(IntPtr cPointer, IntPtr rendererDataCPointer, out IntPtr error);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiWidget_getRenderer(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_show(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_showWithEffect(IntPtr cPointer, ShowAnimationType type, Time duration);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_hide(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_hideWithEffect(IntPtr cPointer, ShowAnimationType type, Time duration);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern bool tguiWidget_isVisible(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_enable(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_disable(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern bool tguiWidget_isEnabled(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiWidget_getWidgetType(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_moveToFront(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern void tguiWidget_moveToBack(IntPtr cPointer);

		[DllImport("ctgui", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern IntPtr tguiWidget_getParent(IntPtr cPointer);

		#endregion
	}
}
