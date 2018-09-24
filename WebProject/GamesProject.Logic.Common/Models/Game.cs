using System;

namespace GamesProject.Logic.Common.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? PubDate { get; set; }

        public string Link { get; set; }

        public string Enclosure { get; set; }

        public string Guid { get; set; }
    }
}
