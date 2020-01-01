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
using SFML.System;

namespace TGUI
{
    /// <summary>
    /// Scrollable panel widget
    /// </summary>
    public class ScrollablePanel : Panel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ScrollablePanel()
            : base(tguiScrollablePanel_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ScrollablePanel(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ScrollablePanel(ScrollablePanel copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After calling this function, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new ScrollablePanelRenderer Renderer
        {
            get { return new ScrollablePanelRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new ScrollablePanelRenderer SharedRenderer
        {
            get { return new ScrollablePanelRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets the size available for child widgets
        /// </summary>
        /// <remarks>
        /// If the content size is larger than the size of the panel then scrollbars will be displayed.
        ///
        /// When the content size is (0,0), which is the default, then the content size is determined by the child widgets.
        /// </remarks>
        public Vector2f ContentSize
        {
            get { return tguiScrollablePanel_getContentSize(CPointer); }
            set { tguiScrollablePanel_setContentSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the width of the scrollbars
        /// </summary>
        public float ScrollbarWidth
        {
            get { return tguiScrollablePanel_getScrollbarWidth(CPointer); }
            set { tguiScrollablePanel_setScrollbarWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets when the vertical scrollbar should be displayed
        /// </summary>
        public Scrollbar.Policy VerticalScrollbarPolicy
        {
            get { return tguiScrollablePanel_getVerticalScrollbarPolicy(CPointer); }
            set { tguiScrollablePanel_setVerticalScrollbarPolicy(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets when the horizontal scrollbar should be displayed
        /// </summary>
        public Scrollbar.Policy HorizontalScrollbarPolicy
        {
            get { return tguiScrollablePanel_getHorizontalScrollbarPolicy(CPointer); }
            set { tguiScrollablePanel_setHorizontalScrollbarPolicy(CPointer, value); }
        }

        /// <summary>
        /// Gets the amount of pixels the child widgets have been shifted to be displayed by the scrollable panel (i.e. the value of the scrollbars)
        /// </summary>
        public Vector2f ContentOffset
        {
            get { return tguiScrollablePanel_getContentOffset(CPointer); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiScrollablePanel_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollablePanel_setContentSize(IntPtr cPointer, Vector2f contentSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiScrollablePanel_getContentSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollablePanel_setScrollbarWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiScrollablePanel_getScrollbarWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollablePanel_setVerticalScrollbarPolicy(IntPtr cPointer, Scrollbar.Policy policy);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Scrollbar.Policy tguiScrollablePanel_getVerticalScrollbarPolicy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiScrollablePanel_setHorizontalScrollbarPolicy(IntPtr cPointer, Scrollbar.Policy policy);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Scrollbar.Policy tguiScrollablePanel_getHorizontalScrollbarPolicy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiScrollablePanel_getContentOffset(IntPtr cPointer);

        #endregion
    }
}
