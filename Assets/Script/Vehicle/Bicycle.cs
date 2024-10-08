using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bicycle : Vehicle              //탈것 클래스 상속
{
    public override void Move()
    {
        base.Move();    //기본 함수 동작을 base 키위드로 동작 시킨다.
        //자전거 만의 추가 동작
        transform.Rotate(0, Mathf.Sin(Time.time) * 10 * Time.deltaTime, 0);
    }

    public override void Horn()
    {
        Debug.Log("자전거 경적");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
