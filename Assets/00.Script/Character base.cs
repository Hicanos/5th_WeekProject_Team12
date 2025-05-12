using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 모든 캐릭터(플레이어/몬스터 등)의 공통 기능을 담당하는 추상 컨트롤러
/// 이동, 체력 처리, 스프라이트 반전 등 기본 행동 제공
/// </summary>
public enum PLAYERSTATE //상태머신 enum (아직 미구현)
{
    IDLE,
    MOVE,
    JUMP,
    DASH,
    WALL
}


public abstract class Characterbase : MonoBehaviour
{
    protected Rigidbody2D rb;                    // 이동을 위한 리지드바디
    protected SpriteRenderer spriteRenderer;     // 좌우 반전을 위한 스프라이트 렌더러
    protected MyAnimationController Anim;
   

    /// <summary> 초기화: 리지드바디, 스프라이트 찾고 스탯 초기화 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<MyAnimationController>();
    }
   
    
    protected PLAYERSTATE currentState;
    //상태변경 함수 미구현
    protected void ChangeState(PLAYERSTATE newState)
    {
        if (currentState == newState) return;

        Debug.Log($"상태 전이: {currentState} -> {newState}");
        currentState = newState;
    }

    //조작 스타일 enum
    protected enum CHAR
    {
        DOG,
        CAT
    }
    [Header("조작 스타일")]
    [SerializeField] protected CHAR enumChar;
    protected void ControlKey()
    {
        switch (enumChar)
        {
            case CHAR.DOG:
                leftKey = KeyCode.LeftArrow;
                rightKey = KeyCode.RightArrow;
                jumpKey = KeyCode.UpArrow;
                SkillKey = KeyCode.DownArrow;
                break;

            case CHAR.CAT:
                leftKey = KeyCode.A;
                rightKey = KeyCode.D;
                jumpKey = KeyCode.W;
                SkillKey = KeyCode.S;
                break;
        }

    }

    /*---------------------------------------캐릭터 공통 변수---------------------------------------*/
    
    // 입력값 저장
    protected float moveX;
    protected float moveY;
    protected Vector2 moveInput;
  
    [Header("이름")]
    [SerializeField] protected string Name = string.Empty;

    [Header("조작키 설정")]
    [SerializeField] protected KeyCode leftKey;
    [SerializeField] protected KeyCode rightKey;
    [SerializeField] protected KeyCode jumpKey;
    [SerializeField] protected KeyCode SkillKey;
    [SerializeField] protected int moveSpeed = 5;

    [Header("횡스크롤 물리 기반 점프 시스템")]
    [SerializeField] protected Transform groundCheck;      // 바닥 판정 위치
    [SerializeField] protected float groundRayRange = 0.1f; // 바닥 체크 범위(raycast길이)
    [SerializeField] protected LayerMask groundLayer;      // 바닥 레이어
    [SerializeField] protected float jumpPower = 5f;       // 점프 힘
    [SerializeField] protected int maxJumpCount = 2;//다중점프 최대 횟수
    protected int currentJumpCount = 0;//현재 점프수 저장 할 변수

    [Header("헬멧 피벗")]
    [SerializeField] protected Transform headPivot;
    protected GameObject helmet;

    [Header("스킬 쿨타임")]
    [SerializeField] protected float SkillCoolTime = 1f;
    
    /*---------------------------------------단발성 스킬 모음---------------------------------------*/
    //스킬 요청할 불
    protected bool SkillReq = false;

    // 스킬 입력 받을 매서드
    protected virtual void InstantSkillCall()
    {
        if (Input.GetKeyDown(SkillKey) && skillReady)
        {
            //쿨타임 시작 
            StartCoroutine(SkillCoolDown(SkillCoolTime));
            SkillReq = true;
            Debug.Log($"{Name}스킬 키 입력");
        }
    }

    //단발성 스킬호출
    protected virtual void InstantSkillActivate()
    {
        if (SkillReq)
        {
            SkillReq = false;
            InstantSkill();

            Debug.Log($"{Name}스킬발동");
        }
    }
    // 재정의용 단발성 스킬 함수
    protected virtual void InstantSkill() { }

    /*---------------------------------------토글형 스킬 모음---------------------------------------*/
    //토글스킬용 불값
    protected bool isToggled = false;

    //토글형 스킬 입력 감지
    protected virtual void ToggleSkillCall()
    {
        if (Input.GetKeyDown(SkillKey) && !isToggled)
        {
            //쿨타임 시작 
            StartCoroutine(SkillCoolDown(SkillCoolTime));
            isToggled = true;
            ToggleSkillOn();
            Debug.Log($"{Name}스킬 키 입력");
            Debug.Log($"{Name}토글스킬발동");
        }
        else if (Input.GetKeyDown(SkillKey) && isToggled)
        {
            isToggled = false;
            ToggleSkillOff();
            Debug.Log($"{Name}스킬 키 입력");
            Debug.Log($"{Name}토글스킬해제");
        }
    }

    //재정의용 토글 스킬 On함수
    protected virtual void ToggleSkillOn() { }

    //재정의용 토글 스킬 Off함수
    protected virtual void ToggleSkillOff() { }

    /*---------------------------------------쿨타임 코루틴---------------------------------------*/
    //스킬 쿨타임용 불
    protected bool skillReady = true;

    //스킬 쿨타임용 코루틴
    protected virtual IEnumerator SkillCoolDown(float cooldown)
    {
        Debug.Log("쿨타임 발동");
        cooldown = SkillCoolTime;
        skillReady = false;
        yield return new WaitForSeconds(cooldown);
        skillReady = true;
        Debug.Log("쿨타임 끝");
    }
    /*---------------------------------------애니메이션용 함수---------------------------------------*/
    protected virtual void HandleJumpAnim()
    {
        bool isJump = currentJumpCount > 0 || (currentJumpCount == 0 && !IsGrounded());
        Anim.SetJump(isJump);
    }
    protected virtual void HandleSkillAnim()
    {
        bool isSkill = SkillReq;
        Anim.SetSkill(isSkill);
    }

    protected virtual void HandleCrashAnim()
    {
        Anim.SetCrash();
    }
    protected virtual void HandleMoveAnim()
    {
        bool isMoving = moveX != 0;
        Anim.SetMove(isMoving);
    }
    /// <summary>
    /// 캐릭터 좌우 반전 (flipX) 처리
    /// 무기에는 적용하지 않음
    /// </summary>
    protected virtual void SpriteFlip()
    {
        if (moveX != 0)
            Anim.SetFlip(moveX < 0);
        if (moveX != 0)
            if (headPivot != null)
            {
                headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f);
            }
    }
    /*---------------------------------------움직임관련 함수---------------------------------------*/
    //점프 콜을 위한 불
    protected bool jumpReq = false;
    //땅에 붙어있는지 감지할 불반환 함수
    protected virtual bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundRayRange, groundLayer);
        return hit.collider != null;
    }
    /// <summary>
    /// 이동 키 입력을 받아 방향 벡터 계산
    /// </summary>
    protected virtual void MoveCall()
    {
        moveX = 0;
        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
        //입력값을 통해 방향 구하기
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    /// <summary> 이동 처리: 속도 적용 </summary>
    protected virtual void MoveActivate(Vector2 input)
    {
        Vector2 velocity = rb.velocity;
        // 이동속도를 방향에 곱해주기
        velocity.x = input.x * moveSpeed;
        rb.velocity = velocity;
    }

    /// <summary>
    /// 점프 키 입력 감지 및 쿨타임 확인 후 점프 실행
    /// </summary>
    protected virtual void JumpCall()//점프키 입력 감지
    {   //키 입력, 점프 횟수가 최대 점프보다 작을 때,벽타기 중이 아닐 때 
        if ((Input.GetKeyDown(jumpKey) && currentJumpCount < maxJumpCount))
        {
            // 단순 낙하중에 점프 방지
            if (currentJumpCount == 0 && !IsGrounded())
            { return; }

            jumpReq = true;
        }
    }

    //점프 호출
    protected virtual void JumpAtivate()
    {
        if (jumpReq)
        {   // Y 속도 초기화
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            //up방향으로 점프파워만큼 임펄스 에드포스
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            if (currentJumpCount < maxJumpCount)
            {
                currentJumpCount++;
                //점프 횟수 누적 및 디버깅용 로그 
                Debug.Log($"{Name}점프 {currentJumpCount}/{maxJumpCount}");
            }
            jumpReq = false;
        }
    }

    //땅에 착지했는지 감지하는 매서드
    protected void CheckLanding()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0 && IsGrounded())
        {
            currentJumpCount = 0;
            Anim.SetJump(false);

            Debug.Log($"{Name}착지!");
        }
    }
    /*---------------------------------------ect.---------------------------------------*/

    //raycast 기즈모 시각화하기 위한 매서드
    protected virtual void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundRayRange);

    }
}

