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
    public class Tab : Widget
    {
        public event EventHandler<CallbackArgs> TabChangedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string       m_LoadedConfigFile = "";

        private bool         m_SplitImage = false;
        private bool         m_SeparateSelectedImage = true;

        private uint         m_TabHeight = 0;
        private uint         m_TextSize = 0;

        private uint         m_MaximumTabWidth = 0;

        // The distance between the side of the tab and the text that is drawn on top of the tab.
        private uint         m_DistanceToSide = 5;

        private int          m_SelectedTab = 0;

        private List<string> m_TabNames = new List<string>();
        private List<float>  m_NameWidth = new List<float>();

        private Impl.Sprite m_TextureNormal_L = new Impl.Sprite();
        private Impl.Sprite m_TextureNormal_M = new Impl.Sprite();
        private Impl.Sprite m_TextureNormal_R = new Impl.Sprite();
        private Impl.Sprite m_TextureSelected_L = new Impl.Sprite();
        private Impl.Sprite m_TextureSelected_M = new Impl.Sprite();
        private Impl.Sprite m_TextureSelected_R = new Impl.Sprite();

        private Text         m_Text = new Text();


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Tab ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Tab (Tab copy) : base(copy)
        {
            TabChangedCallback = copy.TabChangedCallback;

            m_LoadedConfigFile      = copy.m_LoadedConfigFile;
            m_SplitImage            = copy.m_SplitImage;
            m_SeparateSelectedImage = copy.m_SeparateSelectedImage;
            m_TabHeight             = copy.m_TabHeight;
            m_TextSize              = copy.m_TextSize;
            m_MaximumTabWidth       = copy.m_MaximumTabWidth;
            m_DistanceToSide        = copy.m_DistanceToSide;
            m_SelectedTab           = copy.m_SelectedTab;
            m_TabNames              = copy.m_TabNames;
            m_NameWidth             = copy.m_NameWidth;
            m_Text                  = copy.m_Text;

            Global.TextureManager.CopyTexture(copy.m_TextureNormal_L, m_TextureNormal_L);
            Global.TextureManager.CopyTexture(copy.m_TextureNormal_M, m_TextureNormal_M);
            Global.TextureManager.CopyTexture(copy.m_TextureNormal_R, m_TextureNormal_R);

            Global.TextureManager.CopyTexture(copy.m_TextureSelected_L, m_TextureSelected_L);
            Global.TextureManager.CopyTexture(copy.m_TextureSelected_M, m_TextureSelected_M);
            Global.TextureManager.CopyTexture(copy.m_TextureSelected_R, m_TextureSelected_R);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a Tab section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Tab (string configFileFilename)
        {
            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "Tab");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "separateselectedimage")
                    m_SeparateSelectedImage = configFile.ReadBool(i);
                else if (configFile.Properties[i] == "textcolor")
                    m_Text.Color = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "distancetoside")
                    DistanceToSide = Convert.ToUInt32(configFile.Values [i]);
                else if (configFile.Properties[i] == "normalimage")
                {
                    configFile.ReadTexture (i, configFileFolder, m_TextureNormal_M);
                    m_SplitImage = false;
                }
                else if (configFile.Properties[i] == "selectedimage")
                    configFile.ReadTexture (i, configFileFolder, m_TextureSelected_M);
                else if (configFile.Properties[i] == "normalimage_l")
                    configFile.ReadTexture (i, configFileFolder, m_TextureNormal_L);
                else if (configFile.Properties[i] == "normalimage_m")
                {
                    configFile.ReadTexture(i, configFileFolder, m_TextureNormal_M);
                    m_SplitImage = true;
                }
                else if (configFile.Properties[i] == "normalimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureNormal_R);
                else if (configFile.Properties[i] == "selectedimage_l")
                    configFile.ReadTexture (i, configFileFolder, m_TextureSelected_L);
                else if (configFile.Properties[i] == "selectedimage_m")
                    configFile.ReadTexture(i, configFileFolder, m_TextureSelected_M);
                else if (configFile.Properties[i] == "selectedimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureSelected_R);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section Tab in " + configFileFilename + ".");
            }

            // Check if the image is split
            if (m_SplitImage)
            {
                // Make sure the required textures were loaded
                if ((m_TextureNormal_L.texture != null) && (m_TextureNormal_M.texture != null) && (m_TextureNormal_R.texture != null))
                {
                    m_TabHeight = (uint)m_TextureNormal_M.Size.Y;

                    m_TextureNormal_M.texture.texture.Repeated = true;
                }
                else
                    throw new Exception("Not all needed images were loaded for the tab. Is the Tab section in " + configFileFilename + " complete?");

                // Check if optional textures were loaded
                if ((m_TextureSelected_L.texture != null) && (m_TextureSelected_M.texture != null) && (m_TextureSelected_R.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Selected;

                    m_TextureSelected_M.texture.texture.Repeated = true;
                }
            }
            else // The image isn't split
            {
                // Make sure the required texture was loaded
                if (m_TextureNormal_M.texture != null)
                {
                    m_TabHeight = (uint)m_TextureNormal_M.Size.Y;
                }
                else
                    throw new Exception("NormalImage wasn't loaded. Is the Tab section in " + configFileFilename + " complete?");

                // Check if optional textures were loaded
                if (m_TextureSelected_M.texture != null)
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Selected;
                }
            }

            // Recalculate the text size when auto sizing
            if (m_TextSize == 0)
                TextSize = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Tab ()
        {
            if (m_TextureNormal_L.texture != null)    Global.TextureManager.RemoveTexture(m_TextureNormal_L);
            if (m_TextureNormal_M.texture != null)    Global.TextureManager.RemoveTexture(m_TextureNormal_M);
            if (m_TextureNormal_R.texture != null)    Global.TextureManager.RemoveTexture(m_TextureNormal_R);

            if (m_TextureSelected_L.texture != null)  Global.TextureManager.RemoveTexture(m_TextureSelected_L);
            if (m_TextureSelected_M.texture != null)  Global.TextureManager.RemoveTexture(m_TextureSelected_M);
            if (m_TextureSelected_R.texture != null)  Global.TextureManager.RemoveTexture(m_TextureSelected_R);
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
                // Add the width of all the tabs together
                float width = 0;
                for (int i = 0; i < m_NameWidth.Count; ++i)
                {
                    if (m_SplitImage)
                        width += System.Math.Max ((m_MaximumTabWidth != 0) ? System.Math.Min(m_NameWidth[i] + (2 * m_DistanceToSide), m_MaximumTabWidth) : m_NameWidth[i] + (2 * m_DistanceToSide),
                                                  (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X) * (m_TabHeight / (float)(m_TextureNormal_M.Size.Y)));
                    else
                        width += (m_MaximumTabWidth != 0) ? System.Math.Max (m_NameWidth[i] + (2 * m_DistanceToSide), m_MaximumTabWidth) : m_NameWidth[i] + (2 * m_DistanceToSide);
                }

                return new Vector2f(width, m_TabHeight);
            }
            set
            {
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Adds a new tab.
        ///
        /// \param name    The name of the tab (this is the text that will be drawn on top of the tab).
        /// \param select  Do you want the new tab to be selected immediately?
        ///
        /// \return  The index of the tab in the list.
        ///
        /// \warning The index returned by this function may no longer be correct when a tab is removed.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint Add (string name, bool selectTab = true)
        {
            // Add the tab
            m_TabNames.Add(name);

            // Calculate the width of the tab
            m_Text.DisplayedString = name;
            m_NameWidth.Add(m_Text.GetLocalBounds().Width);

            // If the tab has to be selected then do so
            if (selectTab)
                m_SelectedTab = m_TabNames.Count - 1;

            // Return the index of the new tab
            return (uint)m_TabNames.Count - 1;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Selects the tab with a given name.
        ///
        /// \param name  The name of the tab to select.
        ///
        /// When the name doen't match any tab then nothing will be changed.
        /// If there are multiple tabs with the same name then the first one will be selected.
        ///
        /// \see select(int)
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Select (string name)
        {
            // Select the tab
            int index = m_TabNames.IndexOf (name);
            if (index != -1)
            {
                m_SelectedTab = index;
                return;
            }

            Internal.Output("TGUI warning: Failed to select the tab. The name didn't match any tab.");
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Selects the tab with a given index.
        ///
        /// \param index  The index of the tab to select.
        ///
        /// When the index is too high then nothing will happen.
        ///
        /// \see select(sf::String)
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Select (uint index)
        {
            // If the index is too big then do nothing
            if (index > m_TabNames.Count - 1)
            {
                Internal.Output("TGUI warning: Failed to select the tab. The index was too high.");
                return;
            }

            // Select the tab
            m_SelectedTab = (int)index;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Deselects the selected tab.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Deselect ()
        {
            m_SelectedTab = -1;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes a tab with a given name.
        ///
        /// \param name  The name of the tab to remove.
        ///
        /// When multiple tabs have the same name, only the first will be removed.
        ///
        /// \see remove(unsigned int)
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (string name)
        {
            // Remove the tab
            int index = m_TabNames.IndexOf (name);
            if (index != -1)
            {
                m_TabNames.RemoveAt(index);
                m_NameWidth.RemoveAt(index);

                // Check if the selected tab should change
                if (m_SelectedTab == index)
                    m_SelectedTab = -1;
                else if (m_SelectedTab > index)
                    --m_SelectedTab;

                return;
            }

            Internal.Output("TGUI warning: Failed to remove the tab. The name didn't match any tab.");
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes a tab with a given index.
        ///
        /// \param index  The index of the tab to remove.
        ///
        /// When the index is too high then nothing will happen.
        ///
        /// \see remove(sf::String)
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Remove (uint index)
        {
            // The index can't be too high
            if (index > m_TabNames.Count - 1)
            {
                Internal.Output("TGUI warning: Failed to remove the tab. The index was too high.");
                return;
            }

            // Remove the tab
            m_TabNames.RemoveAt((int)index);
            m_NameWidth.RemoveAt((int)index);

            // Check if the selected tab should change
            if (m_SelectedTab == (int)(index))
                m_SelectedTab = -1;
            else if (m_SelectedTab > (int)(index))
                --m_SelectedTab;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes all tabs.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAll ()
        {
            m_TabNames.Clear();
            m_NameWidth.Clear();
            m_SelectedTab = -1;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Get the name of the currently selected tab.
        ///
        /// \return The name of the tab.
        ///         When no tab is selected then this function returns an empty string.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string GetSelected ()
        {
            if (m_SelectedTab == -1)
                return "";
            else
                return m_TabNames[m_SelectedTab];
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Get the index of the currently selected tab.
        ///
        /// \return The index of the tab.
        ///         When no tab is selected then this function returns -1.
        ///
        /// \warning The index returned by this function may no longer be correct when a tab is removed.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetSelectedIndex ()
        {
            return m_SelectedTab;
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
        /// \brief Changes the character size of the text.
        ///
        /// \param size  The new size of the text.
        ///              If the size is 0 (default) then the text will be scaled to fit in the tab.
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
                // Change the text size
                m_TextSize = value;

                // Check if the text is auto sized
                if (m_TextSize == 0)
                {
                    // Calculate the text size
                    m_Text.DisplayedString = "kg";
                    m_Text.CharacterSize = (uint)(m_TabHeight * 0.85f);
                    m_Text.CharacterSize = (uint)(m_Text.CharacterSize - m_Text.GetLocalBounds().Top);
                }
                else // When the text has a fixed size
                {
                    // Set the text size
                    m_Text.CharacterSize = m_TextSize;
                }

                // Recalculate the name widths
                for (int i = 0; i < m_NameWidth.Count; ++i)
                {
                    m_Text.DisplayedString = m_TabNames[i];
                    m_NameWidth[i] = m_Text.GetLocalBounds().Width;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the height of the tabs.
        ///
        /// \param height  Height of the tabs
        ///
        /// By default, it is the height of the tab image that is loaded with the load function.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint TabHeight
        {
            get
            {
                return m_TabHeight;
            }
            set
            {
                // Make sure that the height changed
                if (m_TabHeight != value)
                {
                    m_TabHeight = value;

                    // Recalculate the size when auto sizing
                    if (m_TextSize == 0)
                        TextSize = 0;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the maximum tab width of the tabs.
        ///
        /// \param maximumWidth  Maximum width of a single tab
        ///
        /// If the text on the tab is longer than this width then it will be cropped to fit inside the tab.
        /// By default, the maximum width is 0 which means that there is no limitation.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint MaximumTabWidth
        {
            get
            {
                return m_MaximumTabWidth;
            }
            set
            {
                m_MaximumTabWidth = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the distance between the text and the side of the tab.
        ///
        /// \param distanceToSide  distance between the text and the side of the tab
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint DistanceToSide
        {
            get
            {
                return m_DistanceToSide;
            }
            set
            {
                m_DistanceToSide = value;
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

                m_TextureNormal_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureNormal_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureNormal_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureSelected_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureSelected_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureSelected_R.sprite.Color = new Color(255, 255, 255, m_Opacity);

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

            if (m_MouseHover)
                MouseLeftWidget();

            m_MouseHover = false;
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed (MouseButtonEventArgs e)
        {
            float width = Position.X;

            // Loop through all tabs
            for (int i = 0; i < m_NameWidth.Count; ++i)
            {
                // Append the width of the tab
                if (m_SplitImage)
                    width += System.Math.Max ((m_MaximumTabWidth != 0) ? System.Math.Min(m_NameWidth[i] + (2 * m_DistanceToSide), m_MaximumTabWidth) : m_NameWidth[i] + (2 * m_DistanceToSide),
                                              (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X) * (m_TabHeight / (float)(m_TextureNormal_M.Size.Y)));
                else
                    width += (m_MaximumTabWidth != 0) ? System.Math.Min (m_NameWidth[i] + (2 * m_DistanceToSide), m_MaximumTabWidth) : m_NameWidth [i] + (2 * m_DistanceToSide);

                // Check if the mouse went down on the tab
                if (e.X < width)
                {
                    // Select this tab
                    m_SelectedTab = i;

                    // Add the callback (if the user requested it)
                    if (TabChangedCallback != null)
                    {
                        m_Callback.Trigger = CallbackTrigger.TabChanged;
                        m_Callback.Value   = m_SelectedTab;
                        m_Callback.Text    = m_TabNames[i];
                        m_Callback.Mouse.X = (int)(e.X - Position.X);
                        m_Callback.Mouse.Y = (int)(e.Y - Position.Y);
                        TabChangedCallback (this, m_Callback);
                    }

                    // The tab was found
                    break;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // This function is called when the widget is added to a container.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void Initialize(Container parent)
        {
            base.Initialize(parent);
            m_Text.Font = Parent.GlobalFont;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // Draws the widget on the render target.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Get the old clipping area
            int[] scissor = new int[4];
            Gl.glGetIntegerv(Gl.GL_SCISSOR_BOX, scissor);

            // Calculate the scale factor of the view
            float scaleViewX = target.Size.X / target.GetView().Size.X;
            float scaleViewY = target.Size.Y / target.GetView().Size.Y;

            // Apply the transformations
            states.Transform *= Transform;

            float scalingY = m_TabHeight / (float)(m_TextureNormal_M.Size.Y);
            bool clippingRequired = false;
            uint tabWidth;
            FloatRect realRect;
            FloatRect defaultRect;
            Text tempText = new Text(m_Text);

            // Calculate the height and top of all strings
            tempText.DisplayedString = "kg";
            defaultRect = tempText.GetLocalBounds();

            // Loop through all tabs
            for (int i = 0; i < m_TabNames.Count; ++i)
            {
                // Find the tab height
                if (m_MaximumTabWidth != 0)
                {
                    if (m_MaximumTabWidth < m_NameWidth[i] + (2 * m_DistanceToSide))
                    {
                        tabWidth = m_MaximumTabWidth;
                        clippingRequired = true;
                    }
                    else
                        tabWidth = (uint)(m_NameWidth[i] + (2 * m_DistanceToSide));
                }
                else
                    tabWidth = (uint)(m_NameWidth[i] + (2 * m_DistanceToSide));

                // There is a minimum tab width
                if (tabWidth < 2 * m_DistanceToSide)
                    tabWidth = 2 * m_DistanceToSide;

                // Check if the image is split
                if (m_SplitImage)
                {
                    // There is another minimum when using SplitImage
                    float minimumWidth = (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X) * scalingY;
                    if (tabWidth < minimumWidth)
                        tabWidth = (uint)(minimumWidth);

                    // Set the scaling
                    states.Transform.Scale(scalingY, scalingY);

                    // Draw the left tab image
                    if (m_SeparateSelectedImage)
                    {
                        if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                            target.Draw(m_TextureSelected_L.sprite, states);
                        else
                            target.Draw(m_TextureNormal_L.sprite, states);
                    }
                    else // There is no separate selected image
                    {
                        target.Draw(m_TextureNormal_L.sprite, states);

                        if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                            target.Draw(m_TextureSelected_L.sprite, states);
                    }

                    // Check if the middle image may be drawn
                    if ((scalingY * (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X)) < tabWidth)
                    {
                        // Calculate the scale for our middle image
                        float scaleX = (tabWidth / (float)(m_TextureNormal_M.Size.X)) - (((m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X) * scalingY) / m_TextureNormal_M.Size.X);

                        // Put the middle image on the correct position
                        states.Transform.Translate((float)(m_TextureNormal_L.Size.X), 0);

                        // Set the scale for the middle image
                        states.Transform.Scale(scaleX / scalingY, 1);

                        // Draw the middle tab image
                        if (m_SeparateSelectedImage)
                        {
                            if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                                target.Draw(m_TextureSelected_M.sprite, states);
                            else
                                target.Draw(m_TextureNormal_M.sprite, states);
                        }
                        else // There is no separate selected image
                        {
                            target.Draw(m_TextureNormal_M.sprite, states);

                            if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                                target.Draw(m_TextureSelected_M.sprite, states);
                        }

                        // Put the right image on the correct position
                        states.Transform.Translate((float)(m_TextureNormal_M.Size.X), 0);

                        // Set the scale for the right image
                        states.Transform.Scale(scalingY / scaleX, 1);

                        // Draw the right tab image
                        if (m_SeparateSelectedImage)
                        {
                            if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                                target.Draw(m_TextureSelected_R.sprite, states);
                            else
                                target.Draw(m_TextureNormal_R.sprite, states);
                        }
                        else // There is no separate selected image
                        {
                            target.Draw(m_TextureNormal_R.sprite, states);

                            if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                                target.Draw(m_TextureSelected_R.sprite, states);
                        }

                        // Undo the translation
                        states.Transform.Translate(-(m_TextureNormal_L.Size.X + (m_TextureNormal_M.Size.X * scaleX / scalingY)), 0);
                    }
                    else // The edit box isn't width enough, we will draw it at minimum size
                    {
                        // Put the right image on the correct position
                        states.Transform.Translate((float)(m_TextureNormal_L.Size.X), 0);

                        // Draw the right tab image
                        if (m_SeparateSelectedImage)
                        {
                            if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                                target.Draw(m_TextureSelected_R.sprite, states);
                            else
                                target.Draw(m_TextureNormal_R.sprite, states);
                        }
                        else // There is no separate selected image
                        {
                            target.Draw(m_TextureNormal_R.sprite, states);

                            if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                                target.Draw(m_TextureSelected_R.sprite, states);
                        }

                        // Undo the translation
                        states.Transform.Translate(-(float)(m_TextureNormal_L.Size.X), 0);
                    }

                    // Undo the scaling of the split images
                    states.Transform.Scale(1.0f / scalingY, 1.0f / scalingY);
                }
                else // The image isn't split
                {
                    // Set the scaling
                    states.Transform.Scale(tabWidth / (float)(m_TextureNormal_M.Size.X), scalingY);

                    // Draw the tab image
                    if (m_SeparateSelectedImage)
                    {
                        if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                            target.Draw(m_TextureSelected_M.sprite, states);
                        else
                            target.Draw(m_TextureNormal_M.sprite, states);
                    }
                    else // There is no separate selected image
                    {
                        target.Draw(m_TextureNormal_M.sprite, states);

                        if ((m_SelectedTab == i) && ((m_WidgetPhase & (byte)WidgetPhase.Selected) != 0))
                            target.Draw(m_TextureSelected_M.sprite, states);
                    }

                    // Undo the scaling
                    states.Transform.Scale((float)(m_TextureNormal_M.Size.X) / tabWidth, 1.0f / scalingY);
                }

                // Draw the text
                {
                    // Get the current size of the text, so that we can recalculate the position
                    tempText.DisplayedString = m_TabNames [i];
                    realRect = tempText.GetLocalBounds();

                    // Calculate the new position for the text
                    if ((m_SplitImage) && (tabWidth == (m_TextureNormal_L.Size.X + m_TextureNormal_R.Size.X) * scalingY))
                        realRect.Left = ((tabWidth - realRect.Width) / 2.0f) - realRect.Left;
                    else
                        realRect.Left = m_DistanceToSide - realRect.Left;
                    realRect.Top = ((m_TabHeight - defaultRect.Height) / 2.0f) - defaultRect.Top;

                    // Move the text to the correct position
                    states.Transform.Translate((float)System.Math.Floor(realRect.Left + 0.5), (float)System.Math.Floor(realRect.Top + 0.5));

                    // Check if clipping is required for this text
                    if (clippingRequired)
                    {
                        // Get the global position
                        Vector2f topLeftPosition = states.Transform.TransformPoint((target.GetView().Size / 2.0f) - target.GetView().Center);
                        Vector2f bottomRightPosition = states.Transform.TransformPoint(new Vector2f(tabWidth - (2 * m_DistanceToSide), (m_TabHeight + defaultRect.Height) / 2.0f) - target.GetView().Center + (target.GetView().Size / 2.0f));

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
                        Gl.glScissor(scissorLeft, (int)(target.Size.Y - scissorBottom), scissorRight - scissorLeft, scissorBottom - scissorTop);
                    }

                    // Draw the text
                    target.Draw(tempText, states);

                    // Undo the translation of the text
                    states.Transform.Translate(-(float)System.Math.Floor(realRect.Left + 0.5f), -(float)System.Math.Floor(realRect.Top + 0.5f));

                    // Reset the old clipping area when needed
                    if (clippingRequired)
                    {
                        clippingRequired = false;
                        Gl.glScissor(scissor[0], scissor[1], scissor[2], scissor[3]);
                    }
                }

                // Set the next tab on the correct position
                states.Transform.Translate((float)(tabWidth), 0);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

