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
using System.IO;
using System.Resources;
using System.Drawing;

namespace TGUI
{
    public class TextureManager
    {
        public static List<Impl.Texture> m_Textures = new List<Impl.Texture>();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Loads a texture.
        ///
        /// \param filename  Filename of the image to load.
        /// \param texture   The texture widget to store the loaded image.
        /// \param rect      Load only part of the image. Don't pass this parameter if you want to load the full image.
        ///
        /// The second time you call this function with the same filename, the previously loaded image will be reused.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void GetTexture(string filename, Impl.Sprite sprite, SFML.Graphics.IntRect rect = new SFML.Graphics.IntRect())
        {
            // Loop all our textures to check if we already have this one
            foreach (Impl.Texture texture in m_Textures)
            {
                if (texture.filename == filename)
                {
                    // The texture is now used at multiple places
                    texture.users++;

                    // Set the texture in the sprite
                    sprite.texture = texture;
                    sprite.sprite.Texture = texture.texture;

                    // Set only a part of the texture when asked
                    if (!rect.Equals(new SFML.Graphics.IntRect ()))
                        sprite.sprite.TextureRect = rect;

                    return;
                }
            }

            // Load the new texture
            Impl.Texture newTexture = new Impl.Texture();

            // Use file if a ResourceFile is not in use
            if (Global.ResourceManager == null)
            {
                newTexture.image = new SFML.Graphics.Image(filename);
            }
            else
            {
                if (Global.ResourceManager.GetObject(filename) is byte[])
                {
                    byte[] raw = Global.ResourceManager.GetObject(filename) as byte[];
                    MemoryStream mem = new MemoryStream(raw);
                    newTexture.image = new SFML.Graphics.Image(mem);
                }
                else if (Global.ResourceManager.GetObject(filename) is Image)
                {
                    Image raw = Global.ResourceManager.GetObject(filename) as Image;
                    MemoryStream mem = new MemoryStream();

                    // Copy the image to the Stream
                    raw.Save(mem, raw.RawFormat);

                    // Copy stream into new memory stream - prevents an AccessViolationException
                    mem = new MemoryStream(mem.ToArray());

                    newTexture.image = new SFML.Graphics.Image(mem);
                }
            }

            newTexture.texture = new SFML.Graphics.Texture (newTexture.image);
            newTexture.filename = filename;
            newTexture.users = 1;

            // Set the texture in the sprite
            sprite.texture = newTexture;
            sprite.sprite.Texture = newTexture.texture;

            // Set only a part of the texture when asked
            if (!rect.Equals(new SFML.Graphics.IntRect ()))
                sprite.sprite.TextureRect = rect;

            // Add the texture to the list
            m_Textures.Add (newTexture);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Share the image with another texture.
        ///
        /// \param textureToCopy  The original texture.
        /// \param newTexture     The texture that will get the same image as the texture that is being c
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool CopyTexture(Impl.Sprite spriteToCopy, Impl.Sprite newSprite)
        {
            // Find the texture
            foreach (Impl.Texture texture in m_Textures)
            {
                if (spriteToCopy.texture == texture)
                {
                    // The texture is now used in another place
                    texture.users++;
                    newSprite.texture = texture;
                    newSprite.sprite = new SFML.Graphics.Sprite (spriteToCopy.sprite);
                    return true;
                }
            }

            // We didn't have the texture and we can't store it without a filename
            newSprite.texture = null;
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes the texture.
        ///
        /// \param textureToRemove  The texture that should be removed.
        ///
        /// When no other texture is using the same image then the image will be removed from memory.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveTexture(Impl.Sprite sprite)
        {
            // Find the texture
            foreach (Impl.Texture texture in m_Textures)
            {
                if (sprite.texture == texture)
                {
                    // If this was the only place where the texture is used then delete it
                    if (--texture.users == 0)
                    {
                        // Remove the texture from the list
                        m_Textures.Remove(texture);
                        break;
                    }
                }
            }

            sprite.texture = null;
            sprite.sprite.Texture = null;
            return;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}

