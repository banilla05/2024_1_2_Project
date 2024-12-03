using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;                        // 건물 감지 범위
    private Vector3 lastPosition;                           // 플레이어의 마지막 위치 저장
    private float moveThreshold = 0.1f;                     // 이동 감지 임계값
    private ConstructibleBuilding currentNearbyBuilding;    // 현재 가까이있는 건설 가능한 건물
    private BuildingCrafter currentBuildingCrafter;

    void Start()
    {
        lastPosition = transform.position;
        CheckForBuilding();
    }

    void Update()
    {
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForBuilding();
            lastPosition = transform.position;
        }

        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstructed)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());
            }
            else if (currentBuildingCrafter != null)
            {
                //Debug.Log($"{currentBuildingCrafter.buildingName}의 제작 메뉴 열기");
                CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);
            }
        }
    }

    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;
        ConstructibleBuilding closestBuilding = null;
        BuildingCrafter closestCrafter = null;

        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();
            if (building != null && building.canBuild && !building.isConstructed)
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closestCrafter = building.GetComponent<BuildingCrafter>();
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)
        {
            currentNearbyBuilding = closestBuilding;
            currentBuildingCrafter = closestCrafter;

            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstructed)
            {
                if (FloatingTextManager.instance != null)
                {
                    FloatingTextManager.instance.Show(
                        $"[F]키로 {currentNearbyBuilding.buildingName} 건설 (나무 {currentNearbyBuilding.requiredTree}개 필요)",
                        currentNearbyBuilding.transform.position + Vector3.up);
                }
            }
        }
    }
}
