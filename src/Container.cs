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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SFML.System;

namespace TGUI
{
    /// <summary>
    /// Container widget. Parent class for widgets that contain child widgets.
    /// </summary>
    public abstract class Container : Widget
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        protected Container(IntPtr cPointer)
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
        /// Adds a widget to the container
        /// </summary>
        /// <param name="widget">The widget you would like to add</param>
        /// <param name="widgetName">You can give the widget a unique name to retrieve it from the container later</param>
        /// <remarks>
        /// The widget name should not contain whitespace
        /// </remarks>
        public void Add(Widget widget, string widgetName = "")
        {
            tguiContainer_add(CPointer, widget.CPointer, Util.ConvertStringForC_UTF32(widgetName));

            widget.ParentGui = ParentGui;
            myWidgets.Add(widget);
            myWidgetIds.Add(widgetName);
        }

        /// <summary>
        /// Returns a widget that was added earlier
        /// </summary>
        /// <param name="widgetName">The name that was given to the widget when it was added to the container</param>
        /// <returns>
        /// The earlier added widget
        /// </returns>
        /// <remarks>
        /// The container will first search for widgets that are direct children of it, but when none of the child widgets match
        /// the given name, a recursive search will be performed.
        /// The function returns null when an unknown widget name was passed.
        /// </remarks>
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
            return Util.GetWidgetFromC(tguiContainer_get(CPointer, Util.ConvertStringForC_UTF32(widgetName)), ParentGui);
        }

        /// <summary>
        /// Returns a list of all the widgets in this container
        /// </summary>
        /// <returns>
        /// List of widgets that have been added to the container
        /// </returns>
        public List<Widget> GetWidgets()
        {
            // We can't use our myWidgets member because the c++ code may contain more widgets (e.g. it could have been loaded from a file inside the c++ code)

            unsafe
            {
                IntPtr* WidgetsPtr = tguiContainer_getWidgets(CPointer, out uint Count);
                List<Widget> Widgets = new List<Widget>();
                for (uint i = 0; i < Count; ++i)
                    Widgets.Add(Util.GetWidgetFromC(WidgetsPtr[i], ParentGui));

                return Widgets;
            }
        }

        /// <summary>
        /// Returns a list of the names of all the widgets in this container
        /// </summary>
        /// <returns>
        /// List of widget names belonging to the widgets that were added to the container
        /// </returns>
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

        /// <summary>
        /// Removes a single widget that was added to the container
        /// </summary>
        /// <param name="widget">Widget to remove</param>
        /// <returns>
        /// True when widget is removed, false when widget was not found
        /// </returns>
        public bool Remove(Widget widget)
        {
            var index = myWidgets.IndexOf(widget);
            if (index != -1)
            {
                myWidgets.RemoveAt(index);
                myWidgetIds.RemoveAt(index);
            }

            return tguiContainer_remove(CPointer, widget.CPointer);
        }

        /// <summary>
        /// Removes all widgets that were added to the container
        /// </summary>
        public void RemoveAllWidgets()
        {
            tguiContainer_removeAllWidgets(CPointer);

            myWidgets.Clear();
            myWidgetIds.Clear();
        }

        /// <summary>
        /// Focuses the next widget in this container
        /// </summary>
        /// <returns>
        /// Whether a new widget was focused
        /// </returns>
        public bool FocusNextWidget()
        {
            return tguiContainer_focusNextWidget(CPointer);
        }

        /// <summary>
        /// Focuses the previous widget in this container
        /// </summary>
        /// <returns>
        /// Whether a new widget was focused
        /// </returns>
        public bool FocusPreviousWidget()
        {
            return tguiContainer_focusPreviousWidget(CPointer);
        }

        /// <summary>
        /// Gets the space available for widgets inside the container
        /// </summary>
        public Vector2f InnerSize
        {
            get { return tguiContainer_getInnerSize(CPointer); }
        }

        /// <summary>
        /// Gets the distance between the position of the container and a widget that would be drawn inside
        /// this container on relative position (0,0)
        /// </summary>
        public Vector2f ChildWidgetsOffset
        {
            get { return tguiContainer_getChildWidgetsOffset(CPointer); }
        }

        /// <summary>
        /// Loads the child widgets from a text file
        /// </summary>
        /// <param name="filename">Filename of the widget file</param>
        /// <exception cref="TGUIException">Thrown when file could not be opened or parsing failed</exception>
        public void LoadWidgetsFromFile(string filename)
        {
            if (!tguiContainer_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_ASCII(filename)))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        /// <summary>
        /// Saves the child widgets to a text file
        /// </summary>
        /// <param name="filename">Filename of the widget file</param>
        /// <exception cref="TGUIException">Thrown when file could not be opened for writing</exception>
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
        static extern protected bool tguiContainer_remove(IntPtr cPointer, IntPtr cPointerWidget);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiContainer_removeAllWidgets(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiContainer_focusNextWidget(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiContainer_focusPreviousWidget(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Vector2f tguiContainer_getInnerSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected Vector2f tguiContainer_getChildWidgetsOffset(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiContainer_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiContainer_saveWidgetsToFile(IntPtr cPointer, IntPtr filename);

        #endregion
    }
}
