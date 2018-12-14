using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CSharpBeltTest.Models
{
    public class Participant
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int ParticipantId { get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public int ActivityId {get; set;}
        public Activity Activity{get; set;}
        
    }
}
