using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle                              //Vehicle 상속
{
    public override void Horn()                         //추상함수 실제 구현
    {
        Debug.Log("차동차 경적");
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
