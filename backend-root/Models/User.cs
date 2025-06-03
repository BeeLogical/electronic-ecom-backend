using System;
using System.Collections.Generic;

namespace backend_root.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
    public StatusEnum Status { get; set; } = StatusEnum.active;

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<SalesTransaction> SalesTransactions { get; set; } = new List<SalesTransaction>();
}

public enum StatusEnum
{
    pending,
    active,
    inactive
}