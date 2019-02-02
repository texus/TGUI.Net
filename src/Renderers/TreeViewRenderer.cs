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
    /// Renderer for tree view widgets
    /// </summary>
    public class TreeViewRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TreeViewRenderer()
            : base(tguiTreeViewRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal TreeViewRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public TreeViewRenderer(TreeViewRenderer copy)
            : base(tguiTreeViewRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Gets or sets the size of the borders
        /// </summary>
        public Outline Borders
        {
            get { return new Outline(tguiTreeViewRenderer_getBorders(CPointer)); }
            set { tguiTreeViewRenderer_setBorders(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the size of the padding
        /// </summary>
        public Outline Padding
        {
            get { return new Outline(tguiTreeViewRenderer_getPadding(CPointer)); }
            set { tguiTreeViewRenderer_setPadding(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return tguiTreeViewRenderer_getBackgroundColor(CPointer); }
            set { tguiTreeViewRenderer_setBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color used for the item below the mouse
        /// </summary>
        public Color BackgroundColorHover
        {
            get { return tguiTreeViewRenderer_getBackgroundColorHover(CPointer); }
            set { tguiTreeViewRenderer_setBackgroundColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color of the selected item
        /// </summary>
        public Color SelectedBackgroundColor
        {
            get { return tguiTreeViewRenderer_getSelectedBackgroundColor(CPointer); }
            set { tguiTreeViewRenderer_setSelectedBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color used for the selected item when the mouse hovers over it
        /// </summary>
        public Color SelectedBackgroundColorHover
        {
            get { return tguiTreeViewRenderer_getSelectedBackgroundColorHover(CPointer); }
            set { tguiTreeViewRenderer_setSelectedBackgroundColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color
        /// </summary>
        public Color TextColor
        {
            get { return tguiTreeViewRenderer_getTextColor(CPointer); }
            set { tguiTreeViewRenderer_setTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the item below the mouse
        /// </summary>
        public Color TextColorHover
        {
            get { return tguiTreeViewRenderer_getTextColorHover(CPointer); }
            set { tguiTreeViewRenderer_setTextColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the selected item
        /// </summary>
        public Color SelectedTextColor
        {
            get { return tguiTreeViewRenderer_getSelectedTextColor(CPointer); }
            set { tguiTreeViewRenderer_setSelectedTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text color of the selected item when the mouse hovers over it
        /// </summary>
        public Color SelectedTextColorHover
        {
            get { return tguiTreeViewRenderer_getSelectedTextColorHover(CPointer); }
            set { tguiTreeViewRenderer_setSelectedTextColorHover(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders
        /// </summary>
        public Color BorderColor
        {
            get { return tguiTreeViewRenderer_getBorderColor(CPointer); }
            set { tguiTreeViewRenderer_setBorderColor(CPointer, value); }
        }

        /// <summary>
        /// Sets the image used in front of an expanded item
        /// </summary>
        public Texture TextureBranchExpanded
        {
            set { tguiTreeViewRenderer_setTextureBranchExpanded(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Sets the image used in front of an collapsed item
        /// </summary>
        public Texture TextureBranchCollapsed
        {
            set { tguiTreeViewRenderer_setTextureBranchCollapsed(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Sets the image used in front of a leaf item
        /// </summary>
        public Texture TextureLeaf
        {
            set { tguiTreeViewRenderer_setTextureLeaf(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the renderer data of the scrollbar
        /// </summary>
        public RendererData Scrollbar
        {
            get { return new RendererData(tguiTreeViewRenderer_getScrollbar(CPointer)); }
            set { tguiTreeViewRenderer_setScrollbar(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the width of the scrollbar
        /// </summary>
        public float ScrollbarWidth
        {
            get { return tguiTreeViewRenderer_getScrollbarWidth(CPointer); }
            set { tguiTreeViewRenderer_setScrollbarWidth(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTreeViewRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTreeViewRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTreeViewRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setPadding(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTreeViewRenderer_getPadding(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setBackgroundColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getBackgroundColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setSelectedBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getSelectedBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setSelectedBackgroundColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getSelectedBackgroundColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setTextColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getTextColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setSelectedTextColorHover(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getSelectedTextColorHover(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiTreeViewRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setTextureBranchExpanded(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setTextureBranchCollapsed(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setTextureLeaf(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setScrollbar(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiTreeViewRenderer_getScrollbar(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiTreeViewRenderer_setScrollbarWidth(IntPtr cPointer, float width);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiTreeViewRenderer_getScrollbarWidth(IntPtr cPointer);

        #endregion
    }
}
