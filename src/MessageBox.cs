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
using SFML.Window;
using SFML.Graphics;
using Tao.OpenGl;

namespace TGUI
{
    public class MessageBox : ChildWindow
    {
        public event EventHandler<CallbackArgs> ButtonClickedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string       m_LoadedConfigFile = "";
        private string       m_ButtonConfigFileFilename = "";

        private List<Button> m_Buttons = new List<Button>();

        private Label        m_Label = new Label();

        private uint         m_TextSize = 16;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal MessageBox ()
        {
            Add(m_Label, "MessageBoxText");
            m_Label.TextSize = m_TextSize;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MessageBox (MessageBox copy) : base(copy)
        {
            ButtonClickedCallback      = copy.ButtonClickedCallback;

            m_LoadedConfigFile         = copy.m_LoadedConfigFile;
            m_ButtonConfigFileFilename = copy.m_ButtonConfigFileFilename;
            m_Buttons                  = copy.m_Buttons;
            m_Label                    = copy.m_Label;
            m_TextSize                 = copy.m_TextSize;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a MessageBox section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MessageBox (string configFileFilename)
        {
            Add(m_Label, "MessageBoxText");
            m_Label.TextSize = m_TextSize;

            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "MessageBox");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            bool childWindowPropertyFound = false;
            bool buttonPropertyFound = false;

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "textcolor")
                    m_Label.TextColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "childwindow")
                {
                    if ((configFile.Values[i].Length < 3) || (configFile.Values[i][0] != '"') || (configFile.Values[i][configFile.Values[i].Length-1] != '"'))
                        throw new Exception("Failed to parse value for ChildWindow in section MessageBox in " + configFileFilename + ".");

                    InternalLoad (configFileFolder + configFile.Values [i].Substring (1, configFile.Values [i].Length - 2));
                    childWindowPropertyFound = true;
                }
                else if (configFile.Properties[i] == "button")
                {
                    if ((configFile.Values[i].Length < 3) || (configFile.Values[i][0] != '"') || (configFile.Values[i][configFile.Values[i].Length-1] != '"'))
                        throw new Exception("Failed to parse value for Button in section MessageBox in " + configFileFilename + ".");

                    m_ButtonConfigFileFilename = configFileFolder + configFile.Values [i].Substring (1, configFile.Values [i].Length - 2);
                    buttonPropertyFound = true;
                }
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section MessageBox in " + configFileFilename + ".");
            }

            if (!childWindowPropertyFound)
                throw new Exception("TGUI error: Missing a ChildWindow property in section MessageBox in " + configFileFilename + ".");

            if (!buttonPropertyFound)
                throw new Exception("TGUI error: Missing a Button property in section MessageBox in " + configFileFilename + ".");
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string Text
        {
            get
            {
                return m_Label.Text;
            }
            set
            {
                m_Label.Text = value;
                Rearrange();
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
                return m_Label.TextFont;
            }
            set
            {
                m_Label.TextFont = value;
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
                return m_Label.TextColor;
            }
            set
            {
                m_Label.TextColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint TextSize
        {
            get
            {
                return m_TextSize;
            }
            set
            {
                // Change the text size
                m_TextSize = value;
                m_Label.TextSize = value;

                for (int i = 0; i < m_Buttons.Count; ++i)
                    m_Buttons[i].TextSize = m_TextSize;

                Rearrange();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddButton (string caption)
        {
            Button button = Add (new Button (m_ButtonConfigFileFilename));
            button.TextSize = m_TextSize;
            button.Text = caption;
            button.LeftMouseClickedCallback += ButtonClickedCallbackFunction;
            button.SpaceKeyPressedCallback += ButtonClickedCallbackFunction;
            button.ReturnKeyPressedCallback += ButtonClickedCallbackFunction;

            m_Buttons.Add(button);

            Rearrange();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Rearrange ()
        {
            float width, height;

            // Calculate the button size
            float buttonWidth = 5.0f * m_TextSize;
            float buttonHeight = m_TextSize * 10.0f / 8.0f;
            for (int i = 0; i < m_Buttons.Count; ++i)
            {
                width = new Text(m_Buttons[i].Text, m_Buttons[i].TextFont, m_TextSize).GetLocalBounds().Width;
                if (buttonWidth < width * 10.0f / 9.0f)
                    buttonWidth = width * 10.0f / 9.0f;
            }

            // Calculate the space needed for the buttons
            float distance = buttonHeight * 2.0f / 3.0f;
            float buttonsAreaWidth = distance;
            for (int i = 0; i < m_Buttons.Count; ++i)
            {
                m_Buttons[i].Size = new Vector2f(buttonWidth, buttonHeight);
                buttonsAreaWidth += m_Buttons[i].Size.X + distance;
            }

            // Calculate the suggested size of the window
            width = 2*distance + m_Label.Size.X;
            height = 3*distance + m_Label.Size.Y + buttonHeight;

            // Make sure the buttons fit inside the message box
            if (buttonsAreaWidth > width)
                width = buttonsAreaWidth;

            // Set the size of the window
            Size = new Vector2f(width, height);

            // Set the text on the correct position
            m_Label.Position = new Vector2f(distance, distance);

            // Set the buttons on the correct position
            float leftPosition = 0;
            float topPosition = 2*distance + m_Label.Size.Y;
            for (int i = 0; i < m_Buttons.Count; ++i)
            {
                leftPosition += distance + ((width - buttonsAreaWidth) / (m_Buttons.Count + 1));
                m_Buttons[i].Position = new Vector2f(leftPosition, topPosition);
                leftPosition += m_Buttons[i].Size.X;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ButtonClickedCallbackFunction(object sender, CallbackArgs e)
        {
            if (ButtonClickedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.ButtonClicked;
                m_Callback.Text    = ((Button)sender).Text;
                ButtonClickedCallback (this, m_Callback);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

