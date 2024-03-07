using Microsoft.AspNetCore.Components.Forms;
using PeopleItTest.Models;

namespace PeopleItTest.Services.ExcelUpload
{
    public interface IExcelUploadService
    {
        Task<List<ProjectQuote>> ProcessExcelFile(IBrowserFile file);
    }
}
