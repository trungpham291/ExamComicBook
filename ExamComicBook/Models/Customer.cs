using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ExamComicBook.Models;

public class Customer
{
    [Key]
    public int CustomerID { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    public DateTime Registration { get; set; } = DateTime.Now;

    public ICollection<Rental> Rentals { get; set; }
}
