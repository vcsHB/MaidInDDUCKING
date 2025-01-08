using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class SkillTree : MonoBehaviour
{
    public TechTreeSO treeSO;
    public Dictionary<NodeSO, Node> nodeDic;
    //public WarningPanel warningPanel;
    //public TechTreeTooltipPanel tooltipPanel;
    [SerializeField] private Node nodePf;

    public Transform edgeParent;
    public Transform edgeFillParent;
    private string _path;

    [SerializeField] private RectTransform _treeRect;

    public UnityEvent<int, int> selectNodeEvent;

    private void Awake()
    {
        nodeDic = new Dictionary<NodeSO, Node>();
        _path = Path.Combine(Application.dataPath, "TechTree.json");
    }

    private void Start()
    {
        int childCnt = transform.childCount;

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            for (int j = 0; j < childCnt; j++)
            {
                if (transform.GetChild(j).TryGetComponent(out Node node))
                {
                    if (treeSO.nodes[i].id != node.NodeType.id) continue;

                    nodeDic.Add(node.NodeType, node);
                }
            }
        }

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            NodeSO nodeSO = treeSO.nodes[i];

            if (nodeDic.TryGetValue(nodeSO, out Node node))
                node.Init(node.NodeType.id == 0);
        }

        Load();
    }

    [ContextMenu("CreateNodes")]
    private void CreateNodes()
    {
        treeSO.nodes.ForEach(node =>
        {
            Node nodeInstance = Instantiate(nodePf, transform);
            nodeInstance.SetNode(node);

            if (node is PartNodeSO part)
            {
                //nodeInstance.name = part.openPart.ToString();
            }
            else if (node is WeaponNodeSO weapon)
            {
                //nodeInstance.name = weapon.weapon.ToString();
            }
        });
    }

    [ContextMenu("RefreshNodes")]
    private void RefreshNode()
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if (transform.GetChild(i).TryGetComponent(out Node node))
        //    {
        //        Node nodeInstance = Instantiate(nodePf, transform);
        //        nodeInstance.SetEdgePosition(node.GetEdgePosition());
        //        nodeInstance.SetPosition(node.GetPosition());
        //        nodeInstance.transform.SetSiblingIndex(i);

        //        nodeInstance.SetNode(node.NodeType);

        //        if (node.NodeType is PartNodeSO part)
        //        {
        //            nodeInstance.name = part.openPart.ToString();
        //        }
        //        else if (node.NodeType is WeaponNodeSO weapon)
        //        {
        //            nodeInstance.name = weapon.weapon.ToString();
        //        }
        //        else if (node.NodeType is StartNodeSO)
        //        {
        //            nodeInstance.name = "StartNode";
        //            nodeInstance.DestroyEdge();
        //        }

        //        DestroyImmediate(node.gameObject);
        //        //Destroy(node.gameObject);
        //    }
        //}
    }

    public bool TryGetNode(NodeSO nodeSO, out Node node)
    {
        if (nodeSO == null)
        {
            node = null;
            return false;
        }

        return nodeDic.TryGetValue(nodeSO, out node);
    }

    public Node GetNode(int id)
    {
        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            if (treeSO.nodes[i].id == id)
            {
                NodeSO nodeSO = treeSO.nodes[i];
                return nodeDic[nodeSO];
            }
        }
        return null;
    }

    public void Save()
    {
        List<Node> parts = new List<Node>();
        List<Node> weapons = new List<Node>();
        treeSO.nodes.ForEach(n =>
        {
            if (n is PartNodeSO)
            {
                if (TryGetNode(n, out Node node))
                {
                    parts.Add(node);
                }
            }

            if (n is WeaponNodeSO)
            {
                if (TryGetNode(n, out Node node))
                {
                    weapons.Add(node);
                }
            }
        });


    }

    public void Load()
    {
        treeSO.nodes.ForEach(n =>
        {

        });
    }
}


[Serializable]
public class TechTreeSave
{
    public List<bool> nodeEnable = new List<bool>();
}
