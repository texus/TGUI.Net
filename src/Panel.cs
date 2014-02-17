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
using SFML.Window;
using SFML.Graphics;
using Tao.OpenGl;

namespace TGUI
{
    public class Panel : Container
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Panel ()
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
        public Panel (Panel copy) : base(copy)
        {
            LeftMousePressedCallback = copy.LeftMousePressedCallback;
            LeftMouseReleasedCallback = copy.LeftMouseReleasedCallback;
            LeftMouseClickedCallback = copy.LeftMouseClickedCallback;

            m_Size            = copy.m_Size;
            m_BackgroundColor = copy.m_BackgroundColor;
            m_Texture         = copy.m_Texture;
            
            if (m_Texture != null)
            {
                m_Sprite.Texture = m_Texture;
                m_Sprite.Scale = new Vector2f(m_Size.X / m_Texture.Size.X, m_Size.Y / m_Texture.Size.Y);
                m_Sprite.Color = new Color (255, 255, 255, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the panel
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

                // If there is a background texture then resize it
                if (m_Texture != null)
                    m_Sprite.Scale = new Vector2f(m_Size.X / m_Texture.Size.X, m_Size.Y / m_Texture.Size.Y);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Background texture of the panel
        /// </summary>
        ///
        /// If the texture has a different size than the panel then it will be scaled to fill the whole panel.
        /// Pass null to this function to remove the background texture.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SFML.Graphics.Texture BackgroundTexture
        {
            get
            {
                return m_Texture;
            }
            set
            {
                m_Texture = value;

                // Set the texture for the sprite
                if (m_Texture != null)
                {
                    m_Sprite.Texture = m_Texture;
                    m_Sprite.Scale = new Vector2f(m_Size.X / m_Texture.Size.X, m_Size.Y / m_Texture.Size.Y);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Background color of the panel.
        /// Fully transparent by default.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color BackgroundColor
        {
            get
            {
                return m_BackgroundColor;
            }
            set
            {
                m_BackgroundColor = value;
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

                m_Sprite.Color = new Color(255, 255, 255, m_Opacity);
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
            // Check if the mouse is inside the panel
            if (Transform.TransformRect(new FloatRect(0, 0, m_Size.X, m_Size.Y)).Contains(x, y))
                return true;

            if (m_MouseHover)
            {
                MouseLeftWidget();

                // Tell the widgets inside the panel that the mouse is no longer on top of them
                for (int i = 0; i < m_Widgets.Count; ++i)
                    m_Widgets[i].MouseNotOnWidget();

                m_MouseHover = false;
            }

            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been pressed on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed (MouseButtonEventArgs e)
        {
            if (MouseOnWidget(e.X, e.Y))
            {
                m_MouseDown = true;

                if (LeftMousePressedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.LeftMousePressed;
                    m_Callback.Mouse.X = (int)(e.X - Position.X);
                    m_Callback.Mouse.Y = (int)(e.Y - Position.Y);
                    LeftMousePressedCallback (this, m_Callback);
                }
            }

            base.OnLeftMousePressed (e);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            if (MouseOnWidget(e.X, e.Y))
            {
                if (LeftMouseReleasedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.LeftMouseReleased;
                    m_Callback.Mouse.X = (int)(e.X - Position.X);
                    m_Callback.Mouse.Y = (int)(e.Y - Position.Y);
                    LeftMouseReleasedCallback (this, m_Callback);
                }

                if (m_MouseDown)
                {
                    if (LeftMouseClickedCallback != null)
                    {
                        m_Callback.Trigger = CallbackTrigger.LeftMouseClicked;
                        m_Callback.Mouse.X = (int)(e.X - Position.X);
                        m_Callback.Mouse.Y = (int)(e.Y - Position.Y);
                        LeftMouseClickedCallback (this, m_Callback);
                    }
                }
            }

            m_MouseDown = false;

            base.OnLeftMouseReleased (e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Calculate the scale factor of the view
            float scaleViewX = target.Size.X / target.GetView().Size.X;
            float scaleViewY = target.Size.Y / target.GetView().Size.Y;

            // Get the global position
            Vector2f topLeftPosition = states.Transform.TransformPoint(Position - target.GetView().Center + (target.GetView().Size / 2.0f));
            Vector2f bottomRightPosition = states.Transform.TransformPoint(Position + m_Size - target.GetView().Center + (target.GetView().Size / 2.0f));

            // Get the old clipping area
            int[] scissor = new int[4];
            Gl.glGetIntegerv(Gl.GL_SCISSOR_BOX, scissor);

            // Calculate the clipping area
            int scissorLeft = System.Math.Max((int)(topLeftPosition.X * scaleViewX), scissor[0]);
            int scissorTop = System.Math.Max((int)(topLeftPosition.Y * scaleViewY), (int)(target.Size.Y) - scissor[1] - scissor[3]);
            int scissorRight = System.Math.Min((int)(bottomRightPosition.X * scaleViewX), scissor[0] + scissor[2]);
            int scissorBottom = System.Math.Min((int)(bottomRightPosition.Y * scaleViewY), (int)(target.Size.Y) - scissor[1]);

            // If the widget outside the window then don't draw anything
            if (scissorRight < scissorLeft)
                scissorRight = scissorLeft;
            else if (scissorBottom < scissorTop)
                scissorTop = scissorBottom;

            // Set the clipping area
            Gl.glScissor(scissorLeft, (int)(target.Size.Y - scissorBottom), scissorRight - scissorLeft, scissorBottom - scissorTop);

            // Set the transform
            states.Transform *= Transform;

            // Draw the background
            if (m_BackgroundColor.A != 0)
            {
                RectangleShape background = new RectangleShape(m_Size);
                background.FillColor = m_BackgroundColor;
                target.Draw(background, states);
            }

            // Draw the background texture if there is one
            if (m_Texture != null)
                target.Draw(m_Sprite, states);

            // Draw the widgets
            DrawContainer(target, states);

            // Reset the old clipping area
            Gl.glScissor(scissor[0], scissor[1], scissor[2], scissor[3]);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the LeftMousePressed event</summary>
        public event EventHandler<CallbackArgs> LeftMousePressedCallback;

        /// <summary>Event handler for the LeftMouseReleased event</summary>
        public event EventHandler<CallbackArgs> LeftMouseReleasedCallback;

        /// <summary>Event handler for the LeftMouseClicked event</summary>
        public event EventHandler<CallbackArgs> LeftMouseClickedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private Vector2f m_Size = new Vector2f(100, 100);

        private Color    m_BackgroundColor = new Color(220, 220, 220, 255);

        private SFML.Graphics.Texture m_Texture = null;
        private Sprite  m_Sprite = new Sprite();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
