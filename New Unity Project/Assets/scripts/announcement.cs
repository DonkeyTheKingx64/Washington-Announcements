using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//searlization converts the object to a set of bytes in order
//to store it in a database (or memory or a file, but for our purpose,
//a database)
[Serializable]

//most of the scripts used in unity inherit from MonoBehaviour but we
//don't need that for our class since we're using it as a regular class
//and don't intend to do anything with it in terms of actual development
//of the app (in the editor)

//this class allows us to make announcements with fields: class name,
//the title and the content
public class announcement
{
    //the class' name, eg: CSE 143
    public string class_name;
    
    //the title of the announcement to be made
    public string title;
    
    //the actual announcement
    public string content;

    //constructor to set the fields using whatever announcement is in question
    //which is retrieved from the config_firebase.cs file
    public announcement() {
        class_name = config_firebase.class_name;
        title = config_firebase.title;
        content = config_firebase.content;
    }
}
