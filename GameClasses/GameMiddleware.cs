using System;
using System.Threading.Tasks;

namespace WebProject.GameClasses
{
    public class GameMiddleware
    {
        public bool GameStarted { get; set; }
        public string UserInput { get; set; }
        public string Output { get; set; }
        public bool? Continue { get; set; }



        public async Task<string> AwaitInput()
        {
            while(UserInput is null)
            {
                await Task.Delay(500);
            }
            string input = UserInput;
            UserInput = null;
            return input;
        }


        public async Task<bool> AwaitAnswer()
        {
            while (Continue is null)
            {
                await Task.Delay(500);
            }
            bool cont = Continue.Value;
            Continue = null;
            return cont;
        }

    }
}