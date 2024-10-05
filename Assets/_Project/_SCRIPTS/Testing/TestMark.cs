using UnityEngine;

namespace Gameplay
{
    public class TestMark : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private MarkType _mark;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _enemy.Mark(_mark);
        }
    }
}
