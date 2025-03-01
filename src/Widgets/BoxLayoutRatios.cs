// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// BoxLayoutRatios widget
    /// </summary>
    public class BoxLayoutRatios : BoxLayout
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal BoxLayoutRatios(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public BoxLayoutRatios(BoxLayoutRatios copy)
            : base(copy)
        {
        }

        public void Add(Widget? widget, float ratio, string widgetName = "")
        {
            tguiBoxLayoutRatios_add(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, ratio, Util.ConvertStringForC_UTF32(widgetName));
        }

        public void Insert(int index, Widget? widget, float ratio, string widgetName = "")
        {
            tguiBoxLayoutRatios_insert(CPointer, (UIntPtr)index, widget is null ? IntPtr.Zero : widget.CPointer, ratio, Util.ConvertStringForC_UTF32(widgetName));
        }

        public void AddSpace(float ratio)
        {
            tguiBoxLayoutRatios_addSpace(CPointer, ratio);
        }

        public void InsertSpace(int index, float ratio)
        {
            tguiBoxLayoutRatios_insertSpace(CPointer, (UIntPtr)index, ratio);
        }

        public bool SetRatio(Widget? widget, float ratio)
        {
            return tguiBoxLayoutRatios_setRatio(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, ratio) != 0;
        }

        public bool SetRatio(int index, float ratio)
        {
            return tguiBoxLayoutRatios_setRatioAtIndex(CPointer, (UIntPtr)index, ratio) != 0;
        }

        public float GetRatio(Widget? widget)
        {
            return tguiBoxLayoutRatios_getRatio(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public float GetRatio(int index)
        {
            return tguiBoxLayoutRatios_getRatioAtIndex(CPointer, (UIntPtr)index);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiBoxLayoutRatios_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiBoxLayoutRatios_add(IntPtr cPointer, IntPtr widget, float ratio, IntPtr widgetName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiBoxLayoutRatios_insert(IntPtr cPointer, UIntPtr index, IntPtr widget, float ratio, IntPtr widgetName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiBoxLayoutRatios_addSpace(IntPtr cPointer, float ratio);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiBoxLayoutRatios_insertSpace(IntPtr cPointer, UIntPtr index, float ratio);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiBoxLayoutRatios_setRatio(IntPtr cPointer, IntPtr widget, float ratio);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiBoxLayoutRatios_setRatioAtIndex(IntPtr cPointer, UIntPtr index, float ratio);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiBoxLayoutRatios_getRatio(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiBoxLayoutRatios_getRatioAtIndex(IntPtr cPointer, UIntPtr index);

        #endregion
    }
}
