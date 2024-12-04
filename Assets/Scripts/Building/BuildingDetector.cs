using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;                              //������ ���� ����
    private Vector3 lastPosition;                                 //�÷��̾��� ������ ��ġ ����
    private float moveThreshold = 0.1f;                           //�̵����� �Ӱ谪
    private ConstructibleBuilding currentNearbyBuilding;          //���� �����̿� �ִ� �Ǽ������� �ǹ�
    private BuildingCrafter currentBuildingCrafter;               //�߰� : ���� �ǹ��� ���� �ý���

    void Start()
    {
        lastPosition = transform.position;                        //���� �� ���� ��ġ�� ������ ������ ����
        CheckForBuilding();                                       //�ʱ� ������ üũ ����
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ �����Ÿ� �̻� �̵��ߴ��� üũ
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForBuilding();                                   //�̵��� ������ üũ
            lastPosition = transform.position;                    //���� ��ġ�� ������ ��ġ�� ������Ʈ
        }

        if (currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstrcted)
            {
                currentNearbyBuilding.StartConstruction(GetComponent<PlayerInventory>());           //PlayerInventory�� �����Ͽ� ������ ����
            }
            else if(currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName}�� ���� �޴� ����");
                CraftingUIManager.instance?.ShowUI(currentBuildingCrafter);                          //�̱������� �����ؼ� UIǥ�ø� �Ѵ�.
            }
        }
    }

    private void CheckForBuilding()
    {
        //���� ���� ���� ��� �ݶ��̴��� ã��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;                          //���� ����� �Ÿ��� �ʱⰪ
        ConstructibleBuilding closestBuilding = null;                    //���� ����� ������ �ʱⰪ
        BuildingCrafter closestCrafter = null;                           //�߰�

        //�� �ݶ��̴��� �˻��Ͽ� ���� ������ �������� ã��
        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();         //�������� ����
            if (building != null)  //��� �ǹ������� ����
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);  //�Ÿ����
                if (distance < closestDistance)                                                      //�� ����� �������� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closestCrafter = building.GetComponent<BuildingCrafter>();                       //���⼭ ũ������ ��������
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)                                                //���� ����� �ǹ��� ����Ǿ��� ���� �޼��� ǥ��
        {
            currentNearbyBuilding = closestBuilding;                                                //���� �����ǹ� ������Ʈ
            currentBuildingCrafter = closestCrafter;                                                //�߰�
            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstrcted)
            {
                if (FloatingTextManager.instance != null)
                {
                    FloatingTextManager.instance.Show($"[F]Ű�� {currentNearbyBuilding.buildingName}�Ǽ� (���� {currentNearbyBuilding.requiredTree}�� �ʿ�)"
                        , currentNearbyBuilding.transform.position + Vector3.up);
                }
            }
        }
    }

}
