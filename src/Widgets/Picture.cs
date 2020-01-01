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
using SFML.Graphics;

namespace TGUI
{
    /// <summary>
    /// Picture widget
    /// </summary>
    public class Picture : ClickableWidget
    {
        /// <summary>
        /// Constructor to create the picture with a texture loaded from a file
        /// </summary>
        /// <param name="filename">Filename of the texture to load</param>
        public Picture(string filename = "")
            : base(tguiPicture_create())
        {
            if (filename.Length > 0)
                Renderer.Texture = new Texture(filename);
        }

        /// <summary>
        /// Constructor to create the picture with a given texture
        /// </summary>
        /// <param name="texture">Texture used by the widget</param>
        public Picture(Texture texture)
            : base(tguiPicture_create())
        {
            if (texture != null)
                Renderer.Texture = texture;
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Picture(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Picture(Picture copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After calling this function, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new PictureRenderer Renderer
        {
            get { return new PictureRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new PictureRenderer SharedRenderer
        {
            get { return new PictureRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets whether the widget should completely ignore mouse events and let them pass to the widgets behind it.
        /// </summary>
        public bool IgnoreMouseEvents
        {
            get { return tguiPicture_isIgnoringMouseEvents(CPointer); }
            set { tguiPicture_ignoreMouseEvents(CPointer, value); }
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            DoubleClickedCallback = new CallbackActionVector2f(ProcessDoubleClickedSignal);
            if (tguiWidget_connectVector2f(CPointer, Util.ConvertStringForC_ASCII("DoubleClicked"), DoubleClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessDoubleClickedSignal(Vector2f pos)
        {
            DoubleClicked?.Invoke(this, new SignalArgsVector2f(pos));
        }

        /// <summary>Event handler for the DoubleClicked signal</summary>
        public event EventHandler<SignalArgsVector2f> DoubleClicked = null;

        private CallbackActionVector2f DoubleClickedCallback;

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiPicture_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiPicture_ignoreMouseEvents(IntPtr cPointer, bool ignore);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiPicture_isIgnoringMouseEvents(IntPtr cPointer);

        #endregion
    }
}
