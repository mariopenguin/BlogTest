using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static MySql.Data.MySqlClient.MySqlConnection;
using MySqlConnector;
using System.Reflection.PortableExecutable;
using BlogEngine.Models;

namespace BlogEngine.Database
{
	public class PostDBController
	{
        private static String connectionString = "Server=127.0.0.1;Port=13306;Database=blog;Uid=root;Pwd=root;";
        private MySqlConnection connection = new MySqlConnection(connectionString);
        private MySqlDataReader myReader;
        private MySqlCommand myCmd;
        private Boolean error = false;

        public PostDBController()
        {
        }

        public Object getPosts()
        {
            List<Post> listPost = new List<Post>();
            myReader = executeQuery("select * from post");
            while (myReader.Read())
            {
                listPost.Add(createPost(myReader));
            }
            myReader.Close();
            return listPost;
        }

        public Object getPostById(int id)
        {
            Post post = null;
            this.myReader = executeQuery(String.Format("select * from post where id={0}", id));
            while (myReader.Read())
            {
                post = createPost(myReader);
            }
            myReader.Close();
            return post;
        }

        public MySqlDataReader executeQuery(String query)
        {
            if (!connection.State.ToString().Equals("Open"))
                connection.Open();
            myCmd = connection.CreateCommand();
            myCmd.CommandText = query;
            myReader = myCmd.ExecuteReader();
            return myReader;
        }

        public static Post createPost(MySqlDataReader myReader)
        {
            int postId = myReader.GetInt32(0);
            String title = myReader.GetString(1);
            DateTime date = myReader.GetDateTime(2);
            String content = myReader.GetString(3);
            int catId = myReader.GetInt16(4);
            return new Post(postId, title, date, content, catId);
        }
    }
}

