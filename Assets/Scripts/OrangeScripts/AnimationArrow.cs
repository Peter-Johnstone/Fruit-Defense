using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationArrow : MonoBehaviour
{
    // Define the event using a delegate
    public event Action<GameObject> OnBecameHidden;

    // This method is called by Unity when the object becomes invisible
    private void OnBecameInvisible()
    {
        // Invoke the event
        OnBecameHidden?.Invoke(gameObject);
    }
}
