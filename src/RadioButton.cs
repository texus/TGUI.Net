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
    public class RadioButton : Checkbox
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal RadioButton ()
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
        public RadioButton (RadioButton copy) : base(copy)
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a RadioButton section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public RadioButton (string configFileFilename)
        {
            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "RadioButton");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

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
                                    + "' in section RadioButton in " + configFileFilename + ".");
            }

            // Make sure the required textures were loaded
            if ((m_TextureChecked.texture != null) && (m_TextureUnchecked.texture != null))
            {
                Size = new Vector2f(m_TextureUnchecked.Size.X, m_TextureChecked.Size.Y);
            }
            else
            {
                throw new Exception("Not all needed images were loaded for the radio button. Is the RadioButton section in "
                                    + configFileFilename + " complete?");
            }

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
        /// Check the radio button and uncheck all other radio buttons that have the same parent
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Check ()
        {
            if (!IsChecked())
            {
                // Tell our parent that all the radio buttons should be unchecked
                m_Parent.UncheckRadioButtons();

                // Check this radio button
                base.Check();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Does nothing, take a look at ForceUncheck instead.
        /// Unchecking a radio button isn't possible so this function makes sure that nothing happens when trying to uncheck it.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Uncheck ()
        {
            // The radio button can't be unchecked, so we override the original function with an empty one.
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Unchecks the radio button
        /// </summary>
        ///
        /// If you really want a radio button to be unchecked, you can use this function.
        /// But you should rather call the UncheckRadioButtons function from the parent widget.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void ForceUncheck ()
        {
            base.Uncheck ();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
