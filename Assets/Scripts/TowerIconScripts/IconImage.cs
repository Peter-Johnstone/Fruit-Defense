using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconImage : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = GetComponentInParent<TowerIcon>().towerPrefab.GetComponent<SpriteRenderer>().sprite;
    }
}
