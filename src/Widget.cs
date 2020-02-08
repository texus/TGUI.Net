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
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SFML.System;

namespace TGUI
{
    /// <summary>
    /// Base class for all widgets
    /// </summary>
    public class Widget : SFML.ObjectBase
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected Widget(IntPtr cPointer)
            : base(cPointer)
        {
            InitSignals(); // Calls the function in the derived class
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Widget(Widget copy)
            : base(tguiWidget_copy(copy.CPointer))
        {
            InitSignals(); // Calls the function in the derived class
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiWidget_destroy(CPointer);
        }

        /// <summary>
        /// Gets or sets the position of the widget inside its parent
        /// </summary>
        public Vector2f Position
        {
            get { return tguiWidget_getPosition(CPointer); }
            set { tguiWidget_setPosition(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the position of the widget inside its parent as a layout
        /// </summary>
        public Layout2d PositionLayout
        {
            set { tguiWidget_setPositionFromLayout(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Sets the position of the widget inside its parent
        /// </summary>
        /// <param name="layout">Position of the widget that could be fixed or relative to the parent size</param>
        [Obsolete("Use PositionLayout property instead")]
        public void SetPosition(Layout2d layout)
        {
            tguiWidget_setPositionFromLayout(CPointer, layout.CPointer);
        }

        /// <summary>
        /// Gets the absolute position of the widget instead of the relative position in its parent
        /// </summary>
        public Vector2f AbsolutePosition
        {
            get { return tguiWidget_getAbsolutePosition(CPointer); }
        }

        /// <summary>
        /// Gets or sets the size of the widget
        /// </summary>
        public Vector2f Size
        {
            get { return tguiWidget_getSize(CPointer); }
            set { tguiWidget_setSize(CPointer, value); }
        }

        /// <summary>
        /// Sets the size of the widget as a layout
        /// </summary>
        public Layout2d SizeLayout
        {
            set { tguiWidget_setSizeFromLayout(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Sets the size of the widget
        /// </summary>
        /// <param name="layout">Size of the widget that could be fixed or relative to the parent size</param>
        [Obsolete("Use SizeLayout property instead")]
        public void SetSize(Layout2d layout)
        {
            tguiWidget_setSizeFromLayout(CPointer, layout.CPointer);
        }

        /// <summary>
        /// Gets the entire size that the widget is using
        /// </summary>
        /// <remarks>
        /// The full size will equal the size for most widgets.
        /// E.g a check box where the check mark leaves the box will have a larger full size. 
        /// </remarks>
        public Vector2f FullSize
        {
            get { return tguiWidget_getFullSize(CPointer); }
        }

        /// <summary>
        /// Gets the distance between the position where the widget is drawn and where the widget is placed
        /// </summary>
        /// <remarks>
        /// The offset is (0,0) for almost all widgets.
        /// E.g. a check box where the check mark leaves the box will have a negative vertical offset. 
        /// </remarks>
        public Vector2f WidgetOffset
        {
            get { return tguiWidget_getWidgetOffset(CPointer); }
        }

        /// <summary>
        /// Connect a signal handler that will be called when the signal is emitted
        /// </summary>
        /// <param name="signalName">Name of the signal to connect</param>
        /// <param name="func">Signal handler, taking no parameters</param>
        /// <returns>Unique id of the connection</returns>
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

        /// <summary>
        /// Connect a signal handler that will be called when the signal is emitted
        /// </summary>
        /// <param name="signalName">Name of the signal to connect</param>
        /// <param name="func">Signal handler, taking the widget as parameter</param>
        /// <returns>Unique id of the connection</returns>
        public uint Connect(string signalName, Action<Widget> func)
        {
            return Connect(signalName, () => func(this));
        }

        /// <summary>
        /// Connect a signal handler that will be called when the signal is emitted
        /// </summary>
        /// <param name="signalName">Name of the signal to connect</param>
        /// <param name="func">Signal handler, taking the widget and signal name as parameters</param>
        /// <returns>Unique id of the connection</returns>
        public uint Connect(string signalName, Action<Widget, string> func)
        {
            return Connect(signalName, () => func(this, signalName));
        }

        /// <summary>
        /// Disconnects a signal handler
        /// </summary>
        /// <param name="id">Id of the connection (returned by the Connect functions)</param>
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

        /// <summary>
        /// Disconnects all signal handler from a certain signal
        /// </summary>
        /// <param name="signalName">Name of the signal</param>
        public void DisconnectAll(string signalName)
        {
            signalName = signalName.ToLower();
            if (myConnectedSignals.ContainsKey(signalName))
                myConnectedSignals.Remove(signalName);

            tguiWidget_disconnectAll(CPointer, Util.ConvertStringForC_ASCII(signalName));
        }

        /// <summary>
        /// Disconnects all signal handlers from all signals
        /// </summary>
        public void DisconnectAll()
        {
            myConnectedSignals.Clear();
            tguiWidget_disconnectAll(CPointer, IntPtr.Zero);
        }

        /// <summary>
        /// Gets or sets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After retrieving the renderer, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public WidgetRenderer Renderer
        {
            get { return new WidgetRenderer(tguiWidget_getRenderer(CPointer)); }
            set { SetRenderer(value.Data); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public WidgetRenderer SharedRenderer
        {
            get { return new WidgetRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Sets new renderer data for the widget. The renderer determines how the widget looks.
        /// </summary>
        /// <param name="rendererData">new renderer data</param>
        /// <remarks>
        /// The renderer data is shared with this widget. When the data is changed, this widget will be updated as well.
        /// </remarks>
        public void SetRenderer(RendererData rendererData)
        {
            if (!tguiWidget_setRenderer(CPointer, rendererData.CPointer))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        /// <summary>
        /// Gets or sets whether the widget is shown or hidden
        /// </summary>
        public bool Visible
        {
            get { return tguiWidget_isVisible(CPointer); }
            set { tguiWidget_setVisible(CPointer, value); }
        }

        /// <summary>
        /// Shows the widget by introducing it with an animation
        /// </summary>
        /// <param name="type">Type of the animation</param>
        /// <param name="duration">Duration of the animation</param>
        /// <remarks>
        /// The animation will also be played if the widget was already visible.
        ///
        /// During the animation the position, size and/or opacity may change. Once the animation is done the widget will
        /// be back in the state in which it was when this function was called.
        /// </remarks>
        public void ShowWithEffect(ShowAnimationType type, Time duration)
        {
            tguiWidget_showWithEffect(CPointer, type, duration);
        }

        /// <summary>
        /// Hides the widget by making it leave with an animation
        /// </summary>
        /// <param name="type">Type of the animation</param>
        /// <param name="duration">Duration of the animation</param>
        /// <remarks>
        /// If the widget is already hidden then the animation will still play but you will not see it.
        ///
        /// During the animation the position, size and/or opacity may change. Once the animation is done the widget will
        /// be back in the state in which it was when this function was called, except that it will no longer be visible.
        /// </remarks>
        public void HideWithEffect(ShowAnimationType type, Time duration)
        {
            tguiWidget_hideWithEffect(CPointer, type, duration);
        }

        /// <summary>
        /// Gets or sets whether the widget is enabled
        /// </summary>
        public bool Enabled
        {
            get { return tguiWidget_isEnabled(CPointer); }
            set { tguiWidget_setEnabled(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the widget currently has focus
        /// </summary>
        /// <remarks>
        /// When a widget is focused, the previously focused widget will be unfocused.
        /// Note that setting the property only works properly when the widget was already added to its parent (e.g. the Gui).
        /// </remarks>
        public bool Focus
        {
            get { return tguiWidget_isFocused(CPointer); }
            set { tguiWidget_setFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the widget can be focused
        /// </summary>
        /// <remarks>
        /// By default all widgets are focusable
        /// </remarks>
        public bool Focusable
        {
            get { return tguiWidget_isFocusable(CPointer); }
            set { tguiWidget_setFocusable(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the custom data to store inside the widget
        /// </summary>
        public object UserData
        {
            get { return myUserData; }
            set { myUserData = value; }
        }

        /// <summary>
        /// Gets the type of the widget
        /// </summary>
        public string WidgetType
        {
            get { return Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(CPointer)); }
        }

        /// <summary>
        /// Gets the parent to which the widget was added
        /// </summary>
        /// <remarks>
        /// The setter is only intended for internal use.
        /// </remarks>
        public Container Parent
        {
            get { return myParent; }
            set { myParent = value; }
        }

        /// <summary>
        /// Gets whether there is an active animation (started with ShowWithEffect or HideWithEffect)
        /// </summary>
        public bool AnimationPlaying
        {
            get { return tguiWidget_isAnimationPlaying(CPointer); }
        }

        /// <summary>
        /// Places the widget before all other widgets in its parent
        /// </summary>
        /// <remarks>
        /// Note that this function only has an effect when the widget was already added to its parent (e.g. the Gui).
        /// </remarks>
        public void MoveToFront()
        {
            if (myParent != null)
                myParent.MoveWidgetToFront(this);
            else if (myParentGui != null)
                myParentGui.MoveWidgetToFront(this);
        }

        /// <summary>
        /// Places the widget behind all other widgets in its parent
        /// </summary>
        /// <remarks>
        /// Note that this function only has an effect when the widget was already added to its parent (e.g. the Gui).
        /// </remarks>
        public void MoveToBack()
        {
            if (myParent != null)
                myParent.MoveWidgetToBack(this);
            else if (myParentGui != null)
                myParentGui.MoveWidgetToBack(this);
        }

        /// <summary>
        /// Gets or sets the tool tip that should be displayed when hovering over the widget
        /// </summary>
        public Widget ToolTip
        {
            get { return Util.GetWidgetFromC(tguiWidget_getToolTip(CPointer), ParentGui); }
            set
            {
                if (value != null)
                    tguiWidget_setToolTip(CPointer, value.CPointer);
                else
                    tguiWidget_setToolTip(CPointer, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Gets or sets the character size of the text in this widget if it uses text
        /// </summary>
        public uint TextSize
        {
            get { return tguiWidget_getTextSize(CPointer); }
            set { tguiWidget_setTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the name of a widget
        /// </summary>
        /// <remarks>
        /// This name is overwritten when adding the widget to its parent. You should only change it afterwards.
        /// </remarks>
        public string Name
        {
            get { return Util.GetStringFromC_UTF32(tguiWidget_getName(CPointer)); }
            set { tguiWidget_setName(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        /// <summary>
        /// Gets whether the mouse position lies on top of the widget
        /// </summary>
        /// <param name="pos">Mouse position, relative to the parent widget</param>
        /// <returns>Is the mouse on top of the widget?</returns>
        public bool MouseOnWidget(Vector2f pos)
        {
            return tguiWidget_mouseOnWidget(CPointer, pos);
        }


        /// <summary>
        /// Gets the gui to which the widget was added.
        /// </summary>
        /// <remarks>
        /// The setter is only intended for internal use.
        /// </remarks>
        public virtual Gui ParentGui
        {
            get { return myParentGui; }
            set { myParentGui = value; }
        }

        /// <summary>
        /// Enables or disables certain signals (e.g. to change a property without triggering the signal)
        /// </summary>
        /// <param name="signalName">Signal that should be enabled/disabled</param>
        /// <param name="enabled">Whether the signal needs to be enabled or disabled</param>
        public void SetSignalEnabled(string signalName, bool enabled)
        {
            tguiWidget_setSignalEnabled(CPointer, Util.ConvertStringForC_ASCII(signalName), enabled);
        }


        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[Widget] Type(" + WidgetType + ")";
        }

        /// <summary>
        /// Helper function to throw an error if connecting signal failed and store the signal
        /// in a list for signals that need to be disconnected when the class instance is destroyed.
        /// </summary>
        /// <param name="callbackId">Unique id of the callback</param>
        protected void AddInternalSignal(uint callbackId)
        {
            if (callbackId == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            myInternalSignalIds.Add(callbackId);
        }

        /// <summary>
        /// Helper function to trigger an event without parameters
        /// <param name="eventKey">Unique event identifier</param>
        /// </summary>
        protected void SendSignal(object eventKey)
        {
            if (myEventHandlerList[eventKey] != null)
                ((EventHandler)myEventHandlerList[eventKey])(this, EventArgs.Empty);
        }

        /// <summary>
        /// Helper function to trigger an event of type EventHandler<EventArgsType>
        /// <param name="eventKey">Unique event identifier</param>
        /// <param name="eventArgs">Arguments for the callback</param>
        /// </summary>
        protected void SendSignal<EventArgsType>(object eventKey, EventArgsType eventArgs) where EventArgsType : EventArgs
        {
            if (myEventHandlerList[eventKey] != null)
                ((EventHandler<EventArgsType>)myEventHandlerList[eventKey])(this, eventArgs);
        }

        /// <summary>
        /// Initializes the internal signals
        /// </summary>
        protected virtual void InitSignals()
        {
            PositionChangedCallback = new CallbackActionVector2f((pos) => SendSignal(myPositionChangedEventKey, new SignalArgsVector2f(pos)));
            AddInternalSignal(tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("PositionChanged"), PositionChangedCallback));

            SizeChangedCallback = new CallbackActionVector2f((size) => SendSignal(mySizeChangedEventKey, new SignalArgsVector2f(size)));
            AddInternalSignal(tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("SizeChanged"), SizeChangedCallback));

            MouseEnteredCallback = new CallbackAction(() => SendSignal(myMouseEnteredEventKey));
            AddInternalSignal(tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("MouseEntered"), MouseEnteredCallback));

            MouseLeftCallback = new CallbackAction(() => SendSignal(myMouseLeftEventKey));
            AddInternalSignal(tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("MouseLeft"), MouseLeftCallback));

            FocusedCallback = new CallbackAction(() => SendSignal(myFocusedEventKey));
            AddInternalSignal(tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("Focused"), FocusedCallback));

            UnfocusedCallback = new CallbackAction(() => SendSignal(myUnfocusedEventKey));
            AddInternalSignal(tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("Unfocused"), UnfocusedCallback));

            AnimationFinishedCallback = new CallbackActionAnimation((type, visible) => SendSignal(myAnimationFinishedEventKey, new SignalArgsAnimation(type, visible)));
            AddInternalSignal(tguiWidget_connectAnimation(CPointer, Util.ConvertStringForC_ASCII("AnimationFinished"), AnimationFinishedCallback));
        }

        /// <summary>
        /// Disconnects the internal signals
        /// </summary>
        protected void DeinitSignals()
        {
            foreach (var id in myInternalSignalIds)
                tguiWidget_disconnect(CPointer, id);
        }

        /// <summary>Event handler for the Clicked signal</summary>
        public event EventHandler<SignalArgsVector2f> PositionChanged
        {
            add { myEventHandlerList.AddHandler(myPositionChangedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myPositionChangedEventKey, value); }
        }

        /// <summary>Event handler for the Clicked signal</summary>
        public event EventHandler<SignalArgsVector2f> SizeChanged
        {
            add { myEventHandlerList.AddHandler(mySizeChangedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(mySizeChangedEventKey, value); }
        }

        /// <summary>Event handler for the MouseEntered signal</summary>
        public event EventHandler MouseEntered
        {
            add { myEventHandlerList.AddHandler(myMouseEnteredEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myMouseEnteredEventKey, value); }
        }

        /// <summary>Event handler for the MouseLeft signal</summary>
        public event EventHandler MouseLeft
        {
            add { myEventHandlerList.AddHandler(myMouseLeftEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myMouseLeftEventKey, value); }
        }

        /// <summary>Event handler for the Focused signal</summary>
        public event EventHandler Focused
        {
            add { myEventHandlerList.AddHandler(myFocusedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myFocusedEventKey, value); }
        }

        /// <summary>Event handler for the Unfocused signal</summary>
        public event EventHandler Unfocused
        {
            add { myEventHandlerList.AddHandler(myFocusedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myFocusedEventKey, value); }
        }

        /// <summary>Event handler for the AnimationFinished signal</summary>
        public event EventHandler<SignalArgsAnimation> AnimationFinished
        {
            add { myEventHandlerList.AddHandler(myAnimationFinishedEventKey, value); }
            remove { myEventHandlerList.RemoveHandler(myAnimationFinishedEventKey, value); }
        }

        private CallbackActionVector2f  PositionChangedCallback;
        private CallbackActionVector2f  SizeChangedCallback;
        private CallbackAction          MouseEnteredCallback;
        private CallbackAction          MouseLeftCallback;
        private CallbackAction          FocusedCallback;
        private CallbackAction          UnfocusedCallback;
        private CallbackActionAnimation AnimationFinishedCallback;

        protected Container myParent = null;
        protected Gui myParentGui = null;
        private object myUserData = null;

        protected Dictionary<string, List<uint>> myConnectedSignals = new Dictionary<string, List<uint>>();
        protected List<uint> myInternalSignalIds = new List<uint>();
        protected EventHandlerList myEventHandlerList = new EventHandlerList();

        static readonly object myPositionChangedEventKey = new object();
        static readonly object mySizeChangedEventKey = new object();
        static readonly object myMouseEnteredEventKey = new object();
        static readonly object myMouseLeftEventKey = new object();
        static readonly object myFocusedEventKey = new object();
        static readonly object myUnfocusedEventKey = new object();
        static readonly object myAnimationFinishedEventKey = new object();

        protected delegate void CallbackAction();
        protected delegate void CallbackActionVector2f(Vector2f param);
        protected delegate void CallbackActionString(IntPtr param);
        protected delegate void CallbackActionInt(int param);
        protected delegate void CallbackActionUInt(uint param);
        protected delegate void CallbackActionFloat(float param);
        protected delegate void CallbackActionRange(float param1, float param2);
        protected delegate void CallbackActionItemSelected(IntPtr param1, IntPtr param2);
        protected delegate void CallbackActionAnimation(ShowAnimationType type, bool visibles);

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tgui_getLastError();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiWidget_getRenderer(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiWidget_getSharedRenderer(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiWidget_connect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackAction func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiWidget_connectVector2f(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiWidget_connectString(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionString func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiWidget_connectBool(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionInt func);

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
        static extern protected uint tguiWidget_connectAnimation(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackActionAnimation func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidget_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_destroy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setPosition(IntPtr cPointer, Vector2f pos);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setPositionFromLayout(IntPtr cPointer, IntPtr layout2d);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiWidget_getPosition(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiWidget_getAbsolutePosition(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiWidget_getWidgetOffset(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setSize(IntPtr cPointer, Vector2f size);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setSizeFromLayout(IntPtr cPointer, IntPtr layout2d);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiWidget_getSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiWidget_getFullSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_disconnect(IntPtr cPointer, uint id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_disconnectAll(IntPtr cPointer, IntPtr signalName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidget_setRenderer(IntPtr cPointer, IntPtr rendererDataCPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setVisible(IntPtr cPointer, bool visible);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidget_isVisible(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_showWithEffect(IntPtr cPointer, ShowAnimationType type, Time duration);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_hideWithEffect(IntPtr cPointer, ShowAnimationType type, Time duration);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setEnabled(IntPtr cPointer, bool enabled);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidget_isEnabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setFocused(IntPtr cPointer, bool focused);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidget_isFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setFocusable(IntPtr cPointer, bool focusable);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidget_isFocusable(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidget_getWidgetType(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setToolTip(IntPtr cPointer, IntPtr toolTipCPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidget_getToolTip(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidget_getParent(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidget_isAnimationPlaying(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiWidget_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidget_setName(IntPtr cPointer, IntPtr name);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidget_getName(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidget_mouseOnWidget(IntPtr cPointer, Vector2f pos);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidget_setSignalEnabled(IntPtr cPointer, IntPtr signalName, bool enabled);

        #endregion
    }
}
