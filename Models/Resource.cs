﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_Invoice_System.Models
{
    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        public string ResourceName { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public int ConsumedTotalHours { get; set; }

        public bool IsActive { get; set; } = true;

        // New property: tracks if this resource has already been invoiced.
        public bool IsInvoiced { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
