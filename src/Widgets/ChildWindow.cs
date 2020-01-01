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
using SFML.System;

namespace TGUI
{
    /// <summary>
    /// Child window widget
    /// </summary>
    public class ChildWindow : Container
    {
        /// <summary>
        /// Buttons that can be displayed in the title bar
        /// </summary>
        [Flags]
        public enum TitleButton
        {
            ///<summary>Display no buttons</summary>
            None     = 0,

            ///<summary>Display the close button</summary>
            Close    = 1 << 0,

            ///<summary>Display the maximize button</summary>
            Maximize = 1 << 1,

            ///<summary>Display the minimize button</summary>
            Minimize = 1 << 2
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChildWindow()
            : base(tguiChildWindow_create())
        {
        }

        /// <summary>
        /// Constructor to create the ChildWindow with the given title and title buttons
        /// </summary>
        /// <param name="title">Title to display in the title bar</param>
        /// <param name="titleButtons">Buttons to display in the title bar</param>
        public ChildWindow(string title, TitleButton titleButtons = TitleButton.Close)
            : base(tguiChildWindow_create())
        {
            Title = title;
            TitleButtons = titleButtons;
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        protected internal ChildWindow(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public ChildWindow(ChildWindow copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After calling this function, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new ChildWindowRenderer Renderer
        {
            get { return new ChildWindowRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new ChildWindowRenderer SharedRenderer
        {
            get { return new ChildWindowRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets the minimum size of the child window
        /// </summary>
        /// <remarks>
        /// The given minimum size excludes the borders and titlebar.
        /// If the window is smaller than the minimum size, it will automatically be enlarged.
        /// </remarks>
        public Vector2f MinimumSize
        {
            get { return tguiChildWindow_getMinimumSize(CPointer); }
            set { tguiChildWindow_setMinimumSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the maximum size of the child window
        /// </summary>
        /// <remarks>
        /// The given maximum size excludes the borders and titlebar.
        /// If the window is larger than the maximum size, it will automatically be shrunk.
        /// </remarks>
        public Vector2f MaximumSize
        {
            get { return tguiChildWindow_getMaximumSize(CPointer); }
            set { tguiChildWindow_setMaximumSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the title that is displayed in the title bar of the child window
        /// </summary>
        public string Title
        {
            get { return Util.GetStringFromC_UTF32(tguiChildWindow_getTitle(CPointer)); }
            set { tguiChildWindow_setTitle(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        /// <summary>
        /// Gets or sets the character size of the title
        /// </summary>
        /// <remarks>
        /// If the size is set to 0 then the character size is determined by the height of the title bar.
        /// </remarks>
        public uint TitleTextSize
        {
            get { return tguiChildWindow_getTitleTextSize(CPointer); }
            set { tguiChildWindow_setTitleTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the title alignment
        /// </summary>
        public HorizontalAlignment TitleAlignment
        {
            get { return tguiChildWindow_getTitleAlignment(CPointer); }
            set { tguiChildWindow_setTitleAlignment(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the title buttons that are shown in the title bar
        /// </summary>
        /// <remarks>
        /// By default ChildWindows only display a close button.
        /// </remarks>
        /// <example>
        /// The following example gives the ChildWindow both a minimize and close button.
        /// <code>
        /// childWindow.SetTitleButtons(ChildWindow.TitleButton.Minimize | ChildWindow.TitleButton.Close);
        /// </code>
        /// </example>
        public TitleButton TitleButtons
        {
            get { return tguiChildWindow_getTitleButtons(CPointer); }
            set { tguiChildWindow_setTitleButtons(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the child window can be resized by dragging its borders or not
        /// </summary>
        public bool Resizable
        {
            get { return tguiChildWindow_isResizable(CPointer); }
            set { tguiChildWindow_setResizable(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the child window is to be kept inside its parent
        /// </summary>
        /// <remarks>
        /// When it's set to true, it will not be possible to move the window outside its parent, not even partially.
        /// It's set to false by default.
        /// </remarks>
        public bool KeepInParent
        {
            get { return tguiChildWindow_isKeptInParent(CPointer); }
            set { tguiChildWindow_setKeepInParent(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the child window can be moved by dragging its title bar or not
        /// </summary>
        /// <remarks>
        /// Locking the position only affects user interaction, the setPosition function will still move the window.
        /// </remarks>
        public bool PositionLocked
        {
            get { return tguiChildWindow_isPositionLocked(CPointer); }
            set { tguiChildWindow_setPositionLocked(CPointer, value); }
        }

        /// <summary>
        /// Try to close the window
        /// </summary>
        /// <remarks>
        /// This will trigger the Closed signal. If no callback is requested then the window will be closed.
        /// </remarks>
        public void CloseWindow()
        {
            ProcessClosedSignal();
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            MousePressedCallback = new CallbackAction(ProcessMousePressedSignal);
            if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("MousePressed"), MousePressedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            ClosedCallback = new CallbackAction(ProcessClosedSignal);
            if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("Closed"), ClosedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            MaximizedCallback = new CallbackAction(ProcessMaximizedSignal);
            if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("Maximized"), MaximizedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            MinimizedCallback = new CallbackAction(ProcessMinimizedSignal);
            if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("Minimized"), MinimizedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            EscapeKeyPressedCallback = new CallbackAction(ProcessEscapeKeyPressed);
            if (tguiWidget_connect(CPointer, Util.ConvertStringForC_ASCII("EscapeKeyPressed"), EscapeKeyPressedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessMousePressedSignal()
        {
            MousePressed?.Invoke(this, EventArgs.Empty);
        }

        private void ProcessClosedSignal()
        {
            if (Closed != null)
                Closed(this, EventArgs.Empty);
            else
            {
                // Actually close the window when no signal handler is connected
                if (!myConnectedSignals.ContainsKey("closed"))
                {
                    if (Parent != null)
                        Parent.Remove(this);
                }
            }
        }

        private void ProcessMaximizedSignal()
        {
            Maximized?.Invoke(this, EventArgs.Empty);
        }

        private void ProcessMinimizedSignal()
        {
            Minimized?.Invoke(this, EventArgs.Empty);
        }

        private void ProcessEscapeKeyPressed()
        {
            EscapeKeyPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Event handler for the MousePressed signal</summary>
        public event EventHandler MousePressed = null;

        /// <summary>Event handler for the Closed signal</summary>
        public event EventHandler Closed = null;

        /// <summary>Event handler for the Maximized signal</summary>
        public event EventHandler Maximized = null;

        /// <summary>Event handler for the Minimized signal</summary>
        public event EventHandler Minimized = null;

        /// <summary>Event handler for the EscapeKeyPressed signal</summary>
        public event EventHandler EscapeKeyPressed = null;

        private CallbackAction MousePressedCallback;
        private CallbackAction ClosedCallback;
        private CallbackAction MaximizedCallback;
        private CallbackAction MinimizedCallback;
        private CallbackAction EscapeKeyPressedCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChildWindow_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setTitle(IntPtr cPointer, IntPtr value);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setMaximumSize(IntPtr cPointer, Vector2f maxSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiChildWindow_getMaximumSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setMinimumSize(IntPtr cPointer, Vector2f minSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiChildWindow_getMinimumSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChildWindow_getTitle(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setTitleTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiChildWindow_getTitleTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setTitleAlignment(IntPtr cPointer, HorizontalAlignment alignment);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private HorizontalAlignment tguiChildWindow_getTitleAlignment(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setTitleButtons(IntPtr cPointer, TitleButton buttons);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private TitleButton tguiChildWindow_getTitleButtons(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setResizable(IntPtr cPointer, bool resizable);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiChildWindow_isResizable(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setKeepInParent(IntPtr cPointer, bool keepInParent);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiChildWindow_isKeptInParent(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindow_setPositionLocked(IntPtr cPointer, bool positionLocked);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiChildWindow_isPositionLocked(IntPtr cPointer);

        #endregion
    }
}
