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
    public class SpinButton : ClickableWidget
    {
        public event EventHandler<CallbackArgs> ValueChangedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string m_LoadedConfigFile = "";

        // Is the spin button draw vertically (arrows on top of each other)?
        private bool   m_VerticalScroll = true;

        private int    m_Minimum = 0;
        private int    m_Maximum = 10;
        private int    m_Value = 0;

        // Is there a separate hover image, or is it a semi-transparent image that is drawn on top of the others?
        private bool   m_SeparateHoverImage = false;

        // On which arrow is the mouse?
        private bool   m_MouseHoverOnTopArrow = false;
        private bool   m_MouseDownOnTopArrow = false;

        private Impl.Sprite m_TextureArrowUpNormal = new Impl.Sprite();
        private Impl.Sprite m_TextureArrowUpHover = new Impl.Sprite();
        private Impl.Sprite m_TextureArrowDownNormal = new Impl.Sprite();
        private Impl.Sprite m_TextureArrowDownHover = new Impl.Sprite();


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal SpinButton ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SpinButton (SpinButton copy) : base(copy)
        {
            ValueChangedCallback   = copy.ValueChangedCallback;

            m_LoadedConfigFile     = copy.m_LoadedConfigFile;
            m_VerticalScroll       = copy.m_VerticalScroll;
            m_Minimum              = copy.m_Minimum;
            m_Maximum              = copy.m_Maximum;
            m_Value                = copy.m_Value;
            m_SeparateHoverImage   = copy.m_SeparateHoverImage;
            m_MouseHoverOnTopArrow = copy.m_MouseHoverOnTopArrow;
            m_MouseDownOnTopArrow  = copy.m_MouseDownOnTopArrow;

            Global.TextureManager.CopyTexture(copy.m_TextureArrowUpNormal, m_TextureArrowUpNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowUpHover, m_TextureArrowUpHover);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowDownNormal, m_TextureArrowDownNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowDownHover, m_TextureArrowDownHover);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a SpinButton section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SpinButton (string configFileFilename)
        {
            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "SpinButton");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "separatehoverimage")
                    m_SeparateHoverImage = configFile.ReadBool(i);
                else if (configFile.Properties[i] == "arrowupnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowUpNormal);
                else if (configFile.Properties[i] == "arrowuphoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowUpHover);
                else if (configFile.Properties[i] == "arrowdownnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowDownNormal);
                else if (configFile.Properties[i] == "arrowdownhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowDownHover);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section SpinButton in " + configFileFilename + ".");
            }

            // Make sure the required textures were loaded
            if ((m_TextureArrowUpNormal.texture != null) && (m_TextureArrowDownNormal.texture != null))
            {
                m_Size = new Vector2f(m_TextureArrowUpNormal.Size.X, m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y);
            }
            else
            {
                throw new Exception("Not all needed images were loaded for the spin button. Is the SpinButton section in " + configFileFilename + " complete?");
            }

            // Check if optional textures were loaded
            if ((m_TextureArrowUpHover.texture != null) && (m_TextureArrowDownHover.texture != null))
            {
                m_WidgetPhase |= (byte)WidgetPhase.Hover;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~SpinButton ()
        {
            if (m_TextureArrowUpNormal.texture != null)   Global.TextureManager.RemoveTexture(m_TextureArrowUpNormal);
            if (m_TextureArrowUpHover.texture != null)    Global.TextureManager.RemoveTexture(m_TextureArrowUpHover);
            if (m_TextureArrowDownNormal.texture != null) Global.TextureManager.RemoveTexture(m_TextureArrowDownNormal);
            if (m_TextureArrowDownHover.texture != null)  Global.TextureManager.RemoveTexture(m_TextureArrowDownHover);
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
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Sets a minimum value.
        ///
        /// \param minimum  The new minimum value
        ///
        /// When the value is too small then it will be changed to this minimum.
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
                if (m_Value < m_Minimum)
                    m_Value = m_Minimum;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Sets a maximum value.
        ///
        /// \param maximum  The new maximum value
        ///
        /// When the value is too big then it will be changed to this maximum.
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
                if (m_Value > m_Maximum)
                    m_Value = m_Maximum;
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
                m_Value = value;

                // When the value is below the minimum or above the maximum then adjust it
                if (m_Value < m_Minimum)
                    m_Value = m_Minimum;
                else if (m_Value > m_Maximum)
                    m_Value = m_Maximum;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes whether the spin button lies vertical or horizontal (arrows above or next to each other).
        ///
        /// \param verticallScroll  Does the spin button lie vertically?
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

                m_TextureArrowUpNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureArrowUpHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureArrowDownNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureArrowDownHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed(MouseButtonEventArgs e)
        {
            m_MouseDown = true;

            // Check if the mouse is on top of the upper/right arrow
            if (m_VerticalScroll)
            {
                if (Transform.TransformRect(new FloatRect(0, 0, m_Size.X, m_Size.Y / 2.0f)).Contains(e.X, e.Y))
                    m_MouseDownOnTopArrow = true;
                else
                    m_MouseDownOnTopArrow = false;
            }
            else
            {
                if (Transform.TransformRect(new FloatRect(0, 0, m_Size.X / 2.0f, m_Size.Y)).Contains(e.X, e.Y))
                    m_MouseDownOnTopArrow = false;
                else
                    m_MouseDownOnTopArrow = true;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased(MouseButtonEventArgs e)
        {
            // Check if the mouse went down on the spin button
            if (m_MouseDown)
            {
                m_MouseDown = false;

                // Check if the arrow went down on the top/right arrow
                if (m_MouseDownOnTopArrow)
                {
                    // Check if the mouse went up on the same arrow
                    if (((m_VerticalScroll == true)  && (Transform.TransformRect(new FloatRect(0, 0, m_Size.X, m_Size.Y / 2.0f)).Contains(e.X, e.Y)))
                        || ((m_VerticalScroll == false) && (Transform.TransformRect(new FloatRect(0, 0, m_Size.X / 2.0f, m_Size.Y)).Contains(e.X, e.Y) == false)))
                    {
                        // Increment the value
                        if (m_Value < m_Maximum)
                            ++m_Value;
                        else
                            return;
                    }
                    else
                        return;
                }
                else // The mouse went down on the bottom/left arrow
                {
                    // Check if the mouse went up on the same arrow
                    if (((m_VerticalScroll == true)  && (Transform.TransformRect(new FloatRect(0, 0, m_Size.X, m_Size.Y / 2.0f)).Contains(e.X, e.Y) == false))
                        || ((m_VerticalScroll == false) && (Transform.TransformRect(new FloatRect(0, 0, m_Size.X / 2.0f, m_Size.Y)).Contains(e.X, e.Y))))
                    {
                        // Decrement the value
                        if (m_Value > m_Minimum)
                            --m_Value;
                        else
                            return;
                    }
                    else
                        return;
                }

                // Add the callback (if the user requested it)
                if (ValueChangedCallback != null)
                {
                    m_Callback.Trigger = CallbackTrigger.ValueChanged;
                    m_Callback.Value   = m_Value;
                    ValueChangedCallback (this, m_Callback);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved(MouseMoveEventArgs e)
        {
            // Check if the mouse is on top of the upper/right arrow
            if (m_VerticalScroll)
            {
                if (Transform.TransformRect(new FloatRect(0, 0, m_Size.X, m_Size.Y / 2.0f)).Contains(e.X, e.Y))
                    m_MouseHoverOnTopArrow = true;
                else
                    m_MouseHoverOnTopArrow = false;
            }
            else
            {
                if (Transform.TransformRect(new FloatRect(0, 0, m_Size.X / 2.0f, m_Size.Y)).Contains(e.X, e.Y))
                    m_MouseHoverOnTopArrow = false;
                else
                    m_MouseHoverOnTopArrow = true;
            }

            if (m_MouseHover == false)
                MouseEnteredWidget ();

            m_MouseHover = true;
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
            // Adjust the transformation
            states.Transform *= Transform;

            // Check if the image is drawn in the same direction than it was loaded
            if (m_VerticalScroll)
            {
                states.Transform.Scale(m_Size.X / m_TextureArrowUpNormal.Size.X, m_Size.Y / (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y));

                // Draw the first arrow
                if (m_SeparateHoverImage)
                {
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        if (m_MouseHoverOnTopArrow)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                    }
                    else
                        target.Draw(m_TextureArrowUpNormal.sprite, states);
                }
                else // The hover image should be drawn on top of the normal image
                {
                    target.Draw(m_TextureArrowUpNormal.sprite, states);

                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        if (m_MouseHoverOnTopArrow)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                    }
                }

                // Set the second arrow on the correct position
                states.Transform.Translate(0, (float)(m_TextureArrowUpNormal.Size.Y));

                // Draw the second arrow
                if (m_SeparateHoverImage)
                {
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        if (m_MouseHoverOnTopArrow)
                            target.Draw(m_TextureArrowDownHover.sprite, states);
                    }
                    else
                        target.Draw(m_TextureArrowDownNormal.sprite, states);
                }
                else // The hover image should be drawn on top of the normal image
                {
                    target.Draw(m_TextureArrowDownNormal.sprite, states);

                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        if (!m_MouseHoverOnTopArrow)
                            target.Draw(m_TextureArrowDownHover.sprite, states);
                    }
                }
            }
            else // The image is not drawn in the same direction than the loaded image
            {
                states.Transform.Scale(m_Size.X / (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y), m_Size.Y / m_TextureArrowUpNormal.Size.X);

                // Rotate the arrow
                states.Transform.Rotate(-90, (float)(m_TextureArrowUpNormal.Size.X), (float)(m_TextureArrowUpNormal.Size.Y));

                // Set the left arrow on the correct position
                states.Transform.Translate((float)(m_TextureArrowUpNormal.Size.Y), 0);

                // Draw the first arrow
                if (m_SeparateHoverImage)
                {
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        if (m_MouseHoverOnTopArrow)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                    }
                    else
                        target.Draw(m_TextureArrowUpNormal.sprite, states);
                }
                else // The hover image should be drawn on top of the normal image
                {
                    target.Draw(m_TextureArrowUpNormal.sprite, states);

                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        if (!m_MouseHoverOnTopArrow)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                    }
                }

                // Set the right arrow on the correct position
                states.Transform.Translate(0, m_TextureArrowUpNormal.Size.Y);

                // Draw the second arrow
                if (m_SeparateHoverImage)
                {
                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        if (m_MouseHoverOnTopArrow)
                            target.Draw(m_TextureArrowDownHover.sprite, states);
                    }
                    else
                        target.Draw(m_TextureArrowDownNormal.sprite, states);
                }
                else // The hover image should be drawn on top of the normal image
                {
                    target.Draw(m_TextureArrowDownNormal.sprite, states);

                    if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    {
                        if (m_MouseHoverOnTopArrow)
                            target.Draw(m_TextureArrowDownHover.sprite, states);
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

