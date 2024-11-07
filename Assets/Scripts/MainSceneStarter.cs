using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class MainSceneStarter : MonoBehaviour
{
    public string trackedImageName = "non";
    public string onTracked;

    [SerializeField] private ARTrackedImageManager aRTrackedImageManager;
    [SerializeField] private ARSession aRSession;

    [SerializeField] private ImageTrackingData imageTrackingData;
    [SerializeField] private ImageData imageData;
    [SerializeField] private ModelData modelData;

    void Awake()
    {
        StartCoroutine(WaitForARSession());
    }

    // void Start()
    // {

    // }
    private IEnumerator WaitForARSession()
    {
        yield return ARSession.CheckAvailability();

        while (ARSession.state != ARSessionState.SessionInitializing && ARSession.state != ARSessionState.SessionTracking)
        {
            onTracked = "NonARSession";
            yield return null;
        }

        // ここで画像を追加する処理を行う
        StartCoroutine(AddImageToLibrary());
    }

    private IEnumerator AddImageToLibrary()
    {
        onTracked = "Let's Go";
        if (!aRTrackedImageManager.descriptor.supportsMutableLibrary)
        {
            onTracked = "Don't suport";
            yield break;
        }
        aRTrackedImageManager.referenceLibrary = aRTrackedImageManager.CreateRuntimeLibrary();
        if (aRTrackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            onTracked = "AddImage";
            if (imageTrackingData.imageTrackingManagers.Count == 0)
            {
                onTracked = "Can't use this image";
                yield break;
            }
            foreach (var trackingData in imageTrackingData.imageTrackingManagers)
            {
                ImageManager image = imageData.imageManagers.Find(n => n.imageID == trackingData.imageID);
                onTracked = $"{image.imageID}";
                var addImage = CreateReadableTexture2D(image.image);
                mutableLibrary.ScheduleAddImageWithValidationJob(
                    addImage,
                    $"{image.imageID}",
                    0.5f
                );
            }
            //トラッキングをオンにする
            aRTrackedImageManager.enabled = true;
            if (aRTrackedImageManager.enabled)
            {
                onTracked = "Success";
            }
        }
    }

    void OnEnable()
    {
        onTracked = "OnEnable";
        // トラッキングされた画像が変更された際のイベントを登録
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        onTracked = "OnDisable";
        // トラッキングされた画像が変更された際のイベントを解除
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // トラッキングされた画像が変更された際の処理
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        onTracked = "ImagesChanged";
        // 追加された画像
        foreach (var trackedImage in eventArgs.added)
        {
            // マーカー名を取得
            var name = trackedImage.referenceImage.name;
            // マーカー名とプレハブのマッピングからプレハブを取得
            var model = imageTrackingData.imageTrackingManagers.Find(x => $"{x.imageID}" == name);
            var prefab = modelData.modelManagers.Find(x => x.modelID == model.modelID)?.model;
            if (prefab != null)
            {
                // ARTrackedImageのTransformの位置を少し上に調整
                PositionManager positionData = model.modelPosition;
                var pos = trackedImage.transform.position;
                pos.x += positionData.X;
                pos.y += positionData.Y;
                pos.z += positionData.Z;

                //画像を基準にモデルを配置
                RotationManager rotationData = model.modelRotation;
                var rote = trackedImage.transform.eulerAngles;
                rote.x += rotationData.X;
                rote.y += rotationData.Y;
                rote.z += rotationData.Z;

                var instance = Instantiate(
                    prefab,
                    pos,
                    Quaternion.Euler(rote),
                    trackedImage.transform
                );
                var size = model.modelSize;
                instance.transform.localScale = Vector3.one * size;
            }
        }
        // 更新された画像
        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                trackedImage.gameObject.SetActive(true);
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                trackedImage.gameObject.SetActive(false);
            }
        }
        // 削除された画像
        foreach (var trackedImage in eventArgs.removed)
        {
            trackedImage.gameObject.SetActive(false);
        }
    }

    void AddImages(List<Texture2D> imagesToAdd, List<EventData.ImagePrefabs> imagePrefabs)
    {
        var library = aRTrackedImageManager.CreateRuntimeLibrary();
        int count = 0;
        if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            foreach (Texture2D image in imagesToAdd)
            {
                mutableLibrary.ScheduleAddImageWithValidationJob(image, imagePrefabs[count].imageName, 0.5f);
                count++;
            }
        }
    }

    Texture2D CreateReadableTexture2D(Texture2D originalTexture)
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(
            originalTexture.width,
            originalTexture.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear);

        Graphics.Blit(originalTexture, renderTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;

        Texture2D readableTexture = new Texture2D(originalTexture.width, originalTexture.height);
        readableTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        readableTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);

        return readableTexture;
    }

    public void ChangeScene()
    {
        StartCoroutine(LoadNextSceneAsync("PlaneScene"));
    }

    private IEnumerator LoadNextSceneAsync(string sceneName)
    {
        // 前のロードシーンをアンロード
        SceneManager.UnloadSceneAsync("MainScene");

        // 次のシーンを非同期で読み込み
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }
}
