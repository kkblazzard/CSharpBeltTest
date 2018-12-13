
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
        
        
        public DateTime created_at {get; set;}=DateTime.Now;
        public DateTime updated_at {get; set;}=DateTime.Now;
        public List<User> Users {get; set;}
    }
        
}