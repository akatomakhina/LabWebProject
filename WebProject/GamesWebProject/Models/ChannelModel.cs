using System;

namespace EntertainmentPortal.Web.Models.Sports
{
    public class ChannelModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string LinkRSS { get; set; }

        public DateTime? LastModified { get; set; }
    }
}