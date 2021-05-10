using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagetScript : MonoBehaviour
{
    public int score = 0;

    public void UpdateScore()
    {
        score += 1;
    }

    // if score > 15 switch attack to ult 
}
