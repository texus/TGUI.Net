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

using System;
using System.Collections.Generic;
using System.Resources;
using SFML.Window;
using SFML.Graphics;
using Tao.OpenGl;

namespace TGUI
{
    public class Gui
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the gui and set the window on which the gui should be drawn
        /// </summary>
        ///
        /// <param name="window">The sfml window that will be used by the gui</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Gui (RenderWindow window)
        {
            m_Window = window;
            m_Container.m_Window = window;
            m_Container.m_ContainerFocused = true;

            window.MouseMoved += new EventHandler<MouseMoveEventArgs>(OnMouseMoved);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMousePressed);
            window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(OnMouseReleased);
            window.KeyPressed += new EventHandler<KeyEventArgs>(m_Container.m_EventManager.OnKeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(m_Container.m_EventManager.OnKeyReleased);
            window.TextEntered += new EventHandler<TextEventArgs>(m_Container.m_EventManager.OnTextEntered);
            window.MouseWheelMoved += new EventHandler<MouseWheelEventArgs>(OnMouseWheelMoved);

            window.LostFocus += (s, e) => m_Focused = false;
            window.GainedFocus += (s, e) => m_Focused = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the gui and set the window on which the gui should be drawn
        /// </summary>
        ///
        /// <param name="window">The sfml window that will be used by the gui</param>
        /// <param name="manager">The resource manager to use when loading widgets</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Gui(RenderWindow window, ResourceManager manager) :
            this(window)
        {
            Global.ResourceManager = manager;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws all the widgets that were added to the gui
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Draw ()
        {
            // Update the time
            int currentTime = Environment.TickCount;
            if (m_Focused)
                m_Container.m_EventManager.UpdateTime (currentTime - m_StartTime);
            m_StartTime = currentTime;

            // Check if clipping is enabled
            int clippingEnabled = Gl.glIsEnabled(Gl.GL_SCISSOR_TEST);
            int[] scissor;
            scissor = new int[4];

            if (clippingEnabled != 0)
            {
                // Remember the old clipping area
                Gl.glGetIntegerv(Gl.GL_SCISSOR_BOX, scissor);
            }
            else // Clipping was disabled
            {
                // Enable clipping
                Gl.glEnable(Gl.GL_SCISSOR_TEST);
                Gl.glScissor(0, 0, (int)m_Window.Size.X, (int)m_Window.Size.Y);
            }

            // Draw the window with all widgets inside it
            m_Container.DrawContainer(m_Window, SFML.Graphics.RenderStates.Default);

            // Reset clipping to its original state
            if (clippingEnabled != 0)
                Gl.glScissor(scissor[0], scissor[1], scissor[2], scissor[3]);
            else
                Gl.glDisable(Gl.GL_SCISSOR_TEST);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Window on which the gui should be drawn
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public RenderWindow Window
        {
            get
            {
                return m_Window;
            }
            set
            {
                m_Window = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if the window is focused
        /// </summary>
        ///
        /// When the window is unfocused, animations (e.g. flashing caret of an edit box) will be paused.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Focused
        {
            get
            {
                return m_Focused;
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
                return m_Container.GlobalFont;
            }
            set
            {
                m_Container.GlobalFont = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns a list of the names of all the widgets
        /// </summary>
        ///
        /// <returns>List of all widget names</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<string> GetWidgetNames()
        {
            return m_Container.GetWidgetNames();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns a list of all the widgets
        /// </summary>
        ///
        /// <returns>List of all widget</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Widget> GetWidgets()
        {
            return m_Container.GetWidgets();
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
        /// Button button = gui.Add(new Button("theme.conf"), "MyButton");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Add<T> (T widget, string name = "") where T : Widget
        {
            return m_Container.Add (widget, name);
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
        /// Button button = gui.Add(new Button("theme.conf"), "MyButton");
        /// Button button2 = gui.Get("MyButton");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Get<T> (string name) where T : Widget
        {
            return m_Container.Get<T> (name);
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
        /// Picture pic = gui.Add(new Picture("image.png"), "myPicture");
        /// Picture pic2 = gui.Copy(pic, "mySecondPicture");
        /// Picture pic3 = gui.Copy(gui.Get("myPicture"), "myThirdPicture");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Copy<T>(T oldWidget, string newWidgetName = "") where T : Widget
        {
            return m_Container.Copy<T> (oldWidget, newWidgetName);
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
        /// Picture pic1 = gui.Add(new Picture("image.png"), "Picture1");
        /// gui.Add(new Picture("image.png"), "Picture2");
        /// gui.remove(pic1);
        /// gui.remove(gui.get("Picture2"));
        /// </code>
        ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (Widget widget)
        {
            m_Container.Remove (widget);
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
        /// gui.Add(new Picture("image.png"), "MyPicture");
        /// gui.remove("MyPicture");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (string name)
        {
            m_Container.Remove (name);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes all widgets that were added to the container
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAllWidgets ()
        {
            m_Container.RemoveAllWidgets ();
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
            return m_Container.SetWidgetName(widget, name);
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
            return m_Container.GetWidgetName(widget, ref name);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Focuses an widget
        /// </summary>
        ///
        /// <param name="widget">The widget that has to be focused</param>
        ///
        /// The previously focused widget will be unfocused.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void FocusWidget (Widget widget)
        {
            m_Container.FocusWidget (widget);
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
            m_Container.FocusNextWidget ();
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
            m_Container.FocusPreviousWidget ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Unfocus all the widgets
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UnfocusWidgets ()
        {
            m_Container.UnfocusWidgets ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Uncheck all the radio buttons
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UncheckRadioButtons ()
        {
            m_Container.UncheckRadioButtons ();
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
            m_Container.MoveWidgetToFront (widget);
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
            m_Container.MoveWidgetToBack (widget);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Used to clean-up the internal widgets and textures.
        /// This is a temporary way to avoid crashes on exit (with mono).
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CleanUp ()
        {
            m_Container.RemoveAllWidgets ();
            Global.TextureManager.m_ImageMap.Clear ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widgets that the mouse has moved
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnMouseMoved (object sender, MouseMoveEventArgs e)
        {
            Vector2f mouseCoords = m_Window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            e.X = (int)mouseCoords.X;
            e.Y = (int)mouseCoords.Y;

            m_Container.m_EventManager.OnMouseMoved (sender, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widgets that the left mouse button has been pressed
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnMousePressed (object sender, MouseButtonEventArgs e)
        {
            Vector2f mouseCoords = m_Window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            e.X = (int)mouseCoords.X;
            e.Y = (int)mouseCoords.Y;

            m_Container.m_EventManager.OnMousePressed (sender, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widgets that the left mouse button has been released
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnMouseReleased (object sender, MouseButtonEventArgs e)
        {
            Vector2f mouseCoords = m_Window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            e.X = (int)mouseCoords.X;
            e.Y = (int)mouseCoords.Y;

            m_Container.m_EventManager.OnMouseReleased (sender, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widgets that the mouse wheel has moved
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnMouseWheelMoved (object sender, MouseWheelEventArgs e)
        {
            Vector2f mouseCoords = m_Window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            e.X = (int)mouseCoords.X;
            e.Y = (int)mouseCoords.Y;

            m_Container.m_EventManager.OnMouseWheelMoved (sender, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private RenderWindow m_Window;

        private int m_StartTime = Environment.TickCount;

        private bool m_Focused = true;

        private GuiContainer m_Container = new GuiContainer();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
