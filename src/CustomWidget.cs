/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2025 Bruno Van de Velde (vdv_b@tgui.eu)
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
/*
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// Base class for custom widgets
    /// </summary>
    /// <remarks>
    /// When retrieving the widget from the Gui with the Get function, an instance of CustomWidget will be
    /// returned and not an instance of the derived class.
    /// To store properties in the custom C# widget, all data should be placed into the WidgetData property,
    /// as this is the only part of the widget that is stored in C#.
    /// </remarks>
    public class CustomWidget : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomWidget()
            : base(tguiCustomWidget_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal CustomWidget(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public CustomWidget(CustomWidget copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Stores all data associated to the custom widget
        /// </summary>
        public object WidgetData => Util.CustomWidgetData[CPointer];

        public static void PositionChanged(CustomWidget, Vector2f)
        {
            
        }

CTGUI_API void tguiCustomWidget_setPositionChangedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiVector2f));
CTGUI_API void tguiCustomWidget_setSizeChangedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiVector2f));
CTGUI_API void tguiCustomWidget_setVisibleChangedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiBool));
CTGUI_API void tguiCustomWidget_setEnableChangedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiBool));
CTGUI_API void tguiCustomWidget_setFocusChangedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiBool));
CTGUI_API void tguiCustomWidget_setCanGainFocusCallback(tguiWidget* widget, tguiBool (*function)(tguiWidget*));
CTGUI_API void tguiCustomWidget_setGetFullSizeCallback(tguiWidget* widget, tguiVector2f (*function)(tguiWidget*));
CTGUI_API void tguiCustomWidget_setGetWidgetOffsetCallback(tguiWidget* widget, tguiVector2f (*function)(tguiWidget*));
CTGUI_API void tguiCustomWidget_setUpdateTimeCallback(tguiWidget* widget, tguiBool (*function)(tguiWidget*, tguiDuration));
CTGUI_API void tguiCustomWidget_setMouseOnWidgetCallback(tguiWidget* widget, tguiBool (*function)(tguiWidget*, tguiVector2f));
CTGUI_API void tguiCustomWidget_setLeftMousePressedCallback(tguiWidget* widget, tguiBool (*function)(tguiWidget*, tguiVector2f));
CTGUI_API void tguiCustomWidget_setLeftMouseReleasedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiVector2f));
CTGUI_API void tguiCustomWidget_setRightMousePressedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiVector2f));
CTGUI_API void tguiCustomWidget_setRightMouseReleasedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiVector2f));
CTGUI_API void tguiCustomWidget_setMouseMovedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiVector2f));
CTGUI_API void tguiCustomWidget_setKeyPressedCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiKeyEvent));
CTGUI_API void tguiCustomWidget_setTextEnteredCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiChar32));
CTGUI_API void tguiCustomWidget_setScrolledCallback(tguiWidget* widget, tguiBool (*function)(tguiWidget*, float, tguiVector2f, tguiBool));
CTGUI_API void tguiCustomWidget_setMouseNoLongerOnWidgetCallback(tguiWidget* widget, void (*function)(tguiWidget*));
CTGUI_API void tguiCustomWidget_setLeftMouseButtonNoLongerDownCallback(tguiWidget* widget, void (*function)(tguiWidget*));
CTGUI_API void tguiCustomWidget_setMouseEnteredWidgetCallback(tguiWidget* widget, void (*function)(tguiWidget*));
CTGUI_API void tguiCustomWidget_setMouseLeftWidgetCallback(tguiWidget* widget, void (*function)(tguiWidget*));
CTGUI_API void tguiCustomWidget_setRendererChangedCallback(tguiWidget* widget, tguiBool (*function)(tguiWidget*, tguiUtf32));
CTGUI_API void tguiCustomWidget_setDrawCallback(tguiWidget* widget, void (*function)(tguiWidget*, tguiBackendRenderTarget*, tguiRenderStates*)); 



Add the following to the Util class:
    public static Dictionary<IntPtr, object> CustomWidgetData { get; } = new Dictionary<IntPtr, object>();
    Inside the UnmanagedWidgetCleanupCallback function:
        // If the widget being destroyed was a custom widget then also destroy its associated data
        CustomWidgetData.Remove(widgetCPointer);


        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiCustomWidget_create();

        #endregion
    }
}
*/
