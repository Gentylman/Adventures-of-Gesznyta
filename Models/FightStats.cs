namespace RpgGame.Models
{
    public class FightStats
    {
        public List<int> DamageGiven { get; set; }
        public List<int> DamageTaken { get; set; }
        public List<Items> drops { get; set; }
        public int Exp { get; set; }
        public int Coins { get; set; }



    }
}
