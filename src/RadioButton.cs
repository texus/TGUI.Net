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
    public class RadioButton : ClickableWidget
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal RadioButton ()
        {
            m_Text.Color = Color.Black;
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
            CheckedCallback = copy.CheckedCallback;
            UncheckedCallback = copy.UncheckedCallback;
            SpaceKeyPressedCallback = copy.SpaceKeyPressedCallback;
            ReturnKeyPressedCallback = copy.ReturnKeyPressedCallback;

            m_LoadedConfigFile = copy.m_LoadedConfigFile;
            m_Checked          = copy.m_Checked;
            m_AllowTextClick   = copy.m_AllowTextClick;
            m_Text             = new Text(copy.m_Text);
            m_TextSize         = copy.m_TextSize;

            Global.TextureManager.CopyTexture(copy.m_TextureUnchecked, m_TextureUnchecked);
            Global.TextureManager.CopyTexture(copy.m_TextureChecked, m_TextureChecked);
            Global.TextureManager.CopyTexture(copy.m_TextureHover, m_TextureHover);
            Global.TextureManager.CopyTexture(copy.m_TextureFocused, m_TextureFocused);
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
            m_Text.Color = Color.Black;

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
        /// Destructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~RadioButton ()
        {
            if (m_TextureChecked.texture != null)    Global.TextureManager.RemoveTexture(m_TextureChecked);
            if (m_TextureUnchecked.texture != null)  Global.TextureManager.RemoveTexture(m_TextureUnchecked);
            if (m_TextureHover.texture != null)      Global.TextureManager.RemoveTexture(m_TextureHover);
            if (m_TextureFocused.texture != null)    Global.TextureManager.RemoveTexture(m_TextureFocused);
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

                m_TextureUnchecked.sprite.Position = value;
                m_TextureChecked.sprite.Position = new Vector2f(value.X, value.Y + m_TextureUnchecked.Size.Y - m_TextureChecked.Size.Y);
                m_TextureFocused.sprite.Position = value;
                m_TextureHover.sprite.Position = value;

                FloatRect textBounds = m_Text.GetLocalBounds();
                m_Text.Position = new Vector2f(value.X + (float)System.Math.Floor(m_Size.X * 11.0f / 10.0f - textBounds.Left),
                                               value.Y + (float)System.Math.Floor(((m_Size.Y - textBounds.Height) / 2.0f) - textBounds.Top));
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the radio button
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override Vector2f Size
        {
            get
            {
                if (m_Text.DisplayedString.Length == 0)
                    return m_Size;
                else
                    return new Vector2f((m_Size.X * 11.0f / 10.0f) + m_Text.GetLocalBounds().Left + m_Text.GetLocalBounds().Width, m_Size.Y);
            }
            set
            {
                m_Size = value;

                // If the text is auto sized then recalculate the size
                if (m_TextSize == 0)
                    Text = m_Text.DisplayedString;

                Vector2f scaling = new Vector2f(m_Size.X / m_TextureUnchecked.Size.X, m_Size.Y / m_TextureUnchecked.Size.Y);
                m_TextureChecked.sprite.Scale = scaling;
                m_TextureUnchecked.sprite.Scale = scaling;
                m_TextureFocused.sprite.Scale = scaling;
                m_TextureHover.sprite.Scale = scaling;

                // Reposition the text
                Position = Position;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Check the radio button and uncheck all other radio buttons that have the same parent
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Check ()
        {
            if (m_Checked == false)
            {
                // Tell our parent that all the radio buttons should be unchecked
                m_Parent.UncheckRadioButtons();

                // Check this radio button
                m_Checked = true;

                // Add the callback (if the user requested it)
                SendCheckedCallback ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Unchecks the radio button
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Uncheck ()
        {
            if (m_Checked)
            {
                m_Checked = false;

                // Add the callback (if the user requested it)
                SendUncheckedCallback ();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns whether the radio button is checked or not
        /// </summary>
        ///
        /// <returns>Is the radio button checked?</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsChecked ()
        {
            return m_Checked;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The caption of the radio button.
        /// This is the text that is drawn next to the radio button.
        /// </summary>
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

                // Check if the text is auto sized
                if (m_TextSize == 0)
                {
                    m_Text.CharacterSize = (uint)(m_Size.Y);
                    m_Text.CharacterSize = (uint)(m_Text.CharacterSize - m_Text.GetLocalBounds().Top);
                }
                else // When the text has a fixed size
                {
                    // Set the text size
                    m_Text.CharacterSize = m_TextSize;
                }

                // Reposition the text
                Position = Position;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Font of the text.
        /// By default, the GlobalFont of the parent is used.
        /// </summary>
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
        /// <summary>
        /// The color of the text
        /// </summary>
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
        /// <summary>
        /// Character size of the text.
        /// If the size is set to 0 then the text will be auto-sized.
        /// </summary>
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
        /// <summary>
        /// Allow (or disallow) the radio button to be checked by clicking on the text next to the radio button
        /// </summary>
        ///
        /// <param name="acceptTextClick">Will clicking on the text trigger a checked event?</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AllowTextClick (bool acceptTextClick)
        {
            m_AllowTextClick = acceptTextClick;
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

                m_TextureChecked.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureUnchecked.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureFocused.sprite.Color = new Color(255, 255, 255, m_Opacity);
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
            if (Transform.TransformRect(new FloatRect(0, 0, Size.X, Size.Y)).Contains(x, y))
                return true;

            // Check if the mouse is on top of the text
            if (m_AllowTextClick)
            {
                FloatRect bounds = m_Text.GetLocalBounds();
                if (new FloatRect(bounds.Left, bounds.Top, bounds.Width, bounds.Height).Contains(x - (Position.X + ((m_Size.X * 11.0f / 10.0f))), y - Position.Y - ((m_Size.Y - bounds.Height) / 2.0f) + bounds.Top))
                    return true;
            }

            if (m_MouseHover)
                MouseLeftWidget();

            m_MouseHover = false;
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            // Check the radio button
            if (m_MouseDown == true)
                Check ();

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
                // Check the radio button
                Check ();

                // Add the callback (if the user requested it)
                SendSpaceKeyPressedCallback ();
            }
            else if (e.Code == Keyboard.Key.Return)
            {
                // Check the radio button
                Check ();

                // Add the callback (if the user requested it)
                SendReturnKeyPressedCallback ();
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
            // We can't be focused when we don't have a focus image
            if ((m_WidgetPhase & (byte)WidgetPhase.Focused) == 0)
                Focused = false;
            else
                base.OnWidgetFocused ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initializes the widget now that it has been added to a parent widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void Initialize(Container parent)
        {
            base.Initialize(parent);
            m_Text.Font = parent.GlobalFont;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (m_Checked)
                target.Draw(m_TextureChecked.sprite, states);
            else
                target.Draw(m_TextureUnchecked.sprite, states);

            // When the radio button is focused then draw an extra image
            if (m_Focused && (m_WidgetPhase & (byte)WidgetPhase.Focused) != 0)
                target.Draw(m_TextureFocused.sprite, states);

            // When the mouse is on top of the radio button then draw an extra image
            if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                target.Draw(m_TextureHover.sprite, states);

            // Draw the text
            target.Draw(m_Text, states);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Send a checked callback
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void SendCheckedCallback ()
        {
            if (CheckedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.Checked;
                m_Callback.Checked = IsChecked();
                CheckedCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Send a unchecked callback
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void SendUncheckedCallback ()
        {
            if (UncheckedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.Unchecked;
                m_Callback.Checked = IsChecked();
                UncheckedCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Send a space key pressed callback
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void SendSpaceKeyPressedCallback ()
        {
            if (SpaceKeyPressedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.SpaceKeyPressed;
                m_Callback.Checked = IsChecked();
                SpaceKeyPressedCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Send a return key pressed callback
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void SendReturnKeyPressedCallback ()
        {
            if (ReturnKeyPressedCallback != null)
            {
                m_Callback.Trigger = CallbackTrigger.ReturnKeyPressed;
                m_Callback.Checked = IsChecked();
                ReturnKeyPressedCallback (this, m_Callback);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the Checked event</summary>
        public event EventHandler<CallbackArgs> CheckedCallback;

        /// <summary>Event handler for the Unchecked event</summary>
        public event EventHandler<CallbackArgs> UncheckedCallback;

        /// <summary>Event handler for the SpaceKeyPressed event</summary>
        public event EventHandler<CallbackArgs> SpaceKeyPressedCallback;

        /// <summary>Event handler for the ReturnKeyPressed event</summary>
        public event EventHandler<CallbackArgs> ReturnKeyPressedCallback;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected string m_LoadedConfigFile = "";

        // This is the checked flag. When the radio button is checked then this variable will be true.
        protected bool m_Checked = false;

        // When this boolean is true (default) then the radio button will also be checked by clicking on the text.
        protected bool m_AllowTextClick = true;

        // This will contain the text that is written next to radio button.
        protected Text m_Text = new Text();

        // This will store the size of the text ( 0 to auto size )
        protected uint m_TextSize = 0;

        protected Impl.Sprite m_TextureUnchecked = new Impl.Sprite();
        protected Impl.Sprite m_TextureChecked = new Impl.Sprite();
        protected Impl.Sprite m_TextureHover = new Impl.Sprite();
        protected Impl.Sprite m_TextureFocused = new Impl.Sprite();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
