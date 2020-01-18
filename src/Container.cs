/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2020 Bruno Van de Velde (vdv_b@tgui.eu)
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
            // If not found, it is still possible that it exists (e.g. it could have been loaded from a file inside the c++ code)
            return Util.GetWidgetFromC(tguiContainer_get(CPointer, Util.ConvertStringForC_UTF32(widgetName)), ParentGui);
        }

        /// <summary>
        /// Gets a list of all the widgets in this container or completely replace all widgets with a new list
        /// </summary>
        /// <remarks>
        /// Setting this property is equivalent to calling RemoveAllWidgets() and then call Add(widget) for every widget in the list.
        /// By setting the widgets via this property you lose the ability to give a name to the widget for later retrieval.
        /// </remarks>
        public IReadOnlyList<Widget> Widgets
        {
            get
            {
                unsafe
                {
                    IntPtr* widgetsPtr = tguiContainer_getWidgets(CPointer, out uint count);
                    Widget[] widgets = new Widget[count];
                    for (uint i = 0; i < count; ++i)
                        widgets[i] = Util.GetWidgetFromC(widgetsPtr[i], ParentGui);

                    return widgets;
                }
            }
            set
            {
                RemoveAllWidgets();
                foreach(Widget widget in value)
                    Add(widget);
            }
        }

        /// <summary>
        /// Returns a list of all the widgets in this container
        /// </summary>
        /// <returns>
        /// List of widgets that have been added to the container
        /// </returns>
        [Obsolete("Use Widgets property instead")]
        public List<Widget> GetWidgets()
        {
            unsafe
            {
                IntPtr* widgetsPtr = tguiContainer_getWidgets(CPointer, out uint count);
                List<Widget> widgets = new List<Widget>();
                for (uint i = 0; i < count; ++i)
                    widgets.Add(Util.GetWidgetFromC(widgetsPtr[i], ParentGui));

                return widgets;
            }
        }

        /// <summary>
        /// Gets the list of the names of all the widgets in this container
        /// </summary>
        public IReadOnlyList<string> WidgetNames
        {
            get
            {
                unsafe
                {
                    IntPtr* namesPtr = tguiContainer_getWidgetNames(CPointer, out uint count);
                    string[] names = new string[count];
                    for (uint i = 0; i < count; ++i)
                        names[i] = Util.GetStringFromC_UTF32(namesPtr[i]);

                    return names;
                }
            }
        }

        /// <summary>
        /// Returns a list of the names of all the widgets in this container
        /// </summary>
        /// <returns>
        /// List of widget names belonging to the widgets that were added to the container
        /// </returns>
        [Obsolete("Use WidgetNames property instead")]
        public List<string> GetWidgetNames()
        {
            unsafe
            {
                IntPtr* namesPtr = tguiContainer_getWidgetNames(CPointer, out uint count);
                List<string> names = new List<string>();
                for (uint i = 0; i < count; ++i)
                    names.Add(Util.GetStringFromC_UTF32(namesPtr[i]));

                return names;
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
            return tguiContainer_remove(CPointer, widget.CPointer);
        }

        /// <summary>
        /// Removes all widgets that were added to the container
        /// </summary>
        public void RemoveAllWidgets()
        {
            tguiContainer_removeAllWidgets(CPointer);
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
        /// <param name="replaceExisting">Remove existing widgets first if there are any</param>
        /// <exception cref="TGUIException">Thrown when file could not be opened or parsing failed</exception>
        public void LoadWidgetsFromFile(string filename, bool replaceExisting = true)
        {
            if (!tguiContainer_loadWidgetsFromFile(CPointer, Util.ConvertStringForC_ASCII(filename), replaceExisting))
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

        /// <summary>
        /// Gets the gui to which the widget was added.
        /// </summary>
        /// <remarks>
        /// The setter is only intended for internal use.
        /// </remarks>
        public override Gui ParentGui
        {
            get { return myParentGui; }
            set
            {
                myParentGui = value;
                foreach (var widget in myWidgets)
                    widget.ParentGui = value;
            }
        }

        protected List<Widget> myWidgets = new List<Widget>();


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiContainer_add(IntPtr cPointer, IntPtr cPointerWidget, IntPtr widgetName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiContainer_get(IntPtr cPointer, IntPtr widgetName);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiContainer_getWidgets(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern private IntPtr* tguiContainer_getWidgetNames(IntPtr cPointer, out uint count);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiContainer_remove(IntPtr cPointer, IntPtr cPointerWidget);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiContainer_removeAllWidgets(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiContainer_focusNextWidget(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiContainer_focusPreviousWidget(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiContainer_getInnerSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private Vector2f tguiContainer_getChildWidgetsOffset(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiContainer_loadWidgetsFromFile(IntPtr cPointer, IntPtr filename, bool replaceExisting);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiContainer_saveWidgetsToFile(IntPtr cPointer, IntPtr filename);

        #endregion
    }
}
