// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Firebase.Extensions;
// using Firebase.Firestore;

// public class FirebaseController : MonoBehaviour
// {
//     private bool setData = false;
//     public string dataSt;
//     public void GetFirestoreData()
//     {
//         //Firestoreを使えるようにする
//         var db = FirebaseFirestore.DefaultInstance;
//         DocumentReference docRef = db.Collection("events").Document("9Kwe3wUTRAeHxVRP7B4N");
//         // CollectionReference imageColRef = docRef.Collection("Image");
//         Query imageColQuery = docRef.Collection("Image");
//         imageColQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
//         {
//             if (task.IsCompleted)
//             {
//                 QuerySnapshot imageColQuerySnapshot = task.Result;
//                 foreach (DocumentSnapshot documentSnapshot in imageColQuerySnapshot.Documents)
//                 {
//                     Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
//                     Dictionary<string, object> city = documentSnapshot.ToDictionary();
//                     foreach (KeyValuePair<string, object> pair in city)
//                     {
//                         Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
//                         if ($"{pair.Key}" == "imageID")
//                         {
//                             dataSt = string.Format("{0}: {1}", pair.Key, pair.Value);
//                         }
//                     }

//                     // Newline to separate entries
//                     Debug.Log("");
//                 }
//             }
//             else
//             {
//                 Debug.Log("サブコレクションの取得に失敗しました。");
//             }
//         });
//         // docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
//         // {
//         //     DocumentSnapshot snapshot = task.Result;
//         //     if (snapshot.Exists)
//         //     {
//         //         Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
//         //         Dictionary<string, object> city = snapshot.ToDictionary();
//         //         foreach (KeyValuePair<string, object> pair in city)
//         //         {
//         //             Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
//         //             if($"{pair.Key}"=="close"){
//         //                 dataSt=string.Format("{0}: {1}", pair.Key, pair.Value);
//         //             }
//         //         }
//         //     }
//         //     else
//         //     {
//         //         Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
//         //     }
//         // });
//     }
// }
