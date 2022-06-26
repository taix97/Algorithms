using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

class BreadthFirstPathFinder : PathFinder
{
    List<Node> toDo = new List<Node>();
    List<Node> visited = new List<Node>();

    List<Node> path = new List<Node>();

    Node currentNode = null;

    public BreadthFirstPathFinder(NodeGraph pNode) : base(pNode) { }

    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        toDo.Clear();
        visited.Clear();
        path.Clear();

        toDo.Add(pFrom);

        while (true)
        {
            //Set the current node to pFrom and make it visited
            currentNode = toDo[0];
            toDo.RemoveAt(0);
            visited.Add(currentNode);

            //Check if 
            if (currentNode == pTo)
            {
                path.Add(currentNode);

                while (currentNode != pFrom)
                {
                    path.Add(currentNode.parentNode);
                    currentNode = currentNode.parentNode;
                }

                path.Reverse();
                return path;
            }
            else
            {
                foreach (Node node in currentNode.connections)
                {
                    if (toDo.Contains(node) || visited.Contains(node))
                    {
                        continue;
                    }
                    else
                    {
                        //Make currentNode a parent and add it to toDo
                        node.parentNode = currentNode;
                        toDo.Add(node);
                    }
                }
            }
        }
    }
}
