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
using Tao.OpenGl;

namespace TGUI
{
    public class EditBox : ClickableWidget, WidgetBorders
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// The text alignment
        enum Alignment
        {
            /// Put the text on the left side (default)
            Left,

            /// Center the text
            Center,

            /// Put the text on the right side (e.g. for numbers)
            Right
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public event EventHandler<CallbackArgs> TextChangedCallback;
        public event EventHandler<CallbackArgs> ReturnKeyPressedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private string         m_LoadedConfigFile = "";

        // Is the selection point visible or not?
        private bool           m_SelectionPointVisible = true;

        // When this boolean is true then you can no longer add text when the EditBox is full.
        // Changing it to false will allow you to scroll the text (default).
        // You can change the boolean with the limitTextWidth(bool) function.
        private bool           m_LimitTextWidth = false;

        // The text inside the edit box
        private string         m_DisplayedText = "";
        private string         m_Text = "";

        // This will store the size of the text ( 0 to auto size )
        private uint           m_TextSize = 0;

        // The text alignment
        private Alignment      m_TextAlignment = Alignment.Left;

        // The selection
        private uint           m_SelChars = 0;
        private uint           m_SelStart = 0;
        private uint           m_SelEnd = 0;

        // The password character
        private string         m_PasswordChar = "";

        // The maximum allowed characters.
        // Zero by default, meaning no limit.
        private uint           m_MaxChars = 0;

        // If this is true then the L, M and R images will be used.
        // If it is false then the button is just one big image that will be stored in the M image.
        private bool           m_SplitImage = false;

        // When the text width is not limited, you can scroll the edit box and only a part will be visible.
        private uint           m_TextCropPosition = 0;

        // The rectangle behind the selected text
        private RectangleShape m_SelectedTextBackground = new RectangleShape();

        // The flickering selection point
        private RectangleShape m_SelectionPoint = new RectangleShape(new Vector2f(1, 0));

        // We need three SFML texts to draw our text, and one more for calculations.
        private Text           m_TextBeforeSelection = new Text();
        private Text           m_TextSelection = new Text();
        private Text           m_TextAfterSelection = new Text();
        private Text           m_TextFull = new Text();

        private Impl.Sprite m_TextureNormal_L = new Impl.Sprite();
        private Impl.Sprite m_TextureHover_L = new Impl.Sprite();
        private Impl.Sprite m_TextureFocused_L = new Impl.Sprite();

        private Impl.Sprite m_TextureNormal_M = new Impl.Sprite();
        private Impl.Sprite m_TextureHover_M = new Impl.Sprite();
        private Impl.Sprite m_TextureFocused_M = new Impl.Sprite();

        private Impl.Sprite m_TextureNormal_R = new Impl.Sprite();
        private Impl.Sprite m_TextureHover_R = new Impl.Sprite();
        private Impl.Sprite m_TextureFocused_R = new Impl.Sprite();

        // Is there a possibility that the user is going to double click?
        private bool           m_PossibleDoubleClick = false;

        private bool           m_NumbersOnly = false;

        // Is there a separate hover image, or is it a semi-transparent image that is drawn on top of the others?
        private bool           m_SeparateHoverImage = false;

