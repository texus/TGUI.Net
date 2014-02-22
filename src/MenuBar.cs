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
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using Tao.OpenGl;

namespace TGUI
{
    public class MenuBar : Widget
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MenuBar ()
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
        public MenuBar (MenuBar copy) : base(copy)
        {
            MenuItemClickedCallback   = copy.MenuItemClickedCallback;

            m_LoadedConfigFile        = copy.m_LoadedConfigFile;
            m_TextFont                = copy.m_TextFont;
            m_Size                    = copy.m_Size;
            m_TextSize                = copy.m_TextSize;
            m_DistanceToSide          = copy.m_DistanceToSide;
            m_MinimumMenuWidth        = copy.m_MinimumMenuWidth;
            m_BackgroundColor         = copy.m_BackgroundColor;
            m_TextColor               = copy.m_TextColor;
            m_SelectedBackgroundColor = copy.m_SelectedBackgroundColor;
            m_SelectedTextColor       = copy.m_SelectedTextColor;

            m_Menus = new List<Menu>();
            foreach (Menu menu in copy.m_Menus)
            {
                AddMenu(menu.text.DisplayedString);

                foreach (Text text in menu.menuItems)
                    AddMenuItem(menu.text.DisplayedString, text.DisplayedString);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a MenuBar section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MenuBar (string configFileFilename)
        {
            m_LoadedConfigFile = Global.ResourcePath + configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (m_LoadedConfigFile, "MenuBar");

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "backgroundcolor")
                    BackgroundColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "textcolor")
                    TextColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "selectedbackgroundcolor")
                    SelectedBackgroundColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "selectedtextcolor")
                    SelectedTextColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "distancetoside")
                    DistanceToSide = Convert.ToUInt32(configFile.Values [i]);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section MenuBar in " + m_LoadedConfigFile + ".");
            }
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
        /// Size of the menu bar
        /// </summary>
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
                TextSize = (uint)(m_Size.Y * 0.85);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adds a new menu
        /// </summary>
        ///
        /// <param name="text">The text written on the menu</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddMenu (string text)
        {
            Menu menu = new Menu();

            menu.menuItems = new List<Text> ();
            menu.selectedMenuItem = null;

            menu.text = new Text (text, m_TextFont);
            menu.text.Color = m_TextColor;
            menu.text.CharacterSize = m_TextSize;
            menu.text.CharacterSize = (uint)(menu.text.CharacterSize - menu.text.GetLocalBounds().Top);

            m_Menus.Add (menu);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adds a new menu item
        /// </summary>
        ///
        /// <param name="menuText">The name of the menu to which the menu item will be added</param>
        /// <param name="text">The text written on this menu item</param>
        ///
        /// <returns>True when the item was added, false when \a menu was not found</returns>
        ///
        /// Usage example:
        /// <code>
        /// menuBar.AddMenu("File");
        /// menuBar.AddMenuItem("File", "Load");
        /// menuBar.AddMenuItem("File", "Save");
        /// </code>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool AddMenuItem (string menuText, string text)
        {
            // Search for the menu
            foreach (Menu menu in m_Menus)
            {
                // If this is the menu then add the menu item to it
                if (menu.text.DisplayedString == menuText)
                {
                    Text menuItem = new Text (text, m_TextFont);
                    menuItem.Color = m_TextColor;
                    menuItem.CharacterSize = m_TextSize;
                    menuItem.CharacterSize = (uint)(menuItem.CharacterSize - menuItem.GetLocalBounds().Top);

                    menu.menuItems.Add (menuItem);
                    return true;
                }
            }

            // Couldn't find the menu
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes a menu and all its menu items
        /// </summary>
        ///
        /// <param name="menuText">The name of the menu to remove</param>
        ///
        /// <returns>True when the menu was removed, false when \a menu was not found</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RemoveMenu (string menuText)
        {
            // Search for the menu
            foreach (Menu menu in m_Menus)
            {
                // If this is the menu then remove it
                if (menu.text.DisplayedString == menuText)
                {
                    m_Menus.Remove(menu);

                    // The menu was removed, so it can't remain open
                    if (m_VisibleMenu == menu)
                        m_VisibleMenu = null;

                    return true;
                }
            }

            // Couldn't find the menu
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the menu item
        /// </summary>
        ///
        /// <param name="menuText">The name of the menu in which the menu item is located</param>
        /// <param name="menuItemText">The name of the menu item to remove</param>
        ///
        /// <returns>True when the item was removed, false when menuText or menuItemText was not found</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RemoveMenuItem (string menuText, string menuItemText)
        {
            // Search for the menu
            foreach (Menu menu in m_Menus)
            {
                // If this is the menu then search for the menu item
                if (menu.text.DisplayedString == menuText)
                {
                    foreach (Text menuItem in menu.menuItems)
                    {
                        // If this is the menu item then remove it
                        if (menuItem.DisplayedString == menuItemText)
                        {
                            menu.menuItems.Remove(menuItem);

                            // The item can't still be selected
                            if (menu.selectedMenuItem == menuItem)
                                menu.selectedMenuItem = null;

                            return true;
                        }
                    }
                }
            }

            // Couldn't find the menu
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes all menus
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAllMenus ()
        {
            m_Menus.Clear ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Background color of the menu bar
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color BackgroundColor
        {
            get
            {
                return m_BackgroundColor;
            }
            set
            {
                m_BackgroundColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Text color of the menus and menu items in the menu bar
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color TextColor
        {
            get
            {
                return m_TextColor;
            }
            set
            {
                m_TextColor = value;

                foreach (Menu menu in m_Menus)
                {
                    foreach (Text text in menu.menuItems)
                    {
                        if (menu.selectedMenuItem != text)
                            text.Color = value;
                    }

                    menu.text.Color = value;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The background color of the selected menu item
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color SelectedBackgroundColor
        {
            get
            {
                return m_SelectedBackgroundColor;
            }
            set
            {
                m_SelectedBackgroundColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The text color of the selected menu item
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color SelectedTextColor
        {
            get
            {
                return m_SelectedTextColor;
            }
            set
            {
                m_SelectedTextColor = value;

                if (m_VisibleMenu != null)
                {
                    if (m_VisibleMenu.selectedMenuItem != null)
                        m_VisibleMenu.selectedMenuItem.Color = value;
                }
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
                return m_TextFont;
            }
            set
            {
                m_TextFont = value;

                foreach (Menu menu in m_Menus)
                {
                    foreach (Text text in menu.menuItems)
                    {
                        text.Font = value;
                    }

                    menu.text.Font = value;
                }

                TextSize = m_TextSize;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Character size of the text.
        /// If the size is 0 (default) then the text will be scaled to fit in the menu bar.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint TextSize
        {
            get
            {
                return m_TextSize;
            }
            set
            {
                m_TextSize = value;

                foreach (Menu menu in m_Menus)
                {
                    foreach (Text text in menu.menuItems)
                    {
                        text.CharacterSize = m_TextSize;
                        text.CharacterSize = (uint)(text.CharacterSize - text.GetLocalBounds().Top);
                    }

                    menu.text.CharacterSize = m_TextSize;
                    menu.text.CharacterSize = (uint)(menu.text.CharacterSize - menu.text.GetLocalBounds().Top);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Distance between the text and the side of the menu item
        /// </summary>
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
        /// <summary>
        /// Minimum width of the menus.
        /// </summary>
        ///
        /// When a menu is displayed, the width will be either this or the width of the longest text in the menu.
        /// The default minimum width is 125 pixels.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint MinimumMenuWidth
        {
            get
            {
                return m_MinimumMenuWidth;
            }
            set
            {
                m_MinimumMenuWidth = value;
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
            // Check if the mouse is on top of the menu bar
            if (Transform.TransformRect(new FloatRect(0, 0, m_Size.X, m_Size.Y)).Contains(x, y))
                return true;
            else
            {
                // Check if there is a menu open
                if (m_VisibleMenu != null)
                {
                    // Search the left position of the open menu
                    float left = 0;
                    int index = m_Menus.IndexOf (m_VisibleMenu);
                    for (int i = 0; i < index; ++i)
                        left += m_Menus[i].text.GetLocalBounds().Width + (2 * m_DistanceToSide);

                    // Find out what the width of the menu should be
                    float width = 0;
                    for (int j = 0; j < m_VisibleMenu.menuItems.Count; ++j)
                    {
                        if (width < m_VisibleMenu.menuItems[j].GetLocalBounds().Width + (3 * m_DistanceToSide))
                            width = m_VisibleMenu.menuItems[j].GetLocalBounds().Width + (3 * m_DistanceToSide);
                    }

                    // There is a minimum width
                    if (width < m_MinimumMenuWidth)
                        width = m_MinimumMenuWidth;

                    // Check if the mouse is on top of the open menu
                    if (Transform.TransformRect(new FloatRect(left, m_Size.Y, width, m_Size.Y * m_VisibleMenu.menuItems.Count)).Contains(x, y))
                        return true;
                }
            }

            if (m_MouseHover)
                MouseLeftWidget ();

            m_MouseHover = false;
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been pressed on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed (MouseButtonEventArgs e)
        {
            // Check if the mouse is on top of the menu bar (not on an open menus)
            if (e.Y <= m_Size.Y + Position.Y)
            {
                // Loop through the menus to check if the mouse is on top of them
                float menuWidth = 0;
                for (int i = 0; i < m_Menus.Count; ++i)
                {
                    menuWidth += m_Menus[i].text.GetLocalBounds().Width + (2 * m_DistanceToSide);
                    if (e.X < menuWidth)
                    {
                        // Close the menu when it was already open
                        if (m_VisibleMenu == m_Menus[i])
                        {
                            if (m_VisibleMenu.selectedMenuItem != null)
                            {
                                m_VisibleMenu.selectedMenuItem.Color = m_TextColor;
                                m_VisibleMenu.selectedMenuItem = null;
                            }

                            m_VisibleMenu = null;
                        }

                        // If this menu can be opened then do so
                        else if (m_Menus[i].menuItems.Count != 0)
                            m_VisibleMenu = m_Menus[i];

                        break;
                    }
                }
            }

            m_MouseDown = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            if (m_MouseDown)
            {
                // Check if the mouse is on top of one of the menus
                if (e.Y > m_Size.Y + Position.Y)
                {
                    int selectedMenuItem = (int)System.Math.Floor((e.Y - m_Size.Y - Position.Y) / m_Size.Y);

                    if (selectedMenuItem < m_VisibleMenu.menuItems.Count)
                    {
                        if (MenuItemClickedCallback != null)
                        {
                            m_Callback.Trigger = CallbackTrigger.MenuItemClicked;
                            m_Callback.Text = m_VisibleMenu.selectedMenuItem.DisplayedString;
                            m_Callback.Index = (uint)m_Menus.IndexOf(m_VisibleMenu);
                            MenuItemClickedCallback (this, m_Callback);
                        }

                        if (m_VisibleMenu != null)
                        {
                            if (m_VisibleMenu.selectedMenuItem != null)
                            {
                                m_VisibleMenu.selectedMenuItem.Color = m_TextColor;
                                m_VisibleMenu.selectedMenuItem = null;
                            }

                            m_VisibleMenu = null;
                        }
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse has moved on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved (MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget ();

            m_MouseHover = true;

            // Check if the mouse is on top of the menu bar (not on an open menus)
            if (e.Y <= m_Size.Y + Position.Y)
            {
                // Loop through the menus to check if the mouse is on top of them
                float menuWidth = 0;
                foreach (Menu menu in m_Menus)
                {
                    menuWidth += menu.text.GetLocalBounds().Width + (2 * m_DistanceToSide);
                    if (e.X < menuWidth)
                    {
                        // Check if the menu is already open
                        if (m_VisibleMenu == menu)
                        {
                            // If one of the menu items is selected then unselect it
                            if (m_VisibleMenu.selectedMenuItem != null)
                            {
                                m_VisibleMenu.selectedMenuItem.Color = m_TextColor;
                                m_VisibleMenu.selectedMenuItem = null;
                            }
                        }
                        else // The menu isn't open yet
                        {
                            // If there is another menu open then close it first
                            if (m_VisibleMenu != null)
                            {
                                // If an item in that other menu was selected then unselect it first
                                if (m_VisibleMenu.selectedMenuItem != null)
                                {
                                    m_VisibleMenu.selectedMenuItem.Color = m_TextColor;
                                    m_VisibleMenu.selectedMenuItem = null;
                                }

                                m_VisibleMenu = null;
                            }

                            // If this menu can be opened then do so
                            if (menu.menuItems.Count != 0)
                                m_VisibleMenu = menu;
                        }
                        break;
                    }
                }
            }
            else // The mouse is on top of one of the menus
            {
                // Calculate on what menu item the mouse is located
                int selectedMenuItem = (int)System.Math.Floor((e.Y - m_Size.Y - Position.Y) / m_Size.Y);

                // Check if the mouse is on a different item than before
                if (selectedMenuItem != m_VisibleMenu.menuItems.IndexOf (m_VisibleMenu.selectedMenuItem))
                {
                    // If another of the menu items is selected then unselect it
                    if (m_VisibleMenu.selectedMenuItem != null)
                        m_VisibleMenu.selectedMenuItem.Color = m_TextColor;

                    // Mark the item below the mouse as selected
                    m_VisibleMenu.selectedMenuItem = m_VisibleMenu.menuItems[selectedMenuItem];
                    m_VisibleMenu.selectedMenuItem.Color = m_SelectedTextColor;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNoLongerDown()
        {
            // Check if there is still a menu open
            if (m_VisibleMenu != null)
            {
                // If an item in that menu was selected then unselect it first
                if (m_VisibleMenu.selectedMenuItem != null)
                {
                    m_VisibleMenu.selectedMenuItem.Color = m_TextColor;
                    m_VisibleMenu.selectedMenuItem = null;
                }

                m_VisibleMenu = null;
            }
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
            TextFont = parent.GlobalFont;

            m_Size.X = m_Parent.Size.X;
            m_Size.Y = 20;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            // Draw the background
            RectangleShape background = new RectangleShape(m_Size);
            background.FillColor = m_BackgroundColor;
            target.Draw(background, states);

            // Draw the menus
            for (int i = 0; i < m_Menus.Count; ++i)
            {
                states.Transform.Translate(m_DistanceToSide, 0);
                target.Draw(m_Menus[i].text, states);

                // Is the menu open?
                if (m_VisibleMenu == m_Menus[i])
                {
                    states.Transform.Translate(-(float)(m_DistanceToSide), m_Size.Y);

                    // Find out what the width of the menu should be
                    float menuWidth = 0;
                    for (int j = 0; j < m_Menus[i].menuItems.Count; ++j)
                    {
                        if (menuWidth < m_Menus[i].menuItems[j].GetLocalBounds().Width + (3 * m_DistanceToSide))
                            menuWidth = m_Menus[i].menuItems[j].GetLocalBounds().Width + (3 * m_DistanceToSide);
                    }

                    // There is a minimum width
                    if (menuWidth < m_MinimumMenuWidth)
                        menuWidth = m_MinimumMenuWidth;

                    // Draw the background of the menu
                    background = new RectangleShape(new Vector2f(menuWidth, m_Size.Y * m_Menus[i].menuItems.Count));
                    background.FillColor = m_BackgroundColor;
                    target.Draw(background, states);

                    // If there is a selected menu item then draw its background
                    if (m_Menus[i].selectedMenuItem != null)
                    {
                        states.Transform.Translate(0, m_Menus[i].menuItems.IndexOf(m_Menus[i].selectedMenuItem) * m_Size.Y);
                        background = new RectangleShape(new Vector2f(menuWidth, m_Size.Y));
                        background.FillColor = m_SelectedBackgroundColor;
                        target.Draw(background, states);
                        states.Transform.Translate(0, m_Menus[i].menuItems.IndexOf(m_Menus[i].selectedMenuItem) * -m_Size.Y);
                    }

                    states.Transform.Translate(2 * m_DistanceToSide, 0);

                    // Draw the menu items
                    for (int j = 0; j < m_Menus[i].menuItems.Count; ++j)
                    {
                        target.Draw(m_Menus[i].menuItems[j], states);
                        states.Transform.Translate(0, m_Size.Y);
                    }

                    states.Transform.Translate(m_Menus[i].text.GetLocalBounds().Width, -m_Size.Y * (m_Menus[i].menuItems.Count + 1));
                }
                else // The menu isn't open
                {
                    states.Transform.Translate(m_Menus[i].text.GetLocalBounds().Width + m_DistanceToSide, 0);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the MenuItemClicked event</summary>
        public event EventHandler<CallbackArgs> MenuItemClickedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private class Menu
        {
            public Text text;
            public List<Text> menuItems;
            public Text selectedMenuItem;
        };

        private string   m_LoadedConfigFile = "";

        private List<Menu> m_Menus = new List<Menu>();

        private Menu     m_VisibleMenu = null;

        private Font     m_TextFont = null;

        private Vector2f m_Size = new Vector2f(0, 20);

        private uint     m_TextSize = 0;

        private uint     m_DistanceToSide = 0;

        private uint     m_MinimumMenuWidth = 100;

        private Color    m_BackgroundColor = new Color(255, 255, 255);
        private Color    m_TextColor = new Color(0, 0, 0);
        private Color    m_SelectedBackgroundColor = new Color(50, 100, 200);
        private Color    m_SelectedTextColor = new Color(255, 255, 255);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
