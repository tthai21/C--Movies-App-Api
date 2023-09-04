namespace C__Movies_App_Api
{
    public class FavoriteRequest
    {

        public int MovieId { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public int Year { get; set; }
        public List<GenreRequest> GenreRequest { get; set; }


    }
}