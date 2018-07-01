/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2016 Bruno Van de Velde (vdv_b@tgui.eu)
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented;
//    you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment
//    in the product documentation would be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such,
//    and must not be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SFML.System;

namespace TGUI
{
    public abstract class Container : Widget
    {
        protected Container(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public Container(Container copy)
            : base(copy)
        {
        }

        public void Add(Widget widget, string widgetName = "")
        {
            tguiContainer_add(CPointer, widget.CPointer, Util.ConvertStringForC_UTF32(widgetName));

            myWidgets.Add(widget);
            myWidgetIds.Add(widgetName);
        }

        public Widget Get(string widgetName)
        {
            // Search for the widget locally in the direct children
            for (var i = 0; i < myWidgetIds.Count; ++i)
            {
                if (myWidgetIds[i] == widgetName)
                    return myWidgets[i];
            }

            // Search for the widget locally, recursively through the children
            for (var i = 0; i < myWidgetIds.Count; ++i)
            {
                if (myWidgets[i] is Container)
                {
                    var widget = ((Container)myWidgets[i]).Get(widgetName);
                    if (widget != null)
                        return widget;
                }
            }

            // If not found, it is still possible that it exists (e.g. it could have been loaded from a file inside the c++ code)
            IntPtr WidgetCPointer = tguiContainer_get(CPointer, Util.ConvertStringForC_UTF32(widgetName));
            if (WidgetCPointer == IntPtr.Zero)
                return null;

            Type type = Type.GetType("TGUI." + Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(WidgetCPointer)));
            return (Widget)Activator.CreateInstance(type, new object[]{ WidgetCPointer });
        }

        public List<Widget> GetWidgets()
        {
            // We can't use our myWidgets member because the c++ code may contain more widgets (e.g. it could have been loaded from a file inside the c++ code)

            unsafe
            {
                IntPtr* WidgetsPtr = tguiContainer_getWidgets(CPointer, out uint Count);
                List<Widget> Widgets = new List<Widget>();
                for (uint i = 0; i < Count; ++i)
                {
                    IntPtr WidgetCPointer = WidgetsPtr[i];
                    Type type = Type.GetType("TGUI." + Util.GetStringFromC_ASCII(tguiWidget_getWidgetType(WidgetCPointer)));
                    Widgets.Add((Widget)Activator.CreateInstance(type, new object[]{ WidgetCPointer }));
                }

                return Widgets;
            }
        }

        public List<string> GetWidgetNames()
        {
            // We can't use our myWidgetIds member because the c++ code may contain more widgets (e.g. it could have been loaded from a file inside the c++ code)

            unsafe
            {
                IntPtr* NamesPtr = tguiContainer_getWidgetNames(CPointer, out uint Count);
                List<string> Names = new List<string>();
                for (uint i = 0; i < Count; ++i)
                    Names.Add(Util.GetStringFromC_UTF32(NamesPtr[i]));

                return Names;
            }
        }

        public void Remove(Widget widget)
        {
            tguiContainer_remove(CPointer, widget.CPointer);

            var index = myWidgets.IndexOf(widget);
            if (index != -1)
            {
                myWidgets.RemoveAt(index);
                myWidgetIds.RemoveAt(index);
            }
        }

        public void RemoveAllWidgets()
        {
            tguiContainer_removeAllWidgets(CPointer);

            myWidgets.Clear();
            myWidgetIds.Clear();
        }

        public bool FocusNextWidget()
        {
            return tguiContainer_focusNextWidget(CPointer);
        }

        public bool FocusPreviousWidget()
        {
            return tguiContainer_focusPreviousWidget(CPointer);
        }

        public Vector2f ChildWidgetsOffset
        {
            get { return tguiContainer_getChildWidgetsOffset(CPointer); }
        }

        public void LoadWidgetsFromFile(string filename)
        {
            if (!tguiContainer_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_ASCII(filename)))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        public void SaveWidgetsToFile(string filename)
        {
            if (!tguiContainer_saveWidgetsToFile(CPointer, Util.ConvertStringForC_ASCII(filename)))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }


        protected List<Widget> myWidgets = new List<Widget>();
        protected List<string> myWidgetIds = new List<string>();


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiContainer_add(IntPtr cPointer, IntPtr cPointerWidget, IntPtr widgetName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiContainer_get(IntPtr cPointer, IntPtr widgetName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern protected IntPtr* tguiContainer_getWidgets(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern protected IntPtr* tguiContainer_getWidgetNames(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiContainer_remove(IntPtr cPointer, IntPtr cPointerWidget);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiContainer_removeAllWidgets(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiContainer_focusNextWidget(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiContainer_focusPreviousWidget(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Vector2f tguiContainer_getChildWidgetsOffset(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiContainer_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiContainer_saveWidgetsToFile(IntPtr cPointer, IntPtr filename);

        #endregion
    }
}
