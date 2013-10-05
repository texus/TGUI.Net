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
using System.Resources;
using SFML.Window;
using SFML.Graphics;
using Tao.OpenGl;

namespace TGUI
{
    public class Gui
    {
        private RenderWindow m_Window;

        private int m_StartTime = Environment.TickCount;

        private bool m_Focused = false;

        private GuiContainer m_Container = new GuiContainer();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Construct the gui and set the window on which the gui should be drawn.
        ///
        /// \param window  The sfml window that will be used by the gui.
        ///
        /// If you use this constructor then you will no longer have to call setWindow yourself.
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
        /// \brief Construct the gui and set the window on which the gui should be drawn.
        ///
        /// \param window  The sfml window that will be used by the gui.
        /// \param manager The resource manager to use when loading widgets.
        ///
        /// If you use this constructor then you will no longer have to call setWindow yourself.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Gui(RenderWindow window, ResourceManager manager) :
            this(window)
        {
            Global.ResourceManager = manager;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Draws all the widgets that were added to the gui.
        ///
        /// \param resetView  Reset the view of the window while drawing the gui.
        ///                   If false, the current view will be used, otherwise the view will be reset.
        ///
        /// When this function ends, the view will never be changed. Any changes to the view are temporary.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Draw (bool resetView = false)
        {
            View oldView = m_Window.GetView();

            // Reset the view when requested
            if (resetView)
                m_Window.SetView(m_Window.DefaultView);

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

            m_Window.SetView(oldView);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Set the window on which the gui should be drawn.
        ///
        /// \param window  The sfml window that will be used by the gui.
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
        /// \brief Check if the window is focused.
        ///
        /// When the window is unfocused, animations (e.g. flashing caret of an edit box) will be paused.
        ///
        /// \return Is the window currently focused?
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
        /// \brief Returns the global font.
        ///
        /// This is the font that is used for newly created widget by default.
        ///
        /// \return global font
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
        /// \brief Returns a list of the names of all the widgets.
        ///
        /// \return Vector of all widget names
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<string> GetWidgetNames()
        {
            return m_Container.GetWidgetNames();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns a list of all the widgets.
        ///
        /// \return Vector of all widget pointers
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Widget> GetWidgets()
        {
            return m_Container.GetWidgets();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Adds an widget to the container.
        ///
        /// \param widgetPtr   Pointer to the widget you would like to add
        /// \param widgetName  If you want to access the widget later then you must do this with this name
        ///
        /// Usage example:
        /// \code
        /// ...
        /// \endcode
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Add<T> (T widget, string name = "") where T : Widget
        {
            return m_Container.Add (widget, name);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns a pointer to an earlier created widget.
        ///
        /// \param widgetName The name that was given to the widget when it was added to the container.
        ///
        /// \return Pointer to the earlier created widget
        ///
        /// \warning This function will return nullptr when an unknown widget name was passed.
        ///
        /// Usage example:
        /// \code
        /// ...
        /// \endcode
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public T Get<T> (string name) where T : Widget
        {
            return m_Container.Get<T> (name);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes a single widget that was added to the container.
        ///
        /// \param widget  Pointer to the widget to remove
        ///
        /// Usage example:
        /// \code
        /// ...
        /// \endcode
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (Widget widget)
        {
            m_Container.Remove (widget);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes all widgets that were added to the container.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (string name)
        {
            m_Container.Remove (name);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes all widgets that were added to the container.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAllWidgets ()
        {
            m_Container.RemoveAllWidgets ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the name of a widget.
        ///
        /// \param widget  Widget of which the name should be changed
        /// \param name    New name for the widget
        ///
        /// \return True when the name was changed, false when the widget wasn't part of this container.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool SetWidgetName(Widget widget, string name)
        {
            return m_Container.SetWidgetName(widget, name);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns the name of a widget.
        ///
        /// \param widget  Widget of which the name should be retrieved
        /// \param name    Name for the widget
        ///
        /// \return False is returned when the widget wasn't part of this container.
        ///         In this case the name parameter is left unchanged.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool GetWidgetName(Widget widget, ref string name)
        {
            return m_Container.GetWidgetName(widget, ref name);
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
            m_Container.FocusWidget (widget);
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
            m_Container.FocusNextWidget ();
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
            m_Container.FocusPreviousWidget ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Unfocus all the widgets.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UnfocusWidgets ()
        {
            m_Container.UnfocusWidgets ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Uncheck all the radio buttons.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void UncheckRadioButtons ()
        {
            m_Container.UncheckRadioButtons ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Places an widget before all other widgets.
        ///
        /// \param widget  The widget that should be moved to the front
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveWidgetToFront (Widget widget)
        {
            m_Container.MoveWidgetToFront (widget);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Places an widget behind all other widgets.
        ///
        /// \param widget  The widget that should be moved to the back
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void MoveWidgetToBack (Widget widget)
        {
            m_Container.MoveWidgetToBack (widget);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Used to clean-up the internal widgets and textures.
        /// This is a temporary way to avoid crashes on exit.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CleanUp ()
        {
            m_Container.RemoveAllWidgets ();
            Global.TextureManager.m_ImageMap.Clear ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnMouseMoved (object sender, MouseMoveEventArgs e)
        {
            Vector2f mouseCoords = m_Window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            e.X = (int)mouseCoords.X;
            e.Y = (int)mouseCoords.Y;

            m_Container.m_EventManager.OnMouseMoved (sender, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnMousePressed (object sender, MouseButtonEventArgs e)
        {
            Vector2f mouseCoords = m_Window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            e.X = (int)mouseCoords.X;
            e.Y = (int)mouseCoords.Y;

            m_Container.m_EventManager.OnMousePressed (sender, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnMouseReleased (object sender, MouseButtonEventArgs e)
        {
            Vector2f mouseCoords = m_Window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            e.X = (int)mouseCoords.X;
            e.Y = (int)mouseCoords.Y;

            m_Container.m_EventManager.OnMouseReleased (sender, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnMouseWheelMoved (object sender, MouseWheelEventArgs e)
        {
            Vector2f mouseCoords = m_Window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            e.X = (int)mouseCoords.X;
            e.Y = (int)mouseCoords.Y;

            m_Container.m_EventManager.OnMouseWheelMoved (sender, e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

