using System;
namespace BlogEngine.Models
{
	public class Post
	{
        public int id { get; set; }
		public String title { get; set; }
		public String content { get; set; }
        public DateTime publicationDate { get; set; }
        public int categoryId { get; set; }
        public Post(int id, string title, DateTime publicationDate, string content, int catId)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.publicationDate = publicationDate;
            this.categoryId = catId;
        }

        public string toString()
        {
            return String.Format("Post id {0} title {1} content {2} publicationDate {3} and categoryId {4}",
                this.id,this.title,this.publicationDate.ToString(),this.publicationDate,this.categoryId);
        }
    }
}

