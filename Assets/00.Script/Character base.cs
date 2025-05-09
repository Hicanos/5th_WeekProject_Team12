using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 모든 캐릭터(플레이어/몬스터 등)의 공통 기능을 담당하는 추상 컨트롤러
/// 이동, 체력 처리, 스프라이트 반전 등 기본 행동 제공
/// </summary>


public abstract class Characterbase: MonoBehaviour
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
    [SerializeField]protected  CHAR enumChar;
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

    [Header("조작키 설정")]
    [SerializeField]protected KeyCode leftKey ;
    [SerializeField]protected KeyCode rightKey ;
    [SerializeField]protected KeyCode jumpKey ;
    [SerializeField]protected KeyCode SkillKey ;
    [SerializeField]protected int moveSpeed=5;
       
    [Header("횡스크롤 물리 기반 점프 시스템")]
    [SerializeField]protected Transform groundCheck;      // 바닥 판정 위치
    [SerializeField]protected float rayRange = 0.1f; // 바닥 체크 범위(raycast길이)
    [SerializeField]protected LayerMask groundLayer;      // 바닥 레이어
    [SerializeField]protected float jumpPower = 5f;       // 점프 힘
    [SerializeField]protected int maxJumpCount = 2;
    protected int currentJumpCount = 0;
   
    [Header("헬멧 피벗")]
    [SerializeField] protected Transform headPivot;
     protected GameObject helmet;

    /// <summary> 초기화: 리지드바디, 스프라이트 찾고 스탯 초기화 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<MyAnimationController>();
    }

    /// <summary> 이동 처리: 속도 적용 및 방향 반전 </summary>
    protected virtual void Move(Vector2 input)
    {

        Vector2 velocity = rb.velocity;
        velocity.x = input.x * moveSpeed;
        rb.velocity = velocity;

        
            // 좌우 반전 처리
       /*     if (input.x != 0)
                spriteRenderer.flipX = input.x < 0; */

     }
   
   protected virtual void Skill()
    { 
        
    
    }




    //점프 착지 판정용 불값 필요 시 사용
    protected bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayRange, groundLayer);
        return hit.collider != null;
    }
    protected void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * rayRange);
    }
    //벽에 붙기위한 콜라이더 감지
    /*  private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.CompareTag("wall"))
          {

          }
      }*/
    /// <summary>
    /// 이동 키 입력을 받아 방향 벡터 계산
    /// </summary>
    protected void HandleInput()
    {
        /*moveX = Input.GetAxisRaw("Horizontal");*/
        moveX = 0;

        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
        moveInput = new Vector2(moveX, moveY).normalized;
    }
    /// <summary>
    /// 이동 중 여부에 따라 애니메이션 파라미터 설정
    /// </summary>
    protected void HandleAnimation()
    {
        bool isMoving = moveX != 0;
        Anim.SetMove(isMoving);
    }




    /// <summary>
    /// 캐릭터 좌우 반전 (flipX) 처리
    /// 무기에는 적용하지 않음
    /// </summary>
    protected void HeadSpriteFlip()
    {
        if (moveX != 0)
            Anim.SetFlip(moveX < 0);
        if (moveX != 0)
            if (headPivot != null)
            {
                headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f);
            }
    }


    /// <summary>
    /// 점프 키 입력 감지 및 쿨타임 확인 후 점프 실행
    /// </summary>
    protected void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {

            if (currentJumpCount < maxJumpCount)
            {
                //Vector3 v = rb.velocity;
                //v.y +=  jumpPower;
                //rb.velocity = v;
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Y 속도 초기화
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                currentJumpCount++;
                Anim.SetJump(true);
                Debug.Log($"점프 {currentJumpCount}/{maxJumpCount}");
            }


        }
    }


}

