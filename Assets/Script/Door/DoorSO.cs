using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorSO", menuName = "Scriptable Objects/DoorSO")]
public class DoorSO : ScriptableObject
{
    public List<DoorOrder> doorOpenOrders = new();
    private int currentDoorOrderIndex = 0;
    public void init()
    {
        currentDoorOrderIndex = 0;

    }
    public int[] GetOpenDoors()
    {
        return doorOpenOrders[currentDoorOrderIndex].doorIDs;
    }
    public void NextDoorOrder()
    {
        if (currentDoorOrderIndex < doorOpenOrders.Count - 1)
        {
            currentDoorOrderIndex++;
        }
    }
}
[Serializable]
public class DoorOrder
{
    public int[] doorIDs;

}