using GamesProject.Logic.Common.Models;
using GamesWebProject.Models.Abstract;
using System;

namespace GamesWebProject.Models
{
    public class FavoriteGamesApiModel : BaseApiModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GameId { get; set; }

        public DateTime? LastModified { get; set; }

        public FavoriteGamesApiModel(FavoriteGames favoriteGame)
        {
            Id = favoriteGame.Id;
            LastModified = favoriteGame.LastModified;
            GameId = favoriteGame.GameId;
            UserId = favoriteGame.UserId;
        }
        public override string _self { get; set; }
    }
}