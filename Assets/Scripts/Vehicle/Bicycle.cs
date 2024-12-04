using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : Vehicle
{
    public override void Move()
    {
        base.Move();     //기본 함수 동작을 base 키워드로 동작시킨다.
        //자전거 만의 추가 동작
        transform.Rotate(0, Mathf.Sin(Time.time) * 10 * Time.deltaTime, 0);
        Debug.Log(Mathf.Sin(Time.time));
    }

    public override void Horn()
    {
        Debug.Log("자전거 경적소리");
    }
}
