using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.C__Script.GameCore.Api.Models
{
    //{
    //  "gameIdTag": "string",
    //  "devicePlayerId": "string",
    //  "firstName": "string",
    //  "lastName": "string",
    //  "email": "string",
    //  "username": "stringst",
    //  "password": "stringst"
    //}
    [Serializable]
    public class Api_PlayerInfo
    {
        public string id;
        public string gameIdTag;
        public string devicePlayerId;
        public string firstName;
        public string lastName;
        public string sysName;
        public string username;
        public string password;
    }
}
