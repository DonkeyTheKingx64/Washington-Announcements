using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

//this class is responsible for the sign up and login features
public class signup : MonoBehaviour
{
    //serialized fields allow us to refer to other objects in the inspector
    //without making the fields public

    //refrence to the email
    [SerializeField] InputField email;

    //refrence to the password
    [SerializeField] InputField password;

    //refrence to the animator (fade in and out)
    [SerializeField] Animator anim;

    //remember the credentials on this device?
    [SerializeField] Toggle remember;

    //to report any errors encountered
    [SerializeField] Text errors;

    //local id is the uniquie id or the UID
    public static string localId;

    //id tokes expire within an hour, they're used in 
    //a "yeah, we can allow him to edit the databse" way
    public string idToken;

    //authorization key is provided by firebase
    private string authKey = "AIzaSyDIN47G1n-i3LcxTl5B1hnGAyKZhjZyTiU";

    //Start is called before the first frame update
    public void Start()
    {
        errors.text = "";
        if (PlayerPrefs.GetInt("remember", 0) == 1) {
            email.text = PlayerPrefs.GetString("email", "");
            password.text = PlayerPrefs.GetString("password", "");
            SignIn();
        }
    }

    //the sign up feature
    public void SignUp() {
        if (email.text.EndsWith("@uw.edu"))
        {
            string datainjsonformat = "{\"email\":\"" + email.text + "\",\"password\":\"" + password.text + "\",\"returnSecureToken\":true}";
            RestClient.Post<signin_response>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + authKey, datainjsonformat).Then(response =>
            {
                string emailVerify = "{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + response.idToken + "\"}";
                RestClient.Post("https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + authKey, emailVerify);
                localId = response.localId;
                idToken = response.idToken;
                listofcourses courses = new listofcourses();
                RestClient.Put("https://final-project-1e038.firebaseio.com/userss/" + localId + ".json", courses);
                errors.text = "a verification email will be sent \n in a few minutes, please confirm";
            }).Catch(error =>
            {
                errors.text = "account with the same email \n already exists";
            });
        }
        else {
            errors.text = "email must be a UW email";
        }
    }

    //the sign in feature
    public void SignIn()
    {
        string datainjsonformat = "{\"email\":\"" + email.text + "\",\"password\":\"" + password.text + "\",\"returnSecureToken\":true}";
        RestClient.Post<signin_response>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + authKey, datainjsonformat).Then(response =>
        {
            string idtokenuse = "{\"idToken\":\""+ response.idToken + "\"}";
            RestClient.Post("https://identitytoolkit.googleapis.com/v1/accounts:lookup?key=" + authKey, idtokenuse).Then(emailresponse =>
            {
                
                //we have to go a little dirty here, for some reason the email response gives us
                //a user array with details for each user - except there is always only 1 user. 
                //the problem is that there is no straightfoward way to serialize arrays, we'd have to 
                //import a 3rd party plugin like fsSerializer by Jacob Dufault from github to make it work
                //but I don't want to have to do that, so using the fact there is always only 1 element in the array
                //that this post request returns, instead of trying to parse it into anything, we just read the json file
                //as is and find find if the 16th character that comes after the beginning of the string "emailVerified" : "true"
                //is t (for true) or f (for false) xD 
                
                //the proper way to do it would've been to parse it into an object and check the value of the emailVerified
                //bool xD
                
                string hack = emailresponse.Text;
                int start = hack.IndexOf("emailVerified");
                char[] hackarray = hack.ToCharArray();
                if(hackarray[start+16] == 't') 
                {
                    //save the info only if it is correct
                    if (remember.isOn && PlayerPrefs.GetInt("remember", 0) == 0)
                    {
                        PlayerPrefs.SetInt("remember", 1);
                        PlayerPrefs.SetString("email", email.text);
                        PlayerPrefs.SetString("password", password.text);
                    }
                    localId = response.localId;
                    idToken = response.idToken;
                    listofcourses courses = new listofcourses();
                    go();
                }
                else {
                    errors.text = "email not yet verified";
                }
            });
        }).Catch(error =>
        {
            errors.text = "incorrect email or password";
        });
    }

    //called if everything is successful and the user may proceed
    //to the main screen
    public void go()
    {
        anim.SetTrigger("end");
        StartCoroutine("loadScene");
    }

    //opens the "add new course" page
    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.1f);
        Application.LoadLevelAsync("students");
    }
}
