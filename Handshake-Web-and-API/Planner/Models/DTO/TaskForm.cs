﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.Models.DTO
{
    public class TaskForm
    {
        public long Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Subject { get; set; }

        [StringLength(128)]
        public string Type { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Completion { get; set; }
        public int Offset { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long UserId { get; internal set; }
    }
}