using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�÷��̾��� ���¸� ����
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;        //���� �����̾��� ���¸� ��Ÿ���� ����
    public PlayerController PlayerController;     //PlayerController�� ����

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();        //���� ������Ʈ�� �پ��ִ� PlayerController
    }

    void Start()
    {
        //�ʱ� ���¸� IdleState �� ����
        TransitionToState(new IdleState(this));
    }

    void Update()
    {
        //���� ���°� �����Ѵٸ� �ش� ������ Update �ż��� ȣ��
        if( currentState != null)
        {
            currentState.Update();
        }
    }
    private void FixedUpdate()
    {

        //���� ���°� �����Ѵٸ� �ش� ������ FixedUpdate �ż��� ȣ��
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }

    public void TransitionToState(PlayerState newState)
    {
        //���� ���¿� ���ο� ���°� ���� Ÿ�� �� ���
        if (currentState?.GetType() == newState.GetType())
        {
            return;             //���� Ÿ���̸� ���¸� ��ȯ ���� �ʰ� ����
        }

        //���� ���°� �����Ѵٸ� Exit �ż��帣 ȣ��
        currentState?.Exit();       //�˻��ؼ� ȣ�� ���� (?)�� IF ����

        //���ο� ���·� ��ȯ
        currentState = newState;

        //���ο� ������ Enter �޼��� ȣ�� (���� ����)
        currentState.Enter();

        //�α׿� ���� ��ȯ ������ ���
        //Debug.Log($"���� ��ȯ �Ǵ� ������Ʈ : {newState.GetType().Name}");
    }
}
