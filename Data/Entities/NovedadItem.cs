﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace InventarioLPS.Data.Entities;

public partial class NovedadItem
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public string Novedad { get; set; }

    public int IdItemInventario { get; set; }

    public virtual ItemInventario IdItemInventarioNavigation { get; set; }
}