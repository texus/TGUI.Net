using System.Collections.Generic;

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

#region Imports

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe void tguiTreeViewConstNode_destroy(ConstNodeImpl* node);

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe ConstNodeImpl* tguiTreeView_getNode(IntPtr cPointer, IntPtr[] hierarchy, UIntPtr hierarchyLength); // tguiTreeViewConstNode_destroy must be called on the returned value

[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
private static extern unsafe ConstNodeImpl** tguiTreeView_getNodes(IntPtr cPointer, out UIntPtr count);

#endregion
