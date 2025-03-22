using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown;

    float collisionTime = 0f;

    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = LevelGenerator.instance;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time - collisionTime > collisionCooldown)
        {
            collisionTime = Time.time;
            animator.SetTrigger("Hit");
            levelGenerator.SetChunkMovementSpeed(-2f);
        }
    }
}
