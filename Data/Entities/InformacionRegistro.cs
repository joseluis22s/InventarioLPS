﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace InventarioLPS.Data.Entities;

public partial class InformacionRegistro
{
    public int Id { get; set; }

    public string NumeroDocumento { get; set; }

    public decimal MontoTotal { get; set; }

    public int IdFormaRegistro { get; set; }

    public DateTime FechaCompra { get; set; }

    public virtual FormaRegistro IdFormaRegistroNavigation { get; set; }

    public virtual ICollection<ItemInventario> ItemInventario { get; set; } = new List<ItemInventario>();
}