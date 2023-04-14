using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Runtime.Factories.View.Explosions
{
    public class ExplosionsFactory : MonoBehaviour
    {
        [SerializeField] private Explosion _prefab;

        private readonly List<Explosion> _spawned = new();

        public Explosion Create()
        {
            var explosion = _spawned.FirstOrDefault(exp => exp.IsPlaying == false);

            if (explosion == null)
            {
                explosion = Instantiate(_prefab, transform);
                _spawned.Add(explosion);
            }

            return explosion;
        }
    }
}