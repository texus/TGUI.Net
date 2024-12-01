// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Picture widget
    /// </summary>
    public class Picture : ClickableWidget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Picture()
            : base(tguiPicture_create())
        {
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

        public new PictureRenderer Renderer
        {
            get => new PictureRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new PictureRenderer SharedRenderer => new PictureRenderer(tguiWidget_getSharedRenderer(CPointer));

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

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiPicture_create();

        #endregion
    }
}
