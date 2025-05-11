using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaramCharacterDog : Characterbase
{

    protected override void Awake()
    {
        enumChar = CHAR.DOG;
        base.Awake();
        ControlKey();
    }
    /// <summary>
    /// 입력매서드는 여기에
    /// </summary>
    private void Update()
    {
        HandleInput();         // 이동 입력 
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
        
        Move(moveInput); // 이동 호출 
        HeadSpriteFlip();    // 캐릭터 좌우 반전
        HandleMoveAnim();     // 이동 애니메이션 
        SkillActivate(); //스킬발동
    }

    protected override void SkillCall()
    {
        if (IsGrounded() && IsGrounded() && currentJumpCount == 0)
            base.SkillCall();
    }
    protected override void Skill()
    {

        Vector2 currentLook; /*= new Vector2(moveX, 0).normalized;*/
        if (this.spriteRenderer.flipX == false)
        {
            currentLook = new Vector2(1, 0);
        }
        else { currentLook = new Vector2(-1, 0); }
        rb.velocity = Vector2.zero; //  속도 초기화
        rb.AddForce(currentLook * 50f, ForceMode2D.Impulse);

        //스킬 구성에 있어 좌우 방향 Update에서 갱신중이니 예외 필요
        //예외처리 단순화를 위해서 "상태머신" 고려하기 
    }
}



