using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace App.Main.Effects
{
    public class DamageEffect : MonoBehaviour
    {
        [SerializeField] private float _suicideTime = 0.3f;
        public void Initialize(int damageValue, GameObject canvas)
        {
            transform.SetParent(canvas.transform, false);
            Vector2 tmpPos = Camera.main.WorldToScreenPoint(transform.localPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), tmpPos, Camera.main, out var localPos);
            transform.localPosition = localPos;

            GetComponent<Text>().text = damageValue.ToString();
            StartCoroutine(DestroyEffect());
        }

        private IEnumerator DestroyEffect()
        {
            yield return new WaitForSeconds(0.05f);
            transform.position += new Vector3(0, 0.3f, 0);
            yield return new WaitForSeconds(0.05f);
            transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(_suicideTime - 0.1f);
            
            Destroy(gameObject);
        }
    }
}
