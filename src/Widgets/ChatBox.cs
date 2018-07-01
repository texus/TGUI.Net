/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2016 Bruno Van de Velde (vdv_b@tgui.eu)
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
    public class ChatBox : Widget
    {
        public ChatBox()
            : base(tguiChatBox_create())
        {
        }

        protected internal ChatBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public ChatBox(ChatBox copy)
            : base(copy)
        {
        }

        public new ChatBoxRenderer Renderer
        {
            get { return new ChatBoxRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        public new ChatBoxRenderer SharedRenderer
        {
            get { return new ChatBoxRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        public void AddLine(string text)
        {
            tguiChatBox_addLine(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        public void AddLine(string text, Color color)
        {
            tguiChatBox_addLineWithColor(CPointer, Util.ConvertStringForC_UTF32(text), color);
        }

        public string GetLine(uint lineIndex)
        {
            return Util.GetStringFromC_UTF32(tguiChatBox_getLine(CPointer, lineIndex));
        }

        public Color GetLineColor(uint lineIndex)
        {
            return tguiChatBox_getLineColor(CPointer, lineIndex);
        }

        public bool RemoveLine(uint lineIndex)
        {
            return tguiChatBox_removeLine(CPointer, lineIndex);
        }

        public void RemoveAllLines()
        {
            tguiChatBox_removeAllLines(CPointer);
        }

        public uint GetLineAmount()
        {
            return tguiChatBox_getLineAmount(CPointer);
        }

        public uint LineLimit
        {
            get { return tguiChatBox_getLineLimit(CPointer); }
            set { tguiChatBox_setLineLimit(CPointer, value); }
        }

        public uint TextSize
        {
            get { return tguiChatBox_getTextSize(CPointer); }
            set { tguiChatBox_setTextSize(CPointer, value); }
        }

        public Color TextColor
        {
            get { return tguiChatBox_getTextColor(CPointer); }
            set { tguiChatBox_setTextColor(CPointer, value); }
        }

        public bool LinesStartFromTop
        {
            get { return tguiChatBox_getLinesStartFromTop(CPointer); }
            set { tguiChatBox_setLinesStartFromTop(CPointer, value); }
        }

        public bool NewLinesBelowOthers
        {
            get { return tguiChatBox_getNewLinesBelowOthers(CPointer); }
            set { tguiChatBox_setNewLinesBelowOthers(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiChatBox_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChatBox_addLine(IntPtr cPointer, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChatBox_addLineWithColor(IntPtr cPointer, IntPtr text, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiChatBox_getLine(IntPtr cPointer, uint lineIndex);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiChatBox_getLineColor(IntPtr cPointer, uint lineIndex);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiChatBox_removeLine(IntPtr cPointer, uint lineIndex);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChatBox_removeAllLines(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiChatBox_getLineAmount(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChatBox_setLineLimit(IntPtr cPointer, uint limit);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiChatBox_getLineLimit(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChatBox_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiChatBox_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChatBox_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Color tguiChatBox_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChatBox_setLinesStartFromTop(IntPtr cPointer, bool startFromTop);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiChatBox_getLinesStartFromTop(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiChatBox_setNewLinesBelowOthers(IntPtr cPointer, bool newLinesBelowOthers);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiChatBox_getNewLinesBelowOthers(IntPtr cPointer);

        #endregion
    }
}
