using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats SurvivalStats;          //Ŭ���� ����

    //������ ������ ������ �����ϴ� ����
    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;

    public void Start()
    {
        SurvivalStats = GetComponent<SurvivalStats>();
    }
    public void Use(ItemType itemType)
    {
        if (GetItemCount(itemType) < 0)//�ش� �������� �ִ��� Ȯ��
        {
            return;
        }

        switch (itemType)
        {
            case ItemType.VegetableStew:
                RemoveItem(ItemType.VegetableStew, 1);
                SurvivalStats.EatFood(RecipeList.KitchenRecipes[0].hungerRestoreAmount);
                break;
            case ItemType.FruitSalad:
                RemoveItem(ItemType.FruitSalad, 1);
                SurvivalStats.EatFood(RecipeList.KitchenRecipes[1].hungerRestoreAmount);
                break;
            case ItemType.RepairKit:
                RemoveItem(ItemType.RepairKit, 1);
                SurvivalStats.RepairSuit(RecipeList.KitchenRecipes[2].repairAmount);
                break;
        }
    }

    //���� �������� �Ѳ����� ȹ��

    public void AddItem(ItemType itemType, int amount)
    {
        //amount ��ŭ ������ addItem ȣ��
        for (int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }

    //�������� �߰��ϴ� �Լ�, ������ ������ ���� �ش� �������� ������ ������Ŵ
    public void AddItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;   //���簳������
                Debug.Log($"ũ����Ż ȹ��! ���� ���� : {crystalCount}");    //���簳�����
                break;

            case ItemType.Plant:
                plantCount++;
                Debug.Log($"ũ����Ż ȹ��! ���� ���� : {plantCount}");
                break;

            case ItemType.Bush:
                bushCount++;
                Debug.Log($"ũ����Ż ȹ��! ���� ���� : {bushCount}");
                break;

            case ItemType.Tree:
                treeCount++;
                Debug.Log($"ũ����Ż ȹ��! ���� ���� : {treeCount}");
                break;
        }
    }
    
    public bool RemoveItem(ItemType itemType, int amount = 1)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)
                {
                    crystalCount -= amount;
                    Debug.Log($"ũ����Ż {amount} ���! ���� ���� : {crystalCount}");    //���簳�����
                    return true;
                }
                break;

            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount -= amount;
                    Debug.Log($"�Ĺ� {amount} ���! ���� ���� : {plantCount}");
                    return true;
                }
                break;

            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;
                    Debug.Log($"ũ����Ż {amount} ���!! ���� ���� : {bushCount}");
                    return true;
                }
                break;

            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;
                    Debug.Log($"ũ����Ż {amount} ���!! ���� ���� : {treeCount}");
                    return true;
                }
                break;
        }
        Debug.Log($"{itemType} �������� �����մϴ�");
        return false;
    }

    public int GetItemCount(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                return bushCount;
            case ItemType.Tree:
                return treeCount;
            default:
                return 0;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("====�κ��丮====");
        Debug.Log($"ũ����Ż + {crystalCount}��");
        Debug.Log($"�Ĺ� + {plantCount}��");
        Debug.Log($"��Ǯ + {bushCount}��");
        Debug.Log($"���� + {treeCount}��");
        Debug.Log("==================");

    }
}
