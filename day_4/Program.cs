using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 4");

            string input = File.ReadAllText("input.txt");

            List<string> passportsRaw = input.Split("\n\n").ToList();

            int validPassports = 0;

            foreach (string passportRaw in passportsRaw)
            {
                int entryCount = 0;
                bool containsCid = false;
                bool entryValid = true;
                foreach (string ppSpace in passportRaw.Replace("\n", " ").Split(" ").ToList())
                {
                    if (!ppSpace.Contains(":"))
                    {
                        continue;
                    }
                    string entryKey = ppSpace.Split(":")[0];
                    string entryValue = ppSpace.Split(":")[1];
                    entryCount += 1;
                    //cid (Country ID) - ignored, missing or not.
                    if (entryKey.Equals("cid"))
                    {
                        if (!containsCid)
                        {
                            containsCid = true;
                        }
                    }
                    //byr (Birth Year) - four digits; at least 1920 and at most 2002.
                    else if (entryKey.Equals("byr"))
                    {
                        int.TryParse(entryValue, out int birthDate);
                        if (!(birthDate >= 1920 && birthDate <= 2002))
                        {
                            entryValid = false;
                            Console.WriteLine("Birth Date not valid");
                            continue;
                        }
                    }
                    //iyr (Issue Year) - four digits; at least 2010 and at most 2020.
                    else if (entryKey.Equals("iyr"))
                    {
                        int.TryParse(entryValue, out int issueDate);
                        if (!(issueDate >= 2010 && issueDate <= 2020))
                        {
                            entryValid = false;
                            Console.WriteLine("Issue Year not valid");
                            continue;
                        }
                    }
                    //eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
                    else if (entryKey.Equals("eyr"))
                    {
                        int.TryParse(entryValue, out int expDate);
                        if (!(expDate >= 2020 && expDate <= 2030))
                        {
                            entryValid = false;
                            Console.WriteLine("Exp date not valid");
                            continue;
                        }
                    }
                    //hgt (Height) - a number followed by either cm or in:
                    //    If cm, the number must be at least 150 and at most 193. hgt:192cm
                    //    If in, the number must be at least 59 and at most 76. hgt:156in
                    else if (entryKey.Equals("hgt"))
                    {
                        if (entryValue.EndsWith("cm"))
                        {
                            int.TryParse(entryValue.TrimEnd('m').TrimEnd('c'), out int height);
                            if (!(height >= 150 && height <= 193))
                            {
                                entryValid = false;
                                Console.WriteLine("Height cm not valid");
                                continue;
                            }
                        }
                        else if (entryValue.EndsWith("in"))
                        {
                            int.TryParse(entryValue.TrimEnd('n').TrimEnd('i'), out int height);
                            if (!(height >= 59 && height <= 76))
                            {
                                entryValid = false;
                                Console.WriteLine("Height in not valid");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Height not");
                            continue;
                        }

                    }
                    //hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                    else if (entryKey.Equals("hcl"))
                    {
                        try
                        {
                            if (entryValue.ToCharArray().Length != 7)
                            {
                                entryValid = false;
                                Console.WriteLine("Hair Color not valid");
                                continue;
                            }
                            int.Parse(entryValue.TrimStart('#'), System.Globalization.NumberStyles.HexNumber);
                        }
                        catch (Exception)
                        {
                            entryValid = false;
                            Console.WriteLine("Hair Color no hex");
                            continue;
                        }

                    }
                    //ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
                    else if (entryKey.Equals("ecl"))
                    {
                        if (!(entryValue.Equals("amb") ||
                                entryValue.Equals("blu") ||
                                entryValue.Equals("brn") ||
                                entryValue.Equals("gry") ||
                                entryValue.Equals("grn") ||
                                entryValue.Equals("hzl") ||
                                entryValue.Equals("oth")))
                        {
                            entryValid = false;
                            Console.WriteLine("Eye color not valid");
                            continue;
                        }
                    }
                    //pid (Passport ID) - a nine-digit number, including leading zeroes.
                    else if (entryKey.Equals("pid"))
                    {
                        try
                        {
                            if (entryValue.ToCharArray().Length != 9)
                            {
                                entryValid = false;
                                Console.WriteLine("PID not valid, length");
                                continue;
                            }
                            ulong.Parse(entryValue);
                        }
                        catch (Exception)
                        {
                            entryValid = false;
                            Console.WriteLine("PID not valid, parse");
                            continue;
                        }
                    }
                    else
                    {
                        entryValid = false;
                        Console.WriteLine("Entry id not valid: " + entryValue);
                        continue;
                    }
                }
                Console.WriteLine(passportRaw);
                Console.WriteLine("Entry Count: " + entryCount);
                Console.WriteLine("Contains CID: " + containsCid);
                Console.WriteLine("Entry Valid: " + entryValid);
                if ((entryCount == 8 && entryValid) || (entryCount == 7 && !containsCid && entryValid))
                {
                    validPassports++;
                    Console.WriteLine("Passport valid");
                }
                Console.WriteLine("################################################");
            }

            Console.WriteLine("Valid passports: " + validPassports);
        }
    }
}
