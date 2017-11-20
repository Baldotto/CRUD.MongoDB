
using System;
using System.Collections.Generic;
using System.Text;

namespace VB.CRUD.Domain
{
    public abstract class Entity
    {


        public int Id { get; set; }

        public Entity()
        {
            Id =  Guid.NewGuid().GetHashCode();
        }

        public bool IsTransient()
        {
            return this.Id == default(int);
        }
       
    }
}
