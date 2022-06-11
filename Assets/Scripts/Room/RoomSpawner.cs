﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Room[] RoomPrefabs;
    public Room StartingRoom;

    private Room[,] spawnedRooms;

    private /*IEnumerator*/void Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartingRoom;

        for (int i = 0; i < 12; i++)
        {
            //// Это вот просто убрать чтобы подземелье генерировалось мгновенно на старте
            //yield return new WaitForSecondsRealtime(2.5f);

            PlaceOneRoom();
        }
    }

    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        // Эту строчку можно заменить на выбор комнаты с учётом её вероятности, вроде как в ChunksPlacer.GetRandomChunk()
        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);

        int limit = 500;
        while (limit-- > 0)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            // или сколько у него соседей, чтобы генерировать более плотные, или наоборот, растянутые данжи
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            //newRoom.RotateRandomly();

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x-5) * 20, (position.y - 5) * 13, 0) ;
                spawnedRooms[position.x, position.y] = newRoom;
                Debug.Log($"NEW ROOM ({position.x},{position.y})");
                return;
            }
        }

        Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorUp != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorDown != null) neighbours.Add(Vector2Int.up);
        if (room.DoorDown != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorUp != null) neighbours.Add(Vector2Int.down);
        if (room.DoorRight != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorLeft != null) neighbours.Add(Vector2Int.right);
        if (room.DoorLeft != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorRight != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        Debug.Log($"ROOMS: {room} {selectedRoom}");
        Debug.Log($"SELECTED DIRECTION {selectedDirection}"); 
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