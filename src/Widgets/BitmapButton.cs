// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// BitmapButton widget
    /// </summary>
    public class BitmapButton : Button
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BitmapButton()
            : base(tguiBitmapButton_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal BitmapButton(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public BitmapButton(BitmapButton copy)
            : base(copy)
        {
        }

        public Texture Image
        {
            get => new Texture(tguiBitmapButton_getImage(CPointer));
            set => tguiBitmapButton_setImage(CPointer, value.CPointer);
        }

        public float ImageScaling
        {
            get => tguiBitmapButton_getImageScaling(CPointer);
            set => tguiBitmapButton_setImageScaling(CPointer, value);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiBitmapButton_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiBitmapButton_getImage(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiBitmapButton_setImage(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiBitmapButton_getImageScaling(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiBitmapButton_setImageScaling(IntPtr cPointer, float value);

        #endregion
    }
}
