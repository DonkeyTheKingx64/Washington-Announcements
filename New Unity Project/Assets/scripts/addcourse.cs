using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//opens the add a course scene
public class addcourse : MonoBehaviour
{
    //serialized fields allow us to refer to other objects in the inspector
    //without making the fields public

    //a refrence to the animator for scene transition
    [SerializeField] Animator anim;

    public void go() {
        anim.SetTrigger("end");
        StartCoroutine("loadScene");
    }

    //opens the "add new course" page
    IEnumerator loadScene() {
        yield return new WaitForSeconds(0.1f);
        Application.LoadLevelAsync("newcourse"); //CHANGE TO newcourse
    }

    //refreshes the scene
    public void refresh() {
        SceneManager.LoadScene("students");
    }
}
