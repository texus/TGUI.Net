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
            for (int i = 0; i < copy.m_EventManager.m_Widgets.Count; ++i)
            {
                m_EventManager.m_Widgets.Add ((Widget)System.Activator.CreateInstance (copy.m_EventManager.m_Widgets[i].GetType(), copy.m_EventManager.m_Widgets[i]));
                m_Names.Add(copy.m_Names[i]);

                m_EventManager.m_Widgets[m_EventManager.m_Widgets.Count-1].m_Parent = this;
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
            return m_EventManager.m_Widgets;
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

            m_EventManager.m_Widgets.Add (widget);
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
                if (m_EventManager.m_Widgets [index] is T)
                    return (T)m_EventManager.m_Widgets [index];
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
            m_EventManager.m_Widgets.Add(widget);
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
            int index = m_EventManager.m_Widgets.IndexOf (widget);
            if (index != -1)
            {
                // Unfocus the widget, just in case it was focused
                widget.Focused = false;

                // Remove the widget from the list
                m_Names.RemoveAt (index);
                m_EventManager.m_Widgets.RemoveAt (index);
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
                Remove (m_EventManager.m_Widgets[index]);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes all widgets that were added to the container
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void RemoveAllWidgets ()
        {
            m_EventManager.UnfocusWidgets ();

            m_Names.Clear ();
            m_EventManager.m_Widgets.Clear ();
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
            for (int i = 0; i < m_EventManager.m_Widgets.Count; ++i)
            {
                if (m_EventManager.m_Widgets[i] == widget)
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
            for (int i = 0; i < m_EventManager.m_Widgets.Count; ++i)
            {
                if (m_EventManager.m_Widgets[i] == widget)
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
            m_EventManager.FocusWidget (widget);
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
            m_EventManager.FocusNextWidget ();
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
            m_EventManager.FocusPreviousWidget ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Unfocus all the widgets
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UnfocusWidgets ()
        {
            m_EventManager.UnfocusWidgets ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Uncheck all the radio buttons
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UncheckRadioButtons ()
        {
            foreach (Widget widget in m_EventManager.m_Widgets)
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
        /// <summary>
        /// Places a widget behind all other widgets
        /// </summary>
        ///
        /// <param name="widget">The widget that should be moved to the back</param>
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

                foreach (Widget widget in m_EventManager.m_Widgets)
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
            m_EventManager.UpdateTime(m_AnimationTimeElapsed);
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

            // Let the event manager handle the event
            m_EventManager.OnMouseMoved(this, e);
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

            // Let the event manager handle the event
            m_EventManager.OnMousePressed(this, e);
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

            // Let the event manager handle the event
            m_EventManager.OnMouseReleased(this, e);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that a special key has been pressed while the widget was focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnKeyPressed (KeyEventArgs e)
        {
            // Let the event manager handle the event
            m_EventManager.OnKeyPressed(this, e);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that text has been typed while the widget was focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnTextEntered (TextEventArgs e)
        {
            m_EventManager.OnTextEntered(this, e);
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

            m_EventManager.OnMouseWheelMoved(this, e);
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
                m_EventManager.MouseNotOnWidget();

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
            m_EventManager.MouseNoLongerDown ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that it has been unfocused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnWidgetUnfocused()
        {
            m_EventManager.UnfocusWidgets();
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
            foreach (Widget widget in m_EventManager.m_Widgets)
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
        internal bool FocusNextWidgetInContainer ()
        {
            return m_EventManager.FocusNextWidgetInContainer ();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private Font m_GlobalFont;
        private List<string> m_Names = new List<string>();

        internal EventManager m_EventManager = new EventManager();
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
    }
}
