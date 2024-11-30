using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger - 100;               //�ִ� ��ⷮ
    public float currentHunger;                 //���� ��ⷮ
    public float hungerSecreaseRate = 1;        //�ʴ� ��� ���ҷ�

    [Header("Space Suit Settings")]
    public float maxSuitDurability - 100;       //�ִ� ���ֺ� ������
    public float currentSuitDurability ;        //���� ���ֺ� ������
    public float havestingDamage = 5.0f;        //������ ���ֺ� ������
    public float havestingDamage = 3.0f;        //���۽� ���ֺ� ������

    private bool isGameOver = false;            //���� ���� ����
    private bool isPaused = false;              //�Ͻ� ���� ����
    private float hungerTimer = 0;              //��� ���� Ÿ�̸�

    public float GetHungerPercentage()          //����� & ���� �Լ�
    {
        return (currentHunger / maxSuitDurability) * 100;
    }

    public bool IsGameOver()                    //���� ���� Ȯ�� �Լ�
    {
        return isGameOver;
    }

    public void ResetStats()                    //���� �Լ� �ڼ� ( ������ �ʱ�ȭ �뵵)
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
