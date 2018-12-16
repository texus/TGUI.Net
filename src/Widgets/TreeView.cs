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
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace TGUI
{
    /// <summary>
    /// Tree view widget
    /// </summary>
    public class TreeView : Widget
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TreeView()
            : base(tguiTreeView_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal TreeView(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public TreeView(TreeView copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After calling this function, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new TreeViewRenderer Renderer
        {
            get { return new TreeViewRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new TreeViewRenderer SharedRenderer
        {
            get { return new TreeViewRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Adds a new item to the tree view
        /// </summary>
        /// <param name="hierarchy">Hierarchy of items, with the last item being the leaf item</param>
        /// <param name="createParents">Should the hierarchy be created if it did not exist yet?</param>
        /// <returns>
        /// True when the item was added (always the case if createParents is true)
        /// </returns>
        /// <example>
        /// <code>
        /// treeView.AddItem(new List&lt;string&gt;{"Parent_1", "Child_1"});
        /// treeView.AddItem(new List&lt;string&gt;{"Parent_2", "Child_2", "Grandchild"});
        /// </code>
        /// </example>
        public bool AddItem(List<string> hierarchy, bool createParents = true)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Count];
            for (int i = 0; i < hierarchy.Count; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiTreeView_addItem(CPointer, hierarchyForC, (uint)hierarchyForC.Length, createParents);
        }

        /// <summary>
        /// Expands the given item
        /// </summary>
        /// <param name="hierarchy">Hierarchy of items, identifying the node that has to be expanded</param>
        public void Expand(List<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Count];
            for (int i = 0; i < hierarchy.Count; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            tguiTreeView_expand(CPointer, hierarchyForC, (uint)hierarchyForC.Length);
        }

        /// <summary>
        /// Expands all items
        /// </summary>
        public void ExpandAll()
        {
            tguiTreeView_expandAll(CPointer);
        }

        /// <summary>
        /// Collapses the given item
        /// </summary>
        /// <param name="hierarchy">Hierarchy of items, identifying the node that has to be collapsed</param>
        public void Collapse(List<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Count];
            for (int i = 0; i < hierarchy.Count; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            tguiTreeView_collapse(CPointer, hierarchyForC, (uint)hierarchyForC.Length);
        }

        /// <summary>
        /// Collapse all items
        /// </summary>
        public void CollapseAll()
        {
            tguiTreeView_collapseAll(CPointer);
        }

        /// <summary>
        /// Removes an item
        /// </summary>
        /// <param name="hierarchy">Hierarchy of items, identifying the node to be removed</param>
        /// <param name="removeParentsWhenEmpty">Also delete the parent of the deleted item if it has no other children</param>
        /// <returns>
        /// True when the item existed and was removed, false when hierarchy was incorrect
        /// </returns>
        /// <example>
        /// <code>
        /// treeView.RemoveItem(new List&lt;string&gt;{"Parent_1", "Child_1"});
        /// treeView.RemoveItem(new List&lt;string&gt;{"Parent_2", "Child_2", "Grandchild"});
        /// </code>
        /// </example>
        public bool RemoveItem(List<string> hierarchy, bool removeParentsWhenEmpty = true)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Count];
            for (int i = 0; i < hierarchy.Count; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiTreeView_removeItem(CPointer, hierarchyForC, (uint)hierarchyForC.Length, removeParentsWhenEmpty);
        }

        /// <summary>
        /// Removes all items
        /// </summary>
        public void RemoveAllItems()
        {
            tguiTreeView_removeAllItems(CPointer);
        }

        /// <summary>
        /// Deselect the item if one was selected
        /// </summary>
        public void DeselectItem()
        {
            tguiTreeView_deselectItem(CPointer);
        }

        /// <summary>
        /// Gets or sets the item height
        /// </summary>
        public uint ItemHeight
        {
            get { return tguiTreeView_getItemHeight(CPointer); }
            set { tguiTreeView_setItemHeight(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the character size of the text
        /// </summary>
        public uint TextSize
        {
            get { return tguiTreeView_getTextSize(CPointer); }
            set { tguiTreeView_setTextSize(CPointer, value); }
        }

        protected override void InitSignals()
        {
            base.InitSignals();

            ItemSelectedCallback = new CallbackActionString(ProcessItemSelectedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("ItemSelected"), ItemSelectedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            DoubleClickedCallback = new CallbackActionString(ProcessDoubleClickedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("DoubleClicked"), DoubleClickedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            ExpandedCallback = new CallbackActionString(ProcessExpandedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("Expanded"), ExpandedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            CollapsedCallback = new CallbackActionString(ProcessCollapsedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("Collapsed"), CollapsedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessItemSelectedSignal(IntPtr item)
        {
            ItemSelected?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(item)));
        }

        private void ProcessDoubleClickedSignal(IntPtr item)
        {
            DoubleClicked?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(item)));
        }

        private void ProcessExpandedSignal(IntPtr item)
        {
            Expanded?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(item)));
        }

        private void ProcessCollapsedSignal(IntPtr item)
        {
            Collapsed?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(item)));
        }

        /// <summary>Event handler for the ItemSelected signal</summary>
        public event EventHandler<SignalArgsString> ItemSelected = null;

        /// <summary>Event handler for the DoubleClicked signal</summary>
        public event EventHandler<SignalArgsString> DoubleClicked = null;

        /// <summary>Event handler for the Expanded signal</summary>
        public event EventHandler<SignalArgsString> Expanded = null;

        /// <summary>Event handler for the Collapsed signal</summary>
        public event EventHandler<SignalArgsString> Collapsed = null;

        private CallbackActionString ItemSelectedCallback;
        private CallbackActionString DoubleClickedCallback;
        private CallbackActionString ExpandedCallback;
        private CallbackActionString CollapsedCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTreeView_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiTreeView_addItem(IntPtr cPointer, IntPtr[] hierarcy, uint hierarchyLength, bool createParents);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTreeView_expand(IntPtr cPointer, IntPtr[] hierarcy, uint hierarchyLength);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTreeView_expandAll(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTreeView_collapse(IntPtr cPointer, IntPtr[] hierarcy, uint hierarchyLength);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTreeView_collapseAll(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiTreeView_removeItem(IntPtr cPointer, IntPtr[] hierarcy, uint hierarchyLength, bool removeParentsWhenEmpty);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTreeView_removeAllItems(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTreeView_deselectItem(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTreeView_setItemHeight(IntPtr cPointer, uint itemHeight);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiTreeView_getItemHeight(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTreeView_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected uint tguiTreeView_getTextSize(IntPtr cPointer);

        #endregion
    }
}
