// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// ToggleButton widget
    /// </summary>
    public class ToggleButton : ButtonBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ToggleButton()
            : base(tguiToggleButton_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ToggleButton(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ToggleButton(ToggleButton copy)
            : base(copy)
        {
        }

        public bool Down
        {
            get => tguiToggleButton_isDown(CPointer) != 0;
            set => tguiToggleButton_setDown(CPointer, value ? (byte)1 : (byte)0);
        }

        public class ToggleEventArgs : EventArgs
        {
            public ToggleEventArgs(bool down)
            {
                Down = down;
            }
            public bool Down { get; }
        }
        public event EventHandler<ToggleEventArgs> OnToggle
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackBool func = (byte val) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ToggleEventArgs(val != 0));
                };
                uint id = tguiWidget_signalBoolConnect(CPointer, Util.ConvertStringForC_UTF32("Toggled"), func);
                ConnectEventHandler(id, "Toggled", value, func);
            }
            remove
            {
                DisconnectEventHandler("Toggled", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiToggleButton_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiToggleButton_isDown(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiToggleButton_setDown(IntPtr cPointer, byte value);

        #endregion
    }
}
