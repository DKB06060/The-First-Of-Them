using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 input;
    public Vector2 lastMoveDir;

    public bool allowMovement = true;
    public bool isMoving = false;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (allowMovement)
        {

            // Get raw input
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
        }

        // Normalize so diagonals aren't faster
        input = input.normalized;

        isMoving = input != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            lastMoveDir = input;
            animator.SetFloat("MoveX", input.x);
            animator.SetFloat("MoveY", input.y);
        }
        else
        {
            // Keep last direction for idle animations
            animator.SetFloat("MoveX", lastMoveDir.x);
            animator.SetFloat("MoveY", lastMoveDir.y);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
}