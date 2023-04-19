using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzDataAccess.Models;

public class CustomerModel
{
    [DisplayName("Id")]
    public int? id { get; set; }
    [StringLength(45)]
    [DisplayName("First Name")]
    public string first_name { get; set; }
    [StringLength(45)]
    [DisplayName("Last Name")]
    public string last_name { get; set; }
    [StringLength(50)]
    public string email { get; set; }
    [Range(0, 1, ErrorMessage = "Value must be 0 or 1")]
    public int? active { get; set; }
    public DateTime? create_date { get; set; }
    public DateTime? last_update { get; set; }
    public AddressModel? Address { get; set; }
}
