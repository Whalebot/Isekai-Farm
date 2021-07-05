using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Moveset", menuName = "ScriptableObjects/Moveset")]
public class Moveset : ScriptableObject
{
    public Move dashAttack;
    public Move airDashAttack;
    public Move runningLight;
    public Move runningHeavy;

    public Combo lightCombo;
    public Combo heavyCombo;
    public Combo[] combos;
    public Combo extra;
}

