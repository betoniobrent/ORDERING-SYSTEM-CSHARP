using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace OrderingProgram
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] code = { "F1", "F2", "F3", "F4", "F5", "F6", "D1", "D2", "D3", "D4", "D5", "D6", "E1", "E2", "E3", "E4", "E5", "E6", "X" };
            string[] menu = { "Tapsilog", "Porksilog", "Chicksilog", "Tocilog", "Sisiglog", "Bacsilog",
                              "Water", "Sprite", "Ice tea", "Coke", "Mountain Dew", "Pineapple Juice",
                              "Leche Flan", "Halo-Halo", "Biko", "Buko Pandan", "Taho" , "Espasol", "Finish Ordering"};
            decimal[] price = { 50.00m, 55.00m, 60.00m, 55.00m, 65.00m, 70.00m,
                                20.00m, 20.00m, 25.00m, 25.00m, 30.00m, 25.00m,
                                40.00m, 45.00m, 35.00m, 40.00m, 25.00m, 20.00m, 0 };

            string strprice = "";

            string transact = "N";
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------");
                Console.WriteLine("| WELCOME TO MONKEY EATERY |");
                Console.WriteLine("----------------------------\n");
                Console.ResetColor();


                Console.Write("\nEnter customer name: ");
                string customerName = Console.ReadLine();

                bool isValidInput = false;
                string orderTypeString = "";

                while (!isValidInput)
                {
                    Console.Write("\nEnter 'D' for Dine-in or 'T' for Take-out: ");
                    string orderType = Console.ReadLine().Trim().ToLower();

                    if (orderType == "d")
                    {
                        orderTypeString = "Dine-in";
                        isValidInput = true;
                    }
                    else if (orderType == "t")
                    {
                        orderTypeString = "Take-out";
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input. Please enter 'D' for Dine-in or 'T' for Take-out.");

                    }
                }





                Console.WriteLine("\n--------------- MAIN COURSES --------------");
                DisplayMenuSection(code, menu, price, 0, 5);

                Console.WriteLine("\n----------------- DRINKS ------------------");
                DisplayMenuSection(code, menu, price, 6, 11);

                Console.WriteLine("\n----------------- DESSERTS ----------------");
                DisplayMenuSection(code, menu, price, 12, 17);

                string[] order_list = new string[1];
                int qty;
                string strQty;
                string coup;
                decimal subtotal = 0;
                string order;
                int code_index;
                int current_order_index = 0;
                decimal grand_total = 0;
                Console.WriteLine("\n-------------------------------------------");
                do
                {
                    //take orders
                    Console.Write("Enter menu code (Enter 'X' to finish ordering) :");
                    order = Console.ReadLine().ToUpper();
                    code_index = Array.IndexOf(code, order);
                    if (code_index < 0)
                    {
                        Console.WriteLine("Invalid code!!!!");
                    }
                    else
                    {
                        if (order != "X")
                        {
                            do
                            {
                                Console.Write("Enter Qty: ");
                                strQty = Console.ReadLine();
                                if (int.TryParse(strQty, out qty) == false)
                                {
                                    Console.WriteLine("Invalid quantity value!!!");
                                }
                            }
                            while (int.TryParse(strQty, out qty) == false);

                            subtotal = price[code_index] * qty;
                            grand_total = grand_total + subtotal;
                            order_list[current_order_index] = order.PadRight(10) + menu[code_index].PadRight(30) +
                                price[code_index].ToString().PadRight(10) + qty.ToString().PadRight(10) + subtotal.ToString().PadLeft(10);

                            Array.Resize(ref order_list, order_list.Length + 1);
                            current_order_index++;
                        }
                        else
                        {
                            if (grand_total == 0)
                            {
                                Environment.Exit(0);
                            }
                        }
                    }
                } while (order != "X");



                decimal amount_tendered = 0;
                decimal change = 0;
                string str_amount;
                Console.WriteLine("\n----------------------------------------------------------------------");
                Console.Write("\nDo you have a coupon? (Y/N): ");
                string input = Console.ReadLine().ToUpper();

                decimal discountPercentage = 0;

                if (input == "Y")
                {

                    Console.Write("Please enter the coupon discount percentage: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal enteredPercentage))
                    {
                        discountPercentage = enteredPercentage;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input. Using default discount (0%).");
                    }
                }
                else if (input == "N")
                {
                    discountPercentage = 0;
                }
                else
                {
                    Console.WriteLine("\nInvalid input - Using default discount (0%).");
                }

                Console.Write("\nWould you like to give a tip? (Type 'Y' for Yes or 'N' for No): ");
                string wantToTip = Console.ReadLine().ToUpper();

                decimal tipAmount = 0;


                if (wantToTip == "Y")
                {
                    Console.Write("\nEnter the tip amount: ");
                    string tipInput = Console.ReadLine();

                    if (decimal.TryParse(tipInput, out tipAmount))
                    {

                        Console.WriteLine($"Thank you for your tip of {tipAmount:C}. It's greatly appreciated!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid tip amount. No tip will be added.");
                    }
                }
                else if (wantToTip == "N")
                {
                    Console.WriteLine("\nNo tip will be added.");
                }
                else
                {
                    Console.WriteLine("\nInvalid Input - No tip will be added.");
                }




                Console.WriteLine("\n----------------------------------------------------------------------");

                if (grand_total > 0)
                {


                    Console.WriteLine("\n---------------------------- ORDER SUMMARY ---------------------------");
                    Console.WriteLine("\nCode".PadRight(11) + "Menu".PadRight(30) + "Price".PadRight(10) + "Qty".PadRight(10) + "Sub Total".PadLeft(10));
                    for (int i = 0; i < order_list.Length - 1; i++)
                    {
                        Console.WriteLine(order_list[i]);
                    }

                    // Prompt to remove an order
                    Console.WriteLine("\nEnter the menu code of the item you want to remove: (Enter 'X' to finish)");
                    string removeCode = Console.ReadLine().ToUpper();



                    while (removeCode != "X")
                    {
                        bool found = false;

                        Console.WriteLine("Enter the quantity to remove:");
                        int quantityToRemove = int.Parse(Console.ReadLine());

                        for (int i = 0; i < order_list.Length - 1; i++)
                        {
                            if (order_list[i].StartsWith(removeCode))
                            {
                                found = true;

                                string[] orderDetails = order_list[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                decimal orderSubtotal = decimal.Parse(orderDetails[orderDetails.Length - 1]);
                                int orderQty = int.Parse(orderDetails[orderDetails.Length - 2]);

                                if (quantityToRemove <= orderQty)
                                {
                                    decimal pricePerItem = orderSubtotal / orderQty;
                                    decimal subtotalToRemove = pricePerItem * quantityToRemove;
                                    grand_total -= subtotalToRemove;

                                    if (quantityToRemove < orderQty)
                                    {
                                        orderDetails[orderDetails.Length - 2] = (orderQty - quantityToRemove).ToString();
                                        orderDetails[orderDetails.Length - 1] = ((orderQty - quantityToRemove) * pricePerItem).ToString("#,0.00");
                                        order_list[i] = string.Join(" ", orderDetails);
                                    }
                                    else
                                    {
                                        order_list[i] = " ";
                                    }

                                    Console.WriteLine($"Order '{removeCode}' (x{quantityToRemove}) has been removed.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Quantity to remove exceeds available quantity.");
                                    break;
                                }
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine("Invalid menu code! Enter a valid menu code to remove or 'X' to finish.");
                        }

                        Console.WriteLine("\nEnter the menu code of the item you want to remove (or 'X' to finish removing):");
                        removeCode = Console.ReadLine().ToUpper();
                    }


                    Console.WriteLine("\n----------------------------------------------------------------------");

                    decimal totalWithDiscount = grand_total - (grand_total * (discountPercentage / 100));
                    decimal totalWithTip = totalWithDiscount + tipAmount;
                    //accept payment and compute change
                    do
                    {
                        do
                        {

                            string str_total1 = "Total Amount: " + totalWithTip.ToString("#,0.00");
                            Console.WriteLine(str_total1);
                            Console.Write("\nEnter amount tendered: ");
                            str_amount = Console.ReadLine();

                        } while (decimal.TryParse(str_amount, out amount_tendered) == false);

                        if (Convert.ToDecimal(str_amount) < totalWithTip)
                        {
                            Console.WriteLine("Amount tendered must be greater than the total amount...");
                        }


                    } while (Convert.ToDecimal(str_amount) < totalWithTip);

                    Console.Clear();
                    change = amount_tendered - totalWithTip;
                    Random random = new Random();
                    int minRange = 1;
                    int maxRange = 100;
                    int randomNumber = random.Next(minRange, maxRange + 1);


                    Console.Clear();
                    Console.WriteLine("\n------------------------------- RECEIPT ------------------------------");
                    Console.WriteLine("\n---------------------------- MONKEY EATERY ---------------------------");
                    Console.WriteLine("\nCode".PadRight(11) + "Menu".PadRight(30) + "Price".PadRight(10) + "Qty".PadRight(10) + "Sub Total".PadLeft(10));

                    for (int i = 0; i < order_list.Length - 1; i++)
                    {
                        if (order_list[i].StartsWith(" "))
                        {
                            Console.WriteLine(order_list[i]);
                        }
                        else
                        {
                            string currentOrder = order_list[i];
                            Console.WriteLine(currentOrder);
                        }
                    }
                    Console.WriteLine("\n----------------------------------------------------------------------");

                    totalWithDiscount = grand_total - (grand_total * (discountPercentage / 100));
                    totalWithTip = totalWithDiscount + tipAmount;


                    string dateTimeNow = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    Console.WriteLine("Date and Time: ".PadRight(50) + dateTimeNow);
                    Console.WriteLine("Server Name: ".PadRight(50) + "ADMIN");
                    Console.WriteLine("Customer Name: ".PadRight(50) + customerName);
                    Console.WriteLine("Order Number: ".PadRight(50) + randomNumber);
                    Console.WriteLine("Order Type: ".PadRight(50) + orderTypeString);
                    Console.WriteLine("\n----------------------------------------------------------------------");
                    Console.WriteLine("Amount Given: ".PadRight(50) + str_amount);
                    Console.WriteLine($"Total Amount (Discount {discountPercentage} %): ".PadRight(50) + totalWithDiscount.ToString("#,0.00"));
                    Console.WriteLine("Given Tip: ".PadRight(50) + tipAmount);
                    Console.WriteLine("Total Amount: ".PadRight(50) + totalWithTip);
                    Console.WriteLine("Change: ".PadRight(50) + change.ToString("#,0.00"));

                    Console.WriteLine("\n--------------- THANK-YOU-FOR-VISITING-MONKEY-EATERY -----------------");

                }
                Console.WriteLine("\n----------------------------------------------------------------------");


                do
                {
                    Console.Write("\nAnother trasaction:(Y/N): ");
                    transact = Console.ReadLine().ToUpper();
                } while (transact != "Y" && transact != "N");



            } while (transact != "N");
            Console.WriteLine("Press any key to exit.....");


            Console.ReadKey();
        }
        static void DisplayMenuSection(string[] code, string[] menu, decimal[] price, int startIdx, int endIdx)
        {
            for (int i = startIdx; i <= endIdx; i++)
            {
                Console.WriteLine($"[{code[i]}] {menu[i]}".PadRight(30) + $"{price[i]:C}".PadLeft(10));

            }
        }
    }
}