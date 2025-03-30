using UnityEngine;

public class RockVFXPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem vfx;
    [SerializeField] AudioSource audio;
    [SerializeField] float cooldown;

    float curr;

    private void Start()
    {
        curr = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.GetContact(0);
        Vector3 vfxPos = cp.point;
        vfx.transform.position = vfxPos;

        if (Time.time - curr > cooldown)
        {
            curr = Time.time;

            vfx.Play();
            audio.Play();
        }
    }
}
