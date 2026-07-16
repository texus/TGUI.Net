/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2026 Bruno Van de Velde (vdv_b@tgui.eu)
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

namespace TGUI
{
    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        Top,
        Center,
        Bottom
    }

    /// <summary>
    /// Alignments for how to position a widget in its parent
    /// </summary>
    public enum AutoLayout
    {
        /// <summary>Position and size need to be manually set. This is the default.</summary>
        Manual,

        /// <summary>Places the widget on on the top and sets its width to the area between Leftmost and Rightmost aligned components. Height needs to be manually set.</summary>
        Top,

        /// <summary>Places the widget on the left side and sets its height to the area between Top and Bottom aligned components. Width needs to be manually set.</summary>
        Left,

        /// <summary>Places the widget on the right side and sets its height to the area between Top and Bottom aligned components. Width needs to be manually set.</summary>
        Right,

        /// <summary>Places the widget on on the bottom and sets its width to the area between Leftmost and Rightmost aligned components. Height needs to be manually set.</summary>
        Bottom,

        /// <summary>Places the widget on the left side and sets height to 100%. Width needs to be manually set. Same as Left alignment if no widget uses Top or Bottom alignment.</summary>
        Leftmost,

        /// <summary>Places the widget on the right side and sets height to 100%. Width needs to be manually set. Same as Right alignment if no widget uses Top or Bottom alignment.</summary>
        Rightmost,

        /// <summary>Sets the position and size to fill the entire area that isn't already taken by components with the other AutoLayout values.</summary>
        Fill,
    }
}
