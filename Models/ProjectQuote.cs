using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleItTest.Models
{
    [Keyless]
    public class ProjectQuote
    {
        [Required(ErrorMessage = "Quote date is required.")]
        public DateTime QuoteSentDate { get; set; }

        [Required(ErrorMessage = "Salesperson is required.")]
        [StringLength(50)]
        public string Salesperson { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Project code is required.")]
        [StringLength(50)]
        public string ProjectCode { get; set; }

        [Required(ErrorMessage = "Customer is required.")]
        [StringLength(100)]
        public string Customer { get; set; }

        [StringLength(50)]
        public string? CustomerCity { get; set; }

        [StringLength(2)]
        public string? CustomerState { get; set; }

        [StringLength(50)]
        public string? MarketingCategory { get; set; }

        [Required(ErrorMessage = "Number of quotes is required.")]
        public int NumberOfQuotes { get; set; }

        [Required(ErrorMessage = "Net total is required.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalNet { get; set; }

        public ProjectQuote()
        {
            Salesperson = string.Empty;
            ProjectName = string.Empty;
            ProjectCode = string.Empty;
            Customer = string.Empty;
        }
    }

}
