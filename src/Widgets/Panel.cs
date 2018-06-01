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
using SFML.System;

namespace TGUI
{
	public class Panel : Group
	{
		public Panel()
			: base(tguiPanel_create())
		{
		}

		public Panel(Vector2f size)
			: this()
		{
			Size = size;
		}

		public Panel(float width, float height)
			: this(new Vector2f(width, height))
		{
		}

		protected internal Panel(IntPtr cPointer)
			: base(cPointer)
		{
		}

		public Panel(Panel copy)
			: base(copy)
		{
		}

		public new PanelRenderer Renderer
		{
			get { return new PanelRenderer(tguiWidget_getRenderer(CPointer)); }
		}

        public new PanelRenderer SharedRenderer
		{
			get { return new PanelRenderer(tguiWidget_getSharedRenderer(CPointer)); }
		}

		protected override void InitSignals()
		{
			base.InitSignals();

            MousePressedCallback = new CallbackActionVector2f(ProcessMousePressedSignal);
		    if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("MousePressed"), MousePressedCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

		    MouseReleasedCallback = new CallbackActionVector2f(ProcessMouseReleasedSignal);
		    if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("MouseReleased"), MouseReleasedCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

		    ClickedCallback = new CallbackActionVector2f(ProcessClickedSignal);
		    if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("Clicked"), ClickedCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
		}

		private void ProcessMousePressedSignal(Vector2f pos)
		{
            MousePressed?.Invoke(this, new SignalArgsVector2f(pos));
        }

		private void ProcessMouseReleasedSignal(Vector2f pos)
		{
            MouseReleased?.Invoke(this, new SignalArgsVector2f(pos));
        }

		private void ProcessClickedSignal(Vector2f pos)
		{
            Clicked?.Invoke(this, new SignalArgsVector2f(pos));
        }

		/// <summary>Event handler for the MousePressed signal</summary>
		public event EventHandler<SignalArgsVector2f> MousePressed = null;

		/// <summary>Event handler for the MouseReleased signal</summary>
		public event EventHandler<SignalArgsVector2f> MouseReleased = null;

		/// <summary>Event handler for the Clicked signal</summary>
		public event EventHandler<SignalArgsVector2f> Clicked = null;

	    private CallbackActionVector2f MousePressedCallback;
	    private CallbackActionVector2f MouseReleasedCallback;
	    private CallbackActionVector2f ClickedCallback;


	    #region Imports

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiPanel_create();

		#endregion
	}
}
