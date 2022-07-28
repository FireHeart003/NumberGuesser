using System;
using System.Data.SqlClient;

// Namespace
namespace NumberGuesser
{
    // Main Class
    class Program
    {
        /*private int score = 0;
        String lastSavedDate = "";
        String inputName;*/

        // Entry Point Method
        static void Main(string[] args)
        {
            PlayGame();
        }

        static void PlayGame(){
            
           GetAppInfo();

            String userName = GreetUser();

            while (true)
            {

                // Init Set correct number
                //int correctNumber = 7;

                //Create a new ranodom object
                Random random = new Random();

                // Init Set correct number
                int correctNumber = random.Next(1, 10);

                // Init guess var
                int guess = 0;

                int cnt = 1;
                String lastSavedDate = "";

                Console.WriteLine("Guess a number between 1 and 10");

                // While guess is not correct
                while (guess != correctNumber)
                {
                    // Get users input
                    string input = Console.ReadLine();

                    // Make sure its a number
                    if (!int.TryParse(input, out guess))//?
                    {
                        //Print error message
                        PrintColorMessage(ConsoleColor.Red, "Please use an actual number.");

                        continue;//?
                     
                    }

                    // Cast to int and put in guess variable
                    guess = Int32.Parse(input);

                    // Match guess to correct number
                    if (guess != correctNumber)
                    {
                        cnt++;
                        // Print error message
                        PrintColorMessage(ConsoleColor.Red, "Wrong number, please try again.");
                    }

                }

                //Output success message
                PrintColorMessage(ConsoleColor.Yellow, "You are CORRECT!!!");

                SaveScore(cnt,userName);

                // Ask to play again
                Console.WriteLine("Play Again? [Y or N]");

                // Get answer
                string answer = Console.ReadLine().ToUpper();

                if (answer == "Y")
                {
                    continue;
                }
                else if (answer == "N")//?
                {
                    return;
                }
                else
                {
                    return;
                }


            }

        }

        // Ask to Save score
        static void SaveScore(int score, String user)
        {
            Console.WriteLine("Would you like to save your score? [Y or N]");
            string answer = Console.ReadLine().ToUpper();
            if (answer == "Y")
            {
                //Console.WriteLine(score + user);
                SqlConnection _con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\guar0\Documents\NumberGuesserData.mdf;Integrated Security=True;Connect Timeout=30");
                String currentDate = DateTime.Now.ToString("MMM dd yyyy");
                SqlCommand _cmd = new SqlCommand("INSERT INTO TableTwo (USERNAME, SCORES, DATES) VALUES(@a1,@a2,@a3)",_con); // + "', DATES='" + currentDate + "' WHERE USERNAME='" + user + "';", _con);
                _cmd.Parameters.AddWithValue("@a1", user);
                _cmd.Parameters.AddWithValue("@a2", score);
                _cmd.Parameters.AddWithValue("@a3", DateTime.Now.ToString("MMM dd yyyy"));
               SqlDataReader myReader;
                try
                {
                    _con.Open();
                    myReader = _cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _con.Close();
            }
            else
            {
                return;
            }

        }

        /* SqlConnection _conLoad = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\guar0\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
            _conLoad.Open();
            SqlCommand cmdLoad = new SqlCommand("Select Message, DATE from Login WHERE USERNAME = '" + userName + "';",_conLoad);
            SqlDataReader da = cmdLoad.ExecuteReader();
            while (da.Read())//why a while loop
            {
                textBox1.Text = da.GetValue(0).ToString();
                saveDate = da.GetValue(1).ToString();
                lastSavedDate.Text = saveDate;
            }
            _conLoad.Close();
*/
        // Get and display app info
        static void GetAppInfo()
        {
            // Set app vars
            string appName = "Number Guesser";
            string appVersion = "1.0.0";
            string appAuthor = "Nathan Chen";

            // Change text color
            Console.ForegroundColor = ConsoleColor.Green;

            // Write out app info
            Console.WriteLine("{0}: Version {1} by {2}", appName, appVersion, appAuthor);

            // Reset text color
            Console.ResetColor();
        }

        // Asks user name and greet
        static String GreetUser()
        {
            // Ask users name
            Console.WriteLine("What is your name?");

            // Get user input 
            String inputName = Console.ReadLine();

            Console.WriteLine("Hello {0}, let's play a game...", inputName);
            return inputName;
        }

        
        // Print color message
        static void PrintColorMessage(ConsoleColor color, String message)
        {
            // Change text color
            Console.ForegroundColor = color;
            // Write out app info
            Console.WriteLine(message);

            // Reset text color
            Console.ResetColor();
        }
    }
}
