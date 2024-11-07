using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackSceneController : MonoBehaviour
{
    [SerializeField] private TrackingModeData trackingModeData;
    [SerializeField] private AsyncOperation asyncLoad;
    public void BackFromMainScene()
    {
        StartCoroutine(LoadNextSceneAsync("WaitingScene"));
    }
    public void BackFromPlaneScene()
    {
        if (trackingModeData.useImageTracking || trackingModeData.useGeospatial)
        {
            StartCoroutine(LoadNextSceneAsync("MainScene"));
        }
        else
        {
            StartCoroutine(LoadNextSceneAsync("WaitingScene"));
        }
    }
    public void BackFromImmersal()
    {
        if (trackingModeData.useImageTracking || trackingModeData.useGeospatial)
        {
            StartCoroutine(LoadNextSceneAsync("MainScene"));
        }
        else if (trackingModeData.usePlaneTracking)
        {
            StartCoroutine(LoadNextSceneAsync("PlaneScene"));
        }
        else
        {
            StartCoroutine(LoadNextSceneAsync("WaitingScene"));
        }
    }


    private IEnumerator LoadNextSceneAsync(string sceneName)
    {
        // 次のシーンを非同期で読み込み
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        // ロードが90%完了するまで待機
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        Debug.Log("OK");
        asyncLoad.allowSceneActivation = true;
    }
}
