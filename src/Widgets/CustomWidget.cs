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
using System.Security;
using System.Runtime.InteropServices;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    /// <summary>
    /// Base class for widgets implemented in C#
    /// </summary>
    public class CustomWidget : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomWidget()
            : base(tguiCustomWidgetForBindings_create())
        {
            InitCustomWidgetCallbacks();
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        protected internal CustomWidget(IntPtr cPointer)
            : base(cPointer)
        {
            InitCustomWidgetCallbacks();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public CustomWidget(CustomWidget copy)
            : base(copy)
        {
            InitCustomWidgetCallbacks();
        }

        /// <summary>
        /// Function called when widget position changes
        /// </summary>
        /// <param name="position">New position of the widget</param>
        protected virtual void OnPositionChanged(Vector2f position)
        {
        }

        /// <summary>
        /// Function called when widget size changes
        /// </summary>
        /// <param name="size">New size of the widget</param>
        protected virtual void OnSizeChanged(Vector2f size)
        {
        }

        /// <summary>
        /// Function called when widget visibility changes
        /// </summary>
        /// <param name="visible">Whether the widget is now shown or hidden</param>
        protected virtual void OnVisibleChanged(bool visible)
        {
        }

        /// <summary>
        /// Function called when widget is enabled or disabled
        /// </summary>
        /// <param name="enabled">Whether the widget is now enabled or disabled</param>
        protected virtual void OnEnableChanged(bool enabled)
        {
        }

        /// <summary>
        /// Function called when widget is focused or unfocused
        /// </summary>
        /// <param name="focused">Whether the widget is now focused or unfocused</param>
        protected virtual void OnFocusChanged(bool focused)
        {
        }

        /// <summary>
        /// Function called when the widget wants to know if it can be focused
        /// </summary>
        /// <returns>Whether this type of widget can receive focus</returns>
        protected virtual bool OnCanGainFocus()
        {
            return false;
        }

        /// <summary>
        /// Function called when the widget wants to know its entire size,
        /// including things like borders that would be drawn around it.
        /// </summary>
        /// <returns>Full size of the widget</returns>
        protected virtual Vector2f OnGetFullSize()
        {
            return Size;
        }

        /// <summary>
        /// Function called when the widget wants to know its position in the gui
        /// instead of just the relative position to its parent.
        /// </summary>
        /// <returns>Absolute position of the widget</returns>
        protected virtual Vector2f OnGetAbsolutePosition()
        {
            if (Parent != null)
                return Parent.AbsolutePosition + Parent.ChildWidgetsOffset + Position;
            else
                return Position;
        }

        /// <summary>
        /// Function called when the widget wants to know the distance between the position where
        /// the widget is drawn and where the widget is placed.
        /// </summary>
        /// <returns>Offset of the widget</returns>
        protected virtual Vector2f OnGetWidgetOffset()
        {
            return new Vector2f(0, 0);
        }

        /// <summary>
        /// Function called before drawing to inform the widget how much time has passed since the last draw
        /// </summary>
        /// <param name="elapsedTime">Time elapsed since last update</param>
        protected virtual void OnUpdate(Time elapsedTime)
        {
        }

        /// <summary>
        /// Function called when the widget wants to know if the mouse is on top of it
        /// </summary>
        /// <param name="pos">Mouse position relative to the parent of the widget</param>
        /// <returns>Whether the mouse is on top of the widget</returns>
        protected virtual bool OnMouseOnWidget(Vector2f pos)
        {
            return false;
        }

        /// <summary>
        /// Function called when the left mouse button was pressed on top of the widget
        /// </summary>
        /// <param name="pos">Mouse position relative to the parent of the widget</param>
        protected virtual void OnLeftMousePressed(Vector2f pos)
        {
        }

        /// <summary>
        /// Function called when the left mouse button was released on top of the widget
        /// </summary>
        /// <param name="pos">Mouse position relative to the parent of the widget</param>
        protected virtual void OnLeftMouseReleased(Vector2f pos)
        {
        }

        /// <summary>
        /// Function called when the right mouse button was pressed on top of the widget
        /// </summary>
        /// <param name="pos">Mouse position relative to the parent of the widget</param>
        protected virtual void OnRightMousePressed(Vector2f pos)
        {
        }

        /// <summary>
        /// Function called when the right mouse button was released on top of the widget
        /// </summary>
        /// <param name="pos">Mouse position relative to the parent of the widget</param>
        protected virtual void OnRightMouseReleased(Vector2f pos)
        {
        }

        /// <summary>
        /// Function called when the mouse moved (while the mouse is on top of the widget)
        /// </summary>
        /// <param name="pos">Mouse position relative to the parent of the widget</param>
        protected virtual void OnMouseMoved(Vector2f pos)
        {
        }

        /// <summary>
        /// Function called when a key was pressed while the widget was focused
        /// </summary>
        /// <param name="keyEvent">Event containing the pressed key and the state of the modifier keys</param>
        protected virtual void OnKeyPressed(KeyEvent keyEvent)
        {
        }

        /// <summary>
        /// Function called when a character was typed while the widget was focused
        /// </summary>
        /// <param name="key">UTF-32 Unicode value of the character</param>
        protected virtual void OnTextEntered(uint key)
        {
        }

        /// <summary>
        /// Function called when the mouse wheel is moved while the mouse is on top of the widget
        /// </summary>
        /// <param name="delta">Scroll amount, positive when scrolling up, negative when scrolling down</param>
        /// <param name="mousePos">Mouse position relative to the parent of the widget</param>
        /// <returns>Whether the event was handled by the widget</returns>
        protected virtual bool OnMouseWheelScrolled(float delta, Vector2f mousePos)
        {
            return false;
        }

        /// <summary>
        /// Function called when the mouse is no longer on top of the widget
        /// </summary>
        protected virtual void OnMouseNoLongerOnWidget()
        {
        }

        /// <summary>
        /// Function called when the left mouse button was released (not necessarily on top of the widget) after it was pressed on the widget
        /// </summary>
        protected virtual void OnLeftMouseButtonNoLongerDown()
        {
        }

        /// <summary>
        /// Function called when the mouse is now on top of the button
        /// </summary>
        protected virtual void OnMouseEnteredWidget()
        {
        }

        /// <summary>
        /// Function called when the mouse is no longer on top of the widget
        /// </summary>
        protected virtual void OnMouseLeftWidget()
        {
        }

        /// <summary>
        /// Function called when a renderer property changes
        /// </summary>
        /// <param name="property">Property that was changed</param>
        /// <returns>
        /// True if the change has been fully processed, false when the
        /// base class should also be informed about the change.
        /// </returns>
        protected virtual bool OnRendererChanged(string property)
        {
            return false;
        }

        /// <summary>
        /// Function called when widget should draw itself
        /// </summary>
        /// <param name="states">States for drawing</param>
        /// <remarks>
        /// The render target to draw on can be found at myParentGui.Target
        /// </remarks>
        protected virtual void OnDraw(RenderStates states)
        {
        }


        private void InitCustomWidgetCallbacks()
        {
            PositionChangedCallback = new CallbackCustomWidgetVector2f(OnPositionChanged);
            tguiCustomWidgetForBindings_setPositionChangedCallback(CPointer, PositionChangedCallback);

            SizeChangedCallback = new CallbackCustomWidgetVector2f(OnSizeChanged);
            tguiCustomWidgetForBindings_setSizeChangedCallback(CPointer, SizeChangedCallback);

            VisibleChangedCallback = new CallbackCustomWidgetBool(OnVisibleChanged);
            tguiCustomWidgetForBindings_setVisibleChangedCallback(CPointer, VisibleChangedCallback);

            EnableChangedCallback = new CallbackCustomWidgetBool(OnEnableChanged);
            tguiCustomWidgetForBindings_setEnableChangedCallback(CPointer, EnableChangedCallback);

            FocusChangedCallback = new CallbackCustomWidgetBool(OnFocusChanged);
            tguiCustomWidgetForBindings_setFocusChangedCallback(CPointer, FocusChangedCallback);

            CanGainFocusCallback = new CallbackCustomWidgetGetBool(OnCanGainFocus);
            tguiCustomWidgetForBindings_setCanGainFocusCallback(CPointer, CanGainFocusCallback);

            GetFullSizeCallback = new CallbackCustomWidgetGetVector2f(OnGetFullSize);
            tguiCustomWidgetForBindings_setGetFullSizeCallback(CPointer, GetFullSizeCallback);

            GetAbsolutePositionCallback = new CallbackCustomWidgetGetVector2f(OnGetAbsolutePosition);
            tguiCustomWidgetForBindings_setGetAbsolutePositionCallback(CPointer, GetAbsolutePositionCallback);

            GetWidgetOffsetCallback = new CallbackCustomWidgetGetVector2f(OnGetWidgetOffset);
            tguiCustomWidgetForBindings_setGetWidgetOffsetCallback(CPointer, GetWidgetOffsetCallback);

            UpdateCallback = new CallbackCustomWidgetUpdate(OnUpdate);
            tguiCustomWidgetForBindings_setUpdateCallback(CPointer, UpdateCallback);

            MouseOnWidgetCallback = new CallbackCustomWidgetMouseOnWidget(OnMouseOnWidget);
            tguiCustomWidgetForBindings_setMouseOnWidgetCallback(CPointer, MouseOnWidgetCallback);

            LeftMousePressedCallback = new CallbackCustomWidgetVector2f(OnLeftMousePressed);
            tguiCustomWidgetForBindings_setLeftMousePressedCallback(CPointer, LeftMousePressedCallback);

            LeftMouseReleasedCallback = new CallbackCustomWidgetVector2f(OnLeftMouseReleased);
            tguiCustomWidgetForBindings_setLeftMouseReleasedCallback(CPointer, LeftMouseReleasedCallback);

            RightMousePressedCallback = new CallbackCustomWidgetVector2f(OnRightMousePressed);
            tguiCustomWidgetForBindings_setRightMousePressedCallback(CPointer, RightMousePressedCallback);

            RightMouseReleasedCallback = new CallbackCustomWidgetVector2f(OnRightMouseReleased);
            tguiCustomWidgetForBindings_setRightMouseReleasedCallback(CPointer, RightMouseReleasedCallback);

            MouseMovedCallback = new CallbackCustomWidgetVector2f(OnMouseMoved);
            tguiCustomWidgetForBindings_setMouseMovedCallback(CPointer, MouseMovedCallback);

            KeyPressedCallback = new CallbackCustomWidgetKeyPressed(OnKeyPressed);
            tguiCustomWidgetForBindings_setKeyPressedCallback(CPointer, KeyPressedCallback);

            TextEnteredCallback = new CallbackCustomWidgetTextEntered(OnTextEntered);
            tguiCustomWidgetForBindings_setTextEnteredCallback(CPointer, TextEnteredCallback);

            MouseWheelScrolledCallback = new CallbackCustomWidgetMouseWheelScrolled(OnMouseWheelScrolled);
            tguiCustomWidgetForBindings_setMouseWheelScrolledCallback(CPointer, MouseWheelScrolledCallback);

            MouseNoLongerOnWidgetCallback = new CallbackCustomWidgetVoid(OnMouseNoLongerOnWidget);
            tguiCustomWidgetForBindings_setMouseNoLongerOnWidgetCallback(CPointer, MouseNoLongerOnWidgetCallback);

            LeftMouseButtonNoLongerDownCallback = new CallbackCustomWidgetVoid(OnLeftMouseButtonNoLongerDown);
            tguiCustomWidgetForBindings_setLeftMouseButtonNoLongerDownCallback(CPointer, LeftMouseButtonNoLongerDownCallback);

            MouseEnteredWidgetCallback = new CallbackCustomWidgetVoid(OnMouseEnteredWidget);
            tguiCustomWidgetForBindings_setMouseEnteredWidgetCallback(CPointer, MouseEnteredWidgetCallback);

            MouseLeftWidgetCallback = new CallbackCustomWidgetVoid(OnMouseLeftWidget);
            tguiCustomWidgetForBindings_setMouseLeftWidgetCallback(CPointer, MouseLeftWidgetCallback);

            RendererChangedCallback = new CallbackCustomWidgetRenderer(OnRendererChanged);
            tguiCustomWidgetForBindings_setRendererChangedCallback(CPointer, RendererChangedCallback);

            DrawCallback = new CallbackCustomWidgetDraw(ProcessOnDrawCallback);
            tguiCustomWidgetForBindings_setDrawCallback(CPointer, DrawCallback);
        }

        private void ProcessOnDrawCallback(RenderStatesMarshalData data)
        {
            Texture texture = null; // The constructor of Texture that takes the CPointer as argument is internal :(
            var shader = (data.shader != IntPtr.Zero) ? new Shader(data.shader) : null;
            var states = new RenderStates(data.blendMode, data.transform, texture, shader);
            OnDraw(states);
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct RenderStatesMarshalData
        {
            public BlendMode blendMode;
            public Transform transform;
            public IntPtr texture;
            public IntPtr shader;
        }


        private delegate void CallbackCustomWidgetVector2f(Vector2f param);
        private delegate void CallbackCustomWidgetBool(bool param);
        private delegate void CallbackCustomWidgetVoid();
        private delegate Vector2f CallbackCustomWidgetGetVector2f();
        private delegate bool CallbackCustomWidgetGetBool();
        private delegate void CallbackCustomWidgetUpdate(Time time);
        private delegate bool CallbackCustomWidgetMouseOnWidget(Vector2f pos);
        private delegate void CallbackCustomWidgetKeyPressed(KeyEvent keyEvent);
        private delegate void CallbackCustomWidgetTextEntered(uint key);
        private delegate bool CallbackCustomWidgetMouseWheelScrolled(float delta, Vector2f mousePos);
        private delegate bool CallbackCustomWidgetRenderer(string property);
        private delegate void CallbackCustomWidgetDraw(RenderStatesMarshalData states);

        private CallbackCustomWidgetVector2f            PositionChangedCallback;
        private CallbackCustomWidgetVector2f            SizeChangedCallback;
        private CallbackCustomWidgetBool                VisibleChangedCallback;
        private CallbackCustomWidgetBool                EnableChangedCallback;
        private CallbackCustomWidgetBool                FocusChangedCallback;
        private CallbackCustomWidgetGetBool             CanGainFocusCallback;
        private CallbackCustomWidgetGetVector2f         GetFullSizeCallback;
        private CallbackCustomWidgetGetVector2f         GetAbsolutePositionCallback;
        private CallbackCustomWidgetGetVector2f         GetWidgetOffsetCallback;
        private CallbackCustomWidgetUpdate              UpdateCallback;
        private CallbackCustomWidgetMouseOnWidget       MouseOnWidgetCallback;
        private CallbackCustomWidgetVector2f            LeftMousePressedCallback;
        private CallbackCustomWidgetVector2f            LeftMouseReleasedCallback;
        private CallbackCustomWidgetVector2f            RightMousePressedCallback;
        private CallbackCustomWidgetVector2f            RightMouseReleasedCallback;
        private CallbackCustomWidgetVector2f            MouseMovedCallback;
        private CallbackCustomWidgetKeyPressed          KeyPressedCallback;
        private CallbackCustomWidgetTextEntered         TextEnteredCallback;
        private CallbackCustomWidgetMouseWheelScrolled  MouseWheelScrolledCallback;
        private CallbackCustomWidgetVoid                MouseNoLongerOnWidgetCallback;
        private CallbackCustomWidgetVoid                LeftMouseButtonNoLongerDownCallback;
        private CallbackCustomWidgetVoid                MouseEnteredWidgetCallback;
        private CallbackCustomWidgetVoid                MouseLeftWidgetCallback;
        private CallbackCustomWidgetRenderer            RendererChangedCallback;
        private CallbackCustomWidgetDraw                DrawCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiCustomWidgetForBindings_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setPositionChangedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setSizeChangedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setVisibleChangedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetBool func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setEnableChangedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetBool func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setFocusChangedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetBool func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setCanGainFocusCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetGetBool func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setGetFullSizeCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetGetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setGetAbsolutePositionCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetGetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setGetWidgetOffsetCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetGetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setUpdateCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetUpdate func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setMouseOnWidgetCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetMouseOnWidget func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setLeftMousePressedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setLeftMouseReleasedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setRightMousePressedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setRightMouseReleasedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setMouseMovedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVector2f func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setKeyPressedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetKeyPressed func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setTextEnteredCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetTextEntered func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setMouseWheelScrolledCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetMouseWheelScrolled func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setMouseNoLongerOnWidgetCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVoid func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setLeftMouseButtonNoLongerDownCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVoid func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setMouseEnteredWidgetCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVoid func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setMouseLeftWidgetCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetVoid func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setRendererChangedCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetRenderer func);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiCustomWidgetForBindings_setDrawCallback(IntPtr cPointer, [MarshalAs(UnmanagedType.FunctionPtr)] CallbackCustomWidgetDraw func);

        #endregion
    }
}
