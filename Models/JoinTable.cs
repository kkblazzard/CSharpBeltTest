using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CSharpBeltTest.Models
{
    public class JoinTable
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int JoinTableId { get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public int ActivityId {get; set;}
        public Activity Activity{get; set;}
        
    }
}
