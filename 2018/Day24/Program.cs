using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day24
{
    class Program
    {
        //public class Unit
        //{
        //    public int hp;
        //    public string[] weakness;
        //    public string[] immunity;
        //    public int damage;
        //    public string damageType;
        //    public int initiative;
        //    public int type;

        //    public Unit(int hp, string[] weakness, string[] immunity, int damage, string damageType, int initiative, int type)
        //    {
        //        this.hp = hp;
        //        this.weakness = weakness;
        //        this.immunity = immunity;
        //        this.damage = damage;
        //        this.damageType = damageType;
        //        this.initiative = initiative;
        //        this.type = type;
        //    }
        //}

        public class UnitGroup
        {
            public int index;
            public int unitCount;
            public int hp;
            public int totalHp;
            public string[] weakness;
            public string[] immunity;
            public int damage;
            public string damageType;
            public int initiative;
            public int type;

            public UnitGroup(int index, int unitCount, int hp, int totalHp, string[] weakness, string[] immunity, int damage, string damageType, int initiative, int type)
            {
                this.index = index;
                this.unitCount = unitCount;
                this.hp = hp;
                this.totalHp = totalHp;
                this.weakness = weakness;
                this.immunity = immunity;
                this.damage = damage;
                this.damageType = damageType;
                this.initiative = initiative;
                this.type = type;
            }
        }
        static void Main(string[] args)
        {
            /*
             *    Immune System:
             *    597 units each with 4458 hit points with an attack that does 73 slashing damage at initiative 6
             *    4063 units each with 9727 hit points (weak to radiation) with an attack that does 18 radiation damage at initiative 9
             *    2408 units each with 5825 hit points (weak to slashing; immune to fire, radiation) with an attack that does 17 slashing damage at initiative 2
             *    5199 units each with 8624 hit points (immune to fire) with an attack that does 16 radiation damage at initiative 15
             *    1044 units each with 4485 hit points (weak to bludgeoning) with an attack that does 41 radiation damage at initiative 3
             *    4890 units each with 9477 hit points (immune to cold; weak to fire) with an attack that does 19 slashing damage at initiative 7
             *    1280 units each with 10343 hit points with an attack that does 64 cold damage at initiative 19
             *    609 units each with 6435 hit points with an attack that does 86 cold damage at initiative 17
             *    480 units each with 2750 hit points (weak to cold) with an attack that does 57 fire damage at initiative 11
             *    807 units each with 4560 hit points (immune to fire, slashing; weak to bludgeoning) with an attack that does 56 radiation damage at initiative 8
             *
             *    Infection:
             *    1237 units each with 50749 hit points (weak to radiation; immune to cold, slashing, bludgeoning) with an attack that does 70 radiation damage at initiative 12
             *    4686 units each with 25794 hit points (immune to cold, slashing; weak to bludgeoning) with an attack that does 10 bludgeoning damage at initiative 14
             *    1518 units each with 38219 hit points (weak to slashing, fire) with an attack that does 42 radiation damage at initiative 16
             *    4547 units each with 21147 hit points (weak to fire; immune to radiation) with an attack that does 7 slashing damage at initiative 4
             *    1275 units each with 54326 hit points (immune to cold) with an attack that does 65 cold damage at initiative 20
             *    436 units each with 36859 hit points (immune to fire, cold) with an attack that does 164 fire damage at initiative 18
             *    728 units each with 53230 hit points (weak to radiation, bludgeoning) with an attack that does 117 fire damage at initiative 5
             *    2116 units each with 21754 hit points with an attack that does 17 bludgeoning damage at initiative 10
             *    2445 units each with 21224 hit points (immune to cold) with an attack that does 16 cold damage at initiative 13
             *    3814 units each with 22467 hit points (weak to bludgeoning, radiation) with an attack that does 10 cold damage at initiative 1
             */

            var matchRegex = new Regex(@"(\d+) units each with (\d+) hit points (\((((\w+) to ((\w+)(, )?)*))?((; )?((\w+) to ((\w+)(, )?)*))??\) )?with an attack that does (\d+) (\w+) damage at initiative (\d+)");
            var input = File.ReadAllLines("Input.txt");
            //var units = new List<Unit>();
            var unitGroups = new List<UnitGroup>();
            int type = 0;
            int index = 1;
            foreach (var line in input)
            {
                if (line.StartsWith("Infection"))
                {
                    type = 1;
                    index = 1;
                }



                var match = matchRegex.Match(line);
                if (match.Success)
                {
                    var unitCount = int.Parse(match.Groups[1].Value);
                    var hp = int.Parse(match.Groups[2].Value);
                    var weakness = new List<string>();
                    var immunity = new List<string>();
                    if (match.Groups[8].Value.Length > 0)
                    {
                        var type1 = match.Groups[6].Value;
                        //weakness
                        foreach (Capture capture in match.Groups[8].Captures)
                        {
                            if (type1 == "weak")
                            {
                                weakness.Add(capture.Value);
                            }
                            else
                            {
                                immunity.Add(capture.Value);
                            }
                        }

                    }
                    if (match.Groups[15].Value.Length > 0)
                    {
                        var type1 = match.Groups[13].Value;
                        //immunity
                        foreach (Capture capture in match.Groups[15].Captures)
                        {
                            if (type1 == "weak")
                            {
                                weakness.Add(capture.Value);
                            }
                            else
                            {
                                immunity.Add(capture.Value);
                            }
                        }

                    }


                    var damage = int.Parse(match.Groups[17].Value);
                    var damageType = match.Groups[18].Value;
                    var initiative = int.Parse(match.Groups[19].Value);

                    //for (int i = 0; i < unitCount; i++)
                    //{
                    //    units.Add(new Unit(hp, weakness.ToArray(), immunity.ToArray(), damage, damageType, initiative, type));
                    //}
                    unitGroups.Add(new UnitGroup(index, unitCount, hp, hp * unitCount, weakness.ToArray(), immunity.ToArray(), damage, damageType, initiative, type));

                    //Console.WriteLine("Match: " + match.Value);
                    //int groupIndex = 0;
                    //foreach (Group group in match.Groups)
                    //{
                    //    Console.WriteLine("     Group" + groupIndex + ": " + group.Value);
                    //    int captureIndex = 0;
                    //    if (group.Captures.Count > 1)
                    //    {
                    //        foreach (Capture capture in group.Captures)
                    //        {
                    //            Console.WriteLine("          Capture" + captureIndex + ": " + capture.Value);
                    //            captureIndex++;
                    //        }
                    //    }
                    //    groupIndex++;
                    //}
                    index++;
                }
                //else
                //{
                //    Console.WriteLine("No Match: " + line);
                //}
            }


            var initialGroups = unitGroups;

            var boost = 43;
            while (true)
            {
                unitGroups = initialGroups.Select(g => new UnitGroup(g.index, g.unitCount, g.hp, g.hp * g.unitCount, g.weakness, g.immunity, g.damage + (g.type == 0 ? boost : 0), g.damageType, g.initiative, g.type)).ToList();
                while (unitGroups.Count(g => g.type == 0 && g.totalHp > 0) > 0 && unitGroups.Count(g => g.type == 1 && g.totalHp > 0) > 0)
                {
                    //Console.WriteLine();
                    //foreach (var unitGroup in unitGroups.Where(g => g.totalHp > 0))
                    //{
                    //    Console.WriteLine((unitGroup.type == 0 ? "Immune" : "Infection") + " group " + unitGroup.index + " contains " + unitGroup.unitCount + " units");
                    //}
                    //Console.WriteLine();
                    
                    //target selection
                    //in decreasing order of effective power
                    //in a tie, the group with the higher initiative chooses first
                    var attackGroups = new List<(UnitGroup attacker, UnitGroup attackee, int damage)> ();
                    foreach (var attackGroup in unitGroups.Where(g => g.totalHp > 0).OrderByDescending(g => effectivePower(g)).ThenByDescending(g => g.initiative))
                    {
                        var groupToAttack = unitGroups.Where(g => g.totalHp > 0 && g.type != attackGroup.type && !attackGroups.Any(ag => ag.attackee == g)).Select(g => new { group = g, damage = getDamage(attackGroup, g), power = effectivePower(g) }).Where(g => g.damage > 0).OrderByDescending(g => g.damage).ThenByDescending(g => g.power).ThenByDescending(g => g.group.initiative).FirstOrDefault();
                        if (groupToAttack != null)
                        {
                            attackGroups.Add((attackGroup, groupToAttack.group, groupToAttack.damage));
                        }
                    }
                    //attack
                    foreach (var attackGroup in attackGroups.OrderByDescending(a => a.attacker.initiative))
                    {
                        var damage = getDamage(attackGroup.attacker, attackGroup.attackee);
                        //attackGroup.attackee.totalHp -= damage;
                        //Console.WriteLine((attackGroup.attacker.type == 0 ? "Immune" : "Infection") + " group " + attackGroup.attacker.index + " deals " + (attackGroup.attackee.type == 0 ? "Immune" : "Infection") + " group " + attackGroup.attackee.index + " " + attackGroup.damage + " damage");

                        var losses = Math.Min((int)Math.Floor(damage / (double)attackGroup.attackee.hp), attackGroup.attackee.unitCount);
                        //Console.WriteLine(losses + " losses");
                        attackGroup.attackee.unitCount -= losses;
                        attackGroup.attackee.totalHp = attackGroup.attackee.hp * attackGroup.attackee.unitCount;

                        //if (attackGroup.attackee.totalHp > 0)
                        //{
                        //    attackGroup.attackee.totalHp = (int)Math.Ceiling(attackGroup.attackee.totalHp / (double)attackGroup.attackee.hp) * attackGroup.attackee.hp;
                        //    attackGroup.attackee.unitCount = attackGroup.attackee.totalHp / attackGroup.attackee.hp;
                        //}
                        //else
                        //{
                        //    attackGroup.attackee.unitCount = 0;
                        //    attackGroup.attackee.totalHp = 0;
                        //}
                    }

                    //foreach (var unitGroup in unitGroups)
                    //{
                    //    if (unitGroup.totalHp > 0)
                    //    {
                    //        unitGroup.totalHp = (int)Math.Ceiling(unitGroup.totalHp / (double)unitGroup.hp) * unitGroup.hp;
                    //        unitGroup.unitCount = unitGroup.totalHp / unitGroup.hp;
                    //    }
                    //    else
                    //    {
                    //        unitGroup.unitCount = 0;
                    //        unitGroup.totalHp = 0;
                    //    }
                    //}

                    //Console.WriteLine();
                }
                if (unitGroups.Where(g => g.totalHp > 0 && g.type == 0).Count() > 0)
                {
                    break;
                }
                boost+=1;
            }

            var result = unitGroups.Where(g => g.totalHp > 0).Sum(g => g.unitCount);
            Console.WriteLine(result);
            Console.ReadLine();
            //20753 == too high
        }

        private static int getDamage(UnitGroup attackGroup, UnitGroup defendGroup)
        {
            if (defendGroup.immunity.Contains(attackGroup.damageType))
            {
                return 0;
            }
            if (defendGroup.weakness.Contains(attackGroup.damageType))
            {
                return effectivePower(attackGroup) * 2;
            }
            return effectivePower(attackGroup);
        }

        private static int effectivePower(UnitGroup g)
        {
            return g.unitCount * g.damage;
        }
    }
}
