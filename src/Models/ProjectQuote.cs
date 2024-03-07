using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleItTest.Models
{
    public class ProjectQuote
    {
        [Key]
        public int ProjectQuoteId { get; set; }

        [Required(ErrorMessage = "Quote date is required.")]
        public DateTime QuoteSentDate { get; set; }

        [Required(ErrorMessage = "Salesperson is required.")]
        [StringLength(50, ErrorMessage = "The Salesperson cannot exceed 50 characters.")]
        public string Salesperson { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(50, ErrorMessage = "The Project Name cannot exceed 50 characters.")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Project code is required.")]
        [StringLength(50, ErrorMessage = "The Project Code cannot exceed 50 characters.")]
        public string ProjectCode { get; set; }

        [Required(ErrorMessage = "Customer is required.")]
        [StringLength(100, ErrorMessage = "The Customer Name cannot exceed 100 characters.")]
        public string Customer { get; set; }

        [StringLength(50, ErrorMessage = "The City cannot exceed 50 characters.")]
        public string? CustomerCity { get; set; }

        [StringLength(2, ErrorMessage = "The State cannot exceed 2 characters.")]
        public string? CustomerState { get; set; }

        [StringLength(50, ErrorMessage = "The Marketing Category cannot exceed 50 characters.")]
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
