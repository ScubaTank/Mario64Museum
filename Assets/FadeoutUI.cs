using System.Collections;
using UnityEngine;

public class FadeoutUI : MonoBehaviour
{
    [Header("Timing Settings")]
        [SerializeField] private float delayBeforeFade = 3.0f; // How long it stays fully visible
        [SerializeField] private float fadeDuration = 1.0f;    // How long the actual fade takes
    
        private CanvasGroup _canvasGroup;
    
        void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            // Start the process immediately on launch
            StartCoroutine(HandleFade());
        }
    
        private IEnumerator HandleFade()
        {
            // 1. Wait for the initial delay (the "Warning" period)
            yield return new WaitForSeconds(delayBeforeFade);
    
            // 2. Gradually fade the alpha
            float elapsedTime = 0;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
                yield return null;
            }
    
            // 3. Final cleanup
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            
            // Optional: Disable the object entirely to save performance
            // gameObject.SetActive(false);
        }

        public void Restart()
        {
            _canvasGroup.alpha = 1;
            StartCoroutine(HandleFade());
        }
}
