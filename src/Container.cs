// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class Container : Widget
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Container(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Container(Container copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Loads the child widgets from a text file
        /// </summary>
        /// <param name="filename">Filename of the widget file</param>
        /// <param name="loadOptions">Settings to use for loading</param>
        /// <exception cref="Exception">The file could not be loaded</exception>
        public void LoadWidgetsFromFile(string filename, FormLoadOptions? loadOptions = null)
        {
            loadOptions ??= new FormLoadOptions();
            if (tguiContainer_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_UTF32(filename), loadOptions.CPointer) == 0)
                throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
        }

        /// <summary>
        /// Saves the child widgets to a text file
        /// </summary>
        /// <param name="filename">Filename of the widget file</param>
        /// <exception cref="Exception">The file could not be saved</exception>
        public void SaveWidgetsToFile(string filename)
        {
            if (tguiContainer_saveWidgetsToFile(CPointer, Util.ConvertStringForC_UTF32(filename)) == 0)
                throw new Exception(Util.GetStringFromC_UTF32(tgui_getLastError()));
        }

        public void Add(Widget? widget, string widgetName = "")
        {
            tguiContainer_add(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, Util.ConvertStringForC_UTF32(widgetName));
        }

        public Widget? Get(string widgetName)
        {
            return Util.GetWidgetFromC(tguiContainer_get(CPointer, Util.ConvertStringForC_UTF32(widgetName)));
        }

        public unsafe IReadOnlyList<Widget> GetWidgets()
        {
            IntPtr* returnWidgetsC = tguiContainer_getWidgets(CPointer, out UIntPtr returnCount);
            Widget[] returnWidgets = new Widget[(int)returnCount];
            for (int i = 0; i < (int)returnCount; ++i)
                returnWidgets[i] = Util.GetWidgetFromC(returnWidgetsC[i]) ?? throw new ArgumentNullException();

            return returnWidgets;
        }

        public bool Remove(Widget? widget)
        {
            return tguiContainer_remove(CPointer, widget is null ? IntPtr.Zero : widget.CPointer) != 0;
        }

        public void RemoveAllWidgets()
        {
            tguiContainer_removeAllWidgets(CPointer);
        }

        public void MoveWidgetToFront(Widget? widget)
        {
            tguiContainer_moveWidgetToFront(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public void MoveWidgetToBack(Widget? widget)
        {
            tguiContainer_moveWidgetToBack(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public int MoveWidgetForward(Widget? widget)
        {
            return (int)tguiContainer_moveWidgetForward(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public int MoveWidgetBackward(Widget? widget)
        {
            return (int)tguiContainer_moveWidgetBackward(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public bool SetWidgetIndex(Widget? widget, int index)
        {
            return tguiContainer_setWidgetIndex(CPointer, widget is null ? IntPtr.Zero : widget.CPointer, (UIntPtr)index) != 0;
        }

        public int GetWidgetIndex(Widget? widget)
        {
            return tguiContainer_getWidgetIndex(CPointer, widget is null ? IntPtr.Zero : widget.CPointer);
        }

        public Widget? FocusedChild
        {
            get => Util.GetWidgetFromC(tguiContainer_getFocusedChild(CPointer));
        }

        public Widget? FocusedLeaf
        {
            get => Util.GetWidgetFromC(tguiContainer_getFocusedLeaf(CPointer));
        }

        public Widget? GetWidgetAtPos(Vector2f pos, bool recursive)
        {
            return Util.GetWidgetFromC(tguiContainer_getWidgetAtPos(CPointer, pos, recursive ? (byte)1 : (byte)0));
        }

        public bool FocusNextWidget(bool recursive = true)
        {
            return tguiContainer_focusNextWidget(CPointer, recursive ? (byte)1 : (byte)0) != 0;
        }

        public bool FocusPreviousWidget(bool recursive = true)
        {
            return tguiContainer_focusPreviousWidget(CPointer, recursive ? (byte)1 : (byte)0) != 0;
        }

        public Vector2f InnerSize
        {
            get => tguiContainer_getInnerSize(CPointer);
        }

        public Vector2f ChildWidgetsOffset
        {
            get => tguiContainer_getChildWidgetsOffset(CPointer);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContainer_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename, IntPtr loadOptions);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContainer_saveWidgetsToFile(IntPtr cPointer, IntPtr filename);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContainer_add(IntPtr cPointer, IntPtr widget, IntPtr widgetName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiContainer_get(IntPtr cPointer, IntPtr widgetName);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe private static extern IntPtr* tguiContainer_getWidgets(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContainer_remove(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContainer_removeAllWidgets(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContainer_moveWidgetToFront(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContainer_moveWidgetToBack(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiContainer_moveWidgetForward(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiContainer_moveWidgetBackward(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContainer_setWidgetIndex(IntPtr cPointer, IntPtr widget, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiContainer_getWidgetIndex(IntPtr cPointer, IntPtr widget);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiContainer_getFocusedChild(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiContainer_getFocusedLeaf(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiContainer_getWidgetAtPos(IntPtr cPointer, Vector2f pos, byte recursive);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContainer_focusNextWidget(IntPtr cPointer, byte recursive);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContainer_focusPreviousWidget(IntPtr cPointer, byte recursive);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiContainer_getInnerSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiContainer_getChildWidgetsOffset(IntPtr cPointer);

        #endregion
    }
}
