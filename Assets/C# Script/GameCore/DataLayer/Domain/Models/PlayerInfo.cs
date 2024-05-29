using MongoDB.Bson;
using Realms;

namespace Assets.DataLayer.Domain.Models
{
    public class PlayerInfo : RealmObject
    {
        [PrimaryKey]
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        public string SysName { get; internal set; }
        public GameSetting Setting { get; set; }
        public string? ServerPlayerId { get; set; }
        // Other field
        public RealmInteger<int> Record { get; set; }
    }
}
