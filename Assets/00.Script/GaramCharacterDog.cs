using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �÷��̾� ���� ��Ʈ�ѷ�
/// - �Է� ó��
/// - �̵�/����/���� ����
/// - ���� ȸ�� �� ĳ���� ���� ����
/// - �ִϸ��̼� ����
/// - �̱������� �ν��Ͻ� ����
/// </summary>
public class GaramCharacterDog : Characterbase
{
   /* /// <summary> �������� ���� ������ �ν��Ͻ� (�̱���) </summary>
    public static GaramCharacterDog Instance { get; private set; }*/
   
    [Header("����Ű ����")]
    [SerializeField] private KeyCode leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rightKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode jumpKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode SkillKey = KeyCode.DownArrow;
    // �Է°� ����
    private float moveX;
    private float moveY;
    private Vector2 moveInput;

    // ���� ȸ�� ������ (����ǹ�)
    [Header("��� �ǹ�")]
    [SerializeField] private Transform headPivot;

   
    [Header("Ⱦ��ũ�� ���� ��� ���� �ý���")]
   /* [SerializeField] private Transform groundCheck;      // �ٴ� ���� ��ġ
    [SerializeField] private float groundCheckRadius = 0.1f; // �ٴ� üũ ����
    [SerializeField] private LayerMask groundLayer;      // �ٴ� ���̾�*/
    [SerializeField] private float jumpPower = 5f;       // ���� ��
    [SerializeField] private int maxJumpCount = 1;
    private int currentJumpCount = 0;



    private void Start()
    {

    }


    /// <summary>
    /// Awake: ������Ʈ �ʱ�ȭ �� �̱��� ����
    /// </summary>
    protected override void Awake()
    {
        base.Awake();


        // �̱��� �ν��Ͻ� ����
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }*/
    }

    /// <summary>
    /// Update: �����Ӹ��� ȣ��, �Է� ó�� �� �ִϸ��̼� ���� ���
    /// </summary>
    private void Update()
    {
        HandleInput();         // �̵� �Է� ����
        HandleAnimation();     // �̵� �ִϸ��̼� ó��
        HeadSpriteFlip();    // ĳ���� �¿� ����

        Vector2 input = new Vector2(moveX, moveY);

      /*  bool gr = IsGrounded();*/
        Move(input); // BaseController�� �̵� ó�� ȣ��
        HandleJump();   // ���� �Է� ó��
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0 /*&& gr*/)
        {
            currentJumpCount = 0;
            Anim.SetJump(false);
            Debug.Log("����!");
        }
       
        

      /*  if (gr) { 
            Debug.Log(""); }*/
    }
        

    /// <summary>
    /// FixedUpdate: ���� �ð����� ȣ��Ǹ�, ���� ��� �̵� ó��
    /// </summary>
    private void FixedUpdate()
    {
        
               
    }

    /// <summary>
    /// �̵� Ű �Է��� �޾� ���� ���� ���
    /// </summary>
    private void HandleInput()
    {
        moveX = 0;

        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
        moveInput = new Vector2(moveX, moveY).normalized;
    }
    /// <summary>
    /// �̵� �� ���ο� ���� �ִϸ��̼� �Ķ���� ����
    /// </summary>
    private void HandleAnimation()
    {
        bool isMoving = moveX != 0;
        Anim.SetMove(isMoving);
    }




    /// <summary>
    /// ĳ���� �¿� ���� (flipX) ó��
    /// ���⿡�� �������� ����
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
    /// ���� Ű �Է� ���� �� ��Ÿ�� Ȯ�� �� ���� ����
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
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Y �ӵ� �ʱ�ȭ
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                currentJumpCount++;
                Anim.SetJump(true);
                Debug.Log($"���� {currentJumpCount}/{maxJumpCount}");
            }

            
        }
    }

    /*   //���� ���� ������ �Ұ� �ʿ� �� ���
       private bool IsGrounded()
       {*//*Debug.Log($"ground");*//*
           return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
           */
    /*  //OverlapCircle �ð�ȭ ������ 
  private void OnDrawGizmosSelected()
  {
      Debug.Log("null");
      if (groundCheck == null) return;

      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
      //Debug.Log("�����");
  }*/
    //���� �ٱ����� �ݶ��̴� ����
    /*  private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.CompareTag("wall"))
          {

          }
      }*/
}


