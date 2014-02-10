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

using SFML.Window;
using SFML.Graphics;

namespace TGUI
{
    public abstract class Transformable
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor, only intended for internal use
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected internal Transformable ()
        {
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        ///
        /// <param name="copy">Instance to copy</param>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Transformable (Transformable copy)
        {
            m_Position = copy.m_Position;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Position of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual Vector2f Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
                m_TransformNeedUpdate = true;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the widget
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public abstract Vector2f Size
        {
            get;
            set;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Full size of the widget
        /// </summary>
        ///
        /// FullSize will return a value equal or greater than what Size returns.
        /// If the widget would e.g. have borders around it then FullSize will be the size, including these borders.
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual Vector2f FullSize
        {
            get
            {
                return Size;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the transform
        /// </summary>
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected Transform Transform
        {
            get
            {
                if (m_TransformNeedUpdate)
                {
                    m_TransformNeedUpdate = false;

                    m_Transform = new Transform( 1,  0, m_Position.X,
                                                 0,  1, m_Position.Y,
                                                 0,  0,  1);
                }
                return m_Transform;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        internal Vector2f m_Position       = new Vector2f();
        private bool m_TransformNeedUpdate = true;
        private Transform m_Transform      = new Transform();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
