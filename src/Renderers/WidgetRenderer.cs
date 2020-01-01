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
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using SFML.Graphics;

namespace TGUI
{
    /// <summary>
    /// Base class for renderers of all widgets
    /// </summary>
    public class WidgetRenderer : SFML.ObjectBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public WidgetRenderer()
            : base(tguiWidgetRenderer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal WidgetRenderer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public WidgetRenderer(WidgetRenderer copy)
            : base(tguiWidgetRenderer_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        ///<param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiWidgetRenderer_destroy(CPointer);
        }

        /// <summary>
        /// Gets or sets the opacity of the widget
        /// </summary>
        /// <remarks>
        /// 0 means completely transparent, while 1 (default) means fully opaque
        /// </remarks>
        public float Opacity
        {
            get { return tguiWidgetRenderer_getOpacity(CPointer); }
            set { tguiWidgetRenderer_setOpacity(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the opacity of the widget when it is disabled
        /// </summary>
        /// <remarks>
        /// 0 means completely transparent, while 1 (default) means fully opaque.
        /// Set to -1 (default) to use the normal opacity value even when the widget is disabled.
        /// </remarks>
        public float OpacityDisabled
        {
            get { return tguiWidgetRenderer_getOpacityDisabled(CPointer); }
            set { tguiWidgetRenderer_setOpacityDisabled(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the font used for the text in the widget
        /// </summary>
        /// <remarks>
        /// When you don't set this font then the font from the parent widget will be used.
        /// </remarks>
        public Font Font
        {
            set { tguiWidgetRenderer_setFont(CPointer, value.CPointer); }
        }

        /// <summary>
        /// Gets or sets whether mouse events should be ignored on transparent parts of the texture of the widget in normal state
        /// </summary>
        /// <remarks>
        /// When mouse events are ignored, they are passed to a widget behind the widget.
        /// By default, mouse events are NOT ignored and the widget will receive mouse events even on transparent texture parts.
        ///
        /// This property does nothing if the widget doesn't use textures.
        /// </remarks>
        public bool TransparentTexture
        {
            get { return tguiWidgetRenderer_getTransparentTexture(CPointer); }
            set { tguiWidgetRenderer_setTransparentTexture(CPointer, value); }
        }

        /// <summary>
        /// Gets the renderer data that is shared between the renderers
        /// </summary>
        public RendererData Data
        {
            get { return new RendererData(tguiWidgetRenderer_getData(CPointer)); }
        }

        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        public override string ToString()
        {
            return "[WidgetRenderer]";
        }


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidgetRenderer_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidgetRenderer_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidgetRenderer_destroy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidgetRenderer_setOpacity(IntPtr cPointer, float opacity);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiWidgetRenderer_getOpacity(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidgetRenderer_setOpacityDisabled(IntPtr cPointer, float opacity);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiWidgetRenderer_getOpacityDisabled(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidgetRenderer_setFont(IntPtr cPointer, IntPtr font);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiWidgetRenderer_setTransparentTexture(IntPtr cPointer, bool ignoreTransparentParts);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiWidgetRenderer_getTransparentTexture(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidgetRenderer_getData(IntPtr cPointer);

        #endregion
    }
}
