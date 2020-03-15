using System;
using System.Collections.Generic;
using System.Text;

namespace NeighbourhoodServices.Web.ViewModels.Announcement
{
   public class AnnouncementInputModel
    {
        public string description { get; set; }

        public int price { get; set; }

        public string priceFor { get; set; }

        public string address { get; set; }

        public string category{ get; set; }
    }
}
