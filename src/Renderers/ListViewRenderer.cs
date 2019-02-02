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
using SFML.Graphics;

namespace TGUI
{
    /// <summary>
    /// Renderer for list view widgets
    /// </summary>
    public class ListViewRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ListViewRenderer()
            : base(tguiListViewRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ListViewRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ListViewRenderer(ListViewRenderer copy)
            : base(tguiListViewRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Gets or sets the size of the borders
        /// </summary>
        public Outline Borders
        {
            get { return new Outline(tguiListViewRenderer_getBorders(CPointer)); }
            set { tguiListViewRenderer_setBorders(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the size of the padding
        /// </summary>
        public Outline Padding
        {
            get { return new Outline(tguiListViewRenderer_getPadding(CPointer)); }
            set { tguiListViewRenderer_setPadding(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return tguiListViewRenderer_getBackgroundColor(CPointer); }
            set { tguiListViewRenderer_setBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color used for the item below the mouse
        /// </summary>
        public Color BackgroundColorHover
        {
            get { return tguiListViewRenderer_getBackgroundColorHover(CPointer); }
            set { tguiListViewRenderer_setBackgroundColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color of the selected item
        /// </summary>
        public Color SelectedBackgroundColor
        {
            get { return tguiListViewRenderer_getSelectedBackgroundColor(CPointer); }
            set { tguiListViewRenderer_setSelectedBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color used for the selected item when the mouse hovers over it
        /// </summary>
        public Color SelectedBackgroundColorHover
        {
            get { return tguiListViewRenderer_getSelectedBackgroundColorHover(CPointer); }
            set { tguiListViewRenderer_setSelectedBackgroundColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color
        /// </summary>
        public Color TextColor
        {
            get { return tguiListViewRenderer_getTextColor(CPointer); }
            set { tguiListViewRenderer_setTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the item below the mouse
        /// </summary>
        public Color TextColorHover
        {
            get { return tguiListViewRenderer_getTextColorHover(CPointer); }
            set { tguiListViewRenderer_setTextColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the selected item
        /// </summary>
        public Color SelectedTextColor
        {
            get { return tguiListViewRenderer_getSelectedTextColor(CPointer); }
            set { tguiListViewRenderer_setSelectedTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the selected item when the mouse hovers over it
        /// </summary>
        public Color SelectedTextColorHover
        {
            get { return tguiListViewRenderer_getSelectedTextColorHover(CPointer); }
            set { tguiListViewRenderer_setSelectedTextColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color of the header
        /// </summary>
        public Color HeaderBackgroundColor
        {
            get { return tguiListViewRenderer_getHeaderBackgroundColor(CPointer); }
            set { tguiListViewRenderer_setHeaderBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the header captions
        /// </summary>
        public Color HeaderTextColor
        {
            get { return tguiListViewRenderer_getHeaderTextColor(CPointer); }
            set { tguiListViewRenderer_setHeaderTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders
        /// </summary>
        public Color BorderColor
        {
            get { return tguiListViewRenderer_getBorderColor(CPointer); }
            set { tguiListViewRenderer_setBorderColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the separators
        /// </summary>
        /// <remarks>
        /// The border color will be used when no separator color is set.
        /// </remarks>
        public Color SeparatorColor
        {
            get { return tguiListViewRenderer_getSeparatorColor(CPointer); }
            set { tguiListViewRenderer_setSeparatorColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the renderer data of the scrollbar
        /// </summary>
        public RendererData Scrollbar
        {
            get { return new RendererData(tguiListViewRenderer_getScrollbar(CPointer)); }
            set { tguiListViewRenderer_setScrollbar(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the width of the scrollbar
        /// </summary>
        public float ScrollbarWidth
        {
            get { return tguiListViewRenderer_getScrollbarWidth(CPointer); }
            set { tguiListViewRenderer_setScrollbarWidth(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListViewRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListViewRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListViewRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setPadding(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListViewRenderer_getPadding(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setSelectedBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getSelectedBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setSelectedBackgroundColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getSelectedBackgroundColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setTextColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setSelectedTextColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getSelectedTextColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setHeaderBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getHeaderBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setHeaderTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getHeaderTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setSeparatorColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiListViewRenderer_getSeparatorColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setScrollbar(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiListViewRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiListViewRenderer_setScrollbarWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiListViewRenderer_getScrollbarWidth(IntPtr cPointer);

        #endregion
    }
}
