using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CompanyForCreationDto
    {
        [Required(ErrorMessage = "Company name is required.")]
        [MaxLength(50, ErrorMessage = "Company name cannot exceed 50 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string? Country { get; init; }

        [Required(ErrorMessage = "Employees list is required.")]
        public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
    }
    
}
