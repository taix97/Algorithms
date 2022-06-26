﻿using System.Drawing;

/**
 * This class represents (the data for) a Room, at this moment only a rectangle in the dungeon.
 */
class Room
{
	public Rectangle area;

	public bool doorRight = false;
	public bool doorTop = false;
	public bool doorLeft = false;
	public bool doorBottom = false;
	public Node node;

	public Room (Rectangle pArea)
	{
		area = pArea;
	}

	//TODO: Implement a toString method for debugging?
	//Return information about the type of object and it's data
	//eg Room: (x, y, width, height)

}
