bool isContinue = true;
while (isContinue)
{
    Console.WriteLine("1)Add Post To Database");
    Console.WriteLine("2)Get Missing Posts From Api");
    Console.WriteLine("3)Get Post Count Of User");
    Console.WriteLine("0)Exit");

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
                    await Methods.AddPostToDatabase(id);
                    break;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 2:
                try
                {
                    await Methods.GetMissingPostsFromApi();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 3:
                try
                {
                    Console.WriteLine("Enter user Id");
                    int userId = Convert.ToInt32(Console.ReadLine());
                    await Methods.GetPostCountOfUser(userId);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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

