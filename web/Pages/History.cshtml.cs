using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace web.Pages
{
    public class HistoryModel : PageModel
    {
        readonly IConfiguration _configuration;

        public HistoryModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class HistoryItem
        {
            public string Location { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
        }

        public IEnumerable<HistoryItem> Items { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OrderBy { get; set; }

        public async Task OnGet()
        {
            Items = await QueryDatabase();
        }

        private async Task<IEnumerable<HistoryItem>> QueryDatabase()
        {
            var results = new List<HistoryItem>();
            var connectionString = _configuration.GetConnectionString("mydb");

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Location, Date, Amount FROM History";
                if(OrderBy != null)
                {
                    query += " ORDER BY " + OrderBy;
                }

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(new HistoryItem
                        {
                            Location = reader.GetString(0),
                            Date = reader.GetDateTime(1),
                            Amount = reader.GetDecimal(2)
                        });
                    }
                }
            }

            return results;
        }
    }
}