using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InventoryManagementSystem
{
    class PRODUCT_CATEGORIES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Product_Category_ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(15)]
        [MinLength(4)]
        public string Product_Name{ get; set; }
      
        [Required]
        [MaxLength(100)]
        [MinLength(4)]
        public string Description { get; set; }
       
        public ICollection<PRODUCTS> Products { get; set; }
        public ICollection<USERS> users { get; set; }


    }
}
