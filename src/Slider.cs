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
    public class Slider : Widget
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Slider ()
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
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a Slider section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Slider (string configFileFilename)
        {
            m_DraggableWidget = true;
            m_LoadedConfigFile = Global.ResourcePath + configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (m_LoadedConfigFile, "Slider");

            // Find the folder that contains the config file
            string configFileFolder = m_LoadedConfigFile.Substring(0, m_LoadedConfigFile.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

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
                                    + "' in section Slider in " + m_LoadedConfigFile + ".");
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

                    m_TextureTrackNormal_M.texture.texture.Repeated = true;
                }
                else
                    throw new Exception("Not all needed images were loaded for the slider. Is the Slider section in " + m_LoadedConfigFile + " complete?");

                // Check if optional textures were loaded
                if ((m_TextureTrackHover_L.texture != null) && (m_TextureTrackHover_M.texture != null)
                    && (m_TextureTrackHover_R.texture != null) && (m_TextureThumbHover.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;

                    m_TextureTrackHover_M.texture.texture.Repeated = true;
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
                    throw new Exception("Not all needed images were loaded for the slider. Is the Slider section in " + m_LoadedConfigFile + " complete?");

                // Check if optional textures were loaded
                if ((m_TextureTrackHover_M.texture != null) && (m_TextureThumbHover.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Destructor
        /// </summary>
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
        /// Position of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override Vector2f Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                base.Position = value;

                if (m_SplitImage)
                {
                    m_TextureTrackNormal_L.sprite.Position = new Vector2f(value.X, value.Y);
                    m_TextureTrackHover_L.sprite.Position = new Vector2f(value.X, value.Y);

                    // Swap the width and height meanings depending on how the slider lies
                    float width;
                    float height;
                    if (m_VerticalScroll)
                    {
                        width = m_Size.X;
                        height = m_Size.Y;
                    }
                    else
                    {
                        width = m_Size.Y;
                        height = m_Size.X;
                    }

                    if (m_VerticalImage)
                    {
                        // Check if the middle image may be drawn
                        if ((m_TextureTrackNormal_M.sprite.Scale.X * (m_TextureTrackNormal_L.Size.Y + m_TextureTrackNormal_R.Size.Y)) < height)
                        {
                            float scalingX = width / m_TextureTrackNormal_M.Size.X;

                            m_TextureTrackNormal_M.sprite.Position = new Vector2f(value.X, value.Y + (m_TextureTrackNormal_L.Size.Y * m_TextureTrackNormal_L.sprite.Scale.Y));
                            m_TextureTrackHover_M.sprite.Position = new Vector2f(value.X, value.Y + (m_TextureTrackHover_L.Size.Y * m_TextureTrackHover_L.sprite.Scale.Y));

                            m_TextureTrackNormal_R.sprite.Position = new Vector2f(value.X, m_TextureTrackNormal_M.sprite.Position.Y + (m_TextureTrackNormal_M.sprite.TextureRect.Height * scalingX));
                            m_TextureTrackHover_R.sprite.Position = new Vector2f(value.X, m_TextureTrackHover_M.sprite.Position.Y + (m_TextureTrackHover_M.sprite.TextureRect.Height * scalingX));
                        }
                        else // The middle image isn't drawn
                        {
                            m_TextureTrackNormal_R.sprite.Position = new Vector2f(value.X, value.Y + (m_TextureTrackNormal_L.Size.Y * m_TextureTrackNormal_L.sprite.Scale.Y));
                            m_TextureTrackHover_R.sprite.Position = new Vector2f(value.X, value.Y + (m_TextureTrackHover_L.Size.Y * m_TextureTrackHover_L.sprite.Scale.Y));
                        }
                    }
                    else // The slider image lies vertical
                    {
                        // Check if the middle image may be drawn
                        if ((m_TextureTrackNormal_M.sprite.Scale.Y * (m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_R.Size.X)) < height)
                        {
                            float scalingY = width / m_TextureTrackNormal_M.Size.Y;

                            m_TextureTrackNormal_M.sprite.Position = new Vector2f(value.X + (m_TextureTrackNormal_L.Size.X * m_TextureTrackNormal_L.sprite.Scale.X), value.Y);
                            m_TextureTrackHover_M.sprite.Position = new Vector2f(value.X + (m_TextureTrackHover_L.Size.X * m_TextureTrackHover_L.sprite.Scale.X), value.Y);

                            m_TextureTrackNormal_R.sprite.Position = new Vector2f(m_TextureTrackNormal_M.sprite.Position.X + (m_TextureTrackNormal_M.sprite.TextureRect.Width * scalingY), value.Y);
                            m_TextureTrackHover_R.sprite.Position = new Vector2f(m_TextureTrackHover_M.sprite.Position.X + (m_TextureTrackHover_M.sprite.TextureRect.Width * scalingY), value.Y);
                        }
                        else // The middle image isn't drawn
                        {
                            m_TextureTrackNormal_R.sprite.Position = new Vector2f(value.X + (m_TextureTrackNormal_L.Size.X * m_TextureTrackNormal_L.sprite.Scale.X), value.Y);
                            m_TextureTrackHover_R.sprite.Position = new Vector2f(value.X + (m_TextureTrackHover_L.Size.X * m_TextureTrackHover_L.sprite.Scale.X), value.Y);
                        }
                    }
                }
                else // The images aren't split
                {
                    m_TextureTrackNormal_M.sprite.Position = new Vector2f(value.X, value.Y);
                    m_TextureTrackHover_M.sprite.Position = new Vector2f(value.X, value.Y);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the slider
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

                // Apply the miniumum size and set the texture rect of the middle image
                if (m_SplitImage)
                {
                    float scaling;
                    if (m_VerticalImage)
                    {
                        if (m_VerticalScroll)
                        {
                            scaling = m_Size.X / m_TextureTrackNormal_M.Size.X;
                            float minimumHeight = (m_TextureTrackNormal_L.Size.Y + m_TextureTrackNormal_R.Size.Y) * scaling;

                            if (m_Size.Y < minimumHeight)
                                m_Size.Y = minimumHeight;

                            m_TextureTrackNormal_M.sprite.TextureRect = new IntRect(0, 0, (int)m_TextureTrackNormal_M.Size.X, (int)((m_Size.Y - minimumHeight) / scaling));
                            m_TextureTrackHover_M.sprite.TextureRect = new IntRect(0, 0, (int)m_TextureTrackHover_M.Size.X, (int)((m_Size.Y - minimumHeight) / scaling));
                        }
                        else
                        {
                            scaling = m_Size.Y / m_TextureTrackNormal_M.Size.X;
                            float minimumWidth = (m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_R.Size.X) * scaling;

                            if (m_Size.X < minimumWidth)
                                m_Size.X = minimumWidth;

                            m_TextureTrackNormal_M.sprite.TextureRect = new IntRect(0, 0, (int)m_TextureTrackNormal_M.Size.X, (int)((m_Size.X - minimumWidth) / scaling));
                            m_TextureTrackHover_M.sprite.TextureRect = new IntRect(0, 0, (int)m_TextureTrackNormal_M.Size.X, (int)((m_Size.X - minimumWidth) / scaling));
                        }
                    }
                    else
                    {
                        if (m_VerticalScroll)
                        {
                            scaling = m_Size.X / m_TextureTrackNormal_M.Size.Y;
                            float minimumHeight = (m_TextureTrackNormal_L.Size.Y + m_TextureTrackNormal_R.Size.Y) * scaling;

                            if (m_Size.Y < minimumHeight)
                                m_Size.Y = minimumHeight;

                            m_TextureTrackNormal_M.sprite.TextureRect = new IntRect(0, 0, (int)((m_Size.Y - minimumHeight) / scaling), (int)m_TextureTrackNormal_M.Size.Y);
                            m_TextureTrackHover_M.sprite.TextureRect = new IntRect(0, 0, (int)((m_Size.Y - minimumHeight) / scaling), (int)m_TextureTrackHover_M.Size.Y);
                        }
                        else
                        {
                            scaling = m_Size.Y / m_TextureTrackNormal_M.Size.Y;
                            float minimumWidth = (m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_R.Size.X) * scaling;

                            if (m_Size.X < minimumWidth)
                                m_Size.X = minimumWidth;

                            m_TextureTrackNormal_M.sprite.TextureRect = new IntRect(0, 0, (int)((m_Size.X - minimumWidth) / scaling), (int)m_TextureTrackNormal_M.Size.Y);
                            m_TextureTrackHover_M.sprite.TextureRect = new IntRect(0, 0, (int)((m_Size.X - minimumWidth) / scaling), (int)m_TextureTrackHover_M.Size.Y);
                        }
                    }

                    m_TextureTrackNormal_L.sprite.Scale = new Vector2f(scaling, scaling);
                    m_TextureTrackHover_L.sprite.Scale = new Vector2f(scaling, scaling);

                    m_TextureTrackNormal_M.sprite.Scale = new Vector2f(scaling, scaling);
                    m_TextureTrackHover_M.sprite.Scale = new Vector2f(scaling, scaling);

                    m_TextureTrackNormal_R.sprite.Scale = new Vector2f(scaling, scaling);
                    m_TextureTrackHover_R.sprite.Scale = new Vector2f(scaling, scaling);
                }
                else // The image is not split
                {
                    if (m_VerticalImage == m_VerticalScroll)
                    {
                        m_TextureTrackNormal_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureTrackNormal_M.Size.X, m_Size.Y / m_TextureTrackNormal_M.Size.Y);
                        m_TextureTrackHover_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureTrackHover_M.Size.X, m_Size.Y / m_TextureTrackHover_M.Size.Y);
                    }
                    else
                    {
                        m_TextureTrackNormal_M.sprite.Scale = new Vector2f(m_Size.Y / m_TextureTrackNormal_M.Size.X, m_Size.X / m_TextureTrackNormal_M.Size.Y);
                        m_TextureTrackHover_M.sprite.Scale = new Vector2f(m_Size.Y / m_TextureTrackHover_M.Size.X, m_Size.X / m_TextureTrackHover_M.Size.Y);
                    }
                }

                // Apply the scaling to the thumb image
                if (m_VerticalImage)
                {
                    if (m_VerticalScroll)
                    {
                        m_TextureThumbNormal.sprite.Scale = new Vector2f(m_Size.X / m_TextureTrackNormal_M.Size.X, m_Size.X / m_TextureTrackNormal_M.Size.X);
                        m_TextureThumbHover.sprite.Scale = new Vector2f(m_Size.X / m_TextureTrackHover_M.Size.X, m_Size.X / m_TextureTrackHover_M.Size.X);
                    }
                    else // Slider is displayed horizontal
                    {
                        m_TextureThumbNormal.sprite.Scale = new Vector2f(m_Size.Y / m_TextureTrackNormal_M.Size.X, m_Size.Y / m_TextureTrackNormal_M.Size.X);
                        m_TextureThumbHover.sprite.Scale = new Vector2f(m_Size.Y / m_TextureTrackHover_M.Size.X, m_Size.Y / m_TextureTrackHover_M.Size.X);
                    }
                }
                else // Slider image lies horizontal
                {
                    if (m_VerticalScroll)
                    {
                        m_TextureThumbNormal.sprite.Scale = new Vector2f(m_Size.X / m_TextureTrackNormal_M.Size.Y, m_Size.X / m_TextureTrackNormal_M.Size.Y);
                        m_TextureThumbHover.sprite.Scale = new Vector2f(m_Size.X / m_TextureTrackHover_M.Size.Y, m_Size.X / m_TextureTrackHover_M.Size.Y);
                    }
                    else // Slider is displayed horizontal
                    {
                        m_TextureThumbNormal.sprite.Scale = new Vector2f(m_Size.Y / m_TextureTrackNormal_M.Size.Y, m_Size.Y / m_TextureTrackNormal_M.Size.Y);
                        m_TextureThumbHover.sprite.Scale = new Vector2f(m_Size.Y / m_TextureTrackHover_M.Size.Y, m_Size.Y / m_TextureTrackHover_M.Size.Y);
                    }
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
                else // m_VerticalImage != m_VerticalScroll
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

                // Recalculate the position of the images
                Position = Position;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Minimum value of the slider
        /// </summary>
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
        /// <summary>
        /// Maximum value of the scrollbar
        /// </summary>
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
        /// <summary>
        /// Value of the slider
        /// </summary>
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
                    if (ValueChangedCallback != null)
                    {
                        m_Callback.Trigger = CallbackTrigger.ValueChanged;
                        m_Callback.Value   = Value;
                        ValueChangedCallback (this, m_Callback);
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Does the slider lie vertically?
        /// </summary>
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
                        Size = new Vector2f(m_Size.Y, m_Size.X);
                    else
                        Size = new Vector2f(m_Size.X, m_Size.Y);
                }
                else // The slider lies horizontal
                {
                    if (m_Size.Y > m_Size.X)
                        Size = new Vector2f(m_Size.Y, m_Size.X);
                    else
                        Size = new Vector2f(m_Size.X, m_Size.Y);
                }
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
        /// <summary>
        /// Ask the widget if the mouse is on top of it
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override bool MouseOnWidget(float x, float y)
        {
            // Calculate the thumb position
            float thumbLeft;
            float thumbTop;
            if (m_VerticalScroll)
            {
                thumbLeft = (m_Size.X - m_ThumbSize.X) / 2.0f;
                thumbTop = (((float)(Value - m_Minimum) / (m_Maximum - m_Minimum)) * m_Size.Y) - (m_ThumbSize.Y / 2.0f);
            }
            else // The slider lies horizontal
            {
                thumbLeft = (((float)(Value - m_Minimum) / (m_Maximum - m_Minimum)) * m_Size.X) - (m_ThumbSize.X / 2.0f);
                thumbTop = (m_Size.Y - m_ThumbSize.Y) / 2.0f;
            }

            // Check if the mouse is on top of the thumb
            if (new FloatRect(Position.X + thumbLeft, Position.Y + thumbTop, m_ThumbSize.X, m_ThumbSize.Y).Contains(x, y))
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
            m_MouseDown = false;
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
        /// <summary>
        /// Tells the widget that the mouse wheel has moved while the mouse was on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseWheelMoved (MouseWheelEventArgs e)
        {
            if (Value - e.Delta < m_Minimum)
                Value = m_Minimum;
            else
                Value = Value - e.Delta;
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
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Remember the current transformation
            Transform oldTransform = states.Transform;

            // Check if the image is split
            if (m_SplitImage)
            {
                // Set the rotation
                if ((m_VerticalImage == true) && (m_VerticalScroll == false))
                {
                    states.Transform.Rotate(-90,
                                            Position.X + m_TextureTrackNormal_L.Size.X * 0.5f * (m_Size.Y / m_TextureTrackNormal_L.Size.X),
                                            Position.Y + m_TextureTrackNormal_L.Size.X * 0.5f * (m_Size.Y / m_TextureTrackNormal_M.Size.X));
                }
                else if ((m_VerticalImage == false) && (m_VerticalScroll == true))
                {
                    states.Transform.Rotate(90,
                                            Position.X + m_TextureTrackNormal_L.Size.Y * 0.5f * (m_Size.X / m_TextureTrackNormal_L.Size.Y),
                                            Position.Y + m_TextureTrackNormal_L.Size.Y * 0.5f * (m_Size.X / m_TextureTrackNormal_L.Size.Y));
                }

                if (m_SeparateHoverImage)
                {
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        target.Draw(m_TextureTrackHover_L.sprite, states);
                        target.Draw(m_TextureTrackHover_M.sprite, states);
                        target.Draw(m_TextureTrackHover_R.sprite, states);
                    }
                    else
                    {
                        target.Draw(m_TextureTrackNormal_L.sprite, states);
                        target.Draw(m_TextureTrackNormal_M.sprite, states);
                        target.Draw(m_TextureTrackNormal_R.sprite, states);
                    }
                }
                else // The hover image is drawn on top of the normal one
                {
                    target.Draw(m_TextureTrackNormal_L.sprite, states);
                    target.Draw(m_TextureTrackNormal_M.sprite, states);
                    target.Draw(m_TextureTrackNormal_R.sprite, states);

                    // When the mouse is on top of the button then draw an extra image
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        target.Draw(m_TextureTrackHover_L.sprite, states);
                        target.Draw(m_TextureTrackHover_M.sprite, states);
                        target.Draw(m_TextureTrackHover_R.sprite, states);
                    }
                }
            }
            else // The image is not split
            {
                // Set the rotation
                if ((m_VerticalImage == true) && (m_VerticalScroll == false))
                {
                    states.Transform.Rotate(-90,
                                            Position.X + m_TextureTrackNormal_M.Size.X * 0.5f * (m_Size.Y / m_TextureTrackNormal_M.Size.X),
                                            Position.Y + m_TextureTrackNormal_M.Size.X * 0.5f * (m_Size.Y / m_TextureTrackNormal_M.Size.X));
                }
                else if ((m_VerticalImage == false) && (m_VerticalScroll == true))
                {
                    states.Transform.Rotate(90,
                                            Position.X + m_TextureTrackNormal_M.Size.Y * 0.5f * (m_Size.X / m_TextureTrackNormal_M.Size.Y),
                                            Position.Y + m_TextureTrackNormal_M.Size.Y * 0.5f * (m_Size.X / m_TextureTrackNormal_M.Size.Y));
                }

                if (m_SeparateHoverImage)
                {
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

            // Reset the transform
            states.Transform = oldTransform;
            states.Transform *= Transform;

            // The thumb will be on a different position when we are scrolling vertically or not
            if (m_VerticalScroll)
            {
                // Set the translation and scale for the thumb
                states.Transform.Translate((m_Size.X - m_ThumbSize.X) * 0.5f,
                                           (((float)(Value - m_Minimum) / (m_Maximum - m_Minimum)) * m_Size.Y) - (m_ThumbSize.Y * 0.5f));
            }
            else // the slider lies horizontal
            {
                // Set the translation and scale for the thumb
                states.Transform.Translate((((float)(Value - m_Minimum) / (m_Maximum - m_Minimum)) * m_Size.X) - (m_ThumbSize.X * 0.5f),
                                           (m_Size.Y - m_ThumbSize.Y) * 0.5f);
            }

            // It is possible that the image is not drawn in the same direction than the loaded image
            if ((m_VerticalImage == true) && (m_VerticalScroll == false))
            {
                states.Transform.Rotate(-90,
                                        m_TextureThumbNormal.Size.X * 0.5f * (m_Size.Y / m_TextureTrackNormal_M.Size.X),
                                        m_TextureThumbNormal.Size.X * 0.5f * (m_Size.Y / m_TextureTrackNormal_M.Size.X));
            }
            else if ((m_VerticalImage == false) && (m_VerticalScroll == true))
            {
                states.Transform.Rotate(90,
                                        m_TextureThumbNormal.Size.Y * 0.5f * (m_Size.X / m_TextureTrackNormal_M.Size.Y),
                                        m_TextureThumbNormal.Size.Y * 0.5f * (m_Size.X / m_TextureTrackNormal_M.Size.Y));
            }

            // Draw the normal thumb image
            target.Draw(m_TextureThumbNormal.sprite, states);

            // When the mouse is on top of the slider then draw the hover image
            if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                target.Draw(m_TextureThumbHover.sprite, states);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the ValueChanged event</summary>
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
    }
}
