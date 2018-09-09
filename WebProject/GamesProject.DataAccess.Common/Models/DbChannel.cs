using System;
using System.Collections.Generic;

namespace GamesProject.DataAccess.Common.Models
{
    public class DbChannel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        //public virtual ICollection<DbGame> Platform { get; set; }

        //public virtual ICollection<DbGame> Categories { get; set; }

        //public DateTime PublicationDate { get; set; }

        public DateTime? LastModified { get; set; }

        public virtual ICollection<DbGame> Games { get; set; }
    }
}
