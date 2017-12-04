using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    private struct OpenConnection
    {
        public int x;
        public int z;
        public Direction from;
        public RoomController room;

        public OpenConnection(int x, int z, Direction from, RoomController room)
        {
            this.x = x;
            this.z = z;
            this.from = from;
            this.room = room;
        }
    }

    public int roomCountGoal = 10;
    
    public List<RoomController> rooms;
    public GameObject buildingRoot;

    private List<RoomController> connectsAll = new List<RoomController>();
    private List<RoomController> connectsfromNorth = new List<RoomController>();
    private List<RoomController> connectsfromSouth = new List<RoomController>();
    private List<RoomController> connectsfromEast = new List<RoomController>();
    private List<RoomController> connectsfromWest = new List<RoomController>();

    private Dictionary<string, RoomController> usedSpaces = new Dictionary<string, RoomController>();

    private List<OpenConnection> openConnections = new List<OpenConnection>();

    private float roomSizeZ = 7f;
    private float roomSizeX = 8f;

    // Use this for initialization
    void Start () {

        foreach (RoomController r in rooms)
        {
            if (r.canNorth)
                connectsfromSouth.Add(r);
            if (r.canSouth)
                connectsfromNorth.Add(r);
            if (r.canEast)
                connectsfromWest.Add(r);
            if (r.canWest)
                connectsfromEast.Add(r);
            if (r.canNorth && r.canSouth && r.canEast && r.canWest)
                connectsAll.Add(r);
        }

        buildingRoot = new GameObject("NewBuilding");
        GenerateRooms();

        //Updates the total number of items for the scoreManager
        int totalItems = buildingRoot.GetComponentsInChildren<DraggableObject>(true).Length;
        ScoreManager.instance.SetTotalItems(totalItems);
    }

    void GenerateRooms()
    {
        //We start by picking and instantiating a room that can connect in all directions
        int i = Random.Range(0, connectsAll.Count);
        RoomController firstRoom = GameObject.Instantiate<RoomController>(connectsAll[i]);
        firstRoom.gameObject.name = "0,0";
        firstRoom.transform.position = new Vector3(0, 0, 0);
        firstRoom.transform.parent = buildingRoot.transform;
        roomCountGoal--;
        usedSpaces.Add("0,0", firstRoom);
        openConnections.Add(new OpenConnection(1, 0, Direction.EAST, firstRoom));
        openConnections.Add(new OpenConnection(-1, 0, Direction.WEST, firstRoom));
        openConnections.Add(new OpenConnection(0, 1, Direction.SOUTH, firstRoom));
        openConnections.Add(new OpenConnection(0, -1, Direction.NORTH, firstRoom));

        while (roomCountGoal > 0 && openConnections.Count > 0)
        {
            AttemptGenerateRoom();
        }
    }

    void AttemptGenerateRoom()
    {
        OpenConnection o = openConnections[Random.Range(0, openConnections.Count)];
        //No matter what, once we consider a connection we remove it
        openConnections.Remove(o);
        if( !usedSpaces.ContainsKey(o.x+","+ o.z))
        {
            List<RoomController> possibleRoomList;
            switch (o.from)
            {
                case Direction.NORTH:
                    possibleRoomList = connectsfromNorth;
                    break;
                case Direction.SOUTH:
                    possibleRoomList = connectsfromSouth;
                    break;
                case Direction.EAST:
                    possibleRoomList = connectsfromEast;
                    break;
                case Direction.WEST:
                default:
                    possibleRoomList = connectsfromWest;
                    break;
            }

            RoomController newRoom = GameObject.Instantiate<RoomController>( possibleRoomList[Random.Range(0,possibleRoomList.Count)]);
            newRoom.transform.parent = buildingRoot.transform;
            newRoom.gameObject.name = o.x + "," + o.z;
            roomCountGoal--;
            newRoom.transform.position = new Vector3(o.x* roomSizeX, 0, o.z* roomSizeZ);
            usedSpaces.Add(o.x + "," + o.z, newRoom);

            OpenWall(newRoom, o);

            if (newRoom.canNorth && o.from != Direction.SOUTH && !usedSpaces.ContainsKey(o.x+","+ (o.z+1)))
            {
                openConnections.Add(new OpenConnection(o.x, o.z + 1, Direction.SOUTH, newRoom));
            }
            if (newRoom.canSouth && o.from != Direction.NORTH && !usedSpaces.ContainsKey(o.x + "," + (o.z - 1)))
            {
                openConnections.Add(new OpenConnection(o.x, o.z - 1, Direction.NORTH, newRoom));
            }
            if (newRoom.canEast && o.from != Direction.WEST && !usedSpaces.ContainsKey((o.x+1) + "," + o.z))
            {
                openConnections.Add(new OpenConnection(o.x + 1, o.z, Direction.EAST, newRoom));
            }
            if (newRoom.canWest && o.from != Direction.EAST && !usedSpaces.ContainsKey((o.x - 1) + "," + o.z))
            {
                openConnections.Add(new OpenConnection(o.x - 1, o.z, Direction.WEST, newRoom));
            }
        }
        else
        {
            //Maybe check if a second entrance can be added?
        }
    }

    void OpenWall(RoomController room, OpenConnection o)
    {
        switch(o.from)
        {
            case Direction.NORTH:
                room.NorthDoor.SetActive(true);
                o.room.SouthDoor.SetActive(true);
                room.NorthWall.SetActive(false);
                o.room.SouthWall.SetActive(false);                
                break;
            case Direction.SOUTH:
                room.SouthDoor.SetActive(true);
                o.room.NorthDoor.SetActive(true);
                room.SouthWall.SetActive(false);
                o.room.NorthWall.SetActive(false);
                break;
            case Direction.EAST:
                room.WestWall.SetActive(false);
                o.room.EastWall.SetActive(false);
                break;
            case Direction.WEST:
                room.EastWall.SetActive(false);
                o.room.WestWall.SetActive(false);
                break;
        }

    }
}