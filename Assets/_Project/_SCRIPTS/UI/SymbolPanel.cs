using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SymbolPanel : MonoBehaviour
    {
        [SerializeField] private Shooting _shooting;
        [SerializeField] private Image _img1;
        [SerializeField] private Image _img2;
        [SerializeField] private Image _img3;
        [SerializeField] private Sprite _X;
        [SerializeField] private Sprite _Y;
        [SerializeField] private Sprite _N;

        private void OnEnable()
        {
            OnSwitch(_shooting.Mark);
            _shooting.SwitchedMark += OnSwitch;
        }

        private void OnDisable()
        {
            _shooting.SwitchedMark -= OnSwitch;
        }

        private void OnSwitch(MarkType mark)
        {
            if (mark == MarkType.X)
            {
                _img1.sprite = _N;
                _img2.sprite = _X;
                _img3.sprite = _Y;
            }    
            else if (mark == MarkType.Y)
            {
                _img1.sprite = _X;
                _img2.sprite = _Y;
                _img3.sprite = _N;
            }    
            else if (mark == MarkType.N)
            {
                _img1.sprite = _Y;
                _img2.sprite = _N;
                _img3.sprite = _X;
            }    
        }
    }
}
