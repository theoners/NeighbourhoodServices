namespace NeighbourhoodServices.Web.Infrastructure
{
    using System;

    public class Page
    {
        public const int AnnouncementsPerPage = 10;

        public int CurrentPage { get; set; }

        public int AnnouncementsCount { get; set; }

        public int PagesCount => (int)Math.Ceiling(this.AnnouncementsCount / (decimal)AnnouncementsPerPage);

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}
