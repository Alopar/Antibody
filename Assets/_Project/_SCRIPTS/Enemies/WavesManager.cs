using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class WavesManager : MonoBehaviour
    {


        private List<Enemy> _enemies = new();

        public int EnemiesCount => _enemies.Count;


    }
}
