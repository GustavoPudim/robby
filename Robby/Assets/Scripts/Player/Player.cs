using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Attributes")]
    public float speed = 5.0f;
    public float pickItemsRange = 1.6f;
    public float attackTime = 5 / 12.0f;
    public float attackRange = 1.6f;

    [Space]

    [Header("References")]
    public Rigidbody2D rigidbody;
    public Animator animator;
    public Inventory inventory;
    public GameObject pauseMenu;

    private Vector2 movement;
    private bool punch;

    public static bool isPaused = false;

    public bool isInventoryOpen
    {
        get { return inventory.gameObject.activeSelf; }
    }

    public bool isRunning 
    {
        get { return !isPaused && !isInventoryOpen; }
    }

    private Controls controls;
    private Enums.Direction direction = Enums.Direction.Down;

    private void Awake()
    {
        controls = Manager.controls;
        controls.Player.Attack.performed += ctx => Util.InvokeIf(Attack, isRunning);
        controls.Player.Attack.canceled += ctx => StartCoroutine(StopAttack());
        controls.Player.Pick.performed += ctx => Util.InvokeIf(PickItem, isRunning);
        controls.Player.ToggleInventory.performed += ctx => Util.InvokeIf(ToggleInventory, !isPaused);
        controls.Player.TogglePauseMenu.performed += ctx => TogglePause();
        controls.Player.UseItem.performed += ctx => Util.InvokeIf(inventory.useItem, isInventoryOpen);
    }

    private void Start()
    {
        // Initialize Inventory (makes sure all slots run the awake method)
        inventory.gameObject.SetActive(true);
        inventory.gameObject.SetActive(false);
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;

        if (isRunning)
        {
            movement = controls.Player.Move.ReadValue<Vector2>();
            Animate();
        }
        
        rigidbody.velocity = movement * speed;
    }

    public void Attack()
    {
        animator.SetBool("Punch", true);
        InvokeRepeating("Damage", attackTime, attackTime);
    }

    public IEnumerator StopAttack()
    {
        animator.SetBool("Punch", false);
        yield return new WaitForSeconds(attackTime);
        CancelInvoke("Damage");

        yield return null;
    }
    
    void Damage()
    {
        Vector2 punchDirection = Vector2.zero;

        switch(direction)
        {
            case Enums.Direction.Right:
                punchDirection = Vector2.right;
                break;
            case Enums.Direction.Left:
                punchDirection = Vector2.left;
                break;
            case Enums.Direction.Down:
                punchDirection = Vector2.down;
                break;
            case Enums.Direction.Up:
                punchDirection = Vector2.up;
                break;
            default:
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, punchDirection, attackRange, 1 << 8);
        if (hit) hit.transform.gameObject.GetComponent<Block>().Damage(0.5f);
    }

    public void ChangeDirection(Enums.Direction dir)
    {
        direction = dir;
    }

    void Animate()
    {
        if (movement.sqrMagnitude > 0.1f)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    public GameObject GetClosestDrop()
    {
        Collider2D[] drops = Physics2D.OverlapCircleAll(gameObject.transform.position, pickItemsRange, 1 << 9);

        float smallerDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (Collider2D drop in drops)
        {
            float distance = Vector3.Distance(drop.transform.position, transform.position);
            if (distance > smallerDistance) break;

            closestObject = drop.gameObject;
            smallerDistance = distance;
        }

        return closestObject;
    }

    public void PickItem()
    {
        if (!GetClosestDrop()) return;
        
        string itemId = GetClosestDrop().GetComponent<ItemEntity>().id;

        inventory.AddItem(itemId);
        Destroy(GetClosestDrop());
    }

    public void ToggleInventory()
    {
        inventory.gameObject.SetActive(!isInventoryOpen);
        
    }

    public void TogglePause()
    {
        if(isInventoryOpen)
        {
            ToggleInventory();
            return;
        }

        pauseMenu.gameObject.SetActive(!isPaused);
        isPaused = pauseMenu.gameObject.activeSelf;
    }
}
