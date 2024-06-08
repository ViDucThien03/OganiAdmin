using System;
using System.Collections.Generic;

namespace OganiAdmin.Models;

public partial class Category
{
    public int CateId { get; set; }

    public string? CateName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
