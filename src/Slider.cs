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
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    public class Slider : Widget
    {
        public event EventHandler<CallbackArgs> ValueChangedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected string   m_LoadedConfigFile = "";

        // When the mouse went down, did it go down on top of the thumb? If so, where?
        protected internal bool m_MouseDownOnThumb = false;
        protected Vector2f      m_MouseDownOnThumbPos = new Vector2f();

        protected int      m_Minimum = 0;
        protected int      m_Maximum = 10;
        protected int      m_Value = 0;

        // Is the slider draw vertically?
        protected bool     m_VerticalScroll = true;

        // Does the image lie vertically?
        protected bool     m_VerticalImage = true;

        // If this is true then the L, M and R images will be used.
        // If it is false then the slider is just one big image that will be stored in the M image.
        protected bool     m_SplitImage = false;

        // Is there a separate hover image, or is it a semi-transparent image that is drawn on top of the others?
        protected bool     m_SeparateHoverImage = false;

        // The size of the slider and its thumb
        protected Vector2f m_Size;
        protected Vector2f m_ThumbSize;

        protected Impl.Sprite m_TextureTrackNormal_L = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackNormal_M = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackNormal_R = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackHover_L = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackHover_M = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackHover_R = new Impl.Sprite();
        protected Impl.Sprite m_TextureThumbNormal = new Impl.Sprite();
        protected Impl.Sprite m_TextureThumbHover = new Impl.Sprite();


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Slider ()
        {
            m_DraggableWidget = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Slider (Slider copy) : base(copy)
        {
            ValueChangedCallback = copy.ValueChangedCallback;

            m_LoadedConfigFile    = copy.m_LoadedConfigFile;
            m_MouseDownOnThumb    = copy.m_MouseDownOnThumb;
            m_MouseDownOnThumbPos = copy.m_MouseDownOnThumbPos;
            m_Minimum             = copy.m_Minimum;
            m_Maximum             = copy.m_Maximum;
            m_Value               = copy.m_Value;
            m_VerticalScroll      = copy.m_VerticalScroll;
            m_VerticalImage       = copy.m_VerticalImage;
            m_SplitImage          = copy.m_SplitImage;
            m_SeparateHoverImage  = copy.m_SeparateHoverImage;
            m_Size                = copy.m_Size;
            m_ThumbSize           = copy.m_ThumbSize;

            Global.TextureManager.CopyTexture(copy.m_TextureTrackNormal_L, m_TextureTrackNormal_L);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackNormal_M, m_TextureTrackNormal_M);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackNormal_R, m_TextureTrackNormal_R);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackHover_L, m_TextureTrackHover_L);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackHover_M, m_TextureTrackHover_M);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackHover_R, m_TextureTrackHover_R);
            Global.TextureManager.CopyTexture(copy.m_TextureThumbNormal, m_TextureThumbNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureThumbHover, m_TextureThumbHover);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a Slider section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Slider (string configFileFilename)
        {
            m_DraggableWidget = true;
            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "Slider");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "separatehoverimage")
                    m_SeparateHoverImage = configFile.ReadBool(i);
                else if (configFile.Properties[i] == "verticalscroll")
                {
                    m_VerticalScroll = configFile.ReadBool(i);
                    m_VerticalImage = m_VerticalScroll;
                }
                else if (configFile.Properties[i] == "tracknormalimage")
                {
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackNormal_M);
                    m_SplitImage = false;
                }
                else if (configFile.Properties[i] == "trackhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackHover_M);
                else if (configFile.Properties[i] == "thumbnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureThumbNormal);
                else if (configFile.Properties[i] == "thumbhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureThumbHover);
                else if (configFile.Properties[i] == "tracknormalimage_l")
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackNormal_L);
                else if (configFile.Properties[i] == "tracknormalimage_m")
                {
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackNormal_M);
                    m_SplitImage = true;
                }
                else if (configFile.Properties[i] == "tracknormalimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackNormal_R);
                else if (configFile.Properties[i] == "trackhoverimage_l")
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackHover_L);
                else if (configFile.Properties[i] == "trackhoverimage_m")
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackHover_M);
                else if (configFile.Properties[i] == "trackhoverimage_r")
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackHover_R);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section Slider in " + configFileFilename + ".");
            }

            // Check if the image is split
            if (m_SplitImage)
            {
                // Make sure the required textures were loaded
                if ((m_TextureTrackNormal_L.texture != null) && (m_TextureTrackNormal_M.texture != null)
                    && (m_TextureTrackNormal_R.texture != null) && (m_TextureThumbNormal.texture != null))
                {
                    // Set the size of the slider
                    if (m_VerticalImage)
                        m_Size = new Vector2f((float)(m_TextureTrackNormal_M.Size.X), (float)(m_TextureTrackNormal_L.Size.Y + m_TextureTrackNormal_M.Size.Y + m_TextureTrackNormal_R.Size.Y));
                    else
                        m_Size = new Vector2f((float)(m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_M.Size.X + m_TextureTrackNormal_R.Size.X), (float)(m_TextureTrackNormal_M.Size.Y));

                    // Set the thumb size
                    m_ThumbSize = new Vector2f(m_TextureThumbNormal.Size.X, m_TextureThumbNormal.Size.Y);
                }
                else
                    throw new Exception("Not all needed images were loaded for the slider. Is the Slider section in " + configFileFilename + " complete?");

                // Check if optional textures were loaded
                if ((m_TextureTrackHover_L.texture != null) && (m_TextureTrackHover_M.texture != null)
                    && (m_TextureTrackHover_R.texture != null) && (m_TextureThumbHover.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;
                }
            }
            else // The image isn't split
            {
                // Make sure the required textures were loaded
                if ((m_TextureTrackNormal_M.texture != null) && (m_TextureThumbNormal.texture != null))
                {
                    // Set the size of the slider
                    m_Size = new Vector2f(m_TextureTrackNormal_M.Size.X, m_TextureTrackNormal_M.Size.Y);

                    // Set the thumb size
                    m_ThumbSize = new Vector2f(m_TextureThumbNormal.Size.X, m_TextureThumbNormal.Size.Y);
                }
                else
                    throw new Exception("Not all needed images were loaded for the slider. Is the Slider section in " + configFileFilename + " complete?");

                // Check if optional textures were loaded
                if ((m_TextureTrackHover_M.texture != null) && (m_TextureThumbHover.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Slider ()
        {
            if (m_TextureTrackNormal_L.texture != null) Global.TextureManager.RemoveTexture(m_TextureTrackNormal_L);
            if (m_TextureTrackHover_L.texture != null)  Global.TextureManager.RemoveTexture(m_TextureTrackHover_L);
            if (m_TextureTrackNormal_M.texture != null) Global.TextureManager.RemoveTexture(m_TextureTrackNormal_M);
            if (m_TextureTrackHover_M.texture != null)  Global.TextureManager.RemoveTexture(m_TextureTrackHover_M);
            if (m_TextureTrackNormal_R.texture != null) Global.TextureManager.RemoveTexture(m_TextureTrackNormal_R);
            if (m_TextureTrackHover_R.texture != null)  Global.TextureManager.RemoveTexture(m_TextureTrackHover_R);
            if (m_TextureThumbNormal.texture != null)   Global.TextureManager.RemoveTexture(m_TextureThumbNormal);
            if (m_TextureThumbHover.texture != null)    Global.TextureManager.RemoveTexture(m_TextureThumbHover);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns the filename of the config file that was used to load the widget.
        ///
        /// \return Filename of loaded config file.
        ///         Empty string when no config file was loaded yet.
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
        /// \brief Changes the size of the widget.
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

                // Set the thumb size
                if (m_VerticalImage == m_VerticalScroll)
                {
                    if (m_VerticalScroll)
                    {
                        m_ThumbSize.X = (m_Size.X / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.X;
                        m_ThumbSize.Y = (m_Size.X / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.Y;
                    }
                    else
                    {
                        m_ThumbSize.X = (m_Size.Y / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.X;
                        m_ThumbSize.Y = (m_Size.Y / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.Y;
                    }
                }
                else
                {
                    if (m_VerticalScroll)
                    {
                        m_ThumbSize.X = (m_Size.X / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.Y;
                        m_ThumbSize.Y = (m_Size.X / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.X;
                    }
                    else
                    {
                        m_ThumbSize.X = (m_Size.Y / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.Y;
                        m_ThumbSize.Y = (m_Size.Y / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.X;
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Sets a minimum value.
        ///
        /// \param minimum  The new minimum value
        ///
        /// When the value is too small then it will be changed to this minimum.
        /// When the maximum value is lower than the new minimum then it will be changed to this new minimum value.
        /// The default minimum value is 0.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual int Minimum
        {
            get
            {
                return m_Minimum;
            }
            set
            {
                m_Minimum = value;

                // The minimum can never be greater than the maximum
                if (m_Minimum > m_Maximum)
                    m_Maximum = m_Minimum;

                // When the value is below the minimum then adjust it
                if (Value < m_Minimum)
                    Value = m_Minimum;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Sets a maximum value.
        ///
        /// \param maximum  The new maximum value
        ///
        /// When the value is too big then it will be changed to this maximum.
        /// When the minimum value is higher than the new maximum then it will be changed to this new maximum value.
        /// The default maximum value is 10.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual int Maximum
        {
            get
            {
                return m_Maximum;
            }
            set
            {
                m_Maximum = value;

                // The maximum can never be below the minimum
                if (m_Maximum < m_Minimum)
                    m_Minimum = m_Maximum;

                // When the value is above the maximum then adjust it
                if (Value > m_Maximum)
                    Value = m_Maximum;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the current value.
        ///
        /// \param value  The new value
        ///
        /// The value can't be smaller than the minimum or bigger than the maximum.
        /// The default value is 0.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual int Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (m_Value != value)
                {
                    // Set the new value
                    m_Value = value;

                    // When the value is below the minimum or above the maximum then adjust it
                    if (m_Value < m_Minimum)
                        m_Value = m_Minimum;
                    else if (m_Value > m_Maximum)
                        m_Value = m_Maximum;

                    // Add the callback (if the user requested it)
                    SendValueChangedCallback ();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes whether the slider lies vertical or horizontal.
        ///
        /// \param verticallScroll  Does the slider lie vertically?
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool VerticalScroll
        {
            get
            {
                return m_VerticalScroll;
            }
            set
            {
                m_VerticalScroll = value;

                // Swap the width and height if needed
                if (m_VerticalScroll)
                {
                    if (m_Size.X > m_Size.Y)
                        m_Size = new Vector2f(m_Size.Y, m_Size.X);
                }
                else // The slider lies horizontal
                {
                    if (m_Size.Y > m_Size.X)
                        m_Size = new Vector2f(m_Size.Y, m_Size.X);
                }

                // Set the thumb size
                if (m_VerticalImage == m_VerticalScroll)
                {
                    if (m_VerticalScroll)
                    {
                        m_ThumbSize.X = (m_Size.X / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.X;
                        m_ThumbSize.Y = (m_Size.X / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.Y;
                    }
                    else
                    {
                        m_ThumbSize.X = (m_Size.Y / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.X;
                        m_ThumbSize.Y = (m_Size.Y / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.Y;
                    }
                }
                else
                {
                    if (m_VerticalScroll)
                    {
                        m_ThumbSize.X = (m_Size.X / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.Y;
                        m_ThumbSize.Y = (m_Size.X / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.X;
                    }
                    else
                    {
                        m_ThumbSize.X = (m_Size.Y / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.Y;
                        m_ThumbSize.Y = (m_Size.Y / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.X;
                    }
                }
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

                m_TextureTrackNormal_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackHover_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackNormal_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackHover_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackNormal_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackHover_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureThumbNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureThumbHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override bool MouseOnWidget(float x, float y)
        {
            // Calculate the thumb size and position
            float thumbWidth, thumbHeight;
            float thumbLeft,  thumbTop;

            // The size is different when the image is rotated
            if (m_VerticalImage == m_VerticalScroll)
            {
                thumbWidth = m_ThumbSize.X;
                thumbHeight = m_ThumbSize.Y;
            }
            else
            {
                thumbWidth = m_ThumbSize.Y;
                thumbHeight = m_ThumbSize.X;
            }

            // Calculate the thumb position
            if (m_VerticalScroll)
            {
                thumbLeft = (m_Size.X - thumbWidth) / 2.0f;
                thumbTop = (((float)(Value - m_Minimum) / (m_Maximum - m_Minimum)) * m_Size.Y) - (thumbHeight / 2.0f);
            }
            else // The slider lies horizontal
            {
                thumbLeft = (((float)(Value - m_Minimum) / (m_Maximum - m_Minimum)) * m_Size.X) - (thumbWidth / 2.0f);
                thumbTop = (m_Size.Y - thumbHeight) / 2.0f;
            }

            // Check if the mouse is on top of the thumb
            if (new FloatRect(Position.X + thumbLeft, Position.Y + thumbTop, thumbWidth, thumbHeight).Contains(x, y))
            {
                m_MouseDownOnThumb = true;
                m_MouseDownOnThumbPos.X = x - Position.X - thumbLeft;
                m_MouseDownOnThumbPos.Y = y - Position.Y - thumbTop;
                return true;
            }
            else // The mouse is not on top of the thumb
                m_MouseDownOnThumb = false;

            // Check if the mouse is on top of the track
            if (Transform.TransformRect(new FloatRect(0, 0, m_Size.X, m_Size.Y)).Contains(x, y))
                return true;

            if (m_MouseHover)
                MouseLeftWidget();

            // The mouse is not on top of the slider
            m_MouseHover = false;
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed(MouseButtonEventArgs e)
        {
            m_MouseDown = true;

            // Refresh the value
            MouseMoveEvent move = new MouseMoveEvent();
            move.X = e.X;
            move.Y = e.Y;
            OnMouseMoved (new MouseMoveEventArgs(move));
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased(MouseButtonEventArgs e)
        {
            m_MouseDown = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved(MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget();

            m_MouseHover = true;

            // Check if the mouse button is down
            if (m_MouseDown)
            {
                // Check in which direction the slider goes
                if (m_VerticalScroll)
                {
                    // Check if the thumb is being dragged
                    if (m_MouseDownOnThumb)
                    {
                        // Set the new value
                        if ((e.Y - m_MouseDownOnThumbPos.Y + (m_ThumbSize.Y / 2.0f) - Position.Y) > 0)
                            Value = (int)((((e.Y - m_MouseDownOnThumbPos.Y + (m_ThumbSize.Y / 2.0f) - Position.Y) / m_Size.Y) * (m_Maximum - m_Minimum)) + m_Minimum + 0.5f);
                        else
                            Value = m_Minimum;
                    }
                    else // The click occured on the track
                    {
                        // If the position is positive then calculate the correct value
                        if ((e.Y - Position.Y) > 0)
                            Value = (int)((((e.Y - Position.Y) / m_Size.Y) * (m_Maximum - m_Minimum)) + m_Minimum + 0.5f);
                        else // The position is negative, the calculation can't be done (but is not needed)
                            Value = m_Minimum;
                    }
                }
                else // the slider lies horizontal
                {
                    // Check if the thumb is being dragged
                    if (m_MouseDownOnThumb)
                    {
                        // Set the new value
                        if ((e.X - m_MouseDownOnThumbPos.X + (m_ThumbSize.X / 2.0f) - Position.X) > 0)
                            Value = (int)((((e.X - m_MouseDownOnThumbPos.X + (m_ThumbSize.X / 2.0f) - Position.X) / m_Size.X) * (m_Maximum - m_Minimum)) + m_Minimum + 0.5f);
                        else
                            Value = m_Minimum;
                    }
                    else // The click occured on the track
                    {
                        // If the position is positive then calculate the correct value
                        if (e.X - Position.X > 0)
                            Value = (int)((((e.X - Position.X) / m_Size.X) * (m_Maximum - m_Minimum)) + m_Minimum + 0.5f);
                        else // The position is negative, the calculation can't be done (but is not needed)
                            Value = m_Minimum;
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseWheelMoved(MouseWheelEventArgs e)
        {
            if (Value - e.Delta < m_Minimum)
                Value = m_Minimum;
            else
                Value = Value - e.Delta;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnWidgetFocused()
        {
            Focused = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // Draws the widget on the render target.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            Vector2f scaling = new Vector2f();

            // Apply the transformation
            states.Transform *= Transform;

            // Remember the current transformation
            Transform oldTransform = states.Transform;

            // Check if the image is split
            if (m_SplitImage)
            {
                // Get the scale factors
                if (m_VerticalScroll == m_VerticalImage)
                {
                    scaling.X = m_Size.X / (m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_M.Size.X + m_TextureTrackNormal_R.Size.X);
                    scaling.Y = m_Size.Y / m_TextureTrackNormal_M.Size.Y;
                }
                else
                {
                    // Check in what direction the slider should rotate
                    if ((m_VerticalImage == true) && (m_VerticalScroll == false))
                    {
                        // Set the rotation
                        states.Transform.Rotate(-90,
                                                (m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_M.Size.X + m_TextureTrackNormal_R.Size.X) * 0.5f,
                                                m_TextureTrackNormal_M.Size.X * 0.5f);
                    }
                    else // if ((m_VerticalImage == false) && (m_VerticalScroll == true))
                    {
                        // Set the rotation
                        states.Transform.Rotate(90,
                                                (m_TextureTrackNormal_L.Size.Y + m_TextureTrackNormal_M.Size.Y + m_TextureTrackNormal_R.Size.Y) * 0.5f,
                                                m_TextureTrackNormal_M.Size.Y * 0.5f);
                    }

                    scaling.X = m_Size.X / (m_TextureTrackNormal_L.Size.Y + m_TextureTrackNormal_M.Size.Y + m_TextureTrackNormal_R.Size.Y);
                    scaling.Y = m_Size.Y / m_TextureTrackNormal_M.Size.X;
                }

                // Set the scale for the left image
                states.Transform.Scale(scaling.Y, scaling.Y);

                // Draw the left image
                {
                    // Check if there is a separate hover image
                    if (m_SeparateHoverImage)
                    {
                        // Draw the correct image
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureTrackHover_L.sprite, states);
                        else
                            target.Draw(m_TextureTrackNormal_L.sprite, states);
                    }
                    else
                    {
                        // Draw the normal track image
                        target.Draw(m_TextureTrackNormal_L.sprite, states);

                        // When the mouse is on top of the slider then draw the hover image
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureTrackHover_L.sprite, states);
                    }
                }

                // Check if the middle image may be drawn
                if ((scaling.Y * (m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_R.Size.X))
                    < scaling.X * (m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_M.Size.X + m_TextureTrackNormal_R.Size.X))
                {
                    // Calculate the scale for our middle image
                    float scaleX = (((m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_M.Size.X + m_TextureTrackNormal_R.Size.X)  * scaling.X)
                                    - ((m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_R.Size.X) * scaling.Y))
                        / m_TextureTrackNormal_M.Size.X;

                    // Put the middle image on the correct position
                    states.Transform.Translate((float)(m_TextureTrackNormal_L.Size.X), 0);

                    // Set the scale for the middle image
                    states.Transform.Scale(scaleX / scaling.Y, 1);

                    // Draw the middle image
                    {
                        // Check if there is a separate hover image
                        if (m_SeparateHoverImage)
                        {
                            // Draw the correct image
                            if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                                target.Draw(m_TextureTrackHover_M.sprite, states);
                            else
                                target.Draw(m_TextureTrackNormal_M.sprite, states);
                        }
                        else
                        {
                            // Draw the normal track image
                            target.Draw(m_TextureTrackNormal_M.sprite, states);

                            // When the mouse is on top of the slider then draw the hover image
                            if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                                target.Draw(m_TextureTrackHover_M.sprite, states);
                        }
                    }

                    // Put the right image on the correct position
                    states.Transform.Translate((float)(m_TextureTrackNormal_M.Size.X), 0);

                    // Set the scale for the right image
                    states.Transform.Scale(scaling.Y / scaleX, 1);
                }
                else // The middle image is not drawn
                    states.Transform.Translate((float)(m_TextureTrackNormal_L.Size.X), 0);

                // Draw the right image
                {
                    // Check if there is a separate hover image
                    if (m_SeparateHoverImage)
                    {
                        // Draw the correct image
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureTrackHover_R.sprite, states);
                        else
                            target.Draw(m_TextureTrackNormal_R.sprite, states);
                    }
                    else
                    {
                        // Draw the normal track image
                        target.Draw(m_TextureTrackNormal_R.sprite, states);

                        // When the mouse is on top of the slider then draw the hover image
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureTrackHover_R.sprite, states);
                    }
                }
            }
            else // The image is not split
            {
                if (m_VerticalScroll == m_VerticalImage)
                {
                    // Set the scaling
                    scaling.X = m_Size.X / m_TextureTrackNormal_M.Size.X;
                    scaling.Y = m_Size.Y / m_TextureTrackNormal_M.Size.Y;
                    states.Transform.Scale(scaling);
                }
                else
                {
                    // Set the scaling
                    scaling.X = m_Size.X / m_TextureTrackNormal_M.Size.Y;
                    scaling.Y = m_Size.Y / m_TextureTrackNormal_M.Size.X;
                    states.Transform.Scale(scaling);

                    // Set the rotation
                    if ((m_VerticalImage == true) && (m_VerticalScroll == false))
                        states.Transform.Rotate(-90, m_TextureTrackNormal_M.Size.X * 0.5f, m_TextureTrackNormal_M.Size.X * 0.5f);
                    else // if ((m_VerticalImage == false) && (m_VerticalScroll == true))
                        states.Transform.Rotate(90, m_TextureTrackNormal_M.Size.Y * 0.5f, m_TextureTrackNormal_M.Size.Y * 0.5f);
                }

                // Draw the normal track image
                target.Draw(m_TextureTrackNormal_M.sprite, states);

                // When the mouse is on top of the slider then draw the hover image
                if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureTrackHover_M.sprite, states);
            }

            // Reset the transform
            states.Transform = oldTransform;

            // The thumb will be on a different position when we are scrolling vertically or not
            if (m_VerticalScroll)
            {
                // Set the translation and scale for the thumb
                states.Transform.Translate((int)(m_Size.X - m_ThumbSize.X) * 0.5f,
                                           (((float)(Value - m_Minimum) / (m_Maximum - m_Minimum)) * m_Size.Y) - (m_ThumbSize.Y * 0.5f));

                // Set the scale for the thumb
                states.Transform.Scale(scaling.X, scaling.X);
            }
            else // the slider lies horizontal
            {
                // Set the translation and scale for the thumb
                states.Transform.Translate((((float)(Value - m_Minimum) / (m_Maximum - m_Minimum)) * m_Size.X) - (m_ThumbSize.X * 0.5f),
                                           (m_Size.Y - m_ThumbSize.Y) * 0.5f);

                // Set the scale for the thumb
                states.Transform.Scale(scaling.Y, scaling.Y);
            }

            // It is possible that the image is not drawn in the same direction than the loaded image
            if ((m_VerticalImage == true) && (m_VerticalScroll == false))
            {
                // Set the rotation
                states.Transform.Rotate(-90, m_TextureThumbNormal.Size.X * 0.5f, m_TextureThumbNormal.Size.X * 0.5f);
            }
            else if ((m_VerticalImage == false) && (m_VerticalScroll == true))
            {
                // Set the rotation
                states.Transform.Rotate(90, m_TextureThumbNormal.Size.Y * 0.5f, m_TextureThumbNormal.Size.Y * 0.5f);
            }

            // Draw the normal thumb image
            target.Draw(m_TextureThumbNormal.sprite, states);

            // When the mouse is on top of the slider then draw the hover image
            if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                target.Draw(m_TextureThumbHover.sprite, states);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void SendValueChangedCallback ()
        {
            if (ValueChangedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.ValueChanged;
                m_Callback.Value   = Value;
                ValueChangedCallback (this, m_Callback);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

