using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GaramCharacterDog : Characterbase
{

    protected override void Awake()
    {
        enumChar = CHAR.DOG;//캐릭터 분류
        base.Awake();
        ControlKey();//조작키할당
    }
    /// <summary>
    /// 입력매서드는 여기에
    /// </summary>
    private void Update()
    {
        if (isDash)
        {
            return;
        }
        else
        {
            InstantSkillCall(); //스킬 키 입력
            JumpCall();   // 점프 입력 
            MoveCall();   // 이동 입력 
        }


    }
    /// <summary>
    /// 행동매서드는 여기에
    /// </summary>
    private void FixedUpdate()
    {

        HandleJumpAnim();//점프 애니메이션
        CheckLanding();//착지 판정
        if (isDash)
        {
            return;
        }
        else
        {
            JumpAtivate();//점프 실행
            MoveActivate(moveInput); // 이동 호출 
        }


        SpriteFlip();    // 캐릭터 좌우 반전
        HandleMoveAnim();     // 이동 애니메이션 
        InstantSkillActivate(); //스킬발동
        HandleSkillAnim();//스킬 애니매이션
    }

    protected override void InstantSkillCall()
    {
        if (IsGrounded() && currentJumpCount == 0) //착지상태일때만 스킬 사용 가능 하게 하기
            base.InstantSkillCall();

    }
    protected override void InstantSkill()
    {

        Vector2 currentLook; //방향을 잡기위한 백터값
        if (this.spriteRenderer.flipX == false)
        {
            currentLook = new Vector2(1, 0);
        }
        else
        {
            currentLook = new Vector2(-1, 0);
        }
        rb.velocity = Vector2.zero; //  속도 초기화
        rb.AddForce(currentLook * 10f, ForceMode2D.Impulse);
        StartCoroutine(DashDuration(currentLook,0.2f,30f));

    }
    private IEnumerator DashDuration(Vector2 direction, float duration, float dashSpeed)
    {
        isDash = true;
        
        yield return new WaitForSeconds(0.1f);
        rb.velocity = direction * dashSpeed;  // 순간적으로 빠른 속도 설정
       

        yield return new WaitForSeconds(duration); // 딱 duration만큼만 이동

        rb.velocity = Vector2.zero;  // 즉시 멈추기
        isDash = false;
    }
    //스킬 구성에 있어 좌우 방향 Update에서 갱신중이니 예외 필요
    //예외처리 단순화를 위해서 "상태머신" 고려하기 
    private bool isDash = false; // 강아지 스킬발동중을 감지할 불값
    protected override void MoveCall() //이동키 입력 감지 
    {
        /*moveX = Input.GetAxisRaw("Horizontal");*///각기 다른 두 캐릭터를 조작해야 해서 입력 방식을 바꿈 

       
            moveX = 0;

            if (Input.GetKey(leftKey)) moveX -= 1;
            if (Input.GetKey(rightKey)) moveX += 1;
            moveInput = new Vector2(moveX, moveY).normalized;
        
        //입력값을 통해 방향 구하기
    }

    protected override void HandleSkillAnim()
    {
        bool isSkill = isDash;
        Anim.SetSkill(isSkill);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 돌진 중이 아닐 때는 무시
        if (!isDash) return;

        // 레이어 체크: 감지할 레이어에 해당하는지 확인
        if (((1 << other.gameObject.layer) & DestroyLayer) != 0)
        {
            Destroy(other.gameObject);
            HandleCrashAnim();
            isDash = false;                     
            rb.velocity = Vector2.zero;
            
            Debug.Log(" 돌진으로 파괴됨: " + other.name);
        }
    }

}




