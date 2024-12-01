using System.Collections.Generic;

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

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe void tguiMenuBarMenuList_destroy(MenuListImpl* menuList);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe MenuListImpl* tguiMenuBar_getMenus(IntPtr cPointer);

#endregion
