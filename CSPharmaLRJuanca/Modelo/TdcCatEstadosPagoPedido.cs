﻿using System;
using System.Collections.Generic;

namespace CSPharmaLRJuanca.Modelo;

public partial class TdcCatEstadosPagoPedido
{
    public string MdUuid { get; set; } = null!;

    public DateTime MdDate { get; set; }

    public int Id { get; set; }

    public string CodEstadoPago { get; set; } = null!;

    public string? DesEstadoPago { get; set; }

    public virtual ICollection<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; } = new List<TdcTchEstadoPedido>();
}