using System;
using VB.CRUD.Domain;
using MongoDB.Driver;
using VB.CRUD.Domain.Interfaces;
using System.Threading.Tasks;
using System.Dynamic;

namespace VB.CRUD.Infra.Data.MongoDB.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly MongoContext _mongoContext;

        public PostRepository(MongoContext mongoContext)
        {
            this._mongoContext = mongoContext;
        }


        public Post BuscarPostPorId(int id)
        {
            return  _mongoContext.Posts.FindSync(p => p.Id == id).SingleOrDefault();
        }

        public void AdicionarPost(Post post)
        {
            _mongoContext.Posts.InsertOneAsync(post);
        }

        public void AtualizarPost(Post post, int Id)
        {
            _mongoContext.Posts.ReplaceOne(e => e.Id == post.Id, post);
        }

        public void DeletarPost(int id)
        {
            _mongoContext.Posts.DeleteOne(e => e.Id == id);
        }

    }
}
