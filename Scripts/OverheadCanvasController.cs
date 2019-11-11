using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheadCanvasController : MonoBehaviour
{
    public Text objectName;
    public Image progressBar;
    private GameObject player;
    private float progressbarWidth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        progressbarWidth = progressBar.rectTransform.rect.width;
    }

    private void Update()
    {
        transform.rotation = player.transform.rotation;
    }

    public void SetProgressbar(float aRatio)
    {
        progressBar.rectTransform.sizeDelta = new Vector2(progressbarWidth * aRatio, progressBar.rectTransform.rect.height);
    }
}
