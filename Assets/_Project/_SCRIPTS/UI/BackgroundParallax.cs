using System;
using UnityEngine;

namespace Gameplay
{
    public class BackgroundParallax : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 2)] private float _offsetSpeed;

        [Space(10)]
        [SerializeField] private Transform _background;
        #endregion

        #region FIELDS PRIVATE
        private Camera _camera;
        #endregion

        #region METHODS PRIVATE
        private void Init()
        {
            _camera = Camera.main;

            TileBackground(10, 38.5f, 21.5f);
            TileBackground(10, 38.5f, -21.5f);
            TileBackground(10, -38.5f, -21.5f);
            TileBackground(10, -38.5f, 21.5f);
        }

        private void TileBackground(int count, float offsetX, float offsetY)
        {
            for (int i = 0; i < count; i++)
            {
                for (var j = 0; j < count; j++)
                {
                    if (i == 0 && j == 0) continue;

                    var background = Instantiate(_background, transform);
                    var position = background.localPosition;
                    position.x += offsetX * i;
                    position.y += offsetY * j;
                    background.localPosition = position;
                }
            }
        }

        private void SetOffset()
        {
            var offset = (Vector3.zero - _camera.transform.position) * _offsetSpeed;
            offset.z = 0;
            transform.localPosition = offset;
        }
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            Init();
        }

        private void LateUpdate()
        {
            SetOffset();
        }
        #endregion
    }
}
