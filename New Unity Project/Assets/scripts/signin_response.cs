using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//searlization converts the object to a set of bytes in order
//to store it in a database (or memory or a file, but for our purpose,
//a database)
[Serializable]

//this calss saves the local id and the id token of the current login session
//it needs to be done this way and not simply create strings because of json parsing
public class signin_response
{
    public string localId;
    public string idToken;
}
