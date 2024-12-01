// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Button widget
    /// </summary>
    public class Button : ButtonBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Button()
            : base(tguiButton_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Button(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Button(Button copy)
            : base(copy)
        {
        }

        public class PressEventArgs : EventArgs
        {
            public PressEventArgs(string buttonText)
            {
                ButtonText = buttonText;
            }
            public string ButtonText { get; }
        }
        public event EventHandler<PressEventArgs> OnPress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackString func = (IntPtr str) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new PressEventArgs(Util.GetStringFromC_UTF32(str)));
                };
                uint id = tguiWidget_signalStringConnect(CPointer, Util.ConvertStringForC_UTF32("Pressed"), func);
                ConnectEventHandler(id, "Pressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("Pressed", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiButton_create();

        #endregion
    }
}
