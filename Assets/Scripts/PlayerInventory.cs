using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats SurvivalStats;          //클래스 선언

    //각각의 아이템 개수를 저장하는 변수
    public int crystalCount = 0;
    public int plantCount = 0;
    public int bushCount = 0;
    public int treeCount = 0;

    public int vegetableStewCount = 0;          //야채 스튜 개수
    public int fruitSaledCount = 0;             //과일 샐러드 개수
    public int repairKitCount = 0;              //수리 키트 개수

    public void Start()
    {
        SurvivalStats = GetComponent<SurvivalStats>();
    }
    public void Use(ItemType itemType)
    {
        if (GetItemCount(itemType) < 0)//해당 아이템이 있는지 확인
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

    //여러 아이템을 한꺼번에 획득

    public void AddItem(ItemType itemType, int amount)
    {
        //amount 만큼 여러번 addItem 호출
        for (int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }

    //아이템을 추가하는 함수, 아이템 종류에 따라 해당 아이템의 개수를 증가시킴
    public void AddItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;   //현재개수증가
                Debug.Log($"크리스탈 획득! 현재 개수 : {crystalCount}");    //현재개수출력
                break;

            case ItemType.Plant:
                plantCount++;
                Debug.Log($"크리스탈 획득! 현재 개수 : {plantCount}");
                break;

            case ItemType.Bush:
                bushCount++;
                Debug.Log($"크리스탈 획득! 현재 개수 : {bushCount}");
                break;

            case ItemType.Tree:
                treeCount++;
                Debug.Log($"크리스탈 획득! 현재 개수 : {treeCount}");
                break;

            case ItemType.VegetableStew:
                vegetableStewCount++;
                Debug.Log($"야채 스튜 획득! 현재 개수 :{vegetableStewCount}");        //현재 야채 스튜 개수 출력
                break;
            case ItemType.FruitSalad:
                fruitSaledCount++;
                Debug.Log($"과일 샐러드 획득! 현재 개수 :{fruitSaledCount}");        //현재 야채 스튜 개수 출력
                break;
            case ItemType.RepairKit:
                repairKitCount++;
                Debug.Log($"수리키트 획득! 현재 개수 :{repairKitCount}");        //현재 야채 스튜 개수 출력
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
                    Debug.Log($"크리스탈 {amount} 사용! 현재 개수 : {crystalCount}");    //현재개수출력
                    return true;
                }
                break;

            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount -= amount;
                    Debug.Log($"식물 {amount} 사용! 현재 개수 : {plantCount}");
                    return true;
                }
                break;

            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;
                    Debug.Log($"크리스탈 {amount} 사용!! 현재 개수 : {bushCount}");
                    return true;
                }
                break;

            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;
                    Debug.Log($"크리스탈 {amount} 사용!! 현재 개수 : {treeCount}");
                    return true;
                }
                break;

            case ItemType.VegetableStew:
                if (vegetableStewCount >= amount)
                {
                    vegetableStewCount -= amount;
                    Debug.Log($"야채 스튜 {amount} 사용!! 현재 개수 : {vegetableStewCount}");
                    return true;
                }
                break;

            case ItemType.FruitSalad:
                if (fruitSaledCount >= amount)
                {
                    fruitSaledCount -= amount;
                    Debug.Log($"과일 샐러드 {amount} 사용!! 현재 개수 : {fruitSaledCount}");
                    return true;
                }
                break;

            case ItemType.RepairKit:
                if (repairKitCount >= amount)
                {
                    repairKitCount -= amount;
                    Debug.Log($"수리 키트 {amount} 사용!! 현재 개수 : {treeCount}");
                    return true;
                }
                break;
        }
        Debug.Log($"{itemType} 아이템이 부족합니다");
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

            case ItemType.VegetableStew:
                return vegetableStewCount;
            case ItemType.FruitSalad:
                return fruitSaledCount;
            case ItemType.RepairKit:
                return repairKitCount;
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
        Debug.Log("====인벤토리====");
        Debug.Log($"크리스탈 + {crystalCount}개");
        Debug.Log($"식물 + {plantCount}개");
        Debug.Log($"수풀 + {bushCount}개");
        Debug.Log($"나무 + {treeCount}개");
        Debug.Log("==================");

    }
}
