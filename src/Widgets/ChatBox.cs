// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// ChatBox widget
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

        public ScrollbarAccessor Scrollbar => new ScrollbarAccessor(tguiScrollbarChildInterface_getScrollbar(CPointer));

        public new ChatBoxRenderer Renderer
        {
            get => new ChatBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new ChatBoxRenderer SharedRenderer => new ChatBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        public void AddLine(string text)
        {
            tguiChatBox_addLine(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        public void AddLine(string text, Color? color)
        {
            tguiChatBox_addLineWithColor(CPointer, Util.ConvertStringForC_UTF32(text), Util.ConvertColorForC(color));
        }

        public void AddLine(string text, Color? color, TextStyles style)
        {
            tguiChatBox_addLineWithColorAndStyle(CPointer, Util.ConvertStringForC_UTF32(text), Util.ConvertColorForC(color), style);
        }

        public string GetLine(int lineIndex)
        {
            return Util.GetStringFromC_UTF32(tguiChatBox_getLine(CPointer, (UIntPtr)lineIndex));
        }

        public Color? GetLineColor(int lineIndex)
        {
            return Util.GetColorFromC(tguiChatBox_getLineColor(CPointer, (UIntPtr)lineIndex));
        }

        public TextStyles GetLineTextStyle(int lineIndex)
        {
            return tguiChatBox_getLineTextStyle(CPointer, (UIntPtr)lineIndex);
        }

        public bool RemoveLine(int lineIndex)
        {
            return tguiChatBox_removeLine(CPointer, (UIntPtr)lineIndex) != 0;
        }

        public void RemoveAllLines()
        {
            tguiChatBox_removeAllLines(CPointer);
        }

        public int LineAmount
        {
            get => (int)tguiChatBox_getLineAmount(CPointer);
        }

        public int LineLimit
        {
            get => (int)tguiChatBox_getLineLimit(CPointer);
            set => tguiChatBox_setLineLimit(CPointer, (UIntPtr)value);
        }

        public Color? TextColor
        {
            get => Util.GetColorFromC(tguiChatBox_getTextColor(CPointer));
            set => tguiChatBox_setTextColor(CPointer, Util.ConvertColorForC(value));
        }

        public TextStyles TextStyle
        {
            get => tguiChatBox_getTextStyle(CPointer);
            set => tguiChatBox_setTextStyle(CPointer, value);
        }

        public bool LinesStartFromTop
        {
            get => tguiChatBox_getLinesStartFromTop(CPointer) != 0;
            set => tguiChatBox_setLinesStartFromTop(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool NewLinesBelowOthers
        {
            get => tguiChatBox_getNewLinesBelowOthers(CPointer) != 0;
            set => tguiChatBox_setNewLinesBelowOthers(CPointer, value ? (byte)1 : (byte)0);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChatBox_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarChildInterface_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_addLine(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_addLineWithColor(IntPtr cPointer, IntPtr text, ColorCTGUI color);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_addLineWithColorAndStyle(IntPtr cPointer, IntPtr text, ColorCTGUI color, TextStyles style);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChatBox_getLine(IntPtr cPointer, UIntPtr lineIndex);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChatBox_getLineColor(IntPtr cPointer, UIntPtr lineIndex);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiChatBox_getLineTextStyle(IntPtr cPointer, UIntPtr lineIndex);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiChatBox_removeLine(IntPtr cPointer, UIntPtr lineIndex);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_removeAllLines(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiChatBox_getLineAmount(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiChatBox_getLineLimit(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_setLineLimit(IntPtr cPointer, UIntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiChatBox_getTextColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_setTextColor(IntPtr cPointer, ColorCTGUI value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TextStyles tguiChatBox_getTextStyle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_setTextStyle(IntPtr cPointer, TextStyles value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiChatBox_getLinesStartFromTop(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_setLinesStartFromTop(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiChatBox_getNewLinesBelowOthers(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChatBox_setNewLinesBelowOthers(IntPtr cPointer, byte value);

        #endregion
    }
}
