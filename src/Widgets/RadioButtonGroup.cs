// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// RadioButtonGroup widget
    /// </summary>
    public class RadioButtonGroup : Container
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RadioButtonGroup()
            : base(tguiRadioButtonGroup_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal RadioButtonGroup(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public RadioButtonGroup(RadioButtonGroup copy)
            : base(copy)
        {
        }

        RadioButton? CheckedRadioButton => (RadioButton?)Util.GetWidgetFromC(tguiRadioButtonGroup_getCheckedRadioButton(CPointer));

        public void UncheckRadioButtons()
        {
            tguiRadioButtonGroup_uncheckRadioButtons(CPointer);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonGroup_getCheckedRadioButton(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiRadioButtonGroup_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiRadioButtonGroup_uncheckRadioButtons(IntPtr cPointer);

        #endregion
    }
}
