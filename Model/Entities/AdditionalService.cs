using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class AdditionalService
{
    public int Id { get; set; }

    public int Price { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ResAddService> ResAddServices { get; set; } = new List<ResAddService>();
}
