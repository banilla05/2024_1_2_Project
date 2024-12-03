using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public float speed = 10f;

    // 가상 함수 : 이동
    public virtual void Move()
    {
        // 앞으로 해당 속도만큼 움직인다
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // 추상 함수 : 경적
    public abstract void Horn();     // 경적 함수는 선언만 한다.
}
