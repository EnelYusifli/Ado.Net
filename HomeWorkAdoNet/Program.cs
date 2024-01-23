bool isContinue = true;
while (isContinue)
{
    Console.WriteLine("----------------------------------------");
    Console.WriteLine("\n1)Add Post To Database");
    Console.WriteLine("2)Get Missing Posts From Api");
    Console.WriteLine("3)Get Post Count Of User");
    Console.WriteLine("0)Exit");
    Console.WriteLine("\nChoose an option");
    int intOption = Convert.ToInt32(Console.ReadLine());

    if (intOption >= 0 && intOption <= 3)
    {
        switch (intOption)
        {
            case 1:
                try
                {
                    Console.WriteLine("Enter post Id");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green;
                    await Methods.AddPostToDatabase(id);
                    Console.ResetColor();
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                break;
            case 2:
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    await Methods.GetMissingPostsFromApi();
                    Console.ResetColor();
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                break;
            case 3:
                try
                {

                    Console.WriteLine("Enter user Id");
                    int userId = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Post Count Of User is ");
                    await Methods.GetPostCountOfUser(userId);
                    Console.ResetColor();
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                break;
            case 0:
                isContinue = false;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter correct option number");
                Console.ResetColor();
                break;
        }
    }
}

