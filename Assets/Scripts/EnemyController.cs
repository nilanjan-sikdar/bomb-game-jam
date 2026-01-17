using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement (Left/Right Only)")]
    public float speed = 3f;
    public float patrolDistance = 4f;

    [Header("Vision (Looking DOWN)")]
    public Transform eyePivot;
    public float viewDistance = 5f;

    // Set this to "Everything" in Inspector so walls block the laser
    public LayerMask hitLayers;

    public float scanSpeed = 3f;
    public float scanWidth = 45f;

    // --- NEW: Add the Line Renderer Variable ---
    public LineRenderer laserLine;

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

        // --- NEW: Setup the Line defaults (optional but safe) ---
        if (laserLine != null)
        {
            laserLine.positionCount = 2; 
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
        float finalAngle = 360f + angleOffset; 

        eyePivot.localRotation = Quaternion.Euler(0, 0, finalAngle);
                
        // 1. Single Raycast 
        RaycastHit2D hit = Physics2D.Raycast(eyePivot.position, eyePivot.right, viewDistance, hitLayers);

        // --- NEW: UPDATE THE VISUAL LASER ---
        if (laserLine != null)
        {
            // Start the line at the eye
            laserLine.SetPosition(0, eyePivot.position);

            if (hit.collider != null)
            {
                // If we hit something (Wall or Player), stop the laser there
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                // If we hit nothing, shoot the laser to max distance
                laserLine.SetPosition(1, eyePivot.position + eyePivot.right * viewDistance);
            }
        }
        // ------------------------------------

        // 2. Check what we hit
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
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