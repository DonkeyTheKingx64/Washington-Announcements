using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEngine.SceneManagement;

//this class retrives the info about the announcement user wants to see
//and displays it
public class viewannouncement : MonoBehaviour
{
    //serialized fields allow us to refer to other objects in the inspector
    //without making the fields public

    //the class' name
    [SerializeField] Text big_code;

    //the title of the announcement
    [SerializeField] Text big_title;

    //the content of the announcement 
    [SerializeField] Text big_content;

    //handles the fade animation
    [SerializeField] Animator anim;

    //an announcement object
    announcement ans;

    //start is called only once when the scene is loaded
    private void Start()
    {
        RestClient.Get<announcement>("https://final-project-1e038.firebaseio.com/" + PlayerPrefs.GetString("code") + "/" + PlayerPrefs.GetString("title") + ".json").Then(response =>
        {
            ans = response;
            display();
        });
    }

    //displays the info about the announcement
    private void display() {
        big_code.text = ans.class_name.ToString().ToUpper();
        big_title.text = ans.title.ToString();
        big_content.text = ans.content.ToString();
    }

    //to go back to the scene where we can see all the announcements
    public void goBack() {
        anim.SetTrigger("end");
        StartCoroutine("loadScene");
    }
    //to go back to the scene where we can see all the announcements
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.1f);
        Application.LoadLevelAsync("students");
    }
}
