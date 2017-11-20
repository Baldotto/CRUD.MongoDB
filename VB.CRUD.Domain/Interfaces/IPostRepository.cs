using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace VB.CRUD.Domain.Interfaces
{
    public interface IPostRepository
    {
        Post BuscarPostPorId(int id);
        void AdicionarPost(Post post);
        void AtualizarPost(Post post, int Id);
        void DeletarPost(int id);
    }
}
