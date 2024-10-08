using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;        //상태 머신에 대한 참조 (이후 구현)
    protected PlayerController playerController;        //플레이러 컨트롤러에 대한 참조

    //생성자: 상태 머신과 플레이어 컨트롤러 참조 초기화
    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.PlayerController;
    }

    //가상 메서드들 : 하위 클래서에서 필요에 따라 오버라이드
    public virtual void Enter() { }     //상태 진입 시 호출
    public virtual void Exit() { }      //상태 종료 시 호출
    public virtual void Update() { }    //매 프레임 호출
    public virtual void FixedUpdate() { }  //고정 시간 간격으로 호출 (물리 연산용)

    //상태 전환과 조건을 체크하는 매서드
    protected void CheckTransition()
    {
        if(playerController.isGround())
        {
            //지상에 있을 때의 상태 전환 로직
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Verctal") != 0)    //이동키가 눌렸을 때
            {
                stateMachine.TransitionToState(new MovingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new IdleState(stateMachine));
            }
        }
        else
        {
            //공중에 있을때 상태 전환 로직
            if(playerController.GetVerticalVelocity() > 0)
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new FallingState(stateMachine));
            }
        }

    }
}

//IdleState : 플레이어가 정지해 있는 상태
public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransition();          //매 프레임마다 상태 전환 조건 체크
    }
}

//MovingState : 플레이어가 정지해 있는 상태
public class MovingState : PlayerState
{
    public MovingState(PlayerStateMachine stateMachine) : base (stateMachine) { }

    public override void Update()
    {
        CheckTransition();              //매 프레임마다 상태 전환 조건 체크
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();              //물리 기반 이동 처리
    }
}

public class JumpingState : PlayerState
{
    public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        CheckTransition();              //매 프레임마다 상태 전환 조건 체크
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();              //물리 기반 이동 처리
    }
}

//FallingState : 플레이어가 낙하 중일때
public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransition();          //매 프레임마다 상태 전환 조건 체크
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();          //물리 기반 이동 처리
    }
}


