using System;
namespace BlogEngine.Models
{
	public class Category
	{
		public int id { get; set; }
		public String title { get; set; }

		public Category(int id,String title)
		{
			this.id = id;
			this.title = title;
		}

        public Category()
        {
        }

        public String toString()
		{
			return String.Format("Category with Id: {0} and title {1}",this.id,this.title);
		}

	}
}

