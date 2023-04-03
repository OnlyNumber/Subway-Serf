using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

public class RealTimeDatabase : MonoBehaviour
{
    DatabaseReference dbRef;

    UserData userDataTransfer;

    //[SerializeField]
    //PlayerDataSO playerData;

    

    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject leaderBoard;

    [SerializeField]
    private GameObject textForLeaderBoard;

    [SerializeField]
    private Text scoreNow;

    [SerializeField]
    private Text[] leaderBoardFields;

    
    private void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log("Start Db");

        Debug.Log(DataHolder.id);

        StartCoroutine(LoadData(DataHolder.id));
        StartCoroutine(LoadAllUserByScore());

    }

    private void OnApplicationQuit()
    {
        SaveRecord();
    }


    private IEnumerator LoadData(string userName)
    {
        var user = dbRef.Child("users").Child(userName).GetValueAsync();

        yield return new WaitUntil(predicate: () => user.IsCompleted);

        if(user.Exception != null)
        {
            Debug.Log(user.Exception);
        }
        else if(user.Result == null)
        {
            Debug.Log("Null");
        }
        else
        {
            DataSnapshot snapshot = user.Result;

            userDataTransfer = new UserData(snapshot.Child("name").Value.ToString(), int.Parse(snapshot.Child("score").Value.ToString()));

            nameText.text = userDataTransfer.name;

            scoreText.text = userDataTransfer.score.ToString();
        }
    }

    public void ShowTable()
    {
        StartCoroutine(LoadAllUserByScore());
    }


    private IEnumerator LoadAllUserByScore()
    {
        var user = dbRef.Child("users").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => user.IsCompleted);

        if(user.Exception != null)
        {
            Debug.LogError(user.Exception);
        }
        else if(user.Result.Value == null)
        {
            Debug.Log("Null");
        }
        else
        {
            DataSnapshot snapshot = user.Result;

            List<DataSnapshot> reverseList = new List<DataSnapshot>();


            foreach (DataSnapshot clidSnapshot in snapshot.Children)
            {
                reverseList.Add(clidSnapshot);
            }

            reverseList.Reverse();

            for (int i = 0; i < 10; i++)
            {
                if (reverseList.Count > i)
                {
                    leaderBoardFields[i].text = reverseList[i].Child("name").Value.ToString() + ":  " + reverseList[i].Child("score").Value.ToString();
                }
                else
                {
                    leaderBoardFields[i].text = "";
                }
            }
        }


    }

    private void SaveData(string Name, int score)
    {
            UserData user2229 = new UserData(Name, score);

            string json = JsonUtility.ToJson(user2229);

            dbRef.Child("users").Child(DataHolder.id).SetRawJsonValueAsync(json);
    
    }

    private bool CheckRecord()
    {
        if(int.Parse(scoreNow.text) > int.Parse(scoreText.text))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void SaveRecord()
    {
        if(CheckRecord())
        {
            scoreText.text = scoreNow.text;

            SaveData(nameText.text, int.Parse(scoreNow.text));
        }
    }

    //public void SignOut();
    



    
    

}
