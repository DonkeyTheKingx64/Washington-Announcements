using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEngine.SceneManagement;

//this class handles what happens when an announcement card is clicked
public class open_panel : MonoBehaviour
{
    //serialized fields allow us to refer to other objects in the inspector
    //without making the fields public

    //refrence to the card
    [SerializeField] GameObject panel;

    //an announcement object
    announcement ant;

    //called when the user clicks on an announcement
    public void clicked() {
        Text code = panel.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        Text title = panel.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
        RestClient.Get<announcement>("https://final-project-1e038.firebaseio.com/" + code.text.ToUpper() + "/" + title.text + ".json").Then(response =>
                {
                    ant = response;
                    more();
                });
    }

    //since we've got the information about what announcement we're talking
    //about, we can use this announcment's information to make our view announcement page
    private void more() {
        //since we're loading a new scene to view an announcement, we need to pass the info
        //about the announcement to the next scene, we do this by saving the info about
        //the annoucnemnt locally and retireve the same in the new scene
        PlayerPrefs.SetString("code", ant.class_name.ToUpper());
        PlayerPrefs.SetString("title", ant.title);
        PlayerPrefs.SetString("content", ant.content);
        SceneManager.LoadScene("view_announcement", LoadSceneMode.Single);
    }
}
