// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// CheckBox widget
    /// </summary>
    public class CheckBox : RadioButton
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CheckBox()
            : base(tguiCheckBox_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal CheckBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public CheckBox(CheckBox copy)
            : base(copy)
        {
        }

        public new CheckBoxRenderer Renderer
        {
            get => new CheckBoxRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new CheckBoxRenderer SharedRenderer => new CheckBoxRenderer(tguiWidget_getSharedRenderer(CPointer));

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiCheckBox_create();

        #endregion
    }
}
