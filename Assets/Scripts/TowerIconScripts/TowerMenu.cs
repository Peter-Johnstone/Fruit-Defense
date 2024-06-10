using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenu : MonoBehaviour
{

    private static bool _lastDraggerActive;


    public static void ActivateNewDragger(GameObject dragger)
    {
        if (_lastDraggerActive)
        {
            dragger.SetActive(false);
            _lastDraggerActive = false;
        }
        else
        {
            dragger.SetActive(true);
            _lastDraggerActive = true;
        }
    }

    public static void SetDraggerInactive(GameObject dragger)
    {
        dragger.SetActive(false);
        _lastDraggerActive = false;
    }


}
