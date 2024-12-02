using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerController playerController;        //PlayerController�� ����

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();   //���� ������Ʈ�� �پ��ִ� PlayerController�� ����
    }

    void Start()
    {
        //�ʱ� ���¸� idleState �� ����
        TransitionToState(new IdleState(this));
    }

    void Update()
    {
        //���� ���°� �����Ѵٸ� �ش� ������ Update �޼��� ȣ��
        if (currentState != null)
        {
            currentState.Update();
        }

        //�׽�Ʈ
        //Debug.Log("Test_������� Ÿ���� " + currenState.GetType());
    }
    void FixedUpdate()
    {
        //���� ���°� �����Ѵٸ� �ش� ������ FixedUpdate �޼��� ȣ��
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }

    public void TransitionToState(PlayerState newState)
    {
        //���� ���¿� ���ο� ���°� ������ Ȯ��
        if (currentState?.GetType() == newState.GetType())
        {
            return;  //���� Ÿ���̸� ���¸� ��ȯ���� �ʰ� ����
        }
        //���� ���°� �����Ѵٸ� Exit �޼��带 ȣ��
        currentState?.Exit();   //�˻��ؼ� ȣ�� ����(?)�� IF����

        //���ο� ���·� ��ȯ
        currentState = newState;

        //���ο� ������ Enter �޼��� ȣ�� (���� ����)
        currentState.Enter();

        //�α׿� ���� ��ȯ ������ ���
        Debug.Log($"���� ��ȯ �Ǵ� ������Ʈ : {newState.GetType().Name}");
    }
}
