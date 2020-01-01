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
    /// <summary>
    /// Renderer for the MenuBar widget
    /// </summary>
    public class MenuBarRenderer : WidgetRenderer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuBarRenderer()
            : base(tguiMenuBarRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        protected internal MenuBarRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public MenuBarRenderer(MenuBarRenderer copy)
            : base(tguiMenuBarRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return tguiMenuBarRenderer_getBackgroundColor(CPointer); }
            set { tguiMenuBarRenderer_setBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the background color of the selected item
        /// </summary>
        public Color SelectedBackgroundColor
        {
            get { return tguiMenuBarRenderer_getSelectedBackgroundColor(CPointer); }
            set { tguiMenuBarRenderer_setSelectedBackgroundColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text
        /// </summary>
        public Color TextColor
        {
            get { return tguiMenuBarRenderer_getTextColor(CPointer); }
            set { tguiMenuBarRenderer_setTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text from the selected item
        /// </summary>
        public Color SelectedTextColor
        {
            get { return tguiMenuBarRenderer_getSelectedTextColor(CPointer); }
            set { tguiMenuBarRenderer_setSelectedTextColor(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text when disabled
        /// </summary>
        public Color TextColorDisabled
        {
            get { return tguiMenuBarRenderer_getTextColorDisabled(CPointer); }
            set { tguiMenuBarRenderer_setTextColorDisabled(CPointer, value); }
        }

        /// <summary>
        /// Sets the image that is used to fill the entire menu bar
        /// </summary>
        public Texture TextureBackground
        {
            set { tguiMenuBarRenderer_setTextureBackground(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Sets the image that is displayed when the menu item is not selected
        /// </summary>
        public Texture TextureItemBackground
        {
            set { tguiMenuBarRenderer_setTextureItemBackground(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Sets the image that is used as background of the selected menu item
        /// </summary>
        public Texture TextureSelectedItemBackground
        {
            set { tguiMenuBarRenderer_setTextureSelectedItemBackground(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets the distance between the text and the side of the menu item
        /// </summary>
        public float DistanceToSide
        {
            get { return tguiMenuBarRenderer_getDistanceToSide(CPointer); }
            set { tguiMenuBarRenderer_setDistanceToSide(CPointer, value); }
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiMenuBarRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiMenuBarRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiMenuBarRenderer_getBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setSelectedBackgroundColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiMenuBarRenderer_getSelectedBackgroundColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiMenuBarRenderer_getTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setSelectedTextColor(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiMenuBarRenderer_getSelectedTextColor(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setTextColorDisabled(IntPtr cPointer, Color color);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Color tguiMenuBarRenderer_getTextColorDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setTextureBackground(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setTextureItemBackground(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setTextureSelectedItemBackground(IntPtr cPointer, IntPtr texture);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBarRenderer_setDistanceToSide(IntPtr cPointer, float distanceToSide);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiMenuBarRenderer_getDistanceToSide(IntPtr cPointer);

        #endregion
    }
}
