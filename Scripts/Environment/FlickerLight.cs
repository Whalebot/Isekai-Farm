using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    Light flickerLight;
    public float min;
    public float max;
    public AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
        flickerLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        flickerLight.intensity = curve.Evaluate(Mathf.PingPong(Time.time, 1));
    }
}
