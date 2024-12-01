// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class ToolTip
    {
        public static Duration InitialDelay
        {
            get => tguiToolTip_getInitialDelay();
            set => tguiToolTip_setInitialDelay(value);
        }

        public static Vector2f DistanceToMouse
        {
            get => tguiToolTip_getDistanceToMouse();
            set => tguiToolTip_setDistanceToMouse(value);
        }

        public static bool ShowOnDisabledWidget
        {
            get => tguiToolTip_getShowOnDisabledWidget() != 0;
            set => tguiToolTip_setShowOnDisabledWidget(value ? (byte)1 : (byte)0);
        }

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Duration tguiToolTip_getInitialDelay();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiToolTip_setInitialDelay(Duration value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiToolTip_getDistanceToMouse();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiToolTip_setDistanceToMouse(Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiToolTip_getShowOnDisabledWidget();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiToolTip_setShowOnDisabledWidget(byte value);

        #endregion
    }
}
