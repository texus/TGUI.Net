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

namespace TGUI
{
    public class Picture : ClickableWidget
    {
        private string  m_LoadedFilename = "";

        protected Impl.Sprite  m_Sprite = new Impl.Sprite();


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Constructor, only intended for internal use
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Picture ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Picture (Picture copy) : base(copy)
        {
            m_LoadedFilename = copy.m_LoadedFilename;

            Global.TextureManager.CopyTexture(copy.m_Sprite, m_Sprite);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the picture by loading an image from a file.
        /// </summary>
        /// <param name="filename">The absolute or relative filename of the image that should be loaded.</param>
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Picture (string filename)
        {
            m_LoadedFilename = filename;

            Global.TextureManager.GetTexture (filename, m_Sprite);

            // Remember the size of the texture
            Size = new Vector2f(m_Sprite.Size.X, m_Sprite.Size.Y);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Picture()
        {
            Global.TextureManager.RemoveTexture(m_Sprite);
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
                m_Sprite.Position = value;
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
                m_Sprite.sprite.Scale = new Vector2f(m_Size.X / m_Sprite.Size.X, m_Size.Y / m_Sprite.Size.Y);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns the filename of the config file that was used to load the widget.
        ///
        /// \return Filename of loaded config file.
        ///         Empty string when no config file was loaded yet.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string LoadedFilename
        {
            get
            {
                return m_LoadedFilename;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Enable or disable the smooth filter.
        ///
        /// When the filter is activated, the texture appears smoother so that pixels are less noticeable.
        /// However if you want the texture to look exactly the same as its source file, you should leave it disabled.
        /// The smooth filter is disabled by default.
        ///
        /// \param smooth True to enable smoothing, false to disable it
        ///
        /// \see isSmooth
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Smooth
        {
            get
            {
                return m_Sprite.texture.texture.Smooth;
            }
            set
            {
                m_Sprite.texture.texture.Smooth = value;
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

                m_Sprite.sprite.Color = new Color(255, 255, 255, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override bool MouseOnWidget(float x, float y)
        {
            if (Transform.TransformRect(new FloatRect(0, 0, Size.X, Size.Y)).Contains(x, y))
            {
                Vector2f scaling = new Vector2f (Size.X / m_Sprite.Size.X, Size.Y / m_Sprite.Size.Y);

                // Only return true when the pixel under the mouse isn't transparent
                if (!m_Sprite.IsTransparentPixel((uint)((x - Position.X) / scaling.X), (uint)((y - Position.Y) / scaling.Y)))
                    return true;
            }

            if (m_MouseHover)
                MouseLeftWidget();

            m_MouseHover = false;
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // Draws the widget on the render target.
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw (m_Sprite.sprite, states);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

