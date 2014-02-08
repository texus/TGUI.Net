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
    public class ChatBox : Widget, WidgetBorders
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal ChatBox ()
        {
            m_DraggableWidget = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ChatBox (ChatBox copy) : base(copy)
        {
            m_LoadedConfigFile = copy.m_LoadedConfigFile;
            m_LineSpacing      = copy.m_LineSpacing;
            m_TextSize         = copy.m_TextSize;
            m_TextColor        = copy.m_TextColor;
            m_BorderColor      = copy.m_BorderColor;
            m_Borders          = copy.m_Borders;
            m_MaxLines         = copy.m_MaxLines;
            m_FullTextHeight   = copy.m_FullTextHeight;
            m_Panel            = new Panel(copy.m_Panel);

            // If there is a scrollbar then copy it
            if (copy.m_Scroll != null)
                m_Scroll = new Scrollbar(copy.m_Scroll);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a ChatBox section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ChatBox (string configFileFilename)
        {
            m_DraggableWidget = true;

            m_LoadedConfigFile = Global.ResourcePath + configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (m_LoadedConfigFile, "ChatBox");

            // Find the folder that contains the config file
            string configFileFolder = configFileFilename.Substring(0, configFileFilename.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "backgroundcolor")
                    BackgroundColor = configFile.ReadColor(i);
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
                        throw new Exception("Failed to parse value for Scrollbar in section ChatBox in " + m_LoadedConfigFile + ".");

                    // load the scrollbar
                    m_Scroll = new Scrollbar(configFileFolder + (configFile.Values[i]).Substring(1, configFile.Values[i].Length - 2));
                    m_Scroll.VerticalScroll = true;
                    m_Scroll.Size = new Vector2f(m_Scroll.Size.X, m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
                    m_Scroll.LowValue = (int)(m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
                    m_Scroll.Maximum = (int)m_FullTextHeight;
                }
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section ChatBox in " + m_LoadedConfigFile + ".");
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
        /// Size of the chat box
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override Vector2f Size
        {
            get
            {
                return m_Panel.Size;
            }
            set
            {
                // There is a minimum width
                if (m_Scroll == null)
                    value.X = System.Math.Max(50 + m_Borders.Left + m_Borders.Right, value.X);
                else
                    value.X = System.Math.Max(50 + m_Borders.Left + m_Borders.Right + m_Scroll.Size.X, value.X);

                // Remember the old height
                float oldHeight = m_Panel.Size.Y;

                // Set the new size
                m_Panel.Size = value;

                // If there is a scrollbar then reinitialize it
                if (m_Scroll != null)
                {
                    m_Scroll.LowValue = (int)(m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
                    m_Scroll.Size = new Vector2f(m_Scroll.Size.X, m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
                }

                // Find out how much the height has changed
                float heightDiff = m_Panel.Size.Y - oldHeight;

                // Reposition all labels in the chatbox
                foreach (Widget widget in m_Panel.GetWidgets())
                    widget.Position = new Vector2f (widget.Position.X, widget.Position.Y + heightDiff);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add a new line of text to the chat box
        /// </summary>
        ///
        /// <param name="text">Text that will be added to the chat box</param>
        ///
        /// <remarks>The whole text passed to this function will be considered as one line for the GetLine and RemoveLine functions,
        /// even if it is too long and gets split over multiple lines.</remarks>
        ///
        /// <remarks>The default text color and character size will be used.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddLine (string text)
        {
            AddLine (text, m_TextColor, m_TextSize);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add a new line of text to the chat box
        /// </summary>
        ///
        /// <param name="text">Text that will be added to the chat box</param>
        /// <param name="textSize">Size of the text</param>
        ///
        /// <remarks>The whole text passed to this function will be considered as one line for the GetLine and RemoveLine functions,
        /// even if it is too long and gets split over multiple lines.</remarks>
        ///
        /// <remarks>The default text color will be used.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddLine (string text, uint textSize)
        {
            AddLine (text, m_TextColor, textSize);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add a new line of text to the chat box
        /// </summary>
        ///
        /// <param name="text">Text that will be added to the chat box</param>
        /// <param name="color">Color of the text</param>
        ///
        /// <remarks>The whole text passed to this function will be considered as one line for the GetLine and RemoveLine functions,
        /// even if it is too long and gets split over multiple lines.</remarks>
        ///
        /// <remarks>The default character size will be used.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddLine (string text, Color color)
        {
            AddLine (text, color, m_TextSize);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Add a new line of text to the chat box
        /// </summary>
        ///
        /// <param name="text">Text that will be added to the chat box</param>
        /// <param name="color">Color of the text</param>
        /// <param name="textSize">Size of the text</param>
        /// <param name="font">Font of the text (null to use default font)</param>
        ///
        /// <remarks>The whole text passed to this function will be considered as one line for the GetLine and RemoveLine functions,
        /// even if it is too long and gets split over multiple lines.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddLine (string text, Color color, uint textSize, Font font = null)
        {
            var widgets = m_Panel.GetWidgets ();

            // Remove the top line if you exceed the maximum
            if ((m_MaxLines > 0) && (m_MaxLines < widgets.Count + 1))
                RemoveLine (0);

            Label label = m_Panel.Add (new Label ());
            label.TextColor = color;
            label.TextSize = textSize;

            if (font != null)
                label.TextFont = font;

            Label tempLine = new Label();
            tempLine.TextSize = textSize;
            tempLine.TextFont = label.TextFont;

            float width;
            if (m_Scroll == null)
                width = m_Panel.Size.X - m_Borders.Left - m_Borders.Right;
            else
                width = m_Panel.Size.X - m_Borders.Left - m_Borders.Right - m_Scroll.Size.X;

            if (width < 0)
                width = 0;

            // Split the label over multiple lines if necessary
            int pos = 0;
            int size = 0;
            while (pos + size < text.Length)
            {
                tempLine.Text = text.Substring(pos, ++size);

                if (tempLine.Size.X + 4.0f > width)
                {
                    label.Text = label.Text + text.Substring(pos, size - 1) + "\n";

                    pos = pos + size - 1;
                    size = 0;
                }
            }
            label.Text = label.Text + tempLine.Text;

            m_FullTextHeight += GetLineSpacing((uint)widgets.Count - 1);

            if (m_Scroll != null)
            {
                m_Scroll.Maximum = (int)m_FullTextHeight;

                if (m_Scroll.Maximum > m_Scroll.LowValue)
                    m_Scroll.Value = m_Scroll.Maximum - m_Scroll.LowValue;
            }

            // Reposition the labels
            UpdateDisplayedText();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the contents of the requested line
        /// </summary>
        ///
        /// <param name="lineIndex">The index of the line of which you request the contents. The first line has index 0.</param>
        ///
        /// <returns>The contents of the requested line</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string GetLine (uint lineIndex)
        {
            if (lineIndex < m_Panel.GetWidgets().Count)
            {
                return ((Label)(m_Panel.GetWidgets()[(int)lineIndex])).Text;
            }
            else // Index too high
                return "";
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the requested line
        /// </summary>
        ///
        /// <param name="lineIndex">The index of the line that should be removed. The first line has index 0.</param>
        ///
        /// <returns>True if the line was removed, false if no such line existed (index too high).</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RemoveLine (uint lineIndex)
        {
            if (lineIndex < m_Panel.GetWidgets().Count)
            {
                Label label = (Label)m_Panel.GetWidgets()[(int)lineIndex];
                m_FullTextHeight -= GetLineSpacing(lineIndex);
                m_Panel.Remove(label);

                if (m_Scroll != null)
                    m_Scroll.Maximum = (int)m_FullTextHeight;

                UpdateDisplayedText();
                return true;
            }
            else // Index too high
                return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes all lines from the chat box
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAllLines ()
        {
            m_Panel.RemoveAllWidgets();

            m_FullTextHeight = 0;

            if (m_Scroll != null)
                m_Scroll.Maximum = (int)m_FullTextHeight;

            UpdateDisplayedText();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the amount of lines in the chat box
        /// </summary>
        ///
        /// <returns>Number of lines in the chat box</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint GetLineAmount ()
        {
            return (uint)m_Panel.GetWidgets().Count;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Maximum amount of lines in the chat box
        /// </summary>
        ///
        /// <remarks>Only the last lines will be kept. Lines above those will be removed.
        /// Set to 0 to disable the line limit (default).</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint MaxLines
        {
            get
            {
                return m_MaxLines;
            }
            set
            {
                m_MaxLines = value;

                var widgets = m_Panel.GetWidgets();
                if ((m_MaxLines > 0) && (m_MaxLines < widgets.Count))
                {
                    for (uint i = 0; i < widgets.Count - m_MaxLines; i++)
                        m_FullTextHeight -= GetLineSpacing(i);

                    widgets.RemoveRange(0, (int)(widgets.Count - m_MaxLines));

                    if (m_Scroll != null)
                        m_Scroll.Maximum = (int)(widgets.Count * m_TextSize * 1.4f);

                    UpdateDisplayedText();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The default font of the text.
        /// By default, the GlobalFont of the parent is used.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Font TextFont
        {
            get
            {
                return m_Panel.GlobalFont;
            }
            set
            {
                m_Panel.GlobalFont = value;

                foreach (Label label in m_Panel.GetWidgets())
                    label.TextFont = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The default character size of a new line of text.
        /// The minimum text size is 8.
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

                // There is a minimum text size
                if (m_TextSize < 8)
                    m_TextSize = 8;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The default text color of a new line of text.
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
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Borders of the chat box
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
                // Reposition the labels
                foreach (Widget widget in m_Panel.GetWidgets())
                    widget.Position = new Vector2f (widget.Position.X + value.Left - m_Borders.Left, widget.Position.Y);

                m_Borders = value;

                // There is a minimum width
                float width = m_Panel.Size.X;
                if (width < (50 + m_Borders.Left + m_Borders.Right))
                    width = 50 + m_Borders.Left + m_Borders.Right;

                // Make sure that the panel has a valid size
                m_Panel.Size = new Vector2f(width, m_Panel.Size.Y);

                // If there is a scrollbar then reinitialize it
                if (m_Scroll != null)
                {
                    m_Scroll.LowValue = (int)(m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
                    m_Scroll.Size = new Vector2f(m_Scroll.Size.X, m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The background color that will be used inside the chat box
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color BackgroundColor
        {
            get
            {
                return m_Panel.BackgroundColor;
            }
            set
            {
                m_Panel.BackgroundColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The border color that will be used inside the chat box
        /// </summary>
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
        /// <summary>
        /// Changes the line spacing of all lines
        /// </summary>
        ///
        /// By default, line spacing is chosen based on the font and character size. This also means that when mixing different
        /// text styles in ChatBox, lines can have different line spacings.
        /// By calling this function, all line spacings will be set to the value passed to this function.
        ///
        /// The line spacing should be equal or greater than the text size to avoid overlapping lines.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint LineSpacing
        {
            get
            {
                return m_LineSpacing;
            }
            set
            {
                m_LineSpacing = value;

                UpdateDisplayedText();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Changes the scrollbar of the chat box
        /// </summary>
        ///
        /// <param name="scrollbarConfigFileFilename">Filename of the config file.
        /// The config file must contain a Scrollbar section with the needed information.</param>
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
            m_Scroll.Size = new Vector2f(m_Scroll.Size.X, m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
            m_Scroll.LowValue = (int)(m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
            m_Scroll.Maximum = (int)m_FullTextHeight;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the scrollbar from the chat box
        /// </summary>
        ///
        /// <remarks>When there are too many lines to fit in the chat box then some lines will be removed.</remarks>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveScrollbar ()
        {
            m_Scroll = null;
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

                m_Panel.Transparency = value;

                if (m_Scroll != null)
                    m_Scroll.Transparency = value;
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
            // Pass the event to the scrollbar (if there is one)
            if (m_Scroll != null)
            {
                // Temporarily set the position of the scroll
                m_Scroll.Position = new Vector2f(Position.X + m_Panel.Size.X - m_Borders.Right - m_Scroll.Size.X, Position.Y + m_Borders.Top);

                // Pass the event
                m_Scroll.MouseOnWidget(x, y);

                // Reset the position
                m_Scroll.Position = new Vector2f(0, 0);
            }

            if (Transform.TransformRect(new FloatRect(m_Borders.Left,
                                                      m_Borders.Top,
                                                      Size.X - m_Borders.Left - m_Borders.Right,
                                                      Size.Y - m_Borders.Top - m_Borders.Bottom)).Contains(x, y))
            {
                return true;
            }
            else
            {
                if (m_MouseHover)
                    MouseLeftWidget();

                m_MouseHover = false;
                return false;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the left mouse has been pressed on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnLeftMousePressed (MouseButtonEventArgs e)
        {
            // Set the mouse down flag to true
            m_MouseDown = true;

            // If there is a scrollbar then pass the event
            if (m_Scroll != null)
            {
                // Remember the old scrollbar value
                int oldValue = m_Scroll.Value;

                // Temporarily set the position of the scroll
                m_Scroll.Position = new Vector2f(Position.X + m_Panel.Size.X - m_Borders.Right - m_Scroll.Size.X, Position.Y + m_Borders.Top);

                // Pass the event
                if (m_Scroll.MouseOnWidget(e.X, e.Y))
                    m_Scroll.OnLeftMousePressed(e);

                // Reset the position
                m_Scroll.Position = new Vector2f(0, 0);

                // If the value of the scrollbar has changed then update the text
                if (oldValue != m_Scroll.Value)
                    UpdateDisplayedText();
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
            // If there is a scrollbar then pass it the event
            if (m_Scroll != null)
            {
                // Only pass the event when the scrollbar still thinks the mouse is down
                if (m_Scroll.m_MouseDown == true)
                {
                    // Remember the old scrollbar value
                    int oldValue = m_Scroll.Value;

                    // Temporarily set the position of the scroll
                    m_Scroll.Position = new Vector2f(Position.X + m_Panel.Size.X - m_Borders.Right - m_Scroll.Size.X, Position.Y + m_Borders.Top);

                    // Pass the event
                    m_Scroll.OnLeftMouseReleased(e);

                    // Reset the position
                    m_Scroll.Position = new Vector2f(0, 0);

                    // If the value of the scrollbar has changed then update the text
                    if (oldValue != m_Scroll.Value)
                    {
                        // Check if the scrollbar value was incremented (you have pressed on the down arrow)
                        if (m_Scroll.Value == oldValue + 1)
                        {
                            // Decrement the value
                            m_Scroll.Value = m_Scroll.Value - 1;

                            // Scroll down with the whole item height instead of with a single pixel
                            m_Scroll.Value = (int)(m_Scroll.Value + (int)m_TextSize - (m_Scroll.Value % (int)m_TextSize));
                        }
                        else if (m_Scroll.Value == oldValue - 1) // Check if the scrollbar value was decremented (you have pressed on the up arrow)
                        {
                            // increment the value
                            m_Scroll.Value = m_Scroll.Value + 1;

                            // Scroll up with the whole item height instead of with a single pixel
                            if (m_Scroll.Value % (int)m_TextSize > 0)
                                m_Scroll.Value = m_Scroll.Value - (m_Scroll.Value % (int)m_TextSize);
                            else
                                m_Scroll.Value = m_Scroll.Value - (int)m_TextSize;
                        }

                        UpdateDisplayedText();
                    }
                }
            }

            m_MouseDown = false;
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
                MouseEnteredWidget();

            // Set the mouse move flag
            m_MouseHover = true;

            // If there is a scrollbar then pass the event
            if (m_Scroll != null)
            {
                // Temporarily set the position of the scroll
                m_Scroll.Position = new Vector2f(Position.X + m_Panel.Size.X - m_Borders.Right - m_Scroll.Size.X, Position.Y + m_Borders.Top);
                
                // Check if you are dragging the thumb of the scrollbar
                if ((m_Scroll.m_MouseDown) && (m_Scroll.m_MouseDownOnThumb))
                {
                    // Remember the old scrollbar value
                    int oldValue = m_Scroll.Value;

                    // Pass the event, even when the mouse is not on top of the scrollbar
                    m_Scroll.OnMouseMoved(e);

                    // If the value of the scrollbar has changed then update the text
                    if (oldValue != m_Scroll.Value)
                        UpdateDisplayedText();
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
        /// <summary>
        /// Tells the widget that the mouse is no longer on top of the widget
        /// </summary>
        ///
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
        /// <summary>
        /// Tells the widget that the left mouse has been released
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void MouseNoLongerDown()
        {
            m_MouseDown = false;

            if (m_Scroll != null)
                m_Scroll.m_MouseDown = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse wheel has moved while the mouse was on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseWheelMoved (MouseWheelEventArgs e)
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
                        m_Scroll.Value = m_Scroll.Value + (-e.Delta * (int)m_TextSize);
                    }
                    else // You are scrolling up
                    {
                        int change = e.Delta * (int)m_TextSize;

                        // Scroll up
                        if (change < m_Scroll.Value)
                            m_Scroll.Value = m_Scroll.Value - change;
                        else
                            m_Scroll.Value = 0;
                    }

                    UpdateDisplayedText();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initializes the widget now that it has been added to a parent widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private uint GetLineSpacing(uint lineNumber)
        {
            System.Diagnostics.Debug.Assert(lineNumber < m_Panel.GetWidgets().Count);

            // If a line spacing was manually set then just return that one
            if (m_LineSpacing > 0)
                return m_LineSpacing;

            Label line = m_Panel.GetWidgets()[(int)lineNumber] as Label;
            int lineSpacing = m_Panel.GlobalFont.GetLineSpacing(line.TextSize);

            if (lineSpacing > line.TextSize)
                return (uint)lineSpacing;
            else
                return (uint)Math.Ceiling(line.Size.Y * 13.5 / 10.0);
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


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update the position of the labels
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UpdateDisplayedText ()
        {
            float position;
            if (m_Scroll != null)
                position = m_Borders.Top + 2.0f - m_Scroll.Value;
            else
                position = m_Borders.Top + 2.0f;

            var labels = m_Panel.GetWidgets();
            for (int i = 0; i < labels.Count; ++i)
            {
                Label label = labels[i] as Label;
                label.Position = new Vector2f(m_Borders.Left + 2.0f, position);

                position += GetLineSpacing((uint)i);

                // Hide the label when it is no longer visible
                if ((label.Position.Y + label.Size.Y < m_Borders.Top) || (label.Position.Y > m_Panel.Size.Y - m_Borders.Bottom))
                    label.Visible = false;
                else
                    label.Visible = true;
            }

            // Correct the position when there is no scrollbar
            if ((m_Scroll == null) && (labels.Count > 0))
            {
                Label lastLabel = (Label)labels[labels.Count-1];
                if (position > m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom)
                {
                    float diff = position - (m_Panel.Size.Y - m_Borders.Top - m_Borders.Bottom);
                    foreach (Label label in labels)
                        label.Position = new Vector2f(label.Position.X, label.Position.Y - diff);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Adjust the transformation
            states.Transform *= Transform;

            // Draw the panel
            target.Draw(m_Panel, states);

            // Draw left border
            RectangleShape border = new RectangleShape(new Vector2f(m_Borders.Left, m_Panel.Size.Y));
            border.FillColor = m_BorderColor;
            target.Draw(border, states);

            // Draw top border
            border.Size = new Vector2f(m_Panel.Size.X, m_Borders.Top);
            target.Draw(border, states);

            // Draw right border
            border.Position = new Vector2f(m_Panel.Size.X - m_Borders.Right, 0);
            border.Size = new Vector2f(m_Borders.Right, m_Panel.Size.Y);
            target.Draw(border, states);

            // Draw bottom border
            border.Position = new Vector2f(0, m_Panel.Size.Y - m_Borders.Bottom);
            border.Size = new Vector2f(m_Panel.Size.X, m_Borders.Bottom);
            target.Draw(border, states);

            // Check if there is a scrollbar
            if (m_Scroll != null)
            {
                // Draw the scrollbar
                states.Transform.Translate(m_Panel.Size.X - m_Borders.Right - m_Scroll.Size.X, m_Borders.Top);
                target.Draw(m_Scroll, states);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string    m_LoadedConfigFile = "";

        private uint      m_LineSpacing = 0;
        private uint      m_TextSize = 16;
        private Color     m_TextColor = Color.Black;
        private Color     m_BorderColor = Color.Black;
        private Borders   m_Borders = new Borders();

        private uint      m_MaxLines = 0;

        private float     m_FullTextHeight = 0;

        // The panel containing the labels
        private Panel     m_Panel = new Panel();

        // The scrollbar
        private Scrollbar m_Scroll = null;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
