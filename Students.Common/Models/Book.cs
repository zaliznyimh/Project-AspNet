using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Models;

public class Book
{
    public int Id { get; set; }
    public string BookTitle { get; set; } = string.Empty;

    [StringLength(13, MinimumLength = 13)]
    public string ISBN { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime PublicationDate { get; set; }

    public Book() { 

    }
}
