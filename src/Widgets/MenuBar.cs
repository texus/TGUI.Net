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
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace TGUI
{
    /// <summary>
    /// Menu bar widget
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

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After calling this function, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new MenuBarRenderer Renderer
        {
            get { return new MenuBarRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new MenuBarRenderer SharedRenderer
        {
            get { return new MenuBarRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Adds a new menu
        /// </summary>
        /// <param name="text">The text written on the menu</param>
        public void AddMenu(string text)
        {
            tguiMenuBar_addMenu(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        /// <summary>
        /// Adds a new menu item to the last added menu
        /// </summary>
        /// <param name="text">The text written on this menu item</param>
        /// <returns>
        /// True when the item was added, false when the menu bar doesn't contain any menus yet
        /// </returns>
        /// <example>
        /// <code>
        /// menuBar.AddMenu("File");
        /// menuBar.AddMenuItem("Load");
        /// menuBar.AddMenuItem("Save");
        /// menuBar.AddMenu("Edit");
        /// menuBar.AddMenuItem("Undo");
        /// </code>
        /// </example>
        public bool AddMenuItem(string text)
        {
            return tguiMenuBar_addMenuItemToLastMenu(CPointer, Util.ConvertStringForC_UTF32(text));
        }

        /// <summary>
        /// Adds a new menu item to an existing menu
        /// </summary>
        /// <param name="menu">The name of the menu to which the menu item will be added</param>
        /// <param name="text">The text written on this menu item</param>
        /// <returns>
        /// True when the item was added, false when menu was not found
        /// </returns>
        /// <example>
        /// <code>
        /// menuBar.AddMenu("File");
        /// menuBar.AddMenu("Edit");
        /// menuBar.AddMenuItem("File", "Load");
        /// menuBar.AddMenuItem("File", "Save");
        /// menuBar.AddMenuItem("Edit", "Undo");
        /// </code>
        /// </example>
        public bool AddMenuItem(string menu, string text)
        {
            return tguiMenuBar_addMenuItem(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(text));
        }

        /// <summary>
        /// Adds a new menu item (or sub menu item)
        /// </summary>
        /// <param name="hierarchy">Hierarchy of the menu items, starting with the menu and ending with menu item to be added</param>
        /// <param name="createParents">Should the hierarchy be created if it did not exist yet?</param>
        /// <returns>
        /// True when the item was added, false when createParents was false and the parents hierarchy does not exist
        /// or if hierarchy does not contain at least 2 elements.
        /// </returns>
        /// <example>
        /// <code>
        /// menuBar.AddMenuItem(new List&lt;string&gt;{"File", "Save"});
        /// menuBar.AddMenuItem(new List&lt;string&gt;{"View", "Messages", "Tags", "Important"});
        /// </code>
        /// </example>
        public bool AddMenuItem(List<string> hierarchy, bool createParents = true)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Count];
            for (int i = 0; i < hierarchy.Count; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_addMenuItemHierarchy(CPointer, hierarchyForC, (uint)hierarchyForC.Length, createParents);
        }

        /// <summary>
        /// Removes all menus
        /// </summary>
        public void RemoveAllMenus()
        {
            tguiMenuBar_removeAllMenus(CPointer);
        }

        /// <summary>
        /// Removes a menu
        /// </summary>
        /// <param name="menu">The name of the menu to remove</param>
        /// <returns>
        /// True when the menu was removed, false when menu was not found
        /// </returns>
        public bool RemoveMenu(string menu)
        {
            return tguiMenuBar_removeMenu(CPointer, Util.ConvertStringForC_UTF32(menu));
        }

        /// <summary>
        /// Removes a menu item
        /// </summary>
        /// <param name="menu">The name of the menu in which the menu item is located</param>
        /// <param name="menuItem">The name of the menu item to remove</param>
        /// <returns>
        /// True when the item was removed, false when menu or menuItem was not found
        /// </returns>
        public bool RemoveMenuItem(string menu, string menuItem)
        {
            return tguiMenuBar_removeMenuItem(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(menuItem));
        }

        /// <summary>
        /// Removes a menu item (or sub menu item)
        /// </summary>
        /// <param name="hierarchy">Hierarchy of the menu item, starting with the menu and ending with menu item to be deleted</param>
        /// <param name="removeParentsWhenEmpty">Also delete the parent of the deleted menu item if it has no other children</param>
        /// <returns>
        /// True when the menu item exists, false when hierarchy was incorrect
        /// </returns>
        /// <example>
        /// <code>
        /// menuBar.RemoveMenuItem(new List&lt;string&gt;{"File", "Save"});
        /// menuBar.RemoveMenuItem(new List&lt;string&gt;{"View", "Messages", "Tags", "Important"});
        /// </code>
        /// </example>
        public bool RemoveMenuItem(List<string> hierarchy, bool removeParentsWhenEmpty = true)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Count];
            for (int i = 0; i < hierarchy.Count; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_removeMenuItemHierarchy(CPointer, hierarchyForC, (uint)hierarchyForC.Length, removeParentsWhenEmpty);
        }

        /// <summary>
        /// Enable or disable an entire menu
        /// </summary>
        /// <param name="menu">The name of the menu to enable or disable</param>
        /// <param name="enabled">Should the menu be enabled or disabled?</param>
        /// <returns>
        /// True when the menu exists, false when menu was not found
        /// </returns>
        public bool SetMenuEnabled(string menu, bool enabled)
        {
            return tguiMenuBar_setMenuEnabled(CPointer, Util.ConvertStringForC_UTF32(menu), enabled);
        }

        /// <summary>
        /// Check if an entire menu is enabled or disabled
        /// </summary>
        /// <param name="menu">The name of the menu to check</param>
        /// <returns>
        /// True if the menu is enabled, false if it was disabled or when the menu did not exist
        /// </returns>
        public bool GetMenuEnabled(string menu)
        {
            return tguiMenuBar_getMenuEnabled(CPointer, Util.ConvertStringForC_UTF32(menu));
        }

        /// <summary>
        /// Enable or disable a menu item
        /// </summary>
        /// <param name="menu">The name of the menu in which the menu item is located</param>
        /// <param name="menuItem">The name of the menu item to enable or disable</param>
        /// <param name="enabled">Should the menu item be enabled or disabled?</param>
        /// <returns>
        /// True when the menu item exists, false when menu or menuItem was not found
        /// </returns>
        public bool SetMenuItemEnabled(string menu, string menuItem, bool enabled)
        {
            return tguiMenuBar_setMenuItemEnabled(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(menuItem), enabled);
        }

        /// <summary>
        /// Check if a menu item is enabled or disabled
        /// </summary>
        /// <param name="menu">The name of the menu in which the menu item is located</param>
        /// <param name="menuItem">The name of the menu item to check</param>
        /// <returns>
        /// True if the menu item is enabled, false if it was disabled or when the menu or menuItem did not exist
        /// </returns>
        public bool GetMenuItemEnabled(string menu, string menuItem)
        {
            return tguiMenuBar_getMenuItemEnabled(CPointer, Util.ConvertStringForC_UTF32(menu), Util.ConvertStringForC_UTF32(menuItem));
        }

        /// <summary>
        /// Enable or disable a menu item
        /// </summary>
        /// <param name="hierarchy">Hierarchy of menu items, starting with the menu and ending with the menu item to enable/disable</param>
        /// <param name="enabled">Should the menu item be enabled or disabled?</param>
        /// <returns>
        /// True when the menu item exists, false when hierarchy was incorrect
        /// </returns>
        public bool SetMenuItemEnabled(List<string> hierarchy, bool enabled)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Count];
            for (int i = 0; i < hierarchy.Count; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_setMenuItemEnabledHierarchy(CPointer, hierarchyForC, (uint)hierarchyForC.Length, enabled);
        }

        /// <summary>
        /// Check if a menu item is enabled or disabled
        /// </summary>
        /// <param name="hierarchy">Hierarchy of menu items, starting with the menu and ending with the menu item to check</param>
        /// <returns>
        /// True if the menu item is enabled, false if it was disabled or when the hierarchy was incorrect
        /// </returns>
        public bool GetMenuItemEnabled(List<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Count];
            for (int i = 0; i < hierarchy.Count; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiMenuBar_getMenuItemEnabledHierarchy(CPointer, hierarchyForC, (uint)hierarchyForC.Length);
        }

        /// <summary>
        /// Gets or sets the character size of the text
        /// </summary>
        public uint TextSize
        {
            get { return tguiMenuBar_getTextSize(CPointer); }
            set { tguiMenuBar_setTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the minimum width of the submenus
        /// </summary>
        /// <remarks>
        /// When a submenu is displayed, the width will be either this or the width of the longest text in the submenu.
        /// The default minimum width is 125 pixels.
        /// </remarks>
        public float MinimumSubMenuWidth
        {
            get { return tguiMenuBar_getMinimumSubMenuWidth(CPointer); }
            set { tguiMenuBar_setMinimumSubMenuWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the menus open above or below the menu bar
        /// </summary>
        public bool InvertedMenuDirection
        {
            get { return tguiMenuBar_getInvertedMenuDirection (CPointer); }
            set { tguiMenuBar_setInvertedMenuDirection (CPointer, value); }
        }

        /// <summary>
        /// Closes the open menu when one of the menus is open
        /// </summary>
        public void CloseMenu()
        {
            tguiMenuBar_closeMenu(CPointer);
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            MenuItemClickedCallback = new CallbackActionString(ProcessMenuItemClickedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("MenuItemClicked"), MenuItemClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessMenuItemClickedSignal(IntPtr menuItem)
        {
            MenuItemClicked?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(menuItem)));
        }

        /// <summary>Event handler for the ItemSelected signal</summary>
        public event EventHandler<SignalArgsString> MenuItemClicked = null;

        private CallbackActionString MenuItemClickedCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiMenuBar_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBar_addMenu(IntPtr cPointer, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_addMenuItem(IntPtr cPointer, IntPtr menu, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_addMenuItemHierarchy(IntPtr cPointer, IntPtr[] hierarcy, uint hierarchyLength, bool createParents);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_addMenuItemToLastMenu(IntPtr cPointer, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_removeMenu(IntPtr cPointer, IntPtr menu);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBar_removeAllMenus(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_removeMenuItem(IntPtr cPointer, IntPtr menu, IntPtr item);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_removeMenuItemHierarchy(IntPtr cPointer, IntPtr[] hierarcy, uint hierarchyLength, bool removeParentsWhenEmpty);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_setMenuEnabled(IntPtr cPointer, IntPtr text, bool enabled);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_getMenuEnabled(IntPtr cPointer, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_setMenuItemEnabled(IntPtr cPointer, IntPtr menu, IntPtr text, bool enabled);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_getMenuItemEnabled(IntPtr cPointer, IntPtr menu, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_setMenuItemEnabledHierarchy(IntPtr cPointer, IntPtr[] hierarcy, uint hierarchyLength, bool enabled);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_getMenuItemEnabledHierarchy(IntPtr cPointer, IntPtr[] hierarcy, uint hierarchyLength);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBar_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiMenuBar_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBar_setMinimumSubMenuWidth(IntPtr cPointer, float minimumSubMenuWidth);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private float tguiMenuBar_getMinimumSubMenuWidth(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBar_setInvertedMenuDirection(IntPtr cPointer, bool invertDirection);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiMenuBar_getInvertedMenuDirection(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiMenuBar_closeMenu(IntPtr cPointer);

        #endregion
    }
}
