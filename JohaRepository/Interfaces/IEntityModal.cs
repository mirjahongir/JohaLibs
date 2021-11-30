using JohaRepository.Models;

using System;

namespace JohaRepository.Interfaces
{
    public interface IEntityModel
    {
       
        string Name { get; set; }
        DateTime CreateDate { get; set; }
        DateTime LastUpdateDate { get; set; }
     
    }
}
