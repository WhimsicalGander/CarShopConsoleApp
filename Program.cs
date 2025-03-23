using CarClassLibrary;
using System.Security.Cryptography.X509Certificates;

namespace CarShopConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            Console.Out.WriteLine("Welcome to the car shop! First create some cars and add them to the inventory, then you may add cars to cart. Then you may checkout.");

            //initializes action number variable
            int action = ChooseAction();

            //action loop
            while (action != 0)
            {
                //takes action depending on number
                switch (action)
                {
                    //to add a car to the carlist
                    case 1:
                        Console.WriteLine("Enter the make of the car: ");
                        string make = Console.ReadLine();
                        Console.WriteLine("Enter the model of the car: ");
                        string model = Console.ReadLine();
                        Console.WriteLine("Enter the color of the car: ");
                        string color = Console.ReadLine();

                        try
                        {
                            Console.WriteLine("Enter the miles on the car: ");
                            int miles = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the year of the car: ");
                            int year = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the price of the car: ");
                            decimal price = decimal.Parse(Console.ReadLine());
                            Car car = new Car(make, model, color, miles, year, price);
                            store.carList.Add(car);
                        }
                        catch(OverflowException){
                            Console.WriteLine("Please enter a valid number");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter a valid number");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter a valid number");
                        }
                        break;

                    //to add a car to the cart
                    case 2:
                        PrintStoreInventory();
                        Console.WriteLine("Write the index of the car you'd like to add to cart");
                        int index = int.Parse(Console.ReadLine());
                        store.shoppingList.Add(store.carList[index]);
                        break;

                    //checkout
                    case 3:
                        decimal total = store.Checkout();
                        PrintShoppingList();
                        Console.WriteLine("Your total is: $" + total);
                        break;

                    //adds cars to the file
                    case 4:
                        FileIO fileIO = new FileIO(store);
                        fileIO.SaveInventory();
                        break;

                    //loads the cars from the file
                    case 5:
                        FileIO fileIO2 = new FileIO(store);
                        store.carList = fileIO2.LoadStore();
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

                PrintStoreInventory();

                action = ChooseAction();
            }
            
            //promputs user for action and returns action number
            int ChooseAction()
            {
                int choice = 0;

                Console.Out.WriteLine("Choose Action: 0 quit, 1 create car, 2 add car to cart, 3 checkout, 4 Save inventory to text file, 5 load text file.");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please Enter a valid number");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Please Enter a valid number");
                }
                catch (Exception)
                {
                    Console.WriteLine("Please Enter a valid number");
                }
                return choice;
            }

            //prints out all cars
            void PrintStoreInventory()
            {
                
                Console.Out.WriteLine("Inventory:");
                for (int i = 0; i < store.carList.Count; i++)
                {
                    Console.Out.WriteLine(i + ": " + store.carList[i]);
                }
            }

            //prints only the cars added to the cart
            void PrintShoppingList()
            {
                Console.Out.WriteLine("Shopping List:");
                for (int i = 0; i < store.shoppingList.Count; i++)
                {
                    Console.Out.WriteLine(i + ": " + store.shoppingList[i]);
                }
            }
        }
    }
}
