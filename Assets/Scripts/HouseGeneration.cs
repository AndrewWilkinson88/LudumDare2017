using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGeneration : MonoBehaviour {

    public List<GameObject> roomPool;
    private List<GameObject> roomExist;
    private List<int[]> potentialRooms;

    static Random rnd = new Random();

    // Use this for initialization
    void Start () {
        roomPool = new List<GameObject>();
        roomExist = new List<GameObject>();
        potentialRooms = new List<int[]>();
        create_house();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void create_house ()
    {
        place_first_room();
        for (int i = 0; i < 10; i++)
        {
            place_next_room();
        }
    }

    void place_first_room()
    {
        /*		From pool_rooms, pick a room with 2 or more exits (ie. North->exists, West->undef, East->exists, South->undef)
        Add object::room1 to array::rooms

        Set room1::coordinates = something, 1,1 maybe?		#place the room somewhere
		Push room1::coordinates to potential_room_coords    #will be removed immediately by update_potential_room_coords
		Call house::update_potential_room_coords(room1)*/

        GameObject room = rnd.Next(List.roomPool);

    }
    
    void place_next_room()
    {
        /* 		Pick a random set of coordinates from potential_room_coords
		roomX = choose a random room from pool_rooms.
		Check that roomX has at least one exit that points to any of the coordinates from existing_rooms::coordinates
			^ if not, then reject and pick a new one
		Add roomX to existing_rooms
		Call update_potential_room_coords(roomX) */

    }

    void update_potentialRooms(GameObject)
    {
        /* 		Remove the current object::room coords from potential_room_coords
		For each exit (NWES):
			if the exit is North:
				Push potential_room_coords, room1 x, room1 y+1 
			if the exit is West:
				Push potential_room_coords, room1 x-1, room1 y 
			if the exit is East:
				Push potential_room_coords, room1 x+1, room1 y 
			if the exit is South:
				Push potential_room_coords, room1 x, room1 y-1 
			^ unless x or y is less than 1 or greater than 4. */
        int[] current_coords = GameObject.coords;
        if (GameObject.exits.North = true)
        {
            push potentialRoom, (current_coords[0], current_coords[1] + 1);
        }
        if (GameObject.exits.West = true)
        {
            push potentialRoom, (current_coords[0] - 1, current_coords[1]);
        }
        if (GameObject.exits.East = true)
        {
            push potentialRoom, (current_coords[0] + 1, current_coords[1]);
        }
        if (GameObject.exits.South = true)
        {
            push potentialRoom, (current_coords[0], current_coords[1] - 1);
        }

    }

    public class Room : HouseGeneration
    {
        string type = new string();
        int[] coords = new int[2] { 0, 0 };
        IDictionary<string, bool> exits = new Dictionary<string, bool>();

        public Room(string type)
        {
            type = type;

        }

        public void add_exit("string")
        {
            if (string = "N")
            {
                exits.Add("North", true);
            }
            else if (string = "W")
            {
                exits.Add("West", true);
            }
            else if (string = "E")
            {
                exits.Add("East", true);
            }
            else if (string = "S")
            {
                exits.Add("South", true);
            }
        }
    }
}

