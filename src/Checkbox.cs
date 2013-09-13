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
    public class Checkbox : ClickableWidget
    {
        public event EventHandler<CallbackArgs> CheckedCallback;
        public event EventHandler<CallbackArgs> UncheckedCallback;
        public event EventHandler<CallbackArgs> SpaceKeyPressedCallback;
        public event EventHandler<CallbackArgs> ReturnKeyPressedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        protected string m_LoadedConfigFile = "";

        // This is the checked flag. When the checkbox is checked then this variable will be true.
        protected bool m_Checked = false;

        // When this boolean is true (default) then the checkbox will also be checked/unchecked by clicking on the text.
        protected bool m_AllowTextClick = true;

        // This will contain the text that is written next to checkbox.
        protected Text m_Text = new Text();

        // This will store the size of the text ( 0 to auto size )
        protected uint m_TextSize = 0;

        protected Impl.Sprite m_TextureUnchecked = new Impl.Sprite();
        protected Impl.Sprite m_TextureChecked = new Impl.Sprite();
        protected Impl.Sprite m_TextureHover = new Impl.Sprite();
        protected Impl.Sprite m_TextureFocused = new Impl.Sprite();
       

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Checkbox()
        {
        }
        

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Checkbox (Checkbox copy) : base(copy)
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
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a Checkbox section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Checkbox (string configFileFilename)
        {
            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "Checkbox");

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
                                    + "' in section Checkbox in " + configFileFilename + ".");
            }

            // Make sure the required textures were loaded
            if ((m_TextureChecked.texture != null) && (m_TextureUnchecked.texture != null))
            {
                Size = new Vector2f(m_TextureUnchecked.Size.X, m_TextureChecked.Size.Y);
            }
            else
                throw new Exception("Not all needed images were loaded for the checkbox. Is the Checkbox section in " + configFileFilename + " complete?");

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
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Checkbox ()
        {
            if (m_TextureChecked.texture != null)    Global.TextureManager.RemoveTexture(m_TextureChecked);
            if (m_TextureUnchecked.texture != null)  Global.TextureManager.RemoveTexture(m_TextureUnchecked);
            if (m_TextureHover.texture != null)      Global.TextureManager.RemoveTexture(m_TextureHover);
            if (m_TextureFocused.texture != null)    Global.TextureManager.RemoveTexture(m_TextureFocused);
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
        /// \brief Changes the size of the widget.
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
        /// \brief Checks the checkbox.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Check ()
        {
            if (m_Checked == false)
            {
                m_Checked = true;

                // Add the callback (if the user requested it)
                if (CheckedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.Checked;
                    m_Callback.Checked = true;
                    CheckedCallback (this, m_Callback);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Unchecks the checkbox.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual void Uncheck ()
        {
            if (m_Checked == true)
            {
                m_Checked = false;

                // Add the callback (if the user requested it)
                if (UncheckedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.Unchecked;
                    m_Callback.Checked = false;
                    UncheckedCallback (this, m_Callback);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns whether the checkbox is checked or not.
        ///
        /// \return Is the checkbox checked?
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsChecked ()
        {
            return m_Checked;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the caption of the checkbox.
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
        /// If the size is set to 0 then the text will be auto-sized.
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
        /// \brief Allow (or disallow) the checkbox to be checked/unchecked by clicking on the text next to the checkbox.
        ///
        /// \param acceptTextClick  Will clicking on the text trigger a checked/unchecked event?
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AllowTextClick (bool acceptTextClick)
        {
            m_AllowTextClick = acceptTextClick;
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

                m_TextureChecked.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureUnchecked.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureFocused.sprite.Color = new Color(255, 255, 255, m_Opacity);

                m_Text.Color = new Color(m_Text.Color.R, m_Text.Color.G, m_Text.Color.B, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
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
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased(MouseButtonEventArgs e)
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
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnKeyPressed(KeyEventArgs e)
        {
            // Check if the space key or the return key was pressed
            if (e.Code == Keyboard.Key.Space)
            {
                if (m_Checked)
                    Uncheck ();
                else
                    Check ();

                if (SpaceKeyPressedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.SpaceKeyPressed;
                    SpaceKeyPressedCallback (this, m_Callback);
                }
            }
            else if (e.Code == Keyboard.Key.Return)
            {
                if (m_Checked)
                    Uncheck ();
                else
                    Check ();

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
            if (m_Checked)
                target.Draw(m_TextureChecked.sprite, states);
            else
                target.Draw(m_TextureUnchecked.sprite, states);

            // When the checkbox is focused then draw an extra image
            if (m_Focused && (m_WidgetPhase & (byte)WidgetPhase.Focused) != 0)
                target.Draw(m_TextureFocused.sprite, states);

            // When the mouse is on top of the checkbox then draw an extra image
            if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                target.Draw(m_TextureHover.sprite, states);

            // Draw the text
            target.Draw(m_Text, states);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

