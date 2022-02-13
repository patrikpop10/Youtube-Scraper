namespace database
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryString {get; set;}
        public virtual List<Video> Videos {get; set;}

    }

}