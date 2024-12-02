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

    private Material buildingMaterial;            //�˹��� ��
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
        if (!canBuild || isConstrcted) return;          //�Ǽ� ����, �Ϸ� ���� üũ�Ͽ� ���Ͻ�Ų��.

        if (inventory.treeCount >= requiredTree)
        {
            inventory.RemoveItem(ItemType.Tree, requiredTree);
            if (FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"{buildingName} �Ǽ�����!", transform.position + Vector3.up);
            }
        }
        else
        {
            if (FloatingTextManager.instance != null)
            {
                FloatingTextManager.instance.Show($"������ �����մϴ�! ({buildingName}) / ({requiredTree})", transform.position + Vector3.up);
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
            FloatingTextManager.instance.Show($"{buildingName} �Ǽ��Ϸ�!", transform.position + Vector3.up);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
