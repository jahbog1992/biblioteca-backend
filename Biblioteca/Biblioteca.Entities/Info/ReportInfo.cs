using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entities.Info
{
    [NotMapped]
    public class ReportInfo
    { 
        public string NombreLibro { get; set; } = default!; 
        public int Total { get; set; }
    }
}
