using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text fpsText;
    public float dt;

    void Update()
    {
        dt += (Time.deltaTime - dt) * 0.1f;
        float fps = 1.0f / dt;
        fpsText.text = "FPS: " +  Mathf.Ceil(fps).ToString();
    }
}