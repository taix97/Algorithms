using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

class OnGraphWayPointAgent : NodeGraphAgent
{
    private Node _target = null;
    private Node currentNode = null;
    List<Node> nodesClicked = new List<Node>();

    public OnGraphWayPointAgent(NodeGraph pNodeGraph) : base(pNodeGraph)
    {
        // Position Morc on a random node
        if (pNodeGraph.nodes.Count > 0)
        {
            currentNode = pNodeGraph.nodes[Utils.Random(0, pNodeGraph.nodes.Count)];
            nodesClicked.Add(currentNode);
            jumpToNode(currentNode);
        }
        pNodeGraph.OnNodeLeftClicked += onClickTarget;
    }

    protected virtual void onClickTarget(Node targetNode)
    {
        if (queue.Count < 5)
        {
            queue.Enqueue(targetNode);
            Console.WriteLine(queue.Count);
        }
    }

    protected override void Update()
    {
        if (!isMoving && queue.Count > 0)
        {
            _target = queue.Dequeue();
        }

        // No target? Stop moving.
        if (_target == null) return;

        //Move towards the target node, if we reached it, clear the target, if not then keep going
        if (moveTowardsNode(_target))
        {
            currentNode = _target;
            _target = null;
        }
    }
}