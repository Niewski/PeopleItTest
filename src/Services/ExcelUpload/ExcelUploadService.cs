using Microsoft.AspNetCore.Components.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PeopleItTest.Models;

namespace PeopleItTest.Services.ExcelUpload
{
    public class ExcelUploadService : IExcelUploadService
    {
        public async Task<List<ProjectQuote>> ProcessExcelFile(IBrowserFile file)
        {
            var quotes = new List<ProjectQuote>();
            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            stream.Position = 0;

            IWorkbook workbook = new XSSFWorkbook(stream); // XSSFWorkbook for .xlsx files
            ISheet sheet = workbook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum; i++) // Assuming first row is header
            {
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    var quote = new ProjectQuote
                    {
                        QuoteSentDate = ConvertCellToDate(row.GetCell(0)),
                        Salesperson = ConvertCellToString(row.GetCell(1)),
                        ProjectName = ConvertCellToString(row.GetCell(2)),
                        ProjectCode = ConvertCellToString(row.GetCell(3)),
                        Customer = ConvertCellToString(row.GetCell(4)),
                        CustomerCity = row.GetCell(5)?.ToString(),
                        CustomerState = row.GetCell(6)?.ToString(),
                        MarketingCategory = row.GetCell(7)?.ToString(),
                        NumberOfQuotes = ConvertCellToInt(row.GetCell(8)),
                        TotalNet = ConvertCellToDecimal(row.GetCell(9)),
                    };
                    quotes.Add(quote);
                }
            }
            return quotes;

        }

        #region Helper Functions

        private DateTime ConvertCellToDate(ICell cell)
        {
            if (cell == null) throw new ArgumentException("Cell cannot be null");

            try
            {
                // Check if the cell is formatted as a date
                if (DateUtil.IsCellDateFormatted(cell))
                {
                    return cell.DateCellValue;
                }
                else if (cell.CellType == CellType.Numeric)
                {
                    // Attempt to treat numeric cells as dates 
                    return DateUtil.GetJavaDate(cell.NumericCellValue);
                }
                else if (cell.CellType == CellType.String)
                {
                    // Try to parse the date from the string
                    DateTime parsedDate;
                    if (DateTime.TryParse(cell.StringCellValue, out parsedDate))
                    {
                        return parsedDate;
                    }
                }
                throw new FormatException($"Cell is improperly formatted: {cell}");
            }
            catch
            {
                throw new InvalidDataException($"Failed to convert cell value to DateTime: {cell}");
            }
        }

        private string ConvertCellToString(ICell cell)
        {
            if (cell == null || cell.CellType == CellType.Blank)
            {
                throw new ArgumentException("Cell cannot be null");
            }

            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue.Trim();
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                default:
                    throw new FormatException("Cell is improperly formatted");
            }
        }

        private int ConvertCellToInt(ICell cell)
        {
            if (cell == null || cell.CellType == CellType.Blank)
            {
                throw new ArgumentException("Cell cannot be null");
            }

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return (int)cell.NumericCellValue;
                case CellType.String:
                    if (int.TryParse(cell.StringCellValue, out int result))
                    {
                        return result;
                    }
                    break;
                default:
                    throw new FormatException("Cell is improperly formatted");
            }

            throw new InvalidDataException($"Failed to convert cell value to integer: {cell}");
        }

        private decimal ConvertCellToDecimal(ICell cell)
        {
            if (cell == null || cell.CellType == CellType.Blank)
            {
                throw new ArgumentException("Cell cannot be null");
            }

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return (decimal)cell.NumericCellValue;
                case CellType.String:
                    if (decimal.TryParse(cell.StringCellValue, out decimal result))
                    {
                        return result;
                    }
                    break;
                default:
                    throw new FormatException("Cell is improperly formatted");
            }

            throw new InvalidDataException($"Failed to convert cell value to decimal: {cell}");
        }


        #endregion
    }
}
