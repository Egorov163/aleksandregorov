using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_guess_the_number.Models
{
    public class GameRule
    {
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
        public int MaxAttemptCount { get; set; }
        public int TheNumber { get; set; }
    }
}
