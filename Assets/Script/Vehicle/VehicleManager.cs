using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public Vehicle[] vehicle;

    public Car car;             //������Ʈ ����
    public Bicycle bicycle;     //������Ʈ ����

    float Timer;                //Ÿ�̸� ����

    void Update()
    {
        Timer -= Time.deltaTime;        //Ÿ�̸� ī��Ʈ�� �Ѵ�.
        if(Timer <= 0 )
        {
            for (int i = 0; i < vehicle.Length; i++)
            {
                vehicle[i].Horn();
            }
            //car.Horn();
            //bicycle.Horn();

             Timer = 1.0f;               //1�ʷ� ������ش�.
        }
    }
}
