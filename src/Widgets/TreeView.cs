// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// TreeView widget
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

        public struct ConstNode
        {
            public bool expanded;
            public string text;
            public List<ConstNode> nodes;
        }

        public unsafe ConstNode GetNode(ReadOnlySpan<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            ConstNodeImpl* nodeC = tguiTreeView_getNode(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length);

            GetNodeImpl(out ConstNode node, in *nodeC);
            return node;
        }

        public unsafe IReadOnlyList<ConstNode> GetNodes()
        {
            ConstNodeImpl** nodesC = tguiTreeView_getNodes(CPointer, out UIntPtr count);
            ConstNode[] nodes = new ConstNode[(int)count];
            for (int i = 0; i < (int)count; ++i)
            {
                GetNodeImpl(out nodes[i], in *nodesC[i]);
            }
            return nodes;
        }

        private unsafe void GetNodeImpl(out ConstNode nodeToFill, in ConstNodeImpl nodeC)
        {
            nodeToFill.expanded = nodeC.expanded != 0;
            nodeToFill.text = Util.GetStringFromC_UTF32(nodeC.text);
            nodeToFill.nodes = new List<ConstNode>();
            if ((int)nodeC.nodesCount > 0)
            {
                for (int i = 0; i < (int)nodeC.nodesCount; ++i)
                {
                    GetNodeImpl(out ConstNode childNode, in nodeC.nodes[i]);
                    nodeToFill.nodes.Add(childNode);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct ConstNodeImpl
        {
            public byte expanded;
            public IntPtr text;
            public ConstNodeImpl* nodes;
            public UIntPtr nodesCount;
        }

        public ScrollbarAccessor VerticalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getVerticalScrollbar(CPointer));
        public ScrollbarAccessor HorizontalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getHorizontalScrollbar(CPointer));

        public new TreeViewRenderer Renderer
        {
            get => new TreeViewRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new TreeViewRenderer SharedRenderer => new TreeViewRenderer(tguiWidget_getSharedRenderer(CPointer));

        public int ItemHeight
        {
            get => (int)tguiTreeView_getItemHeight(CPointer);
            set => tguiTreeView_setItemHeight(CPointer, (uint)value);
        }

        public bool AddItem(ReadOnlySpan<string> hierarchy, bool createParents = true)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiTreeView_addItem(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, createParents ? (byte)1 : (byte)0) != 0;
        }

        public bool ChangeItem(ReadOnlySpan<string> hierarchy, string leafText)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiTreeView_changeItem(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, Util.ConvertStringForC_UTF32(leafText)) != 0;
        }

        public void Expand(ReadOnlySpan<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            tguiTreeView_expand(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length);
        }

        public void Collapse(ReadOnlySpan<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            tguiTreeView_collapse(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length);
        }

        public void ExpandAll()
        {
            tguiTreeView_expandAll(CPointer);
        }

        public void CollapseAll()
        {
            tguiTreeView_collapseAll(CPointer);
        }

        public void DeselectItem()
        {
            tguiTreeView_deselectItem(CPointer);
        }

        public void RemoveAllItems()
        {
            tguiTreeView_removeAllItems(CPointer);
        }

        public IReadOnlyList<string> GetSelectedItem()
        {
            unsafe
            {
                IntPtr* returnStringsC = tguiTreeView_getSelectedItem(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public IReadOnlyList<string> GetHoveredItem()
        {
            unsafe
            {
                IntPtr* returnStringsC = tguiTreeView_getHoveredItem(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public bool SetItemIndexInParent(ReadOnlySpan<string> hierarchy, int index)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiTreeView_setItemIndexInParent(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, (UIntPtr)index) != 0;
        }

        public int GetItemIndexInParent(ReadOnlySpan<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiTreeView_getItemIndexInParent(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length);
        }

        public bool SelectItem(ReadOnlySpan<string> hierarchy)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiTreeView_selectItem(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length) != 0;
        }

        public bool RemoveItem(ReadOnlySpan<string> hierarchy, bool removeParentsWhenEmpty = true)
        {
            IntPtr[] hierarchyForC = new IntPtr[hierarchy.Length];
            for (int i = 0; i < hierarchy.Length; ++i)
                hierarchyForC[i] = Util.ConvertStringForC_UTF32(hierarchy[i]);

            return tguiTreeView_removeItem(CPointer, hierarchyForC, (UIntPtr)hierarchyForC.Length, removeParentsWhenEmpty ? (byte)1 : (byte)0) != 0;
        }

        public class ItemSelectEventArgs : EventArgs
        {
            public ItemSelectEventArgs(string[] hierarchy)
            {
                Hierarchy = hierarchy;
            }
            public string[] Hierarchy { get; }
        }
        public unsafe event EventHandler<ItemSelectEventArgs> OnItemSelect
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
                    value(sender, new ItemSelectEventArgs(stringArray));
                };
                uint id = tguiWidget_signalItemHierarchyConnect(CPointer, Util.ConvertStringForC_UTF32("ItemSelected"), func);
                ConnectEventHandler(id, "ItemSelected", value, func);
            }
            remove
            {
                DisconnectEventHandler("ItemSelected", value);
            }
        }

        public class DoubleClickEventArgs : EventArgs
        {
            public DoubleClickEventArgs(string[] hierarchy)
            {
                Hierarchy = hierarchy;
            }
            public string[] Hierarchy { get; }
        }
        public unsafe event EventHandler<DoubleClickEventArgs> OnDoubleClick
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
                    value(sender, new DoubleClickEventArgs(stringArray));
                };
                uint id = tguiWidget_signalItemHierarchyConnect(CPointer, Util.ConvertStringForC_UTF32("DoubleClicked"), func);
                ConnectEventHandler(id, "DoubleClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("DoubleClicked", value);
            }
        }

        public class ExpandEventArgs : EventArgs
        {
            public ExpandEventArgs(string[] hierarchy)
            {
                Hierarchy = hierarchy;
            }
            public string[] Hierarchy { get; }
        }
        public unsafe event EventHandler<ExpandEventArgs> OnExpand
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
                    value(sender, new ExpandEventArgs(stringArray));
                };
                uint id = tguiWidget_signalItemHierarchyConnect(CPointer, Util.ConvertStringForC_UTF32("Expanded"), func);
                ConnectEventHandler(id, "Expanded", value, func);
            }
            remove
            {
                DisconnectEventHandler("Expanded", value);
            }
        }

        public class CollapseEventArgs : EventArgs
        {
            public CollapseEventArgs(string[] hierarchy)
            {
                Hierarchy = hierarchy;
            }
            public string[] Hierarchy { get; }
        }
        public unsafe event EventHandler<CollapseEventArgs> OnCollapse
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
                    value(sender, new CollapseEventArgs(stringArray));
                };
                uint id = tguiWidget_signalItemHierarchyConnect(CPointer, Util.ConvertStringForC_UTF32("Collapsed"), func);
                ConnectEventHandler(id, "Collapsed", value, func);
            }
            remove
            {
                DisconnectEventHandler("Collapsed", value);
            }
        }

        public class RightClickEventArgs : EventArgs
        {
            public RightClickEventArgs(string[] hierarchy)
            {
                Hierarchy = hierarchy;
            }
            public string[] Hierarchy { get; }
        }
        public unsafe event EventHandler<RightClickEventArgs> OnRightClick
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
                    value(sender, new RightClickEventArgs(stringArray));
                };
                uint id = tguiWidget_signalItemHierarchyConnect(CPointer, Util.ConvertStringForC_UTF32("RightClicked"), func);
                ConnectEventHandler(id, "RightClicked", value, func);
            }
            remove
            {
                DisconnectEventHandler("RightClicked", value);
            }
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void tguiTreeViewConstNode_destroy(ConstNodeImpl* node);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe ConstNodeImpl* tguiTreeView_getNode(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength); // tguiTreeViewConstNode_destroy must be called on the returned value

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe ConstNodeImpl** tguiTreeView_getNodes(IntPtr cPointer, out UIntPtr count);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTreeView_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiDualScrollbarChildInterface_getVerticalScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiDualScrollbarChildInterface_getHorizontalScrollbar(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint tguiTreeView_getItemHeight(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTreeView_setItemHeight(IntPtr cPointer, uint value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTreeView_addItem(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, byte createParents);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTreeView_changeItem(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, IntPtr leafText);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTreeView_expand(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTreeView_collapse(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTreeView_expandAll(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTreeView_collapseAll(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTreeView_deselectItem(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTreeView_removeAllItems(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiTreeView_getSelectedItem(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiTreeView_getHoveredItem(IntPtr cPointer, out UIntPtr returnCount);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTreeView_setItemIndexInParent(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, UIntPtr index);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int tguiTreeView_getItemIndexInParent(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTreeView_selectItem(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTreeView_removeItem(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength, byte removeParentsWhenEmpty);

        #endregion
    }
}
