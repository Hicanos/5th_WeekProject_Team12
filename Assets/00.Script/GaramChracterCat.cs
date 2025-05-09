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
public class GaramCharacterCat : Characterbase
{
    /// <summary> �������� ���� ������ �ν��Ͻ� (�̱���) </summary>
    public static GaramCharacterCat Instance { get; private set; }

    // �Է°� ����
    private float moveX;
    private float moveY;
    private Vector2 moveInput;

    // ���� ȸ�� ������ (����ǹ�)
    [Header("��� �ǹ�")]
    [SerializeField] private Transform headPivot;

    private float lastJumpTime = -999f; // ������ ���� �ð� ���
    [Header("Ⱦ��ũ�� ���� ��� ���� �ý���")]
    [SerializeField] private Transform groundCheck;      // �ٴ� ���� ��ġ
    [SerializeField] private float groundCheckRadius = 0.1f; // �ٴ� üũ ����
    [SerializeField] private LayerMask groundLayer;      // �ٴ� ���̾�
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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    /// <summary>
    /// Update: �����Ӹ��� ȣ��, �Է� ó�� �� �ִϸ��̼� ���� ���
    /// </summary>
    private void Update()
    {
        HandleInput();         // �̵� �Է� ����
        HandleAnimation();     // �̵� �ִϸ��̼� ó��
        HandleSpriteFlip();    // ĳ���� �¿� ����


    }

    /// <summary>
    /// FixedUpdate: ���� �ð����� ȣ��Ǹ�, ���� ��� �̵� ó��
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 input = new Vector2(moveX, moveY);


        Move(input); // BaseController�� �̵� ó�� ȣ��
        HandleJump();   // ���� �Է� ó��
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0)
        {
            currentJumpCount = 0;
            Anim.SetJump(false);
            Debug.Log("����!");
        }

    }

    /// <summary>
    /// �̵� Ű �Է��� �޾� ���� ���� ���
    /// </summary>
    private void HandleInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
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
    private void HandleSpriteFlip()
    {
        if (moveX != 0)
            Anim.SetFlip(moveX < 0);
        if (moveX != 0)
            headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f); // flipX ȿ��
    }


    /// <summary>
    /// ���� Ű �Է� ���� �� ��Ÿ�� Ȯ�� �� ���� ����
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
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Y �ӵ� �ʱ�ȭ
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                currentJumpCount++;
                Anim.SetJump(true);
                Debug.Log($"���� {currentJumpCount}/{maxJumpCount}");
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
    //���� �ٱ����� �ݶ��̴� ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
        {

        }
    }

}