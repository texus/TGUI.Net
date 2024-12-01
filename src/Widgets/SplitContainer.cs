// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// SplitContainer widget
    /// </summary>
    public class SplitContainer : Group
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SplitContainer()
            : base(tguiSplitContainer_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal SplitContainer(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public SplitContainer(SplitContainer copy)
            : base(copy)
        {
        }

        public new SplitContainerRenderer Renderer
        {
            get => new SplitContainerRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new SplitContainerRenderer SharedRenderer => new SplitContainerRenderer(tguiWidget_getSharedRenderer(CPointer));

        public Orientation Orientation
        {
            get => tguiSplitContainer_getOrientation(CPointer);
            set => tguiSplitContainer_setOrientation(CPointer, value);
        }

        public float SplitterWidth
        {
            get => tguiSplitContainer_getSplitterWidth(CPointer);
            set => tguiSplitContainer_setSplitterWidth(CPointer, value);
        }

        public float MinimumGrabWidth
        {
            get => tguiSplitContainer_getMinimumGrabWidth(CPointer);
            set => tguiSplitContainer_setMinimumGrabWidth(CPointer, value);
        }

        public void SetMinValidSplitterOffset(float minOffset)
        {
            tguiSplitContainer_setMinValidSplitterOffset(CPointer, minOffset);
        }

        public void SetMaxValidSplitterOffset(float maxOffset)
        {
            tguiSplitContainer_setMaxValidSplitterOffset(CPointer, maxOffset);
        }

        public void SetSplitterOffset(float offset)
        {
            tguiSplitContainer_setSplitterOffset(CPointer, offset);
        }

        public float GetMinValidSplitterOffset()
        {
            return tguiSplitContainer_getMinValidSplitterOffset(CPointer);
        }

        public float GetMaxValidSplitterOffset()
        {
            return tguiSplitContainer_getMaxValidSplitterOffset(CPointer);
        }

        public float GetSplitterOffset()
        {
            return tguiSplitContainer_getSplitterOffset(CPointer);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSplitContainer_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Orientation tguiSplitContainer_getOrientation(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSplitContainer_setOrientation(IntPtr cPointer, Orientation value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSplitContainer_getSplitterWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSplitContainer_setSplitterWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSplitContainer_getMinimumGrabWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSplitContainer_setMinimumGrabWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSplitContainer_setMinValidSplitterOffset(IntPtr cPointer, float minOffset);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSplitContainer_setMaxValidSplitterOffset(IntPtr cPointer, float maxOffset);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSplitContainer_setSplitterOffset(IntPtr cPointer, float offset);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSplitContainer_getMinValidSplitterOffset(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSplitContainer_getMaxValidSplitterOffset(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSplitContainer_getSplitterOffset(IntPtr cPointer);

        #endregion
    }
}
