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

namespace TGUI
{
	public class Widget : SFML.ObjectBase
	{
		protected Widget(IntPtr cPointer)
			: base(cPointer)
		{
			InitSignals(); // Calls the function in the derived class
		}

		public Widget(Widget copy)
			: base(tguiWidget_copy(copy.CPointer))
		{
			InitSignals(); // Calls the function in the derived class
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
			tguiWidget_setPositionFromLayout(CPointer, layout.CPointer);
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
			tguiWidget_setSizeFromLayout(CPointer, layout.CPointer);
		}

		public Vector2f FullSize
		{
			get { return tguiWidget_getFullSize(CPointer); }
		}

		public Vector2f WidgetOffset
		{
			get { return tguiWidget_getWidgetOffset(CPointer); }
		}

		public uint Connect(string signalName, Action func)
		{
			uint id = tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII(signalName), () => func());
			if (id == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

			// Add the function to our dictionary
			signalName = signalName.ToLower();
			if (!myConnectedSignals.ContainsKey(signalName))
				myConnectedSignals[signalName] = new List<uint>();

			myConnectedSignals[signalName].Add(id);

			return id;
		}

		public uint Connect(string signalName, Action<Widget> func)
		{
			return Connect(signalName, () => func(this));
		}

        public uint Connect(string signalName, Action<Widget, string> func)
		{
			return Connect(signalName, () => func(this, signalName));
		}

		public void Disconnect(uint id)
		{
			tguiWidget_disconnect(CPointer, id);

            foreach (var signal in myConnectedSignals)
            {
                if (signal.Value.Contains(id))
                {
                    if (signal.Value.Count > 1)
					    signal.Value.Remove(id);
				    else
					    myConnectedSignals.Remove(signal.Key);

					break;
                }
            }
		}

		public void DisconnectAll(string signalName)
		{
			signalName = signalName.ToLower();
			if (myConnectedSignals.ContainsKey(signalName))
				myConnectedSignals.Remove(signalName);

            tguiWidget_disconnectAll(CPointer, Util.ConvertStringForC_ASCII(signalName));
		}

        public void DisconnectAll()
		{
			myConnectedSignals.Clear();
            tguiWidget_disconnectAll(CPointer, IntPtr.Zero);
		}

		public WidgetRenderer Renderer
		{
			get { return new WidgetRenderer(tguiWidget_getRenderer(CPointer)); }
		}

        public WidgetRenderer SharedRenderer
		{
			get { return new WidgetRenderer(tguiWidget_getSharedRenderer(CPointer)); }
		}

		public void SetRenderer(RendererData renderer)
		{
            if (!tguiWidget_setRenderer(CPointer, renderer.CPointer))
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
		}

		public bool Visible
		{
			get { return tguiWidget_isVisible(CPointer); }
			set { tguiWidget_setVisible(CPointer, value); }
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
			set { tguiWidget_setEnabled(CPointer, value); }
		}

        public bool Focus
        {
            get { return tguiWidget_isFocused(CPointer); }
            set { tguiWidget_setFocused(CPointer, value); }
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

		public Widget ToolTip
		{
			get
			{
				IntPtr ToolTipCPointer = tguiWidget_getToolTip(CPointer);
				if (ToolTipCPointer == IntPtr.Zero)
					return null;

				Type type = Type.GetType("TGUI." + Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(ToolTipCPointer)));
				return (Widget)Activator.CreateInstance(type, new object[]{ ToolTipCPointer });
			}
			set
			{
				if (value != null)
					tguiWidget_setToolTip(CPointer, value.CPointer);
				else
					tguiWidget_setToolTip(CPointer, IntPtr.Zero);
			}
		}
		
		public bool MouseOnWidget(Vector2f pos)
		{
			return tguiWidget_mouseOnWidget(CPointer, pos);
		}


		////////////////////////////////////////////////////////////
		/// <summary>
		/// Provide a string describing the object
		/// </summary>
		/// <returns>String description of the object</returns>
		////////////////////////////////////////////////////////////
		public override string ToString()
		{
			return "[Widget] Type(" + WidgetType + ")";
		}


