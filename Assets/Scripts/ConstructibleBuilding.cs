using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructibleBuilding : MonoBehaviour
{
    [Header("Building Settings")]
    public BuildingType buildingType;
    public string buildingName;
    public int requiredTree = 5;
    public float constructionTime = 2.0f;

    public bool canBuild = true;
    public bool isConstrcted = false;

    private Material buildingMaterial;            //검물의 매
    void Start()
    {
        buildingMaterial = GetComponent<MeshRenderer>().material;
        //
        Color color = buildingMaterial.color;
        color.a = 0.5f;
        buildingMaterial.color = color;

    }

    public void StartConstruction(PlayerInventory inventory)
    {
        if (!canBuild || isConstrcted) return;          //건설 가능, 완료 변수 체크하여 리턴시킨다.

        if (inventory.treeCount >= requiredTree)
        {
            inventory.RemoveItem(ItemType.Tree, requiredTree);
            if (FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"{buildingName} 건설시작!", transform.position + Vector3.up);
            }
        }
        else
        {
            if (FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"나무가 부족합니다! ({buildingName}) / ({requiredTree})", transform.position + Vector3.up);
            }
        }
    }

    private IEnumerator ConstrutionRoutine()
    {
        canBuild = false;
        float timer = 0;
        Color color = buildingMaterial.color;

        while (timer < constructionTime)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 1f, timer / constructionTime);
            buildingMaterial.color = color;
            yield return null;
        }
        isConstrcted = true;

        if (FloatingTextManager.instance != null)
        {
            FloatingTextManager.instance.Show($"{buildingName} 건설완료!", transform.position + Vector3.up);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
