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
        UserData user = new UserData("TestUser1", 0);

        string json = JsonUtility.ToJson(user);

        dbRef.Child("users").Child("TestUser1").SetRawJsonValueAsync(json);
    }

    public class UserData
    {
        public string name;
        public int score;

        public UserData(string name, int score )
        {
            this.name = name;
            this.score = score;

        }

    }



}
