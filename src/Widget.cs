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
        /// \brief Returns true when the widget is visible.
        ///
        /// \return Is the widget visible?
        ///
        /// If this function returns false then the widget is hidden, which means that it won't receive events and it won't be drawn.
        /// All widgets are visible by default.
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
        /// \brief Returns true when the widget is enabled.
        ///
        /// \return Is the widget enabled?
        ///
        /// If this function returns false then the widget is disabled and will longer receive events and it will thus no longer send callbacks.
        /// All widgets are enabled by default.
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
        /// \brief Returns true when the widget is focused and false otherwise.
        ///
        /// \return Is the widget focused?
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
        /// \brief Returns a pointer to the parent widget.
        ///
        /// \return Pointer to the parent.
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
        /// \brief Changes the transparency of the widget.
        ///
        /// \param transparency  The transparency of the widget.
        ///                      0 is completely transparent, while 255 (default) means fully opaque.
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
        /// \brief Places the widget before all other widgets.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveToFront()
        {
            m_Parent.MoveWidgetToFront(this);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Places the widget behind all other widgets.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveToBack()
        {
            m_Parent.MoveWidgetToBack(this);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the callback id of the widget.
        ///
        /// \param callbackId  The new callback id
        ///
        /// When receiving callback in a function which takes a Callback as parameter (or when polling the callbacks),
        /// you can find a callbackId in this Callback struct which can identify the widget.
        /// This function changes the id that this widget sends on a callback.
        ///
        /// By default, the callback id will be 0.
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
        /// \internal
        // This function is a (slow) way to set properties on the widget, no matter what type it is.
        // When the requested property doesn't exist in the widget then the functions will return false.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool setProperty(string property, string value)
        {
            property = property.ToLower ();

            if (property == "left")
                Position = new Vector2f(Convert.ToSingle(value), Position.Y);
            else if (property == "top")
                Position = new Vector2f(Position.X, Convert.ToSingle(value));
            else if (property == "width")
                Size = new Vector2f(Convert.ToSingle(value), Size.Y);
            else if (property == "height")
                Size = new Vector2f(Size.X, Convert.ToSingle(value));
            else if (property == "visible")
            {
                if ((value == "true") || (value == "True"))
                    m_Visible = true;
                else if ((value == "false") || (value == "False"))
                    m_Visible = false;
                else
                    throw new Exception("Failed to parse 'Visible' property.");
            }
            else if (property == "enabled")
            {
                if ((value == "true") || (value == "True"))
                    m_Enabled = true;
                else if ((value == "false") || (value == "False"))
                    m_Enabled = false;
                else
                    throw new Exception("TGUI error: Failed to parse 'Enabled' property.");
            }
            else if (property == "transparency")
                Transparency = Convert.ToByte(value);
            else if (property == "callbackid")
                m_Callback.Id = Convert.ToUInt32(value);
            else if (property == "callback")
            {
                /*
                List<string> callbacks;
                Internal.DecodeList(value, callbacks);

                foreach (string callback in callbacks)
                {
                    if ((callback == "Focused") || (callback == "focused"))
                        bindCallback(Focused);
                    else if ((callback == "Unfocused") || (callback == "unfocused"))
                        bindCallback(Unfocused);
                    else if ((callback == "MouseEntered") || (callback == "mouseentered"))
                        bindCallback(MouseEntered);
                    else if ((callback == "MouseLeft") || (callback == "mouseleft"))
                        bindCallback(MouseLeft);
                }
                */
            }
            else // The property didn't match
                return false;

            // You pass here when one of the properties matched
            return true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // This function is a (slow) way to get properties of the widget, no matter what type it is.
        // When the requested property doesn't exist in the widget then the functions will return false.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool getProperty(string property, out string value)
        {
            value = "";
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // Returns a list of all properties that can be used in setProperty and getProperty.
        // The second value in the pair is the type of the property (e.g. int, uint, string, ...).
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual List<KeyValuePair<string, string>> getPropertyList()
        {
            return new List<KeyValuePair<string, string>> ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // This function is called right after the elapsed time is changed.
        // The elapsed time is only changed when the widget has set m_AnimatedWidget to true.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnUpdate ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // This function is called when the mouse enters the widget. If requested, a callback will be send.
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
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal abstract bool MouseOnWidget(float x, float y);


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnLeftMousePressed(MouseButtonEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnLeftMouseReleased(MouseButtonEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnMouseMoved(MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget();

            m_MouseHover = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnKeyPressed(KeyEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnTextEntered(TextEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void OnMouseWheelMoved(MouseWheelEventArgs e)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
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
        /// \internal
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
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void MouseNotOnWidget()
        {
            if (m_MouseHover == true)
                MouseLeftWidget();

            m_MouseHover = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void MouseNoLongerDown()
        {
            m_MouseDown = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // This function is called when the widget is added to a container.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal virtual void Initialize (Container parent)
        {
            m_Parent = parent;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // Draws the widget on the render target.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public abstract void Draw (RenderTarget target, RenderStates states);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

