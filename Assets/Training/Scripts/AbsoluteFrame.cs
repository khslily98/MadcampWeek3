﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsoluteFrame : MonoBehaviour
{
    Quaternion _initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        _initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _initialRotation;
    }
}
