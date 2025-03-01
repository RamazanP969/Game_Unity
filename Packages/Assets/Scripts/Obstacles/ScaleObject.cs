using UnityEngine;
using System.Collections;
public class ScaleObject : MonoBehaviour
{
    public float scaleUpDuration = 2f; 
    public float scaleDownDuration = 2f; 
    public Vector3 targetScale = new Vector3(2f, 2f, 2f); 
    private Vector3 originalScale; 

    void Start()
    {
        
        originalScale = transform.localScale;

       
        StartCoroutine(ScaleOverTime());
    }


    IEnumerator ScaleOverTime()
    {
        
        float elapsedTime = 0f;
        while (elapsedTime < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        elapsedTime = 0f;
        while (elapsedTime < scaleDownDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / scaleDownDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        StartCoroutine(ScaleOverTime());
    }
}
