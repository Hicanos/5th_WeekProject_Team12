using UnityEngine;

/// <summary>
/// 기본 캐릭터(플레이어/적 등)의 공통 동작을 정의하는 추상 클래스
/// 이동, 점프 처리, 스킬 처리 등 기본 행동 포함
/// </summary>
public enum PLAYERSTATE // 캐릭터 상태 enum (움직임 상태)
{
    IDLE,
    MOVE,
    JUMP,
    DASH,
    WALL
}

// TilemapCollider와 CompositeCollider 조합으로 충돌이 발생할 때 플레이어가 끼이거나 튕겨나가는 현상 방지를 위해
// 수평 속도 유지하면서 수직 이동만 반영하도록 수정 필요

public abstract class Characterbase : MonoBehaviour
{
    protected Rigidbody2D rb;                    // 이동을 위한 물리 컴포넌트
    protected SpriteRenderer spriteRenderer;     // 외형 표현용 스프라이트 렌더러
    protected MyAnimationController Anim;

    /// <summary> 초기화: 물리, 렌더러, 애니메이션 컴포넌트 초기 설정 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<MyAnimationController>();
    }

    protected PLAYERSTATE currentState;

    // 상태 전환 메서드
    protected void ChangeState(PLAYERSTATE newState)
    {
        if (currentState == newState) return;

        Debug.Log($"상태 변경: {currentState} -> {newState}");
        currentState = newState;
    }

    // 캐릭터 타입 enum
    protected enum CHAR
    {
        DOG,
        CAT
    }

    [Header("캐릭터 타입")]
    [SerializeField] protected CHAR enumChar;

    protected void ControlKey()
    {
        switch (enumChar)
        {
            case CHAR.DOG:
                leftKey = KeyCode.LeftArrow;
                rightKey = KeyCode.RightArrow;
                jumpKey = KeyCode.UpArrow;
                skillKey = KeyCode.DownArrow;
                break;

            case CHAR.CAT:
                leftKey = KeyCode.A;
                rightKey = KeyCode.D;
                jumpKey = KeyCode.W;
                skillKey = KeyCode.S;
                break;
        }
    }

    /*---------------------------------------캐릭터 이동 관련---------------------------------------*/

    protected float moveX;
    protected float moveY;
    protected Vector2 moveInput;

    [Header("이름")]
    [SerializeField] protected string Name = string.Empty;

    [Header("조작 키 설정")]
    [SerializeField] protected KeyCode leftKey;
    [SerializeField] protected KeyCode rightKey;
    [SerializeField] protected KeyCode jumpKey;
    [SerializeField] protected KeyCode skillKey;
    [SerializeField] protected int moveSpeed = 5;

    [Header("점프 체크 관련 설정")]
    [SerializeField] protected Transform groundCheck;      // 땅 체크용 기준 위치
    [SerializeField] protected float groundRayRange = 0.1f; // 바닥 체크 레이 범위
    [SerializeField] protected LayerMask groundLayer;      // 바닥으로 인식할 레이어
    [SerializeField] protected float jumpPower = 5f;       // 점프 파워
    [SerializeField] protected int maxJumpCount = 2;       // 최대 점프 횟수
    protected int currentJumpCount = 0;

    [Header("헬멧 착용 위치")]
    [SerializeField] protected Transform headPivot;
    protected GameObject helmet;

    [Header("스킬 쿨타임")]
    [SerializeField] protected float skillCoolTime = 1f;

    /*---------------------------------------단발형 스킬 처리---------------------------------------*/
    protected bool skillReq = false;

    // 단발형 스킬 입력 감지
    protected virtual void InstantSkillCall()
    {
        if (Input.GetKeyDown(skillKey) && skillReady)
        {
            StartCoroutine(SkillCoolDown(skillCoolTime));
            skillReq = true;
            Debug.Log($"{Name} 스킬 키 입력");
        }
    }

    // 단발형 스킬 실행
    protected virtual void InstantSkillActivate()
    {
        if (skillReq)
        {
            skillReq = false;
            InstantSkill();
            Debug.Log($"{Name} 스킬 발동");
        }
    }

    protected virtual void InstantSkill() { }

    /*---------------------------------------토글형 스킬 처리---------------------------------------*/
    protected bool isToggled = false;

    // 토글형 스킬 입력 감지
    protected virtual void ToggleSkillCall()
    {
        if (Input.GetKeyDown(skillKey) && !isToggled)
        {
            StartCoroutine(SkillCoolDown(skillCoolTime));
            isToggled = true;
            ToggleSkillOn();
            Debug.Log($"{Name} 스킬 키 입력");
            Debug.Log($"{Name} 토글 스킬 ON");
        }
        else if (Input.GetKeyDown(skillKey) && isToggled)
        {
            isToggled = false;
            ToggleSkillOff();
            Debug.Log($"{Name} 스킬 키 입력");
            Debug.Log($"{Name} 토글 스킬 OFF");
        }
    }

    protected virtual void ToggleSkillOn() { }
    protected virtual void ToggleSkillOff() { }

    /*---------------------------------------쿨타임 처리---------------------------------------*/
    protected bool skillReady = true;

    protected virtual IEnumerator SkillCoolDown(float cooldown)
    {
        Debug.Log("쿨타임 시작");
        cooldown = skillCoolTime;
        skillReady = false;
        yield return new WaitForSeconds(cooldown);
        skillReady = true;
        Debug.Log("쿨타임 종료");
    }

    /*---------------------------------------애니메이션 처리---------------------------------------*/
    protected virtual void HandleJumpAnim()
    {
        bool isJump = currentJumpCount > 0 || (currentJumpCount == 0 && !IsGrounded());
        Anim.SetJump(isJump);
    }

    protected virtual void HandleSkillAnim()
    {
        bool isSkill = skillReq;
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
    /// 캐릭터 좌우 반전 (flipX 처리)
    /// </summary>
    protected virtual void SpriteFlip()
    {
        if (moveX != 0)
            Anim.SetFlip(moveX < 0);

        if (moveX != 0 && headPivot != null)
            headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f);
    }

    /*---------------------------------------점프 관련 함수---------------------------------------*/
    protected bool jumpReq = false;

    protected virtual bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundRayRange, groundLayer);
        return hit.collider != null;
    }

    /// <summary> 이동 입력 감지 </summary>
    protected virtual void MoveCall()
    {
        moveX = 0;
        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    /// <summary> 이동 처리 </summary>
    protected virtual void MoveActivate(Vector2 input)
    {
        Vector2 velocity = rb.velocity;
        velocity.x = input.x * moveSpeed;
        rb.velocity = velocity;
    }

    /// <summary> 점프 입력 감지 </summary>
    protected virtual void JumpCall()
    {
        if (Input.GetKeyDown(jumpKey) && currentJumpCount < maxJumpCount)
        {
            if (currentJumpCount == 0 && !IsGrounded())
                return;

            jumpReq = true;
        }
    }

    protected virtual void JumpAtivate()
    {
        if (jumpReq)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            if (currentJumpCount < maxJumpCount)
            {
                currentJumpCount++;
                Debug.Log($"{Name} 점프 {currentJumpCount}/{maxJumpCount}");
            }
            jumpReq = false;
        }
    }

    protected void CheckLanding()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0 && IsGrounded())
        {
            currentJumpCount = 0;
            Anim.SetJump(false);
            Debug.Log($"{Name} 착지!");
        }
    }

    /*---------------------------------------기타---------------------------------------*/

    // 바닥 체크 Ray를 Scene에서 시각화
    protected virtual void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundRayRange);
    }
}
