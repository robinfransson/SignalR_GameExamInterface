using System;
using System.Threading.Tasks;

namespace WebProject.Controllers
{
    public class GameMiddleware
    {
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