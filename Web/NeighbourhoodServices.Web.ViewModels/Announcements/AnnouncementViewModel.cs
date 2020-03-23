﻿using System.Collections.Generic;
using NeighbourhoodServices.Web.ViewModels.Categories;

namespace NeighbourhoodServices.Web.ViewModels.Announcement
{
    using NeighbourhoodServices.Data.Models;
    using NeighbourhoodServices.Services.Mapping;

    public class AnnouncementViewModel : IMapFrom<Announcement>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ServiceType ServiceType { get; set; }

        public string Place { get; set; }

        public string CreatedOn { get; set; }

        public string CategoriesName { get; set; }

        public virtual string User { get; set; }

        public string CategoryName { get; set; }
    }
}