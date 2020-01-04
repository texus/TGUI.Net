/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2020 Bruno Van de Velde (vdv_b@tgui.eu)
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
using SFML.System;
using SFML.Window;

namespace TGUI
{
    /// <summary>
    /// Argument for an event about a Vector2f
    /// </summary>
    public class SignalArgsVector2f : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="val">The Vector2f value</param>
        public SignalArgsVector2f(Vector2f val)
        {
            Value = val;
        }

        /// <summary>The Vector2f value</summary>
        public Vector2f Value;
    }


    /// <summary>
    /// Argument for an event about a string
    /// </summary>
    public class SignalArgsString : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="val">The string value</param>
        public SignalArgsString(string val)
        {
            Value = val;
        }

        /// <summary>The string value</summary>
        public string Value;
    }


    /// <summary>
    /// Argument for an event about an int
    /// </summary>
    public class SignalArgsInt : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="val">The int value</param>
        public SignalArgsInt(int val)
        {
            Value = val;
        }

        /// <summary>The int value</summary>
        public int Value;
    }


    /// <summary>
    /// Argument for an event about an uint
    /// </summary>
    public class SignalArgsUInt : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="val">The unsigned int value</param>
        public SignalArgsUInt(uint val)
        {
            Value = val;
        }

        /// <summary>The unsigned int value</summary>
        public uint Value;
    }


    /// <summary>
    /// Argument for an event about a float
    /// </summary>
    public class SignalArgsFloat : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="val">The float value</param>
        public SignalArgsFloat(float val)
        {
            Value = val;
        }

        /// <summary>The float value</summary>
        public float Value;
    }


    /// <summary>
    /// Argument for an event about a range
    /// </summary>
    public class SignalArgsRange : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="start">The start of the range</param>
        /// <param name="end">The end of the range</param>
        public SignalArgsRange(float start, float end)
        {
            Start = start;
            End = end;
        }

        /// <summary>The start of the range</summary>
        public float Start;

        /// <summary>The end of the range</summary>
        public float End;
    }


    /// <summary>
    /// Argument for an event about a boolean
    /// </summary>
    public class SignalArgsBool : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="val">The boolean value</param>
        public SignalArgsBool(bool val)
        {
            Value = val;
        }

        /// <summary>The boolean value</summary>
        public bool Value;
    }


    /// <summary>
    /// Argument for an event about an item
    /// </summary>
    public class SignalArgsItem : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="item">The item in the event</param>
        /// <param name="id">The id of the item</param>
        public SignalArgsItem(string item, string id)
        {
            Item = item;
            Id = id;
        }

        /// <summary>The item in the event</summary>
        public string Item;

        /// <summary>The id of the item</summary>
        public string Id;
    }


    /// <summary>
    /// Argument for an event about a show or hide animation
    /// </summary>
    public class SignalArgsAnimation : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="type">The type of the animation</param>
        /// <param name="visible">Is the widget being shown or being hidden?</param>
        public SignalArgsAnimation(ShowAnimationType type, bool visible)
        {
            Type = type;
            Visible = visible;
        }

        /// <summary>The type of the animation</summary>
        public ShowAnimationType Type;

        /// <summary>Is the widget being shown or being hidden?</summary>
        public bool Visible;
    }


    /// <summary>
    /// Argument for the Gui.EventProcessed event
    /// </summary>
    public class SignalArgsEventProcessed : EventArgs
    {
        /// <summary>
        /// Constructor that creates the arguments object
        /// </summary>
        /// <param name="ev">The event that was send to the gui</param>
        /// <param name="processed">Whether the gui did something with this event</param>
        public SignalArgsEventProcessed(Event ev, bool processed)
        {
            Event = ev;
            Processed = processed;
        }

        /// <summary>The event that was send to the gui</summary>
        public Event Event;

        /// <summary>Whether the gui did something with this event</summary>
        public bool Processed;
    }
}
