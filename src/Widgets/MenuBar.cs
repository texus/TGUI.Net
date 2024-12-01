// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// MenuBar widget
    /// </summary>
    public class MenuBar : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuBar()
            : base(tguiMenuBar_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal MenuBar(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public MenuBar(MenuBar copy)
            : base(copy)
        {
        }

        public struct Menu
        {
            public string text;
            public bool enabled;
            public List<Menu> menuItems;
        }

        public unsafe IReadOnlyList<Menu> GetMenus()
        {
            var menus = new List<Menu>();
            MenuListImpl* menuList = tguiMenuBar_getMenus(CPointer);
            GetMenusImpl(menus, menuList->menus, (int)menuList->menusCount);
            tguiMenuBarMenuList_destroy(menuList);
            return menus;
        }

        private unsafe void GetMenusImpl(List<Menu> menusToFill, MenuElementImpl* menuElementPtr, int menusCount)
        {
            for (int i = 0; i < menusCount; ++i)
            {
                Menu menu = new Menu();
                menu.text = Util.GetStringFromC_UTF32(menuElementPtr[i].text);
                menu.enabled = menuElementPtr[i].enabled != 0;
                menu.menuItems = new List<Menu>();
                if ((int)menuElementPtr[i].menuItemsCount > 0)
                    GetMenusImpl(menu.menuItems, menuElementPtr[i].menuItems, (int)menuElementPtr[i].menuItemsCount);

                menusToFill.Add(menu);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct MenuElementImpl
        {
            public IntPtr text;
            public byte enabled;
            public MenuElementImpl* menuItems;
            public UIntPtr menuItemsCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct MenuListImpl
        {
            public MenuElementImpl* menus;
            public UIntPtr menusCount;
        }

        public new MenuBarRenderer Renderer
        {
            get => new MenuBarRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new MenuBarRenderer SharedRenderer => new MenuBarRenderer(tguiWidget_getSharedRenderer(CPointer));

        public void AddMenu(string text)
        {
            tguiMenuBar_addMenu(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        public bool AddMenuItem(string menu, string text)
        {
            return tguiMenuBar_addMenuItem(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool AddMenuItem(string text)
        {
            return tguiMenuBar_addMenuItemToLastMenu(CPointer, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool AddMenuItem(ReadOnlySpan<string> hierarchy, bool createParents)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_addMenuItemHierarchy(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, createParents ? (byte)1 : (byte)0) != 0;
        }

        public bool ChangeMenuItem(ReadOnlySpan<string> hierarchy, string text)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_changeMenuItem(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool RemoveMenu(string menu)
        {
            return tguiMenuBar_removeMenu(CPointer, Util.ConvertStringForC_UTF32(menu)) != 0;
        }

        public bool RemoveMenuItem(string menu, string menuItem)
        {
            return tguiMenuBar_removeMenuItem(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(menuItem)) != 0;
        }

        public bool RemoveMenuItem(ReadOnlySpan<string> hierarchy, bool removeParentsWhenEmpty)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_removeMenuItemHierarchy(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, removeParentsWhenEmpty ? (byte)1 : (byte)0) != 0;
        }

        public void RemoveAllMenus()
        {
            tguiMenuBar_removeAllMenus(CPointer);
        }

        public bool SetMenuEnabled(string text, bool enabled)
        {
            return tguiMenuBar_setMenuEnabled(CPointer, Util.ConvertStringForC_UTF32(text), enabled ? (byte)1 : (byte)0) != 0;
        }

        public bool GetMenuEnabled(string text)
        {
            return tguiMenuBar_getMenuEnabled(CPointer, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool SetMenuItemEnabled(string menu, string text, bool enabled)
        {
            return tguiMenuBar_setMenuItemEnabled(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(text), enabled ? (byte)1 : (byte)0) != 0;
        }

        public bool GetMenuItemEnabled(string menu, string text)
        {
            return tguiMenuBar_getMenuItemEnabled(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public bool SetMenuItemEnabled(ReadOnlySpan<string> hierarchy, bool enabled)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_setMenuItemEnabledHierarchy(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, enabled ? (byte)1 : (byte)0) != 0;
        }

        public bool GetMenuItemEnabled(ReadOnlySpan<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_getMenuItemEnabledHierarchy(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length) != 0;
        }

        public void CloseMenu()
        {
            tguiMenuBar_closeMenu(CPointer);
        }

        public float MinimumSubMenuWidth
        {
            get => tguiMenuBar_getMinimumSubMenuWidth(CPointer);
            set => tguiMenuBar_setMinimumSubMenuWidth(CPointer, value);
        }

        public bool InvertedMenuDirection
        {
            get => tguiMenuBar_getInvertedMenuDirection(CPointer) != 0;
            set => tguiMenuBar_setInvertedMenuDirection(CPointer, value ? (byte)1 : (byte)0);
        }

        public class MenuItemClickEventArgs : EventArgs
        {
            public MenuItemClickEventArgs(string[] hierarchy)
            {
                Hierarchy = hierarchy;
            }
            public string[] Hierarchy { get; }
        }
        public unsafe event EventHandler<MenuItemClickEventArgs> OnMenuItemClick
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackItemHierarchy func = (UIntPtr count, IntPtr* strings) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    string[] stringArray = new string[(int)count];
                    for (int i = 0; i < (int)count; ++i)
                        stringArray[i] = Util.GetStringFromC_UTF32(strings[i]);
                    value(sender, new MenuItemClickEventArgs(stringArray));
                };
                uint id = tguiWidget_signalItemHierarchyConnect(CPointer, Util.ConvertStringForC_UTF32("MenuItemClicked"), func);
                ConnectEventHandler(id, "MenuItemClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("MenuItemClicked", value);
            }
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void tguiMenuBarMenuList_destroy(MenuListImpl* menuList);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe MenuListImpl* tguiMenuBar_getMenus(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiMenuBar_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBar_addMenu(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_addMenuItem(IntPtr cPointer, IntPtr menu, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_addMenuItemToLastMenu(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_addMenuItemHierarchy(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, byte createParents);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_changeMenuItem(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_removeMenu(IntPtr cPointer, IntPtr menu);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_removeMenuItem(IntPtr cPointer, IntPtr menu, IntPtr menuItem);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_removeMenuItemHierarchy(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, byte removeParentsWhenEmpty);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBar_removeAllMenus(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_setMenuEnabled(IntPtr cPointer, IntPtr text, byte enabled);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_getMenuEnabled(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_setMenuItemEnabled(IntPtr cPointer, IntPtr menu, IntPtr text, byte enabled);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_getMenuItemEnabled(IntPtr cPointer, IntPtr menu, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_setMenuItemEnabledHierarchy(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, byte enabled);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_getMenuItemEnabledHierarchy(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBar_closeMenu(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiMenuBar_getMinimumSubMenuWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBar_setMinimumSubMenuWidth(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiMenuBar_getInvertedMenuDirection(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiMenuBar_setInvertedMenuDirection(IntPtr cPointer, byte value);

        #endregion
    }
}
