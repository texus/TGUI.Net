// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// ColorPicker widget
    /// </summary>
    public class ColorPicker : ChildWindow
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ColorPicker()
            : base(tguiColorPicker_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ColorPicker(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ColorPicker(ColorPicker copy)
            : base(copy)
        {
        }

        public new ColorPickerRenderer Renderer
        {
            get => new ColorPickerRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new ColorPickerRenderer SharedRenderer => new ColorPickerRenderer(tguiWidget_getSharedRenderer(CPointer));

        public Color? Color
        {
            get => Util.GetColorFromC(tguiColorPicker_getColor(CPointer));
            set => tguiColorPicker_setColor(CPointer, Util.ConvertColorForC(value));
        }

        public class ColorChangeEventArgs : EventArgs
        {
            public ColorChangeEventArgs(Color? color)
            {
                Color = color;
            }
            public Color? Color { get; }
        }
        public event EventHandler<ColorChangeEventArgs> OnColorChange
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackColor func = (ColorCTGUI color) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new ColorChangeEventArgs(Util.GetColorFromC(color)));
                };
                uint id = tguiWidget_signalColorConnect(CPointer, Util.ConvertStringForC_UTF32("ColorChanged"), func);
                ConnectEventHandler(id, "ColorChanged", value, func);
            }
            remove
            {
                DisconnectEventHandler("ColorChanged", value);
            }
        }

        public class OkPressEventArgs : EventArgs
        {
            public OkPressEventArgs(Color? color)
            {
                Color = color;
            }
            public Color? Color { get; }
        }
        public event EventHandler<OkPressEventArgs> OnOkPress
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackColor func = (ColorCTGUI color) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, new OkPressEventArgs(Util.GetColorFromC(color)));
                };
                uint id = tguiWidget_signalColorConnect(CPointer, Util.ConvertStringForC_UTF32("OkPressed"), func);
                ConnectEventHandler(id, "OkPressed", value, func);
            }
            remove
            {
                DisconnectEventHandler("OkPressed", value);
            }
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiColorPicker_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ColorCTGUI tguiColorPicker_getColor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiColorPicker_setColor(IntPtr cPointer, ColorCTGUI value);

        #endregion
    }
}
