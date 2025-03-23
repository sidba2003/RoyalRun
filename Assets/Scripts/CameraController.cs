using JetBrains.Annotations;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int minimumVerticalFOV = 38;
    [SerializeField] int maximumVerticalFOV = 72;
    [SerializeField] int CameraFOVMoveSpeed = 10;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] ParticleSystem speedUpParticleSystem;

    CinemachineCamera camera;
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        camera = GetComponent<CinemachineCamera>();
    }

    public void SetVerticalFOV(int amount)
    {
        float verticalFOV = camera.Lens.FieldOfView + amount;
        verticalFOV = Mathf.Min(Mathf.Max(verticalFOV, minimumVerticalFOV), maximumVerticalFOV);

        if (amount > 0 && !speedUpParticleSystem.isEmitting)
        {
            speedUpParticleSystem.Play();
        } 

        if (amount < 0)
        {
            speedUpParticleSystem.Stop();
        }

        StopAllCoroutines();
        StartCoroutine(LerpFOV(verticalFOV));
    }

    IEnumerator LerpFOV(float lerpEndPoint)
    {
        float elapsedTime = 0f;
        float startPoint = camera.Lens.FieldOfView;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            camera.Lens.FieldOfView = Mathf.Lerp(startPoint, lerpEndPoint, elapsedTime / zoomDuration);

            yield return null;
        }

        camera.Lens.FieldOfView = lerpEndPoint;
    }
}
