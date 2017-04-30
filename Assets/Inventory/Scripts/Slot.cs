using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    public SlotInfo slotInfo;

    public void SetUp(int id)
    {
        slotInfo = new SlotInfo();
        slotInfo.id = id;
        slotInfo.EmptySlot();
    }
	
}
[System.Serializable]
public class SlotInfo
{
    public int id;
    public bool isEmpty;
    public int itemId;
    public int amount;
    public int maxAmount = 10;

    public void EmptySlot()
    {
        isEmpty = true;
        amount = 0;
        itemId = -1;
    }
}
