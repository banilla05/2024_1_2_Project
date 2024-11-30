using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger - 100;               //최대 허기량
    public float currentHunger;                 //현재 허기량
    public float hungerSecreaseRate = 1;        //초당 허기 감소량

    [Header("Space Suit Settings")]
    public float maxSuitDurability - 100;       //최대 우주복 내구도
    public float currentSuitDurability ;        //현재 우주복 내구도
    public float havestingDamage = 5.0f;        //수접시 우주복 데미지
    public float havestingDamage = 3.0f;        //제작시 우주복 데미지

    private bool isGameOver = false;            //게임 오버 상태
    private bool isPaused = false;              //일시 정시 상태
    private float hungerTimer = 0;              //허기 감소 타이머

    public float GetHungerPercentage()          //허기짐 & 리턴 함수
    {
        return (currentHunger / maxSuitDurability) * 100;
    }

    public bool IsGameOver()                    //게임 종료 확인 함수
    {
        return isGameOver;
    }

    public void ResetStats()                    //리셋 함수 자성 ( 변수들 초기화 용도)
    {
        isGameOver = false;
        isPaused = false;
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
        hungerTimer = 0;
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
