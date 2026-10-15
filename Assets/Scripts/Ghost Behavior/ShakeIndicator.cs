using UnityEngine;

public class ShakeIndicator : MonoBehaviour
{
    [Range(0, 180)][SerializeField] private float Intensity = 1f;
    [SerializeField] private float Cooldown = 5f;
    [SerializeField] private float ShakeDuration = 5f;
    private float timer = 0;
    private float shakeTimer = 0;

    private Quaternion initialRotation;

    private void Start() {
        initialRotation = transform.rotation;
    }



    void Update()
    {
        if (timer < Cooldown) {
            timer += Time.deltaTime;
            return;
        } 

            Vector3 rotation = new Vector3(
                Random.Range(-Intensity, Intensity),
                Random.Range(-Intensity, Intensity),
                Random.Range(-Intensity, Intensity)
            );

        transform.rotation = Quaternion.Euler(rotation);
        if (shakeTimer < ShakeDuration) {
            shakeTimer += Time.deltaTime;
        } else {
            shakeTimer = 0;
            timer = 0;
            transform.rotation = initialRotation;
        }

    }
}
