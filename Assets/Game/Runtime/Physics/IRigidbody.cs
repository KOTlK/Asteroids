using System;

namespace Game.Runtime.Physics
{
    public interface IRigidbody
    {
        event Action<IRigidbody> Collided;
    }
}