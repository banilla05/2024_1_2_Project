using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;               //아이템 감지 범위
    private Vector3 lastPosition;                  //플레이어의 마지막 위치 저장
    private float moveThreshold = 0.1f;            //이동감지 임계값
    private ConstructibleBuilding currentNearbyBuilding; //현재 가까이에 있는 건설가능한 건물
    private BuildingCrafter currentBuildingCrafter;      //추가 : 현재 건물의 제작 시스템

    void Start()
    {
        lastPosition = transform.position;        //시작 시 현재 위치를 마지막 위리로 설정
        CheckForBuilding();                          //초기 아이템 체크 수행
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어가 일정거리 이상 이동했는지 체크
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForBuilding();                      //이동시 아이템 체크
            lastPosition = transform.position;    //현재 위치를 마지막 위치로 업데이트
        }

        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstrcted)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());  //PlayerInventory를 참조하여 아이템 수집
            }
            else if(currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName}의 제작 메뉴 열기");
                CraftingUIManager.instance?.ShowUI(currentBuildingCrafter);               //싱글톤으로 접근해서 UI표시를 한다.
            }
        }
    }

    private void CheckForBuilding()
    {
        //감지 범위 내의 모든 콜라이더를 찾음
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;  //가장 가까운 거리의 초기값
        ConstructibleBuilding closestBuilding = null;      //가장 가까운 아이템 초기값
        BuildingCrafter closestCrafter = null;   //추가

        //각 콜라이더를 검사하여 수집 가능한 아이템을 찾음
        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();  //아이템을 감지
            if (building != null)  //모든 건물감지로 변경
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);  //거리계산
                if (distance < closestDistance)  //더 가까운 아이템을 발견 시 업데이트
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closestCrafter = building.GetComponent<BuildingCrafter>();       //여기서 크래프터 가져오기
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding) //가장 가까운 건물이 변경되었을 때만 메세지 표시
        {
            currentNearbyBuilding = closestBuilding;    //가장 가까운건물 업데이트
            currentBuildingCrafter = closestCrafter;    //추가
            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstrcted)
            {
                if (FloatingTextManager.instance != null)
                {
                    FloatingTextManager.instance.Show($"[F]키로 {currentNearbyBuilding.buildingName}건설 (나무 {currentNearbyBuilding.requiredTree}개 필요)"
                        , currentNearbyBuilding.transform.position + Vector3.up);
                }
            }
        }
    }

}
