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
    public class Checkbox : RadioButton
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Checkbox()
        {
            m_DraggableWidget = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a Checkbox section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Checkbox (string configFileFilename)
        {
            m_DraggableWidget = true;

            m_LoadedConfigFile = Global.ResourcePath + configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (m_LoadedConfigFile, "Checkbox");

            // Find the folder that contains the config file
            string configFileFolder = m_LoadedConfigFile.Substring(0, m_LoadedConfigFile.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "textcolor")
                    m_Text.Color = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "checkedimage")
                    configFile.ReadTexture (i, configFileFolder, m_TextureChecked);
                else if (configFile.Properties[i] == "uncheckedimage")
                    configFile.ReadTexture (i, configFileFolder, m_TextureUnchecked);
                else if (configFile.Properties[i] == "hoverimage")
                    configFile.ReadTexture (i, configFileFolder, m_TextureHover);
                else if (configFile.Properties[i] == "focusedimage")
                    configFile.ReadTexture (i, configFileFolder, m_TextureFocused);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section Checkbox in " + m_LoadedConfigFile + ".");
            }

            // Make sure the required textures were loaded
            if ((m_TextureChecked.texture != null) && (m_TextureUnchecked.texture != null))
            {
                Size = new Vector2f(m_TextureUnchecked.Size.X, m_TextureChecked.Size.Y);
            }
            else
                throw new Exception("Not all needed images were loaded for the checkbox. Is the Checkbox section in " + m_LoadedConfigFile + " complete?");

            // Check if optional textures were loaded
            if (m_TextureFocused.texture != null)
            {
                m_AllowFocus = true;
                m_WidgetPhase |= (byte)WidgetPhase.Focused;
            }
            if (m_TextureHover.texture != null)
            {
                m_WidgetPhase |= (byte)WidgetPhase.Hover;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Checks the checkbox
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Check ()
        {
            if (m_Checked == false)
            {
                m_Checked = true;

                // Add the callback (if the user requested it)
                SendCheckedCallback ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Unchecks the checkbox
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Uncheck ()
        {
            if (m_Checked == true)
            {
                m_Checked = false;

                // Add the callback (if the user requested it)
                SendUncheckedCallback ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            // Check or uncheck the checkbox
            if (m_MouseDown == true)
            {
                if (m_Checked)
                    Uncheck ();
                else
                    Check ();
            }

            base.OnLeftMouseReleased (e);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that a special key has been pressed while the widget was focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnKeyPressed (KeyEventArgs e)
        {
            // Check if the space key or the return key was pressed
            if (e.Code == Keyboard.Key.Space)
            {
                if (m_Checked)
                    Uncheck ();
                else
                    Check ();

                SendSpaceKeyPressedCallback ();
            }
            else if (e.Code == Keyboard.Key.Return)
            {
                if (m_Checked)
                    Uncheck ();
                else
                    Check ();

                SendReturnKeyPressedCallback ();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
