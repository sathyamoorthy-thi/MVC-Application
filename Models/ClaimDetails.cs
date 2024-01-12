using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ReimbursementClaim;
using System.Web.Mvc;


namespace ReimbursementClaim
{
    public class ClaimDetails
    {
        [Key]
        public int Id { get; set; }
        
      


        [Display(Name = "UserName")]
        [Required(ErrorMessage ="Name is required")] 
        [StringLength(15)]
        [RegularExpression(@"[A-Z][a-z]*$", ErrorMessage = "Not a valid Username")] 
        public string Name { get; set; }
         
        [Display(Name = "Email")] 
        [Required(ErrorMessage ="Email is required")]  
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(25)] 
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z]+\.[a-z]{2,4}", ErrorMessage = "Enter Correct Email ID")]
        public string Email { get; set; }

        
        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "Contact is required")]  
        [DataType(DataType.PhoneNumber)]  
        [RegularExpression(@"[6-9][0-9]{9}$", ErrorMessage = "Not a valid Phone number")] 
        public string PhoneNumber {get; set;}
         
        [Display(Name = "Reimbursement Type")] 
        [Required(ErrorMessage = "Choose any one of the Reimbursement type.")]  
        public string Type {get; set;}
        
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Date is required")] 
        public string Date {get; set;} 

        [Display(Name = "Amount")]
        [Required(ErrorMessage ="Amount is required")] 
        [Range(500,10000,ErrorMessage = "Amount must be between 500 to 10,000")]
        public string Amount {get; set;}
           
        [Display(Name = "Currency")]   
        [Required(ErrorMessage ="Currency is required")] 
        public string Currency {get;set;}
          
        [Display(Name = "Description")]
        [StringLength(100)]  
        [Required(ErrorMessage =" Description is required")]   
        public string Description {get;set;}

        [ScaffoldColumn(false)] 
        public string Status {get; set;}
        
    }
}
