using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

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
    [SerializeField] private GameObject modelLoadButton;
    [SerializeField] private TextMeshProUGUI debugText;
    // [SerializeField] private FirebaseController firebaseController;

    private PlaneTrackingManager planeTrackingManager = new PlaneTrackingManager(new List<int>(), new List<int>());
    private List<ImageTrackingManager> imageTrackingManager = new List<ImageTrackingManager>();
    private List<ImmersalManager> immersalManager = new List<ImmersalManager>();
    private List<GetImageManager> imageManager = new List<GetImageManager>();
    private List<GetModelManager> modelManager = new List<GetModelManager>();
    private List<ImageManager> images=new List<ImageManager>();
    private List<ModelManager> models=new List<ModelManager>();

    public void Start()
    {
        string ddd = "9Kwe3wUTRAeHxVRP7B4N";
        string aaa = "1,2";
        string bbb = "1,2";
        string ccc = "1,2,3";
        string[] aaa2 = aaa.Split(",");
        List<int> aaa3=aaa2.Select(int.Parse).ToList();
        string[] bbb2 = bbb.Split(",");
        List<int> bbb3=bbb2.Select(int.Parse).ToList();
        string[] ccc2 = ccc.Split(",");
        // FirestoreManager firestoreManager = firebaseController.GetFirestoreManager("9Kwe3wUTRAeHxVRP7B4N");
        List<object> modelID = aaa3.Select(i => (object)i).ToList();
        List<object> imageID = bbb3.Select(i => (object)i).ToList();
        List<int> detectType = ccc2.Select(int.Parse).ToList();
        StartCoroutine(LoadDataAsync("MainScene", ddd, modelID, imageID, detectType));
    }
    private IEnumerator LoadDataAsync(string sceneName, string documentID, List<object> modelID, List<object> imageID, List<int> detectType)
    {
        // IEnumerator coroutine = GetFireStore(documentID, modelID, imageID, detectType);
        // yield return StartCoroutine(coroutine);
        // bool result = (bool)coroutine.Current;
        // if (result)
        // {
        //     Debug.Log("取得成功");
        // }
        // else
        // {
        //     Debug.Log("取得失敗");
        //     yield break;
        // }

        yield return StartCoroutine(PerformSpecificTask());
        // 次のシーンを非同期で読み込み
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;


        // ロードが90%完了するまで待機
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // var firstID = planeTrackingData.planeTrackingManager.mainModelID[0];
        // debugText.text = $"{firstID}";
        asyncLoad.allowSceneActivation = true;
        // modelLoadButton.SetActive(true);
    }

    // public void FileLoad(){
    //     firebaseController.ImageFileDownloader(imageManager[0]);
    // }


    public void StartAR()
    {
        asyncLoad.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync("WaitingScene");
    }


    // private IEnumerator GetFireStore(string documentID, List<object> modelID, List<object> imageID, List<int> detectType)
    // {
    //     modelManager = firebaseController.GetModelData(modelID);
    //     if (detectType.Contains(1))
    //     {
    //         planeTrackingManager = firebaseController.GetPlaneTrackingData(documentID);
    //     }
    //     if (detectType.Contains(2))
    //     {
    //         imageTrackingManager = firebaseController.GetImageTrackingData(documentID);
    //         imageManager = firebaseController.GetImageData(imageID);
    //     }
    //     if (detectType.Contains(3))
    //     {
    //         immersalManager = firebaseController.GetImmersalData(documentID);
    //     }
    //     Debug.Log("GetFireStore");
    //     while (modelManager.Count <= 0)
    //     {
    //         yield return null;
    //     }
    //     while (planeTrackingManager == null && detectType.Contains(1))
    //     {
    //         yield return null;
    //     }
    //     while (imageTrackingManager.Count <= 0 && detectType.Contains(2))
    //     {
    //         yield return null;
    //     }
    //     while (imageManager.Count <= 0 && detectType.Contains(2))
    //     {
    //         yield return null;
    //     }
    //     while (immersalManager.Count <= 0 && detectType.Contains(3))
    //     {
    //         yield return null;
    //     }
    //     yield return true;
    // }

    private IEnumerator PerformSpecificTask()
    {
        //ここに特定の処理を記述
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

