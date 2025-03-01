// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public enum GridAlignment
    {
        Center,
        UpperLeft,
        Up,
        UpperRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
    }

    /// <summary>
    /// Grid widget
    /// </summary>
    public class Grid : Container
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Grid()
            : base(tguiGrid_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Grid(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public Grid(Grid copy)
            : base(copy)
        {
        }

        public unsafe Dictionary<Widget, (int row, int column)> WidgetLocations
        {
            get
            {
                var dict = new Dictionary<Widget, (int row, int column)>();

                WidgetLocationImpl* locationList = tguiGrid_getWidgetLocations(CPointer, out UIntPtr count);
                for (int i = 0; i < (int)count; ++i)
                {
                    Widget? widget = Util.GetWidgetFromC(tguiWidget_addPointerReference(locationList[i].widget));
                    if (!(widget is null))
                        dict[widget] = ((int)locationList[i].row, (int)locationList[i].column);
                }

                tguiGridWidgetLocation_destroy(locationList, count);
                return dict;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WidgetLocationImpl
        {
            public IntPtr widget;
            public UIntPtr row;
            public UIntPtr column;
        }

        public bool AutoSize
        {
            get => tguiGrid_getAutoSize(CPointer) != 0;
            set => tguiGrid_setAutoSize(CPointer, value ? (byte)1 : (byte)0);
        }

        public void AddWidget(Widget? widget, int row, int col, GridAlignment alignment, Outline padding)
        {
            tguiGrid_addWidget(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, (UIntPtr)row, (UIntPtr)col, alignment, padding.CPointer);
        }

        public void SetWidgetCell(Widget? widget, int row, int col, GridAlignment alignment, Outline padding)
        {
            tguiGrid_setWidgetCell(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, (UIntPtr)row, (UIntPtr)col, alignment, padding.CPointer);
        }

        public Widget? GetWidget(int row, int col)
        {
            return Util.GetWidgetFromC(tguiGrid_getWidget(CPointer, (UIntPtr)row, (UIntPtr)col));
        }

        public void SetWidgetAlignment(Widget? widget, GridAlignment alignment = GridAlignment.Center)
        {
            tguiGrid_setWidgetAlignment(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, alignment);
        }

        public void SetWidgetAlignment(int row, int col, GridAlignment alignment = GridAlignment.Center)
        {
            tguiGrid_setWidgetAlignmentByCell(CPointer, (UIntPtr)row, (UIntPtr)col, alignment);
        }

        public GridAlignment GetWidgetAlignment(Widget? widget)
        {
            return tguiGrid_getWidgetAlignment(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public GridAlignment GetWidgetAlignment(int row, int col)
        {
            return tguiGrid_getWidgetAlignmentByCell(CPointer, (UIntPtr)row, (UIntPtr)col);
        }

        public void SetWidgetPadding(Widget? widget, Outline padding)
        {
            tguiGrid_setWidgetPadding(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, padding.CPointer);
        }

        public void SetWidgetPadding(int row, int col, Outline padding)
        {
            tguiGrid_setWidgetPaddingByCell(CPointer, (UIntPtr)row, (UIntPtr)col, padding.CPointer);
        }

        public Outline GetWidgetPadding(Widget? widget)
        {
            return new Outline(tguiGrid_getWidgetPadding(CPointer, widget is null ? IntPtr.Zero : widget.CPointer));
        }

        public Outline GetWidgetPadding(int row, int col)
        {
            return new Outline(tguiGrid_getWidgetPaddingByCell(CPointer, (UIntPtr)row, (UIntPtr)col));
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void tguiGridWidgetLocation_destroy(WidgetLocationImpl* locationList, UIntPtr count);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe WidgetLocationImpl* tguiGrid_getWidgetLocations(IntPtr cPointer, out UIntPtr count);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGrid_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiGrid_getAutoSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGrid_setAutoSize(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGrid_addWidget(IntPtr cPointer, IntPtr widget, UIntPtr row, UIntPtr col, GridAlignment alignment, IntPtr padding);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGrid_setWidgetCell(IntPtr cPointer, IntPtr widget, UIntPtr row, UIntPtr col, GridAlignment alignment, IntPtr padding);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGrid_getWidget(IntPtr cPointer, UIntPtr row, UIntPtr col);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGrid_setWidgetAlignment(IntPtr cPointer, IntPtr widget, GridAlignment alignment);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGrid_setWidgetAlignmentByCell(IntPtr cPointer, UIntPtr row, UIntPtr col, GridAlignment alignment);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern GridAlignment tguiGrid_getWidgetAlignment(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern GridAlignment tguiGrid_getWidgetAlignmentByCell(IntPtr cPointer, UIntPtr row, UIntPtr col);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGrid_setWidgetPadding(IntPtr cPointer, IntPtr widget, IntPtr padding);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiGrid_setWidgetPaddingByCell(IntPtr cPointer, UIntPtr row, UIntPtr col, IntPtr padding);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGrid_getWidgetPadding(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiGrid_getWidgetPaddingByCell(IntPtr cPointer, UIntPtr row, UIntPtr col);

        #endregion
    }
}
