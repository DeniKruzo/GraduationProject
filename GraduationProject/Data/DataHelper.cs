using GraduationProject.Abstract;
using GraduationProject.Data.Domains;
using GraduationProject.Domains;
using GraduationProject.mocks;
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Data
{
    public class DataHelper
    {
        private RoleManager<IdentityRole> _roleManager;
        public DataHelper(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        /// <summary>
        /// Заполняем бд из локального репозитория (тестовы задания)
        /// </summary>
        public static void Seed(GraduationDbContext context)
        {
            if (!context.CategoryOrder.Any())
                context.CategoryOrder.AddRange(CategoriesDict.Select(c => c.Value));

            if (!context.Specialization.Any())
                context.Specialization.AddRange(SpecializationDict.Select(c => c.Value));

            context.SaveChanges();
        }

        //Категории заказов
        private static Dictionary<string, CategoryOrder> category;
        public static Dictionary<string,CategoryOrder> CategoriesDict
        {
            get
            {
                if(category == null)
                {
                    var list = new CategoryOrder[]
                    {
                         new CategoryOrder { Name = "IT-среда", Description = "IT профессии"},
                         new CategoryOrder { Name = "Финансы", Description = "Работа с валютой"}
                    };

                    category = new Dictionary<string, CategoryOrder>();
                    foreach (CategoryOrder c in list)
                        category.Add(c.Name, c);
                }
                return category;
            }
        }

        //Специализации анкет
        private static Dictionary<string, Specialization> specializations;
        public static Dictionary<string, Specialization> SpecializationDict
        {
            get
            {
                if (specializations == null)
                {
                    var list = new Specialization[]
                    {
                         new Specialization { Name = "Backend-программист"},
                         new Specialization { Name = "WEB-Дизайнер"},
                         new Specialization { Name = "Бухгалтер"}
                    };

                    specializations = new Dictionary<string, Specialization>();
                    foreach (Specialization c in list)
                        specializations.Add(c.Name, c);
                }
                return specializations;
            }
        }
    }
}