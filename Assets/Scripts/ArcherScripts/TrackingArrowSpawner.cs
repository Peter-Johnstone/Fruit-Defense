using UnityEngine;
using UnityEngine.Serialization;

public class TrackingArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject animationArrow;
    [SerializeField] private GameObject trackingArrow;
    [SerializeField] private GameObject bow;
        
    private AnimationArrow _animationArrowScript;

    private void Start()
    {
        _animationArrowScript = animationArrow.GetComponent<AnimationArrow>();
        
        // Subscribe to the OnBecameHidden event
        _animationArrowScript.OnBecameHidden += HandleLaunchArrow;
    }

    private void HandleLaunchArrow(GameObject hiddenObject)
    {
        // Instantiates a new arrow as a child of the Tracking Arrow Spawner.
        Instantiate(trackingArrow, animationArrow.transform.position, animationArrow.transform.rotation, transform).transform.SetParent(transform);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        _animationArrowScript.OnBecameHidden -= HandleLaunchArrow;
    }

    public Transform GetTarget()
    {
        return bow.GetComponent<ArcherShooting>().GetTarget();
    }

    public Sprite GetAnimationArrowImage()
    {
        return animationArrow.GetComponent<SpriteRenderer>().sprite;
    }
}