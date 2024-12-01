using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using UniVRM10;
using System.IO;
using System.Threading.Tasks;

public class Waiting : MonoBehaviour
{
    [SerializeField] private Canvas nextButton;
    [SerializeField] private List<GetImageManager> sampleImages;
    [SerializeField] private List<GetModelManager> sampleModels;
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
    [SerializeField] private FirebaseController firebaseController;
    [SerializeField] private LoadFileController loadFileController;
    [SerializeField] private ModelSingleton modelSingleton;
    [SerializeField] private ImageSingleton imageSingleton;

    private PlaneTrackingManager planeTrackingManager = new PlaneTrackingManager(new List<int>(), new List<int>());
    private List<ImageTrackingManager> imageTrackingManager = new List<ImageTrackingManager>();
    private List<ImmersalManager> immersalManager = new List<ImmersalManager>();
    private List<GetImageManager> imageManager = new List<GetImageManager>();
    private List<GetModelManager> modelManager = new List<GetModelManager>();
    private List<ImageManager> images = new List<ImageManager>();
    private List<ModelManager> models = new List<ModelManager>();

    public async void Start()
    {
        string docPath = "/Users/okusan/Downloads/";
        string docPath2 = "/Users/okusan/Desktop/";
        string ddd = "9Kwe3wUTRAeHxVRP7B4N";
        string aaa = "1,2";
        string bbb = "1,2";
        string ccc = "1,2,3";
        string[] aaa2 = aaa.Split(",");
        List<int> modelID = aaa2.Select(int.Parse).ToList();
        string[] bbb2 = bbb.Split(",");
        List<int> imageID = bbb2.Select(int.Parse).ToList();
        string[] ccc2 = ccc.Split(",");
        // FirestoreManager firestoreManager = firebaseController.GetFirestoreManager("9Kwe3wUTRAeHxVRP7B4N");
        List<object> modelIDAsObj = modelID.Select(i => (object)i).ToList();
        List<object> imageIDAsObj = imageID.Select(i => (object)i).ToList();
        List<int> detectType = ccc2.Select(int.Parse).ToList();
        bool isLoadData = await LoadDataAsync("MainScene", docPath, ddd, modelIDAsObj, imageIDAsObj, detectType);
        if (!isLoadData)
        {
            Debug.Log("データのロードに失敗しました。");
            return;
        }
        bool isLoadModel = await LoadModelAsPath(docPath);
        if (!isLoadModel)
        {
            Debug.Log("モデルのロードに失敗しました。");
            return;
        }
        if (detectType.Contains(2))
        {
            bool isLoadImage = await LoadImagesAsPath(docPath2);
            if (!isLoadImage)
            {
                Debug.Log("画像のロードに失敗しました。");
                return;
            }
        }
        nextButton.enabled = true;
    }
    public void ChangeScene()
    {
        asyncLoad.allowSceneActivation = true;//ここでシーン遷移する
    }
    private async Task<bool> LoadDataAsync(string sceneName, string docPath, string documentID, List<object> modelID, List<object> imageID, List<int> detectType)
    {
        bool isChecked = await GetFireStore(documentID,modelID,imageID,detectType);
        // 次のシーンを非同期で読み込み
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        if (!isChecked)
        {
            return false;
        }
        return true;


        // // ロードが90%完了するまで待機
        // while (asyncLoad.progress < 0.9f)
        // {
        //     continue;
        // }
    }

    private async Task<bool> LoadModelAsPath(string docPath)
    {
        foreach (var one in sampleModels)
        {
            GameObject model = null;
            string path = "";
            // if (one.modelID == 0)
            // {
            path = $"{docPath + one.modelName}.{one.fileFormat}";
            // }
            // else
            // {
            //     path = $"{docPath + one.modelName + one.modelID}.{one.fileFormat}";
            // }
            if (!System.IO.File.Exists(path))
            {
                return false;
            }
            Debug.Log(path);
            if (one.fileFormat == "vrm")
            {
                model = await loadFileController.LoadVRM(path);
            }
            var col = model.AddComponent<SphereCollider>();
            col.radius = one.colliderRadius;
            col.center = new Vector3(one.colliderPosition.X, one.colliderPosition.Y, one.colliderPosition.Z);
            ModelManager modelData = new ModelManager(one.modelID, one.modelName, model);
            modelSingleton.modelData.Add(modelData);
            DontDestroyOnLoad(model);
            model.SetActive(false);
        }
        return true;
    }

    private async Task<bool> LoadImagesAsPath(string docPath)
    {
        foreach (var one in sampleImages)
        {
            string path = "";
            // if (one.imageID == 0)
            // {
            path = $"{docPath + one.imageName}.{one.fileFormat}";
            // }
            // else
            // {
            //     path = $"{docPath + one.imageName + one.imageID}.{one.fileFormat}";
            // }
            if (!System.IO.File.Exists(path))
            {
                return false;
            }
            Debug.Log(path);
            Texture2D image = await loadFileController.LoadImage(path);
            ImageManager imageData = new ImageManager(one.imageID, image);
            imageSingleton.imageData.Add(imageData);
        }
        return true;
    }

    // public void FileLoad(){
    //     firebaseController.ImageFileDownloader(imageManager[0]);
    // }


    // public void StartAR()
    // {
    //     asyncLoad.allowSceneActivation = true;
    //     SceneManager.UnloadSceneAsync("WaitingScene");
    // }


    private async Task<bool> GetFireStore(string documentID, List<object> modelID, List<object> imageID, List<int> detectType)
    {
        // modelManager = firebaseController.GetModelData(modelID);
        if (detectType.Contains(1))
        {
            planeTrackingManager = await firebaseController.GetPlaneTrackingData(documentID);
            planeTrackingData.planeTrackingManager = new PlaneTrackingManager(planeTrackingManager.mainModelID, planeTrackingManager.decorationModelID);

        }
        if (detectType.Contains(2))
        {
            imageTrackingManager = await firebaseController.GetImageTrackingData(documentID);
            imageTrackingData.imageTrackingManagers = new List<ImageTrackingManager>(imageTrackingManager);
            // imageManager = firebaseController.GetImageData(imageID);
        }
        if (detectType.Contains(3))
        {
            immersalManager = await firebaseController.GetImmersalData(documentID);
            immersalData.immersalManagers = new List<ImmersalManager>(immersalManager);
        }
        Debug.Log("GetFireStore");
        // while (modelManager.Count <= 0)
        // {
        //     yield return null;
        // }
        while (planeTrackingManager == null && detectType.Contains(1))
        {
            continue;
        }
        while (imageTrackingManager.Count <= 0 && detectType.Contains(2))
        {
            continue;
        }
        // while (imageManager.Count <= 0 && detectType.Contains(2))
        // {
        //     yield return null;
        // }
        while (immersalManager.Count <= 0 && detectType.Contains(3))
        {
            continue;
        }
        return true;
    }

    private async Task<bool> PerformSpecificTask()
    {
        //ここに特定の処理を記述
        Debug.Log("特定の処理を実行中...");
        immersalData.immersalManagers = new List<ImmersalManager>(immersalMocks);
        imageTrackingData.imageTrackingManagers = new List<ImageTrackingManager>(imageTrackingMocks);
        planeTrackingData.planeTrackingManager = new PlaneTrackingManager(planeTrackingMock.mainModelID, planeTrackingMock.decorationModelID);
        return true;
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

