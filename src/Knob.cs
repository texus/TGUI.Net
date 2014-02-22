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
    public class Knob : Widget
    {
        const float pi = 3.14159265358979f;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Knob ()
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
        public Knob (Knob copy) : base(copy)
        {
            ValueChangedCallback = copy.ValueChangedCallback;

            m_loadedConfigFile = copy.m_loadedConfigFile;
            m_size             = copy.m_size;
            m_clockwiseTurning = copy.m_clockwiseTurning;
            m_imageRotation    = copy.m_imageRotation;
            m_startRotation    = copy.m_startRotation;
            m_endRotation      = copy.m_endRotation;
            m_minimum          = copy.m_minimum;
            m_value            = copy.m_value;
            m_maximum          = copy.m_maximum;

            Global.TextureManager.CopyTexture(copy.m_backgroundTexture, m_backgroundTexture);
            Global.TextureManager.CopyTexture(copy.m_foregroundTexture, m_foregroundTexture);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads the widget
        /// </summary>
        ///
        /// <param name="configFileFilename">Filename of the config file.
        /// The config file must contain a Knob section with the needed information.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Knob (string configFileFilename)
        {
            m_DraggableWidget = true;

            m_loadedConfigFile = Global.ResourcePath + configFileFilename;

            // Parse the config file
            ConfigFile configFile = new ConfigFile (m_loadedConfigFile, "Knob");

            // Find the folder that contains the config file
            string configFileFolder = m_loadedConfigFile.Substring(0, m_loadedConfigFile.LastIndexOfAny(new char[] {'/', '\\'}) + 1);

            // Loop over all properties
            for (int i = 0; i < configFile.Properties.Count; ++i)
            {
                if (configFile.Properties[i] == "backgroundimage")
                    configFile.ReadTexture(i, configFileFolder, m_backgroundTexture);
                else if (configFile.Properties[i] == "foregroundimage")
                    configFile.ReadTexture(i, configFileFolder, m_foregroundTexture);
                else if (configFile.Properties[i] == "imagerotation")
                    m_imageRotation = (float)Convert.ToDouble(configFile.Values[i]);
                else
                    Internal.Output("TGUI warning: Unrecognized property '" + configFile.Properties[i]
                                    + "' in section Knob in " + m_loadedConfigFile + ".");
            }

            // Make sure the required textures were loaded
            if ((m_backgroundTexture.texture != null) && (m_foregroundTexture.texture != null))
            {
                // Rotate the image
                m_foregroundTexture.sprite.Origin = new Vector2f(m_foregroundTexture.Size.X / 2.0f, m_foregroundTexture.Size.Y / 2.0f);
                m_foregroundTexture.sprite.Rotation = m_startRotation - m_imageRotation;

                m_foregroundTexture.sprite.Position = new Vector2f(m_backgroundTexture.Size.X / 2.0f, m_backgroundTexture.Size.Y / 2.0f);

                Size = new Vector2f(m_backgroundTexture.Size.X, m_backgroundTexture.Size.Y);
            }
            else
            {
                throw new Exception("Not all needed images were loaded for the knob. Is the Knob section in " + m_loadedConfigFile + " complete?");
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Destructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Knob ()
        {
            if (m_backgroundTexture.texture != null)   Global.TextureManager.RemoveTexture(m_backgroundTexture);
            if (m_foregroundTexture.texture != null)    Global.TextureManager.RemoveTexture(m_foregroundTexture);
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
                return m_loadedConfigFile;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Position of the knob
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
                m_Position = value;

                m_backgroundTexture.sprite.Position = value;
                m_foregroundTexture.sprite.Position = new Vector2f(value.X + (m_backgroundTexture.Size.X / 2.0f),
                                                                   value.Y + (m_backgroundTexture.Size.Y / 2.0f));
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the knob
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override Vector2f Size
        {
            get
            {
                return m_size;
            }
            set
            {
                m_size = value;

                // A negative size is not allowed for this widget
                if (m_size.X < 0) m_size.X = -m_size.X;
                if (m_size.Y < 0) m_size.Y = -m_size.Y;

                m_backgroundTexture.sprite.Scale = new Vector2f(m_size.X / m_backgroundTexture.Size.X, m_size.Y / m_backgroundTexture.Size.Y);
                m_foregroundTexture.sprite.Scale = new Vector2f(m_size.X / m_backgroundTexture.Size.X, m_size.Y / m_backgroundTexture.Size.Y);

                // Reposition the foreground image
                m_foregroundTexture.sprite.Position = new Vector2f(Position.X + (m_size.X / 2.0f), Position.Y + (m_size.Y / 2.0f));
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The start rotation, which is the place where the value should be minimal
        /// </summary>
        ///
        /// The rotation is a number in the interval [0,360[, for which 0 to to the right and the rotation goes counter-clockwise.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public float StartRotation
        {
            get
            {
                return m_startRotation;
            }
            set
            {
                while (value >= 360)
                    value -= 360;
                while (value < 0)
                    value += 360;

                m_startRotation = value;

                // The knob might have to point in a different direction even though it has the same value
                RecalculateRotation();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The end rotation, which is the place where the value should be maximal
        /// </summary>
        ///
        /// The rotation is a number in the interval [0,360[, for which 0 to to the right and the rotation goes counter-clockwise.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public float EndRotation
        {
            get
            {
                return m_endRotation;
            }
            set
            {
                while (value >= 360)
                    value -= 360;
                while (value < 0)
                    value += 360;

                m_endRotation = value;

                // The knob might have to point in a different direction even though it has the same value
                RecalculateRotation();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The value for when the knob would be rotated in the direction of StartRotation
        /// </summary>
        ///
        /// The knob will be rotated to keep representing the value correctly.
        ///
        /// When the value is too small then it will be changed to this minimum.
        /// When the maximum value is lower than the new minimum then it will be changed to this new minimum value.
        /// The default minimum value is 0.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Minimum
        {
            get
            {
                return m_minimum;
            }
            set
            {
                if (m_minimum != value)
                {
                    // Set the new minimum
                    m_minimum = value;

                    // The maximum can't be below the minimum
                    if (m_maximum < m_minimum)
                        Maximum = m_minimum;

                    // When the value is below the minimum then adjust it
                    if (m_value < m_minimum)
                        Value = m_minimum;

                    // The knob might have to point in a different direction even though it has the same value
                    RecalculateRotation();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The value for when the knob would be rotated in the direction of EndRotation
        /// </summary>
        ///
        /// The knob will be rotated to keep representing the value correctly.
        ///
        /// When the value is too big then it will be changed to this maximum.
        /// When the minimum value is higher than the new maximum then it will be changed to this new maximum value.
        /// The default maximum value is 360.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Maximum
        {
            get
            {
                return m_maximum;
            }
            set
            {
                if (m_maximum != value)
                {
                    // Set the new maximum
                    if (value > 0)
                        m_maximum = value;
                    else
                        m_maximum = 1;

                    // The minimum can't be below the maximum
                    if (m_minimum > m_maximum)
                        m_minimum = m_maximum;

                    // When the value is above the maximum then adjust it
                    if (m_value > m_maximum)
                        Value = m_maximum;

                    // The knob might have to point in a different direction even though it has the same value
                    RecalculateRotation();
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Current value of the knob
        /// </summary>
        ///
        /// The knob will be rotated to correctly represent this new value.
        ///
        /// The value can't be smaller than the minimum or bigger than the maximum.
        /// The default value is 0.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Value
        {
            get
            {
                return m_value;
            }
            set
            {
                if (m_value != value)
                {
                    // Set the new value
                    m_value = value;

                    // When the value is below the minimum or above the maximum then adjust it
                    if (m_value < m_minimum)
                        m_value = m_minimum;
                    else if (m_value > m_maximum)
                        m_value = m_maximum;

                    // The knob might have to point in a different direction even though it has the same value
                    RecalculateRotation();

                    // Add the callback (if the user requested it)
                    if (ValueChangedCallback != null)
                    {
                        m_Callback.Trigger = CallbackTrigger.ValueChanged;
                        m_Callback.Value = m_value;
                        ValueChangedCallback (this, m_Callback);
                    }
                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Does the spin button lie vertically?
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool ClockwiseTurning
        {
            get
            {
                return m_clockwiseTurning;
            }
            set
            {
                m_clockwiseTurning = value;

                // The knob might have to point in a different direction even though it has the same value
                RecalculateRotation();
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

                m_backgroundTexture.sprite.Color = new Color(255, 255, 255, m_Opacity);
                m_foregroundTexture.sprite.Color = new Color(255, 255, 255, m_Opacity);
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
            // Check if the mouse is on top of the widget
            if (Transform.TransformRect(new FloatRect(0, 0, Size.X, Size.Y)).Contains(x, y))
            {
                Vector2f scaling = new Vector2f (m_size.X / m_backgroundTexture.Size.X, m_size.Y / m_backgroundTexture.Size.Y);

                // Only return true when the pixel under the mouse isn't transparent
                if (!m_backgroundTexture.IsTransparentPixel((uint)((x - Position.X) / scaling.X), (uint)((y - Position.Y) / scaling.Y)))
                    return true;
            }

            if (m_MouseHover)
                MouseLeftWidget();

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
            // Set the mouse down flag
            m_MouseDown = true;

            // Change the value of the knob depending on where you clicked
            MouseMoveEvent move = new MouseMoveEvent();
            move.X = e.X;
            move.Y = e.Y;
            OnMouseMoved (new MouseMoveEventArgs(move));
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
        /// Tells the widget that the mouse has moved on top of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnMouseMoved (MouseMoveEventArgs e)
        {
            if (m_MouseHover == false)
                MouseEnteredWidget();

            m_MouseHover = true;

            // Get the current position
            Vector2f centerPosition = Position + new Vector2f(m_size.X / 2.0f, m_size.Y / 2.0f);

            // Check if the mouse button is down
            if (m_MouseDown)
            {
                // Find out the direction that the knob should now point
                float angle;
                if (e.X == centerPosition.X)
                {
                    if (e.Y > centerPosition.Y)
                        angle = 270;
                    else if (e.Y < centerPosition.Y)
                        angle = 90;
                    else // You were able to click in the exact center, ignore this click
                        return;
                }
                else
                {
                    angle = (float)Math.Atan2(centerPosition.Y - e.Y, e.X - centerPosition.X) * 180.0f / pi;
                    if (angle < 0)
                        angle += 360;
                }

                // The angle might lie on a part where it isn't allowed
                if (angle > m_startRotation)
                {
                    if ((angle < m_endRotation) && (m_clockwiseTurning))
                    {
                        if ((angle - m_startRotation) <= (m_endRotation - angle))
                            angle = m_startRotation;
                        else
                            angle = m_endRotation;
                    }
                    else if (angle > m_endRotation)
                    {
                        if (((m_startRotation > m_endRotation) && (m_clockwiseTurning))
                            || ((m_startRotation < m_endRotation) && (!m_clockwiseTurning)))
                        {
                            if (Math.Min(angle - m_startRotation, 360 - angle + m_startRotation) <= Math.Min(angle - m_endRotation, 360 - angle + m_endRotation))
                                angle = m_startRotation;
                            else
                                angle = m_endRotation;
                        }
                    }
                }
                else if (angle < m_startRotation)
                {
                    if (angle < m_endRotation)
                    {
                        if (((m_startRotation > m_endRotation) && (m_clockwiseTurning))
                            || ((m_startRotation < m_endRotation) && (!m_clockwiseTurning)))
                        {
                            if (Math.Min(m_startRotation - angle, 360 - m_startRotation + angle) <= Math.Min(m_endRotation - angle, 360 -m_endRotation + angle))
                                angle = m_startRotation;
                            else
                                angle = m_endRotation;
                        }
                    }
                    else if ((angle > m_endRotation) && (!m_clockwiseTurning))
                    {
                        if ((m_startRotation - angle) <= (angle - m_endRotation))
                            angle = m_startRotation;
                        else
                            angle = m_endRotation;
                    }
                }

                // Give the image the correct rotation
                if (m_imageRotation > angle)
                    m_foregroundTexture.sprite.Rotation = m_imageRotation - angle;
                else
                    m_foregroundTexture.sprite.Rotation = 360 - angle + m_imageRotation;

                // Calculate the difference in degrees between the start and end rotation
                float allowedAngle = 0;
                if (m_startRotation == m_endRotation)
                    allowedAngle = 360;
                else
                {
                    if (((m_endRotation > m_startRotation) && (m_clockwiseTurning))
                        || ((m_endRotation < m_startRotation) && (!m_clockwiseTurning)))
                    {
                        allowedAngle = 360 - Math.Abs(m_endRotation - m_startRotation);
                    }
                    else if (((m_endRotation > m_startRotation) && (!m_clockwiseTurning))
                             || ((m_endRotation < m_startRotation) && (m_clockwiseTurning)))
                    {
                        allowedAngle = Math.Abs(m_endRotation - m_startRotation);
                    }
                }

                // Calculate the right value
                if (m_clockwiseTurning)
                {
                    if (angle < m_startRotation)
                        Value = (int)(((m_startRotation - angle) / allowedAngle * (m_maximum - m_minimum)) + m_minimum);
                    else
                    {
                        if (angle == m_startRotation)
                            Value = m_minimum;
                        else
                            Value = (int)((((360.0 - angle) + m_startRotation) / allowedAngle * (m_maximum - m_minimum)) + m_minimum);
                    }
                }
                else // counter-clockwise
                {
                    if (angle >= m_startRotation)
                        Value = (int)(((angle - m_startRotation) / allowedAngle * (m_maximum - m_minimum)) + m_minimum);
                    else
                    {
                        Value = (int)(((angle + (360.0 - m_startRotation)) / allowedAngle * (m_maximum - m_minimum)) + m_minimum);
                    }
                }
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
            Focused = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Recalculates the rotation of the knob
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void RecalculateRotation()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the widget on the render target
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(m_backgroundTexture.sprite, states);
            target.Draw(m_foregroundTexture.sprite, states);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the ValueChanged event</summary>
        public event EventHandler<CallbackArgs> ValueChangedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private string m_loadedConfigFile = "";

        private Impl.Sprite m_backgroundTexture = new Impl.Sprite();
        private Impl.Sprite m_foregroundTexture = new Impl.Sprite();

        private Vector2f m_size = new Vector2f ();

        private bool   m_clockwiseTurning = true; // Does rotating clockwise increment the value?

        private float m_imageRotation = 0;
        private float m_startRotation = 90;
        private float m_endRotation = 90;

        private int    m_minimum = 0;
        private int    m_maximum = 360;
        private int    m_value = 0;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
