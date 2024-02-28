namespace LR7.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
        public string Text { get; set; }
    }
}
