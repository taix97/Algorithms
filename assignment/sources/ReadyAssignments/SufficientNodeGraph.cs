
class SufficientNodeGraph : SampleDungeonNodeGraph
{

    public SufficientNodeGraph(Dungeon pDungeon) : base(pDungeon) { }

    protected override void generate()
    {
        // Adding nodes to all the rooms 
        foreach (Room room in _dungeon.rooms)
        {
            Node node = new Node(getRoomCenter(room));
            room.node = node;
            nodes.Add(node);
        }

        // Adding nodes to all the doors and connecting them to the adjacent room nodes
        foreach (Door door in _dungeon.doors)
        {
            Node node = new Node(getDoorCenter(door));
            door.node = node;
            nodes.Add(node);
            AddConnection(node, door.roomA.node);
            AddConnection(node, door.roomB.node);
        }
        System.Console.WriteLine("The amount of nodes are " + nodes.Count);
    }
}