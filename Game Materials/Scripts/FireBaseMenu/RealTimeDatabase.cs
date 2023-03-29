using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

public class RealTimeDatabase : MonoBehaviour
{
    DatabaseReference dbRef;

    UserData userDataTransfer;

    [SerializeField]
    PlayerDataSO playerData;

    

    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text Score;

    [SerializeField]
    private GameObject leaderBoard;

    [SerializeField]
    private GameObject textForLeaderBoard;

    [SerializeField]
    private Text scoreNow;

    [SerializeField]
    private Text[] LeaderBoardFields;

    
    private void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log("Start Db");

        StartCoroutine(LoadData(playerData.UserId));
        StartCoroutine(LoadAllUserByScore());

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

            Name.text = userDataTransfer.name;

            Score.text = userDataTransfer.score.ToString();
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
                    LeaderBoardFields[i].text = reverseList[i].Child("name").Value.ToString() + ":  " + reverseList[i].Child("score").Value.ToString();
                }
                else
                {
                    LeaderBoardFields[i].text = "";
                }
            }
        }


    }

    private void SaveData(string Name, int score)
    {
            UserData user2229 = new UserData(Name, score);

            string json = JsonUtility.ToJson(user2229);

            dbRef.Child("users").Child(playerData.UserId).SetRawJsonValueAsync(json);
    
    }

    private bool CheckRecord()
    {
        if(int.Parse(scoreNow.text) > int.Parse(Score.text))
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
            Score.text = scoreNow.text;

            SaveData(Name.text, int.Parse(scoreNow.text));
        }
    }


    



    
    

}
