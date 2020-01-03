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
using System.Security;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SFML.Graphics;

namespace TGUI
{
    /// <summary>
    /// Chat box widget
    /// </summary>
    public class ChatBox : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatBox()
            : base(tguiChatBox_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ChatBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ChatBox(ChatBox copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets or sets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After retrieving the renderer, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new ChatBoxRenderer Renderer
        {
            get { return new ChatBoxRenderer(tguiWidget_getRenderer(CPointer)); }
            set { SetRenderer(value.Data); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new ChatBoxRenderer SharedRenderer
        {
            get { return new ChatBoxRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Adds a new line of text to the chat box
        /// </summary>
        /// <remarks>
        /// The whole text passed to this function will be considered as one line for the getLine and removeLine functions,
        /// even if it is too long and gets split over multiple lines.
        ///
        /// The default text color and style will be used.
        /// </remarks>
        /// <param name="text">Text that will be added to the chat box</param>
        public void AddLine(string text)
        {
            tguiChatBox_addLine(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        /// <summary>
        /// Adds a new line of text to the chat box
        /// </summary>
        /// <remarks>
        /// The whole text passed to this function will be considered as one line for the getLine and removeLine functions,
        /// even if it is too long and gets split over multiple lines.
        ///
        /// The default text style will be used.
        /// </remarks>
        /// <param name="text">Text that will be added to the chat box</param>
        /// <param name="color">Color of the text</param>
        public void AddLine(string text, Color color)
        {
            tguiChatBox_addLineWithColor(CPointer, Util.ConvertStringForC_UTF32(text), color);
        }

        /// <summary>
        /// Adds a new line of text to the chat box
        /// </summary>
        /// <remarks>
        /// The whole text passed to this function will be considered as one line for the getLine and removeLine functions,
        /// even if it is too long and gets split over multiple lines.
        /// </remarks>
        /// <param name="text">Text that will be added to the chat box</param>
        /// <param name="color">Color of the text</param>
        /// <param name="style">Text style</param>
        public void AddLine(string text, Color color, Text.Styles style)
        {
            tguiChatBox_addLineWithColorAndStyle(CPointer, Util.ConvertStringForC_UTF32(text), color, style);
        }

        /// <summary>
        /// Returns the contents of the requested line
        /// </summary>
        /// <param name="lineIndex">The index of the line of which you request the contents. The first line has index 0.</param>
        /// <returns>
        /// The contents of the requested line. An empty string will be returned when the index is too high.
        /// </returns>
        public string GetLine(uint lineIndex)
        {
            return Util.GetStringFromC_UTF32(tguiChatBox_getLine(CPointer, lineIndex));
        }

        /// <summary>
        /// Returns the color of the requested line
        /// </summary>
        /// <param name="lineIndex">The index of the line of which you request the color. The first line has index 0.</param>
        /// <returns>
        /// The color of the requested line. The default color (set with setTextColor) when the index is too high.
        /// </returns>
        public Color GetLineColor(uint lineIndex)
        {
            return tguiChatBox_getLineColor(CPointer, lineIndex);
        }

        /// <summary>
        /// Returns the text style of the requested line
        /// </summary>
        /// <param name="lineIndex">The index of the line of which you request the text style. The first line has index 0.</param>
        /// <returns>
        /// The text style of the requested line. The default style (set with setTextStyle) when the index is too high.
        /// </returns>
        public Text.Styles GetLineTextStyle(uint lineIndex)
        {
            return tguiChatBox_getLineTextStyle(CPointer, lineIndex);
        }

        /// <summary>
        /// Removes the requested line
        /// </summary>
        /// <param name="lineIndex">The index of the line that should be removed. The first line has index 0.</param>
        /// <returns>
        /// True if the line was removed, false if no such line existed (index too high)
        /// </returns>
        public bool RemoveLine(uint lineIndex)
        {
            return tguiChatBox_removeLine(CPointer, lineIndex);
        }

        /// <summary>
        /// Removes all lines from the chat box
        /// </summary>
        public void RemoveAllLines()
        {
            tguiChatBox_removeAllLines(CPointer);
        }

        /// <summary>
        /// Returns the amount of lines in the chat box
        /// </summary>
        /// <returns>
        /// Number of lines in the chat box
        /// </returns>
        public uint GetLineAmount()
        {
            return tguiChatBox_getLineAmount(CPointer);
        }

        /// <summary>
        /// Sets a maximum amount of lines in the chat box
        /// </summary>
        /// <remarks>
        /// Only the last maxLines lines will be kept. Lines above those will be removed.
        /// Set to 0 to disable the line limit (default).
        /// </remarks>
        public uint LineLimit
        {
            get { return tguiChatBox_getLineLimit(CPointer); }
            set { tguiChatBox_setLineLimit(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the character size of the text
        /// </summary>
        public uint TextSize
        {
            get { return tguiChatBox_getTextSize(CPointer); }
            set { tguiChatBox_setTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the default text color
        /// </summary>
        public Color TextColor
        {
            get { return tguiChatBox_getTextColor(CPointer); }
            set { tguiChatBox_setTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the default text style
        /// </summary>
        public Text.Styles TextStyle
        {
            get { return tguiChatBox_getTextStyle(CPointer); }
            set { tguiChatBox_setTextStyle(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the first lines start from the top or from the bottom of the chat box
        /// </summary>
        /// <remarks>
        /// Note that this only makes a difference when the lines don't fill the entire chat box.
        /// This does not change the order of the lines.
        /// </remarks>
        public bool LinesStartFromTop
        {
            get { return tguiChatBox_getLinesStartFromTop(CPointer); }
            set { tguiChatBox_setLinesStartFromTop(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whehter new lines are added below the other lines or above them
        /// </summary>
        /// <remarks>
        /// By default the new lines are always added below the others.
        /// </remarks>
        public bool NewLinesBelowOthers
        {
            get { return tguiChatBox_getNewLinesBelowOthers(CPointer); }
            set { tguiChatBox_setNewLinesBelowOthers(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the thumb position of the scrollbar
        /// </summary>
        public uint ScrollbarValue
        {
            get { return tguiChatBox_getScrollbarValue(CPointer); }
            set { tguiChatBox_setScrollbarValue(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChatBox_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_addLine(IntPtr cPointer, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_addLineWithColor(IntPtr cPointer, IntPtr text, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_addLineWithColorAndStyle(IntPtr cPointer, IntPtr text, Color color, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChatBox_getLine(IntPtr cPointer, uint lineIndex);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiChatBox_getLineColor(IntPtr cPointer, uint lineIndex);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiChatBox_getLineTextStyle(IntPtr cPointer, uint lineIndex);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiChatBox_removeLine(IntPtr cPointer, uint lineIndex);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_removeAllLines(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiChatBox_getLineAmount(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_setLineLimit(IntPtr cPointer, uint limit);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiChatBox_getLineLimit(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiChatBox_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiChatBox_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_setTextStyle(IntPtr cPointer, Text.Styles style);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Text.Styles tguiChatBox_getTextStyle(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_setLinesStartFromTop(IntPtr cPointer, bool startFromTop);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiChatBox_getLinesStartFromTop(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_setNewLinesBelowOthers(IntPtr cPointer, bool newLinesBelowOthers);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiChatBox_getNewLinesBelowOthers(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChatBox_setScrollbarValue(IntPtr cPointer, uint newValue);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiChatBox_getScrollbarValue(IntPtr cPointer);

        #endregion
    }
}
