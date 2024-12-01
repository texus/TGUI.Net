// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// TextArea widget
    /// </summary>
    public class TextArea : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TextArea()
            : base(tguiTextArea_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal TextArea(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public TextArea(TextArea copy)
            : base(copy)
        {
        }

        public ScrollbarAccessor VerticalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getVerticalScrollbar(CPointer));
        public ScrollbarAccessor HorizontalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getHorizontalScrollbar(CPointer));

        public new TextAreaRenderer Renderer
        {
            get => new TextAreaRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new TextAreaRenderer SharedRenderer => new TextAreaRenderer(tguiWidget_getSharedRenderer(CPointer));

        public string Text
        {
            get => Util.GetStringFromC_UTF32(tguiTextArea_getText(CPointer));
            set => tguiTextArea_setText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public void AddText(string text)
        {
            tguiTextArea_addText(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        public string DefaultText
        {
            get => Util.GetStringFromC_UTF32(tguiTextArea_getDefaultText(CPointer));
            set => tguiTextArea_setDefaultText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public void SetSelectedText(int selectionStartIndex, int selectionEndIndex)
        {
            tguiTextArea_setSelectedText(CPointer, (UIntPtr)selectionStartIndex, (UIntPtr)selectionEndIndex);
        }

        public string GetSelectedText()
        {
            return Util.GetStringFromC_UTF32(tguiTextArea_getSelectedText(CPointer));
        }

        public int GetSelectionStart()
        {
            return (int)tguiTextArea_getSelectionStart(CPointer);
        }

        public int GetSelectionEnd()
        {
            return (int)tguiTextArea_getSelectionEnd(CPointer);
        }

        public int MaximumCharacters
        {
            get => (int)tguiTextArea_getMaximumCharacters(CPointer);
            set => tguiTextArea_setMaximumCharacters(CPointer, (UIntPtr)value);
        }

        public string TabString
        {
            get => Util.GetStringFromC_UTF32(tguiTextArea_getTabString(CPointer));
            set => tguiTextArea_setTabString(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public int CaretPosition
        {
            get => (int)tguiTextArea_getCaretPosition(CPointer);
            set => tguiTextArea_setCaretPosition(CPointer, (UIntPtr)value);
        }

        public int GetCaretLine()
        {
            return (int)tguiTextArea_getCaretLine(CPointer);
        }

        public int GetCaretColumn()
        {
            return (int)tguiTextArea_getCaretColumn(CPointer);
        }

        public bool ReadOnly
        {
            get => tguiTextArea_isReadOnly(CPointer) != 0;
            set => tguiTextArea_setReadOnly(CPointer, value ? (byte)1 : (byte)0);
        }

        public void EnableMonospacedFontOptimization(bool enable)
        {
            tguiTextArea_enableMonospacedFontOptimization(CPointer, enable ? (byte)1 : (byte)0);
        }

        public int GetLinesCount()
        {
            return (int)tguiTextArea_getLinesCount(CPointer);
        }

        public class TextChangeEventArgs : EventArgs
        {
            public TextChangeEventArgs(string text)
            {
                Text = text;
            }
            public string Text { get; }
        }
        public event EventHandler<TextChangeEventArgs> OnTextChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new TextChangeEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("TextChanged"), func);
                ConnectEventHandler(id, "TextChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("TextChanged", value);
            }
        }

        public event EventHandler OnSelectionChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("SelectionChanged"), func);
                ConnectEventHandler(id, "SelectionChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("SelectionChanged", value);
            }
        }

        public event EventHandler OnCaretPositionChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("CaretPositionChanged"), func);
                ConnectEventHandler(id, "CaretPositionChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("CaretPositionChanged", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextArea_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiDualScrollbarChildInterface_getVerticalScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiDualScrollbarChildInterface_getHorizontalScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextArea_getText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_addText(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextArea_getDefaultText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_setDefaultText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_setSelectedText(IntPtr cPointer, UIntPtr selectionStartIndex, UIntPtr selectionEndIndex);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextArea_getSelectedText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTextArea_getSelectionStart(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTextArea_getSelectionEnd(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTextArea_getMaximumCharacters(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_setMaximumCharacters(IntPtr cPointer, UIntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTextArea_getTabString(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_setTabString(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTextArea_getCaretPosition(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_setCaretPosition(IntPtr cPointer, UIntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTextArea_getCaretLine(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTextArea_getCaretColumn(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTextArea_isReadOnly(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_setReadOnly(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTextArea_enableMonospacedFontOptimization(IntPtr cPointer, byte enable);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiTextArea_getLinesCount(IntPtr cPointer);

        #endregion
    }
}
