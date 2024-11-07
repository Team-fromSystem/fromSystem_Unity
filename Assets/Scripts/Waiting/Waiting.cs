using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Waiting : MonoBehaviour
{
    [SerializeField] private ImmersalData immersalData;
    [SerializeField] private ImageTrackingData imageTrackingData;
    [SerializeField] private PlaneTrackingData planeTrackingData;
    [SerializeField] private ImageData imageData;
    [SerializeField] private ModelData modelData;

    [SerializeField] private List<ImmersalManager> immersalMocks;
    [SerializeField] private List<ImageTrackingManager> imageTrackingMocks;
    [SerializeField] private PlaneTrackingManager planeTrackingMock;
    [SerializeField] private List<ImageManager> imageMocks;
    [SerializeField] private List<ModelManager> modelMocks;
    [SerializeField] private AsyncOperation asyncLoad;
    [SerializeField] private Canvas buttonCanvas;
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private FirebaseController firebaseController;

    public void Start()
    {
        buttonCanvas.enabled = false;
        // firebaseController.GetImmersalData();
        StartCoroutine(LoadNextSceneAsync("MainScene"));
    }
    private IEnumerator LoadNextSceneAsync(string sceneName)
    {
        // yield return StartCoroutine(GetFireStore());
        yield return StartCoroutine(PerformSpecificTask());
        // 次のシーンを非同期で読み込み
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;


        // ロードが90%完了するまで待機
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        Debug.Log("OK");
        // yield return StartCoroutine(WaitSecond());
        buttonCanvas.enabled = true;

        // var firstID = planeTrackingData.planeTrackingManager.mainModelID[0];
        // debugText.text = $"{firstID}";
        asyncLoad.allowSceneActivation = true;
        // // 前のロードシーンをアンロード
        // SceneManager.UnloadSceneAsync("WaitingScene");
    }

    public void StartAR()
    {
        asyncLoad.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync("WaitingScene");
    }

    private IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator GetFireStore(){
        // List<ImageTrackingManager> imageTrackingManager=firebaseController.GetImageTrackingData();
        // while(imageTrackingManager.Count<=0){
        //     yield return null;
        // }
        // Debug.Log($"imageID:{imageTrackingManager[1].imageID}");
        firebaseController.GetModelData();
        yield return null;
    }

    private IEnumerator PerformSpecificTask()
    {
        // ここに特定の処理を記述
        Debug.Log("特定の処理を実行中...");
        immersalData.immersalManagers = new List<ImmersalManager>(immersalMocks);
        imageTrackingData.imageTrackingManagers = new List<ImageTrackingManager>(imageTrackingMocks);
        planeTrackingData.planeTrackingManager = new PlaneTrackingManager(planeTrackingMock.mainModelID, planeTrackingMock.decorationModelID);
        imageData.imageManagers = new List<ImageManager>(imageMocks);
        modelData.modelManagers = new List<ModelManager>(modelMocks);
        yield return null;
        Debug.Log("特定の処理が完了しました");
    }
}


    // if (immersalMocks.Count != 0)
        // {
        //     while (immersalData.immersalManagers.Count == 0)
        //     {
        //         Debug.Log("WaitingImmersalData");
        //         yield return null;
        //     }
        // }
        // if (imageTrackingMocks.Count != 0)
        // {
        //     while (imageTrackingData.imageTrackingManagers.Count == 0)
        //     {
        //         Debug.Log("WaitingImageTrackingData");
        //         yield return null;
        //     }
        // }
        // if (planeTrackingMock != null)
        // {
        //     while (planeTrackingData.planeTrackingManager == null)
        //     {
        //         Debug.Log("WaitingPlaneTrackingData");
        //         yield return null;
        //     }
        // }
        // if (imageMocks.Count != 0)
        // {
        //     while (imageData.imageManagers.Count == 0)
        //     {
        //         Debug.Log("WaitingImageData");
        //         yield return null;
        //     }
        // }
        // if (modelMocks.Count != 0)
        // {
        //     while (modelData.modelManagers.Count == 0)
        //     {
        //         Debug.Log("WaitingModelData");
        //         yield return null;
        //     }
        // }

