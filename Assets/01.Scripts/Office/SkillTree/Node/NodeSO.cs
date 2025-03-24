using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "SO/SkillTreeNode")]
public abstract class NodeSO : ScriptableObject
{
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;

    [HideInInspector] public int id;
    [HideInInspector] public List<NodeSO> nextNodes;
    [HideInInspector] public NodeSO prevNode;

    public Sprite icon;
    public int requireCoin;


    public string nodeName;
    [TextArea]
    public string explain;
}
