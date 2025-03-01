using System.Collections.Generic;

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

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe void tguiContextMenuItemList_destroy(MenuItemListImpl* menuItemList);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe MenuItemListImpl* tguiContextMenu_getMenuItems(IntPtr cPointer);

#endregion
