using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    //POWERUP IS A MORE ACCURATE WORDING
    //1 we need all the powerups (in a list possibly)
    //2 we need to shuffle them and add 3 to the 3 slots to assign them
    //3 we need a UI to pop up and disappear
    //4 show the 3 powerup by assigning them to their empty slots
    //5 pick with keys 1-2-3
    //6 assign the corresponding upgrade to one of the 5 slots in our upgrade inventory
    //7 making sure that no duplicates happen
    //8 making sure if a duplicate is picked, we upgrade the already equipped powerup
    //9 if we run out of space, ability to remove powerup

    /*1*/
    public List<GameObject> powerupsInGame; //This list contains every selectable, run only powerups that Shoot or Shout has


}
