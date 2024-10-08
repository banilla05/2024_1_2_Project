using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public Vehicle[] vehicle;

    public Car car;             //컴포넌트 선언
    public Bicycle bicycle;     //컴포넌트 선언

    float Timer;                //타이머 선언

    void Update()
    {
        Timer -= Time.deltaTime;        //타이머 카운트를 한다.
        if(Timer <= 0 )
        {
            for (int i = 0; i < vehicle.Length; i++)
            {
                vehicle[i].Horn();
            }
            //car.Horn();
            //bicycle.Horn();

             Timer = 1.0f;               //1초로 만들어준다.
        }
    }
}
