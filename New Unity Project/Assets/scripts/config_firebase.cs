using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Numerics;

//this is basically the teacher half (pushing to the database)
//we're using firebase realtime database in this project
//and the rest client api (makes it easier to build for other
//platforms too)
public class config_firebase : MonoBehaviour
{
    
    //the class' name, eg: CSE 143
    public static string class_name;
    
    //the title of the announcement to be made
    public static string title;
    
    //the actual announcement
    public static string content;

    //serialized fields allow us to refer to other objects in the inspector
    //without making the fields public

    //the text object that displays the class' name 
    [SerializeField] private InputField class_name_text;
    
    //the text object that displays the title 
    [SerializeField] private InputField title_text;
    
    //the text object that displays the content
    [SerializeField] private InputField content_text;

    //this gets called when the teacher half of the app hits the submit button
    public void OnSubmit() {
        class_name = class_name_text.text;
        title = title_text.text;
        content = content_text.text;
        GoToDatabase();
    }

    //this gets called when the teacher half of the app hits the submit button
    private void GoToDatabase() {
        announcement anc = new announcement();
        //structures the json databse like so:
        /* root
         *  | |_ class name
         *  |     | |_ title of the first announcement
         *  |     |       |_ details of this announcement
         *  |     |___ title of the next announcement
         *  |             |_ details of this one
         *  |____ another class
         *        .
         *        .
         *        .
         */        
        RestClient.Put("https://final-project-1e038.firebaseio.com/"+class_name.ToUpper()+"/"+title+".json", anc);
    }
}
