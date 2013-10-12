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
using SFML.Graphics;

namespace TGUI
{
    public class TextureManager
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Loads a texture
        /// </summary>
        ///
        /// <param name="filename">Filename of the image to load</param>
        /// <param name="texture">The texture object to store the loaded image</param>
        /// <param name="rect">Load only this part of the image.</param>
        ///
        /// The second time you call this function with the same filename, the previously loaded image will be reused.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void GetTexture(string filename, Impl.Sprite sprite, SFML.Graphics.IntRect rect = new SFML.Graphics.IntRect())
        {
            // Look if we already had this image
            ImageMapData data;
            if (m_ImageMap.TryGetValue(filename, out data))
            {
                // Loop all our textures to find the one containing the image
                foreach (Impl.Texture tex in data.data)
                {
                    // Only reuse the texture when the exact same part of the image is used
                    if ((tex.rect.Left == rect.Left) && (tex.rect.Top == rect.Top)
                        && (tex.rect.Width == rect.Width) && (tex.rect.Height == rect.Height))
                    {
                        // The texture is now used at multiple places
                        ++(tex.users);

                        // We already have the texture, so pass the data
                        sprite.texture = tex;

                        // Set the texture in the sprite
                        sprite.sprite.Texture = tex.texture;
                        return;
                    }
                }
            }
            else // The image doesn't exist yet
            {
                data = new ImageMapData ();
                data.data = new List<Impl.Texture> ();

                m_ImageMap.Add (filename, data);
            }

            // Add new data to the list
            Impl.Texture texture = new Impl.Texture();
            sprite.texture = texture;
            sprite.texture.image = data.image;
            sprite.texture.rect = rect;
            data.data.Add(texture);

            // Load the image
            if (Global.ResourceManager == null)
            {
                sprite.texture.image = new SFML.Graphics.Image (filename);
            }
            else
            {
                if (Global.ResourceManager.GetObject(filename) is byte[])
                {
                    byte[] raw = Global.ResourceManager.GetObject(filename) as byte[];
                    MemoryStream mem = new MemoryStream(raw);
                    sprite.texture.image = new SFML.Graphics.Image(mem);
                }
                else if (Global.ResourceManager.GetObject(filename) is System.Drawing.Image)
                {
                    System.Drawing.Image raw = Global.ResourceManager.GetObject(filename) as System.Drawing.Image;
                    MemoryStream mem = new MemoryStream();

                    // Copy the image to the Stream
                    raw.Save(mem, raw.RawFormat);

                    // Copy stream into new memory stream - prevents an AccessViolationException
                    mem = new MemoryStream(mem.ToArray());

                    sprite.texture.image = new SFML.Graphics.Image(mem);
                }
            }

            // Create a texture from the image
            if ((rect.Left == 0) && (rect.Top == 0) && (rect.Width == 0) && (rect.Height == 0))
                sprite.texture.texture = new SFML.Graphics.Texture (sprite.texture.image);
            else
                sprite.texture.texture = new SFML.Graphics.Texture (sprite.texture.image, rect);

            // Set the texture in the sprite
            sprite.sprite.Texture = sprite.texture.texture;

            // Set the other members of the data
            sprite.texture.filename = filename;
            sprite.texture.users = 1;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Share the image with another sprite
        /// </summary>
        ///
        /// <param name="spriteToCopy">The original sprite</param>
        /// <param name="newSprite">The sprite that will get the same image as the sprite that is being copied</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool CopyTexture(Impl.Sprite spriteToCopy, Impl.Sprite newSprite)
        {
            // Ignore null pointers
            if (spriteToCopy.texture == null)
            {
                newSprite.texture = null;
                return true;
            }

            // Loop all our textures to check if we already have this one
            foreach (var pair in m_ImageMap)
            {
                ImageMapData data = pair.Value;

                foreach (Impl.Texture texture in data.data)
                {
                    // Check if the pointer points to our texture
                    if (texture == spriteToCopy.texture)
                    {
                        // The texture is now used at multiple places
                        ++(texture.users);
                        newSprite.texture = spriteToCopy.texture;
                        newSprite.sprite = new Sprite (spriteToCopy.sprite);
                        return true;
                    }
                }
            }

            Internal.Output("TGUI warning: Can't copy texture that wasn't loaded by TextureManager.");
            return false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the sprite
        /// </summary>
        ///
        /// <param name="sprite">The sprite that should be removed</param>
        ///
        /// When no other sprite is using the same image then the image will be removed from memory.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveTexture(Impl.Sprite sprite)
        {
            // Ignore already removed sprites
            if (sprite.texture == null)
                return;

            // Loop all our textures to check which one it is
            foreach (var pair in m_ImageMap)
            {
                ImageMapData data = pair.Value;

                foreach (Impl.Texture texture in data.data)
                {
                    // Check if the pointer points to our texture
                    if (texture == sprite.texture)
                    {
                        // If this was the only place where the texture is used then delete it
                        if (--(texture.users) == 0)
                        {
                            int usage = 0;
                            foreach (Impl.Texture t in data.data)
                            {
                                if (t.image == data.image)
                                    usage++;
                            }

                            // Remove the texture from the list, or even the whole image if it isn't used anywhere else
                            if (usage == 1)
                                m_ImageMap.Remove(pair.Key);
                            else
                                data.data.Remove(texture);
                        }

                        // The pointer is now useless
                        sprite.texture = null;
                        return;
                    }
                }
            }

//            Internal.Output("TGUI warning: Can't remove texture that wasn't loaded by TextureManager.");
            return;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public struct ImageMapData
        {
            public SFML.Graphics.Image image;
            public List<Impl.Texture> data;
        };

        internal Dictionary<string, ImageMapData> m_ImageMap = new Dictionary<string, ImageMapData>();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
