using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public float speed = 10f;                     //이동속도 변수선언

    //가상 함수 : 이동

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);   //앞으로 해당 속도만큼 이동
    }

    //추상함수 : 경적
    public abstract void Horn();               //경적함수선언
}
