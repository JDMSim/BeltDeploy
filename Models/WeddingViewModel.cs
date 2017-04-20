using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class WeddingViewModel
    {
 
        [RequiredAttribute]
        [DisplayAttribute(Name="Wedder One")]
        public string WedderOne { get; set; }

        [RequiredAttribute]
        [Display(Name="Wedder Two")]
        public string WedderTwo { get; set; }
 
        [RequiredAttribute]
        public DateTime Date { get; set; }

        [RequiredAttribute]
        public string Address { get; set; }
    }
}