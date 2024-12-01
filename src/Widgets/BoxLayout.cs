// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// BoxLayout widget
    /// </summary>
    public class BoxLayout : Group
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal BoxLayout(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public BoxLayout(BoxLayout copy)
            : base(copy)
        {
        }

        public new BoxLayoutRenderer Renderer
        {
            get => new BoxLayoutRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new BoxLayoutRenderer SharedRenderer => new BoxLayoutRenderer(tguiWidget_getSharedRenderer(CPointer));

        public void Insert(int index, Widget? widgetToAdd, string widgetName)
        {
            tguiBoxLayout_insert(CPointer, (UIntPtr)index, widgetToAdd is null ? IntPtr.Zero : widgetToAdd.CPointer, Util.ConvertStringForC_UTF32(widgetName));
        }

        public bool Remove(int index)
        {
            return tguiBoxLayout_removeAtIndex(CPointer, (UIntPtr)index) != 0;
        }

        public Widget? Get(int index)
        {
            return Util.GetWidgetFromC(tguiBoxLayout_getAtIndex(CPointer, (UIntPtr)index));
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiBoxLayout_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiBoxLayout_insert(IntPtr cPointer, UIntPtr index, IntPtr widgetToAdd, IntPtr widgetName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiBoxLayout_removeAtIndex(IntPtr cPointer, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiBoxLayout_getAtIndex(IntPtr cPointer, UIntPtr index);

        #endregion
    }
}
