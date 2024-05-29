using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.C__Script.GameCore.Api.Models
{
    //{
    //  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjY2NTczNjEzZjQwZWRiZmY0NmZjMTUwMyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJraG9uYWtkYXJpIiwianRpIjoiMWFlYWVhZWQtNWY3YS00NDE2LWI5ZDktZWE5NTEwODk2ZTI5IiwiZXhwIjoxNzE2OTk0ODUyLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjA4LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMDgvIn0.i6MufcWkVHJF_hdPoR0YF_fLLml_9lG03_MFNM5r1uc",
    //  "refreshToken": "T7ORq3zS8J5Wzpiafj5/iso1zgt3kn6tPMOV0E24HlUyYpgRRA2XU/ZI0rZovp+yYzPn5OEHTyExNrHqvq7XmA==",
    //  "expiration": "2024-05-29T15:00:52Z"
    //}
    [Serializable]
    public class Api_Token
    {
        public string token;
        public string refreshToken;
        public DateTime expiration;
    }
}
