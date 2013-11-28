/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus's Graphical User Interface
// Copyright (C) 2012-2013 Bruno Van de Velde (vdv_b@tgui.eu)
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
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    public abstract class Widget : Transformable, Drawable
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Widget ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Widget (Widget copy) : base(copy)
        {
            FocusedCallback      = copy.FocusedCallback;
            UnfocusedCallback    = copy.UnfocusedCallback;
            MouseEnteredCallback = copy.MouseEnteredCallback;
            MouseLeftCallback    = copy.MouseLeftCallback;

            m_Enabled            = copy.m_Enabled;
            m_Visible            = copy.m_Visible;
            m_WidgetPhase        = copy.m_WidgetPhase;
            m_Parent             = copy.m_Parent;
            m_Opacity            = copy.m_Opacity;
            m_AllowFocus         = copy.m_AllowFocus;
            m_AnimatedWidget     = copy.m_AnimatedWidget;
            m_DraggableWidget    = copy.m_DraggableWidget;
            m_Container          = copy.m_Container;
            m_Callback           = copy.m_Callback;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Is the widget visible?
        /// Invisible widgets don't receive events and obviously won't get drawn.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Visible
        {
            get
            {
                return m_Visible;
            }
            set
            {
                m_Visible = value;

                if (m_Visible == false)
                {
                    // If the widget is focused then it must be unfocused
                    Focused = false;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Is the widget enabled?
        /// Only enabled widgets will receive events and thus be able to send callbacks.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
            set
            {
                m_Enabled = value;

                if (m_Enabled == false)
                {
                    // Change the mouse button state.
                    m_MouseHover = false;
                    m_MouseDown = false;

                    // If the widget is focused then it must be unfocused
                    Focused = false;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Is the widget focused?
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Focused
        {
            get
            {
                return m_Focused;
            }
            set
            {
                if (value)
                {
                    if (m_Parent != null)
                        m_Parent.FocusWidget(this);
                }
                else
                {
                    if (m_Focused)
                        m_Parent.UnfocusWidgets ();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Parent of the widget, which is the widget that contains this widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Container Parent
        {
            get
            {
                return m_Parent;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Transparency of the widget.
        /// 0 is completely transparent, while 255 (default) means fully opaque.
        /// </summary>
        ///
        /// <remarks>This will only change the transparency of the images. The parts of the widgets that use a color will not
        /// be changed. You must change them yourself by setting the alpha channel of the color.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual byte Transparency
        {
            get
            {
                return m_Opacity;
            }
            set
            {
                m_Opacity = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Places the widget before all other widgets
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveToFront()
        {
            m_Parent.MoveWidgetToFront(this);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Places the widget behind all other widgets
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveToBack()
        {
            m_Parent.MoveWidgetToBack(this);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Callback id of the widget, used to distinguish the widget from the others when receiving a callback
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint CallbackId
        {
            get
            {
                return m_Callback.Id;
            }
            set
            {
                m_Callback.Id = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnUpdate ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Give a callback that the mouse has entered the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal void MouseEnteredWidget()
        {
            if (MouseEnteredCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.MouseEntered;
                MouseEnteredCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Give a callback that the mouse has left the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal void MouseLeftWidget()
        {
            if (MouseLeftCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.MouseLeft;
                MouseLeftCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Ask the widget if the mouse is on top of it
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal abstract bool MouseOnWidget (float x, float y);


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been pressed on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnLeftMousePressed (MouseButtonEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse has moved on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnMouseMoved (MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget();

            m_MouseHover = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that a special key has been pressed while the widget was focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnKeyPressed (KeyEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that text has been typed while the widget was focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnTextEntered (TextEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse wheel has moved while the mouse was on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnMouseWheelMoved (MouseWheelEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that it has been focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnWidgetFocused()
        {
            if (FocusedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.Focused;
                FocusedCallback (this, m_Callback);
            }

            // Make sure the parent is also focused
            if (m_Parent != null)
                m_Parent.Focused = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that it has been unfocused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnWidgetUnfocused()
        {
            if (UnfocusedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.Unfocused;
                UnfocusedCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse has moved on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void MouseNotOnWidget()
        {
            if (m_MouseHover == true)
                MouseLeftWidget();

            m_MouseHover = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void MouseNoLongerDown()
        {
            m_MouseDown = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initializes the widget now that it has been added to a parent widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void Initialize (Container parent)
        {
            m_Parent = parent;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public abstract void Draw (RenderTarget target, RenderStates states);


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the Focused event</summary>
        public event EventHandler<CallbackArgs> FocusedCallback;

        /// <summary>Event handler for the Unfocused event</summary>
        public event EventHandler<CallbackArgs> UnfocusedCallback;

        /// <summary>Event handler for the MouseEntered event</summary>
        public event EventHandler<CallbackArgs> MouseEnteredCallback;

        /// <summary>Event handler for the MouseLeft event</summary>
        public event EventHandler<CallbackArgs> MouseLeftCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // When an widget is disabled, it will no longer receive events
        private bool m_Enabled = true;

        // Is the widget visible? When it is invisible it will not receive events and it won't be drawn.
        private bool m_Visible = true;

        // This will store the different phases that the widget can have
        // e.g. if there isn't a mouse down image then a button should not try to change its image on mouse down
        protected internal byte m_WidgetPhase = 0;

        // This will point to our parent widget. If there is no parent then this will be null.
        protected internal Container m_Parent = null;

        // How transparent is the widget
        protected byte m_Opacity = 255;

        // Is the mouse on top of the widget? Did the mouse go down on the widget?
        protected internal bool m_MouseHover = false;
        protected internal bool m_MouseDown = false;

        // Are you focused on the widget?
        protected internal bool m_Focused = false;

        // Can the widget be focused?
        protected internal bool m_AllowFocus = false;

        // Keep track of the elapsed time.
        protected internal bool m_AnimatedWidget = false;
        protected internal int m_AnimationTimeElapsed = 0;

        // This is set to true for widgets that have something to be dragged around (e.g. sliders and scrollbars)
        protected internal bool m_DraggableWidget = false;

        // This is set to true for widgets that store other widgets inside them
        protected internal bool m_Container = false;

        // Instead of creating a new widget every time, one callback widget is always reused
        protected internal CallbackArgs m_Callback = new CallbackArgs();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
