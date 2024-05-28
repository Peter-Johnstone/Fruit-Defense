using UnityEngine;

public class Hair : MonoBehaviour
{
    private HairShooting _hairShooting;
    
    // Start is called before the first frame update
    void Start()
    {
        _hairShooting = GetComponentInChildren<HairShooting>();
    }

    // Update is called once per frame
    public void FireProjectiles()
    {
        _hairShooting.FireProjectiles();
    }
}
