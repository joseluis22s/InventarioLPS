﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace InventarioLPS.Data.Entities;

public partial class GuiaRemision
{
    public int Id { get; set; }

    public string Numero { get; set; }

    public virtual ICollection<DetalleVendido> DetalleVendido { get; set; } = new List<DetalleVendido>();
}