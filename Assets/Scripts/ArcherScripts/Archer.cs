using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    private ArcherShooting _archerShooting;
    
    private void Start()
    {
        _archerShooting = GetComponentInChildren<ArcherShooting>();
    }
    public void TriggerShoot()
    {
        _archerShooting.TriggerShoot();
    }
}
