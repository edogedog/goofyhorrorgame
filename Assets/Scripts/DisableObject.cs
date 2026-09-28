using System.Collections;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject Obj;
    public float activeTime;
    
    private bool isCoroutineRunning = false;
    private bool isInitialized = false;
    
    void Start()
    {
        isInitialized = true;
    }
    
    void Update()
    {
        // Early exit if not initialized
        if (!isInitialized)
            return;
            
        // Only start coroutine once and avoid multiple starts
        if (Obj != null && Obj.activeSelf == true && !isCoroutineRunning)
        {
            StartCoroutine(DisableObj());
            isCoroutineRunning = true;
        }
    }
    
    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(activeTime);
        if (Obj != null)
        {
            Obj.SetActive(false);
        }
        isCoroutineRunning = false;
    }
}