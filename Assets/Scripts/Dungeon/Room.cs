using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject DoorUp;
    public GameObject DoorDown;
    public GameObject DoorRight;
    public GameObject DoorLeft;
    [Header("")]
    [Space(5)]
    [SerializeField] private List<RoomPreset> _roomPresets = new List<RoomPreset>();

    public bool IsThereTypeInList(RoomType roomType)
    {
        foreach(RoomPreset roomPreset in _roomPresets)
        {
            if(roomPreset.roomType == roomType)
            {
                return true;
            }
        }
        return false;
    }
    public void StartPreset(RoomType roomType)
    {
        foreach (RoomPreset roomPreset in _roomPresets)
        {
            if (roomPreset.roomType == roomType) {
                roomPreset.StartPreset();
                return;
            }
        }
    }

    [Serializable]
    public class RoomPreset
    {
        public RoomType roomType;
        [SerializeField] private GameObject _roomPreset;
        public void StartPreset()
        {
            _roomPreset.SetActive(true);
        }
    }
}

public enum RoomType
{
    None,
    Empty,
    Battle,
    Event,
    Trader,
    Chest
}

