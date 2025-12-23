using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is required.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the name is 30 characters.")]
        public string? Name { get; init; }

        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower tahn 18.")]
        public int Age { get; init; }

        [Required(ErrorMessage = "Position is required.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the position is 20 characters.")]
        public string? Position { get; init; }

    }
}
