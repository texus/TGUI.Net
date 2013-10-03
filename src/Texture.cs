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

namespace TGUI
{
    namespace Impl
    {
        public class Texture
        {
            public SFML.Graphics.Image   image = null;
            public SFML.Graphics.Texture texture = null;
            public SFML.Graphics.IntRect rect = new SFML.Graphics.IntRect(0, 0, 0, 0);

            public string filename = "";
            public uint   users = 0;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public class Sprite
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            public Texture texture = null;

            public SFML.Graphics.Sprite sprite = new SFML.Graphics.Sprite();


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// \brief Default constructor
            ///
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public Sprite()
            {
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// \brief Copy constructor
            ///
            /// \param copy  Instance to copy
            ///
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public Sprite(Sprite copy)
            {
                texture = copy.texture;
                sprite.Texture = texture.texture;
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public Vector2f Position
            {
                get
                {
                    return sprite.Position;
                }
                set
                {
                    sprite.Position = value;
                }
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public Vector2u Size
            {
                get
                {
                    if (texture != null)
                        return texture.texture.Size;
                    else
                        return new Vector2u (0, 0);
                }
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool IsTransparentPixel(uint x, uint y)
            {
                if (texture.image.GetPixel (x + (uint)texture.rect.Left, y + (uint)texture.rect.Top).A == 0)
                    return true;
                else
                    return false;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}

