using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleScript : MonoBehaviour
{
    private float _countDown;

    // Update is called once per frame
    void FixedUpdate()
    {
        _countDown += Time.fixedDeltaTime;
        if(Math.Abs(_countDown - 0.6F) < 0.001)
            GameObject.Destroy(this.gameObject);
    }
}
