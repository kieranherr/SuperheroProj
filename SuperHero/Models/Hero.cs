using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHero.Models
{
    public class Hero
    {
        [Key]
        public string Name { get; set; }
        public string AlterEgo { get; set; }
        public string PrimeAbility { get; set; }
        public string SecondAbility { get; set; }
        public string CatchPhrase { get; set; }


    }
}
