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
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    public abstract class Widget : Transformable, Drawable
    {
        public event EventHandler<CallbackArgs> FocusedCallback;
        public event EventHandler<CallbackArgs> UnfocusedCallback;
        public event EventHandler<CallbackArgs> MouseEnteredCallback;
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
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Widget ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Container Parent
        {
            get
            {
                return m_Parent;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveToFront()
        {
            m_Parent.MoveWidgetToFront(this);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveToBack()
        {
            m_Parent.MoveWidgetToBack(this);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnUpdate ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        // This function is called when the mouse leaves the widget. If requested, a callback will be send.
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
        // Get the WidgetPhases from the string read from the info file.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal void extractPhases(string phases)
        {
            bool nextPhaseFound = true;
            int commaPos;

            while (nextPhaseFound)
            {
                // Search for the next comma
                commaPos = phases.IndexOf(',', 0);

                // Check if there was a comma
                if (commaPos != -1)
                {
                    // Get the next phase
                    string singlePhase = phases.Substring(0, commaPos);

                    // Store the phase
                    if (singlePhase == "hover")
                        m_WidgetPhase |= (byte)WidgetPhase.Hover;
                    else if (singlePhase == "focus")
                        m_WidgetPhase |= (byte)WidgetPhase.Focused;
                    else if (singlePhase == "down")
                        m_WidgetPhase |= (byte)WidgetPhase.MouseDown;
                    else if (singlePhase == "selected")
                        m_WidgetPhase |= (byte)WidgetPhase.Selected;

                    // Remove this phase from the string
                    phases.Remove(0, commaPos+1);
                }
                else // There was no comma
                {
                    nextPhaseFound = false;

                    // Store the phase
                    if (phases == "hover")
                        m_WidgetPhase |= (byte)WidgetPhase.Hover;
                    else if (phases == "focus")
                        m_WidgetPhase |= (byte)WidgetPhase.Focused;
                    else if (phases == "down")
                        m_WidgetPhase |= (byte)WidgetPhase.MouseDown;
                    else if (phases == "selected")
                        m_WidgetPhase |= (byte)WidgetPhase.Selected;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Used to communicate with EventManager.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal abstract bool MouseOnWidget(float x, float y);

        protected internal virtual void OnLeftMousePressed(MouseButtonEventArgs e)
        {
        }

        protected internal virtual void OnLeftMouseReleased(MouseButtonEventArgs e)
        {
        }

        protected internal virtual void OnMouseMoved(MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget();

            m_MouseHover = true;
        }

        protected internal virtual void OnKeyPressed(KeyEventArgs e)
        {
        }

        protected internal virtual void OnTextEntered(TextEventArgs e)
        {
        }

        protected internal virtual void OnMouseWheelMoved(MouseWheelEventArgs e)
        {
        }

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

        protected internal virtual void OnWidgetUnfocused()
        {
            if (UnfocusedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.Unfocused;
                UnfocusedCallback (this, m_Callback);
            }
        }

        protected internal virtual void MouseNotOnWidget()
        {
            if (m_MouseHover == true)
                MouseLeftWidget();

            m_MouseHover = false;
        }

        protected internal virtual void MouseNoLongerDown()
        {
            m_MouseDown = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void Initialize (Container parent)
        {
            m_Parent = parent;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public abstract void Draw (RenderTarget target, RenderStates states);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

