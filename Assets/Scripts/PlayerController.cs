using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    public event System.Action OnPlayerRespawn;
    public Vector3 respawnPosition;
    public int maxLives = 3;
    private int currentLives;
    private Vector3 lastDeathPosition;

    public GameObject deathPanelMenu;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentLives = maxLives;
    }

    private void Update()
    {
        if (!IsPlayerDead())
        {
            HandleMovement();
        }
        else
        {
            ToggleDeathPanelMenu(true);
        }
    }

    private void HandleMovement()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position + new Vector3(input.x, input.y, 0f).normalized * 0.1f; // Adjust offset here

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
                else
                {
                    UnityEngine.Debug.Log("Collision detected. Target position not walkable.");
                }
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        float distance = direction.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("SolidObjects"));

        if (hit.collider != null)
        {
            UnityEngine.Debug.Log("Player collided with a solid object. Movement blocked.");
            return false;
        }

        Collider2D collider = Physics2D.OverlapCircle(targetPos, 0.2f);

        if (collider != null && collider.CompareTag("Enemy"))
        {
            UnityEngine.Debug.Log("Player collided with an enemy.");
            HandlePlayerDeath();
            return false;
        }

        return collider == null || collider.isTrigger || collider.gameObject != gameObject;
    }

    private System.Collections.IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        ResetMovement();
    }

    private void ResetMovement()
    {
        isMoving = false;
    }

    private bool IsPlayerDead()
    {
        return currentLives <= 0;
    }

    private void ToggleDeathPanelMenu(bool show)
    {
        if (deathPanelMenu != null)
        {
            deathPanelMenu.SetActive(show);
        }

        if (show)
        {
            UnityEngine.Debug.Log("Player is dead. Lives remaining: " + currentLives);

            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HandlePlayerDeath();
        }
    }

    private void HandlePlayerDeath()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            ToggleDeathPanelMenu(true);
        }
        else
        {
            Respawn();
        }
    }
    public void AddLives(int numLivesToAdd)
    {
        currentLives += numLivesToAdd;
        UnityEngine.Debug.Log("Added " + numLivesToAdd + " lives. Total lives: " + currentLives);
    }

    public void Respawn()
    {
        if (currentLives > 0)
        {
            FindObjectOfType<EnemySpawner>().DestroyExistingEnemies();

            transform.position = respawnPosition;

            UnityEngine.Debug.Log("Player respawned. Lives remaining: " + currentLives);

            Time.timeScale = 1f;

            OnPlayerRespawn?.Invoke();

            lastDeathPosition = Vector3.zero;
        }
    }
}


