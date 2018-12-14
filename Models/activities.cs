
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpBeltTest.Models

{
    public class Activity
    {
        [Key]
        public int ActivityId {get; set;}
        [Required]
        public string activity_name {get; set;}
        [Required]
        public DateTime date_time {get; set;}
        
        public int duration_time {get; set;}
        public string duration_length {get; set;}
        public string desc {get; set;}
        public int CreatorID {get; set;}
        public DateTime CreatedAt {get; set;}=DateTime.Now;
        public DateTime UpdatedAt {get; set;}=DateTime.Now;
        
        public List<Participant> Participate {get; set;}
    }
        
}