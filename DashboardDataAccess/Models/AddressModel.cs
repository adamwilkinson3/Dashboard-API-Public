using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzDataAccess.Models;

public class AddressModel
{
    [DisplayName("Id")]
    public int? id { get; set; }
    public int? customer_id { get; set; }
    [StringLength(255)]
    public string address1 { get; set; }
    [StringLength(150)]
    public string? address2 { get; set; }
    [Range(1, 50)]
    public int? state_id { get; set; }
    [StringLength(189)]
    public string city { get; set; }
    [StringLength(10)]
    public string? postal_code { get; set; }
    [StringLength(20)]
    public string phone { get; set; }
    public StateModel? State { get; set; }
}
