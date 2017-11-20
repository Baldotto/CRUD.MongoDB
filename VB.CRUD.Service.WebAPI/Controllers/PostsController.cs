using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VB.CRUD.Domain;
using System;
using VB.CRUD.Domain.Interfaces;
using System.Threading.Tasks;

namespace VB.CRUD.Service.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {

        private readonly IPostRepository _postRepository;


        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        
        [HttpGet]
        public IActionResult GetAsync(int id)
        {
            var post = _postRepository.BuscarPostPorId(id);
            return Ok(post);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody]Post value)
        {
             _postRepository.AdicionarPost(value);
            return Ok("Cadastro Realizado com Sucesso");
        }

        
        [HttpPut]
        public IActionResult Put(int id, [FromBody]Post value)
        {
            _postRepository.AtualizarPost(value, id);
            return Ok("Atualização Realizada com Sucesso");
        }

        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _postRepository.DeletarPost(id);
            return Ok("Registro Removido com Sucesso");
        }
    }
}
