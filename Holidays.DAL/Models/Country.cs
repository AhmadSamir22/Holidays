using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holidays.DAL.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Deleted { get; set; }

        
        public List<Holiday> Holidays { get; set; }
    }
}
