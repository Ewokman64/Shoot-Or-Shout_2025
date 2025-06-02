using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoT : MonoBehaviour
{
    //This script is handling the applying of damage overtime to enemies

    //Instance is a more resource friendly alternative for "GameObject.Find".
    //With the usage of instance, we don't need to search the entire game scene.
    //Another important perk is that this script can be accesses from any other script without looking for references

    public static DoT Instance;

    public void ActivateDot()
    {
        Debug.Log("Damage Over Time Activated!");
    }

    void Awake()
    {
        Instance = this;
    }
}
