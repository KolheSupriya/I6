using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventoryManagementSystem
{


    class USERS
    {

        static int user_choice;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int User_ID { get; set; }


        [Required]
        public int Role_ID { get; set; }
        [ForeignKey("Role_ID")]
        public ROLE ROLE { get; set; }


        public int Product_Access { get; set; }
        [ForeignKey("Product_Access")]
        public PRODUCT_CATEGORIES ProductsList { get; set; }



        [Required]
        [Index(IsUnique = true)]
        [MaxLength(15)]
        [MinLength(4)]
        public string User_Name { get; set; }
        [MaxLength(30)]
        [MinLength(4)]
        [Required] public string First_Name { get; set; }
        [MaxLength(15)]
        [MinLength(4)]
        [Required] public string Last_Name { get; set; }
        [MaxLength(15)]
        [MinLength(4)]
        [Required] public string Password { get; set; }

        
       
        [Required] public string City { get; set; }
        [MaxLength(30)]
        [MinLength(4)]
        [Required] public string Address { get; set; }
        [MaxLength(6)]
        [MinLength(5)]
        [Required] public int Zip_Code { get; set; }
        [MaxLength(30)]
        [MinLength(4)]
        [Required] public string Email { get; set; }




    }



}