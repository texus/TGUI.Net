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
using SFML.Window;

namespace TGUI
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public enum CallbackTrigger
    {
        // In comment you can find which part of the callback is used when this trigger is past.
        // Id and Trigger are always filled and are not explicitly mentioned.

        Focused,            //
        Unfocused,          //

        MouseEntered,       //
        MouseLeft,          //

        LeftMousePressed,   // Mouse
        LeftMouseReleased,  // Mouse
        LeftMouseClicked,   // Mouse

        SpaceKeyPressed,    //
        ReturnKeyPressed,   //

        Checked,            //
        Unchecked,          //

        TextChanged,        // Text

        ValueChanged,       // Value / Value2d

        LoadingBarFull,     // Value

        ItemSelected,       // Text, Value

        TabChanged,         // Mouse, Text, Value

        MenuItemClicked,    // Text, Index

        ThumbReturnedToCenter, // Value2d

        // ChildWindow
        Closed,             //
        Moved,              // Position

        // AnimatedPicture
        AnimationFinished
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class CallbackArgs : EventArgs
    {
        // The callback id that was passed to the widget. It is used to identify from what widget the callback came from.
        public uint Id;

        // How did the callbak occur?
        public CallbackTrigger Trigger;

        // When the mouse has something to do with the callback then this data will be filled
        public Vector2i Mouse;

        public string Text;

        // Used when moving child windows.
        public Vector2f Position;

        // Used in any callback coming from Checkbox or RadioButton.
        public bool Checked;

        public int Value;

        public uint Index;

        public Vector2f Value2d;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

