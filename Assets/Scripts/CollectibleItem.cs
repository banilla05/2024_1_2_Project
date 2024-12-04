using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;                         //������ ����(�� : ũ����Ż, �Ĺ�, ��Ǯ, ����)
    public string itemName;                           //������ �̸�
    public float respawnTime = 30.0f;                 //������ �ð�(�������� �ٽ� ���� �� �� ������ ��� �ð�)
    public bool canCollect = true;                    //���� ���� ����(������ �� �ִ��� ���θ� ��Ÿ��)

    //�������� �����ϴ� �޼���, playerInventory�� ���� �κ��丮�� �߰�
    public void CollectItem(PlayerInventory inventory)
    {
        //�������ɿ����Ǵ�
        if(!canCollect) return;

        inventory.AddItem(itemType);
        
        if (FloatingTextManager.instance != null)
        {
            Vector3 textPosition = transform.position + Vector3.up * 0.5f;
            FloatingTextManager.instance.Show($"+{itemName}", textPosition);
        }
        StartCoroutine(RespawnRoutine());
    }
    //������ �������� ó���ϴ� �ڷ�ƾ
    private IEnumerator RespawnRoutine()
    {
        canCollect = false;                                   //�����Ұ��� ���·κ���
        GetComponent<MeshRenderer>().enabled = false;         //��Ƽ���� MeshRenderer�� ���� ������ �ʰ� ��
        GetComponent<MeshCollider>().enabled = false;

        yield return new WaitForSeconds(respawnTime);

        GetComponent<MeshRenderer>().enabled = true;          //�������� �ٽ� ���̰� ��
        GetComponent<MeshCollider>().enabled = true;
         canCollect = true;                                   //�������� ���·� ����
    }
}
