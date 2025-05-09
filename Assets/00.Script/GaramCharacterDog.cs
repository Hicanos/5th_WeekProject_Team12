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
public class GaramCharacterDog : Characterbase
{
   /* /// <summary> 전역에서 접근 가능한 인스턴스 (싱글톤) </summary>
    public static GaramCharacterDog Instance { get; private set; }*/
   
    [Header("조작키 설정")]
    [SerializeField] private KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rightKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode jumpKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode SkillKey = KeyCode.DownArrow;
    // 입력값 저장
    private float moveX;
    private float moveY;
    private Vector2 moveInput;

    // 무기 회전 기준점 (헬멧피벗)
    [Header("헬멧 피벗")]
    [SerializeField] private Transform headPivot;

   
    [Header("횡스크롤 물리 기반 점프 시스템")]
   /* [SerializeField] private Transform groundCheck;      // 바닥 판정 위치
    [SerializeField] private float groundCheckRadius = 0.1f; // 바닥 체크 범위
    [SerializeField] private LayerMask groundLayer;      // 바닥 레이어*/
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
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴 방지
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }*/
    }

    /// <summary>
    /// Update: 프레임마다 호출, 입력 처리 및 애니메이션 제어 담당
    /// </summary>
    private void Update()
    {
        HandleInput();         // 이동 입력 감지
        HandleAnimation();     // 이동 애니메이션 처리
        HeadSpriteFlip();    // 캐릭터 좌우 반전

        Vector2 input = new Vector2(moveX, moveY);

      /*  bool gr = IsGrounded();*/
        Move(input); // BaseController의 이동 처리 호출
        HandleJump();   // 점프 입력 처리
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0 /*&& gr*/)
        {
            currentJumpCount = 0;
            Anim.SetJump(false);
            Debug.Log("착지!");
        }
       
        

      /*  if (gr) { 
            Debug.Log(""); }*/
    }
        

    /// <summary>
    /// FixedUpdate: 고정 시간마다 호출되며, 물리 기반 이동 처리
    /// </summary>
    private void FixedUpdate()
    {
        
               
    }

    /// <summary>
    /// 이동 키 입력을 받아 방향 벡터 계산
    /// </summary>
    private void HandleInput()
    {
        moveX = 0;

        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
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
    private void HeadSpriteFlip()
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
    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) /*&& IsGrounded()*/)
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

    /*   //점프 착지 판정용 불값 필요 시 사용
       private bool IsGrounded()
       {*//*Debug.Log($"ground");*//*
           return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
           */
    /*  //OverlapCircle 시각화 디버깅용 
  private void OnDrawGizmosSelected()
  {
      Debug.Log("null");
      if (groundCheck == null) return;

      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
      //Debug.Log("기즈모");
  }*/
    //벽에 붙기위한 콜라이더 감지
    /*  private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.CompareTag("wall"))
          {

          }
      }*/
}


