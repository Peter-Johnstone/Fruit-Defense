using UnityEngine;
using UnityEngine.U2D.Animation;

public class Hair : MonoBehaviour
{
    private HairShooting _hairShooting;
    public bool HotHairActive { get; private set; }

    private SpriteLibrary _spriteLibrary;

    [SerializeField] private SpriteLibraryAsset hotHairSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteLibrary = GetComponent<SpriteLibrary>();
        _hairShooting = GetComponentInChildren<HairShooting>();
    }

    // Update is called once per frame
    public void FireProjectiles()
    {
        if (HotHairActive)
            _hairShooting.FireProjectilesHot();
        else
            _hairShooting.FireProjectiles();
        
    }

    public void UpgradeHotHair()
    {
        HotHairActive = true;
        _spriteLibrary.spriteLibraryAsset = hotHairSprites;
    }
}
