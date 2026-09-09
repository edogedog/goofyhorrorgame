using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public Animator anim;
    public Transform player;

    void Update()
    {
        // Chase player
        ai.SetDestination(player.position);

        // Monster gets faster as pages are collected
        switch (pickupLetter.pagesCollected)
        {
            case 1:
                ai.speed = 1.5f;
                anim.speed = 0.2f;
                break;

            case 2:
                ai.speed = 1.7f;
                anim.speed = 0.4f;
                break;

            case 3:
                ai.speed = 1.9f;
                anim.speed = 0.6f;
                break;

            case 4:
                ai.speed = 2.5f;
                anim.speed = 0.8f;
                break;

            case 5:
                ai.speed = 3f;
                anim.speed = 1f;
                break;

            case 6:
                ai.speed = 3.5f;
                anim.speed = 1.2f;
                break;

            case 7:
                ai.speed = 3.8f;
                anim.speed = 1.4f;
                break;

            case 8:
                ai.speed = 4f;
                anim.speed = 1.6f;
                break;
        }
    }
}