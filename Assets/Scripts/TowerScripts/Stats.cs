using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int TotalDamage { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        TotalDamage = 0;
    }

    public void IncreaseTotalDamage(int increase)
    {
        TotalDamage += increase;
    }
}
