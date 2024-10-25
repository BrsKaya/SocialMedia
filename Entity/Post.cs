namespace SocialMediaApp.Entity{
    public class Post{
        public int PostId{get; set;}

        public string? Content{get; set;}

        public DateTime PublishedOn{get; set;}

        public int UserId{get; set;}

        public User User{get; set;} = null!;

        public List<Comment> Comment{get; set;} = new List<Comment>();

    }

}