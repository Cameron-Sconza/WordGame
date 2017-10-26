using Shirirtoi.AutoMapper;
using AutoMapper;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Context;
using Library.ViewModels;
using Library.EFModels;
using Repository.Repositories;
using Library.Helpers;
using System.IO;

namespace DBSeed
{
    class Program
    {
        private static GameVM Game;
        private static UserProfileVM UserProfile;
        private static WordBankVM WordBank;
        private static List<RoleVM> Roles = new List<RoleVM>();
        public static HashHelper hashHelper = new HashHelper();
        private static List<Dictionary> DictionaryList = new List<Dictionary>();

        static void Main(string[] args)
        {
            Console.WriteLine("Starting DB Creation.");
            //AutoMapperConfiguration.ConfigMaps();
            //Console.WriteLine("Maps Configured.");
            //InitProp();
            //Console.WriteLine("Props Initialized.");
            //Seed();
            //Console.WriteLine("Seeding Completed.");
            //CreateDatabase();
            //Console.WriteLine("Database Created.");
            Console.ReadLine();
        }

        static void InitProp()
        {
            Game = new GameVM()
            {
                Active = true,
                GameName = "Starter",
                LastLetter = "a",
                PlayerTurn = 1,
                PlayerOne = 1
            };
            UserProfile = new UserProfileVM()
            {
                RoleID = 1,
                Email = "Administrator@Admin.com",
                Password = hashHelper.GetHash("858XqV6"),
                Username = "Administrator",
            };
            WordBank = new WordBankVM()
            {
                GameID = 1,
                Words = "alpha"
            };
            string[] roleNames = new string[3] { "Administrator", "PowerUser", "User" };
            string[] roleDesc = new string[3] { "Full Control", "Can Create Games", "Standard User" };
            for (int i = 0; i < 3; i++)
            {
                var role = new RoleVM()
                {
                    RoleName = roleNames[i],
                    RoleDesc = roleDesc[i]
                };
                Roles.Add(role);
            }
            using (StreamReader reader = new StreamReader("C:\\Users\\Onshore\\Documents\\NotePad++\\AllEnglishWords.txt"))
            {
                string word;
                while ((word = reader.ReadLine()) != null)
                {
                    Dictionary dictionary = new Dictionary()
                    {
                        Word = word.ToLower()
                    };
                    DictionaryList.Add(dictionary);
                }
            }
        }

        static void Seed()
        {
            var GameRepo = new GameRepository();
            var UserRepo = new UserProfileRepository();
            var WordRepo = new WordBankRepository();
            var RoleRepo = new RoleRepository();
            var DicRepo = new DictionaryRepository();
            DicRepo.AddDictionary(DictionaryList.OrderBy(d=>d.Word).Distinct().ToList());
            foreach (var item in Roles)
            {
                RoleRepo.AddRole(item);
            }
            UserRepo.AddUserProfile(UserProfile);
            Console.WriteLine("User Added");
            GameRepo.AddGame(Game);
            Console.WriteLine("Game Added");
            WordRepo.AddWordBank(WordBank);
            Console.WriteLine("Word Added");

        }

        static void CreateDatabase()
        {
            RepositoryContext context = new RepositoryContext("Shiritori");
            context.Database.Delete();
            context.Database.Create();
        }
    }
}
