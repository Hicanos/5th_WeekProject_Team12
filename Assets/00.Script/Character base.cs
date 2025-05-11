using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 모든 캐릭터(플레이어/몬스터 등)의 공통 기능을 담당하는 추상 컨트롤러
/// 이동, 체력 처리, 스프라이트 반전 등 기본 행동 제공
/// </summary>


public abstract class Characterbase : MonoBehaviour
{
    // 입력값 저장
    protected float moveX;
    protected float moveY;
    protected Vector2 moveInput;

    protected Rigidbody2D rb;                    // 이동을 위한 리지드바디
    protected SpriteRenderer spriteRenderer;     // 좌우 반전을 위한 스프라이트 렌더러
    protected MyAnimationController Anim;
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


    [Header("이름")]
    [SerializeField]protected string Name = string.Empty; 
    
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

    [Header("고양이 용 벽탐지")]
    [SerializeField] protected Transform WallCheck;//벽 판정 위치
    [SerializeField] protected float wallRayRange = 0.1f; //벽  체크 범위(raycast길이)
    [SerializeField] protected LayerMask wallLayer;//벽 레이어
    
    [Header("헬멧 피벗")]
    [SerializeField] protected Transform headPivot;
    protected GameObject helmet;

    [Header("스킬 쿨타임")]
    [SerializeField] protected float SkillCoolTime = 1f;
    /// <summary> 초기화: 리지드바디, 스프라이트 찾고 스탯 초기화 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<MyAnimationController>();
    }


    //스킬용 변수
    protected bool SkillReq = false; //스킬 요청할 불
    protected bool skillReady = true;//스킬 쿨타임용 불
    protected virtual void SkillCall() // 스킬 입력 받을 매서드
    {
        if (Input.GetKeyDown(SkillKey) && skillReady)
        {

            StartCoroutine(SkillCoolDown(SkillCoolTime));//쿨타임 시작 
            SkillReq = true;
            Debug.Log("스킬 키 입력");
        }
        

    }
    protected virtual void InstantSkillActivate()      //단발성 스킬호출
    {
        if (SkillReq)
        {
            SkillReq = false;
            Skill();

            Debug.Log("스킬발동");
        }

    }
    protected virtual void ToggleSkillActivate() //on/off형 스킬 호출
    {
        if (SkillReq)
        {
            
            Skill();

            Debug.Log("스킬발동");
        }
        
    }

    protected virtual void Skill()
    {


    }
    protected virtual IEnumerator SkillCoolDown(float cooldown)//스킬 쿨타임용 코루틴
    {
        Debug.Log("쿨타임 발동");
        cooldown = SkillCoolTime;
        skillReady = false;
        yield return new WaitForSeconds(cooldown);
        skillReady = true;
        Debug.Log("쿨타임 끝");
    }
    protected virtual void HandleSkillAnim()
    {
        bool isSkill = SkillReq;
        Anim.SetSkill(isSkill);

    }


    
    
    /// <summary>
    /// 이동 키 입력을 받아 방향 벡터 계산
    /// </summary>
    protected virtual void MoveCall() //이동키 입력 감지 
    {
        /*moveX = Input.GetAxisRaw("Horizontal");*///각기 다른 두 캐릭터를 조작해야 해서 입력 방식을 바꿈 
        moveX = 0;

        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
        moveInput = new Vector2(moveX, moveY).normalized;//입력값을 통해 방향 구하기
    }
    /// <summary> 이동 처리: 속도 적용 </summary>
    protected virtual void MoveActivate(Vector2 input) //이동 호출용 매서드 
    {

        Vector2 velocity = rb.velocity;
        velocity.x = input.x * moveSpeed;// 이동속도를 방향에 곱해주기

        rb.velocity = velocity;


        // 좌우 반전 처리
        /*     if (input.x != 0)
                 spriteRenderer.flipX = input.x < 0; */

    }
    /// <summary>
    /// 이동 중 여부에 따라 애니메이션 파라미터 설정
    /// </summary>
    protected void HandleMoveAnim()
    {
        bool isMoving = moveX != 0;
        Anim.SetMove(isMoving);
    }
    protected void SpriteFlip() // 좌우반전 위한 매서드
    {
        if (moveX != 0)
            Anim.SetFlip(moveX < 0);
        if (moveX != 0)
            if (headPivot != null)
            {
                headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f);
            }
        if (WallCheck != null) //WallCheck - raycast기준점이 되는 오브젝트 뒤집기 
        {
            if (spriteRenderer.flipX)
            {
                WallCheck.localPosition = new Vector3(-wallRayRange, 0f, 0f); // 왼쪽  
            }
            else
            {
                WallCheck.localPosition = new Vector3(wallRayRange, 0f, 0f); // 오른쪽
            }
        }
    }



    /// <summary>
    /// 캐릭터 좌우 반전 (flipX) 처리
    /// 무기에는 적용하지 않음
    /// </summary>


    protected bool jumpReq = false;//점프 콜을 위한 불
    /// <summary>
    /// 점프 키 입력 감지 및 쿨타임 확인 후 점프 실행
    /// </summary>
    protected void JumpCall()//점프키 입력 감지
    {
        if ((Input.GetKeyDown(jumpKey) && currentJumpCount < maxJumpCount)&&!isClimb)//키 입력, 점프 횟수가 최대 점프보다 작을 때,벽타기 중이 아닐 때 
        {
            if (currentJumpCount == 0 && !IsGrounded())// 단순 낙하중에 점프 방지
            { return; }
            jumpReq = true;
        }
    }
    protected void JumpAtivate()//점프 호출
    {
        if (jumpReq)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Y 속도 초기화
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);//up방향으로 점프파워만큼 임펄스 에드포스

            if (currentJumpCount < maxJumpCount)//점프 횟수 누적 및 디버깅용 로그 
            {
                currentJumpCount++;
                Debug.Log($"{Name}점프 {currentJumpCount}/{maxJumpCount}");
            }
            jumpReq = false;
        }


    }

    protected void HandleJumpAnim()
    {
        bool isJump = currentJumpCount > 0 || (currentJumpCount == 0 && !IsGrounded());
        Anim.SetJump(isJump);
    }

    protected void CheckLanding()//땅에 착지했는지 감지하는 매서드
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0 && IsGrounded())
        {
            currentJumpCount = 0;
            Anim.SetJump(false);

            Debug.Log($"{Name}착지!");
        }
    }

    //점프 착지 판정용 불값 필요 시 사용
    protected bool IsGrounded()//땅에 붙어있는지 감지할 불값
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundRayRange, groundLayer);
        return hit.collider != null;
    }
   //벽 감지용 불값
    protected bool IsWallClimb()//벽타기 가능한 상태인지 감지할 불 값
    {
        if (!spriteRenderer.flipX)
        { RaycastHit2D hit = Physics2D.Raycast(WallCheck.position, Vector2.right, wallRayRange, wallLayer);
            return hit.collider != null; }
        else 
        {
            RaycastHit2D hit = Physics2D.Raycast(WallCheck.position, Vector2.left, wallRayRange, wallLayer);
            return hit.collider != null; 
        }


        }
    protected bool isClimb = false; //벽타기 중인지 확인할 불
    protected void OnDrawGizmosSelected() //raycast 기즈모 시각화하기 위한 매서드
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundRayRange);
        if (WallCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(WallCheck.position, WallCheck.position + Vector3.right * wallRayRange);
    }
    
    //밥먹으로 갔음메 
    //먹고 와서 할것, 토글형 스킬 구분 한것 마무리 하기 
    //벽에 달라붙게 하기 
    //
}

