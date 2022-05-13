using System;

namespace Api.Domain.Entites
{
    public abstract class UserEntitiy : BaseEntitiy
    {
        public String Name { get; set; }        
        public String Email { get; set; }    
    }
}