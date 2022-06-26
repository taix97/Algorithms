using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

class PathFindingAgent : OnGraphWayPointAgent
{

    private Node _target = null;
    private Node currentNode = null;
    PathFinder pathFinder;
    private List<Node> nodesToFollow = new List<Node> ();

    public PathFindingAgent(NodeGraph pNodeGraph, PathFinder pPathFinder)  : base(pNodeGraph)
    {
        pathFinder = pPathFinder;

        if(pNodeGraph.nodes.Count > 0)
        {
            currentNode = pNodeGraph.nodes[Utils.Random(0,pNodeGraph.nodes.Count)];
        }
        pNodeGraph.OnNodeLeftClicked += onClickTarget;
    }

    protected override void onClickTarget(Node targetNode)
    {
        nodesToFollow = pathFinder.Generate(currentNode, targetNode);
    }

    protected override void Update()
    {
        if(nodesToFollow.Count > 0)
        {
            currentNode = nodesToFollow[0];
            _target = nodesToFollow[0];
        }

        if(_target == null)
        {
            return;
        }

        if(moveTowardsNode(_target) && nodesToFollow.Count() != 0)
        {
            nodesToFollow.RemoveAt(0);
        }
    }
}