using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    CraftingTable,
    Furnace,
    Kitchen,
    Storage
}

[System.Serializable]

public class CraftingRecipe
{
    public string itemName;
    public ItemType resultItem;         //결과물
    public int resultAmount = 1;        //결과물 개수

    public float hungerRestoreAmount;   //허기회복량 (음식일 경우)
    public float repairAmount;          //수리량(수리 키트일 경우)

    public ItemType[] requiredItems;
    public int[] requiredAmounts;        //필요한 재료
}