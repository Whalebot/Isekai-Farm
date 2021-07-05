using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public CinemachineConfiner confiner;
    public CinemachineVirtualCamera[] cameras;
    public CinemachineFreeLook freeLookCam;
    CinemachineBasicMultiChannelPerlin[] noises;
    [SerializeField] private float shakeTimer;
    private float startTimer;
    private float startIntensity;
    public bool toggle;
    public CinemachineVirtualCamera lockOnCam;
    public CinemachineVirtualCamera lockOnCam2;


    public CinemachineTargetGroup targetGroup;
    public CinemachineVirtualCamera groupCamera;

    public Transform defaultTarget;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //GameObject boundingBox = GameObject.FindGameObjectWithTag("CameraBoundary");
        //if (boundingBox != null)
        //    confiner.m_BoundingVolume = boundingBox.GetComponent<Collider>();

        noises = new CinemachineBasicMultiChannelPerlin[cameras.Length];
        for (int i = 0; i < noises.Length; i++)
        {
            noises[i] = cameras[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }


        freeLookCam.m_XAxis.Value = 0;
        freeLookCam.m_YAxis.Value = 0.5f;

        //freeLookCam.m_YAxisRecentering.RecenterNow();
        //freeLookCam.m_RecenterToTargetHeading.RecenterNow();

        ShakeCamera(0, 0.1F);
    }

    void Noise(float amplitude)
    {

        freeLookCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        freeLookCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    }

    public void SetLockOnTarget(Transform temp)
    {
        toggle = !toggle;
        StartCoroutine("DelayCamSwitch", temp);
        //if (toggle)
        //{
        //    lockOnCam.LookAt = temp;
      

        //    lockOnCam.Priority = 15;
        //    lockOnCam2.Priority = 14;


        //    lockOnCam.gameObject.SetActive(true);
        //    lockOnCam2.gameObject.SetActive(false);
         
        //}
        //else
        //{
        //    lockOnCam2.LookAt = temp;
        
        //    lockOnCam.Priority = 14;
        //    lockOnCam2.Priority = 15;
        //    lockOnCam2.gameObject.SetActive(true);
        //    lockOnCam.gameObject.SetActive(false);

        //}

    }

    IEnumerator DelayCamSwitch(Transform temp) {

        if (toggle)
        {
            lockOnCam.LookAt = targetGroup.transform;
            lockOnCam.gameObject.SetActive(true);
            yield return new WaitForFixedUpdate();
            lockOnCam.Priority = 15;
            lockOnCam2.Priority = 14;
            SetGroupTarget(temp);
            lockOnCam2.gameObject.SetActive(false);

        }
        else
        {
            lockOnCam2.LookAt = targetGroup.transform;
            lockOnCam2.gameObject.SetActive(true);
            yield return new WaitForFixedUpdate();
            lockOnCam.Priority = 14;
            lockOnCam2.Priority = 15;
            SetGroupTarget(temp);
            lockOnCam.gameObject.SetActive(false);

        }

    }

    public void DisableLockOn()
    {
        lockOnCam.gameObject.SetActive(false);
        lockOnCam2.gameObject.SetActive(false);
        lockOnCam.LookAt = null;
        lockOnCam2.LookAt = null;
        targetGroup.m_Targets[1].target = defaultTarget;
    }

    public void SetGroupTarget(Transform temp)
    {
        targetGroup.m_Targets[1].target = temp;
       // groupCamera.gameObject.SetActive(true);
        shakeTimer = 0;
        for (int i = 0; i < noises.Length; i++)
            noises[i].m_AmplitudeGain = 0;
    }

    public void SetGroupTarget()
    {
        groupCamera.gameObject.SetActive(true);
        shakeTimer = 0;
        for (int i = 0; i < noises.Length; i++)
            noises[i].m_AmplitudeGain = 0;
    }

    public void RevertCamera()
    {
        groupCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            {
                for (int i = 0; i < noises.Length; i++)
                {
                    noises[i].m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, (1 - (shakeTimer / startTimer)));

                }
                Noise(Mathf.Lerp(startIntensity, 0f, (1 - (shakeTimer / startTimer))));

            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        startIntensity = intensity;
        shakeTimer = time;
        startTimer = time;
    }
}