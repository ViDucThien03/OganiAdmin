using System;
using System.Collections.Generic;

namespace OganiAdmin.Models;

public partial class Wishlist
{
    public int WishlistId { get; set; }

    public int CusId { get; set; }

    public int? ProductId { get; set; }

    public virtual Customer Cus { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
