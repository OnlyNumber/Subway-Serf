using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;


public class RTDBTest : MonoBehaviour
{
    DatabaseReference dbRef;

    private void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData()
    {
        dbRef.Child("Uadsasdawd").SetValueAsync("adawwda");
    }


}
