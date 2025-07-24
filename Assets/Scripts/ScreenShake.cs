using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public int Iterations = 3;
    public int ShakeAmount = 3;
    public float ShakeDelay = 0.2f;

    public IEnumerator ShakeRoutine()
    {
        Vector3 originalPos = transform.position;
        for (int n = 0; n < Iterations; n++)
        {
            Vector3 pos = Random.insideUnitCircle * ShakeAmount;
            transform.position = transform.position + pos;
            yield return new WaitForSeconds(ShakeDelay);
        }
        transform.position = originalPos;
        yield return null;
    }
}
