
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
        [Display(Name="Activity Name")]
        [MinLength(2)]
        
        public string activity_name {get; set;}

        [Required]
        [Display(Name="Date & Time")]
        [FutureDate(ErrorMessage="Date should be in the future.")]
        public DateTime date_time {get; set;}
        [Display(Name="Duration")]
        
        public int duration_time {get; set;}
        public string duration_length {get; set;}
        [Display(Name="Description")]
        [Required]
        [MinLength(10)]
        public string desc {get; set;}
        public int CreatorID {get; set;}
        public DateTime CreatedAt {get; set;}=DateTime.Now;
        public DateTime UpdatedAt {get; set;}=DateTime.Now;
        
        public List<Participant> Participate {get; set;}
    }
    public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value != null && (DateTime)value > DateTime.Now;
    }
}
        
}