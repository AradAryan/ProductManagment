using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public bool IsAvailable { get; set; }
    }
}