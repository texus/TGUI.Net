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
    public class ListBox : Widget, WidgetBorders
    {
        public event EventHandler<CallbackArgs> ItemSelectedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string m_LoadedConfigFile = "";

        // This contains the different items in the list box
        private List<string> m_Items = new List<string>();

        // What is the index of the selected item?
        private int m_SelectedItem = -1;

        // The size must be stored
        private Vector2f m_Size = new Vector2f(50, 100);
        private uint m_ItemHeight = 24;
        private uint m_TextSize = 19;

        // This will store the maximum number of items in the list box (zero by default, meaning that there is no limit)
        private uint m_MaxItems = 0;

        // When there are too many items a scrollbar will be shown
        internal Scrollbar m_Scroll = null;

        // These colors are used to draw the list box
        private Color m_BackgroundColor = new Color(255, 255, 255);
        private Color m_TextColor = new Color(0, 0, 0);
        private Color m_SelectedBackgroundColor = new Color(50, 100, 200);
        private Color m_SelectedTextColor = new Color(255, 255, 255);
        private Color m_BorderColor = new Color(0, 0, 0);

        // The font used to draw the text
        private Font m_TextFont = null;

        private Borders m_Borders = new Borders();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Default constructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ListBox ()
        {
            m_DraggableWidget = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ListBox (ListBox copy) : base(copy)
        {
            ItemSelectedCallback = copy.ItemSelectedCallback;

            m_LoadedConfigFile        = copy.m_LoadedConfigFile;
            m_Items                   = copy.m_Items;
            m_SelectedItem            = copy.m_SelectedItem;
            m_Size                    = copy.m_Size;
            m_ItemHeight              = copy.m_ItemHeight;
            m_TextSize                = copy.m_TextSize;
            m_MaxItems                = copy.m_MaxItems;
            m_BackgroundColor         = copy.m_BackgroundColor;
            m_TextColor               = copy.m_TextColor;
            m_SelectedBackgroundColor = copy.m_SelectedBackgroundColor;
            m_SelectedTextColor       = copy.m_SelectedTextColor;
            m_BorderColor             = copy.m_BorderColor;
            m_TextFont                = copy.m_TextFont;
            m_Borders                 = copy.m_Borders;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads the widget.
        ///
        /// \param configFileFilename  Filename of the config file.
        ///
        /// The config file must contain a ListBox section with the needed information.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ListBox (string configFileFilename)
        {
            m_DraggableWidget = true;

            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "ListBox");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

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
                else if (configFile.Properties[i] == "bordercolor")
                    BorderColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "borders")
                {
                    Borders borders;
                    if (Internal.ExtractBorders(configFile.Values [i], out borders))
                        Borders = borders;
                }
                else if (configFile.Properties[i] == "scrollbar")
                {
                    if ((configFile.Values[i].Length < 3) || (configFile.Values[i][0] != '"') || (configFile.Values[i][configFile.Values[i].Length-1] != '"'))
                        throw new Exception("Failed to parse value for Scrollbar in section ChatBox in " + configFileFilename + ".");

                    // load the scrollbar
                    m_Scroll = new Scrollbar(configFileFolder + (configFile.Values[i]).Substring(1, configFile.Values[i].Length - 2));
                    m_Scroll.VerticalScroll = true;
                    m_Scroll.Size = new Vector2f(m_Scroll.Size.X, m_Size.Y - m_Borders.Top - m_Borders.Bottom);
                    m_Scroll.LowValue = (int)(m_Size.Y - m_Borders.Top - m_Borders.Bottom);
                    m_Scroll.Maximum = (int)(m_Items.Count * m_ItemHeight);
                }
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section ListBox in " + configFileFilename + ".");
            }
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

                // There is a minimum width
                if (m_Scroll == null)
                    m_Size.X = System.Math.Max(50.0f + m_Borders.Left + m_Borders.Right, m_Size.X);
                else
                    m_Size.X = System.Math.Max(50.0f + m_Borders.Left + m_Borders.Right + m_Scroll.Size.X, m_Size.X);

                // There is also a minimum list box height
                if (m_Size.Y < (m_ItemHeight + m_Borders.Top + m_Borders.Bottom))
                    m_Size.Y = m_ItemHeight + m_Borders.Top + m_Borders.Bottom;

                // If there is a scrollbar then reinitialize it
                if (m_Scroll != null)
                {
                    m_Scroll.Size = new Vector2f(m_Scroll.Size.X, m_Size.Y - m_Borders.Top - m_Borders.Bottom);
                    m_Scroll.LowValue = (int)(m_Size.Y - m_Borders.Top - m_Borders.Bottom);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the background color.
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
        /// \brief Changes/Returns the color of the text.
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
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Set the background color of the selected text that will be used inside the list box.
        ///
        /// \param selectedBackgroundColor  The color of the background of the selected item
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
        /// \brief Set the text color of the selected text that will be used inside the list box.
        ///
        /// \param selectedTextColor  The color of the text when it is selected
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
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the color of the borders.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
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
                return m_TextFont;
            }
            set
            {
                m_TextFont = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Adds an item to the list.
        ///
        /// \param itemName  The name of the item you want to add (this is the text that will be displayed inside the list box)
        ///
        /// \return
        ///         -  The index of the item when it was successfully added.
        ///         -  -1 when the list box is full (you have set a maximum item limit and you are trying to add more items)
        ///         -  -1 when there is no scrollbar and you try to have more items than fit inside the list box
        ///
        /// \warning The index returned by this function is no longer correct when an item is removed.
        ///
        /// \see setMaximumItems
        /// \see setScrollbar
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int AddItem (string itemName)
        {
            // Check if the item limit is reached (if there is one)
            if ((m_MaxItems == 0) || (m_Items.Count < m_MaxItems))
            {
                // If there is no scrollbar then there is another limit
                if (m_Scroll == null)
                {
                    // Calculate the amount of items that fit in the list box
                    uint maximumItems = ((uint)m_Size.Y - m_Borders.Top - m_Borders.Bottom) / m_ItemHeight;

                    // Check if the item still fits in the list box
                    if (m_Items.Count == maximumItems)
                        return -1;
                }

                // Add the item to the list
                m_Items.Add(itemName);

                // If there is a scrollbar then tell it that another item was added
                if (m_Scroll != null)
                    m_Scroll.Maximum = (int)(m_Items.Count * m_ItemHeight);

                // Return the item index
                return m_Items.Count - 1;
            }
            else // The item limit was reached
                return -1;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Selects an item in the list box.
        ///
        /// When adding items to the list box with the addItem function, none of them will be selected.
        /// If you want a default item selected then you can use this function to select it.
        /// The first item that matches the name will be selected.
        ///
        /// \param itemName  The item you want to select
        ///
        /// \return
        ///         - true on success
        ///         - false when none of the items matches the name
        ///
        /// \see setSelectedItem(unsigned int)
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool SetSelectedItem (string itemName)
        {
            // Select the item
            int index = m_Items.IndexOf (itemName);
            if (index != -1)
            {
                m_SelectedItem = index;
                return true;
            }

            Internal.Output("TGUI warning: Failed to select the item in the list box. The name didn't match any item.");

            // No match was found
            m_SelectedItem = -1;
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Selects an item in the list box.
        ///
        /// When adding items to the list box with the addItem function, none of them will be selected.
        /// If you want a default item selected then you can use this function to select it.
        /// If the index is -1 then the \a deselectItem function will be called.
        ///
        /// \param index  The index of the item
        ///
        /// \return
        ///         - true on success
        ///         - false when the index was too high
        ///
        /// \see setSelectedItem(sf::String)
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool SetSelectedItem (int index)
        {
            if (index < 0)
            {
                DeselectItem();
                return true;
            }

            // If the index is too high then deselect the items
            if (index > m_Items.Count - 1)
            {
                Internal.Output("TGUI warning: Failed to select the item in the list box. The index was too high.");
                m_SelectedItem = -1;
                return false;
            }

            // Select the item
            m_SelectedItem = index;
            return true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Deselects the selected item.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void DeselectItem ()
        {
            m_SelectedItem = -1;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes an item from the list with a given index.
        ///
        /// \param index  The index of the item to remove
        ///
        /// \return
        ///        - true when the item was removed
        ///        - false when the index was too high
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RemoveItem (uint index)
        {
            // The index can't be too high
            if (index > m_Items.Count - 1)
            {
                Internal.Output("TGUI warning: Failed to remove the item from the list box. The index was too high.");
                return false;
            }

            // Remove the item
            m_Items.RemoveAt((int)index);

            // If there is a scrollbar then tell it that an item was removed
            if (m_Scroll != null)
                m_Scroll.Maximum = (int)(m_Items.Count * m_ItemHeight);

            // Check if the selected item should change
            if (m_SelectedItem == (int)index)
                m_SelectedItem = -1;
            else if (m_SelectedItem > (int)index)
                --m_SelectedItem;

            return true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes the first item from the list with the given name.
        ///
        /// \param itemName  The item to remove
        ///
        /// \return
        ///        - true when the item was removed
        ///        - false when the name didn't match any item
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RemoveItem (string itemName)
        {
            // Remove the item
            int index = m_Items.IndexOf (itemName);
            if (index != -1)
            {
                m_Items.RemoveAt(index);

                // Check if the selected item should change
                if (m_SelectedItem == index)
                    m_SelectedItem = -1;
                else if (m_SelectedItem > index)
                    --m_SelectedItem;

                // If there is a scrollbar then tell it that an item was removed
                if (m_Scroll != null)
                    m_Scroll.Maximum = (int)(m_Items.Count * m_ItemHeight);

                return true;
            }

            Internal.Output("TGUI warning: Failed to remove the item from the list box. The name didn't match any item.");
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes all items from the list.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAllItems ()
        {
            // Clear the list, remove all items
            m_Items.Clear();

            // Unselect any selected item
            m_SelectedItem = -1;

            // If there is a scrollbar then tell it that all item were removed
            if (m_Scroll != null)
                m_Scroll.Maximum = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns the item name of the item with the given index.
        ///
        /// \param index  The index of the item
        ///
        /// \return The requested item.
        ///         The string will be empty when the index was too high.
        ///
        /// \see getItemIndex
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string GetItem (uint index)
        {
            // The index can't be too high
            if (index > m_Items.Count - 1)
            {
                Internal.Output("TGUI warning: The index of the item was too high. Returning an empty string.");
                return "";
            }

            // Return the item
            return m_Items[(int)index];
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns the index of the first item with the given name.
        ///
        /// \param itemName  The name of the item
        ///
        /// \return The index of the item that matches the name.
        ///         If none of the items matches then the returned index will be -1.
        ///
        /// \warning This index may become wrong when an item is removed from the list.
        ///
        /// \see getItem
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetItemIndex (string itemName)
        {
            return m_Items.IndexOf (itemName);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns the list of all the items.
        ///
        /// \return The vector of strings
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<string> GetItems ()
        {
            return m_Items;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns the currently selected item.
        ///
        /// \return The selected item.
        ///         When no item was selected then this function will return an empty string.
        ///
        /// \see getSelectedItemIndex
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string GetSelectedItem ()
        {
            if (m_SelectedItem == -1)
                return "";
            else
                return m_Items[m_SelectedItem];
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Get the index of the selected item.
        ///
        /// \return The index of the selected item
        ///         When no item was selected then this function returns -1.
        ///
        /// \warning This index may become wrong when an item is removed from the list.
        ///
        /// \see getSelectedItem
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetSelectedItemIndex ()
        {
            return m_SelectedItem;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the scrollbar of the list box.
        ///
        /// \param scrollbarConfigFileFilename  Filename of the config file.
        ///                                     The config file must contain a Scrollbar section with the needed information.
        ///
        /// \return
        ///        - true when the scrollbar was successfully loaded
        ///        - false when the loading of the scrollbar failed
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetScrollbar (string scrollbarConfigFileFilename)
        {
            // Calling setScrollbar with an empty string does the same as removeScrollbar
            if (scrollbarConfigFileFilename.Length == 0)
            {
                RemoveScrollbar();
                return;
            }

            // load the scrollbar
            m_Scroll = new Scrollbar(scrollbarConfigFileFilename);
            m_Scroll.VerticalScroll = true;
            m_Scroll.Size = new Vector2f(m_Scroll.Size.X, m_Size.Y - m_Borders.Top - m_Borders.Bottom);
            m_Scroll.LowValue = (int)(m_Size.Y - m_Borders.Top - m_Borders.Bottom);
            m_Scroll.Maximum = (int)(m_Items.Count * m_ItemHeight);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes the scrollbar from the list box.
        ///
        /// When there are too many items to fit in the list box then the items will be removed.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveScrollbar ()
        {
            m_Scroll = null;

            // When the items no longer fit inside the list box then we need to remove some
            if ((m_Items.Count * m_ItemHeight) > m_Size.Y)
            {
                // Calculate ho many items fit inside the list box
                m_MaxItems = (uint)System.Math.Floor(m_Size.Y / m_ItemHeight);

                // Remove the items that didn't fit inside the list box
                m_Items.RemoveRange((int)m_MaxItems, (int)(m_Items.Count - m_MaxItems));
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the height of the items in the list box.
        ///
        /// \param itemHeight  The size of a single item in the list
        ///
        /// \remarks
        ///         - This size is always a little big greater than the text size.
        ///         - When there is no scrollbar then the items will be removed when they no longer fit inside the list box.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint ItemHeight
        {
            get
            {
                return m_ItemHeight;
            }
            set
            {
                m_ItemHeight = value;

                // There is a minimum height
                if (m_ItemHeight < 10)
                    m_ItemHeight = 10;

                m_TextSize = (uint)(m_ItemHeight * 0.8f);

                // Some items might be removed when there is no scrollbar
                if (m_Scroll == null)
                {
                    // When the items no longer fit inside the list box then we need to remove some
                    if ((m_Items.Count * m_ItemHeight) > m_Size.Y)
                    {
                        // Calculate ho many items fit inside the list box
                        m_MaxItems = (uint)System.Math.Floor(m_Size.Y / m_ItemHeight);

                        // Remove the items that didn't fit inside the list box
                        m_Items.RemoveRange((int)m_MaxItems, (int)(m_Items.Count - m_MaxItems));
                    }
                }
                else // There is a scrollbar
                {
                    // Set the maximum of the scrollbar
                    m_Scroll.Maximum = (int)(m_Items.Count * m_ItemHeight);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes the maximum items that the list box can contain.
        ///
        /// \param maximumItems  The maximum items inside the list box.
        ///                      When the maximum is set to 0 then the limit will be disabled.
        ///
        /// If no scrollbar was loaded then there is always a limitation because there will be a limited space for the items.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint MaximumItems
        {
            get
            {
                return m_MaxItems;
            }
            set
            {
                m_MaxItems = value;

                // Check if we already passed the limit
                if ((m_MaxItems > 0) && (m_MaxItems < m_Items.Count))
                {
                    // Remove the items that passed the limitation
                    m_Items.RemoveRange((int)m_MaxItems, (int)(m_Items.Count - m_MaxItems));

                    // If there is a scrollbar then tell it that the number of items was changed
                    if (m_Scroll != null)
                        m_Scroll.Maximum = (int)(m_Items.Count * m_ItemHeight);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Changes/Returns the border width and border height of the list box.
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

                // There is a minimum width
                if (m_Size.X < (50 + m_Borders.Left + m_Borders.Right))
                    m_Size.X = 50 + m_Borders.Left + m_Borders.Right;

                // There is also a minimum height (when there is no scrollbar)
                if (m_Scroll == null)
                {
                    // If there are items then they should still fit inside the list box
                    if (m_Items.Count > 0)
                    {
                        if (m_Size.Y < ((m_Items.Count * m_ItemHeight) - m_Borders.Top - m_Borders.Bottom))
                            m_Size.Y = (m_Items.Count * m_ItemHeight) - m_Borders.Top - m_Borders.Bottom;
                    }
                    else // There are no items
                    {
                        // At least one item should fit inside the list box
                        if (m_Size.Y < (m_ItemHeight - m_Borders.Top - m_Borders.Bottom))
                            m_Size.Y = m_ItemHeight - m_Borders.Top - m_Borders.Bottom;
                    }
                }
                else // There is a scrollbar, so reinitialize it
                {
                    m_Scroll.Size = new Vector2f(m_Scroll.Size.X, m_Size.Y - m_Borders.Top - m_Borders.Bottom);
                    m_Scroll.LowValue = (int)(m_Size.Y - m_Borders.Top - m_Borders.Bottom);
                }
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

                if (m_Scroll != null)
                    m_Scroll.Transparency = m_Opacity;

                m_BackgroundColor.A = m_Opacity;
                m_TextColor.A = m_Opacity;
                m_SelectedBackgroundColor.A = m_Opacity;
                m_SelectedTextColor.A = m_Opacity;
                m_BorderColor.A = m_Opacity;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override bool MouseOnWidget(float x, float y)
        {
            // Pass the event to the scrollbar (if there is one)
            if (m_Scroll != null)
            {
                // Temporarily set the position of the scroll
                m_Scroll.Position = new Vector2f(Position.X + m_Size.X - m_Borders.Right - m_Scroll.Size.X,
                                                 Position.Y + m_Borders.Top);

                // Pass the event
                m_Scroll.MouseOnWidget(x, y);

                // Reset the position
                m_Scroll.Position = new Vector2f(0, 0);
            }

            // Check if the mouse is on top of the list box
            if (Transform.TransformRect(new FloatRect(m_Borders.Left, m_Borders.Top, m_Size.X - m_Borders.Left - m_Borders.Right, m_Size.Y - m_Borders.Top - m_Borders.Bottom)).Contains(x, y))
                return true;
            else // The mouse is not on top of the list box
            {
                if (m_MouseHover)
                    MouseLeftWidget();

                m_MouseHover = false;
                return false;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed(MouseButtonEventArgs e)
        {
            // Set the mouse down flag to true
            m_MouseDown = true;

            // This will be true when the click didn't occur on the scrollbar
            bool clickedOnListBox = true;

            // If there is a scrollbar then pass the event
            if (m_Scroll != null)
            {
                // Temporarily set the position of the scroll
                m_Scroll.Position = new Vector2f(Position.X + m_Size.X - m_Borders.Right - m_Scroll.Size.X, Position.Y + m_Borders.Top);

                // Pass the event
                if (m_Scroll.MouseOnWidget(e.X, e.Y))
                {
                    m_Scroll.OnLeftMousePressed(e);
                    clickedOnListBox = false;
                }

                // Reset the position
                m_Scroll.Position = new Vector2f(0, 0);
            }

            // If the click occured on the list box
            if (clickedOnListBox)
            {
                // Remember the old selected item
                int oldSelectedItem = m_SelectedItem;

                // Check if there is a scrollbar or whether it is hidden
                if ((m_Scroll != null) && (m_Scroll.LowValue < m_Scroll.Maximum))
                {
                    // Check if we clicked on the first (perhaps partially) visible item
                    if (e.Y - Position.Y - m_Borders.Top <= (m_ItemHeight - (m_Scroll.Value % m_ItemHeight)))
                    {
                        // We clicked on the first visible item
                        m_SelectedItem = (int)(m_Scroll.Value / m_ItemHeight);
                    }
                    else // We didn't click on the first visible item
                    {
                        // Calculate on what item we clicked
                        if ((m_Scroll.Value % m_ItemHeight) == 0)
                            m_SelectedItem = (int)((e.Y - Position.Y - m_Borders.Top) / m_ItemHeight + (m_Scroll.Value / m_ItemHeight));
                        else
                            m_SelectedItem = (int)((((e.Y - Position.Y - m_Borders.Top) - (m_ItemHeight - (m_Scroll.Value % m_ItemHeight))) / m_ItemHeight) + (m_Scroll.Value / m_ItemHeight) + 1);
                    }
                }
                else // There is no scrollbar or it is not displayed
                {
                    // Calculate on which item we clicked
                    m_SelectedItem = (int)((e.Y - Position.Y - m_Borders.Top) / m_ItemHeight);

                    // When you clicked behind the last item then unselect the selected item
                    if (m_SelectedItem > (int)m_Items.Count - 1)
                        m_SelectedItem = -1;
                }

                // Add the callback (if the user requested it)
                if ((oldSelectedItem != m_SelectedItem) && (ItemSelectedCallback != null))
                {
                    // When no item is selected then send an empty string, otherwise send the item
                    if (m_SelectedItem < 0)
                        m_Callback.Text  = "";
                    else
                        m_Callback.Text  = m_Items[m_SelectedItem];

                    m_Callback.Value = m_SelectedItem;
                    m_Callback.Trigger = CallbackTrigger.ItemSelected;
                    ItemSelectedCallback (this, m_Callback);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased(MouseButtonEventArgs e)
        {
            // If there is a scrollbar then pass it the event
            if (m_Scroll != null)
            {
                // Remember the old scrollbar value
                int oldValue = m_Scroll.Value;

                // Temporarily set the position of the scroll
                m_Scroll.Position = new Vector2f(Position.X + (m_Size.X - m_Borders.Right - m_Scroll.Size.X), Position.Y + m_Borders.Top);

                // Pass the event
                m_Scroll.OnLeftMouseReleased(e);

                // Reset the position
                m_Scroll.Position = new Vector2f(0, 0);

                // Check if the scrollbar value was incremented (you have pressed on the down arrow)
                if (m_Scroll.Value == oldValue + 1)
                {
                    // Decrement the value
                    m_Scroll.Value = m_Scroll.Value - 1;

                    // Scroll down with the whole item height instead of with a single pixel
                    m_Scroll.Value = (int)(m_Scroll.Value + m_ItemHeight - (m_Scroll.Value % m_ItemHeight));
                }
                else if (m_Scroll.Value == oldValue - 1) // Check if the scrollbar value was decremented (you have pressed on the up arrow)
                {
                    // increment the value
                    m_Scroll.Value = m_Scroll.Value + 1;

                    // Scroll up with the whole item height instead of with a single pixel
                    if (m_Scroll.Value % m_ItemHeight > 0)
                        m_Scroll.Value = (int)(m_Scroll.Value - (m_Scroll.Value % m_ItemHeight));
                    else
                        m_Scroll.Value = (int)(m_Scroll.Value - m_ItemHeight);
                }
            }

            m_MouseDown = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved(MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget();

            m_MouseHover = true;

            // If there is a scrollbar then pass the event
            if (m_Scroll != null)
            {
                // Temporarily set the position of the scroll
                m_Scroll.Position = new Vector2f(Position.X + (m_Size.X - m_Borders.Right - m_Scroll.Size.X), Position.Y + m_Borders.Top);

                // Check if you are dragging the thumb of the scrollbar
                if ((m_Scroll.m_MouseDown) && (m_Scroll.m_MouseDownOnThumb))
                {
                    // Pass the event, even when the mouse is not on top of the scrollbar
                    m_Scroll.OnMouseMoved(e);
                }
                else // You are just moving the mouse
                {
                    // When the mouse is on top of the scrollbar then pass the mouse move event
                    if (m_Scroll.MouseOnWidget(e.X, e.Y))
                        m_Scroll.OnMouseMoved(e);
                }

                // Reset the position
                m_Scroll.Position = new Vector2f(0, 0);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseWheelMoved(MouseWheelEventArgs e)
        {
            // Only do something when there is a scrollbar
            if (m_Scroll != null)
            {
                if (m_Scroll.LowValue < m_Scroll.Maximum)
                {
                    // Check if you are scrolling down
                    if (e.Delta < 0)
                    {
                        // Scroll down
                        m_Scroll.Value = (int)(m_Scroll.Value + ((-e.Delta) * (m_ItemHeight / 2)));
                    }
                    else // You are scrolling up
                    {
                        int change = (int)((e.Delta) * (m_ItemHeight / 2));

                        // Scroll up
                        if (change < m_Scroll.Value)
                            m_Scroll.Value = m_Scroll.Value - change;
                        else
                            m_Scroll.Value = 0;
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNotOnWidget()
        {
            if (m_MouseHover)
                MouseLeftWidget();

            m_MouseHover = false;

            if (m_Scroll != null)
                m_Scroll.m_MouseHover = false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNoLongerDown()
        {
            m_MouseDown = false;

            if (m_Scroll != null)
                m_Scroll.m_MouseDown = false;
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
        // Draws the widget on the render target.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Calculate the scale factor of the view
            float scaleViewX = target.Size.X / target.GetView().Size.X;
            float scaleViewY = target.Size.Y / target.GetView().Size.Y;

            // Get the global position
            Vector2f topLeftPosition;
            Vector2f bottomRightPosition;

            Vector2f viewPosition = (target.GetView().Size / 2.0f) - target.GetView().Center;

            if ((m_Scroll != null) && (m_Scroll.LowValue < m_Scroll.Maximum))
            {
                topLeftPosition = states.Transform.TransformPoint(Position + new Vector2f(m_Borders.Left, m_Borders.Top) + viewPosition);
                bottomRightPosition = states.Transform.TransformPoint(Position.X + m_Size.X - m_Borders.Right - m_Scroll.Size.X + viewPosition.X, Position.Y + m_Size.Y - m_Borders.Bottom + viewPosition.Y);
            }
            else
            {
                topLeftPosition = states.Transform.TransformPoint(Position + new Vector2f(m_Borders.Left, m_Borders.Top) + viewPosition);
                bottomRightPosition = states.Transform.TransformPoint(Position + m_Size - new Vector2f(m_Borders.Right, m_Borders.Bottom) + viewPosition);
            }

            // Adjust the transformation
            states.Transform *= Transform;

            // Remember the current transformation
            Transform oldTransform = states.Transform;

            // Draw the borders
            {
                // Draw left border
                RectangleShape border = new RectangleShape(new Vector2f(m_Borders.Left, m_Size.Y));
                border.FillColor = m_BorderColor;
                target.Draw(border, states);

                // Draw top border
                border.Size = new Vector2f(m_Size.X, m_Borders.Top);
                target.Draw(border, states);

                // Draw right border
                border.Position = new Vector2f(m_Size.X - m_Borders.Right, 0);
                border.Size = new Vector2f(m_Borders.Right, m_Size.Y);
                target.Draw(border, states);

                // Draw bottom border
                border.Position = new Vector2f(0, m_Size.Y - m_Borders.Bottom);
                border.Size = new Vector2f(m_Size.X, m_Borders.Bottom);
                target.Draw(border, states);
            }

            // Move the front rect a little bit
            states.Transform.Translate((float)(m_Borders.Left), (float)(m_Borders.Top));

            // Draw the background
            {
                RectangleShape front = new RectangleShape (new Vector2f((float)(m_Size.X - m_Borders.Left - m_Borders.Right),
                                                                       (float)(m_Size.Y - m_Borders.Top - m_Borders.Bottom)));
                front.FillColor = m_BackgroundColor;
                target.Draw(front, states);
            }

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

            // Create a text widget to draw the items
            Text text = new Text("", m_TextFont, m_TextSize);

            // Check if there is a scrollbar and whether it isn't hidden
            if ((m_Scroll != null) && (m_Scroll.LowValue < m_Scroll.Maximum))
            {
                // Store the transformation
                Transform storedTransform = states.Transform;

                // Find out which items should be drawn
                uint firstItem = (uint)(m_Scroll.Value / m_ItemHeight);
                uint lastItem = (uint)((m_Scroll.Value + m_Scroll.LowValue) / m_ItemHeight);

                // Show another item when the scrollbar is standing between two items
                if ((m_Scroll.Value + m_Scroll.LowValue) % m_ItemHeight != 0)
                    ++lastItem;

                // Set the clipping area
                Gl.glScissor(scissorLeft, (int)(target.Size.Y - scissorBottom), scissorRight - scissorLeft, scissorBottom - scissorTop);

                for (uint i = firstItem; i < lastItem; ++i)
                {
                    // Restore the transformations
                    states.Transform = storedTransform;

                    // Set the next item
                    text.DisplayedString = m_Items[(int)i];

                    // Get the global bounds
                    FloatRect bounds = text.GetGlobalBounds();

                    // Check if we are drawing the selected item
                    if (m_SelectedItem == (int)(i))
                    {
                        // Draw a background for the selected item
                        {
                            // Set a new transformation
                            states.Transform.Translate(0, ((float)(i * m_ItemHeight) - m_Scroll.Value));

                            // Create and draw the background
                            RectangleShape back = new RectangleShape(new Vector2f((m_Size.X - m_Borders.Left - m_Borders.Right), (float)m_ItemHeight));
                            back.FillColor = m_SelectedBackgroundColor;
                            target.Draw(back, states);

                            // Restore the transformation
                            states.Transform = storedTransform;
                        }

                        // Change the text color
                        text.Color = m_SelectedTextColor;
                    }
                    else // Set the normal text color
                        text.Color = m_TextColor;

                    // Set the translation for the text
                    states.Transform.Translate(2, (float)System.Math.Floor((float)(i * m_ItemHeight) - m_Scroll.Value + ((m_ItemHeight - bounds.Height) / 2.0f) - bounds.Top));

                    // Draw the text
                    target.Draw(text, states);
                }
            }
            else // There is no scrollbar or it is invisible
            {
                // Set the clipping area
                Gl.glScissor(scissorLeft, (int)(target.Size.Y - scissorBottom), scissorRight - scissorLeft, scissorBottom - scissorTop);

                // Store the current transformations
                Transform storedTransform = states.Transform;

                for (uint i = 0; i < m_Items.Count; ++i)
                {
                    // Restore the transformations
                    states.Transform = storedTransform;

                    // Set the next item
                    text.DisplayedString = m_Items[(int)i];

                    // Check if we are drawing the selected item
                    if (m_SelectedItem == (int)(i))
                    {
                        // Draw a background for the selected item
                        {
                            // Set a new transformation
                            states.Transform.Translate(0, (float)(i * m_ItemHeight));

                            // Create and draw the background
                            RectangleShape back = new RectangleShape(new Vector2f((float)(m_Size.X - m_Borders.Left - m_Borders.Right), (float)(m_ItemHeight)));
                            back.FillColor = m_SelectedBackgroundColor;
                            target.Draw(back, states);

                            // Restore the transformation
                            states.Transform = storedTransform;
                        }

                        // Change the text color
                        text.Color = m_SelectedTextColor;
                    }
                    else // Set the normal text color
                        text.Color = m_TextColor;

                    // Get the global bounds
                    FloatRect bounds = text.GetGlobalBounds();

                    // Set the translation for the text
                    states.Transform.Translate(2, (float)System.Math.Floor((i * m_ItemHeight) + ((m_ItemHeight - bounds.Height) / 2.0f) - bounds.Top));

                    // Draw the text
                    target.Draw(text, states);
                }
            }

            // Reset the old clipping area
            Gl.glScissor(scissor[0], scissor[1], scissor[2], scissor[3]);

            // Check if there is a scrollbar
            if (m_Scroll != null)
            {
                // Reset the transformation
                states.Transform = oldTransform;
                states.Transform.Translate((float)(m_Size.X) - m_Borders.Right - m_Scroll.Size.X, (float)(m_Borders.Top));

                // Draw the scrollbar
                target.Draw(m_Scroll, states);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