        private Borders        m_Borders = new Borders();


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal EditBox ()
        {
            m_AnimatedWidget = true;
            m_DraggableWidget = true;
            m_AllowFocus = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public EditBox (EditBox copy) : base(copy)
        {
            TextChangedCallback = copy.TextChangedCallback;
            ReturnKeyPressedCallback = copy.ReturnKeyPressedCallback;

            m_LoadedConfigFile       = copy.m_LoadedConfigFile;
            m_SelectionPointVisible  = copy.m_SelectionPointVisible;
            m_LimitTextWidth         = copy.m_LimitTextWidth;
            m_DisplayedText          = copy.m_DisplayedText;
            m_Text                   = copy.m_Text;
            m_TextSize               = copy.m_TextSize;
            m_TextAlignment          = copy.m_TextAlignment;
            m_SelChars               = copy.m_SelChars;
            m_SelStart               = copy.m_SelStart;
            m_SelEnd                 = copy.m_SelEnd;
            m_PasswordChar           = copy.m_PasswordChar;
            m_MaxChars               = copy.m_MaxChars;
            m_SplitImage             = copy.m_SplitImage;
            m_TextCropPosition       = copy.m_TextCropPosition;
            m_SelectedTextBackground = copy.m_SelectedTextBackground;
            m_SelectionPoint         = copy.m_SelectionPoint;
            m_TextBeforeSelection    = new Text(copy.m_TextBeforeSelection);
            m_TextSelection          = new Text(copy.m_TextSelection);
            m_TextAfterSelection     = new Text(copy.m_TextAfterSelection);
            m_TextFull               = new Text(copy.m_TextFull);
            m_PossibleDoubleClick    = copy.m_PossibleDoubleClick;
            m_NumbersOnly            = copy.m_NumbersOnly;
            m_SeparateHoverImage     = copy.m_SeparateHoverImage;
            m_Borders                = copy.m_Borders;

            Global.TextureManager.CopyTexture(copy.m_TextureNormal_L, m_TextureNormal_L);
            Global.TextureManager.CopyTexture(copy.m_TextureNormal_M, m_TextureNormal_M);
            Global.TextureManager.CopyTexture(copy.m_TextureNormal_R, m_TextureNormal_R);
            Global.TextureManager.CopyTexture(copy.m_TextureHover_L, m_TextureHover_L);
            Global.TextureManager.CopyTexture(copy.m_TextureHover_M, m_TextureHover_M);
            Global.TextureManager.CopyTexture(copy.m_TextureHover_R, m_TextureHover_R);
            Global.TextureManager.CopyTexture(copy.m_TextureFocused_L, m_TextureFocused_L);
            Global.TextureManager.CopyTexture(copy.m_TextureFocused_M, m_TextureFocused_M);
            Global.TextureManager.CopyTexture(copy.m_TextureFocused_R, m_TextureFocused_R);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a EditBox section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public EditBox (string configFileFilename)
        {
            m_AnimatedWidget = true;
            m_DraggableWidget = true;
            m_AllowFocus = true;

            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "EditBox");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "separatehoverimage")
                    m_SeparateHoverImage = configFile.ReadBool(i);
                else if (configFile.Properties[i] == "textcolor")
                {
                    Color color = configFile.ReadColor(i);
                    m_TextBeforeSelection.Color = color;
                    m_TextAfterSelection.Color = color;
                }
                else if (configFile.Properties[i] == "selectedtextcolor")
                    m_TextSelection.Color = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "selectedtextbackgroundcolor")
                    m_SelectedTextBackground.FillColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "selectionpointcolor")
                    m_SelectionPoint.FillColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "selectionpointwidth")
                    SelectionPointWidth = Convert.ToUInt32(configFile.Values [i]);
                else if (configFile.Properties[i] == "borders")
                {
                    Borders borders;
                    if (Internal.ExtractBorders(configFile.Values [i], out borders))
                        Borders = borders;
                }
                else if (configFile.Properties[i] == "normalimage")
                {
                    configFile.ReadTexture (i, configFileFolder, m_TextureNormal_M);
                    m_SplitImage = false;
                }
                else if (configFile.Properties[i] == "hoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureHover_M);
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
                else if (configFile.Properties[i] == "focusedimage_l")
                    configFile.ReadTexture (i, configFileFolder, m_TextureFocused_L);
                else if (configFile.Properties[i] == "focusedimage_m")
                    configFile.ReadTexture(i, configFileFolder, m_TextureFocused_M);
                else if (configFile.Properties[i] == "focusedimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureFocused_R);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section EditBox in " + configFileFilename + ".");
            }

            // Check if the image is split
            if (m_SplitImage)
            {
                // Make sure the required textures were loaded
                if ((m_TextureNormal_L.texture != null) && (m_TextureNormal_M.texture != null) && (m_TextureNormal_R.texture != null))
                {
                    Size = new Vector2f(m_TextureNormal_L.Size.X + m_TextureNormal_M.Size.X + m_TextureNormal_R.Size.X,
                                        m_TextureNormal_M.Size.Y);
                }
                else
                {
                    throw new Exception("Not all needed images were loaded for the edit box. Is the EditBox section in "
                                        + configFileFilename + " complete?");
                }

                // Check if optional textures were loaded
                if ((m_TextureFocused_L.texture != null) && (m_TextureFocused_M.texture != null) && (m_TextureFocused_R.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Focused;
                }
                if ((m_TextureHover_L.texture != null) && (m_TextureHover_M.texture != null) && (m_TextureHover_R.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;
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
                    throw new Exception("NormalImage property wasn't loaded. Is the EditBox section in " + configFileFilename + " complete?");

                // Check if optional textures were loaded
                if (m_TextureFocused_M.texture != null)
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Focused;
                }
                if (m_TextureHover_M.texture != null)
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;
                }
            }

            // Auto-size the text
            TextSize = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~EditBox ()
        {
            if (m_TextureNormal_L.texture != null)   Global.TextureManager.RemoveTexture(m_TextureNormal_L);
            if (m_TextureNormal_M.texture != null)   Global.TextureManager.RemoveTexture(m_TextureNormal_M);
            if (m_TextureNormal_R.texture != null)   Global.TextureManager.RemoveTexture(m_TextureNormal_R);

            if (m_TextureHover_L.texture != null)    Global.TextureManager.RemoveTexture(m_TextureHover_L);
            if (m_TextureHover_M.texture != null)    Global.TextureManager.RemoveTexture(m_TextureHover_M);
            if (m_TextureHover_R.texture != null)    Global.TextureManager.RemoveTexture(m_TextureHover_R);

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
                    m_TextureHover_L.sprite.Position = value;
                    m_TextureNormal_L.sprite.Position = value;
                    m_TextureFocused_L.sprite.Position = value;

                    // Check if the middle image may be drawn
                    if ((m_TextureNormal_M.sprite.Scale.Y * (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X)) < m_Size.X)
                    {
                        m_TextureHover_M.sprite.Position = new Vector2f(value.X + (m_TextureHover_L.Size.X * m_TextureHover_L.sprite.Scale.X), value.Y);
                        m_TextureNormal_M.sprite.Position = new Vector2f(value.X + (m_TextureNormal_L.Size.X * m_TextureNormal_L.sprite.Scale.X), value.Y);
                        m_TextureFocused_M.sprite.Position = new Vector2f(value.X + (m_TextureFocused_L.Size.X * m_TextureFocused_L.sprite.Scale.X), value.Y);

                        m_TextureHover_R.sprite.Position = new Vector2f(m_TextureHover_M.sprite.Position.X + (m_TextureHover_M.Size.X * m_TextureHover_M.sprite.Scale.X), value.Y);
                        m_TextureNormal_R.sprite.Position = new Vector2f(m_TextureNormal_M.sprite.Position.X + (m_TextureNormal_M.Size.X * m_TextureNormal_M.sprite.Scale.X), value.Y);
                        m_TextureFocused_R.sprite.Position = new Vector2f(m_TextureFocused_M.sprite.Position.X + (m_TextureFocused_M.Size.X * m_TextureFocused_M.sprite.Scale.X), value.Y);
                    }
                    else // The middle image isn't drawn
                    {
                        m_TextureHover_R.sprite.Position = new Vector2f(value.X + (m_TextureHover_L.Size.X * m_TextureHover_L.sprite.Scale.X), value.Y);
                        m_TextureNormal_R.sprite.Position = new Vector2f(value.X + (m_TextureNormal_L.Size.X * m_TextureNormal_L.sprite.Scale.X), value.Y);
                        m_TextureFocused_R.sprite.Position = new Vector2f(value.X + (m_TextureFocused_L.Size.X * m_TextureFocused_L.sprite.Scale.X), value.Y);
                    }
                }
                else // The images aren't split
                {
                    m_TextureHover_M.sprite.Position = value;
                    m_TextureNormal_M.sprite.Position = value;
                    m_TextureFocused_M.sprite.Position = value;
                }

                RecalculateTextPositions();
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

                // When using splitimage, make sure that the width isn't too small
                if (m_SplitImage)
                {
                    if ((m_Size.Y / m_TextureNormal_M.Size.Y) * (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X) > m_Size.X)
                        m_Size.X = (m_Size.Y / m_TextureNormal_M.Size.Y) * (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X);
                }
            
                // Recalculate the text size when auto sizing
                if (m_TextSize == 0)
                    Text = Text;

                // Drawing the button image will be different when the image is split
                if (m_SplitImage)
                {
                    float scalingY = m_Size.Y / m_TextureNormal_M.Size.Y;

                    m_TextureHover_L.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureNormal_L.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureFocused_L.sprite.Scale = new Vector2f(scalingY, scalingY);

                    // Check if the middle image may be drawn
                    if (((scalingY) * (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X)) < m_Size.X)
                    {
                        // Calculate the scale for our middle image
                        float scaleX = (m_Size.X / m_TextureNormal_M.Size.X) -
                            (((m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X) * (scalingY))
                             / m_TextureNormal_M.Size.X);

                        m_TextureHover_M.sprite.Scale = new Vector2f(scaleX, scalingY);
                        m_TextureNormal_M.sprite.Scale = new Vector2f(scaleX, scalingY);
                        m_TextureFocused_M.sprite.Scale = new Vector2f(scaleX, scalingY);
                    }
                    else // The middle image is not drawn
                    {
                        m_TextureHover_M.sprite.Scale = new Vector2f(0, 0);
                        m_TextureNormal_M.sprite.Scale = new Vector2f(0, 0);
                        m_TextureFocused_M.sprite.Scale = new Vector2f(0, 0);
                    }

                    m_TextureHover_R.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureNormal_R.sprite.Scale = new Vector2f(scalingY, scalingY);
                    m_TextureFocused_R.sprite.Scale = new Vector2f(scalingY, scalingY);
                }
                else // The image is not split
                {
                    m_TextureHover_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureNormal_M.Size.X, m_Size.Y / m_TextureNormal_M.Size.Y);
                    m_TextureNormal_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureNormal_M.Size.X, m_Size.Y / m_TextureNormal_M.Size.Y);
                    m_TextureFocused_M.sprite.Scale = new Vector2f(m_Size.X / m_TextureNormal_M.Size.X, m_Size.Y / m_TextureNormal_M.Size.Y);
                }

                // Set the size of the selection point
                m_SelectionPoint.Size = new Vector2f(m_SelectionPoint.Size.X,
                                                     m_Size.Y - ((m_Borders.Bottom + m_Borders.Top) * (m_Size.Y / m_TextureNormal_M.Size.Y)));

                // Recalculate the position of the images
                Position = Position;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                // Check if the text is auto sized
                if (m_TextSize == 0)
                {
                    // Calculate the text size
                    m_TextFull.DisplayedString = "kg";
                    m_TextFull.CharacterSize = (uint)(m_Size.Y - ((m_Borders.Top + m_Borders.Bottom) * (m_Size.Y / m_TextureNormal_M.Size.Y)));
                    m_TextFull.CharacterSize = (uint)(m_TextFull.CharacterSize - m_TextFull.GetLocalBounds().Top);
                    m_TextFull.DisplayedString = m_DisplayedText;

                    // Also adjust the character size of the other texts
                    m_TextBeforeSelection.CharacterSize = m_TextFull.CharacterSize;
                    m_TextSelection.CharacterSize = m_TextFull.CharacterSize;
                    m_TextAfterSelection.CharacterSize = m_TextFull.CharacterSize;
                }
                else // When the text has a fixed size
                {
                    // Set the text size
                    m_TextBeforeSelection.CharacterSize = m_TextSize;
                    m_TextSelection.CharacterSize = m_TextSize;
                    m_TextAfterSelection.CharacterSize = m_TextSize;
                    m_TextFull.CharacterSize = m_TextSize;
                }

                // Change the text
                m_Text = value;
                m_DisplayedText = value;

                // If the edit box only accepts numbers then remove all other characters
                if (m_NumbersOnly)
                    SetNumbersOnly(true);

                // If there is a character limit then check if it is exeeded
                if ((m_MaxChars > 0) && (m_DisplayedText.Length > m_MaxChars))
                {
                    // Remove all the excess characters
                    m_Text = m_Text.Remove((int)m_MaxChars);
                    m_DisplayedText = m_DisplayedText.Remove((int)m_MaxChars);
                }

                // Check if there is a password character
                if (m_PasswordChar != "")
                {
                    // Change the displayed text to only contain the password character
                    m_DisplayedText = new string (m_PasswordChar[0], m_DisplayedText.Length);
                }

                // Set the texts
                m_TextBeforeSelection.DisplayedString = m_DisplayedText;
                m_TextSelection.DisplayedString = m_DisplayedText;
                m_TextAfterSelection.DisplayedString = m_DisplayedText;
                m_TextFull.DisplayedString = m_DisplayedText;

                // Calculate the space inside the edit box
                float width;
                if (m_SplitImage)
                    width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
                else
                    width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.X));

                // If the width is negative then the editBox is too small to be displayed
                if (width < 0)
                    width = 0;

                // Check if there is a text width limit
                if (m_LimitTextWidth)
                {
                    // Now check if the text fits into the EditBox
                    while (m_TextBeforeSelection.FindCharacterPos((uint)m_TextBeforeSelection.DisplayedString.Length).X > width)
                    {
                        // The text doesn't fit inside the EditBox, so the last character must be deleted.
                        m_Text = m_Text.Remove(m_Text.Length - 1);
                        m_DisplayedText = m_DisplayedText.Remove(m_DisplayedText.Length - 1);

                        // Set the new text
                        m_TextBeforeSelection.DisplayedString = m_DisplayedText;
                    }

                    // Set the full text again
                    m_TextFull.DisplayedString = m_DisplayedText;
                }
                else // There is no text cropping
                {
                    // Calculate the text width
                    float textWidth = m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X;

                    // If the text can be moved to the right then do so
                    if (textWidth > width)
                    {
                        if (textWidth - m_TextCropPosition < width)
                            m_TextCropPosition = (uint)(textWidth - width);
                    }
                    else
                        m_TextCropPosition = 0;
                }

                // Set the selection point behind the last character
                SetSelectionPointPosition((uint)m_DisplayedText.Length);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint TextSize
        {
            get
            {
                return m_TextFull.CharacterSize;
            }
            set
            {
                m_TextSize = value;

                // Reposition the text
                Text = Text;
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
                return m_TextFull.Font;
            }
            set
            {
                m_TextBeforeSelection.Font = value;
                m_TextSelection.Font = value;
                m_TextAfterSelection.Font = value;
                m_TextFull.Font = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string PasswordCharacter
        {
            get
            {
                return m_PasswordChar;
            }
            set
            {
                m_PasswordChar = value;

                // Reposition the text
                Text = Text;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint MaximumCharacters
        {
            get
            {
                return m_MaxChars;
            }
            set
            {
                m_MaxChars = value;

                // If there is a character limit then check if it is exeeded
                if ((m_MaxChars > 0) && (m_DisplayedText.Length > m_MaxChars))
                {
                    // Remove all the excess characters
                    m_Text = m_Text.Remove((int)m_MaxChars);
                    m_DisplayedText = m_DisplayedText.Remove((int)m_MaxChars);

                    // If we passed here then the text has changed.
                    m_TextBeforeSelection.DisplayedString = m_DisplayedText;
                    m_TextSelection.DisplayedString = "";
                    m_TextAfterSelection.DisplayedString = "";
                    m_TextFull.DisplayedString = m_DisplayedText;

                    // Set the selection point behind the last character
                    SetSelectionPointPosition((uint)m_DisplayedText.Length);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the border width and border height of the edit box.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Borders Borders
        {
            get
            {
                return m_Borders;
            }
            set
            {
                m_Borders = value;

                // Reposition the text
                Text = Text;

                // Set the size of the selection point
                m_SelectionPoint.Size = new Vector2f(m_SelectionPoint.Size.X,
                                                     m_Size.Y - ((m_Borders.Bottom + m_Borders.Top) * (m_Size.Y / m_TextureNormal_M.Size.Y)));
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
                return m_TextBeforeSelection.Color;
            }
            set
            {
                m_TextBeforeSelection.Color = value;
                m_TextAfterSelection.Color = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color SelectedTextColor
        {
            get
            {
                return m_TextSelection.Color;
            }
            set
            {
                m_TextSelection.Color = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color SelectedTextBackgroundColor
        {
            get
            {
                return m_SelectedTextBackground.FillColor;
            }
            set
            {
                m_SelectedTextBackground.FillColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color SelectionPointColor
        {
            get
            {
                return m_SelectionPoint.FillColor;
            }
            set
            {
                m_SelectionPoint.FillColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void LimitTextWidth(bool limitWidth)
        {
            m_LimitTextWidth = limitWidth;

            // Check if the width is being limited
            if (m_LimitTextWidth == true)
            {
                // Calculate the space inside the edit box
                float width;
                if (m_SplitImage)
                    width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
                else
                    width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.X));

                // If the width is negative then the editBox is too small to be displayed
                if (width < 0)
                    width = 0;

                // Now check if the text fits into the EditBox
                while (m_TextBeforeSelection.FindCharacterPos((uint)m_DisplayedText.Length).X > width)
                {
                    // The text doesn't fit inside the EditBox, so the last character must be deleted.
                    m_Text = m_Text.Remove(m_Text.Length - 1);
                    m_DisplayedText = m_DisplayedText.Remove(m_DisplayedText.Length - 1);
                    m_TextBeforeSelection.DisplayedString = m_DisplayedText;
                }

                // The full text might have changed
                m_TextFull.DisplayedString = m_DisplayedText;

                // There is no clipping
                m_TextCropPosition = 0;

                // If the selection point was behind the limit, then set it at the end
                if (m_SelEnd > m_DisplayedText.Length)
                    SetSelectionPointPosition(m_SelEnd);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SetSelectionPointPosition(uint charactersBeforeSelectionPoint)
        {
            // The selection point position has to stay inside the string
            if (charactersBeforeSelectionPoint > m_Text.Length)
                charactersBeforeSelectionPoint = (uint)m_Text.Length;

            // Set the selection point to the correct position
            m_SelChars = 0;
            m_SelStart = charactersBeforeSelectionPoint;
            m_SelEnd = charactersBeforeSelectionPoint;

            // Change our texts
            m_TextBeforeSelection.DisplayedString = m_DisplayedText;
            m_TextSelection.DisplayedString = "";
            m_TextAfterSelection.DisplayedString = "";
            m_TextFull.DisplayedString = m_DisplayedText;

            // Check if scrolling is enabled
            if (m_LimitTextWidth == false)
            {
                // Calculate the space inside the edit box
                float width;
                if (m_SplitImage)
                    width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
                else
                    width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.X));

                // If the width is negative then the editBox is too small to be displayed
                if (width < 0)
                    width = 0;

                // Find out the position of the selection point
                float selectionPointPosition = m_TextFull.FindCharacterPos(m_SelEnd).X;

                if (m_SelEnd == m_DisplayedText.Length)
                    selectionPointPosition += m_TextFull.CharacterSize / 10.0f;

                // If the selection point is too far on the right then adjust the cropping
                if (m_TextCropPosition + width < selectionPointPosition)
                    m_TextCropPosition = (uint)(selectionPointPosition - width);

                // If the selection point is too far on the left then adjust the cropping
                if (m_TextCropPosition > selectionPointPosition)
                    m_TextCropPosition = (uint)(selectionPointPosition);
            }

            RecalculateTextPositions();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint SelectionPointWidth
        {
            get
            {
                return (uint)m_SelectionPoint.Size.X;
            }
            set
            {
                m_SelectionPoint.Size = new Vector2f (value, m_SelectionPoint.Size.Y);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetNumbersOnly(bool numbersOnly)
        {
            m_NumbersOnly = numbersOnly;

            // Remove all letters from the edit box if needed
            if ((numbersOnly) && (m_Text.Length > 0))
            {
                string newText = "";
                bool commaFound = false;

                if ((m_Text[0] == '+') || (m_Text[0] == '-'))
                    newText += m_Text[0];

                for (int i = 0; i < m_Text.Length; ++i)
                {
                    if (!commaFound)
                    {
                        if ((m_Text[i] == ',') || (m_Text[i] == '.'))
                        {
                            newText += m_Text[i];
                            commaFound = true;
                        }
                    }

                    if ((m_Text[i] >= '0') && (m_Text[i] <= '9'))
                        newText += m_Text[i];
                }

                // When the text changed then reposition the text
                if (newText != m_Text)
                    Text = newText;
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

                if (m_SplitImage)
                {
                    m_TextureNormal_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureHover_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureFocused_L.sprite.Color = new Color(255, 255, 255, m_Opacity);

                    m_TextureNormal_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureHover_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                    m_TextureFocused_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                }

                m_TextureNormal_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureHover_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureFocused_M.sprite.Color = new Color(255, 255, 255, m_Opacity);

                m_SelectedTextBackground.FillColor = new Color(m_SelectedTextBackground.FillColor.R, m_SelectedTextBackground.FillColor.G, m_SelectedTextBackground.FillColor.B, m_Opacity);
                m_SelectionPoint.FillColor = new Color(m_SelectionPoint.FillColor.R, m_SelectionPoint.FillColor.G, m_SelectionPoint.FillColor.B, m_Opacity);

                m_TextBeforeSelection.Color = new Color(m_TextBeforeSelection.Color.R, m_TextBeforeSelection.Color.G, m_TextBeforeSelection.Color.B, m_Opacity);
                m_TextSelection.Color = new Color(m_TextSelection.Color.R, m_TextSelection.Color.G, m_TextSelection.Color.B, m_Opacity);
                m_TextAfterSelection.Color = new Color(m_TextAfterSelection.Color.R, m_TextAfterSelection.Color.G, m_TextAfterSelection.Color.B, m_Opacity);
                m_TextFull.Color = new Color(m_TextFull.Color.R, m_TextFull.Color.G, m_TextFull.Color.B, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed(MouseButtonEventArgs e)
        {
            // Calculate the space inside the edit box
            float width;
            if (m_SplitImage)
                width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
            else
                width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.X));

            // If the width is negative then the editBox is too small to be displayed
            if (width < 0)
                width = 0;

            // Find the selection point position
            float positionX;
            if (m_SplitImage)
                positionX = e.X - Position.X - (m_Borders.Left * (m_Size.Y / m_TextureNormal_M.Size.Y));
            else
                positionX = e.X - Position.X - (m_Borders.Left * (m_Size.X / m_TextureNormal_M.Size.X));

            uint selectionPointPosition = FindSelectionPointPosition(positionX);

            // When clicking on the left of the first character, move the pointer to the left
            if ((positionX < 0) && (selectionPointPosition > 0))
                --selectionPointPosition;

            // When clicking on the right of the right character, move the pointer to the right
            else if ((positionX > width) && (selectionPointPosition < m_DisplayedText.Length))
                ++selectionPointPosition;

            // Check if this is a double click
            if ((m_PossibleDoubleClick) && (m_SelChars == 0) && (selectionPointPosition == m_SelEnd))
            {
                // The next click is going to be a normal one again
                m_PossibleDoubleClick = false;

                // Set the selection point at the end of the text
                SetSelectionPointPosition((uint)m_DisplayedText.Length);

                // Select the whole text
                m_SelStart = 0;
                m_SelEnd = (uint)m_Text.Length;
                m_SelChars = (uint)m_Text.Length;

                // Change the texts
                m_TextBeforeSelection.DisplayedString = "";
                m_TextSelection.DisplayedString = m_DisplayedText;
                m_TextAfterSelection.DisplayedString = "";
            }
            else // No double clicking
            {
                // Set the new selection point
                SetSelectionPointPosition(selectionPointPosition);

                // If the next click comes soon enough then it will be a double click
                m_PossibleDoubleClick = true;
            }

            // Let the base class also do stuff
            base.OnLeftMousePressed (e);

            RecalculateTextPositions();

            // The selection point should be visible
            m_SelectionPointVisible = true;
            m_AnimationTimeElapsed = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved(MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget();

            // Set the mouse hover flag
            m_MouseHover = true;

            // The mouse has moved so a double click is no longer possible
            m_PossibleDoubleClick = false;

            // Check if the mouse is hold down (we are selecting multiple characters)
            if (m_MouseDown)
            {
                // Check if there is a text width limit
                if (m_LimitTextWidth)
                {
                    // Find out between which characters the mouse is standing
                    if (m_SplitImage)
                        m_SelEnd = FindSelectionPointPosition(e.X - Position.X - (m_Borders.Left * (m_Size.Y / m_TextureNormal_M.Size.Y)));
                    else
                        m_SelEnd = FindSelectionPointPosition(e.X - Position.X - (m_Borders.Left * (m_Size.X / m_TextureNormal_M.Size.X)));
                }
                else // Scrolling is enabled
                {
                    float scalingX;
                    if (m_SplitImage)
                        scalingX = m_Size.Y / m_TextureNormal_M.Size.Y;
                    else
                        scalingX = m_Size.X / m_TextureNormal_M.Size.X;

                    float width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * scalingX);

                    // If the width is negative then the edit box is too small to be displayed
                    if (width < 0)
                        width = 0;

                    // Check if the mouse is on the left of the text
                    if (e.X - Position.X < m_Borders.Left * scalingX)
                    {
                        // Move the text by a few pixels
                        if (m_TextFull.CharacterSize > 10)
                        {
                            if (m_TextCropPosition > m_TextFull.CharacterSize / 10)
                                m_TextCropPosition -= (uint)(System.Math.Floor(m_TextFull.CharacterSize / 10.0f + 0.5f));
                            else
                                m_TextCropPosition = 0;
                        }
                        else
                        {
                            if (m_TextCropPosition != 0)
                                --m_TextCropPosition;
                        }
                    }
                    // Check if the mouse is on the right of the text AND there is a possibility to scroll
                    else if ((e.X - Position.X > (m_Borders.Left * scalingX) + width) && (m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X > width))
                    {
                        // Move the text by a few pixels
                        if (m_TextFull.CharacterSize > 10)
                        {
                            if (m_TextCropPosition + width < m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X + (m_TextFull.CharacterSize / 10))
                                m_TextCropPosition += (uint)(System.Math.Floor(m_TextFull.CharacterSize / 10.0f + 0.5f));
                            else
                                m_TextCropPosition = (uint)(m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X + (m_TextFull.CharacterSize / 10) - width);
                        }
                        else
                        {
                            if (m_TextCropPosition + width < m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X)
                                ++m_TextCropPosition;
                        }
                    }

                    // Find out between which characters the mouse is standing
                    m_SelEnd = FindSelectionPointPosition(e.X - Position.X - (m_Borders.Left * scalingX));
                }

                // Check if we are selecting text from left to right
                if (m_SelEnd > m_SelStart)
                {
                    // There is no need to redo everything when nothing changed
                    if (m_SelChars != (m_SelEnd - m_SelStart))
                    {
                        // Adjust the number of characters that are selected
                        m_SelChars = m_SelEnd - m_SelStart;

                        // Change our three texts
                        m_TextBeforeSelection.DisplayedString = m_DisplayedText.Substring(0, (int)m_SelStart);
                        m_TextSelection.DisplayedString = m_DisplayedText.Substring((int)m_SelStart, (int)m_SelChars);
                        m_TextAfterSelection.DisplayedString = m_DisplayedText.Substring((int)m_SelEnd);

                        RecalculateTextPositions();
                    }
                }
                else if (m_SelEnd < m_SelStart)
                {
                    // There is no need to redo everything when nothing changed
                    if (m_SelChars != (m_SelStart - m_SelEnd))
                    {
                        // Adjust the number of characters that are selected
                        m_SelChars = m_SelStart - m_SelEnd;

                        // Change our three texts
                        m_TextBeforeSelection.DisplayedString = m_DisplayedText.Substring(0, (int)m_SelEnd);
                        m_TextSelection.DisplayedString = m_DisplayedText.Substring((int)m_SelEnd, (int)m_SelChars);
                        m_TextAfterSelection.DisplayedString = m_DisplayedText.Substring((int)m_SelStart);

                        RecalculateTextPositions();
                    }
                }
                else if (m_SelChars > 0)
                {
                    // Adjust the number of characters that are selected
                    m_SelChars = 0;

                    // Change our three texts
                    m_TextBeforeSelection.DisplayedString = m_DisplayedText;
                    m_TextSelection.DisplayedString = "";
                    m_TextAfterSelection.DisplayedString = "";

                    RecalculateTextPositions();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected internal override void OnKeyPressed(KeyEventArgs e)
        {
            // Check if one of the correct keys was pressed
            if (e.Code == Keyboard.Key.Left)
            {
                // Check if we have selected some text
                if (m_SelChars > 0)
                {
                    // We will not move the selection point, but just undo the selection
                    if (m_SelStart < m_SelEnd)
                        SetSelectionPointPosition(m_SelStart);
                    else
                        SetSelectionPointPosition(m_SelEnd);
                }
                else // When we didn't select any text
                {
                    // Move the selection point to the left
                    if (m_SelEnd > 0)
                        SetSelectionPointPosition(m_SelEnd - 1);
                }

                // Our selection point has moved, it should be visible
                m_SelectionPointVisible = true;
                m_AnimationTimeElapsed = 0;
            }
            else if (e.Code == Keyboard.Key.Right)
            {
                // Check if we have selected some text
                if (m_SelChars > 0)
                {
                    // We will not move the selection point, but just undo the selection
                    if (m_SelStart < m_SelEnd)
                        SetSelectionPointPosition(m_SelEnd);
                    else
                        SetSelectionPointPosition(m_SelStart);
                }
                else // When we didn't select any text
                {
                    // Move the selection point to the right
                    if (m_SelEnd < m_DisplayedText.Length)
                        SetSelectionPointPosition(m_SelEnd + 1);
                }

                // Our selection point has moved, it should be visible
                m_SelectionPointVisible = true;
                m_AnimationTimeElapsed = 0;
            }
            else if (e.Code == Keyboard.Key.Home)
            {
                // Set the selection point to the beginning of the text
                SetSelectionPointPosition(0);

                // Our selection point has moved, it should be visible
                m_SelectionPointVisible = true;
                m_AnimationTimeElapsed = 0;
            }
            else if (e.Code == Keyboard.Key.End)
            {
                // Set the selection point behind the text
                SetSelectionPointPosition((uint)m_Text.Length);

                // Our selection point has moved, it should be visible
                m_SelectionPointVisible = true;
                m_AnimationTimeElapsed = 0;
            }
            else if (e.Code == Keyboard.Key.Return)
            {
                // Add the callback (if the user requested it)
                if (ReturnKeyPressedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.ReturnKeyPressed;
                    m_Callback.Text = m_Text;
                    ReturnKeyPressedCallback (this, m_Callback);
                }
            }
            else if (e.Code == Keyboard.Key.Back)
            {
                // Make sure that we didn't select any characters
                if (m_SelChars == 0)
                {
                    // We can't delete any characters when you are at the beginning of the string
                    if (m_SelEnd == 0)
                        return;

                    // Erase the character
                    m_Text = m_Text.Remove((int)m_SelEnd - 1, 1);
                    m_DisplayedText = m_DisplayedText.Remove((int)m_SelEnd - 1, 1);

                    // Set the selection point back on the correct position
                    SetSelectionPointPosition(m_SelEnd - 1);

                    // Calculate the space inside the edit box
                    float width;
                    if (m_SplitImage)
                        width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
                    else
                        width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.X));

                    // If the width is negative then the edit box is too small to be displayed
                    if (width < 0)
                        width = 0;

                    // Calculate the text width
                    float textWidth = m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X;

                    // If the text can be moved to the right then do so
                    if (textWidth > width)
                    {
                        if (textWidth - m_TextCropPosition < width)
                            m_TextCropPosition = (uint)(textWidth - width);
                    }
                    else
                        m_TextCropPosition = 0;
                }
                else // When you did select some characters, delete them
                    DeleteSelectedCharacters();

                // The selection point should be visible again
                m_SelectionPointVisible = true;
                m_AnimationTimeElapsed = 0;

                // Add the callback (if the user requested it)
                if (TextChangedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.TextChanged;
                    m_Callback.Text = m_Text;
                    TextChangedCallback (this, m_Callback);
                }
            }
            else if (e.Code == Keyboard.Key.Delete)
            {
                // Make sure that no text is selected
                if (m_SelChars == 0)
                {
                    // When the selection point is at the end of the line then you can't delete anything
                    if (m_SelEnd == m_Text.Length)
                        return;

                    // Erase the character
                    m_Text = m_Text.Remove((int)m_SelEnd, 1);
                    m_DisplayedText = m_DisplayedText.Remove((int)m_SelEnd, 1);

                    // Set the selection point back on the correct position
                    SetSelectionPointPosition(m_SelEnd);

                    // Calculate the space inside the edit box
                    float width;
                    if (m_SplitImage)
                        width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
                    else
                        width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.Y));

                    // If the width is negative then the edit box is too small to be displayed
                    if (width < 0)
                        width = 0;

                    // Calculate the text width
                    float textWidth = m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X;

                    // If the text can be moved to the right then do so
                    if (textWidth > width)
                    {
                        if (textWidth - m_TextCropPosition < width)
                            m_TextCropPosition = (uint)(textWidth - width);
                    }
                    else
                        m_TextCropPosition = 0;
                }
                else // You did select some characters, delete them
                    DeleteSelectedCharacters();

                // The selection point should be visible again
                m_SelectionPointVisible = true;
                m_AnimationTimeElapsed = 0;

                // Add the callback (if the user requested it)
                if (TextChangedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.TextChanged;
                    m_Callback.Text = m_Text;
                    TextChangedCallback (this, m_Callback);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnTextEntered (TextEventArgs e)
        {
            // If only numbers are supported then make sure the input is valid
            if (m_NumbersOnly)
            {
                if ((e.Unicode[0] < '0') || (e.Unicode[0] > '9'))
                {
                    if ((e.Unicode[0] == '-') || (e.Unicode[0] == '+'))
                    {
                        if ((m_SelStart == 0) || (m_SelEnd == 0))
                        {
                            if (m_Text.Length != 0)
                            {
                                // You can't have multiple + and - characters after each other
                                if ((m_Text[0] == '-') || (m_Text[0] == '+'))
                                    return;
                            }
                        }
                        else // + and - symbols are only allowed at the beginning of the line
                            return;
                    }
                    else if ((e.Unicode[0] == ',') || (e.Unicode[0] == '.'))
                    {
                        // Only one comma is allowed
                        foreach (char letter in m_Text)
                        {
                            if ((letter == ',') || (letter == '.'))
                                return;
                        }
                    }
                    else // Character not accepted
                        return;
                }
            }

            // If there are selected characters then delete them first
            if (m_SelChars > 0)
                DeleteSelectedCharacters();

            // Make sure we don't exceed our maximum characters limit
            if ((m_MaxChars > 0) && (m_Text.Length + 1 > m_MaxChars))
                return;

            // Insert our character
            m_Text = m_Text.Insert((int)m_SelEnd, e.Unicode);

            // Change the displayed text
            if (m_PasswordChar != "")
                m_DisplayedText = m_DisplayedText.Insert((int)m_SelEnd, m_PasswordChar);
            else
                m_DisplayedText = m_DisplayedText.Insert((int)m_SelEnd, e.Unicode);

            // Append the character to the text
            m_TextFull.DisplayedString = m_DisplayedText;

            // Calculate the space inside the edit box
            float width;
            if (m_SplitImage)
                width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
            else
                width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.X));

            // When there is a text width limit then reverse what we just did
            if (m_LimitTextWidth)
            {
                // Now check if the text fits into the EditBox
                if (m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X > width)
                {
                    // If the text does not fit in the EditBox then delete the added character
                    m_Text = m_Text.Remove((int)m_SelEnd, 1);
                    m_DisplayedText = m_DisplayedText.Remove((int)m_SelEnd, 1);
                    return;
                }
            }

            // Move our selection point forward
            SetSelectionPointPosition(m_SelEnd + 1);

            // The selection point should be visible again
            m_SelectionPointVisible = true;
            m_AnimationTimeElapsed = 0;

            // Add the callback (if the user requested it)
            if (TextChangedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.TextChanged;
                m_Callback.Text = m_Text;
                TextChangedCallback (this, m_Callback);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnWidgetUnfocused()
        {
            // If there is a selection then undo it now
            if (m_SelChars != 0)
                SetSelectionPointPosition(m_SelEnd);

            base.OnWidgetUnfocused();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private uint FindSelectionPointPosition (float posX)
        {
            // This code will crash when the editbox is empty. We need to avoid this.
            if (m_DisplayedText.Length == 0)
                return 0;

            // Find out what the first visible character is
            uint firstVisibleChar;
            if (m_TextCropPosition != 0)
            {
                // Start searching near the selection point to quickly find the character even in a very long string
                firstVisibleChar = m_SelEnd;

                // Go backwards to find the character
                while (m_TextFull.FindCharacterPos(firstVisibleChar-1).X > m_TextCropPosition)
                    --firstVisibleChar;
            }
            else // If the first part is visible then the first character is also visible
                firstVisibleChar = 0;

            string tempString;
            float textWidthWithoutLastChar;
            float fullTextWidth;
            float halfOfLastCharWidth;
            uint lastVisibleChar;

            // Calculate the space inside the edit box
            float width;
            if (m_SplitImage)
                width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
            else
                width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.X));

            // If the width is negative then the editBox is too small to be displayed
            if (width < 0)
                width = 0;

            // Find out how many pixels the text is moved
            float pixelsToMove = 0;
            if (m_TextAlignment != Alignment.Left)
            {
                // Calculate the text width
                float textWidth = m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X;

                // Check if a layout would make sense
                if (textWidth < width)
                {
                    // Set the number of pixels to move
                    if (m_TextAlignment == Alignment.Center)
                        pixelsToMove = (width - textWidth) / 2.0f;
                    else // if (textAlignment == Alignment::Right)
                        pixelsToMove = width - textWidth;
                }
            }

            // Find out what the last visible character is, starting from the selection point
            lastVisibleChar = m_SelEnd;

            // Go forward to find the character
            while (m_TextFull.FindCharacterPos(lastVisibleChar+1).X < m_TextCropPosition + width)
            {
                if (lastVisibleChar == m_DisplayedText.Length)
                    break;

                ++lastVisibleChar;
            }

            // Set the first part of the text
            tempString = m_DisplayedText.Substring(0, (int)firstVisibleChar);
            m_TextFull.DisplayedString = tempString;

            // Calculate the first position
            fullTextWidth = m_TextFull.FindCharacterPos(firstVisibleChar).X;

            // for all the other characters, check where you have clicked.
            for (uint i = firstVisibleChar; i < lastVisibleChar; ++i)
            {
                // Add the next character to the temporary string
                tempString += m_DisplayedText[(int)i];
                m_TextFull.DisplayedString = tempString;

                // Make some calculations
                textWidthWithoutLastChar = fullTextWidth;
                fullTextWidth = m_TextFull.FindCharacterPos(i + 1).X;
                halfOfLastCharWidth = (fullTextWidth - textWidthWithoutLastChar) / 2.0f;

                // Check if you have clicked on the first halve of that character
                if (posX < textWidthWithoutLastChar + pixelsToMove + halfOfLastCharWidth - m_TextCropPosition)
                {
                    m_TextFull.DisplayedString = m_DisplayedText;
                    return i;
                }
            }

            // If you pass here then you clicked behind all the characters
            m_TextFull.DisplayedString = m_DisplayedText;
            return lastVisibleChar;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DeleteSelectedCharacters ()
        {
            // Check if the characters were selected from left to right
            if (m_SelStart < m_SelEnd)
            {
                // Erase the characters
                m_Text = m_Text.Remove((int)m_SelStart, (int)m_SelChars);
                m_DisplayedText = m_DisplayedText.Remove((int)m_SelStart, (int)m_SelChars);

                // Set the selection point back on the correct position
                SetSelectionPointPosition(m_SelStart);
            }
            else // When the text is selected from right to left
            {
                // Erase the characters
                m_Text = m_Text.Remove((int)m_SelEnd, (int)m_SelChars);
                m_DisplayedText = m_DisplayedText.Remove((int)m_SelEnd, (int)m_SelChars);

                // Set the selection point back on the correct position
                SetSelectionPointPosition(m_SelEnd);
            }

            // Calculate the space inside the edit box
            float width;
            if (m_SplitImage)
                width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.Y / m_TextureNormal_M.Size.Y));
            else
                width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * (m_Size.X / m_TextureNormal_M.Size.X));

            // If the width is negative then the edit box is too small to be displayed
            if (width < 0)
                width = 0;

            // Calculate the text width
            float textWidth = m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X;

            // If the text can be moved to the right then do so
            if (textWidth > width)
            {
                if (textWidth - m_TextCropPosition < width)
                    m_TextCropPosition = (uint)(textWidth - width);
            }
            else
                m_TextCropPosition = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void RecalculateTextPositions ()
        {
            float textX = Position.X;
            float textY = Position.Y;

            // Calculate the scaling
            Vector2f scaling = new Vector2f(m_Size.X / m_TextureNormal_M.Size.X, m_Size.Y / m_TextureNormal_M.Size.Y);

            // Calculate the scale of the left and right border
            float borderScale;
            if (m_SplitImage)
                borderScale = scaling.Y;
            else
                borderScale = scaling.X;

            textX += m_Borders.Left * borderScale - m_TextCropPosition;
            textY += m_Borders.Top * scaling.Y;

            // Check if the layout wasn't left
            if (m_TextAlignment != Alignment.Left)
            {
                // Calculate the space inside the edit box
                float width = m_Size.X - ((m_Borders.Left + m_Borders.Right) * borderScale);

                // Calculate the text width
                float textWidth = m_TextFull.FindCharacterPos((uint)m_DisplayedText.Length).X;

                // Check if a layout would make sense
                if (textWidth < width)
                {
                    // Put the text on the correct position
                    if (m_TextAlignment == Alignment.Center)
                        textX += (width - textWidth) / 2.0f;
                    else // if (textAlignment == Alignment::Right)
                        textX += width - textWidth;
                }
            }

            float selectionPointLeft = textX;

            // Set the position of the text
            Text tempText = new Text(m_TextFull);
            tempText.DisplayedString = "kg";
            textY += (((m_Size.Y - ((m_Borders.Top + m_Borders.Bottom) * scaling.Y)) - tempText.GetLocalBounds().Height) * 0.5f) - tempText.GetLocalBounds().Top;

            // Set the text before the selection on the correct position
            m_TextBeforeSelection.Position = new Vector2f((float)System.Math.Floor(textX + 0.5), (float)System.Math.Floor(textY + 0.5));

            // Check if there is a selection
            if (m_SelChars != 0)
            {
                // Watch out for the kerning
                if (m_TextBeforeSelection.DisplayedString.Length > 0)
                    textX += m_TextBeforeSelection.Font.GetKerning(m_DisplayedText[m_TextBeforeSelection.DisplayedString.Length - 1], m_DisplayedText[m_TextBeforeSelection.DisplayedString.Length], m_TextBeforeSelection.CharacterSize);

                textX += m_TextBeforeSelection.FindCharacterPos((uint)m_TextBeforeSelection.DisplayedString.Length).X;

                // Set the position and size of the rectangle that gets drawn behind the selected text
                m_SelectedTextBackground.Size = new Vector2f(m_TextSelection.FindCharacterPos((uint)m_TextSelection.DisplayedString.Length).X,
                                                             (m_Size.Y - ((m_Borders.Top + m_Borders.Bottom) * scaling.Y)));
                m_SelectedTextBackground.Position = new Vector2f((float)System.Math.Floor(textX + 0.5), (float)System.Math.Floor(textY + 0.5));

                // Set the text selected text on the correct position
                m_TextSelection.Position = new Vector2f((float)System.Math.Floor(textX + 0.5), (float)System.Math.Floor(textY + 0.5));

                // Watch out for kerning
                if (m_DisplayedText.Length > m_TextBeforeSelection.DisplayedString.Length + m_TextSelection.DisplayedString.Length)
                    textX += m_TextBeforeSelection.Font.GetKerning(m_DisplayedText[m_TextBeforeSelection.DisplayedString.Length + m_TextSelection.DisplayedString.Length - 1], m_DisplayedText[m_TextBeforeSelection.DisplayedString.Length + m_TextSelection.DisplayedString.Length], m_TextBeforeSelection.CharacterSize);

                // Set the text selected text on the correct position
                textX += m_TextSelection.FindCharacterPos((uint)m_TextSelection.DisplayedString.Length).X;
                m_TextAfterSelection.Position = new Vector2f((float)System.Math.Floor(textX + 0.5), (float)System.Math.Floor(textY + 0.5));
            }

            // Set the position of the selection point
            selectionPointLeft += m_TextFull.FindCharacterPos(m_SelEnd).X - (m_SelectionPoint.Size.X / 2.0f);
            m_SelectionPoint.Position = new Vector2f((float)System.Math.Floor(selectionPointLeft + 0.5), (float)System.Math.Floor((m_Borders.Top * scaling.Y) + Position.Y + 0.5));
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // This function is called when the widget is added to a container.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void Initialize(Container parent)
        {
            base.Initialize(parent);
            TextFont = parent.GlobalFont;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnUpdate ()
        {
            // Only show/hide the selection point every half second
            if (m_AnimationTimeElapsed < 500)
                return;

            // Reset the elapsed time
            m_AnimationTimeElapsed = 0;

            // Only update when the editbox is visible
            if (Visible == false)
                return;

            // Switch the value of the visible flag
            m_SelectionPointVisible = !m_SelectionPointVisible;

            // Too slow for double clicking
            m_PossibleDoubleClick = false;
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
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
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
                    target.Draw(m_TextureNormal_L.sprite, states);
                    target.Draw(m_TextureNormal_M.sprite, states);
                    target.Draw(m_TextureNormal_R.sprite, states);

                    // When the mouse is on top of the edit box then draw an extra image
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        target.Draw(m_TextureHover_L.sprite, states);
                        target.Draw(m_TextureHover_M.sprite, states);
                        target.Draw(m_TextureHover_R.sprite, states);
                    }
                }

                // When the edit box is focused then draw an extra image
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
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                        target.Draw(m_TextureHover_M.sprite, states);
                    else
                        target.Draw(m_TextureNormal_M.sprite, states);
                }
                else // The hover image is drawn on top of the normal one
                {
                    target.Draw(m_TextureNormal_M.sprite, states);

                    // When the mouse is on top of the edit box then draw an extra image
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                        target.Draw(m_TextureHover_M.sprite, states);
                }

                // When the edit box is focused then draw an extra image
                if (m_Focused && (m_WidgetPhase & (byte)WidgetPhase.Focused) != 0)
                    target.Draw(m_TextureFocused_M.sprite, states);
            }

            // Calculate the scaling
            Vector2f scaling = new Vector2f(m_Size.X / m_TextureNormal_M.Size.X, m_Size.Y / m_TextureNormal_M.Size.Y);

            // Calculate the scale of the left and right border
            float borderScale;
            if (m_SplitImage)
                borderScale = scaling.Y;
            else
                borderScale = scaling.X;

            // Calculate the scale factor of the view
            float scaleViewX = target.Size.X / target.GetView().Size.X;
            float scaleViewY = target.Size.Y / target.GetView().Size.Y;

            Vector2f viewPosition = (target.GetView().Size / 2.0f) - target.GetView().Center;

            // Get the global position
            Vector2f topLeftPosition = states.Transform.TransformPoint(Position.X + (m_Borders.Left * borderScale) + viewPosition.X,
                                                                       Position.Y + (m_Borders.Top * scaling.Y) + viewPosition.Y);
            Vector2f bottomRightPosition = states.Transform.TransformPoint(Position.X + (m_Size.X - (m_Borders.Right * borderScale)) + viewPosition.X,
                                                                           Position.Y + (m_Size.Y - (m_Borders.Bottom * scaling.Y)) + viewPosition.Y);

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
            Gl.glScissor(scissorLeft, (int)target.Size.Y - scissorBottom, scissorRight - scissorLeft, scissorBottom - scissorTop);

            target.Draw(m_TextBeforeSelection, states);

            if (m_TextSelection.DisplayedString.Length != 0)
            {
                target.Draw(m_SelectedTextBackground, states);

                target.Draw(m_TextSelection, states);
                target.Draw(m_TextAfterSelection, states);
            }

            // Draw the selection point
            if ((m_Focused) && (m_SelectionPointVisible))
                target.Draw(m_SelectionPoint, states);

            // Reset the old clipping area
            Gl.glScissor(scissor[0], scissor[1], scissor[2], scissor[3]);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

