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

namespace TGUI
{
    public class AnimatedPicture : ClickableWidget
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public AnimatedPicture ()
        {
            m_AnimatedWidget = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public AnimatedPicture (AnimatedPicture copy) : base(copy)
        {
            m_FrameDuration = new List<int>(copy.m_FrameDuration);
            m_CurrentFrame  = copy.m_CurrentFrame;
            m_Playing       = copy.m_Playing;
            m_Looping       = copy.m_Looping;

            for (int i = 0; i < copy.m_Textures.Count; ++i)
            {
                m_Textures.Add (new Impl.Sprite ());
                Global.TextureManager.CopyTexture(copy.m_Textures[i], m_Textures[i]);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Destructor
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~AnimatedPicture()
        {
            foreach (Impl.Sprite texture in m_Textures)
                Global.TextureManager.RemoveTexture(texture);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Load another image/frame from a file
        /// </summary>
        ///
        /// <param name="filename">The filename of the image that you want to use as next frame.</param>
        /// <param name="frameDuration">The amount of time (in milliseconds) that the frame will be displayed on the screen.
        /// When the duration is 0 then the animation will be blocked at that frame.</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddFrame (string filename, uint frameDuration)
        {
            Impl.Sprite tempTexture = new Impl.Sprite();

            // Load the texture from the file
            Global.TextureManager.GetTexture (Global.ResourcePath + filename, tempTexture);

            // If this is the first frame then set it as the current displayed frame
            if (m_Textures.Count == 0)
            {
                m_CurrentFrame = 0;

                // Remember the size of this image
                m_Size = new Vector2f(tempTexture.Size.X, tempTexture.Size.Y);
            }

            // Add the texture
            tempTexture.sprite.Color = new Color(255, 255, 255, m_Opacity);
            m_Textures.Add(tempTexture);

            // Store the frame duration
            m_FrameDuration.Add((int)frameDuration);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the displayed image
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
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Starts or resumes playing the animation
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Play ()
        {
            // You can't start playing when no frames were loaded
            if (m_Textures.Count == 0)
                return;

            // Start playing
            m_Playing = true;

            // Reset the elapsed time
            m_AnimationTimeElapsed = 0;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Pauses the animation.
        /// You can continue the animation with the Start() function.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Pause ()
        {
            m_Playing = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Pauses the animation.
        /// When calling Start() after calling this function, the animation will restart from the first frame.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Stop ()
        {
            m_Playing = false;

            if (m_Textures.Count != 0)
                m_CurrentFrame = 0;
            else
                m_CurrentFrame = -1;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the animation at a specific frame
        /// </summary>
        ///
        /// <param name="frame">The frame that should be displayed. The number is the index of the frame, so the first frame is number 0.</param>
        ///
        /// <returns>True when the frame was selected. False when the index was too high.</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool SetFrame (uint frame)
        {
            // Check if there are no frames
            if (m_Textures.Count == 0)
            {
                m_CurrentFrame = -1;
                return false;
            }

            // Make sure the number isn't too high
            if (frame >= m_Textures.Count)
            {
                // Display the last frame
                m_CurrentFrame = m_Textures.Count - 1;
                return false;
            }

            // The frame exists
            m_CurrentFrame = (int)frame;
            return true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the current displayed frame
        /// </summary>
        ///
        /// <returns>Number of the frame that is currently displayed.
        /// The number is the index of the frame, so the first frame is number 0.
        /// If no frames were loaded then this function will return -1.</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetCurrentFrame ()
        {
            return m_CurrentFrame;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the duration of the currently displayed frame
        /// </summary>
        ///
        /// <returns>Duration of the frame that is currently displayed</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetCurrentFrameDuration ()
        {
            if (m_CurrentFrame >= 0)
                return m_FrameDuration[m_CurrentFrame];
            else
            {
                Internal.Output("TGUI warning: Can't get duration of current frame: no frames available.");
                return 0;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the amount of frames in the animation
        /// </summary>
        ///
        /// <returns>Number of frames</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint GetFrames ()
        {
            return (uint)m_Textures.Count;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes a frame from the animation
        /// </summary>
        ///
        /// <param name="frame">The number of the frame to remove. The number is the index of the frame, so the first frame is number 0.</param>
        ///
        /// <returns>True when the frame was removed. False when the index was too high.</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RemoveFrame (uint frame)
        {
            // Make sure the number isn't too high
            if (frame >= m_Textures.Count)
                return false;

            // Remove the requested frame
            Global.TextureManager.RemoveTexture(m_Textures[(int)frame]);
            m_Textures.RemoveAt((int)frame);
            m_FrameDuration.RemoveAt((int)frame);

            // If the displayed frame was behind the deleted one, then it should be shifted
            if (m_CurrentFrame >= frame)
                --m_CurrentFrame;

            return true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Remove all frames from the animation
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void RemoveAllFrames ()
        {
            // Remove the textures (if we are the only one using it)
            foreach (Impl.Sprite texture in m_Textures)
                Global.TextureManager.RemoveTexture(texture);

            // Clear the vectors
            m_Textures.Clear();
            m_FrameDuration.Clear();

            // Reset the animation
            Stop ();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Turn the looping of the animation on or off.
        /// By default, the animation will not loop.
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Looping
        {
            get
            {
                return m_Looping;
            }
            set
            {
                m_Looping = value;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns whether the animation is still playing
        /// </summary>
        ///
        /// <returns>Is the animation still playing?</returns>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsPlaying ()
        {
            return m_Playing;
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

                foreach (Impl.Sprite sprite in m_Textures)
                    sprite.sprite.Color = new Color(255, 255, 255, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Update the displayed frame
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal override void OnUpdate ()
        {
            // Only continue when you are playing
            if (m_Playing == false)
                return;

            // Check if the next frame should be displayed
            while (m_AnimationTimeElapsed > m_FrameDuration[m_CurrentFrame])
            {
                // Make sure the frame duration isn't 0
                if (m_FrameDuration[m_CurrentFrame] > 0)
                {
                    // Decrease the elapsed time
                    m_AnimationTimeElapsed -= m_FrameDuration[m_CurrentFrame];

                    // Make the next frame visible
                    if (m_CurrentFrame + 1 < m_Textures.Count)
                        ++m_CurrentFrame;
                    else
                    {
                        // If looping is enabled then stat over
                        if (m_Looping == true)
                            m_CurrentFrame = 0;
                        else
                        {
                            // Looping is disabled so stop the animation
                            m_Playing = false;
                            m_AnimationTimeElapsed = 0;
                        }

                        // The animation has finished, send a callback if needed
                        if (AnimationFinishedCallback != null)
                        {
                            m_Callback.Trigger = CallbackTrigger.AnimationFinished;
                            AnimationFinishedCallback (this, m_Callback);
                        }
                    }
                }
                else // The frame has to remain visible
                    m_AnimationTimeElapsed = 0;
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
            if (m_CurrentFrame >= 0)
            {
                states.Transform *= Transform;
                states.Transform.Scale(m_Size.X / m_Textures[m_CurrentFrame].Size.X, m_Size.Y / m_Textures[m_CurrentFrame].Size.Y);
                target.Draw(m_Textures[m_CurrentFrame].sprite, states);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Event handler for the AnimationFinished event</summary>
        public event EventHandler<CallbackArgs> AnimationFinishedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private List<Impl.Sprite> m_Textures = new List<Impl.Sprite>();
        private List<int>         m_FrameDuration = new List<int>();

        private int m_CurrentFrame = -1;

        private bool m_Playing = false;
        private bool m_Looping = false;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
