using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuAnBot_Wpf.Api.Dtos
{
    public class VersionDto
    {
        public string Url { get; set; } = "";
        public string FileName { get; set; } = "";
        public string VersionName { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime Date { get; set; }
    }
}
