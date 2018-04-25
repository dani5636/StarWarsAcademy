using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEffect : MonoBehaviour
{

    private Transform titleTransform;
    private float curTime = 0.0f;
    [SerializeField]
    private float speedModifier = 1.0f;
    [SerializeField]
    private float fadeTime = 5.0f;
    [SerializeField]
    private float fadeoutTime = 1f;

    private bool isFading = false;
    private float targetAlpha = 1;

    // Use this for initialization
	void Start () {
        titleTransform = GetComponent<Transform>();
		
	}
	
	// Update is called once per frame$
	void Update () {
        curTime += Time.deltaTime;

        titleTransform.Translate((Vector3.forward*(curTime/speedModifier))*Time.deltaTime);
        if (curTime > fadeTime && !isFading) {
            //TODO Fade should be implemented here
            Debug.Log("Fade Statement works, No fading yet");
        }


	}

    //TODO implement this fade code show it works correctly
    private IEnumerator Lerp_TextRenderer_Color(TextMesh target_MeshRender, float
    lerpDuration, Color startLerp, Color targetLerp)
    {
        float lerpStart_Time = Time.time;
        float lerpProgress;
        bool lerping = true;
        while (lerping)
        {
            yield return new WaitForEndOfFrame();
            lerpProgress = Time.time - lerpStart_Time;
            if (target_MeshRender != null)
            {
                target_MeshRender.color = Color.Lerp(startLerp, targetLerp, lerpProgress / lerpDuration);
            }
            else
            {
                lerping = false;
            }


            if (lerpProgress >= lerpDuration)
            {
                lerping = false;
            }
        }
        yield break;
    }
}
