using GamesProject.Logic.Common.Models;
using System;

namespace EntertainmentPortal.Web.Models.Sports
{
    public class GameApiModel : BaseApiModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? PubDate { get; set; }

        public string Link { get; set; }

        public string Enclosure { get; set; }

        public string Guid { get; set; }

        public GameApiModel(Game game)
        {
            Id = game.Id;
            Title = game.Title;
            Link = game.Link;
            Description = game.Description;
            PubDate = game.PubDate;
            Enclosure = game.Enclosure;
            Guid = game.Guid;            
        }
        public override string _self { get; set; }
    }
}