		protected virtual void InitSignals()
		{
            PositionChangedCallback = new CallbackActionVector2f(ProcessPositionChangedSignal);
            if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("PositionChanged"), PositionChangedCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

		    SizeChangedCallback = new CallbackActionVector2f(ProcessSizeChangedSignal);
		    if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("SizeChanged"), SizeChangedCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

		    MouseEnteredCallback = new CallbackAction(ProcessMouseEnteredSignal);
		    if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("MouseEntered"), MouseEnteredCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

		    MouseLeftCallback = new CallbackAction(ProcessMouseLeftSignal);
		    if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("MouseLeft"), MouseLeftCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            FocusedCallback = new CallbackAction(ProcessFocusedSignal);
		    if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("Focused"), FocusedCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

		    UnfocusedCallback = new CallbackAction(ProcessUnfocusedSignal);
		    if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("Unfocused"), UnfocusedCallback) == 0)
				throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
		}

		private void ProcessPositionChangedSignal(Vector2f pos)
		{
            PositionChanged?.Invoke(this, new SignalArgsVector2f(pos));
        }

		private void ProcessSizeChangedSignal(Vector2f pos)
		{
            SizeChanged?.Invoke(this, new SignalArgsVector2f(pos));
        }

		private void ProcessMouseEnteredSignal()
		{
            MouseEntered?.Invoke(this, EventArgs.Empty);
        }

		private void ProcessMouseLeftSignal()
		{
            MouseLeft?.Invoke(this, EventArgs.Empty);
        }

        private void ProcessFocusedSignal()
		{
            Focused?.Invoke(this, EventArgs.Empty);
        }

		private void ProcessUnfocusedSignal()
		{
            Unfocused?.Invoke(this, EventArgs.Empty);
        }

		/// <summary>Event handler for the Clicked signal</summary>
		public event EventHandler<SignalArgsVector2f> PositionChanged = null;

		/// <summary>Event handler for the Clicked signal</summary>
		public event EventHandler<SignalArgsVector2f> SizeChanged = null;

		/// <summary>Event handler for the MouseEntered signal</summary>
		public event EventHandler MouseEntered = null;

		/// <summary>Event handler for the MouseLeft signal</summary>
		public event EventHandler MouseLeft = null;

        /// <summary>Event handler for the Focused signal</summary>
		public event EventHandler Focused = null;

		/// <summary>Event handler for the Unfocused signal</summary>
		public event EventHandler Unfocused = null;

	    private CallbackActionVector2f PositionChangedCallback;
	    private CallbackActionVector2f SizeChangedCallback;
	    private CallbackAction         MouseEnteredCallback;
	    private CallbackAction         MouseLeftCallback;
	    private CallbackAction         FocusedCallback;
	    private CallbackAction         UnfocusedCallback;


	    protected Dictionary<string, List<uint>> myConnectedSignals = new Dictionary<string, List<uint>>();


		protected delegate void CallbackAction();
		protected delegate void CallbackActionVector2f(Vector2f param);
		protected delegate void CallbackActionString(IntPtr param);
		protected delegate void CallbackActionInt(int param);
		protected delegate void CallbackActionUInt(uint param);
		protected delegate void CallbackActionFloat(float param);
		protected delegate void CallbackActionRange(float param1, float param2);
		protected delegate void CallbackActionItemSelected(IntPtr param1, IntPtr param2);

		#region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tgui_getLastError();

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidget_copy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_destroy(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_setPosition(IntPtr cPointer, Vector2f pos);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_setPositionFromLayout(IntPtr cPointer, IntPtr layout2d);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiWidget_getPosition(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiWidget_getAbsolutePosition(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiWidget_getWidgetOffset(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_setSize(IntPtr cPointer, Vector2f size);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_setSizeFromLayout(IntPtr cPointer, IntPtr layout2d);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiWidget_getSize(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected Vector2f tguiWidget_getFullSize(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiWidget_connect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackAction func);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiWidget_connectVector2f(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionVector2f func);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiWidget_connectString(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionString func);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiWidget_connectInt(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionInt func);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiWidget_connectUInt(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionUInt func);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiWidget_connectFloat(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionFloat func);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiWidget_connectRange(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionRange func);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected uint tguiWidget_connectItemSelected(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionItemSelected func);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_disconnect(IntPtr cPointer, uint id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_disconnectAll(IntPtr cPointer, IntPtr signalName);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiWidget_setRenderer(IntPtr cPointer, IntPtr rendererDataCPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidget_getRenderer(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidget_getSharedRenderer(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_setVisible(IntPtr cPointer, bool visible);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiWidget_isVisible(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_showWithEffect(IntPtr cPointer, ShowAnimationType type, Time duration);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_hideWithEffect(IntPtr cPointer, ShowAnimationType type, Time duration);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_setEnabled(IntPtr cPointer, bool enabled);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiWidget_isEnabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiWidget_setFocused(IntPtr cPointer, bool focused);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiWidget_isFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidget_getWidgetType(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_moveToFront(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_moveToBack(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected void tguiWidget_setToolTip(IntPtr cPointer, IntPtr toolTipCPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidget_getToolTip(IntPtr cPointer);

		[DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected IntPtr tguiWidget_getParent(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		static extern protected bool tguiWidget_mouseOnWidget(IntPtr cPointer, Vector2f pos);

		#endregion
	}
}
