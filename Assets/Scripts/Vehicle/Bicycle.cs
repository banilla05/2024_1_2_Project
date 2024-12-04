using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : Vehicle
{
    public override void Move()
    {
        base.Move();     //�⺻ �Լ� ������ base Ű����� ���۽�Ų��.
        //������ ���� �߰� ����
        transform.Rotate(0, Mathf.Sin(Time.time) * 10 * Time.deltaTime, 0);
        Debug.Log(Mathf.Sin(Time.time));
    }

    public override void Horn()
    {
        Debug.Log("������ �����Ҹ�");
    }
}
