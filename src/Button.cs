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
    public class Button : ClickableWidget
    {
        public event EventHandler<CallbackArgs> SpaceKeyPressedCallback;
        public event EventHandler<CallbackArgs> ReturnKeyPressedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private string  m_LoadedConfigFile = "";

        private Impl.Sprite  m_TextureNormal_L = new Impl.Sprite();
        private Impl.Sprite  m_TextureHover_L = new Impl.Sprite();
        private Impl.Sprite  m_TextureDown_L = new Impl.Sprite();
        private Impl.Sprite  m_TextureFocused_L = new Impl.Sprite();

        internal Impl.Sprite m_TextureNormal_M = new Impl.Sprite();
        internal Impl.Sprite m_TextureHover_M = new Impl.Sprite();
        internal Impl.Sprite m_TextureDown_M = new Impl.Sprite();
        private  Impl.Sprite m_TextureFocused_M = new Impl.Sprite();

        private Impl.Sprite  m_TextureNormal_R = new Impl.Sprite();
        private Impl.Sprite  m_TextureHover_R = new Impl.Sprite();
        private Impl.Sprite  m_TextureDown_R = new Impl.Sprite();
        private Impl.Sprite  m_TextureFocused_R = new Impl.Sprite();

        // If this is true then the L, M and R images will be used.
        // If it is false then the button is just one big image that will be stored in the M image.
        private bool m_SplitImage = false;

        // Is there a separate hover image, or is it a semi-transparent image that is drawn on top of the others?
        internal bool m_SeparateHoverImage = false;

        // The SFML text
        private Text m_Text = new Text();

        // This will store the size of the text ( 0 to auto size )
        private uint m_TextSize = 0;
       

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Button ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Button (Button copy) : base(copy)
        {
            SpaceKeyPressedCallback = copy.SpaceKeyPressedCallback;
            ReturnKeyPressedCallback = copy.ReturnKeyPressedCallback;

            m_LoadedConfigFile   = copy.m_LoadedConfigFile;
            m_SplitImage         = copy.m_SplitImage;
            m_SeparateHoverImage = copy.m_SeparateHoverImage;
            m_Text               = new Text(copy.m_Text);
            m_TextSize           = copy.m_TextSize;
            
            Global.TextureManager.CopyTexture(copy.m_TextureNormal_L, m_TextureNormal_L);
            Global.TextureManager.CopyTexture(copy.m_TextureNormal_M, m_TextureNormal_M);
            Global.TextureManager.CopyTexture(copy.m_TextureNormal_R, m_TextureNormal_R);
            Global.TextureManager.CopyTexture(copy.m_TextureHover_L, m_TextureHover_L);
            Global.TextureManager.CopyTexture(copy.m_TextureHover_M, m_TextureHover_M);
            Global.TextureManager.CopyTexture(copy.m_TextureHover_R, m_TextureHover_R);
            Global.TextureManager.CopyTexture(copy.m_TextureDown_L, m_TextureDown_L);
            Global.TextureManager.CopyTexture(copy.m_TextureDown_M, m_TextureDown_M);
            Global.TextureManager.CopyTexture(copy.m_TextureDown_R, m_TextureDown_R);
            Global.TextureManager.CopyTexture(copy.m_TextureFocused_L, m_TextureFocused_L);
            Global.TextureManager.CopyTexture(copy.m_TextureFocused_M, m_TextureFocused_M);
            Global.TextureManager.CopyTexture(copy.m_TextureFocused_R, m_TextureFocused_R);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a Button section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Button (string configFileFilename)
        {
            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "Button");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "separatehoverimage")
                    m_SeparateHoverImage = configFile.ReadBool(i);
                else if (configFile.Properties[i] == "textcolor")
                    m_Text.Color = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "normalimage")
                {
                    configFile.ReadTexture (i, configFileFolder, m_TextureNormal_M);
                    m_SplitImage = false;
                }
                else if (configFile.Properties[i] == "hoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureHover_M);
                else if (configFile.Properties[i] == "downimage")
                    configFile.ReadTexture (i, configFileFolder, m_TextureDown_M);
                else if (configFile.Properties[i] == "focusedimage")
                    configFile.ReadTexture (i, configFileFolder, m_TextureFocused_M);
                else if (configFile.Properties[i] == "normalimage_l")
                    configFile.ReadTexture (i, configFileFolder, m_TextureNormal_L);
                else if (configFile.Properties[i] == "normalimage_m")
                {
                    configFile.ReadTexture(i, configFileFolder, m_TextureNormal_M);
                    m_SplitImage = true;
                }
                else if (configFile.Properties[i] == "normalimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureNormal_R);
                else if (configFile.Properties[i] == "hoverimage_l")
                    configFile.ReadTexture(i, configFileFolder, m_TextureHover_L);
                else if (configFile.Properties[i] == "hoverimage_m")
                    configFile.ReadTexture (i, configFileFolder, m_TextureHover_M);
                else if (configFile.Properties[i] == "hoverimage_r")
                    configFile.ReadTexture (i, configFileFolder, m_TextureHover_R);
                else if (configFile.Properties[i] == "downimage_l")
                    configFile.ReadTexture(i, configFileFolder, m_TextureDown_L);
                else if (configFile.Properties[i] == "downimage_m")
                    configFile.ReadTexture(i, configFileFolder, m_TextureDown_M);
                else if (configFile.Properties[i] == "downimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureDown_R);
                else if (configFile.Properties[i] == "focusedimage_l")
                    configFile.ReadTexture (i, configFileFolder, m_TextureFocused_L);
                else if (configFile.Properties[i] == "focusedimage_m")
                    configFile.ReadTexture(i, configFileFolder, m_TextureFocused_M);
                else if (configFile.Properties[i] == "focusedimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureFocused_R);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section Button in " + configFileFilename + ".");
            }

            // Check if the image is split
            if (m_SplitImage)
            {
                // Make sure the required textures were loaded
                if ((m_TextureNormal_L.texture != null) && (m_TextureNormal_M.texture != null) && (m_TextureNormal_R.texture != null))
                {
                    Size = new Vector2f(m_TextureNormal_L.Size.X + m_TextureNormal_M.Size.X + m_TextureNormal_R.Size.X,
                                        m_TextureNormal_M.Size.Y);

                    m_TextureNormal_M.texture.texture.Repeated = true;
                }
                else
                {
                    throw new Exception("Not all needed images were loaded for the button. Is the Button section in "
                                        + configFileFilename + " complete?");
                }

                // Check if optional textures were loaded
                if ((m_TextureFocused_L.texture != null) && (m_TextureFocused_M.texture != null) && (m_TextureFocused_R.texture != null))
                {
                    m_AllowFocus = true;
                    m_WidgetPhase |= (byte)WidgetPhase.Focused;

                    m_TextureFocused_M.texture.texture.Repeated = true;
                }
                if ((m_TextureHover_L.texture != null) && (m_TextureHover_M.texture != null) && (m_TextureHover_R.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;

                    m_TextureHover_M.texture.texture.Repeated = true;
                }
                if ((m_TextureDown_L.texture != null) && (m_TextureDown_M.texture != null) && (m_TextureDown_R.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.MouseDown;

                    m_TextureDown_M.texture.texture.Repeated = true;
                }
            }
            else // The image isn't split
            {
                // Make sure the required texture was loaded
                if (m_TextureNormal_M.texture != null)
                {
                    Size = new Vector2f(m_TextureNormal_M.Size.X, m_TextureNormal_M.Size.Y);
                }
                else
                    throw new Exception("NormalImage property wasn't loaded. Is the Button section in " + configFileFilename + " complete?");

                // Check if optional textures were loaded
                if (m_TextureFocused_M.texture != null)
                {
                    m_AllowFocus = true;
                    m_WidgetPhase |= (byte)WidgetPhase.Focused;
                }
                if (m_TextureHover_M.texture != null)
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;
                }
                if (m_TextureDown_M.texture != null)
                {
                    m_WidgetPhase |= (byte)WidgetPhase.MouseDown;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Button ()
        {
            if (m_TextureNormal_L.texture != null)   Global.TextureManager.RemoveTexture(m_TextureNormal_L);
            if (m_TextureNormal_M.texture != null)   Global.TextureManager.RemoveTexture(m_TextureNormal_M);
            if (m_TextureNormal_R.texture != null)   Global.TextureManager.RemoveTexture(m_TextureNormal_R);

            if (m_TextureHover_L.texture != null)    Global.TextureManager.RemoveTexture(m_TextureHover_L);
            if (m_TextureHover_M.texture != null)    Global.TextureManager.RemoveTexture(m_TextureHover_M);
            if (m_TextureHover_R.texture != null)    Global.TextureManager.RemoveTexture(m_TextureHover_R);

            if (m_TextureDown_L.texture != null)     Global.TextureManager.RemoveTexture(m_TextureDown_L);
            if (m_TextureDown_M.texture != null)     Global.TextureManager.RemoveTexture(m_TextureDown_M);
            if (m_TextureDown_R.texture != null)     Global.TextureManager.RemoveTexture(m_TextureDown_R);

            if (m_TextureFocused_L.texture != null)  Global.TextureManager.RemoveTexture(m_TextureFocused_L);
            if (m_TextureFocused_M.texture != null)  Global.TextureManager.RemoveTexture(m_TextureFocused_M);
            if (m_TextureFocused_R.texture != null)  Global.TextureManager.RemoveTexture(m_TextureFocused_R);
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
        /// \brief Changes/Returns the position of the widget
        ///
        /// The default position of a transformable widget is (0, 0).
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
                    m_TextureDown_L.sprite.Position = value;
                    m_TextureHover_L.sprite.Position = value;
                    m_TextureNormal_L.sprite.Position = value;
                    m_TextureFocused_L.sprite.Position = value;

                    // Check if the middle image may be drawn
                    if ((m_TextureNormal_M.sprite.Scale.Y * (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X)) < m_Size.X)
                    {
                        float scalingY = m_Size.Y / m_TextureNormal_M.Size.Y;

                        m_TextureDown_M.sprite.Position = new Vector2f(value.X + (m_TextureDown_L.Size.X * m_TextureDown_L.sprite.Scale.X), value.Y);
                        m_TextureHover_M.sprite.Position = new Vector2f(value.X + (m_TextureHover_L.Size.X * m_TextureHover_L.sprite.Scale.X), value.Y);
                        m_TextureNormal_M.sprite.Position = new Vector2f(value.X + (m_TextureNormal_L.Size.X * m_TextureNormal_L.sprite.Scale.X), value.Y);
                        m_TextureFocused_M.sprite.Position = new Vector2f(value.X + (m_TextureFocused_L.Size.X * m_TextureFocused_L.sprite.Scale.X), value.Y);

                        m_TextureDown_R.sprite.Position = new Vector2f(m_TextureDown_M.sprite.Position.X + (m_TextureDown_M.sprite.TextureRect.Width * scalingY), value.Y);
                        m_TextureHover_R.sprite.Position = new Vector2f(m_TextureHover_M.sprite.Position.X + (m_TextureHover_M.sprite.TextureRect.Width * scalingY), value.Y);
                        m_TextureNormal_R.sprite.Position = new Vector2f(m_TextureNormal_M.sprite.Position.X + (m_TextureNormal_M.sprite.TextureRect.Width * scalingY), value.Y);
                        m_TextureFocused_R.sprite.Position = new Vector2f(m_TextureFocused_M.sprite.Position.X + (m_TextureFocused_M.sprite.TextureRect.Width * scalingY), value.Y);
                    }
                    else // The middle image isn't drawn
                    {
                        m_TextureDown_R.sprite.Position = new Vector2f(value.X + (m_TextureDown_L.Size.X * m_TextureDown_L.sprite.Scale.X), value.Y);
                        m_TextureHover_R.sprite.Position = new Vector2f(value.X + (m_TextureHover_L.Size.X * m_TextureHover_L.sprite.Scale.X), value.Y);
                        m_TextureNormal_R.sprite.Position = new Vector2f(value.X + (m_TextureNormal_L.Size.X * m_TextureNormal_L.sprite.Scale.X), value.Y);
                        m_TextureFocused_R.sprite.Position = new Vector2f(value.X + (m_TextureFocused_L.Size.X * m_TextureFocused_L.sprite.Scale.X), value.Y);
                    }
                }
                else // The images aren't split
                {
                    m_TextureDown_M.sprite.Position = value;
                    m_TextureHover_M.sprite.Position = value;
                    m_TextureNormal_M.sprite.Position = value;
                    m_TextureFocused_M.sprite.Position = value;
                }

                m_Text.Position = new Vector2f ((float)Math.Round(value.X + (m_Size.X - m_Text.GetLocalBounds().Width) / 2.0 -  m_Text.GetLocalBounds().Left),
                                                (float)Math.Round(value.Y + (m_Size.Y - m_Text.GetLocalBounds ().Height) / 2.0 - m_Text.GetLocalBounds ().Top));
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the size of the button.
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

                // A negative size is not allowed for this widget
                if (m_Size.X < 0) m_Size.X = -m_Size.X;
                if (m_Size.Y < 0) m_Size.Y = -m_Size.Y;
            
                // Recalculate the text size when auto sizing
                if (m_TextSize == 0)
                    Text = Text;

                // Drawing the button image will be different when the image is split
                if (m_SplitImage)
                {
                    float scalingY = m_Size.Y / m_TextureNormal_M.Size.Y;
                    float minimumWidth = (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X) * scalingY;

                    if (m_Size.X < minimumWidth)
                        m_Size.X = minimumWidth;

                    m_TextureDown_L.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureHover_L.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureNormal_L.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureFocused_L.sprite.Scale = new Vector2f(scalingY, scalingY);

                    m_TextureDown_M.sprite.TextureRect = new IntRect(0, 0, (int)((m_Size.X - minimumWidth) / scalingY), (int)m_TextureDown_M.Size.Y);
                    m_TextureHover_M.sprite.TextureRect = new IntRect(0, 0, (int)((m_Size.X - minimumWidth) / scalingY), (int)m_TextureHover_M.Size.Y);
                    m_TextureNormal_M.sprite.TextureRect = new IntRect(0, 0, (int)((m_Size.X - minimumWidth) / scalingY), (int)m_TextureNormal_M.Size.Y);
                    m_TextureFocused_M.sprite.TextureRect = new IntRect(0, 0, (int)((m_Size.X - minimumWidth) / scalingY), (int)m_TextureFocused_M.Size.Y);

                    m_TextureDown_M.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureHover_M.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureNormal_M.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureFocused_M.sprite.Scale = new Vector2f(scalingY, scalingY);

                    m_TextureDown_R.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureHover_R.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureNormal_R.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureFocused_R.sprite.Scale = new Vector2f(scalingY, scalingY);
                }
                else // The image is not split
                {
                    m_TextureDown_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureDown_M.Size.X, m_Size.Y / m_TextureDown_M.Size.Y);
                    m_TextureHover_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureHover_M.Size.X, m_Size.Y / m_TextureHover_M.Size.Y);
                    m_TextureNormal_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureNormal_M.Size.X, m_Size.Y / m_TextureNormal_M.Size.Y);
                    m_TextureFocused_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureFocused_M.Size.X, m_Size.Y / m_TextureFocused_M.Size.Y);
                }

                // Recalculate the position of the images
                Position = Position;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the caption of the button.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string Text
        {
            get
            {
                return m_Text.DisplayedString;
            }
            set
            {
                // Set the new text
                m_Text.DisplayedString = value;
                m_Callback.Text = value;

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
                        m_Text.CharacterSize = (uint)(size * m_Size.X * 0.8f / m_Text.GetGlobalBounds().Width);
                        m_Text.CharacterSize = (uint)(m_Text.CharacterSize - m_Text.GetLocalBounds().Top);
                    }
                }
                else // When the text has a fixed size
                {
                    // Set the text size
                    m_Text.CharacterSize = m_TextSize;
                }

                // Set the position of the text
                m_Text.Position = new Vector2f((float)Math.Round(Position.X + (m_Size.X - m_Text.GetLocalBounds().Width) / 2.0 -  m_Text.GetLocalBounds().Left),
                                               (float)Math.Round(Position.Y + (m_Size.Y - m_Text.GetLocalBounds().Height) / 2.0 -  m_Text.GetLocalBounds().Top));
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
        /// \brief Changes/Returns the character size of the text.
        ///
        /// If the size is set to 0 then the text will be auto-sized to fit inside the button.
        ///
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
                Text = Text;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the transparency of the widget.
        ///
        /// \param transparency  The transparency of the widget.
        ///                      0 is completely transparent, while 255 (default) means fully opaque.
        ///
        /// Note that this will only change the transparency of the images. The parts of the widgets that use a color will not
        /// be changed. You must change them yourself by setting the alpha channel of the color.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override byte Transparency
        {
            set
            {
                base.Transparency = value;

                if (m_SplitImage)
                {
                    m_TextureNormal_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureHover_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureDown_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureFocused_L.sprite.Color = new Color(255, 255, 255, m_Opacity);

                    m_TextureNormal_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureHover_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureDown_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureFocused_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                }

                m_TextureNormal_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureHover_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureDown_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureFocused_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnKeyPressed(KeyEventArgs e)
        {
            // Check if the space key or the return key was pressed
            if (e.Code == Keyboard.Key.Space)
            {
                if (SpaceKeyPressedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.SpaceKeyPressed;
                    SpaceKeyPressedCallback (this, m_Callback);
                }
            }
            else if (e.Code == Keyboard.Key.Return)
            {
                if (ReturnKeyPressedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.ReturnKeyPressed;
                    ReturnKeyPressedCallback (this, m_Callback);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnWidgetFocused()
        {
            // We can't be focused when we don't have a focus image
            if ((m_WidgetPhase & (byte)WidgetPhase.Focused) == 0)
                Focused = false;
            else
                base.OnWidgetFocused ();
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
            if (m_SplitImage)
            {
                if (m_SeparateHoverImage)
                {
                    if (m_MouseDown && m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.MouseDown) != 0)
                    {
                        target.Draw(m_TextureDown_L.sprite, states);
                        target.Draw(m_TextureDown_M.sprite, states);
                        target.Draw(m_TextureDown_R.sprite, states);
                    }
                    else if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        target.Draw(m_TextureHover_L.sprite, states);
                        target.Draw(m_TextureHover_M.sprite, states);
                        target.Draw(m_TextureHover_R.sprite, states);
                    }
                    else
                    {
                        target.Draw(m_TextureNormal_L.sprite, states);
                        target.Draw(m_TextureNormal_M.sprite, states);
                        target.Draw(m_TextureNormal_R.sprite, states);
                    }
                }
                else // The hover image is drawn on top of the normal one
                {
                    if (m_MouseDown && m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.MouseDown) != 0)
                    {
                        target.Draw(m_TextureDown_L.sprite, states);
                        target.Draw(m_TextureDown_M.sprite, states);
                        target.Draw(m_TextureDown_R.sprite, states);
                    }
                    else
                    {
                        target.Draw(m_TextureNormal_L.sprite, states);
                        target.Draw(m_TextureNormal_M.sprite, states);
                        target.Draw(m_TextureNormal_R.sprite, states);
                    }

                    // When the mouse is on top of the button then draw an extra image
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        target.Draw(m_TextureHover_L.sprite, states);
                        target.Draw(m_TextureHover_M.sprite, states);
                        target.Draw(m_TextureHover_R.sprite, states);
                    }
                }

                // When the button is focused then draw an extra image
                if (m_Focused && (m_WidgetPhase & (byte)WidgetPhase.Focused) != 0)
                {
                    target.Draw(m_TextureFocused_L.sprite, states);
                    target.Draw(m_TextureFocused_M.sprite, states);
                    target.Draw(m_TextureFocused_R.sprite, states);
                }
            }
            else // The images aren't split
            {
                if (m_SeparateHoverImage)
                {
                    if (m_MouseDown && m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.MouseDown) != 0)
                    {
                        target.Draw(m_TextureDown_M.sprite, states);
                    }
                    else if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        target.Draw(m_TextureHover_M.sprite, states);
                    }
                    else
                    {
                        target.Draw(m_TextureNormal_M.sprite, states);
                    }
                }
                else // The hover image is drawn on top of the normal one
                {
                    if (m_MouseDown && m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.MouseDown) != 0)
                    {
                        target.Draw(m_TextureDown_M.sprite, states);
                    }
                    else
                    {
                        target.Draw(m_TextureNormal_M.sprite, states);
                    }

                    // When the mouse is on top of the button then draw an extra image
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        target.Draw(m_TextureHover_M.sprite, states);
                    }
                }

                // When the button is focused then draw an extra image
                if (m_Focused && (m_WidgetPhase & (byte)WidgetPhase.Focused) != 0)
                {
                    target.Draw(m_TextureFocused_M.sprite, states);
                }
            }

            // If the button has a text then also draw the text
            target.Draw(m_Text, states);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

