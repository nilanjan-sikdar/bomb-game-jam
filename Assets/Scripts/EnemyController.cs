using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement (Left/Right Only)")]
    public float speed = 3f;
    public float patrolDistance = 4f;

    [Header("Vision (Looking DOWN)")]
    public Transform eyePivot;
    public float viewDistance = 5f;

    // CHANGED: We renamed this to hitLayers so it matches your logic below
    // Set this to "Everything" in Inspector so walls block the laser
    public LayerMask hitLayers;

    public float scanSpeed = 3f;
    public float scanWidth = 45f;

    private Vector3 startPos;
    private Rigidbody2D rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        PerformPatrol();
        ScanDownwards();
    }

    void PerformPatrol()
    {
        float x = startPos.x + Mathf.PingPong(Time.time * speed, patrolDistance * 2) - patrolDistance;
        transform.position = new Vector3(x, startPos.y, transform.position.z);
    }

    void ScanDownwards()
    {
        // Calculate Angle
        float angleOffset = Mathf.Sin(Time.time * scanSpeed) * scanWidth;
        float finalAngle = 360f + angleOffset; // -90 is DOWN in Unity 2D

        eyePivot.localRotation = Quaternion.Euler(0, 0, finalAngle);

        // --- FIXED LOGIC STARTS HERE ---

        // 1. Single Raycast (Removed the duplicate 'hit' variable)
        RaycastHit2D hit = Physics2D.Raycast(eyePivot.position, eyePivot.right, viewDistance, hitLayers);

        // 2. Check what we hit
        if (hit.collider != null)
        {
            // Only kill if the thing we hit is tagged "Player"
            if (hit.collider.CompareTag("Player"))
            {
                // Ensure this matches your actual script name (SimplePlayer vs Movement)
                Movement playerScript = hit.collider.GetComponent<Movement>();

                if (playerScript != null)
                {
                    playerScript.Die();
                }
            }
        }

        Debug.DrawRay(eyePivot.position, eyePivot.right * viewDistance, Color.red);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = Application.isPlaying ? startPos : transform.position;
        Gizmos.DrawLine(new Vector3(center.x - patrolDistance, center.y, 0), new Vector3(center.x + patrolDistance, center.y, 0));
    }
}