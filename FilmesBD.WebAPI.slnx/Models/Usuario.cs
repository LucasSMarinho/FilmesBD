using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FilmesBD.WebAPI.slnx.Models;

[Table("Usuario")]
public partial class Usuario
{
    [Key]
    [StringLength(40)]
    [Unicode(false)]
    public string IdUsuario { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string Senha { get; set; } = null!;
}
