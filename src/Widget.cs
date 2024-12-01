// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class Widget : ObjectBase
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Widget(IntPtr cPointer)
            : base(cPointer)
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackInt(int val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackUInt(uint val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackNUInt(UIntPtr val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackBool(byte val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackFloat(float val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackColor(ColorCTGUI val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackString(IntPtr val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackVector2f(Vector2f val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackFloatRect(FloatRect val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected unsafe delegate void UnmanagedCallbackBoolPtr(byte* val);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackRange(float val1, float val2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected unsafe delegate void UnmanagedCallbackTabSelectionChanging(int index, byte* vetoed);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackChildWindow(IntPtr windowCPointer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackItem(int item);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackPanelListBoxItem(int item);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected unsafe delegate void UnmanagedCallbackFileDialogPaths(UIntPtr count, IntPtr* paths);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackShowEffect(ShowEffectType type, byte show);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void UnmanagedCallbackAnimationType(AnimationType type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected unsafe delegate void UnmanagedCallbackItemHierarchy(UIntPtr count, IntPtr* hierarchy);

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Widget(Widget copy)
            : base(tguiWidget_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiWidget_destroy(CPointer);
        }

        public AutoLayout AutoLayout
        {
            get => tguiWidget_getAutoLayout(CPointer);
            set => tguiWidget_setAutoLayout(CPointer, value);
        }

        public uint Connect(string signalName, Action callbackFunc)
        {
            UnmanagedCallback func = () => { callbackFunc(); };
            uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32(signalName), func);
            ConnectEventHandler(id, signalName, callbackFunc, func);
            return id;
        }

        public bool Disconnect(string signalName, uint id)
        {
            if (Util.Callbacks.ContainsKey(CPointer) && Util.Callbacks[CPointer].ContainsKey(signalName))
            {
                // List<UnmanagedCallbackInfo>
                var callbacks = Util.Callbacks[CPointer][signalName];
                foreach (Util.UnmanagedCallbackInfo callbackInfo in callbacks)
                {
                    if (callbackInfo.id == id)
                    {
                        callbacks.Remove(callbackInfo);
                        break;
                    }
                }

                if (callbacks.Count == 0)
                    Util.Callbacks[CPointer].Remove(signalName);
                if (Util.Callbacks[CPointer].Count == 0)
                    Util.Callbacks.Remove(CPointer);
            }

            return tguiWidget_signalDisconnect(CPointer, Util.ConvertStringForC_UTF32(signalName), id) != 0;
        }

        public void DisconnectAll(string signalName)
        {
            if (Util.Callbacks.ContainsKey(CPointer))
            {
                Util.Callbacks[CPointer].Remove(signalName);
                if (Util.Callbacks[CPointer].Count == 0)
                    Util.Callbacks.Remove(CPointer);
            }

            tguiWidget_signalDisconnectAll(CPointer, Util.ConvertStringForC_UTF32(signalName));
        }

        public bool SetSignalEnabled(string signalName, bool enabled)
        {
            return tguiWidget_setSignalEnabled(CPointer, Util.ConvertStringForC_UTF32(signalName), enabled ? (byte)1 : (byte)0) != 0;
        }

        public bool IsSignalEnabled(string signalName)
        {
            return tguiWidget_isSignalEnabled(CPointer, Util.ConvertStringForC_UTF32(signalName)) != 0;
        }

        public WidgetRenderer Renderer
        {
            get => new WidgetRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public WidgetRenderer SharedRenderer => new WidgetRenderer(tguiWidget_getSharedRenderer(CPointer));

        /// <summary>
        /// Sets new renderer data for the widget. The renderer determines how the widget looks.
        /// </summary>
        /// <param name="rendererData">new renderer data</param>
        /// <remarks>
        /// The renderer data is shared with this widget. When the data is changed, this widget will be updated as well.
        /// </remarks>
        public void SetRenderer(RendererData rendererData)
        {
            if (tguiWidget_setRenderer(CPointer, rendererData.CPointer) == 0)
                throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
        }

        public void ShowWithEffect(ShowEffectType type, Duration duration)
        {
            tguiWidget_showWithEffect(CPointer, type, duration);
        }

        public void HideWithEffect(ShowEffectType type, Duration duration)
        {
            tguiWidget_hideWithEffect(CPointer, type, duration);
        }

        public void MoveWithAnimation(Vector2f position, Duration duration)
        {
            tguiWidget_moveWithAnimation(CPointer, position, duration);
        }

        public void ResizeWithAnimation(Vector2f size, Duration duration)
        {
            tguiWidget_resizeWithAnimation(CPointer, size, duration);
        }

        public Widget? ToolTip
        {
            get => Util.GetWidgetFromC(tguiWidget_getToolTip(CPointer));
            set => tguiWidget_setToolTip(CPointer, value is null ? IntPtr.Zero : value.CPointer);
        }

        public Widget? Parent
        {
            get => Util.GetWidgetFromC(tguiWidget_getParent(CPointer));
        }

        public Gui? ParentGui
        {
            get
            {
                IntPtr guiCPointer = tguiWidget_getParentGui(CPointer);
                try
                {
                    return Util.Guis[guiCPointer];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
        }

        public void UpdateTime(Duration duration)
        {
            tguiWidget_updateTime(CPointer, duration);
        }

        protected void ConnectEventHandler(uint id, string signalName, Delegate handler, Delegate helperFunc)
        {
            if (id == 0)
                throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));

            if (!Util.Callbacks.ContainsKey(CPointer))
                Util.Callbacks.Add(CPointer, new Dictionary<string, List<Util.UnmanagedCallbackInfo>>());
            if (!Util.Callbacks[CPointer].ContainsKey(signalName))
                Util.Callbacks[CPointer].Add(signalName, new List<Util.UnmanagedCallbackInfo>());

            Util.Callbacks[CPointer][signalName].Add(new Util.UnmanagedCallbackInfo(id, handler, helperFunc));
        }

        protected void DisconnectEventHandler(string signalName, Delegate handler)
        {
            if (!Util.Callbacks.ContainsKey(CPointer) || !Util.Callbacks[CPointer].ContainsKey(signalName))
                return;

            uint id = 0;
            foreach (Util.UnmanagedCallbackInfo info in Util.Callbacks[CPointer][signalName])
            {
                if (info.callbackFunc.Equals(handler))
                {
                    id = info.id;
                    break;
                }
            }

            if (id != 0)
                Disconnect(signalName, id);
        }

        public void SetPosition(Vector2f position)
        {
            tguiWidget_setPosition(CPointer, position);
        }

        public void SetPosition(Layout2d layout)
        {
            tguiWidget_setPositionFromLayout(CPointer, layout.CPointer);
        }

        public Vector2f GetPosition()
        {
            return tguiWidget_getPosition(CPointer);
        }

        public Vector2f GetAbsolutePosition()
        {
            return tguiWidget_getAbsolutePosition(CPointer);
        }

        public Vector2f GetAbsolutePosition(Vector2f offset)
        {
            return tguiWidget_getAbsolutePositionWithOffset(CPointer, offset);
        }

        public Vector2f GetWidgetOffset()
        {
            return tguiWidget_getWidgetOffset(CPointer);
        }

        public void SetWidth(float width)
        {
            tguiWidget_setWidth(CPointer, width);
        }

        public void SetWidth(Layout layout)
        {
            tguiWidget_setWidthFromLayout(CPointer, layout.CPointer);
        }

        public void SetHeight(float height)
        {
            tguiWidget_setHeight(CPointer, height);
        }

        public void SetHeight(Layout layout)
        {
            tguiWidget_setHeightFromLayout(CPointer, layout.CPointer);
        }

        public void SetSize(Vector2f size)
        {
            tguiWidget_setSize(CPointer, size);
        }

        public void SetSize(Layout2d layout)
        {
            tguiWidget_setSizeFromLayout(CPointer, layout.CPointer);
        }

        public Vector2f GetSize()
        {
            return tguiWidget_getSize(CPointer);
        }

        public Vector2f GetFullSize()
        {
            return tguiWidget_getFullSize(CPointer);
        }

        public Vector2f Origin
        {
            get => tguiWidget_getOrigin(CPointer);
            set => tguiWidget_setOrigin(CPointer, value);
        }

        public void SetScale(Vector2f origin)
        {
            tguiWidget_setScale(CPointer, origin);
        }

        public void SetScale(Vector2f scale, Vector2f origin)
        {
            tguiWidget_setScaleWithOrigin(CPointer, scale, origin);
        }

        public Vector2f GetScale()
        {
            return tguiWidget_getScale(CPointer);
        }

        public Vector2f GetScaleOrigin()
        {
            return tguiWidget_getScaleOrigin(CPointer);
        }

        public void SetRotation(float angle)
        {
            tguiWidget_setRotation(CPointer, angle);
        }

        public void SetRotation(float angle, Vector2f origin)
        {
            tguiWidget_setRotationWithOrigin(CPointer, angle, origin);
        }

        public float GetRotation()
        {
            return tguiWidget_getRotation(CPointer);
        }

        public Vector2f GetRotationOrigin()
        {
            return tguiWidget_getRotationOrigin(CPointer);
        }

        public bool Visible
        {
            get => tguiWidget_isVisible(CPointer) != 0;
            set => tguiWidget_setVisible(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool Enabled
        {
            get => tguiWidget_isEnabled(CPointer) != 0;
            set => tguiWidget_setEnabled(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool Focused
        {
            get => tguiWidget_isFocused(CPointer) != 0;
            set => tguiWidget_setFocused(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool Focusable
        {
            get => tguiWidget_isFocusable(CPointer) != 0;
            set => tguiWidget_setFocusable(CPointer, value ? (byte)1 : (byte)0);
        }

        public string GetWidgetType()
        {
            return Util.GetStringFromC_UTF32(tguiWidget_getWidgetType(CPointer));
        }

        public void MoveToFront()
        {
            tguiWidget_moveToFront(CPointer);
        }

        public void MoveToBack()
        {
            tguiWidget_moveToBack(CPointer);
        }

        public string UserData
        {
            get => Util.GetStringFromC_UTF32(tguiWidget_getUserData(CPointer));
            set => tguiWidget_setUserData(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public bool HasUserData()
        {
            return tguiWidget_hasUserData(CPointer) != 0;
        }

        public bool IsAnimationPlaying()
        {
            return tguiWidget_isAnimationPlaying(CPointer) != 0;
        }

        public int TextSize
        {
            get => (int)tguiWidget_getTextSize(CPointer);
            set => tguiWidget_setTextSize(CPointer, (uint)value);
        }

        public string WidgetName
        {
            get => Util.GetStringFromC_UTF32(tguiWidget_getWidgetName(CPointer));
            set => tguiWidget_setWidgetName(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public Cursor.Type MouseCursor
        {
            get => tguiWidget_getMouseCursor(CPointer);
            set => tguiWidget_setMouseCursor(CPointer, value);
        }

        public Widget? NavigationUp
        {
            get => Util.GetWidgetFromC(tguiWidget_getNavigationUp(CPointer));
            set => tguiWidget_setNavigationUp(CPointer, value is null ? IntPtr.Zero : value.CPointer);
        }

        public Widget? NavigationDown
        {
            get => Util.GetWidgetFromC(tguiWidget_getNavigationDown(CPointer));
            set => tguiWidget_setNavigationDown(CPointer, value is null ? IntPtr.Zero : value.CPointer);
        }

        public Widget? NavigationLeft
        {
            get => Util.GetWidgetFromC(tguiWidget_getNavigationLeft(CPointer));
            set => tguiWidget_setNavigationLeft(CPointer, value is null ? IntPtr.Zero : value.CPointer);
        }

        public Widget? NavigationRight
        {
            get => Util.GetWidgetFromC(tguiWidget_getNavigationRight(CPointer));
            set => tguiWidget_setNavigationRight(CPointer, value is null ? IntPtr.Zero : value.CPointer);
        }

        public bool IgnoreMouseEvents
        {
            get => tguiWidget_getIgnoreMouseEvents(CPointer) != 0;
            set => tguiWidget_setIgnoreMouseEvents(CPointer, value ? (byte)1 : (byte)0);
        }

        public void FinishAllAnimations()
        {
            tguiWidget_finishAllAnimations(CPointer);
        }

        public void SetAutoLayoutUpdateEnabled(bool enabled)
        {
            tguiWidget_setAutoLayoutUpdateEnabled(CPointer, enabled ? (byte)1 : (byte)0);
        }

        public bool IsMouseDown()
        {
            return tguiWidget_isMouseDown(CPointer) != 0;
        }

        public bool IsMouseOnWidget(Vector2f pos)
        {
            return tguiWidget_isMouseOnWidget(CPointer, pos) != 0;
        }

        public class PositionChangeEventArgs : EventArgs
        {
            public PositionChangeEventArgs(Vector2f position)
            {
                Position = position;
            }
            public Vector2f Position { get; }
        }
        public event EventHandler<PositionChangeEventArgs> OnPositionChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new PositionChangeEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("PositionChanged"), func);
                ConnectEventHandler(id, "PositionChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("PositionChanged", value);
            }
        }

        public class SizeChangeEventArgs : EventArgs
        {
            public SizeChangeEventArgs(Vector2f size)
            {
                Size = size;
            }
            public Vector2f Size { get; }
        }
        public event EventHandler<SizeChangeEventArgs> OnSizeChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackVector2f func = (Vector2f vec) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new SizeChangeEventArgs(vec));
                };
                uint id = tguiWidget_signalVector2fConnect(CPointer, Util.ConvertStringForC_UTF32("SizeChanged"), func);
                ConnectEventHandler(id, "SizeChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("SizeChanged", value);
            }
        }

        public event EventHandler OnFocus
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("Focused"), func);
                ConnectEventHandler(id, "Focused", value, func);
            }
            remove
            {
                DisconnectEventHandler("Focused", value);
            }
        }

        public event EventHandler OnonUnfocus
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("Unfocused"), func);
                ConnectEventHandler(id, "Unfocused", value, func);
            }
            remove
            {
                DisconnectEventHandler("Unfocused", value);
            }
        }

        public event EventHandler OnMouseEnter
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("MouseEntered"), func);
                ConnectEventHandler(id, "MouseEntered", value, func);
            }
            remove
            {
                DisconnectEventHandler("MouseEntered", value);
            }
        }

        public event EventHandler OnMouseLeave
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("MouseLeft"), func);
                ConnectEventHandler(id, "MouseLeft", value, func);
            }
            remove
            {
                DisconnectEventHandler("MouseLeft", value);
            }
        }

        public class ShowEffectFinishEventArgs : EventArgs
        {
            public ShowEffectFinishEventArgs(ShowEffectType type, bool show)
            {
                Type = type;
                Show = show;
            }
            public ShowEffectType Type { get; }
            public bool Show { get; }
        }
        public event EventHandler<ShowEffectFinishEventArgs> OnShowEffectFinish
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackShowEffect func = (ShowEffectType type, byte show) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ShowEffectFinishEventArgs(type, show != 0));
                };
                uint id = tguiWidget_signalShowEffectConnect(CPointer, Util.ConvertStringForC_UTF32("ShowEffectFinished"), func);
                ConnectEventHandler(id, "ShowEffectFinished", value, func);
            }
            remove
            {
                DisconnectEventHandler("ShowEffectFinished", value);
            }
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidget_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_destroy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr tguiWidget_addPointerReference(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_setRenderer(IntPtr cPointer, IntPtr renderer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr tguiWidget_getRenderer(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr tguiWidget_getSharedRenderer(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr tgui_getLastError();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern void tguiWidget_setAutoLayout(IntPtr cPointer, AutoLayout layout);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern AutoLayout tguiWidget_getAutoLayout(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallback function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalIntConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackInt function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalUIntConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackUInt function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalSizeTConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackNUInt function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalBoolConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackBool function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalFloatConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackFloat function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalColorConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackColor function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalStringConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackString function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalVector2fConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackVector2f function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalFloatRectConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackFloatRect function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalBoolPtrConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackBoolPtr function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalRangeConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackRange function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalTabSelectionChangingConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackTabSelectionChanging function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalChildWindowConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackChildWindow function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalItemConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackItem function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalPanelListBoxItemConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackPanelListBoxItem function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalFileDialogPathsConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackFileDialogPaths function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalShowEffectConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackShowEffect function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalAnimationTypeConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackAnimationType function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern uint tguiWidget_signalItemHierarchyConnect(IntPtr cPointer, IntPtr signalName, [MarshalAs(UnmanagedType.FunctionPtr)] UnmanagedCallbackItemHierarchy function);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern byte tguiWidget_signalDisconnect(IntPtr cPointer, IntPtr signalName, uint id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern void tguiWidget_signalDisconnectAll(IntPtr cPointer, IntPtr signalName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern byte tguiWidget_setSignalEnabled(IntPtr cPointer, IntPtr signalName, byte enabled);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern byte tguiWidget_isSignalEnabled(IntPtr cPointer, IntPtr signalName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern void tguiWidget_showWithEffect(IntPtr cPointer, ShowEffectType type, Duration duration);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern void tguiWidget_hideWithEffect(IntPtr cPointer, ShowEffectType type, Duration duration);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern void tguiWidget_moveWithAnimation(IntPtr cPointer, Vector2f position, Duration duration);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern void tguiWidget_resizeWithAnimation(IntPtr cPointer, Vector2f size, Duration duration);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern void tguiWidget_setToolTip(IntPtr cPointer, IntPtr toolTip);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr tguiWidget_getToolTip(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr tguiWidget_getParent(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr tguiWidget_getParentGui(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        protected static extern void tguiWidget_updateTime(IntPtr cPointer, Duration duration);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setPosition(IntPtr cPointer, Vector2f position);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setPositionFromLayout(IntPtr cPointer, IntPtr layout);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getPosition(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getAbsolutePosition(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getAbsolutePositionWithOffset(IntPtr cPointer, Vector2f offset);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getWidgetOffset(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setWidth(IntPtr cPointer, float width);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setWidthFromLayout(IntPtr cPointer, IntPtr layout);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setHeight(IntPtr cPointer, float height);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setHeightFromLayout(IntPtr cPointer, IntPtr layout);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setSize(IntPtr cPointer, Vector2f size);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setSizeFromLayout(IntPtr cPointer, IntPtr layout);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getFullSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getOrigin(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setOrigin(IntPtr cPointer, Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setScale(IntPtr cPointer, Vector2f origin);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setScaleWithOrigin(IntPtr cPointer, Vector2f scale, Vector2f origin);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getScale(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getScaleOrigin(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setRotation(IntPtr cPointer, float angle);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setRotationWithOrigin(IntPtr cPointer, float angle, Vector2f origin);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiWidget_getRotation(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiWidget_getRotationOrigin(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_isVisible(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setVisible(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_isEnabled(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setEnabled(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_isFocused(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setFocused(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_isFocusable(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setFocusable(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidget_getWidgetType(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_moveToFront(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_moveToBack(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidget_getUserData(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setUserData(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_hasUserData(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_isAnimationPlaying(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiWidget_getTextSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setTextSize(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidget_getWidgetName(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setWidgetName(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Cursor.Type tguiWidget_getMouseCursor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setMouseCursor(IntPtr cPointer, Cursor.Type value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidget_getNavigationUp(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setNavigationUp(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidget_getNavigationDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setNavigationDown(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidget_getNavigationLeft(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setNavigationLeft(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiWidget_getNavigationRight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setNavigationRight(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_getIgnoreMouseEvents(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setIgnoreMouseEvents(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_finishAllAnimations(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiWidget_setAutoLayoutUpdateEnabled(IntPtr cPointer, byte enabled);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_isMouseDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiWidget_isMouseOnWidget(IntPtr cPointer, Vector2f pos);

        #endregion
    }
}
