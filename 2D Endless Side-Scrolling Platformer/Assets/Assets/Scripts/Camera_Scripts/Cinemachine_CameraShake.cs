using UnityEngine;
using Unity.Cinemachine;

public class Cinemachine_CameraShake : MonoBehaviour
{
    public static Cinemachine_CameraShake instance;
    private float startShakingTime;
    private CinemachineCamera cinemachineCamera;

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cinemachineCamera = this.gameObject.GetComponent<CinemachineCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startShakingTime > 0f) {
            startShakingTime -= Time.deltaTime;

            if (startShakingTime <= 0f) {
                CinemachineBasicMultiChannelPerlin perlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.AmplitudeGain = 0f;
            }
        }
    }

    public void Shake(float shakeDuration, float intensity) {
        startShakingTime = shakeDuration;
        CinemachineBasicMultiChannelPerlin perlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.AmplitudeGain = intensity;
    }
}
