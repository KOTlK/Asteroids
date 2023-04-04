using Game.Runtime.GameLoop;

namespace Game.Runtime.Ship.Weapons
{
    public interface IWeapon : ILoop
    {
        bool CanShoot { get; }
        void Shoot();
    }
}