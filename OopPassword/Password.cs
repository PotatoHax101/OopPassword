using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPassword
{
    class Password
    {
        public Password(string userInputPassword) //Constructor for user input password
        {
            int score;

            score = caseScore(userInputPassword, 90, 65);

            Console.WriteLine(score);
            Console.ReadLine();

            //score = CalculatePassword(userInputPassword);
            //Result(userInputPassword, score);
        }

        Password() //Constructor for generating password
        {
            StringBuilder generatedPassword = new StringBuilder();
            generatedPassword = GeneratePassword();
        }

        public StringBuilder GeneratePassword()
        {
            throw new NotImplementedException();
        }

        public int CalculatePassword(string password)
        {
            int score = 0;
            int[] scoreArray = new int[6];

            string[] qwertyKeyboard = { "qwertyuiop", "asdfghjkl", "zxcvbnm" };

            scoreArray[0] = password.Length; //Length of password
            scoreArray[1] = caseScore(password, 122, 97); //Lowercase letters
            scoreArray[2] = caseScore(password, 90, 65); //Uppercase letters
            scoreArray[3] = digitScore(password);
            scoreArray[4] = symbolScore(password);
            scoreArray[5] = qwertyPenalty(password, qwertyKeyboard);

            if (scoreArray[1] != 0 & scoreArray[2] != 0 & scoreArray[3] != 0 & scoreArray[4] != 0) //There are lower and upper case letters, digits, and symbols
            {
                score += 10;
            }
            else if (scoreArray[1] != 0 & scoreArray[2] != 0 & scoreArray[3] == 0 & scoreArray[4] == 0) //There are lower and upper case letters; no digits or symbols
            {
                score -= 5;
            }
            else if (scoreArray[1] == 0 & scoreArray[2] == 0 & scoreArray[3] != 0 & scoreArray[4] == 0) //There are digits; no upper/lower case characters, or symbols
            {
                score -= 5;
            }

            for (int i = 0; i < scoreArray.Length; i++)
            {
                score += scoreArray[i];
            }

            return score;
        }

        private int caseScore(string password, int upperBound, int lowerBound) //Upper and lower bound represent the selection of ascii characters to look for
        {
            int localScore = 0;
            int stringIndex;

            for (int i = 0; i < password.Length; i++)
            {
                stringIndex = password[i];
                if (stringIndex <= upperBound & stringIndex >= lowerBound)
                {
                    localScore += 5;
                    break;
                }
            }

            return localScore;
        }

        private int digitScore(string password)
        {
            

            throw new NotImplementedException();
        }

        private int symbolScore(string password)
        {
            throw new NotImplementedException();
        }

        private int qwertyPenalty (string password, string[] qwertyKeyboard) //Please note, I apolgoise greatly for the quadruple nested for-loop
        {
            int penaltyScore = 0;
            char currentChar;
            StringBuilder consecutiveChars = new StringBuilder();
            StringBuilder consecutiveQWERTY = new StringBuilder();

            for (int i = 0; i < password.Length; i++) //Iterating on the number of characters
            {
                currentChar = password[i];
                for (int row = 0; row < 3; row++) //Iterating through the three alphabetical rows of the keyboard
                {
                    for (int t = 0; t < qwertyKeyboard[row].Length; t++) //Iterating through the characters of each
                    {
                        if (qwertyKeyboard[row][t] == currentChar) //If the current character = the character in the qwertyKeyboard array
                        {
                            for (int g = 0; g < 3; g++) //Getting two consecutive characters from the currentChar in the Password string, and quertyKeyboard[row] string
                            {
                                consecutiveChars.Append(password[i + g]); //Gets the next two characters after password[i]
                                consecutiveQWERTY.Append(qwertyKeyboard[row][t + g]); //Gets the next two characters after qwertyKeyboard[row][t]

                                if (consecutiveChars == consecutiveQWERTY) //If the characters after password[i] and qwertyKeyboard[row][t] match, a pattern has been found
                                {
                                    penaltyScore -= 5;
                                }
                            }
                            consecutiveChars.Clear();
                            consecutiveQWERTY.Clear();
                        }
                    }
                }
            }

            return penaltyScore;
        }

        public void Result(string password, int score)
        {

        }
    }
}
