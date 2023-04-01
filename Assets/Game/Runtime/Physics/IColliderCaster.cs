namespace Game.Runtime.Physics
{
    public interface IColliderCaster<TTarget>
    {
         Collision<TTarget> Cast(ICollider collider);
    }
}