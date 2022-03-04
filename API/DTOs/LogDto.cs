using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class LogDto
    {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string User { get; set; }
    public string Position { get; set; }
    public string Field { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    }
}