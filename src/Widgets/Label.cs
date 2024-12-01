// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Label widget
    /// </summary>
    public class Label : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Label()
            : base(tguiLabel_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Label(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Label(Label copy)
            : base(copy)
        {
        }

        public ScrollbarAccessor Scrollbar => new ScrollbarAccessor(tguiScrollbarChildInterface_getScrollbar(CPointer));

        public new LabelRenderer Renderer
        {
            get => new LabelRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new LabelRenderer SharedRenderer => new LabelRenderer(tguiWidget_getSharedRenderer(CPointer));

        public string Text
        {
            get => Util.GetStringFromC_UTF32(tguiLabel_getText(CPointer));
            set => tguiLabel_setText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get => tguiLabel_getHorizontalAlignment(CPointer);
            set => tguiLabel_setHorizontalAlignment(CPointer, value);
        }

        public VerticalAlignment VerticalAlignment
        {
            get => tguiLabel_getVerticalAlignment(CPointer);
            set => tguiLabel_setVerticalAlignment(CPointer, value);
        }

        public bool AutoSize
        {
            get => tguiLabel_getAutoSize(CPointer) != 0;
            set => tguiLabel_setAutoSize(CPointer, value ? (byte)1 : (byte)0);
        }

        public float MaximumTextWidth
        {
            get => tguiLabel_getMaximumTextWidth(CPointer);
            set => tguiLabel_setMaximumTextWidth(CPointer, value);
        }

        public class DoubleClickEventArgs : EventArgs
        {
            public DoubleClickEventArgs(string text)
            {
                Text = text;
            }
            public string Text { get; }
        }
        public event EventHandler<DoubleClickEventArgs> OnDoubleClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new DoubleClickEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("DoubleClicked"), func);
                ConnectEventHandler(id, "DoubleClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("DoubleClicked", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiLabel_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiScrollbarChildInterface_getScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiLabel_getText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabel_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern HorizontalAlignment tguiLabel_getHorizontalAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabel_setHorizontalAlignment(IntPtr cPointer, HorizontalAlignment value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern VerticalAlignment tguiLabel_getVerticalAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabel_setVerticalAlignment(IntPtr cPointer, VerticalAlignment value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiLabel_getAutoSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabel_setAutoSize(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiLabel_getMaximumTextWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiLabel_setMaximumTextWidth(IntPtr cPointer, float value);

        #endregion
    }
}
