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
using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    public class AnimatedPicture : ClickableWidget
    {
        public event EventHandler<CallbackArgs> AnimationFinishedCallback;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private List<Impl.Sprite> m_Textures = new List<Impl.Sprite>();
        private List<int>         m_FrameDuration = new List<int>();

        private int m_CurrentFrame = -1;

        private bool m_Playing = false;
        private bool m_Looping = false;


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Default constructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public AnimatedPicture ()
        {
            m_AnimatedWidget = true;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Copy constructor
        ///
        /// \param copy  Instance to copy
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public AnimatedPicture (AnimatedPicture copy) : base(copy)
        {
            m_FrameDuration = copy.m_FrameDuration;
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
        /// \brief Destructor
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~AnimatedPicture()
        {
            foreach (Impl.Sprite texture in m_Textures)
                Global.TextureManager.RemoveTexture(texture);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Load another image/frame from a file.
        ///
        /// \param filename       The filename of the image that you want to use as next frame.
        /// \param frameDuration  The amount of time that the frame will be displayed on the screen.
        ///                       When the duration is 0 (default) then the animation will be blocked at that frame.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void AddFrame (string filename, uint frameDuration)
        {
            Impl.Sprite tempTexture = new Impl.Sprite();

            // Load the texture from the file
            Global.TextureManager.GetTexture (filename, tempTexture);

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
        /// \brief Changes/Returns the size of the displayed image.
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
        /// \brief Starts or resumes playing the animation.
        ///
        /// \see pause
        /// \see stop
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
        /// \brief Pauses the animation.
        ///
        /// You can continue the animation with the start() function.
        ///
        /// \see play
        /// \see stop
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Pause ()
        {
            m_Playing = false;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Stops the animation.
        ///
        /// When calling start() after calling this function, the animation will restart from the first frame.
        ///
        /// \see play
        /// \see pause
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
        /// \brief Sets the animation at a specific frame.
        ///
        /// \param frame  The frame that should be displayed
        ///
        /// The number is the index of the frame, so the first frame is number 0.
        ///
        /// \return True when the frame was selected.
        ///         False when the index was too high.
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
        /// \brief Returns the current displayed frame.
        ///
        /// \return Number of the frame that is currently displayed.
        ///
        /// The number is the index of the frame, so the first frame is number 0.
        /// If no frames were loaded then this function will return -1.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetCurrentFrame ()
        {
            return m_CurrentFrame;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Returns the duration of the currently displayed frame.
        ///
        /// \return Duration of the frame that is currently displayed.
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
        /// \brief Returns the amount of frames in the animation.
        ///
        /// \return Number of frames
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public uint GetFrames ()
        {
            return (uint)m_Textures.Count;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Removes a frame from the animation.
        ///
        /// \param frame  The number of the frame to remove
        ///
        /// The number is the index of the frame, so the first frame is number 0.
        ///
        /// \return True when the frame was removed.
        ///         False if the index was too high.
        ///
        /// \see removeAllFrames
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
        /// \brief Remove all frames from the animation.
        ///
        /// \see removeFrame
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
        /// \brief Turn the looping of the animation on or off.
        ///
        /// By default, the animation will not loop.
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
        /// \brief Returns whether the animation is still playing.
        ///
        /// \return Is the animation still playing?
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsPlaying ()
        {
            return m_Playing;
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

                foreach (Impl.Sprite sprite in m_Textures)
                    sprite.sprite.Color = new Color(255, 255, 255, m_Opacity);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \internal
        // When the elapsed time changes then this function is called.
        //
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
        /// \internal
        // Draws the widget on the render target.
        //
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
    }
}

