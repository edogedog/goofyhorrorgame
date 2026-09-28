using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public Animator anim;
    public Transform player;
    
    [Header("Settings")]
    [SerializeField] float pathUpdateInterval = 0.5f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float chaseRange = 10f;
    
    [Space(5)]
    [Header("Navigation")]
    [SerializeField] float navMeshHeightSampleDistance = 10f;
    [SerializeField] int navMeshAreaMask = 0;
    
    [Space(5)]
    [Header("Elevation")]
    [SerializeField] float maxElevationGap = 3f;
    [SerializeField] bool chaseOnGroundWhenElevated = true;
    
    float lastPathTime;
    int lastPagesCollected;
    bool gameEnded;
    private bool isInitialized = false;
    
    // Cache for performance
    private NavMeshAgent cachedAi;
    private Animator cachedAnim;
    private Transform cachedPlayer;
    private Vector3 lastPlayerPosition = Vector3.zero;
    private float lastNavMeshSampleTime = 0f;
    
    void Start()
    {
        // Cache components for performance
        cachedAi = ai;
        cachedAnim = anim;
        cachedPlayer = player;
        
        // Initialize last values
        lastPagesCollected = pickupLetter.pagesCollected;
        isInitialized = true;
    }

    void Update()
    {
        // Early exit if components are not properly set or not initialized
        if (cachedAi == null || cachedAnim == null || cachedPlayer == null || !isInitialized)
            return;
        
        // Track pages collected to update speed only when it changes
        int currentPages = pickupLetter.pagesCollected;
        if (currentPages != lastPagesCollected)
        {
            lastPagesCollected = currentPages;
            UpdateSpeed(currentPages);
        }
        
        // Throttle path updates to every pathUpdateInterval seconds
        if (Time.time - lastPathTime < pathUpdateInterval)
            return;
        
        lastPathTime = Time.time;
        
        float distanceToPlayer = Vector3.Distance(transform.position, cachedPlayer.position);
        
        if (distanceToPlayer < attackRange)
        {
            HandlePlayerDeath();
            return;
        }
        
        if (distanceToPlayer < chaseRange)
        {
            Vector3 playerNavPos = GetPlayerNavMeshPosition();
            cachedAi.SetDestination(playerNavPos);
        }
    }
    
    Vector3 GetPlayerNavMeshPosition()
    {
        Vector3 playerPos = cachedPlayer.position;
        Vector3 monsterPos = transform.position;
        
        // Only sample navmesh every few seconds to reduce overhead
        if (Time.time - lastNavMeshSampleTime > 0.5f)
        {
            NavMeshHit hit;
            
            if(NavMesh.SamplePosition(playerPos, out hit, navMeshHeightSampleDistance, navMeshAreaMask))
            {
                float elevationDiff = Mathf.Abs(hit.position.y - monsterPos.y);
                
                if(chaseOnGroundWhenElevated && elevationDiff > maxElevationGap)
                {
                    Debug.Log("Player is elevated (" + elevationDiff.ToString("F2") + " units). Chasing at ground level.");
                    lastNavMeshSampleTime = Time.time;
                    return new Vector3(hit.position.x, monsterPos.y, hit.position.z);
                }
                
                lastNavMeshSampleTime = Time.time;
                return hit.position;
            }
            
            Debug.Log("No NavMesh found near player position. Chasing at exact position.");
        }
        
        // Return cached position if navmesh sampling is skipped
        return playerPos;
    }
    
    void UpdateSpeed(int pages)
    {
        float speed;
        float animSpeed;
        
        switch (pages)
        {
            case 0:
                speed = 1.5f;
                animSpeed = 0.2f;
                break;
            case 1:
                speed = 1.5f;
                animSpeed = 0.2f;
                break;
            case 2:
                speed = 1.7f;
                animSpeed = 0.4f;
                break;
            case 3:
                speed = 1.9f;
                animSpeed = 0.6f;
                break;
            case 4:
                speed = 2.5f;
                animSpeed = 0.8f;
                break;
            case 5:
                speed = 3f;
                animSpeed = 1f;
                break;
            case 6:
                speed = 3.5f;
                animSpeed = 1.2f;
                break;
            case 7:
                speed = 3.8f;
                animSpeed = 1.4f;
                break;
            case 8:
                speed = 4f;
                animSpeed = 1.6f;
                break;
            default:
                speed = 4f;
                animSpeed = 1.6f;
                break;
        }
        
        cachedAi.speed = speed;
        cachedAnim.speed = animSpeed;
    }
    
    void HandlePlayerDeath()
    {
        if (gameEnded)
            return;
        gameEnded = true;
        
        Debug.Log("Monster caught the player!");
        
        Application.Quit();
    }
    
    void ResetGame()
    {
        gameEnded = false;
    }
}