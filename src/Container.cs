/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI.Net - Texus's Graphical User Interface for .Net languages
// Copyright (C) 2013-2014 Bruno Van de Velde (vdv_b@tgui.eu)
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Container ()
        {
            m_Container = true;
            m_AnimatedWidget = true;
            m_AllowFocus = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Container (Container copy) : base(copy)
        {
            m_GlobalFont = copy.m_GlobalFont;

            // Copy all the widgets
            for (int i = 0; i < copy.m_Widgets.Count; ++i)
            {
                m_Widgets.Add ((Widget)System.Activator.CreateInstance (copy.m_Widgets[i].GetType(), copy.m_Widgets[i]));
                m_Names.Add(copy.m_Names[i]);

                m_Widgets[m_Widgets.Count-1].m_Parent = this;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default font for every new widget
        /// </summary>
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
        /// <summary>
        /// Returns a list of the names of all widgets
        /// </summary>
        ///
        /// <returns>List of all widget names</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<string> GetWidgetNames()
        {
            return m_Names;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns a list of all the widgets
        /// </summary>
        ///
        /// <returns>List of all widgets</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Widget> GetWidgets()
        {
            return m_Widgets;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adds a widget to the container
        /// </summary>
        ///
        /// <param name="widget">Widget you would like to add</param>
        /// <param name="name">If you want to access the widget later then you must do this with this name</param>
        ///
        /// Usage example:
        /// <code>
        /// Button button = container.Add(new Button("theme.conf"), "MyButton");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Add<T> (T widget, string name = "") where T : Widget
        {
            widget.Initialize (this);

            m_Widgets.Add (widget);
            m_Names.Add (name);

            return widget;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns an earlier created widget
        /// </summary>
        ///
        /// <param name="name">The name that was given to the widget when it was added to the container</param>
        ///
        /// <returns>Requested widget, or null when no widget has this name or it is of the wrong type</returns>
        ///
        /// Usage example:
        /// <code>
        /// Button button = container.Add(new Button("theme.conf"), "MyButton");
        /// Button button2 = container.Get("MyButton");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Get<T> (string name) where T : Widget
        {
            int index = m_Names.IndexOf (name);
            if (index != -1)
            {
                if (m_Widgets [index] is T)
                    return (T)m_Widgets [index];
                else
                    return null;
            }
            else
                return null;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Makes a copy of any existing widget
        /// </summary>
        ///
        /// <param name="oldWidget">The widget to copy</param>
        /// <param name="newWidgetName">The optional name of the new widget to get it back later</param>
        ///
        /// <returns>The new widget</returns>
        ///
        /// Usage example:
        /// <code>
        /// Picture pic = container.Add(new Picture("image.png"), "myPicture");
        /// Picture pic2 = container.Copy(pic, "mySecondPicture");
        /// Picture pic3 = container.Copy(container.Get("myPicture"), "myThirdPicture");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Copy<T>(T oldWidget, string newWidgetName = "") where T : Widget
        {
            T widget = (T)System.Activator.CreateInstance (typeof(T), oldWidget);
            m_Widgets.Add(widget);
            m_Names.Add(newWidgetName);
            return widget;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes a widget that was added to the container
        /// </summary>
        ///
        /// <param name="widget">The widget to remove from the container</param>
        ///
        /// Usage example:
        /// <code>
        /// Picture pic1 = container.Add(new Picture("image.png"), "Picture1");
        /// container.Add(new Picture("image.png"), "Picture2");
        /// container.remove(pic1);
        /// container.remove(container.get("Picture2"));
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Remove (Widget widget)
        {
            int index = m_Widgets.IndexOf (widget);
            if (index != -1)
            {
                // Unfocus the widget, just in case it was focused
                widget.Focused = false;

                // Remove the widget from the list
                m_Names.RemoveAt (index);
                m_Widgets.RemoveAt (index);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes a widget that was added to the container
        /// </summary>
        ///
        /// <param name="name">Name of the widget to remove, which was given to the widget when it was added to the container</param>
        ///
        /// Usage example:
        /// <code>
        /// container.Add(new Picture("image.png"), "MyPicture");
        /// container.remove("MyPicture");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (string name)
        {
            int index = m_Names.IndexOf (name);
            if (index != -1)
                Remove (m_Widgets[index]);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes all widgets that were added to the container
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void RemoveAllWidgets ()
        {
            m_Names.Clear ();
            m_Widgets.Clear ();

            m_FocusedWidget = null;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Changes the name of an earlier added widget widget
        /// </summary>
        ///
        /// <param name="widget">Widget of which the name should be changed</param>
        /// <param name="name">New name for the widget</param>
        ///
        /// <returns>True when the name was changed, false when the widget wasn't part of this container</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool SetWidgetName(Widget widget, string name)
        {
            for (int i = 0; i < m_Widgets.Count; ++i)
            {
                if (m_Widgets[i] == widget)
                {
                    m_Names[i] = name;
                    return true;
                }
            }

            return false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the name of a widget
        /// </summary>
        ///
        /// <param name="widget">Widget of which the name should be retrieved</param>
        /// <param name="name">Name for the widget</param>
        ///
        /// <returns>False is returned when the widget wasn't part of this container.
        /// In this case the name parameter is left unchanged.</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool GetWidgetName(Widget widget, ref string name)
        {
            for (int i = 0; i < m_Widgets.Count; ++i)
            {
                if (m_Widgets[i] == widget)
                {
                    name = m_Names[i];
                    return true;
                }
            }

            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Focuses a widget
        /// </summary>
        ///
        /// <param name="widget">The widget that has to be focused</param>
        ///
        /// The previously focused widget will be unfocused.
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
        /// <summary>
        /// Focuses the next widget
        /// </summary>
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
        /// <summary>
        /// Focuses the previous widget
        /// </summary>
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
        /// <summary>
        /// Unfocus all the widgets
        /// </summary>
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
        /// <summary>
        /// Uncheck all the radio buttons
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UncheckRadioButtons ()
        {
            foreach (Widget widget in m_Widgets)
            {
                RadioButton radioButton = widget as RadioButton;

                if (radioButton != null)
                    radioButton.Uncheck ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Places a widget before all other widgets
        /// </summary>
        ///
        /// <param name="widget">The widget that should be moved to the front</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveWidgetToFront (Widget widget)
        {
            int index = m_Widgets.IndexOf (widget);
            if (index != -1)
            {
                m_Widgets.Add (widget);
                m_Widgets.RemoveAt (index);

                m_Names.Add (m_Names[index]);
                m_Names.RemoveAt (index);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Places a widget behind all other widgets
        /// </summary>
        ///
        /// <param name="widget">The widget that should be moved to the back</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveWidgetToBack (Widget widget)
        {
            int index = m_Widgets.IndexOf (widget);
            if (index != -1)
            {
                m_Widgets.Insert (0, widget);
                m_Widgets.RemoveAt (index + 1);

                m_Names.Insert (0, m_Names[index]);
                m_Names.RemoveAt (index + 1);
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
        public override byte Transparency
        {
            set
            {
                base.Transparency = value;

                foreach (Widget widget in m_Widgets)
                    widget.Transparency = m_Opacity;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initializes the widget now that it has been added to a parent widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void Initialize(Container parent)
        {
            base.Initialize(parent);
            GlobalFont = parent.GlobalFont;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update the widgets in the container
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnUpdate ()
        {
            // Loop through all widgets
            for (int i = 0; i < m_Widgets.Count; ++i)
            {
                // Check if the widget is a container or an widget that uses the time
                if (m_Widgets[i].m_AnimatedWidget)
                {
                    // Update the elapsed time
                    m_Widgets[i].m_AnimationTimeElapsed += m_AnimationTimeElapsed;
                    m_Widgets[i].OnUpdate ();
                }
            }

            m_AnimationTimeElapsed = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse has moved on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved (MouseMoveEventArgs e)
        {
            // Adjust the mouse position of the event
            e.X = (int)(e.X - Position.X);
            e.Y = (int)(e.Y - Position.Y);

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
            if (MouseOnWhichWidget(ref theWidget, e.X, e.Y))
                theWidget.OnMouseMoved(e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been pressed on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed (MouseButtonEventArgs e)
        {
            // Adjust the mouse position of the event
            e.X = (int)(e.X - Position.X);
            e.Y = (int)(e.Y - Position.Y);

            // Check if the mouse is on top of an widget
            Widget widget = null;
            if (MouseOnWhichWidget(ref widget, e.X, e.Y))
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


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            // Adjust the mouse position of the event
            e.X = (int)(e.X - Position.X);
            e.Y = (int)(e.Y - Position.Y);

            // Check if the mouse is on top of an widget
            Widget theWidget = null;
            if (MouseOnWhichWidget(ref theWidget, e.X, e.Y))
                theWidget.OnLeftMouseReleased(e);

            // Tell all the other widgets that the mouse has gone up
            foreach (Widget widget in m_Widgets)
            {
                if (widget != theWidget)
                    widget.MouseNoLongerDown();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse wheel has moved while the mouse was on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseWheelMoved (MouseWheelEventArgs e)
        {
            // Adjust the mouse position of the event
            e.X = (int)(e.X - Position.X);
            e.Y = (int)(e.Y - Position.Y);

            // Send the event to the widget under the mouse
            Widget widget = null;
            if (MouseOnWhichWidget (ref widget, e.X, e.Y))
                widget.OnMouseWheelMoved (e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse has moved on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNotOnWidget()
        {
            if (m_MouseHover == true)
            {
                MouseLeftWidget();

                foreach (Widget widget in m_Widgets) {
                    widget.MouseNotOnWidget ();
                }

                m_MouseHover = false;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNoLongerDown()
        {
            base.MouseNoLongerDown ();

            // Tell the widgets that the mouse is no longer down
            foreach (Widget widget in m_Widgets) {
                widget.MouseNoLongerDown ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that it has been unfocused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnWidgetUnfocused()
        {
            if (m_FocusedWidget != null)
            {
                m_FocusedWidget.m_Focused = false;
                m_FocusedWidget.OnWidgetUnfocused();
                m_FocusedWidget = null;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw all visible widgets of this container
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        internal void DrawContainer (RenderTarget target, RenderStates states)
        {
            // Draw all widgets when they are visible
            foreach (Widget widget in m_Widgets)
            {
                if (widget.Visible)
                    target.Draw (widget, states);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Focuses the next widget in this container as long as there are still focusable widgets after the current one
        /// </summary>
        ///
        /// <returns>True when another widget was focused,
        /// false when the last widget was focused and now none of the widgets is focused.</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool FocusNextWidgetInContainer ()
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
        /// <summary>
        /// Handle the tab key, focus the next widget when needed.
        /// </summary>
        ///
        /// When the tab key is pressed then this function is called. The focus will move to the next widget (if there is one).
        /// This function will only work when tabKeyUsageEnabled is true.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void TabKeyPressed ()
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
                ((Container)(m_FocusedWidget)).TabKeyPressed ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Checks above which widget the mouse is standing
        /// </summary>
        ///
        /// <returns>The top widget below the mouse, or null when the mouse isn't on any widget</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool MouseOnWhichWidget(ref Widget theWidget, float x, float y)
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

        private Font m_GlobalFont;
        private List<string> m_Names = new List<string>();
        protected List<Widget> m_Widgets = new List<Widget>();

        protected Widget m_FocusedWidget = null;

        internal bool m_ContainerFocused = false;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class GuiContainer : Container
    {
        internal RenderWindow m_Window;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the widget
        /// </summary>
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
        /// <summary>
        /// Ask the widget if the mouse is on top of it
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override bool MouseOnWidget(float x, float y)
        {
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw (RenderTarget target, RenderStates states)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that a special key has been pressed while the widget was focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        internal void OnKeyPressed (object sender, KeyEventArgs e)
        {
            // Only continue when the character was recognised
            if (e.Code != Keyboard.Key.Unknown)
            {
                // Check if there is a focused widget
                if (m_FocusedWidget != null)
                {
                    // Tell the widget that the key was pressed
                    m_FocusedWidget.OnKeyPressed(e);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that a special key has been released while the widget was focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        internal void OnKeyReleased (object sender, KeyEventArgs e)
        {
            // Change the focus to another widget when the tab key was pressed
            if (e.Code == Keyboard.Key.Tab)
                TabKeyPressed();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that text has been typed while the widget was focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        internal void OnTextEntered (object sender, TextEventArgs e)
        {
            // Check if the character that we pressed is allowed
            if ((e.Unicode[0] >= 32) && (e.Unicode[0] != 127))
            {
                // Tell the focused widget that the key was pressed
                if (m_FocusedWidget != null)
                    m_FocusedWidget.OnTextEntered(e);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
