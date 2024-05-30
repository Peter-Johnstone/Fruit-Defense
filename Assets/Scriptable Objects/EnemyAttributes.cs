using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Scriptable_Objects
{
    [CreateAssetMenu]
    public class EnemyAttributes : ScriptableObject
    {
        public float moveSpeed;
        public int lifePoints;
        public SpriteLibraryAsset regularSprites;
        public SpriteLibraryAsset stickySprites;
    }
}