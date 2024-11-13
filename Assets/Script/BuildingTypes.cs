using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    CraftingTable,          //���۴�
    Furnace,                //�뱤��
    Kitchen,                //�ֹ�
    Storage                 //â��
}

[System.Serializable]
public class CraftingRecipe
{
    public string itemName;             //������ ������ �̸�
    public ItemType resultItem;         //�����
    public int resultAmunt = 1;         //����� ����
    public ItemType[] requiresItem;     //�ʿ��� ����
    public int[] requiresAmounts;       //�ǿ��� ��� ����
}
