using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public float currentSpeed;
    public float regularSpeed;
    public float slowSpeed;

    private void Start()
    {
        slowSpeed = regularSpeed * 0.5f;
    }
}
