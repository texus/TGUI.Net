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
    /// <summary>
    /// Triggers that cause a callback.
    ///
    /// The description of the triggers also mention which CallbackArgs are set.
    /// The Id and Trigger members are always set, so they are not explicitly mentioned.
    /// </summary>
    ///
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum CallbackTrigger
    {
        /// <summary>Callback triggered when a widget is focused. Sets no special CallbackArgs.</summary>
        Focused,

        /// <summary>Callback triggered when a widget is unfocused. Sets no special CallbackArgs.</summary>
        Unfocused,

        /// <summary>Callback triggered when the mouse entered the widget. Sets no special CallbackArgs.</summary>
        MouseEntered,

        /// <summary>Callback triggered when the mouse left the widget. Sets no special CallbackArgs.</summary>
        MouseLeft,

        /// <summary>Callback triggered when the left mouse button is pressed on a widget. Sets the Mouse in CallbackArgs.</summary>
        LeftMousePressed,

        /// <summary>Callback triggered when the left mouse button is released on a widget. Sets the Mouse in CallbackArgs.</summary>
        LeftMouseReleased,

        /// <summary>Callback triggered when the left mouse button is clicked on a widget. Sets the Mouse in CallbackArgs.</summary>
        LeftMouseClicked,

        /// <summary>Callback triggered when the space key is pressed while the widget was focused. Sets no special CallbackArgs.</summary>
        SpaceKeyPressed,

        /// <summary>Callback triggered when the return key (enter key) is pressed while the widget was focused. Sets no special CallbackArgs.</summary>
        ReturnKeyPressed,

        /// <summary>Callback triggered when the checkbox was checked. Sets no special CallbackArgs.</summary>
        Checked,

        /// <summary>Callback triggered when the checkbox was unchecked. Sets no special CallbackArgs.</summary>
        Unchecked,

        /// <summary>Callback triggered when the text was changed. Sets the Text in CallbackArgs.</summary>
        TextChanged,

        /// <summary>Callback triggered when the value changed. Sets the Value (or Value2d) in CallbackArgs.</summary>
        ValueChanged,

        /// <summary>Callback triggered when the loading bar is full. Sets the Value in CallbackArgs.</summary>
        LoadingBarFull,

        /// <summary>Callback triggered when another item was selected in the list. Sets the Text and Value in CallbackArgs.</summary>
        ItemSelected,

        /// <summary>Callback triggered when another tab was selected. Sets the Mouse, Text and Value in CallbackArgs.</summary>
        TabChanged,

        /// <summary>Callback triggered when a menu item of the menu bar was clicked. Sets the Text and Index in CallbackArgs.</summary>
        MenuItemClicked,

        /// <summary>Callback triggered when the thumb of Slider2d got reset. Sets the Value2d in CallbackArgs.</summary>
        ThumbReturnedToCenter,

        /// <summary>Callback triggered when the child window is closed. Sets no special CallbackArgs.</summary>
        Closed,

        /// <summary>Callback triggered when the child window is moved. Sets the Position in CallbackArgs.</summary>
        Moved,

        /// <summary>Callback triggered when a button in the message box was clicked. Sets the Text in CallbackArgs.</summary>
        ButtonClicked,

        /// <summary>Callback triggered when the animation of an AnimatedPicture finished. Sets no special CallbackArgs.</summary>
        AnimationFinished
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class CallbackArgs : EventArgs
    {
        /// <summary>The CallbackId of to the widget. It is used to identify from which widget the callback came from. Set for all callbacks.</summary>
        public uint Id;

        /// <summary>The trigger that caused the callback. Set for all callbacks.</summary>
        public CallbackTrigger Trigger;

        /// <summary>Position of the mouse</summary>
        public Vector2i Mouse;

        /// <summary>Text passed by the callback</summary>
        public string Text;

        /// <summary>Position passed by the callback</summary>
        public Vector2f Position;

        /// <summary>Whether the checkbox or radio button is checked</summary>
        public bool Checked;

        /// <summary>Value passed by the callback</summary>
        public int Value;

        /// <summary>Index passed by the callback</summary>
        public uint Index;

        /// <summary>Value2d passed by the callback</summary>
        public Vector2f Value2d;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
