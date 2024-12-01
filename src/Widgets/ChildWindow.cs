// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public enum ChildWindowCloseBehavior
    {
        None,
        Hide,
        Remove,
    }

    /// <summary>
    /// ChildWindow widget
    /// </summary>
    public class ChildWindow : Container
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ChildWindow()
            : base(tguiChildWindow_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ChildWindow(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ChildWindow(ChildWindow copy)
            : base(copy)
        {
        }

        public new ChildWindowRenderer Renderer
        {
            get => new ChildWindowRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new ChildWindowRenderer SharedRenderer => new ChildWindowRenderer(tguiWidget_getSharedRenderer(CPointer));

        public void SetClientSize(Vector2f size)
        {
            tguiChildWindow_setClientSize(CPointer, size);
        }

        public void SetClientSize(Layout2d layout)
        {
            tguiChildWindow_setClientSizeFromLayout(CPointer, layout.CPointer);
        }

        public Vector2f GetClientSize()
        {
            return tguiChildWindow_getClientSize(CPointer);
        }

        public Vector2f MaximumSize
        {
            get => tguiChildWindow_getMaximumSize(CPointer);
            set => tguiChildWindow_setMaximumSize(CPointer, value);
        }

        public Vector2f MinimumSize
        {
            get => tguiChildWindow_getMinimumSize(CPointer);
            set => tguiChildWindow_setMinimumSize(CPointer, value);
        }

        public string Title
        {
            get => Util.GetStringFromC_UTF32(tguiChildWindow_getTitle(CPointer));
            set => tguiChildWindow_setTitle(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public int TitleTextSize
        {
            get => (int)tguiChildWindow_getTitleTextSize(CPointer);
            set => tguiChildWindow_setTitleTextSize(CPointer, (uint)value);
        }

        public HorizontalAlignment TitleAlignment
        {
            get => tguiChildWindow_getTitleAlignment(CPointer);
            set => tguiChildWindow_setTitleAlignment(CPointer, value);
        }

        public int TitleButtons
        {
            get => (int)tguiChildWindow_getTitleButtons(CPointer);
            set => tguiChildWindow_setTitleButtons(CPointer, (uint)value);
        }

        public ChildWindowCloseBehavior CloseBehavior
        {
            get => tguiChildWindow_getCloseBehavior(CPointer);
            set => tguiChildWindow_setCloseBehavior(CPointer, value);
        }

        public bool Resizable
        {
            get => tguiChildWindow_isResizable(CPointer) != 0;
            set => tguiChildWindow_setResizable(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool KeepInParent
        {
            get => tguiChildWindow_getKeepInParent(CPointer) != 0;
            set => tguiChildWindow_setKeepInParent(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool PositionLocked
        {
            get => tguiChildWindow_isPositionLocked(CPointer) != 0;
            set => tguiChildWindow_setPositionLocked(CPointer, value ? (byte)1 : (byte)0);
        }

        public event EventHandler OnMousePress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("MousePressed"), func);
                ConnectEventHandler(id, "MousePressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("MousePressed", value);
            }
        }

        public class CloseEventArgs : EventArgs
        {
            public CloseEventArgs(ChildWindow window)
            {
                Window = window;
            }
            public ChildWindow Window { get; }
        }
        public event EventHandler<CloseEventArgs> OnClose
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackChildWindow func = (IntPtr windowCPointer) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    using var childWindow = new ChildWindow(windowCPointer);value(sender, new CloseEventArgs(childWindow));
                };
                uint id = tguiWidget_signalChildWindowConnect(CPointer, Util.ConvertStringForC_UTF32("Closed"), func);
                ConnectEventHandler(id, "Closed", value, func);
            }
            remove
            {
                DisconnectEventHandler("Closed", value);
            }
        }

        public class MinimizeEventArgs : EventArgs
        {
            public MinimizeEventArgs(ChildWindow window)
            {
                Window = window;
            }
            public ChildWindow Window { get; }
        }
        public event EventHandler<MinimizeEventArgs> OnMinimize
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackChildWindow func = (IntPtr windowCPointer) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    using var childWindow = new ChildWindow(windowCPointer);value(sender, new MinimizeEventArgs(childWindow));
                };
                uint id = tguiWidget_signalChildWindowConnect(CPointer, Util.ConvertStringForC_UTF32("Minimized"), func);
                ConnectEventHandler(id, "Minimized", value, func);
            }
            remove
            {
                DisconnectEventHandler("Minimized", value);
            }
        }

        public class MaximizeEventArgs : EventArgs
        {
            public MaximizeEventArgs(ChildWindow window)
            {
                Window = window;
            }
            public ChildWindow Window { get; }
        }
        public event EventHandler<MaximizeEventArgs> OnMaximize
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackChildWindow func = (IntPtr windowCPointer) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    using var childWindow = new ChildWindow(windowCPointer);value(sender, new MaximizeEventArgs(childWindow));
                };
                uint id = tguiWidget_signalChildWindowConnect(CPointer, Util.ConvertStringForC_UTF32("Maximized"), func);
                ConnectEventHandler(id, "Maximized", value, func);
            }
            remove
            {
                DisconnectEventHandler("Maximized", value);
            }
        }

        public class EscapeKeyPressEventArgs : EventArgs
        {
            public EscapeKeyPressEventArgs(ChildWindow window)
            {
                Window = window;
            }
            public ChildWindow Window { get; }
        }
        public event EventHandler<EscapeKeyPressEventArgs> OnEscapeKeyPress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackChildWindow func = (IntPtr windowCPointer) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    using var childWindow = new ChildWindow(windowCPointer);value(sender, new EscapeKeyPressEventArgs(childWindow));
                };
                uint id = tguiWidget_signalChildWindowConnect(CPointer, Util.ConvertStringForC_UTF32("EscapeKeyPressed"), func);
                ConnectEventHandler(id, "EscapeKeyPressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("EscapeKeyPressed", value);
            }
        }

        public class ClosingEventArgs : EventArgs
        {
            public ClosingEventArgs(bool abort)
            {
                Abort = abort;
            }
            public bool Abort { get; set; }
        }
        public unsafe event EventHandler<ClosingEventArgs> OnClosing
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackBoolPtr func = (byte* ptr) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    var e = new ClosingEventArgs(*ptr != 0);
                    value(sender, e);
                    *ptr = e.Abort ? (byte)1 : (byte)0;
                };
                uint id = tguiWidget_signalBoolPtrConnect(CPointer, Util.ConvertStringForC_UTF32("Closing"), func);
                ConnectEventHandler(id, "Closing", value, func);
            }
            remove
            {
                DisconnectEventHandler("Closing", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindow_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setClientSize(IntPtr cPointer, Vector2f size);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setClientSizeFromLayout(IntPtr cPointer, IntPtr layout);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiChildWindow_getClientSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiChildWindow_getMaximumSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setMaximumSize(IntPtr cPointer, Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiChildWindow_getMinimumSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setMinimumSize(IntPtr cPointer, Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiChildWindow_getTitle(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setTitle(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiChildWindow_getTitleTextSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setTitleTextSize(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern HorizontalAlignment tguiChildWindow_getTitleAlignment(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setTitleAlignment(IntPtr cPointer, HorizontalAlignment value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiChildWindow_getTitleButtons(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setTitleButtons(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ChildWindowCloseBehavior tguiChildWindow_getCloseBehavior(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setCloseBehavior(IntPtr cPointer, ChildWindowCloseBehavior value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiChildWindow_isResizable(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setResizable(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiChildWindow_getKeepInParent(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setKeepInParent(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiChildWindow_isPositionLocked(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiChildWindow_setPositionLocked(IntPtr cPointer, byte value);

        #endregion
    }
}
