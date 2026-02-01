// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// ContextMenu widget
    /// </summary>
    public class ContextMenu : MenuWidgetBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ContextMenu()
            : base(tguiContextMenu_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal ContextMenu(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public ContextMenu(ContextMenu copy)
            : base(copy)
        {
        }

        public struct MenuItem
        {
            public string text;
            public bool enabled;
            public List<MenuItem> subMenuItems;
        }

        public unsafe IReadOnlyList<MenuItem> GetMenuItems()
        {
            var menuItems = new List<MenuItem>();
            MenuItemListImpl* menuItemList = tguiContextMenu_getMenuItems(CPointer);
            GetMenusImpl(menuItems, menuItemList->menuItems, (int)menuItemList->menuItemsCount);
            tguiContextMenuItemList_destroy(menuItemList);
            return menuItems;
        }

        private unsafe void GetMenusImpl(List<MenuItem> menusToFill, MenuElementImpl* menuElementPtr, int menuItemsCount)
        {
            for (int i = 0; i < menuItemsCount; ++i)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.text = Util.GetStringFromC_UTF32(menuElementPtr[i].text);
                menuItem.enabled = menuElementPtr[i].enabled != 0;
                menuItem.subMenuItems = new List<MenuItem>();
                if ((int)menuElementPtr[i].menuItemsCount > 0)
                    GetMenusImpl(menuItem.subMenuItems, menuElementPtr[i].menuItems, (int)menuElementPtr[i].menuItemsCount);

                menusToFill.Add(menuItem);
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
        private unsafe struct MenuItemListImpl
        {
            public MenuElementImpl* menuItems;
            public UIntPtr menuItemsCount;
        }

        public new ContextMenuRenderer Renderer
        {
            get => new ContextMenuRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public new ContextMenuRenderer SharedRenderer => new ContextMenuRenderer(tguiWidget_getSharedRenderer(CPointer));

        public bool IsMenuOpen()
        {
            return tguiContextMenu_isMenuOpen(CPointer) != 0;
        }

        public void OpenMenu()
        {
            tguiContextMenu_openMenu(CPointer);
        }

        public void OpenMenu(Vector2f position)
        {
            tguiContextMenu_openMenuAtPos(CPointer, position);
        }

        public void OpenMenuAtMouseCursor()
        {
            tguiContextMenu_openMenuAtMouseCursor(CPointer);
        }

        public void CloseMenu()
        {
            tguiContextMenu_closeMenu(CPointer);
        }

        public void AddMenuItem(string text)
        {
            tguiContextMenu_addMenuItem(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        public bool AddMenuItem(ReadOnlySpan<string> hierarchy, bool createParents = true)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiContextMenu_addMenuItemHierarchy(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, createParents ? (byte)1 : (byte)0) != 0;
        }

        public bool ChangeMenuItem(ReadOnlySpan<string> hierarchy, string text)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiContextMenu_changeMenuItem(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, Util.ConvertStringForC_UTF32(text)) != 0;
        }

        public void RemoveAllMenuItems()
        {
            tguiContextMenu_removeAllMenuItems(CPointer);
        }

        public bool RemoveMenuItem(string menuItem)
        {
            return tguiContextMenu_removeMenuItem(CPointer, Util.ConvertStringForC_UTF32(menuItem)) != 0;
        }

        public bool RemoveMenuItem(ReadOnlySpan<string> hierarchy, bool removeParentsWhenEmpty = true)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiContextMenu_removeMenuItemHierarchy(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, removeParentsWhenEmpty ? (byte)1 : (byte)0) != 0;
        }

        public bool RemoveSubMenuItems(ReadOnlySpan<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiContextMenu_removeSubMenuItems(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length) != 0;
        }

        public bool SetMenuItemEnabled(string menuItem, bool enabled)
        {
            return tguiContextMenu_setMenuItemEnabled(CPointer, Util.ConvertStringForC_UTF32(menuItem), enabled ? (byte)1 : (byte)0) != 0;
        }

        public bool GetMenuItemEnabled(string menuItem)
        {
            return tguiContextMenu_getMenuItemEnabled(CPointer, Util.ConvertStringForC_UTF32(menuItem)) != 0;
        }

        public bool SetMenuItemEnabled(ReadOnlySpan<string> hierarchy, bool enabled)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiContextMenu_setMenuItemEnabledHierarchy(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, enabled ? (byte)1 : (byte)0) != 0;
        }

        public bool GetMenuItemEnabled(ReadOnlySpan<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiContextMenu_getMenuItemEnabledHierarchy(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length) != 0;
        }

        public float ItemHeight
        {
            get => tguiContextMenu_getItemHeight(CPointer);
            set => tguiContextMenu_setItemHeight(CPointer, value);
        }

        public float MinimumMenuWidth
        {
            get => tguiContextMenu_getMinimumMenuWidth(CPointer);
            set => tguiContextMenu_setMinimumMenuWidth(CPointer, value);
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
        private static extern unsafe void tguiContextMenuItemList_destroy(MenuItemListImpl* menuItemList);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe MenuItemListImpl* tguiContextMenu_getMenuItems(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiContextMenu_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_isMenuOpen(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContextMenu_openMenu(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContextMenu_openMenuAtPos(IntPtr cPointer, Vector2f position);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContextMenu_openMenuAtMouseCursor(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContextMenu_closeMenu(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContextMenu_addMenuItem(IntPtr cPointer, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_addMenuItemHierarchy(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, byte createParents);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_changeMenuItem(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, IntPtr text);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContextMenu_removeAllMenuItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_removeMenuItem(IntPtr cPointer, IntPtr menuItem);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_removeMenuItemHierarchy(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, byte removeParentsWhenEmpty);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_removeSubMenuItems(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_setMenuItemEnabled(IntPtr cPointer, IntPtr menuItem, byte enabled);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_getMenuItemEnabled(IntPtr cPointer, IntPtr menuItem);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_setMenuItemEnabledHierarchy(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, byte enabled);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiContextMenu_getMenuItemEnabledHierarchy(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiContextMenu_getItemHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContextMenu_setItemHeight(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiContextMenu_getMinimumMenuWidth(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiContextMenu_setMinimumMenuWidth(IntPtr cPointer, float value);

        #endregion
    }
}
