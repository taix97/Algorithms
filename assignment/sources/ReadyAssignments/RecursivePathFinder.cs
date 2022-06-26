using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

class RecursivePathFinder : PathFinder
{

    public RecursivePathFinder(NodeGraph pGraph) : base(pGraph) { }

    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        return FindPath(new List<Node>(), new List<Node>(), pFrom, pTo);
    }

    protected List<Node> FindPath(List<Node> visited, List<Node> shortestPath, Node pFrom, Node pTo)
    {
        //Add the starting node to visited list
        visited.Add(pFrom);

        //Chech if every node is visited
        if (pFrom == pTo)
        {
            if (shortestPath.Count == 0 || visited.Count < shortestPath.Count)
            {
                shortestPath = new List<Node>(visited);
            }
            //returns the shortest path
            return shortestPath;
        }

        //Chech every connection and visit every node
        foreach (Node connection in pFrom.connections)
        {
            if (!visited.Contains(connection))
            {
                //Recursively check every node
                shortestPath = FindPath(new List<Node>(visited), shortestPath, connection, pTo);
            }
        }
        return shortestPath;
    }
}