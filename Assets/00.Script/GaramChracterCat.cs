using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterCat : Characterbase
{
    protected override void Awake()
    {
        enumChar = CHAR.CAT; // 캐릭터 분류하기
        base.Awake();
        ControlKey();//분류에 따라 조작키 할당
    }
    /// <summary>
    /// 입력매서드는 여기에
    /// </summary>
    private void Update()
    {
       
            MoveCall();         // 이동 입력 
        SkillCall(); //스킬 키 입력
        JumpCall();   // 점프 입력 
    }
    /// <summary>
    /// 행동매서드는 여기에
    /// </summary>
    private void FixedUpdate()
    {
        JumpAtivate();//점프 실행
        HandleJumpAnim();//점프 애니메이션
        CheckLanding();//착지 판정
        
        MoveActivate(moveInput); // 이동 호출 
        SpriteFlip();    // 캐릭터 좌우 반전
        HandleMoveAnim();     // 이동 애니메이션 
        InstantSkillActivate(); //스킬발동
    }



    protected override void SkillCall()
    {
        if (Input.GetKey(SkillKey) && skillReady&&IsWallClimb())
        {
            StartCoroutine(SkillCoolDown(SkillCoolTime));
            SkillReq = true;
            Debug.Log("스킬 키 입력");
        }
    }
    protected override void Skill()
    {
        if (SkillReq)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f); //벽에 붙듯이 보여주기 위해 z축 회전
            rb.gravityScale = 0f; //중력 0으로 만들기 
            isClimb = true;

        }
        else 
        {
            transform.rotation = Quaternion.identity; // 각도 초기값 (0,0,0)
            rb.gravityScale = 1f;
            isClimb = false;
        }
       
    }
   
    protected override void MoveCall()//벽에 붙었을 시에 조작 방향 변경을 위한 재정의
    {
        if (!isClimb)
        {
            moveX = 0;

            if (Input.GetKey(leftKey)) moveX -= 1;
            if (Input.GetKey(rightKey)) moveX += 1;
            moveInput = new Vector2(moveX, moveY).normalized;
        }
        else
        {
            moveY = 0;

            if (Input.GetKey(leftKey)) moveY -= 1;
            if (Input.GetKey(rightKey)) moveY += 1;
            moveInput = new Vector2(moveX, moveY).normalized;
        }
        }
    /// <summary> 이동 처리: 속도 적용 및 방향 반전 </summary>
    protected override void MoveActivate(Vector2 input)//벽에 붙었을 시에 조작 방향 변경을 위한 재정의
    {
        if (!isClimb)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = input.x * moveSpeed;

            rb.velocity = velocity;
        }
        else
        {
            Vector2 velocity = rb.velocity;
            velocity.y = input.y * moveSpeed;

            rb.velocity = velocity;
        }



    }


}