using System;
using System.Collections.Generic;

namespace GamesProject.Logic.Common.Models
{
    public class Channel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string LinkRSS { get; set; }

        public DateTime? LastModified { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
