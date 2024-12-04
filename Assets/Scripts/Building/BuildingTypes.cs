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
    public ItemType resultItem;                         //�����
    public int resultAmount = 1;                        //����� ����

    public float hungerRestoreAmount;                   //���ȸ���� (������ ���)
    public float repairAmount;                          //������(���� ŰƮ�� ���)

    public ItemType[] requiredItems;
    public int[] requiredAmounts;                       //�ʿ��� ���
}