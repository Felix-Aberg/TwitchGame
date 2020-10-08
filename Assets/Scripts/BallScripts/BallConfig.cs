using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball Config", menuName = "Game/Config/Create Ball Config")]
public class BallConfig : ScriptableObject
{
    /* README
     * If you intend to test different values, maybe copy a existing BallConfig and make a "Beta" version
     */

    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Reminder = "Any values changed in this file WILL BE AUTOMATICALLY SAVED!!!\nThis also goes for play mode";

    [Header("Type multipliers")]

    public float RPMMultiplier;
    public float velocityMultiplier;
    [Tooltip("The maximum RPM that will be multiplied by RPM Multiplier on collision")]
    public float maxRPM;

    [Header("RNG")]

    public float RNGMinMultiplier;
    public float RNGMaxMultiplier;


    [Header("Damage values")]

    public float RPMMinDamageOnHit;
    public float RPMMaxDamageOnHit;
    public float RPMOnKill;
    public float HPDamageOnHit; //not yet used, to be used for durability


    [Header("Crits")]

    [Tooltip("Chance increase on failing crit roll, in percent from 0.0 to 1.0")]
    public float critChanceIncrement;
    [Tooltip("Lowest possible chance to crit, in percent from 0.0 to 1.0")]
    public float minimumCritChance;
    [Tooltip("Increase crit multiplier by this (additively) on successful crit")]
    public float critMultiplierIncrement;


    [Header("Initial Values")]

    public float initRPM;
    public float initCritMultiplier;


}
