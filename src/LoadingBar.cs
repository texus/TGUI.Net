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
    public class LoadingBar : ClickableWidget
    {
        public event EventHandler<CallbackArgs> ValueChangedCallback;
        public event EventHandler<CallbackArgs> LoadingBarFullCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string m_LoadedConfigFile = "";

        private int    m_Minimum = 0;
        private int    m_Maximum = 100;
        private int    m_Value = 0;

        private bool   m_SplitImage = false;

        private Impl.Sprite m_TextureBack_L = new Impl.Sprite();
        private Impl.Sprite m_TextureBack_M = new Impl.Sprite();
        private Impl.Sprite m_TextureBack_R = new Impl.Sprite();
        private Impl.Sprite m_TextureFront_L = new Impl.Sprite();
        private Impl.Sprite m_TextureFront_M = new Impl.Sprite();
        private Impl.Sprite m_TextureFront_R = new Impl.Sprite();

        private Text   m_Text = new Text();
        private uint   m_TextSize = 0;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal LoadingBar ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public LoadingBar (LoadingBar copy) : base(copy)
        {
            ValueChangedCallback = copy.ValueChangedCallback;
            LoadingBarFullCallback = copy.LoadingBarFullCallback;

            m_LoadedConfigFile = copy.m_LoadedConfigFile;
            m_Minimum          = copy.m_Minimum;
            m_Maximum          = copy.m_Maximum;
            m_Value            = copy.m_Value;
            m_SplitImage       = copy.m_SplitImage;
            m_Text             = new Text(copy.m_Text);
            m_TextSize         = copy.m_TextSize;

            Global.TextureManager.CopyTexture(copy.m_TextureBack_L, m_TextureBack_L);
            Global.TextureManager.CopyTexture(copy.m_TextureBack_M, m_TextureBack_M);
            Global.TextureManager.CopyTexture(copy.m_TextureBack_R, m_TextureBack_R);
            Global.TextureManager.CopyTexture(copy.m_TextureFront_L, m_TextureFront_L);
            Global.TextureManager.CopyTexture(copy.m_TextureFront_M, m_TextureFront_M);
            Global.TextureManager.CopyTexture(copy.m_TextureFront_R, m_TextureFront_R);

            RecalculateSize();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a LoadingBar section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public LoadingBar (string configFileFilename)
        {
            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "LoadingBar");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "backimage")
                {
                    configFile.ReadTexture (i, configFileFolder, m_TextureBack_M);
                    m_SplitImage = false;
                }
                else if (configFile.Properties[i] == "frontimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureFront_M);
                else if (configFile.Properties[i] == "backimage_l")
                    configFile.ReadTexture (i, configFileFolder, m_TextureBack_L);
                else if (configFile.Properties[i] == "backimage_m")
                {
                    configFile.ReadTexture(i, configFileFolder, m_TextureBack_M);
                    m_SplitImage = true;
                }
                else if (configFile.Properties[i] == "backimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureBack_R);
                else if (configFile.Properties[i] == "frontimage_l")
                    configFile.ReadTexture(i, configFileFolder, m_TextureFront_L);
                else if (configFile.Properties[i] == "frontimage_m")
                    configFile.ReadTexture (i, configFileFolder, m_TextureFront_M);
                else if (configFile.Properties[i] == "frontimage_r")
                    configFile.ReadTexture (i, configFileFolder, m_TextureFront_R);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section LoadingBar in " + configFileFilename + ".");
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~LoadingBar ()
        {
            if (m_TextureBack_L.texture != null)   Global.TextureManager.RemoveTexture(m_TextureBack_L);
            if (m_TextureBack_M.texture != null)   Global.TextureManager.RemoveTexture(m_TextureBack_M);
            if (m_TextureBack_R.texture != null)   Global.TextureManager.RemoveTexture(m_TextureBack_R);

            if (m_TextureFront_L.texture != null)  Global.TextureManager.RemoveTexture(m_TextureFront_L);
            if (m_TextureFront_M.texture != null)  Global.TextureManager.RemoveTexture(m_TextureFront_M);
            if (m_TextureFront_R.texture != null)  Global.TextureManager.RemoveTexture(m_TextureFront_R);
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

                // Recalculate the size of the front image
                RecalculateSize();

                // Recalculate the text size
                Text = m_Text.DisplayedString;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Minimum
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
                if (m_Value < m_Minimum)
                    m_Value = m_Minimum;

                // Recalculate the size of the front image (the size of the part that will be drawn)
                RecalculateSize();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Maximum
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
                if (m_Value > m_Maximum)
                    m_Value = m_Maximum;

                // Recalculate the size of the front image (the size of the part that will be drawn)
                RecalculateSize();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;

                // When the value is below the minimum or above the maximum then adjust it
                if (m_Value < m_Minimum)
                    m_Value = m_Minimum;
                else if (m_Value > m_Maximum)
                    m_Value = m_Maximum;

                // Recalculate the size of the front image (the size of the part that will be drawn)
                RecalculateSize();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int IncrementValue ()
        {
            // When the value is still below the maximum then adjust it
            if (m_Value < m_Maximum)
            {
                ++m_Value;

                // Add the callback (if the user requested it)
                if (ValueChangedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.ValueChanged;
                    m_Callback.Value   = m_Value;
                    ValueChangedCallback (this, m_Callback);
                }

                // Check if the loading bar is now full
                if (m_Value == m_Maximum)
                {

                    // Add the callback (if the user requested it)
                    if (LoadingBarFullCallback != null)
                    {
                        m_Callback.Trigger = CallbackTrigger.LoadingBarFull;
                        m_Callback.Value   = m_Value;
                        LoadingBarFullCallback (this, m_Callback);
                    }
                }

                // Recalculate the size of the front image (the size of the part that will be drawn)
                RecalculateSize();
            }

            // return the new value
            return m_Value;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string Text
        {
            get
            {
                return m_Text.DisplayedString;
            }
            set
            {
                m_Text.DisplayedString = value;

                // Check if the text is auto sized
                if (m_TextSize == 0)
                {
                    // Calculate a possible text size
                    float size = m_Size.Y * 0.85f;
                    m_Text.CharacterSize = (uint)size;
                    m_Text.CharacterSize = (uint)(m_Text.CharacterSize - m_Text.GetLocalBounds().Top);

                    // Make sure that the text isn't too width
                    if (m_Text.GetGlobalBounds().Width > (m_Size.X * 0.8f))
                    {
                        // The text is too width, so make it smaller
                        m_Text.CharacterSize = (uint)(size / (m_Text.GetGlobalBounds().Width / (m_Size.X * 0.8f)));
                        m_Text.CharacterSize = (uint)(m_Text.CharacterSize - m_Text.GetLocalBounds().Top);
                    }
                }
                else // When the text has a fixed size then just set it
                    m_Text.CharacterSize = m_TextSize;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the font of the text.
        ///
        /// When you don't call this function then the global font will be use.
        /// This global font can be changed with the setGlobalFont function from the parent.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Font TextFont
        {
            get
            {
                return m_Text.Font;
            }
            set
            {
                m_Text.Font = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the color of the text.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color TextColor
        {
            get
            {
                return m_Text.Color;
            }
            set
            {
                m_Text.Color = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint TextSize
        {
            get
            {
                return m_Text.CharacterSize;
            }
            set
            {
                m_Text.CharacterSize = value;

                // Reposition the text
                Text = m_Text.DisplayedString;
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

                m_TextureBack_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureBack_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureBack_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureFront_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureFront_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureFront_R.sprite.Color = new Color(255, 255, 255, m_Opacity);

                m_Text.Color = new Color(m_Text.Color.R, m_Text.Color.G, m_Text.Color.B, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void RecalculateSize ()
        {
            // Check if the image is split
            if (m_SplitImage)
            {
                // Get the bounds of the sprites
                IntRect bounds_L = m_TextureFront_L.sprite.TextureRect;
                IntRect bounds_M = m_TextureFront_M.sprite.TextureRect;
                IntRect bounds_R = m_TextureFront_R.sprite.TextureRect;

                bounds_L.Width = m_TextureBack_L.sprite.TextureRect.Width;
                bounds_M.Width = m_TextureBack_M.sprite.TextureRect.Width;
                bounds_R.Width = m_TextureBack_R.sprite.TextureRect.Width;

                // Calculate the necessary sizes
                float totalWidth;
                float middleTextureWidth;
                float frontSize;

                // Check if the middle image is drawn
                if (m_TextureBack_L.Size.X + m_TextureBack_R.Size.X < m_Size.X)
                {
                    totalWidth = bounds_L.Width + bounds_M.Width + bounds_R.Width;
                    middleTextureWidth = totalWidth - (m_TextureBack_L.Size.X + m_TextureBack_R.Size.X);
                }
                else // The loading bar is too small
                {
                    totalWidth = bounds_L.Width + bounds_R.Width;
                    middleTextureWidth = 0;
                }

                // Only change the width when not dividing by zero
                if ((m_Maximum - m_Minimum) > 0)
                    frontSize = totalWidth * ((m_Value - m_Minimum) / (float)(m_Maximum - m_Minimum));
                else
                    frontSize = totalWidth;

                // Check if a part of the left piece should be visible
                if (frontSize > 0)
                {
                    // Check if a piece of the middle part should be drawn
                    if (frontSize > m_TextureBack_L.Size.X)
                    {
                        // Check if a piece of the right part should be drawn
                        if (frontSize > m_TextureBack_L.Size.X + middleTextureWidth)
                        {
                            // Check if the bar is not full
                            if (frontSize < totalWidth)
                                bounds_R.Width = (int)(frontSize - ((m_TextureBack_L.Size.X) + middleTextureWidth));
                        }
                        else // Only a part of the middle image should be drawn
                        {
                            bounds_M.Width = (int)((frontSize - (m_TextureBack_L.Size.X)) / middleTextureWidth * m_TextureBack_M.Size.X);
                            bounds_R.Width = 0;
                        }
                    }
                    else // Only a part of the left piece should be drawn
                    {
                        bounds_L.Width = (int)frontSize;
                        bounds_M.Width = 0;
                        bounds_R.Width = 0;
                    }
                }
                else // Nothing should be drawn
                {
                    bounds_L.Width = 0;
                    bounds_M.Width = 0;
                    bounds_R.Width = 0;
                }

                m_TextureFront_L.sprite.TextureRect = bounds_L;
                m_TextureFront_M.sprite.TextureRect = bounds_M;
                m_TextureFront_R.sprite.TextureRect = bounds_R;
            }
            else // The image is not split
            {
                // Calculate the size of the front sprite
                IntRect frontBounds = m_TextureFront_M.sprite.TextureRect;

                // Only change the width when not dividing by zero
                if ((m_Maximum - m_Minimum) > 0)
                    frontBounds.Width = (int)(m_TextureBack_M.Size.X * ((m_Value - m_Minimum) / (float)(m_Maximum - m_Minimum)));
                else
                    frontBounds.Width = (int)(m_TextureBack_M.Size.X);

                // Set the size of the front image
                m_TextureFront_M.sprite.TextureRect = frontBounds;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // This function is called when the widget is added to a container.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void Initialize(Container parent)
        {
            base.Initialize(parent);
            m_Text.Font = parent.GlobalFont;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // Draws the widget on the render target.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Apply the transformation
            states.Transform *= Transform;

            // Remember the current transformation
            Transform oldTransform = states.Transform;

            // Check if the image is split
            if (m_SplitImage)
            {
                // Get the scale the images
                float scalingY = m_Size.Y / m_TextureBack_M.Size.Y;

                // Scale the image
                states.Transform.Scale(scalingY, scalingY);

                // Draw the left image of the loading bar
                target.Draw(m_TextureBack_L.sprite, states);
                target.Draw(m_TextureFront_L.sprite, states);

                // Check if the middle image may be drawn
                if ((scalingY * (m_TextureBack_L.Size.X + m_TextureBack_R.Size.X)) < m_Size.X)
                {
                    // Put the middle image on the correct position
                    states.Transform.Translate(m_TextureBack_L.sprite.GetGlobalBounds().Width, 0);

                    // Calculate the scale for our middle image
                    float scaleX = (m_Size.X - ((m_TextureBack_L.Size.X + m_TextureBack_R.Size.X) * scalingY)) / m_TextureBack_M.Size.X;

                    // Set the scale for the middle image
                    states.Transform.Scale(scaleX / scalingY, 1);

                    // Draw the middle image
                    target.Draw(m_TextureBack_M.sprite, states);
                    target.Draw(m_TextureFront_M.sprite, states);

                    // Put the right image on the correct position
                    states.Transform.Translate(m_TextureBack_M.sprite.GetGlobalBounds().Width, 0);

                    // Set the scale for the right image
                    states.Transform.Scale(scalingY / scaleX, 1);

                    // Draw the right image
                    target.Draw(m_TextureBack_R.sprite, states);
                    target.Draw(m_TextureFront_R.sprite, states);
                }
                else // The loading bar isn't width enough, we will draw it at minimum size
                {
                    // Put the right image on the correct position
                    states.Transform.Translate(m_TextureBack_L.sprite.GetGlobalBounds().Width, 0);

                    // Draw the right image
                    target.Draw(m_TextureBack_R.sprite, states);
                    target.Draw(m_TextureFront_R.sprite, states);
                }
            }
            else // The image is not split
            {
                // Scale the image
                states.Transform.Scale(m_Size.X / m_TextureBack_M.Size.X, m_Size.Y / m_TextureBack_M.Size.Y);

                // Draw the loading bar
                target.Draw(m_TextureBack_M.sprite, states);
                target.Draw(m_TextureFront_M.sprite, states);
            }

            // Check if there is a text to draw
            if (m_Text.DisplayedString.Length != 0)
            {
                // Reset the transformations
                states.Transform = oldTransform;

                // Get the current size of the text, so that we can recalculate the position
                FloatRect rect = m_Text.GetGlobalBounds();

                // Calculate the new position for the text
                rect.Left = (m_Size.X - rect.Width) * 0.5f - rect.Left;
                rect.Top = (m_Size.Y - rect.Height) * 0.5f - rect.Top;

                // Set the new position
                states.Transform.Translate((float)System.Math.Floor(rect.Left + 0.5), (float)System.Math.Floor(rect.Top + 0.5));

                // Draw the text
                target.Draw(m_Text, states);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

