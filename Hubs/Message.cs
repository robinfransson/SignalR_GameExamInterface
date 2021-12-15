using System;

namespace WebProject.Hubs
{

    public class Message
    {
        public string User { get; set; }
        public string Text { get; set; }
        public string TimeStamp { get; set; }


        public Message()
        {
            TimeStamp = DateTime.Now.ToShortTimeString();
        }


    }
}
