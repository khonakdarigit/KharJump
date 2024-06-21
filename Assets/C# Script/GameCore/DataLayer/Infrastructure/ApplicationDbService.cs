using Assets.DataLayer.Application.Services.DomainService;
using Assets.DataLayer.Domain.Models;
using Assets.DataLayer.Infrastructure.Services.DomainService;
using Realms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DataLayer.Infrastructure
{
    public class ApplicationDbService
    {
        private readonly Realm _realm;

        public readonly GenericRepository<PlayerInfo> PlayerInfo;


        public ApplicationDbService()
        {
            var config = new RealmConfiguration { SchemaVersion = 6 };
            _realm = Realm.GetInstance(config);


            PlayerInfo = new GenericRepository<PlayerInfo>(_realm);

            //databaseReset();
            SeedDataInitialize();
        }

        private void SeedDataInitialize()
        {
            if (!PlayerInfo.AsQueryable().Any())
            {
                PlayerInfo.Create(new PlayerInfo()
                {
                    SysName = $"Player_{UnityEngine.Random.Range(1000, 9999).ToString()}",
                    Setting = new GameSetting() { GameMute = false }
                });
            }
        }

        public void databaseReset()
        {
            _realm.Write(() =>
            {
                _realm.RemoveAll();
            });
        }
    }
}
