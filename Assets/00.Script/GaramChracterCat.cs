using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 플레이어 전용 컨트롤러
/// - 입력 처리
/// - 이동/점프/공격 실행
/// - 무기 회전 및 캐릭터 방향 반전
/// - 애니메이션 연동
/// - 싱글톤으로 인스턴스 유지
/// </summary>
public class GaramCharacterCat : Characterbase
{
    /// <summary> 전역에서 접근 가능한 인스턴스 (싱글톤) </summary>
    public static GaramCharacterCat Instance { get; private set; }

    // 입력값 저장
    private float moveX;
    private float moveY;
    private Vector2 moveInput;

    // 무기 회전 기준점 (헬멧피벗)
    [Header("헬멧 피벗")]
    [SerializeField] private Transform headPivot;

    private float lastJumpTime = -999f; // 마지막 점프 시간 기록
    [Header("횡스크롤 물리 기반 점프 시스템")]
    [SerializeField] private Transform groundCheck;      // 바닥 판정 위치
    [SerializeField] private float groundCheckRadius = 0.1f; // 바닥 체크 범위
    [SerializeField] private LayerMask groundLayer;      // 바닥 레이어
    [SerializeField] private float jumpPower = 5f;       // 점프 힘
    [SerializeField] private int maxJumpCount = 1;
    private int currentJumpCount = 0;



    private void Start()
    {

    }


    /// <summary>
    /// Awake: 컴포넌트 초기화 및 싱글톤 설정
    /// </summary>
    protected override void Awake()
    {
        base.Awake();


        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴 방지
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    /// <summary>
    /// Update: 프레임마다 호출, 입력 처리 및 애니메이션 제어 담당
    /// </summary>
    private void Update()
    {
        HandleInput();         // 이동 입력 감지
        HandleAnimation();     // 이동 애니메이션 처리
        HandleSpriteFlip();    // 캐릭터 좌우 반전


    }

    /// <summary>
    /// FixedUpdate: 고정 시간마다 호출되며, 물리 기반 이동 처리
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 input = new Vector2(moveX, moveY);


        Move(input); // BaseController의 이동 처리 호출
        HandleJump();   // 점프 입력 처리
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0)
        {
            currentJumpCount = 0;
            Anim.SetJump(false);
            Debug.Log("착지!");
        }

    }

    /// <summary>
    /// 이동 키 입력을 받아 방향 벡터 계산
    /// </summary>
    private void HandleInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveInput = new Vector2(moveX, moveY).normalized;
    }
    /// <summary>
    /// 이동 중 여부에 따라 애니메이션 파라미터 설정
    /// </summary>
    private void HandleAnimation()
    {
        bool isMoving = moveX != 0;
        Anim.SetMove(isMoving);
    }




    /// <summary>
    /// 캐릭터 좌우 반전 (flipX) 처리
    /// 무기에는 적용하지 않음
    /// </summary>
    private void HandleSpriteFlip()
    {
        if (moveX != 0)
            Anim.SetFlip(moveX < 0);
        if (moveX != 0)
            headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f); // flipX 효과
    }


    /// <summary>
    /// 점프 키 입력 감지 및 쿨타임 확인 후 점프 실행
    /// </summary>
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.C) && IsGrounded())
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

    protected override void Move(Vector2 input)
    {
        base.Move(input);

        if (CanWallClimb())
        {
          /*  rb.velocity = new Vector2(rb.velocity.x, input.y * climbSpeed);*/
        }
    }

    bool CanWallClimb() { return true; }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    //벽에 붙기위한 콜라이더 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
        {

        }
    }

}