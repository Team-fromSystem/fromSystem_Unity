using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

public class FirebaseController : MonoBehaviour
{
    public string dataSt;

    [SerializeField] private FirebaseFirestore db;

    [SerializeField] private List<ImageTrackingManager> imageTrackingList;

    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
    }
    public async Task<List<ImageTrackingManager>> GetImageTrackingData(string documentID)
    {
        List<ImageTrackingManager> allData = new List<ImageTrackingManager>();
        DocumentReference docRef = CreateDocRef(documentID, CreateColRef("events"));
        CollectionReference imageColRef = CreateColRef("Image", docRef);
        QuerySnapshot imageColQuerySnapshot = await imageColRef.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in imageColQuerySnapshot.Documents)
        {
            ImageTrackingManager imageTrackingData = ConvertImageTracking(documentSnapshot);
            allData.Add(imageTrackingData);
        }
        Debug.Log($"{allData.Count}");
        return allData;
    }
    private ImageTrackingManager ConvertImageTracking(DocumentSnapshot documentSnapshot)
    {
        Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
        Dictionary<string, object> oneDoc = documentSnapshot.ToDictionary();
        int imageID = (int)Convert.ChangeType(oneDoc["imageID"], typeof(int));
        int modelID = (int)Convert.ChangeType(oneDoc["modelID"], typeof(int));
        float modelSize = (float)Convert.ChangeType(oneDoc["modelSize"], typeof(float));
        PositionManager modelPosition = ConvertPosition(oneDoc["modelPosition"]);
        RotationManager modelROtation = ConvertRotation(oneDoc["modelRotation"]);
        return new ImageTrackingManager(imageID, modelID, modelSize, modelPosition, modelROtation);
    }
    public async Task<PlaneTrackingManager> GetPlaneTrackingData(string documentID)
    {
        PlaneTrackingManager allData = new PlaneTrackingManager(new List<int>(), new List<int>());
        DocumentReference docRef = CreateDocRef(documentID, CreateColRef("events"));
        CollectionReference planeColRef = CreateColRef("Plane", docRef);
        QuerySnapshot planeColQuerySnapshot = await planeColRef.Limit(1).GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in planeColQuerySnapshot.Documents)
        {
            PlaneTrackingManager planeTrackingData = ConvertPlaneTracking(documentSnapshot);
            allData = planeTrackingData;
        }
        // Debug.Log($"{allData.mainModelID[0]}");
        // .ContinueWithOnMainThread(task =>
        // {
        //     if (task.IsCompleted)
        //     {
        //         QuerySnapshot planeColQuerySnapshot = task.Result;
        //         foreach (DocumentSnapshot documentSnapshot in planeColQuerySnapshot.Documents)
        //         {
        //             PlaneTrackingManager planeTrackingData = ConvertPlaneTracking(documentSnapshot);
        //             allData = planeTrackingData;
        //         }
        //         Debug.Log($"{allData.mainModelID[0]}");
        //     }
        //     else
        //     {
        //         Debug.Log("サブコレクションの取得に失敗しました。");
        //     }
        // });
        return allData;
    }
    private PlaneTrackingManager ConvertPlaneTracking(DocumentSnapshot documentSnapshot)
    {
        Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
        Dictionary<string, object> oneDoc = documentSnapshot.ToDictionary();
        List<object> mainModelIDAsObject = (List<object>)Convert.ChangeType(oneDoc["mainModelID"], typeof(List<object>));
        List<int> mainModelID = mainModelIDAsObject.Select(obj => Convert.ToInt32(obj)).ToList();
        List<object> decorationModelIDAsObject = (List<object>)Convert.ChangeType(oneDoc["decorationModelID"], typeof(List<object>));
        List<int> decorationModelID = decorationModelIDAsObject.Select(obj => Convert.ToInt32(obj)).ToList();
        return new PlaneTrackingManager(mainModelID, decorationModelID);
    }

    public async Task<List<ImmersalManager>> GetImmersalData(string documentID)
    {
        List<ImmersalManager> allData = new List<ImmersalManager>();
        DocumentReference docRef = CreateDocRef(documentID, CreateColRef("events"));
        CollectionReference imageColRef = CreateColRef("Immersal", docRef);
        QuerySnapshot immersalColQuerySnapshot = await imageColRef.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in immersalColQuerySnapshot.Documents)
        {
            ImmersalManager immersalData = ConvertImmersal(documentSnapshot);
            allData.Add(immersalData);
        }
        Debug.Log($"{allData.Count}");
        // .ContinueWithOnMainThread(task =>
        // {
        //     if (task.IsCompleted)
        //     {
        //         QuerySnapshot immersalColQuerySnapshot = task.Result;
        //         foreach (DocumentSnapshot documentSnapshot in immersalColQuerySnapshot.Documents)
        //         {
        //             ImmersalManager immersalData = ConvertImmersal(documentSnapshot);
        //             allData.Add(immersalData);
        //         }
        //         Debug.Log($"{allData.Count}");
        //     }
        //     else
        //     {
        //         Debug.Log("サブコレクションの取得に失敗しました。");
        //     }
        // });
        return allData;
    }
    private ImmersalManager ConvertImmersal(DocumentSnapshot documentSnapshot)
    {
        Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
        Dictionary<string, object> oneDoc = documentSnapshot.ToDictionary();
        LocationManager location = ConvertLocation(oneDoc["location"]);
        float radius = (float)Convert.ChangeType(oneDoc["radius"], typeof(float));
        List<ImmersalMapManager> immersalMap = ConvertImmersalMap(oneDoc["immersalMapManager"]);
        List<ImmersalModelManager> immersalModel = ConvertImmersalModel(oneDoc["immersalModelManager"]);
        ImmersalManager immersalManager = new ImmersalManager(location, radius, immersalMap, immersalModel);
        return immersalManager;
    }
    private List<ImmersalMapManager> ConvertImmersalMap(object oneDocObject)
    {
        List<object> immersalMapAsList = (List<object>)Convert.ChangeType(oneDocObject, typeof(List<object>));
        List<ImmersalMapManager> allData = new List<ImmersalMapManager>();
        foreach (var one in immersalMapAsList)
        {
            Dictionary<string, object> immersalMapAsDic = (Dictionary<string, object>)Convert.ChangeType(one, typeof(Dictionary<string, object>));
            int mapID = (int)Convert.ChangeType(immersalMapAsDic["mapID"], typeof(int));
            PositionManager mapPosition = ConvertPosition(immersalMapAsDic["mapPosition"]);
            RotationManager mapRotation = ConvertRotation(immersalMapAsDic["mapRotation"]);
            ImmersalMapManager immersalMapManager = new ImmersalMapManager(mapID, mapPosition, mapRotation);
            allData.Add(immersalMapManager);
        }
        return allData;
    }
    private List<ImmersalModelManager> ConvertImmersalModel(object oneDocObject)
    {
        List<object> immersalModelAsList = (List<object>)Convert.ChangeType(oneDocObject, typeof(List<object>));
        List<ImmersalModelManager> allData = new List<ImmersalModelManager>();
        foreach (var one in immersalModelAsList)
        {
            Dictionary<string, object> immersalModelAsDic = (Dictionary<string, object>)Convert.ChangeType(one, typeof(Dictionary<string, object>));
            int modelID = (int)Convert.ChangeType(immersalModelAsDic["modelID"], typeof(int));
            float modelSize = (float)Convert.ChangeType(immersalModelAsDic["modelSize"], typeof(float));
            PositionManager modelPosition = ConvertPosition(immersalModelAsDic["modelPosition"]);
            RotationManager modelRotation = ConvertRotation(immersalModelAsDic["modelRotation"]);
            ImmersalModelManager immersalModelManager = new ImmersalModelManager(modelID, modelSize, modelPosition, modelRotation);
            allData.Add(immersalModelManager);
        }
        return allData;
    }

    private LocationManager ConvertLocation(object oneDocObject)
    {
        Dictionary<string, object> locationDataAsDic = (Dictionary<string, object>)Convert.ChangeType(oneDocObject, typeof(Dictionary<string, object>));
        float latitude = (float)Convert.ChangeType(locationDataAsDic["latitude"], typeof(float));
        float longitude = (float)Convert.ChangeType(locationDataAsDic["longitude"], typeof(float));
        LocationManager locationManager = new LocationManager(latitude, longitude, 0.0);
        return locationManager;
    }

    public async Task<List<GetImageManager>> GetImageData(List<object> imageID)
    {
        List<GetImageManager> allData = new List<GetImageManager>();
        Query imageColQuery = CreateColRef("images").WhereIn("imageID", imageID);
        QuerySnapshot getImageColQuerySnapshot = await imageColQuery.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in getImageColQuerySnapshot.Documents)
        {
            GetImageManager getImageData = ConvertGetImage(documentSnapshot);
            allData.Add(getImageData);
        }
        Debug.Log($"{allData.Count}");
        // .ContinueWithOnMainThread(task =>
        // {
        //     if (task.IsCompleted)
        //     {
        //         QuerySnapshot getImageColQuerySnapshot = task.Result;
        //         foreach (DocumentSnapshot documentSnapshot in getImageColQuerySnapshot.Documents)
        //         {
        //             GetImageManager getImageData = ConvertGetImage(documentSnapshot);
        //             allData.Add(getImageData);
        //         }
        //         Debug.Log($"{allData.Count}");
        //     }
        //     else
        //     {
        //         Debug.Log("コレクションの取得に失敗しました。");
        //     }
        // });
        return allData;
    }

    private GetImageManager ConvertGetImage(DocumentSnapshot documentSnapshot)
    {
        Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
        Dictionary<string, object> oneDoc = documentSnapshot.ToDictionary();
        int imageID = (int)Convert.ChangeType(oneDoc["imageID"], typeof(int));
        int hostID = (int)Convert.ChangeType(oneDoc["hostID"], typeof(int));
        string imageURL = (string)Convert.ChangeType(oneDoc["imageURL"], typeof(string));
        string imageName = (string)Convert.ChangeType(oneDoc["imageName"], typeof(string));
        string fileFormat = (string)Convert.ChangeType(oneDoc["fileFormat"], typeof(string));
        GetImageManager getImageManager = new GetImageManager(imageID, hostID, imageURL, imageName, fileFormat);
        return getImageManager;
    }

    public async Task<List<GetModelManager>> GetModelData(List<object> modelID)
    {
        List<GetModelManager> allData = new List<GetModelManager>();
        Query modelColQuery = CreateColRef("models").WhereIn("modelID", modelID);
        QuerySnapshot getModelColQuerySnapshot = await modelColQuery.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in getModelColQuerySnapshot.Documents)
        {
            GetModelManager getModelData = ConvertGetModel(documentSnapshot);
            allData.Add(getModelData);
        }
        Debug.Log($"{allData.Count}");
        // .ContinueWithOnMainThread(task =>
        // {
        //     if (task.IsCompleted)
        //     {
        //         QuerySnapshot getModelColQuerySnapshot = task.Result;
        //         foreach (DocumentSnapshot documentSnapshot in getModelColQuerySnapshot.Documents)
        //         {
        //             GetModelManager getModelData = ConvertGetModel(documentSnapshot);
        //             allData.Add(getModelData);
        //         }
        //         Debug.Log($"{allData.Count}");
        //     }
        //     else
        //     {
        //         Debug.Log("コレクションの取得に失敗しました。");
        //     }
        // });
        return allData;
    }

    private GetModelManager ConvertGetModel(DocumentSnapshot documentSnapshot)
    {
        Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
        Dictionary<string, object> oneDoc = documentSnapshot.ToDictionary();
        int modelID = (int)Convert.ChangeType(oneDoc["modelID"], typeof(int));
        int hostID = (int)Convert.ChangeType(oneDoc["hostID"], typeof(int));
        string modelURL = (string)Convert.ChangeType(oneDoc["modelURL"], typeof(string));
        string modelName = (string)Convert.ChangeType(oneDoc["modelName"], typeof(string));
        string fileFormat = (string)Convert.ChangeType(oneDoc["fileFormat"], typeof(string));
        PositionManager colliderPosition = ConvertPosition(oneDoc["colliderPosition"]);
        float colliderRadius = (float)Convert.ChangeType(oneDoc["colliderRadius"], typeof(float));
        GetModelManager getModelManager = new GetModelManager(modelID, hostID, modelURL, modelName, fileFormat, colliderPosition, colliderRadius);
        return getModelManager;
    }

    private FirestoreManager ConvertFirestore(Dictionary<string, object> eventData)
    {
        var modelID = (List<object>)Convert.ChangeType(eventData["modelID"], typeof(List<object>));
        var imageID = (List<object>)Convert.ChangeType(eventData["imageID"], typeof(List<object>));
        var detectTypeAsObj = (List<object>)Convert.ChangeType(eventData["detectType"], typeof(List<object>));
        var detectType = detectTypeAsObj.Select(obj => Convert.ToInt32(obj)).ToList();
        Debug.Log($"{modelID.Count}");
        Debug.Log($"{imageID.Count}");
        Debug.Log($"{detectType.Count}");
        return new FirestoreManager(modelID, imageID, detectType);
    }
    private CollectionReference CreateColRef(string colName, DocumentReference docRef = null)
    {
        if (docRef == null)
        {
            return db.Collection(colName);
        }
        else
        {
            return docRef.Collection(colName);
        }
    }

    private DocumentReference CreateDocRef(string docName, CollectionReference colRef)
    {
        return colRef.Document(docName);
    }

    private Query CreateWhereInQuery(string fieldName, string target, CollectionReference colRef)
    {
        int[] targetID = Array.ConvertAll(target.Split(","), int.Parse);
        object[] targetIDAsObjects = targetID.Cast<object>().ToArray();
        return colRef.WhereIn(fieldName, targetIDAsObjects);
    }
    private PositionManager ConvertPosition(object oneDocObject)
    {
        Dictionary<string, object> modelPosition = (Dictionary<string, object>)Convert.ChangeType(oneDocObject, typeof(Dictionary<string, object>));
        PositionManager positionData = new PositionManager((float)Convert.ChangeType(modelPosition["X"], typeof(float)), (float)Convert.ChangeType(modelPosition["Y"], typeof(float)), (float)Convert.ChangeType(modelPosition["Z"], typeof(float)));
        return positionData;
    }
    private RotationManager ConvertRotation(object oneDocObject)
    {
        Dictionary<string, object> modelRotation = (Dictionary<string, object>)Convert.ChangeType(oneDocObject, typeof(Dictionary<string, object>));
        RotationManager rotaionData = new RotationManager((float)Convert.ChangeType(modelRotation["X"], typeof(float)), (float)Convert.ChangeType(modelRotation["Y"], typeof(float)), (float)Convert.ChangeType(modelRotation["Z"], typeof(float)));
        return rotaionData;
    }
}