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
    public class ComboBox : Widget, WidgetBorders
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal ComboBox ()
        {
            m_DraggableWidget = true;

            m_ListBox.Visible = false;
            m_ListBox.Size = new Vector2f(50, 24);
            m_ListBox.ItemHeight = 24;
            m_ListBox.ItemSelectedCallback += NewItemSelectedCallbackFunction;
            m_ListBox.UnfocusedCallback += ListBoxUnfocusedCallbackFunction;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ComboBox (ComboBox copy) : base(copy)
        {
            ItemSelectedCallback = copy.ItemSelectedCallback;

            m_LoadedConfigFile   = copy.m_LoadedConfigFile;
            m_SeparateHoverImage = copy.m_SeparateHoverImage;
            m_NrOfItemsToDisplay = copy.m_NrOfItemsToDisplay;
            m_Borders            = copy.m_Borders;

            Global.TextureManager.CopyTexture(copy.m_TextureArrowUpNormal, m_TextureArrowUpNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowUpHover, m_TextureArrowUpHover);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowDownNormal, m_TextureArrowDownNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowDownHover, m_TextureArrowDownHover);

            // Copy the list box
            m_ListBox = new ListBox(copy.m_ListBox);

            m_ListBox.Visible = false;
            m_ListBox.ItemSelectedCallback -= copy.NewItemSelectedCallbackFunction;
            m_ListBox.UnfocusedCallback -= copy.ListBoxUnfocusedCallbackFunction;
            m_ListBox.ItemSelectedCallback += NewItemSelectedCallbackFunction;
            m_ListBox.UnfocusedCallback += ListBoxUnfocusedCallbackFunction;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a ComboBox section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ComboBox (string configFileFilename)
        {
            m_DraggableWidget = true;

            m_ListBox.Visible = false;
            m_ListBox.Size = new Vector2f(50, 24);
            m_ListBox.ItemHeight = 24;
            m_ListBox.ItemSelectedCallback += NewItemSelectedCallbackFunction;
            m_ListBox.UnfocusedCallback += ListBoxUnfocusedCallbackFunction;

            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "ComboBox");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "separatehoverimage")
                    m_SeparateHoverImage = configFile.ReadBool(i);
                else if (configFile.Properties[i] == "backgroundcolor")
                    BackgroundColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "textcolor")
                    TextColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "selectedbackgroundcolor")
                    SelectedBackgroundColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "selectedtextcolor")
                    SelectedTextColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "bordercolor")
                    BorderColor = configFile.ReadColor(i);
                else if (configFile.Properties[i] == "arrowupnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowUpNormal);
                else if (configFile.Properties[i] == "arrowuphoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowUpHover);
                else if (configFile.Properties[i] == "arrowdownnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowDownNormal);
                else if (configFile.Properties[i] == "arrowdownhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowDownHover);
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
                    m_ListBox.SetScrollbar (configFileFolder + (configFile.Values[i]).Substring(1, configFile.Values[i].Length - 2));
                }
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section ComboBox in " + configFileFilename + ".");
            }

            // Make sure the required textures were loaded
            if ((m_TextureArrowUpNormal.texture == null) || (m_TextureArrowDownNormal.texture == null))
                throw new Exception("Not all needed images were loaded for the combo box. Is the ComboBox section in " + configFileFilename + " complete?");

            // Check if optional textures were loaded
            if ((m_TextureArrowUpHover.texture != null) && (m_TextureArrowDownHover.texture != null))
            {
                m_WidgetPhase |= (byte)WidgetPhase.Hover;
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
        /// Size of the combo box
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override Vector2f Size
        {
            get
            {
                return new Vector2f(m_ListBox.Size.X, m_ListBox.ItemHeight + m_Borders.Top + m_Borders.Bottom);
            }
            set
            {
                // Set the height of the combo box
                if (value.Y > m_Borders.Top + m_Borders.Bottom)
                    m_ListBox.ItemHeight = (uint)(value.Y - m_Borders.Top - m_Borders.Bottom);
                else
                    m_ListBox.ItemHeight = 10;

                // Set the size of the list box
                if (m_NrOfItemsToDisplay > 0)
                    m_ListBox.Size = new Vector2f(value.X, m_ListBox.ItemHeight * (System.Math.Min(m_NrOfItemsToDisplay, m_ListBox.GetItems().Count)) - 2*m_Borders.Bottom);
                else
                    m_ListBox.Size = new Vector2f(value.X, m_ListBox.ItemHeight * m_ListBox.GetItems().Count - 2*m_Borders.Bottom);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The number of items that are displayed in the list
        /// </summary>
        ///
        /// When there is no scrollbar then this is the maximum number of items.
        /// If there is one, then it will only become visible when there are more items than this number.
        ///
        /// When set to zero then all items are shown (then there will never be a scrollbar).
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint ItemsToDisplay
        {
            get
            {
                return m_NrOfItemsToDisplay;
            }
            set
            {
                m_NrOfItemsToDisplay = value;

                if (m_NrOfItemsToDisplay < m_ListBox.GetItems().Count)
                    m_ListBox.Size = new Vector2f(m_ListBox.Size.X, (m_NrOfItemsToDisplay * m_ListBox.ItemHeight) + 2*m_Borders.Bottom);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The background color
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color BackgroundColor
        {
            get
            {
                return m_ListBox.BackgroundColor;
            }
            set
            {
                m_ListBox.BackgroundColor = value;
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
                return m_ListBox.TextColor;
            }
            set
            {
                m_ListBox.TextColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The background color of the selected item
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color SelectedBackgroundColor
        {
            get
            {
                return m_ListBox.SelectedBackgroundColor;
            }
            set
            {
                m_ListBox.SelectedBackgroundColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The text color of the selected item
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color SelectedTextColor
        {
            get
            {
                return m_ListBox.SelectedTextColor;
            }
            set
            {
                m_ListBox.SelectedTextColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The color of the borders
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color BorderColor
        {
            get
            {
                return m_ListBox.BorderColor;
            }
            set
            {
                m_ListBox.BorderColor = value;
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
                return m_ListBox.TextFont;
            }
            set
            {
                m_ListBox.TextFont = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Borders of the combo box
        /// </summary>
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
                // Calculate the new item height
                uint itemHeight = m_ListBox.ItemHeight + m_Borders.Top + m_Borders.Bottom - value.Top - value.Bottom;

                // Set the new border size
                m_Borders = value;
                m_ListBox.Borders = new Borders(m_Borders.Left, m_Borders.Bottom, m_Borders.Right, m_Borders.Bottom);

                // There is a minimum width
                if (m_ListBox.Size.X < 50 + m_Borders.Left + m_Borders.Right + m_TextureArrowDownNormal.Size.X)
                    m_ListBox.Size = new Vector2f(50 + m_Borders.Left + m_Borders.Right + m_TextureArrowDownNormal.Size.X, m_ListBox.Size.Y);

                // The item height needs to change
                m_ListBox.ItemHeight = itemHeight;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adds an item to the list, so that it can be selected later
        /// </summary>
        ///
        /// <param name="itemName">The name of the item you want to add (this is the text that will be displayed inside the combo box)</param>
        ///
        /// <returns>
        /// -  The index of the item when it was successfully added.
        /// -  -1 when the list is full (you have set a maximum item limit and you are trying to add more items)
        /// -  -1 when there is no scrollbar and you try to have more items than the number of items to display
        /// </returns>
        ///
        /// <remarks>The index returned by this function may no longer correct when an item is removed.</remarks>        ///
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int AddItem (string itemName)
        {
            // Make room to add another item, until there are enough items
            if ((m_NrOfItemsToDisplay == 0) || (m_NrOfItemsToDisplay > m_ListBox.GetItems().Count))
                m_ListBox.Size = new Vector2f(m_ListBox.Size.X, (m_ListBox.ItemHeight * (m_ListBox.GetItems().Count + 1)) + 2*m_Borders.Bottom);

            // Add the item
            return m_ListBox.AddItem(itemName);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Selects an item from the list
        /// </summary>
        ///
        /// When adding items to the combo box with the AddItem function, none of them will be selected.
        /// If you don't want the combo box to stay empty until the user selects something, but you want a default item instead,
        /// then you can use this function to select an item.
        ///
        /// <param name="itemName">The item you want to select</param>
        ///
        /// <returns>True when one of the item matches the name and when it gets selected.</returns>
        ///
        /// <remarks>The first item that matches the name will be selected.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool SetSelectedItem (string itemName)
        {
            return m_ListBox.SetSelectedItem(itemName);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Selects an item from the list
        /// </summary>
        ///
        /// When adding items to the combo box with the AddItem function, none of them will be selected.
        /// If you don't want the combo box to stay empty until the user selects something, but you want a default item instead,
        /// then you can use this function to select an item.
        ///
        /// <param name="index">The index of the item to select</param>
        ///
        /// <returns>True when the index wasn't too high and when it gets selected.</returns>
        ///
        /// <remarks>If the index is -1 then the DeselectItem function will be called.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool SetSelectedItem (int index)
        {
            return m_ListBox.SetSelectedItem(index);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Deselects the selected item
        /// </summary>
        ///
        /// The combo box will be empty after this function is called.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void DeselectItem ()
        {
            m_ListBox.DeselectItem();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes an item from the list with a given index
        /// </summary>
        ///
        /// <param name="index">The index of the item to remove</param>
        ///
        /// <returns>True when the index wasn't too high and the item got removed.</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RemoveItem (uint index)
        {
            return m_ListBox.RemoveItem(index);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the first item from the list with a given name
        /// </summary>
        ///
        /// <param name="itemName">The item to remove</param>
        ///
        /// <returns>True when the name matched with an item and the item got removed.</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RemoveItem (string itemName)
        {
            return m_ListBox.RemoveItem(itemName);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes all items from the list
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAllItems ()
        {
            m_ListBox.RemoveAllItems();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the item name of the item with the given index
        /// </summary>
        ///
        /// <param name="index">The index of the item</param>
        ///
        /// <returns>The requested item, or an empty string when the index was too high</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string GetItem (uint index)
        {
            return m_ListBox.GetItem(index);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the index of the first item with the given name
        /// </summary>
        ///
        /// <param name="itemName">The name of the item</param>
        ///
        /// <returns>The index of the item that matches the name, or -1 when no item matches</returns>
        ///
        /// <remarks>This index may become wrong when an item is removed from the list.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetItemIndex (string itemName)
        {
            return m_ListBox.GetItemIndex(itemName);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the list that contains all the items
        /// </summary>
        ///
        /// <returns>The list of strings</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<string> GetItems ()
        {
            return m_ListBox.GetItems();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the currently selected item
        /// </summary>
        ///
        /// <returns>The selected item, or an empty string when no item is selected</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string GetSelectedItem ()
        {
            return m_ListBox.GetSelectedItem();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the index of the selected item
        /// </summary>
        ///
        /// <returns>The index of the selected item, or -1 when no item was selected</returns>
        ///
        /// <remarks>This index may become wrong when an item is removed from the list.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetSelectedItemIndex ()
        {
            return m_ListBox.GetSelectedItemIndex();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Changes the scrollbar that is displayed next to the list
        /// </summary>
        ///
        /// <param name="scrollbarConfigFileFilename">Filename of the config file.
        /// The config file must contain a Scrollbar section with the needed information.</param>
        ///
        /// <returns>True when the scrollbar was successfully loaded</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetScrollbar (string scrollbarConfigFileFilename)
        {
            m_ListBox.SetScrollbar(scrollbarConfigFileFilename);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the scrollbar
        /// </summary>
        ///
        /// <remarks>When there are too many items to fit in the list then the items will be removed.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveScrollbar ()
        {
            m_ListBox.RemoveScrollbar();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The maximum items that the combo box can contain.
        /// When the maximum is set to 0 then the limit will be disabled.
        /// </summary>
        ///
        /// <remarks>If no scrollbar was loaded then there is always a limitation because there will be a limited space for the items.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint MaximumItems
        {
            get
            {
                return m_ListBox.MaximumItems;
            }
            set
            {
                m_ListBox.MaximumItems = value;
            }
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

                m_ListBox.Transparency = m_Opacity;

                m_TextureArrowUpNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureArrowDownNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureArrowUpHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureArrowDownHover.sprite.Color = new Color(255, 255, 255, m_Opacity);
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
            // Check if the mouse is on top of the combo box
            if ((x > Position.X) && (x < Position.X + m_ListBox.Size.X)
             && (y > Position.Y) && (y < Position.Y + m_ListBox.ItemHeight + m_Borders.Top + m_Borders.Bottom))
            { 
                return true;
            }

            if (m_MouseHover)
                MouseLeftWidget();

            // The mouse is not on top of the combo box
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
            m_MouseDown = true;

            // If the list wasn't visible then open it
            if (!m_ListBox.Visible)
            {
                // Show the list
                ShowListBox ();

                // Check if there is a scrollbar
                if (m_ListBox.m_Scroll != null)
                {
                    // If the selected item is not visible then change the value of the scrollbar
                    if (m_NrOfItemsToDisplay > 0)
                    {
                        if ((uint)(m_ListBox.GetSelectedItemIndex() + 1) > m_NrOfItemsToDisplay)
                            m_ListBox.m_Scroll.Value = (int)((m_ListBox.GetSelectedItemIndex() - m_NrOfItemsToDisplay + 1) * m_ListBox.ItemHeight);
                        else
                            m_ListBox.m_Scroll.Value = 0;
                    }
                }
            }
            else // This list was already open, so close it now
                HideListBox();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMouseReleased (MouseButtonEventArgs e)
        {
            m_MouseDown = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse wheel has moved while the mouse was on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseWheelMoved (MouseWheelEventArgs e)
        {
            if (!m_ListBox.Visible)
            {
                // Check if you are scrolling down
                if (e.Delta < 0)
                {
                    // select the next item
                    if ((uint)(m_ListBox.GetSelectedItemIndex() + 1) < m_ListBox.GetItems().Count)
                        m_ListBox.SetSelectedItem(m_ListBox.GetSelectedItemIndex() + 1);
                }
                else // You are scrolling up
                {
                    // select the previous item
                    if (m_ListBox.GetSelectedItemIndex() > 0)
                        m_ListBox.SetSelectedItem(m_ListBox.GetSelectedItemIndex()-1);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been released
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNoLongerDown()
        {
            m_MouseDown = false;
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
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Shows the list of items
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ShowListBox()
        {
            if (!m_ListBox.Visible)
            {
                m_ListBox.Visible = true;

                Vector2f position = new Vector2f(Position.X, Position.Y + m_ListBox.ItemHeight + m_Borders.Top);

                Widget container = this;
                while (container.Parent != null)
                {
                    container = container.Parent;
                    position += container.Position;

                    // Child window needs an exception
                    ChildWindow child = container as ChildWindow;
                    if (child != null)
                    {
                        position.X += child.Borders.Left;
                        position.Y += child.Borders.Top + child.TitleBarHeight;
                    }
                }

                m_ListBox.Position = position;
                ((Container)(container)).Add(m_ListBox);
                m_ListBox.Focused = true;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Hides the list of items
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void HideListBox()
        {
            // If the list was open then close it now
            if (m_ListBox.Visible)
            {
                m_ListBox.Visible = false;

                // Find the gui in order to remove the ListBox from it
                Widget container = this;
                while (container.Parent != null)
                    container = container.Parent;

                ((Container)(container)).Remove(m_ListBox);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Respond when the internal list box reports that a new item was selected
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void NewItemSelectedCallbackFunction(object sender, CallbackArgs e)
        {
            if (ItemSelectedCallback != null)
            {
                m_Callback.Text    = m_ListBox.GetSelectedItem();
                m_Callback.Value   = m_ListBox.GetSelectedItemIndex();
                m_Callback.Trigger = CallbackTrigger.ItemSelected;
                ItemSelectedCallback (this, m_Callback);
            }

            HideListBox();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Respond when the internal list box gets unfocused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ListBoxUnfocusedCallbackFunction(object sender, CallbackArgs e)
        {
            if (m_MouseHover == false)
                HideListBox();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Calculate the scale factor of the view
            float scaleViewX = target.Size.X / target.GetView().Size.X;
            float scaleViewY = target.Size.Y / target.GetView().Size.Y;

            Vector2f viewPosition = (target.GetView ().Size / 2.0f) - target.GetView ().Center;

            // Get the global position
            Vector2f topLeftPosition = states.Transform.TransformPoint(Position + new Vector2f(m_Borders.Left, m_Borders.Top) + viewPosition);
            Vector2f bottomRightPosition = states.Transform.TransformPoint(Position.X + m_ListBox.Size.X - m_Borders.Right - (m_TextureArrowDownNormal.Size.X * ((float)(m_ListBox.ItemHeight) / m_TextureArrowDownNormal.Size.Y)) + viewPosition.X,
                                                                           Position.Y + m_ListBox.Size.Y - m_Borders.Bottom + viewPosition.Y);

            // Adjust the transformation
            states.Transform *= Transform;

            // Remember the current transformation
            Transform oldTransform = states.Transform;

            // Draw left border
            RectangleShape border = new RectangleShape(new Vector2f(m_Borders.Left, m_ListBox.ItemHeight + m_Borders.Top + m_Borders.Bottom));
            border.FillColor = m_ListBox.BorderColor;
            target.Draw(border, states);

            // Draw top border
            border.Size = new Vector2f(m_ListBox.Size.X, m_Borders.Top);
            target.Draw(border, states);

            // Draw right border
            border.Position = new Vector2f(m_ListBox.Size.X - m_Borders.Right, 0);
            border.Size = new Vector2f(m_Borders.Right, m_ListBox.ItemHeight + m_Borders.Top + m_Borders.Bottom);
            target.Draw(border, states);

            // Draw bottom border
            border.Position = new Vector2f(0, m_ListBox.ItemHeight + m_Borders.Top);
            border.Size = new Vector2f(m_ListBox.Size.X, m_Borders.Bottom);
            target.Draw(border, states);

            // Move the front rect a little bit
            states.Transform.Translate((float)(m_Borders.Left), (float)(m_Borders.Top));

            // Draw the combo box
            RectangleShape Front = new RectangleShape (new Vector2f((float)(m_ListBox.Size.X - m_Borders.Left - m_Borders.Right),
                                                                    (float)(m_ListBox.ItemHeight)));
            Front.FillColor = m_ListBox.BackgroundColor;
            target.Draw(Front, states);

            // Create a text widget to draw it
            Text tempText = new Text("kg", m_ListBox.TextFont);
            tempText.CharacterSize = m_ListBox.ItemHeight;
            tempText.CharacterSize = (uint)(tempText.CharacterSize - tempText.GetLocalBounds().Top);
            tempText.Color = m_ListBox.TextColor;

            // Get the old clipping area
            int[] scissor = new int[4];
            Gl.glGetIntegerv(Gl.GL_SCISSOR_BOX, scissor);

            // Calculate the clipping area
            int scissorLeft = System.Math.Max((int)(topLeftPosition.X * scaleViewX), scissor[0]);
            int scissorTop = System.Math.Max((int)(topLeftPosition.Y * scaleViewY), (int)(target.Size.Y) - scissor[1] - scissor[3]);
            int scissorRight = System.Math.Min((int)(bottomRightPosition.X  * scaleViewX), scissor[0] + scissor[2]);
            int scissorBottom = System.Math.Min((int)(bottomRightPosition.Y * scaleViewY), (int)(target.Size.Y) - scissor[1]);

            // If the widget outside the window then don't draw anything
            if (scissorRight < scissorLeft)
                scissorRight = scissorLeft;
            else if (scissorBottom < scissorTop)
                scissorTop = scissorBottom;

            // Set the clipping area
            Gl.glScissor(scissorLeft, (int)(target.Size.Y - scissorBottom), scissorRight - scissorLeft, scissorBottom - scissorTop);

            // Draw the selected item
            states.Transform.Translate(2, (float)System.Math.Floor((m_ListBox.ItemHeight - tempText.GetLocalBounds().Height) / 2.0f -  tempText.GetLocalBounds().Top));
            tempText.DisplayedString = m_ListBox.GetSelectedItem();
            target.Draw(tempText, states);

            // Reset the old clipping area
            Gl.glScissor(scissor[0], scissor[1], scissor[2], scissor[3]);

            // Reset the transformations
            states.Transform = oldTransform;

            // Set the arrow like it should (down when list box is invisible, up when it is visible)
            if (m_ListBox.Visible)
            {
                float scaleFactor =  (float)(m_ListBox.ItemHeight) / m_TextureArrowUpNormal.Size.Y;
                states.Transform.Translate(m_ListBox.Size.X - m_Borders.Right - (m_TextureArrowUpNormal.Size.X * scaleFactor), (float)(m_Borders.Top));
                states.Transform.Scale(scaleFactor, scaleFactor);

                // Draw the arrow
                if (m_SeparateHoverImage)
                {
                    if ((m_MouseHover) && ((m_WidgetPhase & (byte)WidgetPhase.Hover) != 0))
                        target.Draw(m_TextureArrowUpHover.sprite, states);
                    else
                        target.Draw(m_TextureArrowUpNormal.sprite, states);
                }
                else // There is no separate hover image
                {
                    target.Draw(m_TextureArrowUpNormal.sprite, states);

                    if ((m_MouseHover) && ((m_WidgetPhase & (byte)WidgetPhase.Focused) != 0))
                        target.Draw(m_TextureArrowUpHover.sprite, states);
                }
            }
            else
            {
                float scaleFactor =  (float)(m_ListBox.ItemHeight) / m_TextureArrowDownNormal.Size.Y;
                states.Transform.Translate(m_ListBox.Size.X - m_Borders.Right - (m_TextureArrowDownNormal.Size.X * scaleFactor), (float)(m_Borders.Top));
                states.Transform.Scale(scaleFactor, scaleFactor);

                // Draw the arrow
                if (m_SeparateHoverImage)
                {
                    if ((m_MouseHover) && ((m_WidgetPhase & (byte)WidgetPhase.Hover) != 0))
                        target.Draw(m_TextureArrowDownHover.sprite, states);
                    else
                        target.Draw(m_TextureArrowDownNormal.sprite, states);
                }
                else // There is no separate hover image
                {
                    target.Draw(m_TextureArrowDownNormal.sprite, states);

                    if ((m_MouseHover) && ((m_WidgetPhase & (byte)WidgetPhase.Hover) != 0))
                        target.Draw(m_TextureArrowDownHover.sprite, states);
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the ItemSelected event</summary>
        public event EventHandler<CallbackArgs> ItemSelectedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string  m_LoadedConfigFile = "";

        private bool    m_SeparateHoverImage = false;

        // The number of items to display. If there is a scrollbar then you can scroll to see the other.
        // If there is no scrollbar then this will be the maximum amount of items.
        private uint    m_NrOfItemsToDisplay = 0;

        // Implly a list box is used to store all items
        private ListBox m_ListBox = new ListBox();

        // The textures for the arrow image
        private Impl.Sprite  m_TextureArrowUpNormal = new Impl.Sprite();
        private Impl.Sprite  m_TextureArrowUpHover = new Impl.Sprite();
        private Impl.Sprite  m_TextureArrowDownNormal = new Impl.Sprite();
        private Impl.Sprite  m_TextureArrowDownHover = new Impl.Sprite();

        private Borders m_Borders = new Borders();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
