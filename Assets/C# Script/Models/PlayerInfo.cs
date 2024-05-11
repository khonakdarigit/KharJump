using Realms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : RealmObject
{
    [PrimaryKey]
    public string gamerTag { get; set; }
    public string UserName { get; set; }
    public RealmInteger<int> Record { get; set; }
    public string API_DB_userID { get; set; }
}
