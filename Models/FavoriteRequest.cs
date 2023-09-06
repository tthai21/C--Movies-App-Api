namespace C__Movies_App_Api
{
    public class FavoriteRequest
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public float Rate { get; set; }
        public int Year { get; set; }
        public List<GenreRequest> Genres{ get; set; }


    }
}