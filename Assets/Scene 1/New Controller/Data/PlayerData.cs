using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data / Base Data")]
public class PlayerData : ScriptableObject
{
    public float movementSpeed = 10f;
    public float jumpSpeed = 12f;
    public float doubleJumpMult = .75f;
    public float fallSpeed = .5f;
    public float totalFlyTime = 2f;
    public float flySpeed = 5f;

    public bool canDoubleJump = true;
    public bool canFly = true;
}
