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

using SFML.Window;
using SFML.Graphics;
using Tao.OpenGl;

namespace TGUI
{
    public class Label : ClickableWidget
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Label ()
        {
            m_Background.FillColor = new Color (0, 0, 0, 0);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Label (Label copy) : base(copy)
        {
            m_LoadedConfigFile = copy.m_LoadedConfigFile;
            m_Background       = copy.m_Background;
            m_Text             = new Text(copy.m_Text);
            m_AutoSize         = copy.m_AutoSize;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a Label section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Label (string configFileFilename)
        {
            m_Background.FillColor = new Color (0, 0, 0, 0);

            m_LoadedConfigFile = configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (configFileFilename, "Label");

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "textcolor")
                    m_Text.Color = configFile.ReadColor(i);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section Label in " + configFileFilename + ".");
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

                m_Text.Position = new Vector2f((float)System.Math.Floor(value.X - m_Text.GetLocalBounds().Left + 0.5f), (float)System.Math.Floor(value.Y - m_Text.GetLocalBounds().Top + 0.5f));
                m_Background.Position = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the label
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
                m_Background.Size = value;

                // You are no longer auto-sizing
                m_AutoSize = false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Text of the label</summary>
        ///
        /// When AutoSize is true (default), then the size of the label will be changed to fit the whole text.
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
                m_Text.DisplayedString = value;

                Position = Position;

                // Change the size of the label if necessary
                if (m_AutoSize)
                {
                    m_Size = new Vector2f(m_Text.FindCharacterPos((uint)m_Text.DisplayedString.Length).X - Position.X,
                                          m_Text.GetLocalBounds().Top + m_Text.GetLocalBounds().Height);

                    m_Background.Size = m_Size;
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
                return m_Text.Font;
            }
            set
            {
                m_Text.Font = value;
                Text = Text;
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
        /// The character size of the text
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

                Position = Position;

                // Change the size of the label if necessary
                if (m_AutoSize)
                {
                    m_Size = new Vector2f(m_Text.FindCharacterPos((uint)m_Text.DisplayedString.Length).X - Position.X,
                                          m_Text.GetLocalBounds().Top + m_Text.GetLocalBounds().Height);

                    m_Background.Size = m_Size;
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The background color of the text
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Color BackgroundColor
        {
            get
            {
                return m_Background.FillColor;
            }
            set
            {
                m_Background.FillColor = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Should the size of the label be changed when the text changes?
        /// </summary>
        ///
        /// When the label is in auto-size mode, the width and height of the label will be changed to fit the text.
        /// Otherwise, only the part defined by the size will be visible.
        ///
        /// The label is auto-sized by default.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool AutoSize
        {
            get
            {
                return m_AutoSize;
            }
            set
            {
                m_AutoSize = value;

                // Change the size of the label if necessary
                if (m_AutoSize)
                {
                    m_Size = new Vector2f(m_Text.FindCharacterPos((uint)m_Text.DisplayedString.Length).X - Position.X,
                                          m_Text.GetLocalBounds().Top + m_Text.GetLocalBounds().Height);

                    m_Background.Size = m_Size;
                }
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
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            // When there is no text then there is nothing to draw
            if (m_Text.DisplayedString.Length == 0)
                return;

            // Calculate the scale factor of the view
            float scaleViewX = target.Size.X / target.GetView().Size.X;
            float scaleViewY = target.Size.Y / target.GetView().Size.Y;

            // Get the global position
            Vector2f topLeftPosition = states.Transform.TransformPoint(Position - target.GetView().Center + (target.GetView().Size / 2.0f));
            Vector2f bottomRightPosition = states.Transform.TransformPoint(Position + m_Size - target.GetView().Center + (target.GetView().Size / 2.0f));

            // Get the old clipping area
            int[] scissor = new int[4];
            Gl.glGetIntegerv(Gl.GL_SCISSOR_BOX, scissor);

            // Calculate the clipping area
            int scissorLeft = System.Math.Max((int)(topLeftPosition.X * scaleViewX), scissor[0]);
            int scissorTop = System.Math.Max((int)(topLeftPosition.Y * scaleViewY), (int)(target.Size.Y) - scissor[1] - scissor[3]);
            int scissorRight = System.Math.Min((int)(bottomRightPosition.X * scaleViewX), scissor[0] + scissor[2]);
            int scissorBottom = System.Math.Min((int)(bottomRightPosition.Y * scaleViewY), (int)(target.Size.Y) - scissor[1]);

            // If the object outside the window then don't draw anything
            if (scissorRight < scissorLeft)
                scissorRight = scissorLeft;
            else if (scissorBottom < scissorTop)
                scissorTop = scissorBottom;

            // Set the clipping area
            Gl.glScissor(scissorLeft, (int)(target.Size.Y - scissorBottom), scissorRight - scissorLeft, scissorBottom - scissorTop);

            // Draw the background
            if (m_Background.FillColor.A != 0)
                target.Draw(m_Background, states);

            // Draw the text
            target.Draw(m_Text, states);

            // Reset the old clipping area
            Gl.glScissor(scissor[0], scissor[1], scissor[2], scissor[3]);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string         m_LoadedConfigFile = "";
        private RectangleShape m_Background = new RectangleShape();
        private Text           m_Text = new Text();
        private bool           m_AutoSize = true;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
