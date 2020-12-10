using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
using Proyecto26;

//this is basically the student half (reteriving from the database)
//we're using firebase realtime database in this project
//and the rest client api (makes it easier to build for other
//platforms too)
public class get_firebase : MonoBehaviour
{
    //serialized fields allow us to refer to other objects in the inspector
    //without making the fields public

    //once the announcements are fetched, this creates a "card" with basic
    //info like the class' name and the title of the announcement
    [SerializeField] GameObject card;
    
    //all UI elements need to be child objects of the canvas, to make that happen
    //we need to refer to the scrollview (which is a child of canvas)
    [SerializeField] RectTransform scrollview;
    
    //a refrence to the firebase database
    private DatabaseReference dbref;

    //the Y value of placing the cards
    private int y = 0;

    //a list of classes we need to get the announcements for
    String[] list;

    //awake is called as soon as the scene is loaded
    public void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://final-project-1e038.firebaseio.com/");
        //refrencing to the firebase
        dbref = FirebaseDatabase.DefaultInstance.RootReference;
        RestClient.Get<listofcourses>("https://final-project-1e038.firebaseio.com/userss/" + signup.localId + ".json").Then(response =>
        {
        string str_temp = response.courses;
        string str;
        str = str_temp.Replace(", ", ",");
        list = str.Split(',');
            if (list.Length != 0)
            {
                foreach (string s in list)
                {
                    Debug.Log(s);
                    //ValueChanged is used to subscribe on changes of the contents at a given path
                    //this event is triggered once when the listener is attached and again every time 
                    //the data, including children, changes
                    //when/if the said data is changed, the "changed" function is called
                    FirebaseDatabase.DefaultInstance.GetReference(s).ValueChanged += changed;
                }
            }
        });
    }

    //this gets called when any sort of change is made in the firebase 
    //regarding a particular course
    private void changed(object o, ValueChangedEventArgs args) {
        //DataSnapshot is a firebase term, it contains data from a specified db location

        //args.Snapshot.Children is an iterable of type DataSnapshot
        if (list.Length != 0)
        {
            foreach (DataSnapshot dss in args.Snapshot.Children)
            {
                announcement anf = new announcement();
                anf.class_name = dss.Child("class_name").GetValue(true).ToString();
                anf.title = dss.Child("title").GetValue(true).ToString();
                anf.content = dss.Child("content").GetValue(true).ToString();
                Debug.Log("here!" + anf.class_name);
                doTheRest(anf);
                y -= 375;
            }
        }
    }

    //creates the "card" given the information about announcement in question
    private void doTheRest(announcement ause)
    {
        GameObject new_card = GameObject.Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
        new_card.transform.SetParent(scrollview.transform);
        new_card.GetComponent<RectTransform>().offsetMin = new Vector2(0, y);
        new_card.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        Text code = new_card.transform.GetChild(1).gameObject.GetComponent<Text>();
        Text title = new_card.transform.GetChild(2).gameObject.GetComponent<Text>();
        code.text = ause.class_name.ToUpper();
        title.text = ause.title;
    }

    //adds courses to subscribe to
    public void clickedAdd() {
        SceneManager.LoadScene("newcourse", LoadSceneMode.Single);
    }
}
