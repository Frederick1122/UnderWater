using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{

    #region Singleton

    public static DungeonGeneration Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    #endregion
    [SerializeField] private Room[] _roomPrefabs;
    [SerializeField] private Room _startingRoom;

    private Room[,] _spawnedRooms;

    [SerializeField] private DungeonConfig _dungeonConfig;
    private void Start()
    {
        StartCoroutine(StartingCoroutine());
    }

    IEnumerator StartingCoroutine()
    {
        _spawnedRooms = new Room[11, 11];
        _spawnedRooms[5, 5] = _startingRoom;
        yield return _dungeonConfig.UpdateTotalQuantityRoom();
        RoomType newRoomType = _dungeonConfig.CanCreateRoom();

        while (newRoomType != RoomType.None)
        {
            //// Это вот просто убрать чтобы подземелье генерировалось мгновенно на старте
            //yield return new WaitForSecondsRealtime(2.5f);

            yield return PlaceOneRoom(newRoomType);
            newRoomType = _dungeonConfig.CanCreateRoom();
        }
    }


    private IEnumerator PlaceOneRoom(RoomType roomType)
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        for (int x = 0; x < _spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < _spawnedRooms.GetLength(1); y++)
            {
                if (_spawnedRooms[x, y] == null) continue;

                int maxX = _spawnedRooms.GetLength(0) - 1;
                int maxY = _spawnedRooms.GetLength(1) - 1;

                if (x > 0 && _spawnedRooms[x - 1, y] == null)
                {
                    vacantPlaces.Add(new Vector2Int(x - 1, y));
                }
                if (y > 0 && _spawnedRooms[x, y - 1] == null)
                {
                    vacantPlaces.Add(new Vector2Int(x, y - 1));
                }
                if (x < maxX && _spawnedRooms[x + 1, y] == null)
                {
                    vacantPlaces.Add(new Vector2Int(x + 1, y));
                }
                if (y < maxY && _spawnedRooms[x, y + 1] == null)
                {
                    vacantPlaces.Add(new Vector2Int(x, y + 1));
                }
            }
        }

        Room newRoom = _roomPrefabs[UnityEngine.Random.Range(0, _roomPrefabs.Length)];

        while (!newRoom.IsThereTypeInList(roomType))
        {
            newRoom = _roomPrefabs[UnityEngine.Random.Range(0, _roomPrefabs.Length)];
        }

        newRoom = Instantiate(newRoom);
        int limit = 100;
        bool _roomWasMade = false;

        while (limit-- > 0 & !_roomWasMade)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            // или сколько у него соседей, чтобы генерировать более плотные, или наоборот, растянутые данжи
            Vector2Int position = vacantPlaces.ElementAt(UnityEngine.Random.Range(0, vacantPlaces.Count));
            //newRoom.RotateRandomly();
            if (ConnectToSomething(newRoom, position))
            {
                _roomWasMade = true;
                newRoom.transform.position = new Vector3((position.x - 5) * 20, (position.y - 5) * 13, 0);
                _spawnedRooms[position.x, position.y] = newRoom;
                newRoom.StartPreset(roomType);
                yield return null;
            }
        }

        if (!_roomWasMade)
            Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = _spawnedRooms.GetLength(0) - 1;
        int maxY = _spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorUp != null && p.y < maxY && _spawnedRooms[p.x, p.y + 1]?.DoorDown != null) neighbours.Add(Vector2Int.up);
        if (room.DoorDown != null && p.y > 0 && _spawnedRooms[p.x, p.y - 1]?.DoorUp != null) neighbours.Add(Vector2Int.down);
        if (room.DoorRight != null && p.x < maxX && _spawnedRooms[p.x + 1, p.y]?.DoorLeft != null) neighbours.Add(Vector2Int.right);
        if (room.DoorLeft != null && p.x > 0 && _spawnedRooms[p.x - 1, p.y]?.DoorRight != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
        Room selectedRoom = _spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            room.DoorUp.SetActive(false);
            selectedRoom.DoorDown.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorDown.SetActive(false);
            selectedRoom.DoorUp.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorRight.SetActive(false);
            selectedRoom.DoorLeft.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorLeft.SetActive(false);
            selectedRoom.DoorRight.SetActive(false);
        }

        return true;
    }
}

