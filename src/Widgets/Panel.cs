// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Panel widget
    /// </summary>
    public class Panel : Group
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Panel()
            : base(tguiPanel_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Panel(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Panel(Panel copy)
            : base(copy)
        {
        }

        public new PanelRenderer Renderer
        {
            get => new PanelRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new PanelRenderer SharedRenderer => new PanelRenderer(tguiWidget_getSharedRenderer(CPointer));

        public static void SetEventBubbling(bool useEventBubbling)
        {
            tguiPanel_setEventBubbling(useEventBubbling ? (byte)1 : (byte)0);
        }

        public static bool GetEventBubbling()
        {
            return tguiPanel_getEventBubbling() != 0;
        }

        public class MousePressEventArgs : EventArgs
        {
            public MousePressEventArgs(Vector2f mousePos)
            {
                MousePos = mousePos;
            }
            public Vector2f MousePos { get; }
        }
        public event EventHandler<MousePressEventArgs> OnMousePress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new MousePressEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("MousePressed"), func);
                ConnectEventHandler(id, "MousePressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("MousePressed", value);
            }
        }

        public class MouseReleaseEventArgs : EventArgs
        {
            public MouseReleaseEventArgs(Vector2f mousePos)
            {
                MousePos = mousePos;
            }
            public Vector2f MousePos { get; }
        }
        public event EventHandler<MouseReleaseEventArgs> OnMouseRelease
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new MouseReleaseEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("MouseReleased"), func);
                ConnectEventHandler(id, "MouseReleased", value, func);
            }
            remove
            {
                DisconnectEventHandler("MouseReleased", value);
            }
        }

        public class ClickEventArgs : EventArgs
        {
            public ClickEventArgs(Vector2f mousePos)
            {
                MousePos = mousePos;
            }
            public Vector2f MousePos { get; }
        }
        public event EventHandler<ClickEventArgs> OnClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ClickEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("Clicked"), func);
                ConnectEventHandler(id, "Clicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("Clicked", value);
            }
        }

        public class DoubleClickEventArgs : EventArgs
        {
            public DoubleClickEventArgs(Vector2f mousePos)
            {
                MousePos = mousePos;
            }
            public Vector2f MousePos { get; }
        }
        public event EventHandler<DoubleClickEventArgs> OnDoubleClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new DoubleClickEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("DoubleClicked"), func);
                ConnectEventHandler(id, "DoubleClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("DoubleClicked", value);
            }
        }

        public class RightMousePressEventArgs : EventArgs
        {
            public RightMousePressEventArgs(Vector2f mousePos)
            {
                MousePos = mousePos;
            }
            public Vector2f MousePos { get; }
        }
        public event EventHandler<RightMousePressEventArgs> OnRightMousePress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new RightMousePressEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("RightMousePressed"), func);
                ConnectEventHandler(id, "RightMousePressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("RightMousePressed", value);
            }
        }

        public class RightMouseReleaseEventArgs : EventArgs
        {
            public RightMouseReleaseEventArgs(Vector2f mousePos)
            {
                MousePos = mousePos;
            }
            public Vector2f MousePos { get; }
        }
        public event EventHandler<RightMouseReleaseEventArgs> OnRightMouseRelease
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new RightMouseReleaseEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("RightMouseReleased"), func);
                ConnectEventHandler(id, "RightMouseReleased", value, func);
            }
            remove
            {
                DisconnectEventHandler("RightMouseReleased", value);
            }
        }

        public class RightClickEventArgs : EventArgs
        {
            public RightClickEventArgs(Vector2f mousePos)
            {
                MousePos = mousePos;
            }
            public Vector2f MousePos { get; }
        }
        public event EventHandler<RightClickEventArgs> OnRightClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new RightClickEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("RightClicked"), func);
                ConnectEventHandler(id, "RightClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("RightClicked", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPanel_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiPanel_setEventBubbling(byte useEventBubbling);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiPanel_getEventBubbling();

        #endregion
    }
}
