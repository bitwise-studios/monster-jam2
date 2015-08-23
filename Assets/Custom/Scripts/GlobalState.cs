using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalState : Singleton<GlobalState> {
    // Transient variables
    public bool PlayerCanMove = true;

    // Inventory
    public List<string> Inventory = new List<string>();

    // Scene-specific variables
    /* Scene 1: Dog choice */
    // 0 for ignore, 1 for eat, 2 for kickdog, 3 for feeddog
    public int ChocoDogDecision = 0;

    /* Scene 2: Skip school choice */
    public bool SkippedSchool = false;

    /* Scene 2a [4]: Skip school consequence; not necessary? */
    public bool Busted = false;

    /* Scene 3: Test result */
    public float TestResult = 0.0f;

    /* Scene 4[5]: Years later @ Home */
    // 0 for ignore, 1 for gas, 2 for pipe
    public int HouseChoice = 0;

    /* Scene 5[6,7,8]: Career */
    // Doctor: 0 for ignore, 1 for treat, 2 for experiment
    // Construction: 0 for leave, 1 for give, 2 for explosion
    // Criminal: 0 for robber, 1 for murderer, 2 for sadist
    public int FinalChoice = 0;

}
