using Realms;

public class PlayerProfile : RealmObject
{
    public PlayerProfile() { }
    public PlayerProfile(string userId)
    {
        UserId = userId;
        HighScore = 0;
    }

    [PrimaryKey]
    [MapTo("_id")]
    public string UserId { get; set; }
    [MapTo("high_score")]
    public int HighScore { get; set; }
}
