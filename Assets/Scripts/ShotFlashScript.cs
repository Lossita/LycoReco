using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFlashScript : MonoBehaviour
{
    private float _countDown;

    void FixedUpdate()
    {
        _countDown += Time.fixedDeltaTime;
        if (Math.Abs(_countDown - 0.1F) < 0.001)
            GameObject.Destroy(this.gameObject);
        var sr = this.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, -10F * _countDown + 1);
    }
}
