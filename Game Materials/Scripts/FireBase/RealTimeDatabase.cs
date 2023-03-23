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
    
    private void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        StartCoroutine(LoadData(playerData.UserName));

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
        StartCoroutine(LoadBoardData());
    }


    private IEnumerator LoadBoardData()
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

            GameObject gO;

            List<DataSnapshot> reverseList = new List<DataSnapshot>();


            foreach (DataSnapshot clidSnapshot in snapshot.Children)
            {
                reverseList.Add(clidSnapshot);
            }

            reverseList.Reverse();

            for (int i = 0; i < 5; i++)
            {
                if (reverseList.Count > i)
                {
                    gO = Instantiate(textForLeaderBoard, leaderBoard.transform);

                    gO.GetComponent<Text>().text = reverseList[i].Child("name").Value.ToString();
                }
            }


           

        }


    }


    
    

}
