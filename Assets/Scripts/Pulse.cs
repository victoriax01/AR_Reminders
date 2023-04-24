using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    // WORK IN PROGRESS script to pulse the colour indicators on reminders, however this script was never finished

    // private Material m;
    MeshRenderer meshRenderer;
    [SerializeField]
    public int pulseMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        // m = GetComponent<Material>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // float transp = meshRenderer.material.color.a;
        // while (transp > 0)
        // {
        //     // m.color.a = transp - pulseMultiplier;
        //     Color prevColour = meshRenderer.material.color;
        //     meshRenderer.material.color = new Color(prevColour.r, prevColour.g, prevColour.b, prevColour.a-pulseMultiplier);
        // }
        // Debug.Log(transp);
        // StartCoroutine(fadeOut(meshRenderer, 2f));
        // StartCoroutine(fadeIn(meshRenderer, 2f));
        // StartCoroutine(fadeOut(meshRenderer, 2f));
        StartCoroutine(Fade(meshRenderer));
    }

    IEnumerator Fade(MeshRenderer MyRenderer)
    {
        // loop over 1 second backwards
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            MyRenderer.material.color = new Color(1, 1, 1, i);
            yield return null;
        }
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            MyRenderer.material.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    IEnumerator fadeOut(MeshRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(0.36f, 0, counter / duration);
            Debug.Log(alpha);

            //Change alpha only
            MyRenderer.material.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        yield return new WaitForSeconds(2f);
    }

    IEnumerator fadeIn(MeshRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(0, 0.36f, counter / duration);
            Debug.Log(alpha);

            //Change alpha only
            MyRenderer.material.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        yield return new WaitForSeconds(2f);
    }
}
