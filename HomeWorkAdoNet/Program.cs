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
            case (int)Posts.Helper.Enum.AddPostToDatabase:
                try
                {
                    Console.WriteLine("Enter post Id");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green;
                    await Methods.AddPostToDatabaseAsync(id);
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
            case (int)Posts.Helper.Enum.GetMissingPostsFromApi:
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    await Methods.GetMissingPostsFromApiAsync();
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
            case (int)Posts.Helper.Enum.GetPostCountOfUser:
                try
                {

                    Console.WriteLine("Enter user Id");
                    int userId = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Post Count Of User is ");
                    await Methods.GetPostCountOfUserAsync(userId);
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
            case (int)Posts.Helper.Enum.Exit:
                isContinue = false;
                break;
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please enter correct option number");
        Console.ResetColor();
    }
}

