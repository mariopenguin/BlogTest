using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BlogEngine.Database;
using BlogEngine.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngine.Controllers
{


    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private static CategoryDBController controller = new CategoryDBController();
        //PENDIENTE DE MEJORA Método terminado, devuelve todas las categorías de la bbdd
        // GET: api/values
        [HttpGet]
        public Object Get()
        {
            List<Category> reader = (List<Category>)controller.getCategories();
            String[] listCats = new string[reader.Count()];
            int cont = 0;
            foreach (Category i in reader)
            {
                listCats[cont] = i.toString();
                cont += 1;
            }
            if (listCats.Length == 0)
                return NoContent();
            return Ok(listCats);
        }


        //FINALIZADO Metodo para devolver una categoria por id
        // GET api/values/5
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            Category result = (Category)controller.getCategoryById(id);
            if (result != null)
                return result.toString();
            return NotFound("Category not found");
        }

        // POST api/values
        [HttpPost]
        public void Post(string value)
        {
            controller.insertCategory(value);
        }
        /*
        [HttpGet("{id}")]
        [Route("api/Category/{ID}/posts")]
        public Object GetPostsByCat(int id)
        {
            List<Post> reader = (List<Post>)controller.getCategories();
            String[] listPosts = new string[reader.Count()];
            int cont = 0;
            foreach (Post i in reader)
            {
                listPosts[cont] = i.toString();
                cont += 1;
            }
            if (listPosts.Length == 0)
                return NoContent();
            return Ok(listPosts);
        }
        */
    }
}

