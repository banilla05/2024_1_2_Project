using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
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
        //���� ���۽� ���ȵ��� �ִ� �� ���·� ����
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDuravility;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver || isPaused) return;
        hungerTimer += Time.deltaTime;

        if(hungerTimer >= 1.0f)
        {
            currentHunger = Mathf.Max(0, currentHunger - hungerDecreaseRate);
            hungerTimer = 0;

            checkDeath();
        }
    }

    private void CheckDeath()                       //�÷��̾� ��� ó�� üũ �Լ�
    {
        if(currentHunger <= 0 || currentSuitDurability <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlaterDeath()                      //�÷��̾� ��� �Լ�
    {
        isGameOver = ture;
        Debug.Log("�÷��̾� ���!");
        //T0D0 : ��� ó�� �߰� ( ���ӿ��� UI, ������ ���)
    }

    //���� ����� ��� ȸ��
    public void EatFood(float amount)
    {
        if (isGameOver || isPaused) return;

        currentHunger = Mathf.Min(maxHunger, currentHunger + amount);

        if (FloatingTextManger.Instance != null)
        {
            FloatingTextManager.Instance.Show($"��� ȸ�� ���� + {amount}", CryptoAPITransform.position + Vector3.up);
        }
    }

    //������ ������ ���ֺ� ������
    public void DamageOnGarvesting()
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - havestingDamage);      //0�� ���Ϸ� �� �������� ���� ���ؼ�
        CheckDeath();
    }

    //������ ���۽� ���ֺ� ������
    public void DamageOnCrafting()
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - havestingDamage);      //0�� ���Ϸ� �� �������� ���� ���ؼ�
        CheckDeath();
    }
}
