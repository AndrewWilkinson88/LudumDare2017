using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGeneration : MonoBehaviour
{
    public List<Room> roomPool;

    private Room[,] grid;

    private List<Room> roomExist;
    private List<int[]> potentialRooms;

    //static Random rnd = new Random();

    // Use this for initialization
    void Start()
    {
        grid = new Room[4,4];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i,j] = null;
            }
        }

        roomPool = new List<Room>();
        roomExist = new List<Room>();
        potentialRooms = new List<int[]>();

        create_house();
    }

    void create_house()
    {
        place_first_room();
        for (int i = 0; i < 10; i++)
        {
            //If the potential rooms is ever 0, exit
            if( potentialRooms.Count == 0)
            {
                break;
            }

            place_next_room();
        }
    }

    void place_first_room()
    {
        /*		
        From pool_rooms, pick a room with 2 or more exits (ie. North->exists, West->undef, East->exists, South->undef)
        Add object::room1 to array::rooms

        Set room1::coordinates = something, 1,1 maybe?		#place the room somewhere
		Push room1::coordinates to potential_room_coords    #will be removed immediately by update_potential_room_coords
		Call house::update_potential_room_coords(room1)
        */

        bool valid = false;
        Room room = null;

        //Find a room with more than one exit
        while (!valid)
        {
            room = this.roomPool[Random.Range(0, this.roomPool.Count)];
            int count = 0;

            foreach(string exitName in room.exits.Keys)
            {
                if ( room.exits[exitName] )
                {
                    count++;
                }
            }

            if (count >= 2)
            {
                valid = true;
            }
        }

        //Place the room in the bottom-right corner of the grid
        grid[1,1] = room;
        update_potentialRooms(room, new int[2] { 1, 1 });
    }

    void place_next_room()
    {
        /* 		Pick a random set of coordinates from potential_room_coords
		roomX = choose a random room from pool_rooms.
		Check that roomX has at least one exit that points to any of the coordinates from existing_rooms::coordinates
			^ if not, then reject and pick a new one
		Add roomX to existing_rooms
		Call update_potential_room_coords(roomX) */
        bool valid = false;
        int[] potential_coords = potentialRooms[Random.Range(0, potentialRooms.Count)];
        Room temp_room = null;
        while (!valid)
        {
            temp_room = roomPool[Random.Range(0, roomPool.Count)];
            int[] exitCoords = new int[2];
             
            foreach (string exitName in temp_room.exits.Keys)
            {
                if (temp_room.exits[exitName] == true)
                {
                    if( exitName == "North")
                    {
                        if(potential_coords[1] <= 2 &&
                            grid[potential_coords[0], potential_coords[1] + 1] != null && 
                            grid[potential_coords[0], potential_coords[1] + 1].exits["South"])
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (exitName == "East")
                    {
                        if (potential_coords[0] >= 1 &&
                            grid[potential_coords[0] - 1, potential_coords[1]] != null &&
                            grid[potential_coords[0] - 1, potential_coords[1]].exits["West"])
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (exitName == "West")
                    {
                        if (potential_coords[0] <= 2 &&
                            grid[potential_coords[0] + 1, potential_coords[1]] != null &&
                            grid[potential_coords[0] + 1, potential_coords[1]].exits["East"])
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (exitName == "South")
                    {
                        if (potential_coords[1] >= 1 &&
                            grid[potential_coords[0], potential_coords[1] - 1] != null &&
                            grid[potential_coords[0], potential_coords[1] - 1].exits["North"])
                        {
                            valid = true;
                            break;
                        }
                    }
                }
            }

            grid[potential_coords[0], potential_coords[1]] = temp_room;
            update_potentialRooms(temp_room, potential_coords);
        }
    }

    void update_potentialRooms(Room newRoom, int[] current_coords)
    {
        /* 		
        Remove the current object::room coords from potential_room_coords
        For each exit (NWES):
            if the exit is North:
                Push potential_room_coords, room1 x, room1 y+1 
            if the exit is West:
                Push potential_room_coords, room1 x-1, room1 y 
            if the exit is East:
                Push potential_room_coords, room1 x+1, room1 y 
            if the exit is South:
                Push potential_room_coords, room1 x, room1 y-1 
            ^ unless x or y is less than 1 or greater than 4. 
        */

        //Removed potential room coordinats from room
        int coordsIndex = 0;
        for( int i = 0; i < potentialRooms.Count; i++)
        {
            if(potentialRooms[i][0] == current_coords[0] && potentialRooms[i][1] == current_coords[1])
            {
                coordsIndex = i;
                break;
            }
        }
        potentialRooms.RemoveAt(coordsIndex);

        //Add New potential rooms
        int[] newCoords = new int[2];
        if (newRoom.exits["North"] && current_coords[1] <= 2)
        {
            newCoords[0] = current_coords[0];
            newCoords[1] = current_coords[1] + 1;
            potentialRooms.Add(newCoords);
        }
        if (newRoom.exits["West"] && current_coords[0] >= 1)
        {
            newCoords[0] = current_coords[0] - 1;
            newCoords[1] = current_coords[1];
            potentialRooms.Add(newCoords);
        }
        if (newRoom.exits["East"] && current_coords[0] <= 2)
        {
            newCoords[0] = current_coords[0] + 1 ;
            newCoords[1] = current_coords[1];
            potentialRooms.Add(newCoords);
        }
        if (newRoom.exits["South"] && current_coords[1] >= 1)
        {
            newCoords[0] = current_coords[0];
            newCoords[1] = current_coords[1] - 1;
            potentialRooms.Add(newCoords);
        }
    }
}

public class Room : MonoBehaviour
{
    public Dictionary<string, bool> exits = new Dictionary<string, bool>();
}
