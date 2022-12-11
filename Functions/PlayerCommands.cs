using RpgGame.Models;

namespace RpgGame.Functions
{
    public class PlayerCommands
    {
        public static readonly int BaseHealth = 10;
        public static readonly int BaseStr = 5;
        public static readonly int BaseMagicPower = 0;
        public static readonly int BaseDexterity = 100;
        public static readonly int BaseArmor = 0;
        public static readonly int BaseSwiftness = 1;
        public static Player CreatePlayer(string Name,int ClassId,Guid AccId)
        {
            Player player = new Player();
            
            player.Id = Guid.NewGuid();
            player.Name = Name;
            player.ClassId = ClassId;
            player.AccountId = AccId;
            player.Health = PlayerCommands.BaseHealth;
            player.Strength = PlayerCommands.BaseStr;
            player.MagicPower = PlayerCommands.BaseMagicPower;
            player.Dexterity = PlayerCommands.BaseDexterity;
            player.Armor = PlayerCommands.BaseArmor;
            player.Swiftness = PlayerCommands.BaseSwiftness;
           
            return player; 
        }

        public static int CalculateDmg(object Entity,int multi)
        {
            int dmg = 0;
            if(Entity.GetType() == typeof(Player)){
                //PlayerDmg
                Player player = (Player)Entity;
                switch (player.ClassId)
                {
                    case 1:
                        dmg = player.Strength * multi;
                        return dmg;
                    case 2:
                        dmg = player.Dexterity * multi;
                        return dmg;
                    case 3:
                        dmg = player.MagicPower * multi;
                        return dmg;

                    default:
                        return dmg = 0;
                }



                return dmg = 0; 
            }
            else
            {
                //OpponentDmg
                Opponents opponent = (Opponents)Entity;
                dmg = 0;
                dmg = opponent.Damage * multi;
                return dmg;


            }  

        }

        public static bool AvoidCheck(int Swiftness)
        {
            Random random = new Random();
            int rand = random.Next(1,100);
            decimal percentage = ((decimal)Swiftness / 1000) * 100;
            int counter = Swiftness;
            while(counter>= 0)
            {
                counter--;
                int chance = random.Next(1,100);
                if (chance == rand)
                {
                    return true;
                }
                
            }
            if(counter == 0)
            {
                return false;
            }
            return false;

        }


        public static FightStats Fight(Player player, Opponents opponent)
        {
            
            while((player.Health >= 1) && (opponent.Health >= 1))
            {
                if (AvoidCheck(player.Swiftness))
                {
                    PlayerCommands.CalculateDmg(player, 2);
                }
                if (AvoidCheck(opponent.Swiftness))
                {
                    PlayerCommands.CalculateDmg(opponent, 2);
                }
            }

            FightStats es = new FightStats();
            return es;
        }





    }
}
