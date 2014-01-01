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
using Tao.OpenGl;

namespace TGUI
{
    public class Slider2d : ClickableWidget
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Slider2d ()
        {
            m_DraggableWidget = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Slider2d (Slider2d copy) : base(copy)
        {
            ValueChangedCallback = copy.ValueChangedCallback;
            ThumbReturnedToCenterCallback = copy.ThumbReturnedToCenterCallback;

            m_LoadedConfigFile    = copy.m_LoadedConfigFile;
            m_Minimum             = copy.m_Minimum;
            m_Maximum             = copy.m_Maximum;
            m_Value               = copy.m_Value;
            m_ReturnThumbToCenter = copy.m_ReturnThumbToCenter;
            m_FixedThumbSize      = copy.m_FixedThumbSize;
            m_SeparateHoverImage  = copy.m_SeparateHoverImage;

            Global.TextureManager.CopyTexture(copy.m_TextureTrackNormal, m_TextureTrackNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackHover, m_TextureTrackHover);
            Global.TextureManager.CopyTexture(copy.m_TextureThumbNormal, m_TextureThumbNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureThumbHover, m_TextureThumbHover);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a Slider2d section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Slider2d (string configFileFilename)
        {
            m_DraggableWidget = true;
            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "Slider2d");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "separatehoverimage")
                    m_SeparateHoverImage = configFile.ReadBool(i);
                else if (configFile.Properties[i] == "tracknormalimage")
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackNormal);
                else if (configFile.Properties[i] == "trackhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackHover);
                else if (configFile.Properties[i] == "thumbnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureThumbNormal);
                else if (configFile.Properties[i] == "thumbhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureThumbHover);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section Slider2d in " + configFileFilename + ".");
            }

            // Make sure the required textures were loaded
            if ((m_TextureTrackNormal.texture != null) && (m_TextureThumbNormal.texture != null))
            {
                // Set the size of the slider
                m_Size = new Vector2f(m_TextureTrackNormal.Size.X, m_TextureTrackNormal.Size.Y);
            }
            else
            {
                throw new Exception("Not all needed images were loaded for the slider. Is the Slider2d section in " + configFileFilename + " complete?");
            }

            // Check if optional textures were loaded
            if ((m_TextureTrackHover.texture != null) && (m_TextureThumbHover.texture != null))
            {
                m_WidgetPhase |= (byte)WidgetPhase.Hover;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Destructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Slider2d ()
        {
            if (m_TextureTrackNormal.texture != null) Global.TextureManager.RemoveTexture(m_TextureTrackNormal);
            if (m_TextureTrackHover.texture != null)  Global.TextureManager.RemoveTexture(m_TextureTrackHover);
            if (m_TextureThumbNormal.texture != null) Global.TextureManager.RemoveTexture(m_TextureThumbNormal);
            if (m_TextureThumbHover.texture != null)  Global.TextureManager.RemoveTexture(m_TextureThumbHover);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Filename of the config file that was used to load the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string LoadedConfigFile
        {
            get
            {
                return m_LoadedConfigFile;
            }
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
        /// Minimum value of the slider
        /// </summary>
        ///
        /// When the value is too small then it will be changed to this minimum.
        /// The default minimum value is (-1, -1).
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual Vector2f Minimum
        {
            get
            {
                return m_Minimum;
            }
            set
            {
                m_Minimum = value;

                // The minimum can never be greater than the maximum
                if (m_Minimum.X > m_Maximum.X)
                    m_Maximum.X = m_Minimum.X;
                if (m_Minimum.Y > m_Maximum.Y)
                    m_Maximum.Y = m_Minimum.Y;

                // When the value is below the minimum then adjust it
                if (m_Value.X < m_Minimum.X)
                    m_Value.X = m_Minimum.X;
                if (m_Value.Y < m_Minimum.Y)
                    m_Value.Y = m_Minimum.Y;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Maximum value of the slider
        /// </summary>
        ///
        /// When the value is too big then it will be changed to this maximum.
        /// The default maximum value is (1, 1).
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual Vector2f Maximum
        {
            get
            {
                return m_Maximum;
            }
            set
            {
                m_Maximum = value;

                // The maximum can never be below the minimum
                if (m_Maximum.X < m_Minimum.X)
                    m_Minimum.X = m_Maximum.X;
                if (m_Maximum.Y < m_Minimum.Y)
                    m_Minimum.Y = m_Maximum.Y;

                // When the value is above the maximum then adjust it
                if (m_Value.X > m_Maximum.X)
                    m_Value.X = m_Maximum.X;
                if (m_Value.Y > m_Maximum.Y)
                    m_Value.Y = m_Maximum.Y;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Value of the slider
        /// </summary>
        ///
        /// The value can't be smaller than the minimum or bigger than the maximum.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual Vector2f Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;

                // When the value is below the minimum or above the maximum then adjust it
                if (m_Value.X < m_Minimum.X)
                    m_Value.X = m_Minimum.X;
                else if (m_Value.X > m_Maximum.X)
                    m_Value.X = m_Maximum.X;

                if (m_Value.Y < m_Minimum.Y)
                    m_Value.Y = m_Minimum.Y;
                else if (m_Value.Y > m_Maximum.Y)
                    m_Value.Y = m_Maximum.Y;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Should the thumb image be scaled together with the slider image,
        /// or should the thumb keep its size even if your slider is twice as big as the image itself.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool FixedThumbSize
        {
            get
            {
                return m_FixedThumbSize;
            }
            set
            {
                m_FixedThumbSize = true;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Decide whether or not the thumb will jump back to the center when the mouse is released
        /// </summary>
        ///
        /// <param name="autoCenterThumb">Place the thumb in the center on mouse release?</param>
        ///
        /// This behavior is disabled by default.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void EnableThumbCenter (bool autoCenterThumb)
        {
            m_ReturnThumbToCenter = autoCenterThumb;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Place the thumb back in the center
        /// </summary>
        ///
        /// When EnableThumbCenter(true) was called then this will happen automatically when the mouse is released.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CenterThumb ()
        {
            Value = new Vector2f((m_Maximum.X + m_Minimum.X) * 0.5f, (m_Maximum.Y + m_Minimum.Y) * 0.5f);
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

                m_TextureTrackNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureThumbNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureThumbHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
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

            // Refresh the value
            MouseMoveEvent move = new MouseMoveEvent();
            move.X = e.X;
            move.Y = e.Y;
            OnMouseMoved (new MouseMoveEventArgs(move));
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            base.OnLeftMouseReleased(e);

            if (m_ReturnThumbToCenter)
            {
                Value = new Vector2f((m_Maximum.X + m_Minimum.X) * 0.5f, (m_Maximum.Y + m_Minimum.Y) * 0.5f);

                if (ThumbReturnedToCenterCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.ThumbReturnedToCenter;
                    m_Callback.Value2d = m_Value;
                    ThumbReturnedToCenterCallback (this, m_Callback);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse has moved on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved (MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget ();

            m_MouseHover = true;

            // Remember the old value
            Vector2f oldValue = m_Value;

            // Check if the mouse button is down
            if (m_MouseDown)
            {
                // If the position is positive then calculate the correct value
                if ((e.Y - Position.Y) > 0)
                    m_Value.Y = ((e.Y - Position.Y) / m_Size.Y) * (m_Maximum.Y - m_Minimum.Y) + m_Minimum.Y;
                else // The position is negative, the calculation can't be done (but is not needed)
                    m_Value.Y = m_Minimum.Y;

                // If the position is positive then calculate the correct value
                if ((e.X - Position.X) > 0)
                    m_Value.X = ((e.X - Position.X) / m_Size.X) * (m_Maximum.X - m_Minimum.X) + m_Minimum.X;
                else // The position is negative, the calculation can't be done (but is not needed)
                    m_Value.X = m_Minimum.X;

                // Set the new value, making sure that it lies within the minimum and maximum
                Value = m_Value;

                // Add the callback (if the user requested it)
                if (((oldValue.X != m_Value.X) || (oldValue.Y != m_Value.Y)) && (ValueChangedCallback != null))
                {
                    m_Callback.Trigger = CallbackTrigger.ValueChanged;
                    m_Callback.Value2d = m_Value;
                    ValueChangedCallback (this, m_Callback);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that it has been focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnWidgetFocused()
        {
            Focused = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNoLongerDown()
        {
            m_MouseDown = false;

            if (m_ReturnThumbToCenter)
            {
                if ((m_Value.X != (m_Maximum.X + m_Minimum.X) / 2.0f) || (m_Value.Y != (m_Maximum.Y + m_Minimum.Y) / 2.0f))
                {
                    Value = new Vector2f((m_Maximum.X + m_Minimum.X) / 2.0f, (m_Maximum.Y + m_Minimum.Y) / 2.0f);

                    if (ThumbReturnedToCenterCallback != null)
                    {
                        m_Callback.Trigger = CallbackTrigger.ThumbReturnedToCenter;
                        m_Callback.Value2d = m_Value;
                        ThumbReturnedToCenterCallback (this, m_Callback);
                    }
                }
            }
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

            // Adjust the transformation
            states.Transform *= Transform;

            // Calculate the scale of the slider
            Vector2f scaling;
            scaling.X = m_Size.X / m_TextureTrackNormal.Size.X;
            scaling.Y = m_Size.Y / m_TextureTrackNormal.Size.Y;

            // Set the scale of the slider
            states.Transform.Scale(scaling);

            // Draw the track image
            if (m_SeparateHoverImage)
            {
                if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureTrackHover.sprite, states);
                else
                    target.Draw(m_TextureTrackNormal.sprite, states);
            }
            else // The hover image should be drawn on top of the normal image
            {
                target.Draw(m_TextureTrackNormal.sprite, states);

                if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureTrackHover.sprite, states);
            }

            // Undo the scale
            states.Transform.Scale(1.0f / scaling.X, 1.0f / scaling.Y);

            // Check if the thumb should be scaled together with the slider
            if (m_FixedThumbSize)
            {
                states.Transform.Translate((((m_Value.X - m_Minimum.X) / (m_Maximum.X - m_Minimum.X)) * m_TextureTrackNormal.Size.X * scaling.X) - (m_TextureThumbNormal.Size.X * 0.5f),
                                           (((m_Value.Y - m_Minimum.Y) / (m_Maximum.Y - m_Minimum.Y)) * m_TextureTrackNormal.Size.Y * scaling.Y) - (m_TextureThumbNormal.Size.Y * 0.5f));
            }
            else // The thumb must be scaled
            {
                states.Transform.Translate((((m_Value.X - m_Minimum.X) / (m_Maximum.X - m_Minimum.X)) * m_TextureTrackNormal.Size.X * scaling.X) - (m_TextureThumbNormal.Size.X * 0.5f * scaling.Y),
                                           (((m_Value.Y - m_Minimum.Y) / (m_Maximum.Y - m_Minimum.Y)) * m_TextureTrackNormal.Size.Y * scaling.Y) - (m_TextureThumbNormal.Size.Y * 0.5f * scaling.X));

                // Set the scale for the thumb
                states.Transform.Scale(scaling);
            }

            // Get the old clipping area
            int[] scissor = new int[4];
            Gl.glGetIntegerv(Gl.GL_SCISSOR_BOX, scissor);

            // Calculate the clipping area
            int scissorLeft = System.Math.Max((int)(topLeftPosition.X * scaleViewX), scissor[0]);
            int scissorTop = System.Math.Max((int)(topLeftPosition.Y * scaleViewY), (int)(target.Size.Y) - scissor[1] - scissor[3]);
            int scissorRight = System.Math.Min((int)(bottomRightPosition.X * scaleViewX), scissor[0] + scissor[2]);
            int scissorBottom = System.Math.Min((int)(bottomRightPosition.Y * scaleViewY), (int)(target.Size.Y) - scissor[1]);

            // If the object outside the window then don't draw anything
            if (scissorRight < scissorLeft)
                scissorRight = scissorLeft;
            else if (scissorBottom < scissorTop)
                scissorTop = scissorBottom;

            // Set the clipping area
            Gl.glScissor(scissorLeft, (int)(target.Size.Y - scissorBottom), scissorRight - scissorLeft, scissorBottom - scissorTop);

            // Draw the thumb image
            if (m_SeparateHoverImage)
            {
                if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureThumbHover.sprite, states);
                else
                    target.Draw(m_TextureThumbNormal.sprite, states);
            }
            else // The hover image should be drawn on top of the normal image
            {
                target.Draw(m_TextureThumbNormal.sprite, states);

                if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureThumbHover.sprite, states);
            }

            // Reset the old clipping area
            Gl.glScissor(scissor[0], scissor[1], scissor[2], scissor[3]);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the ValueChanged event</summary>
        public event EventHandler<CallbackArgs> ValueChangedCallback;

        /// <summary>Event handler for the ThumbReturnedToCenter event</summary>
        public event EventHandler<CallbackArgs> ThumbReturnedToCenterCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string   m_LoadedConfigFile = "";

        private Vector2f m_Minimum = new Vector2f(-1, -1);
        private Vector2f m_Maximum = new Vector2f(1, 1);
        private Vector2f m_Value = new Vector2f(0, 0);

        private bool     m_ReturnThumbToCenter = false;
        private bool     m_FixedThumbSize = true;

        private Impl.Sprite m_TextureTrackNormal = new Impl.Sprite();
        private Impl.Sprite m_TextureTrackHover = new Impl.Sprite();
        private Impl.Sprite m_TextureThumbNormal = new Impl.Sprite();
        private Impl.Sprite m_TextureThumbHover = new Impl.Sprite();

        private bool     m_SeparateHoverImage = false;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
