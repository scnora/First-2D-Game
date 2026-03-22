using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void DamageTargets() => entity.DamageTargets();

    private void DisableJumpandMovement()
    {
        entity.EnableMovement(false);
    }

    private void EnableJumpandMovement()
    {
        entity.EnableMovement(true);
    }
}
