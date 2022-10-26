using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject loadCanvas;
    [SerializeField] private Text loadText;
    [SerializeField] private Slider loadSlider;
    private AsyncOperation loadAsync;
    private bool flag = false;

    public void SceneChange(int buildIndex)
    {
        loadCanvas.SetActive(true);
        loadAsync = SceneManager.LoadSceneAsync(buildIndex);
        loadAsync.allowSceneActivation = false;
        flag = true;
    }



    private void Update()
    {
        if (flag)
        {
            loadSlider.value = Mathf.Round(loadAsync.progress);
            loadText.text = loadAsync.progress + " %";
            if (loadSlider.value == loadSlider.maxValue)
            {
                loadAsync.allowSceneActivation = true;
            }
        }
    }
}
