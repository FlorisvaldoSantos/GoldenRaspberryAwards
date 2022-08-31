using System;

namespace Domain
{
    public class ProducerDTO : Base
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
        public DateTime CreatAt { get; set; }

        public ProducerDTO() 
        {
            CreatAt = DateTime.Now;
        }

        public ProducerDTO(string producer, int interval, int previousWin, int followingWin)
        {
            Producer = producer;
            Interval = interval;
            PreviousWin = previousWin;
            FollowingWin = followingWin;
            CreatAt = DateTime.Now;
        }

    }
}
