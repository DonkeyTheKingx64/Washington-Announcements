using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//searlization converts the object to a set of bytes in order
//to store it in a database (or memory or a file, but for our purpose,
//a database)
[Serializable]

//this class saves the list of courses the user is registered for
//it needs to be done this way and not simply create a string because of json parsing
public class listofcourses
{
    public string courses;
}
