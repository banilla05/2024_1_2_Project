using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Crystal,        //ũ����Ż
    Plant,          //�Ĺ�
    Bush,           //��Ǯ
    Tree,           //����
    VegetableStew,  //��ä ��Ʃ(���ȸ��)
    FruitSalad,     //���� ������ (��� ȸ��)
    RepairKit       //����ŰƮ (���ֺ�����)
}

public class ItemDetector : MonoBehaviour
{
    public float checkRadius = 3.0f;               //������ ���� ����
    private Vector3 lastPosition;                  //�÷��̾��� ������ ��ġ ����(�÷��̾ �̵��� ���� ��� �ֺ��� �����ؼ� ������ ȹ��)
    private float moveThreshold = 0.1f;            //�̵����� �Ӱ谪(�÷��̾ �̵��ؾ� �� �ּҰŸ�)
    private CollectibleItem currentNearbyItem;     //���� ���� �����̿� �ִ� ���������� ������

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;        //���� �� ���� ��ġ�� ������ ������ ����
        CheckForItems();                          //�ʱ� ������ üũ ����
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ �����Ÿ� �̻� �̵��ߴ��� üũ
        if (Vector3.Distance(lastPosition, transform.position) > moveThreshold)
        {
            CheckForItems();                      //�̵��� ������ üũ
            lastPosition = transform.position;    //���� ��ġ�� ������ ��ġ�� ������Ʈ
        }

        if (currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());   //PlayerInventory�� �����Ͽ� ������ ����
        }
    }

    public void CheckForItems()
    {
        //���� ���� ���� ��� �ݶ��̴��� ã��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;  //���� ����� �Ÿ��� �ʱⰪ
        CollectibleItem closestItem = null;      //���� ����� ������ �ʱⰪ

        //�� �ݶ��̴��� �˻��Ͽ� ���� ������ �������� ã��
        foreach (Collider collider in hitColliders)
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();  //�������� ����
            if (item != null && item.canCollect)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);  //�Ÿ����
                if (distance < closestDistance)  //�� ����� �������� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }
        if (closestItem != currentNearbyItem)
        {
            currentNearbyItem = closestItem;
            if (currentNearbyItem != null)
            {
                Debug.Log($"[E]Ű�� ���� {currentNearbyItem.itemName} ����");
            }
        }
    }

    //���������� �ð������� ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;                               //���� ���� ���� ����
        Gizmos.DrawWireSphere(transform.position, checkRadius);  //���� ������ ��ü�� ǥ��
    }
}