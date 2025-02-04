/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2024 Bruno Van de Velde (vdv_b@tgui.eu)
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
using System.Linq;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices; // RuntimeHelpers
using System.Collections.Generic; // Dictionary

namespace TGUI
{
    public static class Util
    {
        /// <summary>Name of the CTGUI library to import</summary>
        public const string LibName = "ctgui";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void UnmanagedCallbackWidgetCleanup(IntPtr widgetCPointer);

        // We implemented Equals and GetHashCode members in ObjectBase so that two separate objects that refer to the same C object are considered equal.
        // When keeping track of the disposable objects that are still alive, we however need to store each reference separately.
        private class DisposableObjectsComparer : IEqualityComparer<ObjectBase>
        {
            bool IEqualityComparer<ObjectBase>.Equals(ObjectBase? obj1, ObjectBase? obj2) => object.ReferenceEquals(obj1, obj2);
            int IEqualityComparer<ObjectBase>.GetHashCode(ObjectBase obj) => RuntimeHelpers.GetHashCode(obj);
        }

        public static Dictionary<IntPtr, Gui> Guis { get; } = new Dictionary<IntPtr, Gui>();
        public static Dictionary<IntPtr, Dictionary<string, List<UnmanagedCallbackInfo>>> Callbacks { get; } = new Dictionary<IntPtr, Dictionary<string, List<UnmanagedCallbackInfo>>>();
        internal static HashSet<ObjectBase> DisposableObjects { get; } = new HashSet<ObjectBase>(new DisposableObjectsComparer());
        internal static UnmanagedCallbackWidgetCleanup? UnmanagedWidgetCleanupCallbackFuncPtr;

        public class UnmanagedCallbackInfo
        {
            public UnmanagedCallbackInfo(uint id, Delegate callbackFunc, Delegate helperFunc)
            {
                this.id = id;
                this.callbackFunc = callbackFunc;
                this.helperFunc = helperFunc;
            }

            public uint id;
            public Delegate callbackFunc;
            public Delegate helperFunc;
        }

        internal static void UnmanagedWidgetCleanupCallback(IntPtr widgetCPointer)
        {
            // If we were keeping callback functions alive then we can safely destroy them now that the connected widget no longer exists
            Callbacks.Remove(widgetCPointer);
        }

        internal static void RegisterDisposableObject(ObjectBase obj)
        {
            DisposableObjects.Add(obj);
        }

        internal static void UnregisterDisposableObject(ObjectBase obj)
        {
            if (!DisposableObjects.Remove(obj))
                throw new Exception("UnregisterDisposableObject was called with an unregistered object");
        }

        internal static void DisposeAllRemainingObjects()
        {
            // Destroy all remaining objects so that they no longer exist after the gui is destroyed,
            // because executing the cleanup code isn't allowed to happen once the gui and backend are gone.
            // Attempting to still use these object (other than calling Dispose() on them) will result in an exception.
            while (DisposableObjects.Count > 0)
                DisposableObjects.First().Dispose();
        }

        public static string GetStringFromC_UTF32(IntPtr source)
        {
            // Find the length of the source string (find the terminating 0)
            uint length = 0;
            unsafe
            {
                for (uint* ptr = (uint*)source.ToPointer(); *ptr != 0; ++ptr)
                    length++;
            }
                
            // Copy it to a byte array
            byte[] sourceBytes = new byte[length * 4];
            Marshal.Copy(source, sourceBytes, 0, sourceBytes.Length);

            // Convert it to a C# string
            return System.Text.Encoding.UTF32.GetString(sourceBytes);
        }

        public static string GetStringFromC_ASCII(IntPtr source)
        {
            return Marshal.PtrToStringAnsi(source) ?? "";
        }

        public static IntPtr ConvertStringForC_UTF32(string source)
        {
            // Copy the string to a null-terminated UTF-32 byte array
            byte[] utf32 = System.Text.Encoding.UTF32.GetBytes(source + '\0');

            // Pass it to the C API
            unsafe
            {
                fixed (byte* ptr = utf32)
                {
                    return (IntPtr)ptr;
                }
            }
        }

        public static IntPtr ConvertStringForC_ASCII(string source)
        {
            // Copy the string to a null-terminated ANSI byte array
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(source + '\0');

            // Pass it to the C API
            unsafe
            {
                fixed (byte* ptr = bytes)
                {
                    return (IntPtr)ptr;
                }
            }
        }

        public static Color? GetColorFromC(ColorCTGUI color)
        {
            if (color.IsSet != 0)
                return new Color(color.R, color.G, color.B, color.A);
            else
                return null;
        }

        public static ColorCTGUI ConvertColorForC(Color? color)
        {
            return new ColorCTGUI(color);
        }

        public static Widget? GetWidgetFromC(IntPtr widgetCPointer, Type? widgetType = null)
        {
            if (widgetCPointer == IntPtr.Zero)
                return null;

            if (widgetType is null)
            {
                string widgetTypeStr = Util.GetStringFromC_UTF32(tguiWidget_getWidgetType(widgetCPointer));
                widgetType = Type.GetType("TGUI." + widgetTypeStr);
                if (widgetType is null)
                    throw new Exception("Failed to retrieve widget of type " + widgetTypeStr);
            }

            var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
            return (Widget?)Activator.CreateInstance(widgetType, flags, null, new object[]{ widgetCPointer }, null);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiWidget_getWidgetType(IntPtr cPointer);

        #endregion
    }
}
