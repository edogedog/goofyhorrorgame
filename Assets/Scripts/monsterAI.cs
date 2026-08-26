using UnityEngine;

public class monsterAI
{
    public NavMeshAgent ai;
    public Animator anim:
    public Transform player;
    Vector3 dest;
    void Update()
    {
        dest = player,position;
        ai.destination = dest;
        if (pagesCollected == 1)
        {
            ai.speed = 1.5f;
        }
        if (pagesCollected == 2)
        {
            ai.speed = 1.9f;
        }




    }


}
