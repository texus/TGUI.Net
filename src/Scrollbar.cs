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
    public class Scrollbar : Widget
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Scrollbar ()
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
        public Scrollbar (Scrollbar copy) : base(copy)
        {
            ValueChangedCallback = copy.ValueChangedCallback;

            m_LoadedConfigFile    = copy.m_LoadedConfigFile;
            m_MouseDownOnThumb    = copy.m_MouseDownOnThumb;
            m_MouseDownOnThumbPos = copy.m_MouseDownOnThumbPos;
            m_Maximum             = copy.m_Maximum;
            m_Value               = copy.m_Value;
            m_LowValue            = copy.m_LowValue;
            m_VerticalScroll      = copy.m_VerticalScroll;
            m_VerticalImage       = copy.m_VerticalImage;
            m_AutoHide            = copy.m_AutoHide;
            m_MouseDownOnArrow    = copy.m_MouseDownOnArrow;
            m_SplitImage          = copy.m_SplitImage;
            m_SeparateHoverImage  = copy.m_SeparateHoverImage;
            m_Size                = copy.m_Size;
            m_ThumbSize           = copy.m_ThumbSize;

            Global.TextureManager.CopyTexture(copy.m_TextureTrackNormal_L, m_TextureTrackNormal_L);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackNormal_M, m_TextureTrackNormal_M);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackNormal_R, m_TextureTrackNormal_R);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackHover_L, m_TextureTrackHover_L);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackHover_M, m_TextureTrackHover_M);
            Global.TextureManager.CopyTexture(copy.m_TextureTrackHover_R, m_TextureTrackHover_R);
            Global.TextureManager.CopyTexture(copy.m_TextureThumbNormal, m_TextureThumbNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureThumbHover, m_TextureThumbHover);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowUpNormal, m_TextureArrowUpNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowUpHover, m_TextureArrowUpHover);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowDownNormal, m_TextureArrowDownNormal);
            Global.TextureManager.CopyTexture(copy.m_TextureArrowDownHover, m_TextureArrowDownHover);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a Scrollbar section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Scrollbar (string configFileFilename)
        {
            m_DraggableWidget = true;

            m_LoadedConfigFile = Global.ResourcePath + configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (m_LoadedConfigFile, "Scrollbar");

            // Find the folder that contains the config file
            string configFileFolder = m_LoadedConfigFile.Substring(0, m_LoadedConfigFile.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "separatehoverimage")
                    m_SeparateHoverImage = configFile.ReadBool(i);
                else if (configFile.Properties[i] == "verticalscroll")
                {
                    m_VerticalScroll = configFile.ReadBool(i);
                    m_VerticalImage = m_VerticalScroll;
                }
                else if (configFile.Properties[i] == "tracknormalimage")
                {
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackNormal_M);
                    m_SplitImage = false;
                }
                else if (configFile.Properties[i] == "trackhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackHover_M);
                else if (configFile.Properties[i] == "thumbnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureThumbNormal);
                else if (configFile.Properties[i] == "thumbhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureThumbHover);
                else if (configFile.Properties[i] == "arrowupnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowUpNormal);
                else if (configFile.Properties[i] == "arrowuphoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowUpHover);
                else if (configFile.Properties[i] == "arrowdownnormalimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowDownNormal);
                else if (configFile.Properties[i] == "arrowdownhoverimage")
                    configFile.ReadTexture(i, configFileFolder, m_TextureArrowDownHover);
                else if (configFile.Properties[i] == "tracknormalimage_l")
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackNormal_L);
                else if (configFile.Properties[i] == "tracknormalimage_m")
                {
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackNormal_M);
                    m_SplitImage = true;
                }
                else if (configFile.Properties[i] == "tracknormalimage_r")
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackNormal_R);
                else if (configFile.Properties[i] == "trackhoverimage_l")
                    configFile.ReadTexture(i, configFileFolder, m_TextureTrackHover_L);
                else if (configFile.Properties[i] == "trackhoverimage_m")
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackHover_M);
                else if (configFile.Properties[i] == "trackhoverimage_r")
                    configFile.ReadTexture (i, configFileFolder, m_TextureTrackHover_R);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section Scrollbar in " + m_LoadedConfigFile + ".");
            }

            // Check if the image is split
            if (m_SplitImage)
            {
                 throw new Exception("SplitImage is not yet supported yet in Scrollbar.");
/*
                // Make sure the required textures were loaded
                if ((m_TextureTrackNormal_L.texture != null) && (m_TextureTrackNormal_M.texture != null) && (m_TextureTrackNormal_R.texture != null)
                    && (m_TextureThumbNormal.texture != null) && (m_TextureArrowUpNormal.texture != null) && (m_TextureArrowDownNormal.texture != null))
                {
                    // Set the size of the scrollbar
                    if (m_VerticalImage)
                        Size = new Vector2f((m_TextureTrackNormal_M.Size.X), (float)(m_TextureTrackNormal_L.Size.Y + m_TextureTrackNormal_M.Size.Y + m_TextureTrackNormal_R.Size.Y));
                    else
                        Size = new Vector2f((m_TextureTrackNormal_L.Size.X + m_TextureTrackNormal_M.Size.X + m_TextureTrackNormal_R.Size.X), (float)(m_TextureTrackNormal_M.Size.Y));

                    // Set the thumb size
                    m_ThumbSize = new Vector2f(m_TextureThumbNormal.Size.X, m_TextureThumbNormal.Size.Y);
                }
                else
                    throw new Exception("Not all needed images were loaded for the scrollbar. Is the Scrollbar section in " + m_LoadedConfigFile + " complete?");

                // Check if optional textures were loaded
                if ((m_TextureTrackHover_L.texture != null) && (m_TextureTrackHover_M.texture != null) && (m_TextureTrackHover_R.texture != null)
                    && (m_TextureThumbHover.texture != null) && (m_TextureArrowUpHover.texture != null) && (m_TextureArrowDownHover.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;
                }
*/
            }
            else // The image isn't split
            {
                // Make sure the required textures were loaded
                if ((m_TextureTrackNormal_M.texture != null) && (m_TextureThumbNormal.texture != null)
                    && (m_TextureArrowUpNormal.texture != null) && (m_TextureArrowDownNormal.texture != null))
                {
                    // Set the size of the scrollbar
                    Size = new Vector2f(m_TextureTrackNormal_M.Size.X, m_TextureTrackNormal_M.Size.Y);
                }
                else
                    throw new Exception("TGUI error: Not all needed images were loaded for the scrollbar. Is the Scrollbar section in " + m_LoadedConfigFile + " complete?");

                // Check if optional textures were loaded
                if ((m_TextureTrackHover_M.texture != null) && (m_TextureThumbHover.texture != null)
                    && (m_TextureArrowUpHover.texture != null) && (m_TextureArrowDownHover.texture != null))
                {
                    m_WidgetPhase |= (byte)WidgetPhase.Hover;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Destructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Scrollbar ()
        {
            if (m_TextureTrackNormal_L.texture != null)   Global.TextureManager.RemoveTexture(m_TextureTrackNormal_L);
            if (m_TextureTrackHover_L.texture != null)    Global.TextureManager.RemoveTexture(m_TextureTrackHover_L);
            if (m_TextureTrackNormal_M.texture != null)   Global.TextureManager.RemoveTexture(m_TextureTrackNormal_M);
            if (m_TextureTrackHover_M.texture != null)    Global.TextureManager.RemoveTexture(m_TextureTrackHover_M);
            if (m_TextureTrackNormal_R.texture != null)   Global.TextureManager.RemoveTexture(m_TextureTrackNormal_R);
            if (m_TextureTrackHover_R.texture != null)    Global.TextureManager.RemoveTexture(m_TextureTrackHover_R);
            if (m_TextureThumbNormal.texture != null)     Global.TextureManager.RemoveTexture(m_TextureThumbNormal);
            if (m_TextureThumbHover.texture != null)      Global.TextureManager.RemoveTexture(m_TextureThumbHover);
            if (m_TextureArrowUpNormal.texture != null)   Global.TextureManager.RemoveTexture(m_TextureArrowUpNormal);
            if (m_TextureArrowUpHover.texture != null)    Global.TextureManager.RemoveTexture(m_TextureArrowUpHover);
            if (m_TextureArrowDownNormal.texture != null) Global.TextureManager.RemoveTexture(m_TextureArrowDownNormal);
            if (m_TextureArrowDownHover.texture != null)  Global.TextureManager.RemoveTexture(m_TextureArrowDownHover);
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
        /// Size of the Scrollbar
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

                // A negative size is not allowed for this widget
                if (m_Size.X < 0) m_Size.X = -m_Size.X;
                if (m_Size.Y < 0) m_Size.Y = -m_Size.Y;

                // Set the thumb size
                if (m_VerticalImage == m_VerticalScroll)
                {
                    if (m_VerticalScroll)
                    {
                        m_ThumbSize.X = (m_Size.X / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.X;
                        m_ThumbSize.Y = (m_Size.X / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.Y;
                    }
                    else
                    {
                        m_ThumbSize.X = (m_Size.Y / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.X;
                        m_ThumbSize.Y = (m_Size.Y / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.Y;
                    }
                }
                else // m_VerticalImage != m_VerticalScroll
                {
                    if (m_VerticalScroll)
                    {
                        m_ThumbSize.X = (m_Size.X / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.Y;
                        m_ThumbSize.Y = (m_Size.X / m_TextureTrackNormal_M.Size.Y) * m_TextureThumbNormal.Size.X;
                    }
                    else
                    {
                        m_ThumbSize.X = (m_Size.Y / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.Y;
                        m_ThumbSize.Y = (m_Size.Y / m_TextureTrackNormal_M.Size.X) * m_TextureThumbNormal.Size.X;
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Maximum value of the scrollbar
        /// </summary>
        ///
        /// When the value is bigger than (maximum - low value), the value is set to maximum - low value.
        /// The default maximum value is 10.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Maximum
        {
            get
            {
                return m_Maximum;
            }
            set
            {
                // Set the new maximum
                if (value > 0)
                    m_Maximum = value;
                else
                    m_Maximum = 1;

                // When the value is above the maximum then adjust it
                if (m_Maximum < m_LowValue)
                    Value = 0;
                else if (m_Value > m_Maximum - m_LowValue)
                    Value = m_Maximum - m_LowValue;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Value of the scrollbar.
        /// The value has to be smaller than Maximum - LowValue.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;

                    // When the value is above the maximum then adjust it
                    if (m_Maximum < m_LowValue)
                        Value = 0;
                    else if (m_Value > m_Maximum - m_LowValue)
                        m_Value = m_Maximum - m_LowValue;

                    // Add the callback (if the user requested it)
                    if (ValueChangedCallback != null)
                    {
                        m_Callback.Trigger = CallbackTrigger.ValueChanged;
                        m_Callback.Value   = Value;
                        ValueChangedCallback (this, m_Callback);
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Low value of the scrollbar.
        /// Until the maximum is bigger than this value, no scrollbar will be drawn (when AutoHide is true).
        /// </summary>
        ///
        /// In e.g. a list box, this value is the amount of items that fit inside the list box,
        /// while the maximum is the amount of items that are in the list box at that moment.
        ///
        /// You can choose to always draw the scrollbar by calling AutoHide to false.
        ///
        /// The default low value is 6.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int LowValue
        {
            get
            {
                return m_LowValue;
            }
            set
            {
                if (value < 0)
                    m_LowValue = 0;
                else
                {
                    m_LowValue = value;

                    // When the value is above the maximum then adjust it
                    if (m_Maximum < m_LowValue)
                        Value = 0;
                    else if (m_Value > m_Maximum - m_LowValue)
                        Value = m_Maximum - m_LowValue;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Does the scrollbar lie vertically?
        /// </summary>
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

                // Swap the width and height if needed
                if (m_VerticalScroll)
                {
                    if (m_Size.X > m_Size.Y)
                        Size = new Vector2f(m_Size.Y, m_Size.X);
                    else
                        Size = new Vector2f(m_Size.X, m_Size.Y);
                }
                else // The scrollbar lies horizontal
                {
                    if (m_Size.Y > m_Size.X)
                        Size = new Vector2f(m_Size.Y, m_Size.X);
                    else
                        Size = new Vector2f(m_Size.X, m_Size.Y);
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// How much the value changes when pressing one of the arrows of the scrollbar
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint ArrowScrollAmount
        {
            get
            {
                return m_scrollAmount;
            }
            set
            {
                m_scrollAmount = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Should the scrollbar be hidden when you can't scroll (LowValue >= Maximum)?
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool AutoHide
        {
            get
            {
                return m_AutoHide;
            }
            set
            {
                m_AutoHide = value;
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

                m_TextureTrackNormal_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackHover_L.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackNormal_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackHover_M.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackNormal_R.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureTrackHover_R.sprite.Color = new Color(255, 255, 255, m_Opacity);

                m_TextureThumbNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureThumbHover.sprite.Color = new Color(255, 255, 255, m_Opacity);

                m_TextureArrowUpNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_TextureArrowUpHover.sprite.Color = new Color(255, 255, 255, m_Opacity);

                m_TextureArrowDownNormal.sprite.Color = new Color(255, 255, 255, m_Opacity);
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
            // Don't make any calculations when no scrollbar is needed
            if ((m_Maximum <= m_LowValue) && (m_AutoHide == true))
                return false;

            // Check if the mouse is on top of the scrollbar
            if (Transform.TransformRect(new FloatRect(0, 0, m_Size.X, m_Size.Y)).Contains(x, y))
                return true;

            if (m_MouseHover)
                MouseLeftWidget();

            // The mouse is not on top of the scrollbar
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
            m_MouseDownOnArrow = false;

            // Make sure you didn't click on one of the arrows
            if (m_VerticalScroll)
            {
                float scalingX;
                if (m_VerticalImage == m_VerticalScroll)
                    scalingX = m_Size.X / m_TextureTrackNormal_M.Size.X;
                else
                    scalingX = m_Size.X / m_TextureTrackNormal_M.Size.Y;

                // Check if the arrows are drawn at full size
                if (m_Size.Y > (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scalingX)
                {
                    // Check if you clicked on one of the arrows
                    if (e.Y < Position.Y + (m_TextureArrowUpNormal.Size.Y * scalingX))
                        m_MouseDownOnArrow = true;
                    else if (e.Y > Position.Y + m_Size.Y - (m_TextureArrowUpNormal.Size.Y * scalingX))
                        m_MouseDownOnArrow = true;
                }
                else // The arrows are not drawn at full size (there is no track)
                    m_MouseDownOnArrow = true;
            }
            else // The scrollbar lies horizontal
            {
                float scalingY;
                if (m_VerticalImage == m_VerticalScroll)
                    scalingY = m_Size.Y / m_TextureTrackNormal_M.Size.Y;
                else
                    scalingY = m_Size.Y / m_TextureTrackNormal_M.Size.X;

                // Check if the arrows are drawn at full size
                if (m_Size.X > (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scalingY)
                {
                    // Check if you clicked on one of the arrows
                    if (e.X < Position.X + (m_TextureArrowUpNormal.Size.Y * scalingY))
                        m_MouseDownOnArrow = true;
                    else if (e.X > Position.X + m_Size.X - (m_TextureArrowUpNormal.Size.Y * scalingY))
                        m_MouseDownOnArrow = true;
                }
                else // The arrows are not drawn at full size (there is no track)
                    m_MouseDownOnArrow = true;
            }

            FloatRect thumbRect = GetThumbRect();

            // Check if the mouse is on top of the thumb
            if (new FloatRect(Position.X + thumbRect.Left, Position.Y + thumbRect.Top, thumbRect.Width, thumbRect.Height).Contains(e.X, e.Y))
            {
                m_MouseDownOnThumbPos.X = e.X - Position.X - thumbRect.Left;
                m_MouseDownOnThumbPos.Y = e.Y - Position.Y - thumbRect.Top;

                m_MouseDownOnThumb = true;
            }
            else // The mouse is not on top of the thumb
                m_MouseDownOnThumb = false;

            // Refresh the scrollbar value
            if (m_MouseDownOnArrow == false)
            {
                MouseMoveEvent move = new MouseMoveEvent();
                move.X = e.X;
                move.Y = e.Y;
                OnMouseMoved (new MouseMoveEventArgs(move));
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
            // Check if one of the arrows was clicked
            if ((m_MouseDown) && (m_MouseDownOnArrow))
            {
                // Only continue when the calculations can be made
                if (m_Maximum > m_LowValue)
                {
                    bool valueDown = false;
                    bool valueUp = false;

                    // Check in which direction the scrollbar lies
                    if (m_VerticalScroll)
                    {
                        float scalingX;
                        if (m_VerticalImage == m_VerticalScroll)
                            scalingX = m_Size.X / m_TextureTrackNormal_M.Size.X;
                        else
                            scalingX = m_Size.X / m_TextureTrackNormal_M.Size.Y;

                        // Check if the arrows are drawn at full size
                        if (m_Size.Y > (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scalingX)
                        {
                            // Check if you clicked on the top arrow
                            if (e.Y < Position.Y + (m_TextureArrowUpNormal.Size.Y * scalingX))
                                valueDown = true;

                            // Check if you clicked the down arrow
                            else if (e.Y > Position.Y + m_Size.Y - (m_TextureArrowUpNormal.Size.Y * scalingX))
                                valueUp = true;
                        }
                        else // The arrows are not drawn at full size
                        {
                            // Check on which arrow you clicked
                            if (e.Y < Position.Y + (m_TextureArrowUpNormal.Size.Y * ((m_Size.Y * 0.5f) / m_TextureArrowUpNormal.Size.Y)))
                                valueDown = true;
                            else // You clicked on the bottom arrow
                                valueUp = true;
                        }
                    }
                    else // The scrollbar lies horizontal
                    {
                        float scalingY;
                        if (m_VerticalImage == m_VerticalScroll)
                            scalingY = m_Size.Y / m_TextureTrackNormal_M.Size.Y;
                        else
                            scalingY = m_Size.Y / m_TextureTrackNormal_M.Size.X;

                        // Check if the arrows are drawn at full size
                        if (m_Size.X > (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scalingY)
                        {
                            // Check if you clicked on the left arrow
                            if (e.X < Position.X + (m_TextureArrowUpNormal.Size.Y * scalingY))
                                valueDown = true;

                            // Check if you clicked the right arrow
                            else if (e.X > Position.X + m_Size.X - (m_TextureArrowUpNormal.Size.Y * scalingY))
                                valueUp = true;
                        }
                        else // The arrows are not drawn at full size
                        {
                            // Check on which arrow you clicked
                            if (e.X < Position.X + (m_TextureArrowUpNormal.Size.Y * ((m_Size.X * 0.5f) / m_TextureArrowUpNormal.Size.Y)))
                                valueDown = true;
                            else // You clicked on the right arrow
                                valueUp = true;
                        }
                    }

                    if (valueDown)
                    {
                        if (Value > m_scrollAmount)
                            Value = m_Value - (int)m_scrollAmount;
                        else
                            Value = 0;
                    }
                    else if (valueUp)
                    {
                        if (Value + m_scrollAmount < m_Maximum - m_LowValue + 1)
                            Value = m_Value + (int)m_scrollAmount;
                        else
                            Value = m_Maximum - m_LowValue;
                    }
                }
            }

            // The mouse is no longer down
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

            m_MouseHover = true;

            // Check if the mouse button went down on top of the track (or thumb)
            if ((m_MouseDown) && (m_MouseDownOnArrow == false))
            {
                // Don't continue if the calculations can't be made
                if ((m_Maximum <= m_LowValue) && (m_AutoHide == false))
                    return;

                // Check in which direction the scrollbar lies
                if (m_VerticalScroll)
                {
                    float scalingX;
                    if (m_VerticalImage == m_VerticalScroll)
                        scalingX = m_Size.X / m_TextureTrackNormal_M.Size.X;
                    else
                        scalingX = m_Size.X / m_TextureTrackNormal_M.Size.Y;

                    // Calculate the arrow height
                    float arrowHeight = m_TextureArrowUpNormal.Size.Y * scalingX;

                    // Check if the thumb is being dragged
                    if (m_MouseDownOnThumb)
                    {
                        // Set the new value
                        if ((e.Y - m_MouseDownOnThumbPos.Y - Position.Y - arrowHeight) > 0)
                        {
                            // Calculate the new value
                            int value = (int)((((e.Y - m_MouseDownOnThumbPos.Y - Position.Y - arrowHeight) / (m_Size.Y - (2 * arrowHeight))) * m_Maximum) + 0.5f);

                            // If the value isn't too high then change it
                            if (value <= (m_Maximum - m_LowValue))
                                Value = value;
                            else
                                Value = m_Maximum - m_LowValue;
                        }
                        else // The mouse was above the scrollbar
                            Value = 0;
                    }
                    else // The click occured on the track
                    {
                        // If the position is positive then calculate the correct value
                        if (e.Y > Position.Y + arrowHeight)
                        {
                            // Make sure that you didn't click on the down arrow
                            if (e.Y <= Position.Y + m_Size.Y - arrowHeight)
                            {
                                // Calculate the exact position (a number between 0 and maximum)
                                float value = (((e.Y - Position.Y - arrowHeight) / (m_Size.Y - (2 * arrowHeight))) * m_Maximum);

                                // Check if you clicked above the thumb
                                if (value <= Value)
                                {
                                    float subtractValue = m_LowValue / 3.0f;

                                    // Try to place the thumb on 2/3 of the clicked position
                                    if (value >= subtractValue)
                                        Value = (int)(value - subtractValue + 0.5f);
                                    else
                                        Value = 0;
                                }
                                else // The click occured below the thumb
                                {
                                    float subtractValue = m_LowValue * 2.0f / 3.0f;

                                    // Try to place the thumb on 2/3 of the clicked position
                                    if (value <= (m_Maximum - m_LowValue + subtractValue))
                                        Value = (int)(value - subtractValue + 0.5f);
                                    else
                                        Value = m_Maximum - m_LowValue;
                                }
                            }
                        }

                        FloatRect thumbRect = GetThumbRect();

                        m_MouseDownOnThumbPos.X = e.X - Position.X - thumbRect.Left;
                        m_MouseDownOnThumbPos.Y = e.Y - Position.Y - thumbRect.Top;
                        m_MouseDownOnThumb = true;
                    }
                }
                else // the scrollbar lies horizontal
                {
                    float scalingY;
                    if (m_VerticalImage == m_VerticalScroll)
                        scalingY = m_Size.Y / m_TextureTrackNormal_M.Size.Y;
                    else
                        scalingY = m_Size.Y / m_TextureTrackNormal_M.Size.X;

                    // Calculate the arrow width
                    float arrowWidth = m_TextureArrowUpNormal.Size.Y * scalingY;

                    // Check if the thumb is being dragged
                    if (m_MouseDownOnThumb)
                    {
                        // Set the new value
                        if ((e.X - m_MouseDownOnThumbPos.X - Position.X - arrowWidth) > 0)
                        {
                            // Calculate the new value
                            int value = (int)((((e.X - m_MouseDownOnThumbPos.X - Position.X - arrowWidth) / (m_Size.X - (2 * arrowWidth))) * m_Maximum) + 0.5f);

                            // Set the new value
                            if (value <= (m_Maximum - m_LowValue))
                                Value = value;
                            else
                                Value = m_Maximum - m_LowValue;
                        }
                        else // The mouse was on the left of the scrollbar
                            Value = 0;
                    }
                    else // The click occured on the track
                    {
                        // If the position is positive then calculate the correct value
                        if (e.X > Position.X + arrowWidth)
                        {
                            // Make sure that you didn't click on the left arrow
                            if (e.X <= Position.X + m_Size.X - arrowWidth)
                            {
                                // Calculate the exact position (a number between 0 and maximum)
                                float value = (((e.X - Position.X - arrowWidth) / (m_Size.X - (2 * arrowWidth))) * m_Maximum);

                                // Check if you clicked above the thumb
                                if (value <= Value)
                                {
                                    float subtractValue = m_LowValue / 3.0f;

                                    // Try to place the thumb on 2/3 of the clicked position
                                    if (value >= subtractValue)
                                        Value = (int)(value - subtractValue + 0.5f);
                                    else
                                        Value = 0;
                                }
                                else // The click occured below the thumb
                                {
                                    float subtractValue = m_LowValue * 2.0f / 3.0f;

                                    // Try to place the thumb on 2/3 of the clicked position
                                    if (value <= (m_Maximum - m_LowValue + subtractValue))
                                        Value = (int)(value - subtractValue + 0.5f);
                                    else
                                        Value = m_Maximum - m_LowValue;
                                }
                            }
                        }

                        FloatRect thumbRect = GetThumbRect();

                        m_MouseDownOnThumbPos.X = e.X - Position.X - thumbRect.Left;
                        m_MouseDownOnThumbPos.Y = e.Y - Position.Y - thumbRect.Top;
                        m_MouseDownOnThumb = true;
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that the mouse wheel has moved while the mouse was on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseWheelMoved (MouseWheelEventArgs e)
        {
            if (Value - e.Delta < 0)
                Value = 0;
            else
                Value = Value - e.Delta;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Tells the widget that it has been focused
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnWidgetFocused()
        {
            Focused = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the position and size of the thumb
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private FloatRect GetThumbRect()
        {
            FloatRect thumbRect = new FloatRect(0, 0, m_ThumbSize.X, m_ThumbSize.Y);

            // The scaling depends on how the scrollbar lies
            if (m_VerticalScroll)
            {
                float scalingX;
                if (m_VerticalImage == m_VerticalScroll)
                    scalingX = m_Size.X / m_TextureTrackNormal_M.Size.X;
                else
                    scalingX = m_Size.X / m_TextureTrackNormal_M.Size.Y;

                // Check if the arrows are drawn at full size
                if (m_Size.Y > (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scalingX)
                {
                    // Calculate the track and thumb height
                    float realTrackHeight = m_Size.Y - ((m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scalingX);
                    thumbRect.Height = (((float)m_LowValue / m_Maximum) * realTrackHeight);

                    // Calculate the top position of the thumb
                    thumbRect.Top = (m_TextureArrowUpNormal.Size.Y * scalingX) + (((float)m_Value / (m_Maximum - m_LowValue)) * (realTrackHeight - thumbRect.Height));
                }
                else // The arrows are not drawn at full size
                {
                    thumbRect.Height = 0;
                    thumbRect.Top = (float)m_TextureArrowUpNormal.Size.Y;
                }
            }
            else // The scrollbar lies horizontal
            {
                float scalingY;
                if (m_VerticalImage == m_VerticalScroll)
                    scalingY = m_Size.Y / m_TextureTrackNormal_M.Size.Y;
                else
                    scalingY = m_Size.Y / m_TextureTrackNormal_M.Size.X;

                // Check if the arrows are drawn at full size
                if (m_Size.X > (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scalingY)
                {
                    // Calculate the track and thumb height
                    float realTrackWidth = m_Size.X - ((m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scalingY);
                    thumbRect.Width = (((float)m_LowValue / m_Maximum) * realTrackWidth);

                    // Calculate the left position of the thumb
                    thumbRect.Left = (m_TextureArrowUpNormal.Size.Y * scalingY) + (((float)m_Value / (m_Maximum - m_LowValue)) * (realTrackWidth - thumbRect.Width));
                }
                else // The arrows are not drawn at full size
                {
                    thumbRect.Width = 0;
                    thumbRect.Left = (float)m_TextureArrowUpNormal.Size.Y;
                }
            }

            return thumbRect;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // Don't draw the loading bar when it isn't needed
            if ((m_AutoHide == true) && (m_Maximum <= m_LowValue))
                return;

            // Get the scale factors
            Vector2f scaling;
            if (m_VerticalScroll == m_VerticalImage)
            {
                scaling.X = m_Size.X / m_TextureTrackNormal_M.Size.X;
                scaling.Y = m_Size.Y / m_TextureTrackNormal_M.Size.Y;
            }
            else
            {
                scaling.X = m_Size.X / m_TextureTrackNormal_M.Size.Y;
                scaling.Y = m_Size.Y / m_TextureTrackNormal_M.Size.X;
            }

            // Remember the current transformation
            states.Transform *= Transform;
            Transform oldTransform = states.Transform;

            // Set the scale of the track image
            states.Transform.Scale(scaling.X, scaling.Y);

            // Set the rotation
            if (m_VerticalScroll != m_VerticalImage)
                states.Transform.Rotate(-90, m_TextureTrackNormal_M.Size.X * 0.5f, m_TextureTrackNormal_M.Size.X * 0.5f);

            // Draw the track image
            if (m_SeparateHoverImage)
            {
                if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureTrackHover_M.sprite, states);
                else
                    target.Draw(m_TextureTrackNormal_M.sprite, states);
            }
            else // The hover image should be drawn on top of the normal image
            {
                target.Draw(m_TextureTrackNormal_M.sprite, states);

                if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureTrackHover_M.sprite, states);
            }

            // Reset the transformation (in case there was any rotation)
            states.Transform = oldTransform;

            // The calculation depends on the direction of the scrollbar
            if (m_VerticalScroll)
            {
                // Check if the arrows can be drawn at full size
                if (m_Size.Y > (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scaling.X)
                {
                    // Scale the arrow
                    states.Transform.Scale(scaling.X, scaling.X);

                    // Draw the first arrow
                    if (m_SeparateHoverImage)
                    {
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                        else
                            target.Draw(m_TextureArrowUpNormal.sprite, states);
                    }
                    else // The hover image should be drawn on top of the normal image
                    {
                        target.Draw(m_TextureArrowUpNormal.sprite, states);

                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                    }

                    // Calculate the real track height (height without the arrows)
                    float realTrackHeight = m_Size.Y - ((m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scaling.X);

                    // Calculate the scaling factor
                    float scaleY;
                    if ((m_AutoHide == false) && (m_Maximum <= m_LowValue))
                        scaleY = realTrackHeight / m_ThumbSize.Y;
                    else
                        scaleY = ((((float)(m_LowValue) / m_Maximum)) * realTrackHeight) / m_ThumbSize.Y;

                    // Set the correct transformations for the thumb
                    if (m_VerticalImage)
                    {
                        states.Transform.Translate(0, m_TextureArrowUpNormal.Size.Y + (Value * (realTrackHeight / m_Maximum)) / scaling.X);
                        states.Transform.Scale(1, scaleY);
                    }
                    else // The original image lies horizontal as well
                    {
                        states.Transform.Rotate(90, m_TextureThumbNormal.Size.Y * 0.5f, m_TextureThumbNormal.Size.Y * 0.5f);
                        states.Transform.Translate(m_TextureArrowUpNormal.Size.Y + (Value * (realTrackHeight / m_Maximum)) / scaling.X, 0);
                        states.Transform.Scale(scaleY, 1);
                    }

                    // Draw the thumb image
                    if (m_SeparateHoverImage)
                    {
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureThumbHover.sprite, states);
                        else
                            target.Draw(m_TextureThumbNormal.sprite, states);
                    }
                    else // The hover image should be drawn on top of the normal image
                    {
                        target.Draw(m_TextureThumbNormal.sprite, states);

                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureThumbHover.sprite, states);
                    }

                    // Set the transformation of the second arrow
                    states.Transform = oldTransform;
                    states.Transform.Translate(0, m_Size.Y - (m_TextureArrowDownNormal.Size.Y * scaling.X));
                    states.Transform.Scale(scaling.X, scaling.X);
                }
                else // The arrows can't be drawn at full size
                {
                    // Scale the arrow
                    states.Transform.Scale(scaling.X, (m_Size.Y * 0.5f) / m_TextureArrowUpNormal.Size.Y);

                    // Draw the first arrow
                    if (m_SeparateHoverImage)
                    {
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                        else
                            target.Draw(m_TextureArrowUpNormal.sprite, states);
                    }
                    else // The hover image should be drawn on top of the normal image
                    {
                        target.Draw(m_TextureArrowUpNormal.sprite, states);

                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                    }

                    // Change the position of the second arrow
                    states.Transform.Translate(0, m_TextureArrowUpNormal.Size.Y);
                }
            }
            else // The scrollbar lies horizontal
            {
                // Check if the arrows can be drawn at full size
                if (m_Size.X > (m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scaling.Y)
                {
                    // Scale the arrow
                    states.Transform.Scale(scaling.Y, scaling.Y);

                    // Rotate the arrow
                    states.Transform.Rotate(-90, m_TextureArrowUpNormal.Size.X * 0.5f, m_TextureArrowUpNormal.Size.X * 0.5f);

                    // Draw the first arrow
                    if (m_SeparateHoverImage)
                    {
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                        else
                            target.Draw(m_TextureArrowUpNormal.sprite, states);
                    }
                    else // The hover image should be drawn on top of the normal image
                    {
                        target.Draw(m_TextureArrowUpNormal.sprite, states);

                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                    }

                    // Calculate the real track width (width without the arrows)
                    float realTrackWidth = m_Size.X - ((m_TextureArrowUpNormal.Size.Y + m_TextureArrowDownNormal.Size.Y) * scaling.Y);

                    // Calculate the scaling factor
                    float scaleX;
                    if ((m_AutoHide == false) && (m_Maximum <= m_LowValue))
                        scaleX = realTrackWidth / m_ThumbSize.X;
                    else
                        scaleX = ((((float)(m_LowValue) / m_Maximum)) * realTrackWidth) / m_ThumbSize.X;

                    // Set the correct transformations for the thumb
                    if (m_VerticalImage)
                    {
                        states.Transform.Translate(0, m_TextureArrowUpNormal.Size.Y + (Value * (realTrackWidth / m_Maximum)) / scaling.Y);
                        states.Transform.Scale(1, scaleX);
                    }
                    else // The original image lies horizontal as well
                    {
                        states.Transform.Rotate(90, m_TextureThumbNormal.Size.Y * 0.5f, m_TextureThumbNormal.Size.Y * 0.5f);
                        states.Transform.Translate(m_TextureArrowUpNormal.Size.Y + (Value * (realTrackWidth / m_Maximum)) / scaling.Y, 0);
                        states.Transform.Scale(scaleX, 1);
                    }

                    // Draw the thumb image
                    if (m_SeparateHoverImage)
                    {
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureThumbHover.sprite, states);
                        else
                            target.Draw(m_TextureThumbNormal.sprite, states);
                    }
                    else // The hover image should be drawn on top of the normal image
                    {
                        target.Draw(m_TextureThumbNormal.sprite, states);

                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureThumbHover.sprite, states);
                    }

                    // Set the transformation of the second arrow
                    states.Transform = oldTransform;
                    states.Transform.Translate(m_Size.X - (m_TextureArrowDownNormal.Size.Y * scaling.Y), 0);
                    states.Transform.Scale(scaling.Y, scaling.Y);
                    states.Transform.Rotate(-90, m_TextureArrowUpNormal.Size.X * 0.5f, m_TextureArrowUpNormal.Size.X * 0.5f);
                }
                else // The arrows can't be drawn at full size
                {
                    // Scale the arrow
                    states.Transform.Scale((m_Size.X * 0.5f) / m_TextureArrowUpNormal.Size.Y, scaling.Y);

                    // Rotate the arrow
                    states.Transform.Rotate(-90, m_TextureArrowUpNormal.Size.X * 0.5f, m_TextureArrowUpNormal.Size.X * 0.5f);

                    // Draw the first arrow
                    if (m_SeparateHoverImage)
                    {
                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                        else
                            target.Draw(m_TextureArrowUpNormal.sprite, states);
                    }
                    else // The hover image should be drawn on top of the normal image
                    {
                        target.Draw(m_TextureArrowUpNormal.sprite, states);

                        if (m_MouseHover && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                            target.Draw(m_TextureArrowUpHover.sprite, states);
                    }

                    // Set the translation of the second arrow
                    states.Transform.Translate(0, m_TextureArrowDownNormal.Size.Y);
                }
            }

            // Draw the second arrow
            if (m_SeparateHoverImage)
            {
            if ((m_MouseHover) && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureArrowDownHover.sprite, states);
                else
                    target.Draw(m_TextureArrowDownNormal.sprite, states);
            }
            else // The hover image should be drawn on top of the normal image
            {
                target.Draw(m_TextureArrowDownNormal.sprite, states);

            if ((m_MouseHover) && (m_WidgetPhase & (byte)WidgetPhase.Hover) != 0)
                    target.Draw(m_TextureArrowDownHover.sprite, states);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the ValueChanged event</summary>
        public event EventHandler<CallbackArgs> ValueChangedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected string   m_LoadedConfigFile = "";

        // When the mouse went down, did it go down on top of the thumb? If so, where?
        protected internal bool m_MouseDownOnThumb = false;
        protected Vector2f      m_MouseDownOnThumbPos = new Vector2f();

        protected int  m_Maximum = 10;
        protected int  m_Value = 0;

        // Maximum should be above this value before the scrollbar is needed
        private int    m_LowValue = 6;

        // Is the scrollbar draw vertically?
        protected bool m_VerticalScroll = true;

        // Does the image lie vertically?
        protected bool m_VerticalImage = true;

        // How far should the value change when pressing one of the arrows?
        private uint   m_scrollAmount = 1;

        // When no scrollbar is needed, should the scrollbar be drawn or stay hidden?
        private bool   m_AutoHide = true;

        // Did the mouse went down on one of the arrows?
        private bool   m_MouseDownOnArrow = false;

        // If this is true then the L, M and R images will be used.
        // If it is false then the scrollbar is just one big image that will be stored in the M image.
        protected bool m_SplitImage = false;

        // Is there a separate hover image, or is it a semi-transparent image that is drawn on top of the others?
        protected bool m_SeparateHoverImage = false;

        // The size of the scrollbar and its thumb
        protected Vector2f m_Size;
        protected Vector2f m_ThumbSize;

        protected Impl.Sprite m_TextureTrackNormal_L = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackNormal_M = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackNormal_R = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackHover_L = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackHover_M = new Impl.Sprite();
        protected Impl.Sprite m_TextureTrackHover_R = new Impl.Sprite();
        protected Impl.Sprite m_TextureThumbNormal = new Impl.Sprite();
        protected Impl.Sprite m_TextureThumbHover = new Impl.Sprite();

        private Impl.Sprite m_TextureArrowUpNormal = new Impl.Sprite();
        private Impl.Sprite m_TextureArrowUpHover = new Impl.Sprite();

        private Impl.Sprite m_TextureArrowDownNormal = new Impl.Sprite();
        private Impl.Sprite m_TextureArrowDownHover = new Impl.Sprite();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
