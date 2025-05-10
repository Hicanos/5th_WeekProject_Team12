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

    private void Update()
    {

        HandleInput();         // 이동 입력 감지


        Vector2 input = new Vector2(moveX, moveY);
        Move(input); // BaseController의 이동 처리 호출

        SkillCall();

        HandleJump();   // 점프 입력 처리


    }
    private void FixedUpdate()
    {
        HandleAnimation();     // 이동 애니메이션 처리
        SkillActivate();
        HeadSpriteFlip();    // 캐릭터 좌우 반전
        CheckLanding();

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



