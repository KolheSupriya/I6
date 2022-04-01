using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    class AdminOperations
    {
        public static int loggedIn=0;
        static int user_choice=1;

        public static void updateUserData()
        {

            var ctx = new MyDebContext();
            while (user_choice != 0)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------\nWhat operation do you wish to perform?\n" +
                    "1. Update Username\n" +
                    "2. Update Password\n" +
                    "3. Update Email\n" +
                    "0. Return\n" +
                    "----------------------------------------");
                user_choice = Convert.ToInt32(Console.ReadLine());
                switch (user_choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter user ID: ");
                            int userID = Convert.ToInt32(Console.ReadLine());
                            var find = ctx.USERS.First(x => x.User_ID == userID);
                            Console.Write("Please enter new username: ");
                            string newUserName = null;
                            newUserName=Console.ReadLine();
                            while (isUsernameAvailable(newUserName) != 0)
                            {
                                Console.Write("Username already exist. \nPlease try another one: ");
                                newUserName = Console.ReadLine();
                            }
                            find.User_Name = newUserName;
                            if (ctx.SaveChanges() > 0)
                            {

                                Console.WriteLine("Result-> Username Updated");
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter user id to update password: ");
                            int userID = Convert.ToInt32(Console.ReadLine());
                            while (isUserIDAvailable(userID) == 0)
                            {
                                Console.Write("User does not exist. \nPlease try again: ");
                                userID = Convert.ToInt32(Console.ReadLine());
                            }
                            var find = ctx.USERS.First(x => x.User_ID == userID);
                            Console.Write("Please enter new password: ");
                            string newUserName = null;
                            newUserName = Console.ReadLine();

                            find.Password = newUserName;


                            if (ctx.SaveChanges() > 0)
                            {

                                Console.WriteLine("Result-> Password Updated");
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter user ID to update email: ");
                            int userID = Convert.ToInt32(Console.ReadLine());
                            var find = ctx.USERS.First(x => x.User_ID == userID);
                            while (isUserIDAvailable(userID) == 0)
                            {
                                Console.Write("User does not exist. \nPlease try again: ");
                                userID = Convert.ToInt32(Console.ReadLine());
                            }
                            Console.Write("Please enter new email: ");
                            string newEmail = null;
                            newEmail = Console.ReadLine();
                            while (isEmailAvailable(newEmail) != 0)
                            {
                                Console.Write("Email already exist. \nPlease try another one: ");
                                newEmail = Console.ReadLine();
                            }
                            find.Email = newEmail;
                            if (ctx.SaveChanges() > 0)
                            {

                                Console.WriteLine("Result-> Email Updated");
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 0:
                        {
                            Console.Clear();
                            Program.login();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please enter correct choice");
                            break;
                        }
                }
            }
        }
        public static void addProduct()
        {
            var ctx = new MyDebContext();
            
            Console.WriteLine("----------------------------------------\nEnter following details:");
           
            string description = null;
            Console.Write("Product Name: ");
            var productName = Console.ReadLine();
            while (isProductNameAvailable(productName) != 0)
            {
                Console.Write("Product already exist. \nPlease try another one: ");
                productName = Console.ReadLine();
            }


            Console.Write("Product Description: ");
            description = Console.ReadLine();
            while (!Regex.Match(description, UserOperations.allPattern).Success)
            {
                Console.WriteLine("Please enter product description: (Only alphabets, numbers are allowed and length must be between 1,100): ");
                description = Console.ReadLine();
            }


            PRODUCT_CATEGORIES data = new PRODUCT_CATEGORIES() { Product_Name = productName, Description = description };
            ctx.PRODUCT_CATEGORIES.Add(data);
            if (ctx.SaveChanges() > 0)
            {
                Console.WriteLine("Result-> New product Added");
            }
            else
            {
                Console.WriteLine("Result-> Product not added. Please confront admin");
            }
            Console.ReadKey();
        }
        public static int isUsernameAvailable(string username)
        {
            var ctx = new MyDebContext();
            var users = from data in ctx.USERS select data;

            foreach (var user in users)
            {
                if (user.User_Name == username)
                {
                    return 1;
                }
            }
            return 0;
        }
        public static int isProductNameAvailable(string productname)
        {
            var ctx = new MyDebContext();
            var users = from data in ctx.PRODUCT_CATEGORIES select data;

            foreach (var user in users)
            {
                if (user.Product_Name == productname)
                {
                    return 1;
                }
            }
            return 0;
        }
        public static int isProductIDAvailable(int Pid)
        {
            //Console.WriteLine(Pid);
            var ctx = new MyDebContext();
            var users = from data in ctx.PRODUCT_CATEGORIES select data;

            foreach (var user in users)
            {
                if (user.Product_Category_ID == Pid)
                {
                    return 1;
                }
            }
            return 0;
        }
                                                    public static int isUserIDAvailable(int Pid)
                                                    {
                                                        var ctx = new MyDebContext();
                                                        var users = from data in ctx.USERS select data;

                                                        foreach (var user in users)
                                                        {
                                                            if (user.User_ID == Pid)
                                                            {
                                                                return 1;
                                                            }
                                                        }
                                                        return 0;
                                                    }
        public static int isAdmin(int userID)
        {
            var ctx = new MyDebContext();
            var findUserData = ctx.USERS.First(x => x.User_ID == userID);

            if(findUserData.Role_ID==1)
                return 0;
            return 1;
        }
        public static int isEmailAvailable(string email)
        {
            var ctx = new MyDebContext();
            var users = from data in ctx.USERS select data;

            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    return 1;
                }
            }
            return 0;
        }

        public static int isValidated(string password)
        {
            var ctx = new MyDebContext();
            var users = from data in ctx.USERS select data;

            foreach (var user in users)
            {
                if (user.Password == password)
                {
                    loggedIn = user.Role_ID;
                    return 1;
                }
            }
            return 0;
        }
       
        public static void deleteSubproductByID(int productId)
        {
            var ctx = new MyDebContext();
            PRODUCTS deleteProduct = new PRODUCTS() { Products_ID = productId };
            ctx.Remove(deleteProduct);
            if (ctx.SaveChanges() > 0)
            {
                Console.WriteLine("Result-> Product Deleted");
            }
        }
        public static void deleteProductByID(int productId)
        {
            int totalDeleted = 0;
            var ctx = new MyDebContext();
            PRODUCT_CATEGORIES deleteProduct = new PRODUCT_CATEGORIES() { Product_Category_ID = productId };

            if (isProductIDAvailable(productId) == 0)
            {
                Console.WriteLine("Product does not exist");
                return;
            }
            else
            {

                using (var inventory = new MyDebContext())
                {
                    foreach (var findProductData in inventory.PRODUCTS.ToList())
                    {
                        if (findProductData.Product_Category_ID == productId)
                        {
                            deleteSubproductByID(findProductData.Products_ID);

                        }


                    }
                }
                ctx.Remove(deleteProduct);
                if (ctx.SaveChanges() > 0)
                {
                    totalDeleted ++;
                    
                }
                Console.WriteLine("Result-> Total "+totalDeleted+" Products Deleted");
            }
        }

        public static void getUserDataByID()
        {
            var ctx = new MyDebContext();
            int userID;
            Boolean userExist = false;
            do
            {
                Console.Write("Please enter User ID: ");
                userID = Convert.ToInt32(Console.ReadLine());
                using (var inventory = new MyDebContext())
                {
                    foreach (var item in inventory.USERS.ToList())
                    {
                        if (item.User_ID == userID)
                        {
                            userExist = true;
                            Console.WriteLine("-------------------------------------------");
                            var findUserData = ctx.USERS.First(x => x.User_ID == userID);
                            Console.WriteLine("USER ID: " + findUserData.User_ID);
                            Console.WriteLine("Username: " + findUserData.User_Name);
                            Console.WriteLine("First Name: " + findUserData.First_Name);
                            Console.WriteLine("Last Name: " + findUserData.Last_Name);
                            Console.WriteLine("Password: " + findUserData.Password);
                            Console.WriteLine("City: " + findUserData.City);
                            Console.WriteLine("Address: " + findUserData.Address);
                            Console.WriteLine("Zip Code: " + findUserData.Zip_Code);
                            Console.WriteLine("Email: " + findUserData.Email);

                            UserOperations.getProductUserAccess(findUserData.Product_Access);
                            Console.WriteLine("-------------------------------------------");

                        }
                        else
                        {
                            userExist = false;
                        }

                    }
                    if (!userExist)
                    {
                        Console.WriteLine("User does not exist. Please try again");
                    }
                }
            }
            while (!userExist);
            }


        public static void getUserData()
        {

            using (var inventory = new MyDebContext())
            {
                foreach (var user in inventory.USERS.ToList())
                {

                    Console.WriteLine("User ID: " + user.User_ID);
                    Console.WriteLine("Username: " + user.User_Name);
                    Console.WriteLine("First Name: " + user.First_Name);
                    Console.WriteLine("Last Name: " + user.Last_Name);
                    Console.WriteLine("Password: " + user.Password);
                    Console.WriteLine("City: " + user.City);
                    Console.WriteLine("Address: " + user.Address);
                    Console.WriteLine("Zip_Code: " + user.Zip_Code);
                    Console.WriteLine("Email: " + user.Email);

                    var findProductData = inventory.PRODUCT_CATEGORIES.First(x => x.Product_Category_ID == user.Product_Access);
                  
                    Console.WriteLine("Product alloted: " + findProductData.Product_Name);
                    Console.WriteLine("---------------------------------------------------");


                }



               }
            Console.ReadKey();
        }
        public static void getProductData()
        {
            int totalProducts = 0;
            using (var inventory = new MyDebContext())

            {
                foreach (var product in inventory.PRODUCTS.ToList())
                {
                    totalProducts++;
                    Console.WriteLine("-----------------------------------------");
                    var findProductList = inventory.PRODUCT_CATEGORIES.First(x => x.Product_Category_ID == product.Product_Category_ID);

                    Console.WriteLine("Product CategoryID: " + product.Product_Category_ID);
                    Console.WriteLine("Product Name: " + findProductList.Product_Name);
                    Console.WriteLine("SubProduct Name: " + product.SubProduct_Name);
                    Console.WriteLine("Current Storage: " + product.Current_Storage);
                    Console.WriteLine("Sold: " + product.Sold);
                    Console.WriteLine("Remaining Quantity: " + product.Remaining_Quantity);
                    Console.WriteLine("Unit Price: " + product.Unit_Price);
                    Console.WriteLine("Selling Amount: " + product.Total_Selling_Amount);



                }

            }
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Total Products: "+totalProducts);
            Console.WriteLine("-----------------------------------------");
            Console.ReadKey();
        }

        public static void removeUser(int userID)
        {
            var ctx = new MyDebContext();
            if (isAdmin(userID) == 0)
            {
                Console.WriteLine("Admin can't be deleted.");
                Console.ReadKey();
                return;
            }
            USERS u = new USERS() { User_ID = userID };
             ctx.USERS.Remove(u);
                if (ctx.SaveChanges() > 0)
                {
                    Console.WriteLine("Result-> User " + u.Email + " deleted successfully");
                }
                else
                {
                    Console.WriteLine("Result-> User " + u.Email + " not deleted successfully. Please confront admin");
                }
                Console.ReadKey();
            
        }
        public static void addUser()
        {
            string User_Name = "";
            var ctx = new MyDebContext();
            Boolean productExist = false;
            int productIncharge;
            string Email = "";
            Console.WriteLine("----------------------------------------\nEnter following details:");
            
            Console.Write("Username: ");
            User_Name = Console.ReadLine();
            
            while (isUsernameAvailable(User_Name) != 0)
            {
                Console.Write("Username already taken. \nPlease try another one: ");
                User_Name = Console.ReadLine();
            } 
            Console.Write("First Name: ");
            string First_Name = Console.ReadLine();
            Console.Write("Last Name: ");
            string Last_Name = Console.ReadLine();
            Console.Write("Password: ");
            string Password = Console.ReadLine();
            Console.Write("City: ");
            string City = Console.ReadLine();
            Console.Write("Address: ");
            string Address = Console.ReadLine();
            Console.Write("Zip Code: ");
            int Zip_Code = Convert.ToInt32(Console.ReadLine());
            Console.Write("Email: ");
            Email = Console.ReadLine();
            while (isEmailAvailable(Email) != 0)
            {
                Console.Write("Email already present. \nPlease try another one: ");
                Email = Console.ReadLine();
            }
            do {
                Console.Write("Product ID to allot: ");
                productIncharge = Convert.ToInt32(Console.ReadLine());
                using (var inventory = new MyDebContext())
                {
                    foreach (var item in inventory.PRODUCT_CATEGORIES.ToList())
                    {
                        if (item.Product_Category_ID == productIncharge)
                        {productExist = true;
                            break;}
                        
                    }
                    if (!productExist)
                    {
                        Console.WriteLine("Product does not exist. Please try again");
                    }
                }
            } while (!productExist);
            USERS data = new USERS() { Product_Access = productIncharge, Role_ID = 2, User_Name = User_Name, First_Name = First_Name, Last_Name = Last_Name, Password = Password, City = City, Address = Address, Zip_Code = Zip_Code, Email = Email };
            ctx.USERS.Add(data);
            if (ctx.SaveChanges() > 0)
            {
                Console.WriteLine("Result-> User Added");
            }
            else
            {
                Console.WriteLine("Result-> User not added. Please confirm details");
            }


        }



        public static void addSubproduct()
        {
            var ctx = new MyDebContext();
          
            string categoryId = "", currentStorage = "", sold = "", unitPrice = "";
            int remainingQuantity, totalSellingAmount;
            string subProductName = "", description = "";
            Console.WriteLine("----------------------------------------\nEnter following details:");
            Console.Write("Category ID: ");
            categoryId = Console.ReadLine();
            while (!UserOperations.numRegex.IsMatch(categoryId))
            {
                
                    Console.Write("Please enter product ID: (Only numbers are allowed)");
                    categoryId = Console.ReadLine();
                
                
            }
            while(isProductIDAvailable(Convert.ToInt32(categoryId)) == 0)
            {
                Console.Write("Product does not exist in categories. Please try again: \n");
                categoryId = Console.ReadLine();

            }
          
           
            while (!Regex.Match(subProductName, UserOperations.alphaNumPattern).Success)
            {
                Console.Write("Please enter sub-product name: (Only alphabets,numbers are allowed and at most length must be 30)");
                subProductName = Console.ReadLine();
            }
          
            Console.Write("Product Description: ");
            description = Console.ReadLine();

            while (!Regex.Match(description, UserOperations.allPattern).Success)
            {
                Console.Write("Please enter product description: (Only alphabets,numbers are allowed and length must be between 1,100)");
                description = Console.ReadLine();
            }
           
            Console.Write("Current Storage: ");
            currentStorage = Console.ReadLine();
            while (!UserOperations.numRegex.IsMatch(currentStorage))
            {
                Console.Write("Please enter current storage: (Only numbers are allowed)");
                currentStorage = Console.ReadLine();
            }
  
            Console.Write("Sold: ");
            sold = Console.ReadLine();

            while (!UserOperations.numRegex.IsMatch(sold))
            {
                Console.Write("Please enter sold quantity: (Only numbers are allowed");
                sold = Console.ReadLine();
            }
      
            remainingQuantity = Convert.ToInt32(currentStorage) - Convert.ToInt32(sold);
            Console.Write("Unit Price: ");
            unitPrice = Console.ReadLine();
            while (!UserOperations.numRegex.IsMatch(unitPrice))
            {
                Console.Write("Please enter sold quantity: (Only numbers are allowed");
                unitPrice = Console.ReadLine();
            }
    
            totalSellingAmount = Convert.ToInt32(unitPrice) * Convert.ToInt32(sold);
            PRODUCTS data = new PRODUCTS() { Product_Category_ID = Convert.ToInt32(categoryId), SubProduct_Name = subProductName, Description = description, Current_Storage = Convert.ToInt32(currentStorage), Sold = Convert.ToInt32(sold), Remaining_Quantity = remainingQuantity, Unit_Price = Convert.ToInt32(unitPrice), Total_Selling_Amount = totalSellingAmount };
            ctx.PRODUCTS.Add(data);
            if (ctx.SaveChanges() > 0)
            {
                Console.WriteLine("Result-> New product subcategory Added");
            }
            else
            {
                Console.WriteLine("Result-> Product subcategory not added. Please confront admin");
            }
            Console.ReadKey();
        }
        public static void AdminMenu()
        {
            var ctx = new MyDebContext();
      
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\tLogged in as Admin\n");
                Console.WriteLine("----------------------------------------\nWhat operation do you wish to perform?\n" +
                    "1. Add User\n" +
                    "2. Add Product\n" +
                    "3. Add Subproduct\n" +
                    "4. Get User Data by ID\n" +
                    "5. Get All User's Data\n" +
                    "6. Delete sub-product\n" +
                    "7. Delete Product\n" +
                    "8. Delete User\n" +
                    "9. Get Product Data\n" +
                    "10. Update Product Data\n" +
                    "11. Update user Data\n"+
                    "0. Logout\n" +
                    "----------------------------------------");
                Console.Write("Enter your choice: ");
                user_choice = Convert.ToInt32(Console.ReadLine());
                switch (user_choice)
                {
                    case 1:
                        {
                            addUser();
                             Console.ReadKey(); break;
                        }
                    case 2:
                        {
                            addProduct();
                             Console.ReadKey(); break;
                        }
                    case 3:
                        {
                            addSubproduct();
                             Console.ReadKey(); break;
                        }
                    case 4:
                        {
                            getUserDataByID();
                             Console.ReadKey(); break;
                        }
                    case 5:
                        {
                            getUserData();
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Enter sub-product id you wish to delete: ");
                            deleteSubproductByID(Convert.ToInt32(Console.ReadLine()));
                            
                             Console.ReadKey(); break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Enter product id you wish to delete: ");
                            deleteProductByID(Convert.ToInt32(Console.ReadLine()));
                             Console.ReadKey(); break;
                        

                        }
                    case 8:
                        {
                            Console.Write("Please enter user ID to delete: ");
                            int deleteUserID = Convert.ToInt32(Console.ReadLine());
                            removeUser(deleteUserID);
                            break;
                        }
                    case 9:
                        {
                            getProductData();
                            break;

                        }
                        
                    case 0:
                        {
                            Console.Write("Are you sure you want to logout? (y/n)");
                            if ("y"==Console.ReadLine().ToLower())
                            {
                                AdminOperations.loggedIn =0;
                                System.Environment.Exit(0);
                            }
                            else
                            {
                                continue;
                            }
                            break;
                        }
                    case 10:
                        {
                            UserOperations.updateProductData();
                            break;
                        }
                    case 11:
                        {
                            updateUserData(); 
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please enter correct choice");
                             Console.ReadKey(); break;
                        }
                }
            }
        }

    }
}
