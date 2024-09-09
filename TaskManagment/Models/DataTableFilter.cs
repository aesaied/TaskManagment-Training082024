using Microsoft.AspNetCore.Mvc;

namespace TaskManagment.Models
{
    public class DataTableFilter
    {

        [BindProperty(Name ="search[value]")]
        public string? Filter { get; set; }
        public int Start { get; set; }

        public int Length { get; set; }

        [BindProperty(Name = "order[0][name]")]
        public string? OrderBy { get; set; }

        [BindProperty(Name ="order[0][dir]")]
        public string? OrderDir { get; set; }
    }
}
