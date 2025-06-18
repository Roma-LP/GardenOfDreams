using System.Collections;
using UnityEngine;

namespace _GardenOfDreams.Scripts
{
    public class HitFlashEffect : MonoBehaviour
    {
        [SerializeField] private UnitBase _unitBase;
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private Color _flashColor = Color.red;
        [SerializeField] private float _flashDuration = 0.1f;

        private Color[] _originalColors;

        public void Init()
        {
            _originalColors = new Color[_spriteRenderers.Length];
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _originalColors[i] = _spriteRenderers[i].color;
            }

            _unitBase.OnTakeDamage += PlayFlash;
        }

        private void PlayFlash(float amount)
        {
            StopAllCoroutines();
            StartCoroutine(FlashCoroutine());
        }

        private IEnumerator FlashCoroutine()
        {
            SetColor(_flashColor);
            yield return new WaitForSeconds(_flashDuration);
            SetColorToOriginal();
        }

        private void SetColor(Color color)
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _spriteRenderers[i].color = color;
            }
        }

        private void SetColorToOriginal()
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _spriteRenderers[i].color = _originalColors[i];
            }
        }
    }
}
