using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPassword
{
    class Password
    {
        private readonly int[] specialCharacters = { 33, 36, 37, 94, 38, 42, 40, 41, 45, 95, 61, 43 };

        //Constructor for user input password
        public Password(StringBuilder userInputPassword) 
        {
            int score = CalculatePassword(userInputPassword);
            string strength = strengthCheck(score);
            Result(userInputPassword, score, strength);
        }

        //Constructor for generating password
        public Password() 
        {
            StringBuilder generatedPassword = new StringBuilder();

            string strength = "";
            int score = 0;

            generatedPassword = GeneratePassword(ref score, strength);

            Result(generatedPassword, score, strength);
        }

        public StringBuilder GeneratePassword(ref int score, string strength)
        {
            StringBuilder createdPassword = new StringBuilder();

            Random random = new Random();
            int length = random.Next(8, 13); // Generate rand# between 8 - 12
            int randomSelection = 0;
            char randomCharacter = ' ';

            do
            {
                createdPassword.Clear();
                //Chose random character from array of allowed ascii characters
                for (int i = 0; i < length; i++)
                {
                    randomSelection = random.Next(0, 4);

                    switch (randomSelection)
                    {
                        case 0:
                            randomCharacter = (char)random.Next('A', 'Z' + 1);
                            break;
                        case 1:
                            randomCharacter = (char)random.Next('a', 'z' + 1);
                            break;
                        case 2:
                            randomCharacter = (char)random.Next('0', '9' + 1);
                            break;
                        case 3:
                            randomCharacter = (char)(specialCharacters[random.Next(0, specialCharacters.Length)]);
                            break;
                    }

                    createdPassword.Append(randomCharacter);
                }


                score = CalculatePassword(createdPassword);
                strength = strengthCheck(score);

            } while (strength != "Strong");

            return createdPassword;
        }

        public int CalculatePassword(StringBuilder password)
        {
            int score = 0;
            int[] scoreArray = new int[6];

            string[] qwertyKeyboard = { "qwertyuiop", "asdfghjkl", "zxcvbnm" };

            scoreArray[0] = password.Length; //Length of password
            scoreArray[1] = asciiBoundScore(password, 122, 97); //Lowercase letters
            scoreArray[2] = asciiBoundScore(password, 90, 65); //Uppercase letters
            scoreArray[3] = asciiBoundScore(password, 57, 48); //Digits
            scoreArray[4] = symbolScore(password, specialCharacters);
            scoreArray[5] = qwertyPenalty(password, qwertyKeyboard);

            //There are lower and upper case letters, digits, and symbols
            if (scoreArray[1] != 0 & scoreArray[2] != 0 & scoreArray[3] != 0 & scoreArray[4] != 0)
            {
                score += 10;
            }
            //There are lower and upper case letters; no digits or symbols
            else if (scoreArray[1] != 0 & scoreArray[2] != 0 & scoreArray[3] == 0 & scoreArray[4] == 0)
            {
                score -= 5;
            }
            //There are digits; no upper/lower case characters, or symbols
            else if (scoreArray[1] == 0 & scoreArray[2] == 0 & scoreArray[3] != 0 & scoreArray[4] == 0)
            {
                score -= 5;
            }
            
            //Adding score together
            for (int i = 0; i < scoreArray.Length; i++)
            {
                score += scoreArray[i];
            }

            return score;
        }

        //Upper and lower bound represent the selection of ascii characters to look for
        private int asciiBoundScore(StringBuilder password, int upperBound, int lowerBound)
        {
            int localScore = 0;
            int stringIndex;

            //Checks each character for a match
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

        private int symbolScore(StringBuilder password, int[] specialCharacters)
        {
            int localScore = 0;
            int stringIndex;

            for (int i = 0; i < password.Length; i++)
            {
                stringIndex = password[i];
                for (int j = 0; j < specialCharacters.Length; j++)
                {
                    if (stringIndex == specialCharacters[j])
                    {
                        localScore += 5;
                        return localScore;
                    }
                }
            }

            return localScore;
        }

        private int qwertyPenalty (StringBuilder password, string[] qwertyKeyboard) //Please note: I apolgoise greatly for the quadruple nested for-loop
        {
            int penaltyScore = 0;
            char currentChar;
            StringBuilder consecutiveChars = new StringBuilder(); //Using stringbuilder as it's better for iteration
            StringBuilder consecutiveQWERTY = new StringBuilder();

            //Iterating on the number of characters
            for (int i = 0; i < password.Length; i++)
            {
                currentChar = password[i];

                //Iterating through the three alphabetical rows of the keyboard
                for (int row = 0; row < 3; row++)
                {
                    //Iterating through the characters of each
                    for (int t = 0; t < qwertyKeyboard[row].Length; t++)
                    {
                        //If the current character = the character in the qwertyKeyboard array
                        if (qwertyKeyboard[row][t] == currentChar)
                        {
                            //Getting two consecutive characters from the currentChar in the Password string, and qwertyKeyboard[row] string
                            for (int g = 0; g < 3; g++)
                            { 
                                if ((i + g) < password.Length & (t + g) < qwertyKeyboard[row].Length)
                                {
                                    //Gets the next two characters after password[i]
                                    consecutiveChars.Append(Char.ToLower(password[i + g]));
                                    //Gets the next two characters after qwertyKeyboard[row][t]
                                    consecutiveQWERTY.Append(Char.ToLower(qwertyKeyboard[row][t + g]));
                                }
                            }
                            //If the characters after password[i] and qwertyKeyboard[row][t] match, a pattern has been found
                            if (consecutiveChars.Equals(consecutiveQWERTY) == true)
                            {
                                penaltyScore -= 5;
                            }
                            consecutiveChars.Clear();
                            consecutiveQWERTY.Clear();
                        }
                    }
                }
            }

            return penaltyScore;
        }

        private string strengthCheck(int score)
        {
            string strength;

            if (score >= 20)
            {
                strength = "Strong";
            }
            else if (score < 20 & score >= 0)
            {
                strength = "Medium";
            }
            else
            {
                strength = "Weak";
            }

            return strength;
        }

        public void Result(StringBuilder password, int score, string passwordStrength)
        {
            passwordStrength = strengthCheck(score);

            Console.WriteLine($"Grading the password '{password}'");
            Console.WriteLine($"Password Score: {score}");
            Console.WriteLine($"Password Strength: {passwordStrength}");

            Console.ReadLine();
        }
    }
}
