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

using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    public abstract class Container : Widget
    {
        private Font m_GlobalFont;
        private List<string> m_Names = new List<string>();

        internal EventManager m_EventManager = new EventManager();
        internal bool m_ContainerFocused = false;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Default constructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Container ()
        {
            m_Container = true;
            m_AnimatedWidget = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Container (Container copy) : base(copy)
        {
            m_GlobalFont = copy.m_GlobalFont;

            // Copy all the widgets
            for (int i = 0; i < copy.m_EventManager.m_Widgets.Count; ++i)
            {
                m_EventManager.m_Widgets.Add ((Widget)System.Activator.CreateInstance (copy.m_EventManager.m_Widgets[i].GetType(), copy.m_EventManager.m_Widgets[i]));
                m_Names.Add(copy.m_Names[i]);

                m_EventManager.m_Widgets[m_EventManager.m_Widgets.Count-1].m_Parent = this;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the global font.
        ///
        /// This font will be used by all widgets that are created after changing this font.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Font GlobalFont
        {
            get
            {
                return m_GlobalFont;
            }
            set
            {
                m_GlobalFont = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns a list of all the widgets.
        ///
        /// \return List of all widget pointers
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<string> GetWidgetNames()
        {
            return m_Names;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns a list of the names of all the widgets.
        ///
        /// \return List of all widget names
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Widget> GetWidgets()
        {
            return m_EventManager.m_Widgets;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Adds an widget to the container.
        ///
        /// \param widget   Pointer to the widget you would like to add
        /// \param name     If you want to access the widget later then you must do this with this name
        ///
        /// Usage example:
        /// \code
        /// ...
        /// \endcode
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Add<T> (T widget, string name = "") where T : Widget
        {
            widget.Initialize (this);

            m_EventManager.m_Widgets.Add (widget);
            m_Names.Add (name);

            return widget;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns a pointer to an earlier created widget.
        ///
        /// \param name The name that was given to the widget when it was added to the container.
        ///
        /// \return Pointer to the earlier created widget
        ///
        /// \warning This function will return null when an unknown widget name was passed.
        ///
        /// Usage example:
        /// \code
        /// ...
        /// \endcode
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Get<T> (string name) where T : Widget
        {
            int index = m_Names.IndexOf (name);
            if (index != -1)
            {
                if (m_EventManager.m_Widgets [index] is T)
                    return (T)m_EventManager.m_Widgets [index];
                else
                    return null;
            }
            else
                return null;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes a single widget that was added to the container.
        ///
        /// \param widget  Pointer to the widget to remove
        ///
        /// \see remove(sf::String)
        ///
        /// Usage example:
        /// \code
        /// ...
        /// \endcode
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (Widget widget)
        {
            int index = m_EventManager.m_Widgets.IndexOf (widget);
            if (index != -1)
            {
                // Unfocus the widget, just in case it was focused
                m_EventManager.UnfocusWidget(widget);

                // Remove the widget from the list
                m_Names.RemoveAt (index);
                m_EventManager.m_Widgets.RemoveAt (index);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes a single widget that was added to the container.
        ///
        /// \param widget  Pointer to the widget to remove
        ///
        /// This function is provided for internal use.
        /// The other remove function will probably be easier to use, but in the end they do exactly the same.
        ///
        /// \see remove(Widget::Ptr)
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (string name)
        {
            int index = m_Names.IndexOf (name);
            if (index != -1)
                Remove (m_EventManager.m_Widgets[index]);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes all widgets that were added to the container.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAllWidgets ()
        {
            m_EventManager.UnfocusAllWidgets ();

            m_Names.Clear ();
            m_EventManager.m_Widgets.Clear ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Focuses an widget.
        ///
        /// The previously focused widget will be unfocused.
        ///
        /// \param widget  The widget that has to be focused.
        ///
        /// \see unfocusWidget
        /// \see unfocusAllWidgets
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void FocusWidget (Widget widget)
        {
            m_EventManager.FocusWidget (widget);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Unfocus an widget.
        ///
        /// The next widget will be focused.
        ///
        /// \param widget  The widget that has to be unfocused.
        ///
        /// \see focusWidget
        /// \see unfocusAllWidgets
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UnfocusWidget (Widget widget)
        {
            m_EventManager.UnfocusWidget (widget);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Unfocus all the widgets.
        ///
        /// \see focusWidget
        /// \see unfocusWidget
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UnfocusAllWidgets ()
        {
            m_EventManager.UnfocusAllWidgets ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Uncheck all the radio buttons.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UncheckRadioButtons ()
        {
            foreach (Widget widget in m_EventManager.m_Widgets)
            {
                RadioButton radioButton = widget as RadioButton;

                if (radioButton != null)
                    radioButton.ForceUncheck ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Places an widget before all other widgets.
        ///
        /// \param widget  The widget that should be moved to the front
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveWidgetToFront (Widget widget)
        {
            int index = m_EventManager.m_Widgets.IndexOf (widget);
            if (index != -1)
            {
                m_EventManager.m_Widgets.Add (widget);
                m_EventManager.m_Widgets.RemoveAt (index);

                m_Names.Add (m_Names[index]);
                m_Names.RemoveAt (index);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Places an widget behind all other widgets.
        ///
        /// \param widget  The widget that should be moved to the back
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveWidgetToBack (Widget widget)
        {
            int index = m_EventManager.m_Widgets.IndexOf (widget);
            if (index != -1)
            {
                m_EventManager.m_Widgets.Insert (0, widget);
                m_EventManager.m_Widgets.RemoveAt (index + 1);

                m_Names.Insert (0, m_Names[index]);
                m_Names.RemoveAt (index + 1);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the transparency of the widget.
        ///
        /// 0 is completely transparent, while 255 (default) means fully opaque.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override byte Transparency
        {
            set
            {
                base.Transparency = value;

                foreach (Widget widget in m_EventManager.m_Widgets)
                    widget.Transparency = m_Opacity;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // This function is called when the widget is added to a container.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void Initialize(Container parent)
        {
            base.Initialize(parent);
            GlobalFont = parent.GlobalFont;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnUpdate ()
        {
            m_EventManager.UpdateTime(m_AnimationTimeElapsed);
            m_AnimationTimeElapsed = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved (MouseMoveEventArgs e)
        {
            // Adjust the mouse position of the event
            e.X = (int)(e.X - Position.X);
            e.Y = (int)(e.Y - Position.Y);

            // Let the event manager handle the event
            m_EventManager.OnMouseMoved(this, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed (MouseButtonEventArgs e)
        {
            // Adjust the mouse position of the event
            e.X = (int)(e.X - Position.X);
            e.Y = (int)(e.Y - Position.Y);

            // Let the event manager handle the event
            m_EventManager.OnMousePressed(this, e);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            // Adjust the mouse position of the event
            e.X = (int)(e.X - Position.X);
            e.Y = (int)(e.Y - Position.Y);

            // Let the event manager handle the event
            m_EventManager.OnMouseReleased(this, e);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnKeyPressed (KeyEventArgs e)
        {
            // Let the event manager handle the event
            m_EventManager.OnKeyPressed(this, e);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnTextEntered (TextEventArgs e)
        {
            m_EventManager.OnTextEntered(this, e);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseWheelMoved (MouseWheelEventArgs e)
        {
            // Adjust the mouse position of the event
            e.X = (int)(e.X - Position.X);
            e.Y = (int)(e.Y - Position.Y);

            m_EventManager.OnMouseWheelMoved(this, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNotOnWidget()
        {
            if (m_MouseHover == true)
            {
                MouseLeftWidget();
                m_EventManager.MouseNotOnWidget();

                m_MouseHover = false;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNoLongerDown()
        {
            base.MouseNoLongerDown ();
            m_EventManager.MouseNoLongerDown ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        internal void DrawContainer (RenderTarget target, RenderStates states)
        {
            // Draw all widgets when they are visible
            foreach (Widget widget in m_EventManager.m_Widgets)
            {
                if (widget.Visible)
                    target.Draw (widget, states);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        internal bool FocusNextWidgetInContainer ()
        {
            return m_EventManager.FocusNextWidget ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class GuiContainer : Container
    {
        internal RenderWindow m_Window;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the size of the widget.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override Vector2f Size
        {
            get
            {
                return new Vector2f (m_Window.Size.X, m_Window.Size.Y);
            }
            set
            {
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override bool MouseOnWidget(float x, float y)
        {
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw (RenderTarget target, RenderStates states)
        {
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

