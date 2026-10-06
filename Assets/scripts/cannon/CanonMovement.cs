using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CanonMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 4f;
    public float jumpingPower = 12f;
    public bool isGrounded;

    public Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Reference to the CannonShooter script
    [SerializeField] private CannonShooter cannonShooter;

    // Wall tags
    private string leftWallTag = "wall 1";
    private string rightWallTag = "wall 2";

    // Coin and diamond counters
    private int coinCount = 0;
    private int diamondCount = 0;

    // TextMeshProUGUI references for displaying the counts (assigned in the Inspector)
    public TextMeshProUGUI coinCounterText;
    public TextMeshProUGUI diamondCounterText;

    // Touch button references
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button jumpButton;
    public Button shootButton;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        // Assign touch button event listeners for movement
        if (moveLeftButton != null)
        {
            EventTrigger leftTrigger = moveLeftButton.gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(leftTrigger, EventTriggerType.PointerDown, OnMoveLeftPressed);
            AddEventTrigger(leftTrigger, EventTriggerType.PointerUp, OnMoveLeftReleased);
        }

        if (moveRightButton != null)
        {
            EventTrigger rightTrigger = moveRightButton.gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(rightTrigger, EventTriggerType.PointerDown, OnMoveRightPressed);
            AddEventTrigger(rightTrigger, EventTriggerType.PointerUp, OnMoveRightReleased);
        }

        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(OnJumpPressed);
        }

        if (shootButton != null)
        {
            shootButton.onClick.AddListener(OnShootPressed);
        }

        // Initialize the UI text for coins and diamonds
        UpdateCoinText();
        UpdateDiamondText();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Handle keyboard jumping
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            Jump();
        }

        // Handle releasing jump while moving up
        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Handle keyboard shooting
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnShootPressed();
        }

        // Update touch movement
        if (isMovingLeft)
        {
            horizontal = -1f;
        }
        else if (isMovingRight)
        {
            horizontal = 1f;
        }

        isGrounded = IsGrounded();

        // Update animation based on movement
        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        // Moving the player and preventing it from going out of bounds
        Vector2 newVelocity = new Vector2(horizontal * speed, rb.velocity.y);
        rb.velocity = CheckWallCollision(newVelocity);
    }

    // Prevent player from moving through the walls
    private Vector2 CheckWallCollision(Vector2 velocity)
    {
        Vector2 newPosition = rb.position + velocity * Time.fixedDeltaTime;

        // Check for collision with left and right walls
        RaycastHit2D hitLeft = Physics2D.Raycast(rb.position, Vector2.left, 0.1f, LayerMask.GetMask(leftWallTag));
        RaycastHit2D hitRight = Physics2D.Raycast(rb.position, Vector2.right, 0.1f, LayerMask.GetMask(rightWallTag));

        if (hitLeft.collider != null && velocity.x < 0) // Collision with left wall
        {
            velocity.x = 0;
        }
        else if (hitRight.collider != null && velocity.x > 0) // Collision with right wall
        {
            velocity.x = 0;
        }

        return velocity;
    }

    // Handle touch button callbacks
    public void OnMoveLeftPressed()
    {
        isMovingLeft = true;
    }

    public void OnMoveLeftReleased()
    {
        isMovingLeft = false;
    }

    public void OnMoveRightPressed()
    {
        isMovingRight = true;
    }

    public void OnMoveRightReleased()
    {
        isMovingRight = false;
    }

    public void OnJumpPressed()
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    public void OnShootPressed()
    {
        ShootCannon();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            audioManager.PlaySFX(audioManager.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isGrounded = false;
        }
    }

    private void ShootCannon()
    {
        if (cannonShooter != null)
        {
            cannonShooter.ShootCannonball(); // Use the CannonShooter's ShootCannonball function
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    // Helper function to add event triggers for touch buttons
    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, UnityEngine.Events.UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinText();
            audioManager.PlaySFX(audioManager.coinsCollecting);
            Destroy(collision.gameObject); // Destroy the coin
        }
        else if (collision.gameObject.CompareTag("Diamond"))
        {
            diamondCount++;
            UpdateDiamondText();
            audioManager.PlaySFX(audioManager.coinsCollecting);
            Destroy(collision.gameObject); // Destroy the diamond
        }
    }
    // Update the coin count text
    private void UpdateCoinText()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = coinCount.ToString();
        }
    }

    // Update the diamond count text
    private void UpdateDiamondText()
    {
        if (diamondCounterText != null)
        {
            diamondCounterText.text = diamondCount.ToString();
        }
    }
}
