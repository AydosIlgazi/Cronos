
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Cronos.Application.Entities
{
    public class BaseEntity 
    {
        public int Id { get; set; }
       
        public DateTime CreatedDate { get; set; }
       
        public DateTime ModifiedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
       
        public int Order { get; set; }
       
        public bool IsActive { get; set; }

        public string? ImageUrl { get; set; }

        public string? AltText { get; set; }

        public string? RedirectUrl { get; set; }
        public bool IsDeleted { get; set; }
    }

}
