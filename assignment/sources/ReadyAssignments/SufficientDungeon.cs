using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;

class SufficientDungeon : Dungeon
{
    List<Room> splitRooms;
    List<Point> crossings = new List<Point>();

    Random randWidth = new Random();
    Random randHeight = new Random();

    public SufficientDungeon(Size pSize) : base(pSize) { }

    protected override void generate(int pMinimumRoomSize)
    {
        splitRooms = new List<Room>();

        splitRooms.Add(new Room(new Rectangle(0, 0, size.Width, size.Height)));

        while (splitRooms.Count > 0)
        {
            Split(splitRooms[0], pMinimumRoomSize);
        }

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            for (int j = i + 1; j < rooms.Count; j++)
            {
                AddDoors(rooms[i], rooms[j]);
                //graphics.Clear(Color.White);
                //drawRooms(rooms, Pens.Black);
                //drawRoom(rooms[i], Pens.Green);
                //drawRoom(rooms[j], Pens.Red);
                //drawDoors(doors, Pens.Blue);
                //Console.ReadKey();
            }

            //for (int j = 0; j < rooms.Count-1; j++)
            //{
            //    AddDoors(rooms[j+1], rooms[i]);
            //}

        }
        Console.WriteLine("The amouth of doors are " + doors.Count);
    }

    void Split(Room room, int minRoomSize)
    {
        if (room.area.Width >= minRoomSize * 2)
        {
            int rWidth = randWidth.Next(minRoomSize + 1, room.area.Width - minRoomSize + 1);
            splitRooms.Add(new Room(new Rectangle(room.area.X, room.area.Y, rWidth + 1, room.area.Height)));
            splitRooms.Add(new Room(new Rectangle(room.area.X + rWidth, room.area.Y, room.area.Width - rWidth, room.area.Height)));
            splitRooms.Remove(room);
        }
        else if (room.area.Height >= minRoomSize * 2)
        {
            int rHeight = randHeight.Next(minRoomSize + 1, room.area.Height - minRoomSize + 1);
            splitRooms.Add(new Room(new Rectangle(room.area.X, room.area.Y, room.area.Width, rHeight + 1)));
            splitRooms.Add(new Room(new Rectangle(room.area.X, room.area.Y + rHeight, room.area.Width, room.area.Height - rHeight)));
            splitRooms.Remove(room);
        }
        else
        {
            rooms.Add(room);

            crossings.Add(new Point(room.area.X, room.area.Y));
            crossings.Add(new Point(room.area.X + room.area.Width, room.area.Y + room.area.Height - 1));
            crossings.Add(new Point(room.area.X + room.area.Width - 1, room.area.Y));


            splitRooms.Remove(room);
        }
    }

    public void AddDoors(Room roomA, Room roomB)
    {
        Point rightBottomA = new Point(roomA.area.Right, roomA.area.Y + roomA.area.Height);
        Point rightBottomB = new Point(roomB.area.Right, roomB.area.Y + roomB.area.Height);

        if (roomA.area.IntersectsWith(roomB.area))
        {
            List<int> patica = new List<int>();
            for (int i = 0; i < roomA.area.Height; i++)
            {
                

                if (!crossings.Contains(new Point(roomA.area.Left, roomA.area.Top + i)) &&
                    !crossings.Contains(new Point(roomA.area.Right, roomA.area.Top + i)))
                {
                    patica.Add(roomA.area.Top + i);
                    //doors.Add(new Door(new Point(roomA.area.Left, roomA.area.Top + i), roomA, roomB));
                }
            }
            bool ready = false;
            int r = 0;
            while (ready == false)
            {
                r = Utils.Random(roomA.area.Top + 1, roomA.area.Bottom - 1);
                if (patica.Contains(r))
                {
                    ready = true;
                }
            }


            ////If B is below A and also If B is below A but also on the right
            ////Console.WriteLine("rightBottomA.Y = " + rightBottomA.Y + "roomB.Y = " + roomB.area.Y);
            if (roomA.area.X <= roomB.area.X && roomB.area.Y > roomA.area.Y && roomA.doorBottom == false && roomB.doorTop == false && rightBottomA.Y <= roomB.area.Y + 1)
            {
                doors.Add(new Door(new Point(Utils.Random(roomB.area.X + 1, rightBottomA.X - 1), roomB.area.Y), roomA, roomB));
                roomA.doorBottom = true;
                roomB.doorTop = true;
            }


            //Console.WriteLine("rightBottomB.Y = " + rightBottomB.Y + " roomA.Y = " + roomA.area.Y);
            //If B is above A on the Left 
            else if (roomA.area.X > roomB.area.X && rightBottomA.Y >= rightBottomB.Y && roomA.area.Y + 1 != rightBottomB.Y && roomA.doorLeft == false && roomB.doorRight == false)
            {
                doors.Add(new Door(new Point(roomA.area.Left, Utils.Random(roomA.area.Y + 1, rightBottomB.Y - 1)), roomA, roomB));
                roomA.doorLeft = true;
                roomB.doorRight = true;
            }

            //Console.WriteLine("rightBottomA.X = " + rightBottomA.X + " roomB.area.X = " + roomB.area.X);
            // Console.WriteLine("rightBottomA.Y = " + rightBottomA.Y + " rightBottomB.Y = " + rightBottomB.Y);

            //If B is next to A on the right 
            else if (rightBottomA.X == roomB.area.X + 1 && rightBottomA.Y == rightBottomB.Y && roomA.area.Y == roomB.area.Y && roomA.doorRight == false && roomB.doorLeft == false)
            {
                doors.Add(new Door(new Point(roomB.area.Left, Utils.Random(roomA.area.Y + 1, roomB.area.Bottom - 1)), roomA, roomB));
                roomA.doorRight = true;
                roomB.doorLeft = true;
            }

            ////If B is next to A on the left
            else if (rightBottomB.X == roomA.area.X + 1 && rightBottomA.Y == rightBottomB.Y && roomA.area.Y == roomB.area.Y && roomA.doorLeft == false && roomB.doorRight == false)
            {
                doors.Add(new Door(new Point(roomB.area.Right - 1, Utils.Random(roomA.area.Y + 1, roomB.area.Bottom - 1)), roomA, roomB));
                roomA.doorLeft = true;
                roomB.doorRight = true;
            }

            ////If B is Above A on the Right
            ////Console.WriteLine("rightBottomB.Y = " + rightBottomB.Y + "roomA.area.Y  = " + roomA.area.Y);
            //Console.WriteLine("roomB.area.Bottom = " + roomB.area.Bottom + "rightBottomA.Y = " + rightBottomA.Y);
            else if (roomB.area.X > roomA.area.X && rightBottomB.Y - 1 > roomA.area.Y + 1 && rightBottomB.X != roomA.area.X && rightBottomA.Y >= rightBottomB.Y && roomB.area.Bottom != rightBottomA.Y && roomA.doorRight == false && roomB.doorLeft == false)
            {
                doors.Add(new Door(new Point(roomA.area.Right - 1, Utils.Random(roomA.area.Y + 1, rightBottomB.Y - 1)), roomA, roomB));
                roomA.doorRight = true;
                roomB.doorLeft = true;
            }

            ////If B is Above A
            ////Console.WriteLine("RoomA.area.X = " + roomA.area.X + " rightBottomB.X = " + rightBottomB.X);
            else if (roomA.area.Y > roomB.area.Y && roomA.area.Y + 1 >= rightBottomB.Y && roomA.area.X != rightBottomB.X && rightBottomB.X >= rightBottomA.X && roomB.area.X <= roomA.area.X && roomB.doorBottom == false && roomA.doorTop == false)
            {
                doors.Add(new Door(new Point(Utils.Random(roomB.area.X + 1, roomA.area.Right - 1), roomA.area.Y), roomA, roomB));
                roomA.doorTop = true;
                roomB.doorBottom = true;
            }
        }
    }
}
