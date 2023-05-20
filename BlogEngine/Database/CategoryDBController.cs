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

//using var connection = new MySqlConnection("Server=myserver;User ID=mylogin;Password=mypass;Database=mydatabase");

namespace BlogEngine.Database
{
	public class CategoryDBController
	{
        //private String connectionString = "Data Source=localhost,13306;Integrated Security=true;Database=blog;User Id=root;Password=root;";
        private static String connectionString = "Server=127.0.0.1;Port=13306;Database=blog;Uid=root;Pwd=root;";
        private MySqlConnection connection = new MySqlConnection(connectionString);
        private MySqlDataReader myReader;
        private MySqlCommand myCmd;
        private Boolean error = false;

        public CategoryDBController()
        {
        }

        public Object getCategories()
        {
            List<Category> listCat = new List<Category>();
            myReader = executeQuery("select * from category");
            while (myReader.Read())
            {
                int catId = myReader.GetInt32(0);
                String catName = myReader.GetString(1);
                listCat.Add(new Category(catId, catName));
            }
            myReader.Close();
            return listCat;
        }

        public Object getPostsByCat(int idCat)
        {
            List<Post> listPosts = new List<Post>();
            myReader = executeQuery(String.Format("select * from post where id={0}", idCat));
            while (myReader.Read())
            {
                listPosts.Add(PostDBController.createPost(myReader));
            }
            myReader.Close();
            return listPosts;
        }

        public Object getCategoryById(int id)
        {
            Category category = null;
            this.myReader = executeQuery(String.Format("select * from category where id={0}", id));
            while (myReader.Read())
            {
                int catId = myReader.GetInt32(0);
                String catName = myReader.GetString(1);
                category = new Category(catId, catName);
            }
            myReader.Close();
            return category;
        }

        public Boolean insertCategory(String title)
        {
            string stre = String.Format("INSERT INTO category(catName) VALUES ('{0}')", title);
            this.myReader = executeQuery(stre);
            myReader.Close();
            return error;
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
    }
}

