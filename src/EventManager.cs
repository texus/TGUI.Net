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
    internal class EventManager
    {
        // This vector will hold all widgets
        internal List<Widget> m_Widgets = new List<Widget>();

        // The id of the focused widget
        internal Widget m_FocusedWidget = null;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnMouseMoved (object sender, MouseMoveEventArgs e)
        {
            // Loop through all widgets
            foreach (Widget widget in m_Widgets)
            {
                // Check if the mouse went down on the widget
                if (widget.m_MouseDown)
                {
                    // Some widgets should always receive mouse move events, even if the mouse is no longer on top of them.
                    if (widget.m_DraggableWidget || widget.m_Container)
                    {
                        widget.OnMouseMoved(e);
                        return;
                    }
                }
            }

            // If the mouse is on top of an widget then send the event to the widget
            Widget theWidget = null;
            if (MouseOnWidget(ref theWidget, e.X, e.Y))
                theWidget.OnMouseMoved(e);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnMousePressed (object sender, MouseButtonEventArgs e)
        {
            // Check if the left mouse was pressed
            if (e.Button == Mouse.Button.Left)
            {
                // Check if the mouse is on top of an widget
                Widget widget = null;
                if (MouseOnWidget(ref widget, e.X, e.Y))
                {
                    // Focus the widget
                    FocusWidget(widget);

                    // Check if the widget is a container
                    if (widget.m_Container)
                    {
                        // If another widget was focused then unfocus it now
                        if ((m_FocusedWidget != null) && (m_FocusedWidget != widget))
                        {
                            widget.m_Focused = false;
                            widget.OnWidgetUnfocused();
                            m_FocusedWidget = null;
                        }
                    }

                    widget.OnLeftMousePressed(e);
                }
                else // The mouse didn't went down on an widget, so unfocus the focused widget
                    UnfocusWidgets();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnMouseReleased (object sender, MouseButtonEventArgs e)
        {
            // Check if the left mouse was released
            if (e.Button == Mouse.Button.Left)
            {
                // Check if the mouse is on top of an widget
                Widget theWidget = null;
                if (MouseOnWidget(ref theWidget, e.X, e.Y))
                    theWidget.OnLeftMouseReleased(e);

                // Tell all the other widgets that the mouse has gone up
                foreach (Widget widget in m_Widgets)
                {
                    if (widget != theWidget)
                        widget.MouseNoLongerDown();
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnKeyPressed (object sender, KeyEventArgs e)
        {
            // Only continue when the character was recognised
            if (e.Code != Keyboard.Key.Unknown)
            {
                // Check if there is a focused widget
                if (m_FocusedWidget != null)
                {
                    // Check the pressed key
                    if ((e.Code == Keyboard.Key.Left)
                        || (e.Code == Keyboard.Key.Right)
                        || (e.Code == Keyboard.Key.Up)
                        || (e.Code == Keyboard.Key.Down)
                        || (e.Code == Keyboard.Key.Back)
                        || (e.Code == Keyboard.Key.Delete)
                        || (e.Code == Keyboard.Key.Space)
                        || (e.Code == Keyboard.Key.Return))
                    {
                        // Tell the widget that the key was pressed
                        m_FocusedWidget.OnKeyPressed(e);
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnKeyReleased (object sender, KeyEventArgs e)
        {
            // Change the focus to another widget when the tab key was pressed
            if (e.Code == Keyboard.Key.Tab)
                TabKeyPressed();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnTextEntered (object sender, TextEventArgs e)
        {
            // Check if the character that we pressed is allowed
            if ((e.Unicode[0] >= 30) && (e.Unicode[0] != 127))
            {
                // Tell the focused widget that the key was pressed
                if (m_FocusedWidget != null)
                    m_FocusedWidget.OnTextEntered(e);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnMouseWheelMoved (object sender, MouseWheelEventArgs e)
        {
            // Send the event to the widget under the mouse
            Widget widget = null;
            if (MouseOnWidget (ref widget, e.X, e.Y))
                widget.OnMouseWheelMoved (e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Focuses an widget.
        ///
        /// The previously focused widget will be unfocused.
        ///
        /// \param widget  The widget that has to be focused.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void FocusWidget (Widget widget)
        {
            // Unfocus the currently focused widget
            if (m_FocusedWidget != null)
            {
                m_FocusedWidget.m_Focused = false;
                m_FocusedWidget.OnWidgetUnfocused();
            }

            // Focus the new widget
            m_FocusedWidget = widget;
            widget.m_Focused = true;
            widget.OnWidgetFocused();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Focuses the next widget.
        ///
        /// The currently focused widget will be unfocused, even if it was the only widget.
        /// When no widget was focused, the first widget in the container will be focused.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void FocusNextWidget ()
        {
            int focusedWidgetIndex = m_Widgets.IndexOf (m_FocusedWidget);

            // Loop all widgets behind the focused one
            for (int i = focusedWidgetIndex+1; i < m_Widgets.Count; ++i)
            {
                // If you are not allowed to focus the widget, then skip it
                if (m_Widgets[i].m_AllowFocus == true)
                {
                    // Make sure that the widget is visible and enabled
                    if ((m_Widgets[i].Visible) && (m_Widgets[i].Enabled))
                    {
                        if (m_FocusedWidget != null)
                        {
                            // unfocus the current widget
                            m_FocusedWidget.m_Focused = false;
                            m_FocusedWidget.OnWidgetUnfocused();
                        }

                        // Focus on the new widget
                        m_FocusedWidget = m_Widgets[i];
                        m_Widgets[i].m_Focused = true;
                        m_Widgets[i].OnWidgetFocused();
                        return;
                    }
                }
            }

            // None of the widgets behind the focused one could be focused, so loop the ones before it
            for (int i = 0; i < focusedWidgetIndex; ++i)
            {
                // If you are not allowed to focus the widget, then skip it
                if (m_Widgets[i].m_AllowFocus == true)
                {
                    // Make sure that the widget is visible and enabled
                    if ((m_Widgets[i].Visible) && (m_Widgets[i].Enabled))
                    {
                        // unfocus the current widget
                        m_FocusedWidget.m_Focused = false;
                        m_FocusedWidget.OnWidgetUnfocused();

                        // Focus on the new widget
                        m_FocusedWidget = m_Widgets[i];
                        m_Widgets[i].m_Focused = true;
                        m_Widgets[i].OnWidgetFocused();
                        return;
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Focuses the previous widget.
        ///
        /// The currently focused widget will be unfocused, even if it was the only widget.
        /// When no widget was focused, the last widget in the container will be focused.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void FocusPreviousWidget ()
        {
            int focusedWidgetIndex = m_Widgets.IndexOf (m_FocusedWidget);

            // Loop the widgets before the focused one
            for (int i = 0; i < focusedWidgetIndex; ++i)
            {
                // If you are not allowed to focus the widget, then skip it
                if (m_Widgets[i].m_AllowFocus == true)
                {
                    // Make sure that the widget is visible and enabled
                    if ((m_Widgets[i].Visible) && (m_Widgets[i].Enabled))
                    {
                        // unfocus the current widget
                        m_FocusedWidget.m_Focused = false;
                        m_FocusedWidget.OnWidgetUnfocused();

                        // Focus on the new widget
                        m_FocusedWidget = m_Widgets[i];
                        m_Widgets[i].m_Focused = true;
                        m_Widgets[i].OnWidgetFocused();
                        return;
                    }
                }
            }

            // None of the widgets before the focused one could be focused, so loop all widgets behind the focused one
            for (int i = focusedWidgetIndex+1; i < m_Widgets.Count; ++i)
            {
                // If you are not allowed to focus the widget, then skip it
                if (m_Widgets[i].m_AllowFocus == true)
                {
                    // Make sure that the widget is visible and enabled
                    if ((m_Widgets[i].Visible) && (m_Widgets[i].Enabled))
                    {
                        if (m_FocusedWidget != null)
                        {
                            // unfocus the current widget
                            m_FocusedWidget.m_Focused = false;
                            m_FocusedWidget.OnWidgetUnfocused();
                        }

                        // Focus on the new widget
                        m_FocusedWidget = m_Widgets[i];
                        m_Widgets[i].m_Focused = true;
                        m_Widgets[i].OnWidgetFocused();
                        return;
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Unfocus all the widgets.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UnfocusWidgets ()
        {
            if (m_FocusedWidget != null)
            {
                m_FocusedWidget.m_Focused = false;
                m_FocusedWidget.OnWidgetUnfocused();
                m_FocusedWidget = null;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Tell all widgets to update.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UpdateTime (int elapsedTime)
        {
            // Loop through all widgets
            for (int i = 0; i < m_Widgets.Count; ++i)
            {
                // Check if the widget is a container or an widget that uses the time
                if (m_Widgets[i].m_AnimatedWidget)
                {
                    // Update the elapsed time
                    m_Widgets[i].m_AnimationTimeElapsed += elapsedTime;
                    m_Widgets[i].OnUpdate ();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // When the tab key is pressed then this function is called. The focus will move to the next widget (if there is one).
        // This function will only work when tabKeyUsageEnabled is true.
        // The function will return true when another widget was focused.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void TabKeyPressed ()
        {
            // Don't do anything when the tab key usage is disabled
            if (Global.TabKeyUsageEnabled == false)
                return;

            // Check if a container is focused
            if (m_FocusedWidget != null)
            {
                if (m_FocusedWidget.m_Container)
                {
                    // Focus the next widget in container
                    if (((Container)m_FocusedWidget).FocusNextWidgetInContainer())
                        return;
                }
            }

            int focusedWidgetIndex = m_Widgets.IndexOf (m_FocusedWidget);

            // Loop all widgets behind the focused one
            for (int i = focusedWidgetIndex+1; i < m_Widgets.Count; ++i)
            {
                // If you are not allowed to focus the widget, then skip it
                if (m_Widgets[i].m_AllowFocus == true)
                {
                    // Make sure that the widget is visible and enabled
                    if ((m_Widgets[i].Visible) && (m_Widgets[i].Enabled))
                    {
                        if (m_FocusedWidget != null)
                        {
                            // unfocus the current widget
                            m_FocusedWidget.m_Focused = false;
                            m_FocusedWidget.OnWidgetUnfocused();
                        }

                        // Focus on the new widget
                        m_FocusedWidget = m_Widgets[i];
                        m_Widgets[i].m_Focused = true;
                        m_Widgets[i].OnWidgetFocused();
                        return;
                    }
                }
            }

            // None of the widgets behind the focused one could be focused, so loop the ones before it
            for (int i = 0; i < focusedWidgetIndex; ++i)
            {
                // If you are not allowed to focus the widget, then skip it
                if (m_Widgets[i].m_AllowFocus == true)
                {
                    // Make sure that the widget is visible and enabled
                    if ((m_Widgets[i].Visible) && (m_Widgets[i].Enabled))
                    {
                        // unfocus the current widget
                        m_FocusedWidget.m_Focused = false;
                        m_FocusedWidget.OnWidgetUnfocused();

                        // Focus on the new widget
                        m_FocusedWidget = m_Widgets[i];
                        m_Widgets[i].m_Focused = true;
                        m_Widgets[i].OnWidgetFocused();
                        return;
                    }
                }
            }

            // If the currently focused container widget is the only widget to focus, then focus its next child widget
            if ((m_FocusedWidget != null) && (m_FocusedWidget.m_Container))
            {
                ((Container)(m_FocusedWidget)).m_EventManager.TabKeyPressed ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Focuses the next widget in the container. If the last widget was focused then all widgets will be unfocused and
        // this function will return false.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool FocusNextWidgetInContainer ()
        {
            // Don't do anything when the tab key usage is disabled
            if (Global.TabKeyUsageEnabled == false)
                return false;

            // Loop through all widgets after the focused one
            int focusedWidgetIndex = m_Widgets.IndexOf (m_FocusedWidget);
            for (int i = focusedWidgetIndex+1; i < m_Widgets.Count; ++i)
            {
                // If you are not allowed to focus the widget, then skip it
                if (m_Widgets[i].m_AllowFocus == true)
                {
                    // Make sure that the widget is visible and enabled
                    if ((m_Widgets[i].Visible) && (m_Widgets[i].Enabled))
                    {
                        // Container widgets can only be focused it they contain focusable widgets
                        if ((!m_Widgets[i].m_Container) || (((Container)(m_Widgets[i])).FocusNextWidgetInContainer()))
                        {
                            if (focusedWidgetIndex > 0)
                            {
                                // Unfocus the current widget
                                m_FocusedWidget.m_Focused = false;
                                m_FocusedWidget.OnWidgetUnfocused();
                            }

                            // Focus on the new widget
                            m_FocusedWidget = m_Widgets[i];
                            m_Widgets[i].m_Focused = true;
                            m_Widgets[i].OnWidgetFocused();

                            return true;
                        }
                    }
                }
            }

            // We have the highest id
            UnfocusWidgets();
            return false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Tells all widgets that the mouse is no longer on top of them.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MouseNotOnWidget ()
        {
            // Tell the widgets that the mouse is no longer on top of it
            foreach (Widget widget in m_Widgets) {
                widget.MouseNotOnWidget ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Tells all widgets that the mouse is no longer down.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MouseNoLongerDown ()
        {
            // Tell the widgets that the mouse is no longer down
            foreach (Widget widget in m_Widgets) {
                widget.MouseNoLongerDown ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Checks above which widget the mouse is standing.
        // If there is no widget below the mouse then this function will return a null pointer.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool MouseOnWidget(ref Widget theWidget, float x, float y)
        {
            bool widgetFound = false;

            // Loop through all widgets
            foreach (Widget widget in m_Widgets)
            {
                // Check if the widget is visible and enabled
                if ((widget.Visible) && (widget.Enabled))
                {
                    // Ask the widget if the mouse is on top of them
                    if (widget.MouseOnWidget(x, y))
                    {
                        // If there already was an widget then they overlap each other
                        if (widgetFound)
                            theWidget.MouseNotOnWidget();

                        // An widget is found now
                        widgetFound = true;

                        // Also remember what widget should receive the event
                        theWidget = widget;
                    }
                }
            }

            // If our mouse is on top of an widget then return true
            return widgetFound;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

