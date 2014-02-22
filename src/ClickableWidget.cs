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
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    public class ClickableWidget : Widget
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ClickableWidget ()
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
        public ClickableWidget (ClickableWidget copy) : base(copy)
        {
            LeftMousePressedCallback  = copy.LeftMousePressedCallback;
            LeftMouseReleasedCallback = copy.LeftMouseReleasedCallback;
            LeftMouseClickedCallback  = copy.LeftMouseClickedCallback;

            m_Size = copy.m_Size;
        }


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
                return m_Size;
            }
            set
            {
                m_Size = value;
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
            if (Transform.TransformRect(new FloatRect(0, 0, Size.X, Size.Y)).Contains(x, y))
                return true;
            else
            {
                if (m_MouseHover)
                    MouseLeftWidget();

                m_MouseHover = false;
                return false;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been pressed on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed (MouseButtonEventArgs e)
        {
            m_MouseDown = true;

            SendLeftMousePressedCallback (e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            SendLeftMouseReleasedCallback (e);

            if (m_MouseDown == true)
            {
                SendLeftMouseClickedCallback (e);

                m_MouseDown = false;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw (SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Send a left mouse pressed callback
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void SendLeftMousePressedCallback (MouseButtonEventArgs e)
        {
            if (LeftMousePressedCallback != null)
            {
                m_Callback.Mouse = new Vector2i (e.X, e.Y);
                m_Callback.Trigger = CallbackTrigger.LeftMousePressed;
                LeftMousePressedCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Send a left mouse released callback
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void SendLeftMouseReleasedCallback (MouseButtonEventArgs e)
        {
            if (LeftMouseReleasedCallback != null)
            {
                m_Callback.Mouse = new Vector2i (e.X, e.Y);
                m_Callback.Trigger = CallbackTrigger.LeftMousePressed;
                LeftMouseReleasedCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Send a left mouse clicked callback
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void SendLeftMouseClickedCallback (MouseButtonEventArgs e)
        {
            if (LeftMouseClickedCallback != null)
            {
                m_Callback.Mouse = new Vector2i (e.X, e.Y);
                m_Callback.Trigger = CallbackTrigger.LeftMousePressed;
                LeftMouseClickedCallback (this, m_Callback);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the LeftMousePressed event</summary>
        public event EventHandler<CallbackArgs> LeftMousePressedCallback;

        /// <summary>Event handler for the LeftMouseReleased event</summary>
        public event EventHandler<CallbackArgs> LeftMouseReleasedCallback;

        /// <summary>Event handler for the LeftMouseClicked event</summary>
        public event EventHandler<CallbackArgs> LeftMouseClickedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        internal Vector2f m_Size = new Vector2f();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
