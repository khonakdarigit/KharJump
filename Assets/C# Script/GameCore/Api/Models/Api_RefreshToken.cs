using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.C__Script.GameCore.Api.Models
{
    //    {
    //  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjY2NTczNjEzZjQwZWRiZmY0NmZjMTUwMyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJraG9uYWtkYXJpIiwianRpIjoiMWFlYWVhZWQtNWY3YS00NDE2LWI5ZDktZWE5NTEwODk2ZTI5IiwiZXhwIjoxNzE2OTk2NjI3LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjA4LyIsImF1ZCI6WyJodHRwczovL2xvY2FsaG9zdDo3MjA4LyIsImh0dHBzOi8vbG9jYWxob3N0OjcyMDgvIl19.wYFWaC_exfErRaxSVZ2IzW4YHBm0OUIlKD4WcGEDp9I",
    //  "refreshToken": "lJ86KhPqKjZuLDMBvT1swgvMyC//f3c9gCUSY4p9th+iJbUOAR4LWL32r7NfiHmBuzc78PRLMNJs+bVUH9dtrg=="
    //}
    [Serializable]
    public class Api_RefreshToken
    {
        public string accessToken;
        public string refreshToken;

    }
}
