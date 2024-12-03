using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger = 100;               // 최대 허기량
    public float currentHunger;                 // 현재 허기량
    public float hungerDecreaseRate = 1;        // 초당 허기 감소량

    [Header("Space Suit Settings")]
    public float maxSuitDurability = 100;       // 최대 우주복 내구도
    public float currentSuitDurability;         // 현재 우주복 내구도
    public float harvestingDamage = 5.0f;       // 수집시 우주복 데미지
    public float craftingDamage = 3.0f;         // 제작시 우주복 데미지

    private bool isGameover = false;            // 게임 오버 상태
    private bool isPaused = false;              // 일시정지 상태
    private float hungerTimer = 0;              // 허기 타이머

    // Start is called before the first frame update
    void Start()
    {
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameover || isPaused) return;

        hungerTimer += Time.deltaTime;

        if (hungerTimer >= 1.0f)
        {
            currentHunger = Mathf.Max(0, currentHunger - hungerDecreaseRate);
            hungerTimer = 0;

            CheckDeath();
        }
    }

    // 아이템 수집시 우주복 데미지
    public void DamageOnHarvesting()
    {
        if (isGameover || isPaused) return;                                                     // 게임 종료나 중단 상태에서는 동작하지 않게

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - harvestingDamage);         // 0값 이하로 내려가지 않기 위해
        CheckDeath();
    }

    // 아이템 제작시 우주복 데미지
    public void DamageOnCrafting()
    {
        if (isGameover || isPaused) return;                                                     // 게임 종료나 중단 상태에서는 동작하지 않게

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - craftingDamage);         // 0값 이하로 내려가지 않기 위해
        CheckDeath();
    }

    // 음식 섭취로 허기 회복
    public void EatFood(float amount)
    {
        if (isGameover || isPaused) return;                                                     // 게임 종료나 중단 상태에서는 동작하지 않게

        currentHunger = Mathf.Min(maxHunger, currentHunger + amount);                           // max 값을 넘기지 않기 위해

        if (FloatingTextManager.instance != null)
        {
            FloatingTextManager.instance.Show($"허기 회복 + {amount}", transform.position + Vector3.up);
        }
    }

    // 우주복 수리 (크리스탈로 제작한 수리 키트 사용)
    public void RepairSuit(float amount)
    {
        if (isGameover || isPaused) return;                                                     // 게임 종료나 중단 상태에서는 동작하지 않게

        currentSuitDurability = Mathf.Min(maxSuitDurability, currentSuitDurability + amount);   // max 값을 넘기지 않기 위해

        if (FloatingTextManager.instance != null)
        {
            FloatingTextManager.instance.Show($"우주복 수리 + {amount}", transform.position + Vector3.up);
        }
    }

    private void CheckDeath()               // 플레이어 사망 처리 체크 함수
    {
        if(currentHunger <= 0 || currentSuitDurability <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()              // 플레이어 사망 함수
    {
        isGameover = true;
        Debug.Log("플레이어 사망");
        //TODO : 사망 처리 추가 (게임오버 UI, 리스폰 등등)
    }

    public float GetHungerPercentage()              // 허기짐 % 리턴 함수
    {
        return (currentHunger / maxHunger) * 100;
    }

    public float GetSuitDurabilityPercentage()      // 슈트 % 리턴 함수
    {
        return (currentSuitDurability / maxSuitDurability) * 100;
    }

    public bool IsGameOver()        // 게임 종료 확인
    {
        return isGameover;
    }

    public void ResetStats()        // 리셋 함수 작성 (변수들 초기화 용도)
    {
        isGameover = false;
        isPaused = false;
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
        hungerTimer = 0;
    }
}
