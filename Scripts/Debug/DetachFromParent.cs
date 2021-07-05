﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachFromParent : MonoBehaviour
{
    public bool resetRotation;
    public Vector3 defaultRotation;
    private void OnEnable()
    {
        transform.parent = null;
        if (resetRotation)
            transform.rotation = Quaternion.Euler(defaultRotation.x, transform.rotation.eulerAngles.y, defaultRotation.z);
    }
}
