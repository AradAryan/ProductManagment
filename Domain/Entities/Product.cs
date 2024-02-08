using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    public class Product : BaseEntity
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Manufacturer email is required")]
        [StringLength(50, ErrorMessage = "Manufacturer email cannot exceed 50 characters")]
        [EmailAddress]
        public string ManufacturerEmail { get; set; }

        [Phone]
        public string ManufacturerPhone { get; set; }

        [Required(ErrorMessage = "Produce date is required")]
        public DateTime ProduceDate { get; set; }

    }
}