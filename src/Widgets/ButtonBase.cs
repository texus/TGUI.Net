// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// ButtonBase widget
    /// </summary>
    public class ButtonBase : ClickableWidget
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ButtonBase(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ButtonBase(ButtonBase copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Changes the position of the caption text
        /// </summary>
        /// <param name="position">Position of the text within the button (X and Y values between 0 and width/height of the button)</param>
        /// <param name="origin">Origin that determines which part of the text is placed at the given position. The origin consists of x and y components with values between 0 (left/top) and 1 (right/bottom). The default is (0.5f, 0.5f).</param>
        public void SetTextPositionAbs(Vector2f position, Vector2f origin)
        {
            tguiButtonBase_setTextPositionAbs(CPointer, position, origin);
        }

        /// <summary>
        /// Changes the position of the caption text
        /// </summary>
        /// <param name="position">Position of the text within the button, relative to the button size (i.e. X and Y values between 0 and 1)</param>
        /// <param name="origin">Origin that determines which part of the text is placed at the given position. The origin consists of x and y components with values between 0 (left/top) and 1 (right/bottom). The default is (0.5f, 0.5f).</param>
        public void SetTextPositionRel(Vector2f position, Vector2f origin)
        {
            tguiButtonBase_setTextPositionRel(CPointer, position, origin);
        }

        public new ButtonRenderer Renderer
        {
            get => new ButtonRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new ButtonRenderer SharedRenderer => new ButtonRenderer(tguiWidget_getSharedRenderer(CPointer));

        public string Text
        {
            get => Util.GetStringFromC_UTF32(tguiButtonBase_getText(CPointer));
            set => tguiButtonBase_setText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public bool IgnoreKeyEvents
        {
            get => tguiButtonBase_getIgnoreKeyEvents(CPointer) != 0;
            set => tguiButtonBase_setIgnoreKeyEvents(CPointer, value ? (byte)1 : (byte)0);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonBase_setTextPositionAbs(IntPtr cPointer, Vector2f position, Vector2f origin);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonBase_setTextPositionRel(IntPtr cPointer, Vector2f position, Vector2f origin);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonBase_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButtonBase_getText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonBase_setText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiButtonBase_getIgnoreKeyEvents(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiButtonBase_setIgnoreKeyEvents(IntPtr cPointer, byte value);

        #endregion
    }
}
