using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Proyecto26;

//saves a list of courses to get announcements from on the device
public class savecourses : MonoBehaviour
{
    //serialized fields allow us to refer to other objects in the inspector
    //without making the fields public

    //refrence to the inputfield's text
    [SerializeField] Text t;

    //a refrence to the animator for scene transition
    [SerializeField] Animator anim;

    //saves the list of courses typed
    public void clearList() {
        listofcourses loc = new listofcourses();
        RestClient.Put("https://final-project-1e038.firebaseio.com/userss/" + signup.localId + ".json", loc);
    }

    //loads the main announcement page
    public void goBack() {
        listofcourses courseslist = new listofcourses();
        RestClient.Get<listofcourses>("https://final-project-1e038.firebaseio.com/userss/" + signup.localId + ".json").Then(response => 
        {
            string st = response.courses;
            if (st.Length == 0)
            {
                st += t.text.ToUpper();
            }
            else {
                st += ", " + t.text.ToUpper();
            }
            courseslist.courses = st;
            RestClient.Put("https://final-project-1e038.firebaseio.com/userss/" + signup.localId + ".json", courseslist);
            anim.SetTrigger("end");
            StartCoroutine("loadScene");
        });
    }

    //loads the main announcement page
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.1f);
        //Application.LoadLevel("students");
        Application.LoadLevelAsync("students");
    }
}
