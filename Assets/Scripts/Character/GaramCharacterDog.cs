using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
//상태머신, 구조체사용 , 델리게이트 사용 
public class GaramCharacterDog : Characterbase
{
    [Header("강아지 돌진")]
    
        [SerializeField] private LayerMask destroyLayer;//파괴할 오브젝트 레이어
        [SerializeField] private float dashSpeed = 30f;//돌진 속도(힘)
        [SerializeField] private float dashDuration = 0.2f;//돌진지속 시간
    

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
    {  //대시 중 움직임을 막을 것이기 때문에  입력 방지( 호출을 위해 true만든 bool값을 false로 바꾸지 못하게 됨 )
        if (isDash)
        {
            return;
        }
        else
        {
            InstantSkillCall();
            JumpCall();
            MoveCall();
        }
    }
    /// <summary>
    /// 행동매서드는 여기에
    /// </summary>
    private void FixedUpdate()
    {
        HandleJumpAnim();
        CheckLanding();
        //대시중에 rb.velocity값에 간섭을 주기때문에 Update단계에서 막기 
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
    /*------------------------------------------------------------------------------*/
    // 강아지 스킬발동중을 감지할 불값
    private bool isDash = false;
    //착지상태일때만 스킬 사용 가능 하게 하기 위해 재정의0
    protected override void InstantSkillCall()
    {

        if (IsGrounded() && currentJumpCount == 0)
            base.InstantSkillCall();

    }
    //스킬 재정의
    protected override void InstantSkill()
    {
        //방향을 잡기위한 백터값 moveX이용하면 이동중이 아닐땐 0이라서 생각중.나중에 하드코딩 피하게 해보기 
        Vector2 currentLook; 
        if (this.spriteRenderer.flipX == false)
        {
            currentLook = new Vector2(1, 0);
        }
        else
        {
            currentLook = new Vector2(-1, 0);
        }
        rb.velocity = Vector2.zero; //  속도 초기화
        rb.position -= currentLook * 0.1f;// 딱 붙어서 사용할 시 Stay상태라 파괴되지 않으므로 Stay 하나더 만드는 것 보다 연출로 처리하기로 함
        //코루틴 시작 뒤에 매개변수 2개 인스펙터창 조정 가능
        StartCoroutine(DashDuration(currentLook, dashDuration, dashSpeed));

    }

    //대시 연출을 위한 코루틴 
    private IEnumerator DashDuration(Vector2 direction, float duration, float dashSpeed)
    {
        isDash = true;
        
        yield return new WaitForSeconds(0.1f);
        // 순간적으로 빠른 속도 설정
        rb.velocity = direction * dashSpeed;  

        // 딱 duration만큼만 이동
        yield return new WaitForSeconds(duration); 

        // 즉시 멈추기
        rb.velocity = Vector2.zero;
        isDash = false;
    }

    //레이어 감지하여 충돌 오브젝트 파괴하는 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 돌진 중이 아닐 때는 무시
        if (!isDash) return;

        // 레이어 체크: 감지할 레이어에 해당하는지 확인
        if (((1 << other.gameObject.layer) & destroyLayer) != 0)
        {
            Destroy(other.gameObject);
            HandleCrashAnim();
           //한개의 오브젝트 충돌하여 파괴하였을 시 즉시 멈추게 하기위한 구문 
            isDash = false;
            rb.velocity = Vector2.zero;

            Debug.Log(" 돌진으로 파괴됨: " + other.name);
        }
    }
    //지역변수 변경을 위해 재정의
    protected override void HandleSkillAnim()
    {
        bool isSkill = isDash;
        Anim.SetSkill(isSkill);
    }

}




