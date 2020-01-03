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
using SFML.Graphics;

namespace TGUI
{
    public class ChildWindowRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ChildWindowRenderer()
            : base(tguiChildWindowRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ChildWindowRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ChildWindowRenderer(ChildWindowRenderer copy)
            : base(tguiChildWindowRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Gets or sets the size of the borders
        /// </summary>
        public Outline Borders
        {
            get { return new Outline(tguiChildWindowRenderer_getBorders(CPointer)); }
            set { tguiChildWindowRenderer_setBorders(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the color of the title bar
        /// </summary>
        public Color TitleBarColor
        {
            get { return tguiChildWindowRenderer_getTitleBarColor(CPointer); }
            set { tguiChildWindowRenderer_setTitleBarColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text in the title bar
        /// </summary>
        public Color TitleColor
        {
            get { return tguiChildWindowRenderer_getTitleColor(CPointer); }
            set { tguiChildWindowRenderer_setTitleColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return tguiChildWindowRenderer_getBackgroundColor(CPointer); }
            set { tguiChildWindowRenderer_setBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders
        /// </summary>
        public Color BorderColor
        {
            get { return tguiChildWindowRenderer_getBorderColor(CPointer); }
            set { tguiChildWindowRenderer_setBorderColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the borders while the child window is focused
        /// </summary>
        public Color BorderColorFocused
        {
            get { return tguiChildWindowRenderer_getBorderColorFocused(CPointer); }
            set { tguiChildWindowRenderer_setBorderColorFocused(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the size of the border between the title bar and the window contents
        /// </summary>
        public float BorderBelowTitleBar
        {
            get { return tguiChildWindowRenderer_getBorderBelowTitleBar(CPointer); }
            set { tguiChildWindowRenderer_setBorderBelowTitleBar(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the height of the title bar
        /// </summary>
        public float TitleBarHeight
        {
            get { return tguiChildWindowRenderer_getTitleBarHeight(CPointer); }
            set { tguiChildWindowRenderer_setTitleBarHeight(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the distance between the title or close button from the side of the title bar
        /// </summary>
        public float DistanceToSide
        {
            get { return tguiChildWindowRenderer_getDistanceToSide(CPointer); }
            set { tguiChildWindowRenderer_setDistanceToSide(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the distance between the title buttons if multiple exist
        /// </summary>
        public float PaddingBetweenButtons
        {
            get { return tguiChildWindowRenderer_getPaddingBetweenButtons(CPointer); }
            set { tguiChildWindowRenderer_setPaddingBetweenButtons(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the minimum amount of pixels where the child window can be dragged to resize it
        /// </summary>
        /// <remarks>
        /// If the border is larger than this value then this property has no effect. If the borders are smaller,
        /// several invisible pixels on the outside to the border can also be used to resize the child window.
        /// </remarks>
        public float MinimumResizableBorderWidth
        {
            get { return tguiChildWindowRenderer_getMinimumResizableBorderWidth(CPointer); }
            set { tguiChildWindowRenderer_setMinimumResizableBorderWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether characters are rendered on top of the title buttons (e.g. "x" on close button)
        /// </summary>
        public bool ShowTextOnTitleButtons
        {
            get { return tguiChildWindowRenderer_getShowTextOnTitleButtons(CPointer); }
            set { tguiChildWindowRenderer_setShowTextOnTitleButtons(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the texture of the title bar
        /// </summary>
        public Texture TextureTitleBar
        {
            set { tguiChildWindowRenderer_setTextureTitleBar(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the background texture
        /// </summary>
        public Texture TextureBackground
        {
            set { tguiChildWindowRenderer_setTextureBackground(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the look of the close button
        /// </summary>
        public RendererData CloseButton
        {
            get { return new RendererData(tguiChildWindowRenderer_getCloseButton(CPointer)); }
            set { tguiChildWindowRenderer_setCloseButton(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the look of the maximize button
        /// </summary>
        public RendererData MaximizeButton
        {
            get { return new RendererData(tguiChildWindowRenderer_getMaximizeButton(CPointer)); }
            set { tguiChildWindowRenderer_setMaximizeButton(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the look of the minimize button
        /// </summary>
        public RendererData MinimizeButton
        {
            get { return new RendererData(tguiChildWindowRenderer_getMinimizeButton(CPointer)); }
            set { tguiChildWindowRenderer_setMinimizeButton(CPointer, value.CPointer); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChildWindowRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChildWindowRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setBorders(IntPtr cPointer, IntPtr borders);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChildWindowRenderer_getBorders(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setTitleBarColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiChildWindowRenderer_getTitleBarColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setTitleColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiChildWindowRenderer_getTitleColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiChildWindowRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setBorderColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiChildWindowRenderer_getBorderColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setBorderColorFocused(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiChildWindowRenderer_getBorderColorFocused(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setBorderBelowTitleBar(IntPtr cPointer, float border);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiChildWindowRenderer_getBorderBelowTitleBar(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setTitleBarHeight(IntPtr cPointer, float height);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiChildWindowRenderer_getTitleBarHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setDistanceToSide(IntPtr cPointer, float distance);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiChildWindowRenderer_getDistanceToSide(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setPaddingBetweenButtons(IntPtr cPointer, float padding);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiChildWindowRenderer_getPaddingBetweenButtons(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setMinimumResizableBorderWidth(IntPtr cPointer, float minimumBorderWidth);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiChildWindowRenderer_getMinimumResizableBorderWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setShowTextOnTitleButtons(IntPtr cPointer, bool showText);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiChildWindowRenderer_getShowTextOnTitleButtons(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setTextureTitleBar(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setCloseButton(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChildWindowRenderer_getCloseButton(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setMaximizeButton(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChildWindowRenderer_getMaximizeButton(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiChildWindowRenderer_setMinimizeButton(IntPtr cPointer, IntPtr rendererData);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiChildWindowRenderer_getMinimizeButton(IntPtr cPointer);

        #endregion
    }
}
