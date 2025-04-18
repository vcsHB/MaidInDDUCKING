using UnityEngine;
namespace Combat.Casters
{
    [System.Serializable]
    public class KnockbackCasterData : CasterData
    {
        public Vector2 powerDirection;
        public float duration;
        public bool isCrashed; // After Collision Damage By Wall or Floor
        public float crashDamage;
        public KnockbackCasterData(Vector2 direction, float duration, bool isCrashed = false, float crashDamage = 0f)
        {
            this.powerDirection = direction;
            this.duration = duration;
            this.isCrashed = isCrashed;
            this.crashDamage = crashDamage;
        }
    }
    public class KnockbackCaster : MonoBehaviour, ICastable
    {
        [SerializeField] protected KnockbackCasterData _knockbackData;

        public void SetDirection(Vector2 direction)
        {
            _knockbackData.powerDirection = direction;
        }

        public virtual void Cast(Collider2D target)
        {
            if (_knockbackData == null) return;

            if (target.TryGetComponent(out IKnockbackable knockbackable))
            {
                knockbackable.ApplyKnockback(_knockbackData);
            }
        }

        public void HandleSetData(CasterData data)
        {
            if(data is KnockbackCasterData knockbackData)
                _knockbackData = knockbackData;

        }
    }
}