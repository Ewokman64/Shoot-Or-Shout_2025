using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2_Bullet : MonoBehaviour
{
    private float currentBulletSpeed = 30;
    private float defaultBulletSpeed = 30;

    public float currentBulletDamage = 1;
    public float defaultBulletDamage = 1;

    public float currentPiercePower = 1;
    public float defaultPiercePower = 1;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * currentBulletSpeed);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //If the target hit is part of a list/library/whatever called "Enemies", Access Target.Health, call "Deal damage"
        //DealDamage(currentBulletDamage);
    }


    void DealDamage(float damageAmount)
    {
        //Access element that got hit

        //Element.HP -= damageAmount
    }
}
