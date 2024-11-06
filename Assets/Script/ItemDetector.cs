using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//������ ���� ����
public enum ItemType
{
    Crystal,                    //ũ����Ż
    Plant,                      //�Ĺ�
    Bush,                       //��Ǯ
    Tree,                       //����
}

public class ItemDetector : MonoBehaviour
{

    public float checkRadius = 3.0f;            //������ ���� ����
    private Vector3 lastPosition;               //�÷��̾��� ������ ��ġ ���� (�÷��̾ �̵��� ���� ��� �ֺ��� ���� �ؼ� ������ ȹ��)
    private float moveTreshold = 0.1f;          //�̵� ���� �Ӱ谪 (�÷��̾ �̵��ؾ� �� �ּҰŸ�)
    private CollectibleItem currentNearItem;    //���� ���� ������ �ִ� ���� ������ ������


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;          //���� �� ���� ��ġ�� ������ ��ġ�� ����
        CheckForItems();                            //�ʱ� ������ üũ ����
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ� üũ
        if (Vector3.Distance(lastPosition, transform.position) > moveTreshold)
        {
            CheckForItems();            //�̵��� ������ üũ
            lastPosition = transform.position;      //���� ��ġ�� ������ ��ġ�� ������Ʈ
        }

        //����� �������� �ְ� EŰ�� ������ �� ������ ����
        if (currentNearItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearItem.Collectiltem(GetComponent<PlayerInventory>());          //Player inventory�� �����Ͽ� ������ ����
        }
    }

    //�ֺ��� ���� ������ �������� �����ϴ� �Լ�
    private void CheckForItems()
    {
        //���� ���� ���� ��� �ݶ��̴��� ã��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);

        float closestDistance = float.MaxValue;         //���� ����� �Ÿ��� �ʱⰪ
        CollectibleItem closestItem = null;             //���� ����� ������ �ʱⰪ

        foreach (Collider collider in hitColliders)     //�� �ݶ��̴��� �����Ͽ� ���� ������ �������� ã��
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();            //�������� ����
            if (item != null && item.canCollect) //�������� �ְ� ���� �������� Ȯ��
            {
                float distance = Vector3.Distance(transform.position, item.transform.position); //�Ÿ� ���
                if (distance < closestDistance)                //�� ����� �������� �߰� �� ������Ʈ
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }
        if (closestItem != currentNearItem)             //���� ����� �������� ����Ǿ��� ���� �޼��� ǥ��
        {
            currentNearItem = closestItem;      //���� ����� ������ ������Ʈ
            if (currentNearItem != null)
            {
                Debug.Log($"[E] Ű�� ���� {currentNearItem.itemName} ���� ");         //���ο� ������ ���� �޼��� ǥ��
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;          //���� ���� ���� ����
        Gizmos.DrawWireSphere(transform.position, checkRadius);         //���� ������ ��ü�� ǥ��
    }
}