using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonConfig", menuName = "Dungeon/DungeonConfig")]
public class DungeonConfig : ScriptableObject
{
    [SerializeField] private List<RoomConfig> _roomConfigs;
    private List<RoomConfig> _roomConfigsInstance = new List<RoomConfig>();
    private int _totalQuantityRoom = 0;
    public IEnumerator Initialization()
    {
        _roomConfigsInstance.Clear();
        foreach (RoomConfig roomConfig in _roomConfigs)
        {
            RoomConfig configInstance = new RoomConfig(roomConfig);
            _roomConfigsInstance.Add(configInstance);
        }
        _totalQuantityRoom = 0;
        yield return null;
    }
    public IEnumerator UpdateTotalQuantityRoom()
    {
        //todo: убрать отсюда
        yield return Initialization();

        foreach (RoomConfig roomConfig in _roomConfigsInstance)
        {
            _totalQuantityRoom += roomConfig.GetRoomQuantity();
        }
        Debug.Log($"TotalQuantityRoom {_totalQuantityRoom}");
    }

    public RoomType CanCreateRoom()
    {
        if (_totalQuantityRoom == 0) return RoomType.None;
        _totalQuantityRoom--;
        RoomConfig newRoomConfig = _roomConfigsInstance[UnityEngine.Random.Range(0, _roomConfigsInstance.Count)];
        while (!newRoomConfig.CanCreateRoomType())
        {
            newRoomConfig = _roomConfigsInstance[UnityEngine.Random.Range(1, _roomConfigsInstance.Count)];
        }
       
        return newRoomConfig.GetRoomType();
    }
}
[Serializable]
public class RoomConfig
{
    [SerializeField] private int _roomQuantity;
    [SerializeField] private RoomType _roomType;

    public int GetRoomQuantity()
    {
        return _roomQuantity;
    }
    public RoomType GetRoomType()
    {
        return _roomType;
    }

    public bool CanCreateRoomType()
    {
        if (_roomQuantity == 0) return false;
        _roomQuantity--;
        return true;
    }

    public RoomConfig() { }
    public RoomConfig(int roomQuantity, RoomType roomType)
    {
        _roomQuantity = roomQuantity;
        _roomType = roomType;
    }
    public RoomConfig(RoomConfig roomConfig)
    {
        _roomQuantity = roomConfig.GetRoomQuantity();
        _roomType = roomConfig.GetRoomType();
    }
}

