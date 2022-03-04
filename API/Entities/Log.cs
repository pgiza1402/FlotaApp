using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Log
    {
    public int Id { get; set; } 
    public DateTime Date { get; set; } = DateTime.Now;
    public string User { get; set; }
    public string Position { get; set; }
    public string Field { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }


    }
}