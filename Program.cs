using System.Text;
using SafeBanking.Services;

class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        int status = -1;

        while (status != 4)
        {
            Console.WriteLine("=== Welcome to Zedbank! ===");
            Console.WriteLine("Choose an Option");
            Console.WriteLine("1 - Log In");
            Console.WriteLine("2 - Sign Up");
            Console.WriteLine("3 - Cancel My Account");
            Console.WriteLine("4 - Exit");
            
            if (!int.TryParse(Console.ReadLine(), out status))
            {
                Response.InvalidInput();
                continue;
            }
            
            switch (status)
            {
                case 1:
                    Auth.LogIn();
                    break;

                case 2:
                    Auth.SignUp();
                    break;

                case 3:
                    Auth.DeleteAccount();
                    break;

                case 4:
                    break;

                default:
                    Response.InvalidReturn();
                    break;
            }
        }
    }
}