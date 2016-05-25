using System.Collections.Generic;
using LeaveSystem.Web.BLL;
using LeaveSystem.Web.DAL;
using LeaveSystem.Web.Infrastructure;
using LeaveSystem.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace LeaveSystem.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            //��ʼ��
            var userManager = new UserManager(new UserStore(context));
            var roleManager = new RoleManager(new RoleStore(context));
            var departmentManaegr = new DepartmentManager(new DepartmentStore(context));

            string number = "000000";
            string name = "����Ա";
            string password = "123456";

            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new Role("Administrator") { Description = "����ǹ���Ա" });
            }
            if (!roleManager.RoleExists("Teacher"))
            {
                roleManager.Create(new Role("Teacher") { Description = "����ǽ�ʦ" });
            }
            if (!roleManager.RoleExists("Student"))
            {
                roleManager.Create(new Role("Student") { Description = "�����ѧ��" });
            }

            var user = userManager.FindByName(number);
            if (user == null)
            {
                userManager.Create(new User { UserName = number, Name = name }, password);
                user = userManager.FindByName(number);
            }
            if (!userManager.IsInRole(user.Id, "Administrator"))
            {
                userManager.AddToRole(user.Id, "Administrator");
            }

            var userList1 = new[]
            {
                new  User("851900"){Name = "��ʦ00"}, 
                new  User("851901"){Name = "��ʦ01"}, 
                new  User("851902"){Name = "��ʦ02"}, 
                new  User("851903"){Name = "��ʦ03"}, 
                new  User("851904"){Name = "��ʦ04"}, 
                new  User("851905"){Name = "��ʦ05"}, 
                new  User("851906"){Name = "��ʦ06"}, 
                new  User("851907"){Name = "��ʦ07"}, 
                new  User("851908"){Name = "��ʦ08"}, 
                new  User("851909"){Name = "��ʦ09"}, 
            };
            var userList2 = new[]
            {
                new  User("201210409120"){Name = "ѧ��00"}, 
                new  User("201210409121"){Name = "ѧ��01"}, 
                new  User("201210409122"){Name = "ѧ��02"}, 
                new  User("201210409123"){Name = "ѧ��03"}, 
                new  User("201210409124"){Name = "ѧ��04"}, 
                new  User("201210409125"){Name = "ѧ��05"}, 
                new  User("201210409126"){Name = "ѧ��06"}, 
                new  User("201210409127"){Name = "ѧ��07"}, 
                new  User("201210409128"){Name = "ѧ��08"}, 
                new  User("201210409129"){Name = "ѧ��09"}, 
            };

            foreach (var user1 in userList1)
            {
                var tu = userManager.FindByName(user1.UserName);
                if (tu == null)
                {
                    userManager.Create(new User { UserName = user1.UserName, Name = user1.Name }, password);
                    tu = userManager.FindByName(user1.UserName);
                }
                if (!userManager.IsInRole(tu.Id, "Teacher"))
                {
                    userManager.AddToRole(tu.Id, "Teacher");
                }
            }

            foreach (var user1 in userList2)
            {
                var tu = userManager.FindByName(user1.UserName);
                if (tu == null)
                {
                    userManager.Create(new User { UserName = user1.UserName, Name = user1.Name }, password);
                    tu = userManager.FindByName(user1.UserName);
                }
                if (!userManager.IsInRole(tu.Id, "Student"))
                {
                    userManager.AddToRole(tu.Id, "Student");
                }
            }

            new[]
            {
                new Department{Name = "��Ϣ��ѧ�빤��ѧԺ"}, 
                new Department{Name = "ҩѧ�����﹤��ѧԺ"}, 
                new Department{Name = "������Ӱ��ѧԺ"}, 
                new Department{Name = "ҽѧԺ������ѧԺ��"}, 
                new Department{Name = "ʦ��ѧԺ"}, 
                new Department{Name = "�����ѧԺ"}, 
                new Department{Name = "��е����ѧԺ"}, 
                new Department{Name = "��������ľ����ѧԺ"}, 
                new Department{Name = "�����뾭�ù���ѧԺ"}, 
                new Department{Name = "��ѧ�����Ŵ���ѧԺ"}, 
                new Department{Name = "����ѧԺ.��ѧϵ"}, 
                new Department{Name = "����ѧԺ"}, 
            }.ForEach(e => context.Departments.AddOrUpdate(d => d.Name, e));
            context.SaveChanges();

            new[]
            {
                new Grade{GradeNum = "2010"}, 
                new Grade{GradeNum = "2011"}, 
                new Grade{GradeNum = "2012"}, 
                new Grade{GradeNum = "2013"}, 
                new Grade{GradeNum = "2014"}, 
                new Grade{GradeNum = "2015"}, 
                new Grade{GradeNum = "2016"}, 
                new Grade{GradeNum = "2017"}, 
                new Grade{GradeNum = "2018"}, 
            }.ForEach(e => context.Grades.AddOrUpdate(d => d.GradeNum, e));
            context.SaveChanges();

            var tdepart = departmentManaegr.FindDepartmentByNameAsync("��Ϣ��ѧ�빤��ѧԺ").Result;
            new[]
            {
                new Office(){Department = tdepart,Description = "����������������",Name = "����"},
                new Office(){Department = tdepart,Description = "����������",Name = "����"},
                new Office(){Department = tdepart,Description = "ѧԺ�칫��ѧԺ�칫��",Name = "ѧԺ�칫��"},
                new Office(){Department = tdepart,Description = "����칫�ҽ���칫��",Name = "����칫��"},
                new Office(){Department = tdepart,Description = "ѧ�������칫��ѧ�������칫��",Name = "ѧ�������칫��"},
            }.ForEach(e => context.Offices.AddOrUpdate(d => d.Name, e));
            context.SaveChanges();

            new[]
            {
                new Major(){Department = tdepart,Name = "�������"},
                new Major(){Department = tdepart,Name = "����ý�弼��"},
                new Major(){Department = tdepart,Name = "�������ѧ"},
                new Major(){Department = tdepart,Name = "����������"},
                new Major(){Department = tdepart,Name = "���繤��"},
                new Major(){Department = tdepart,Name = "��Ϣ��ѧ�뼼��"},
            }.ForEach(e => context.Majors.AddOrUpdate(d => d.Name, e));
            context.SaveChanges();

            new[]
            {
                new LeaveConfig()
                {
                    Id = 1,
                    LeastDayToSign = 1,
                    LeastResumeDay = 1,
                    ResumeClassroom = "10507",
                    LeastSickLeaveDay = 2,
                    MaxLeaveDayInOneMonth = 10,
                    MaxLeaveClassInOneMonth = 20,
                    MaxLeaveTimeInOneMonth = 5
                }
            }.ForEach(e => context.LeaveConfigs.AddOrUpdate(d => d.Id, e));
            context.SaveChanges();

            new[]
            {
                new LessonInfo()
                {
                    BelongDepartment = tdepart,
                    LessonName = "�������"
                }
            }.ForEach(e => context.LessonInfos.AddOrUpdate(d => d.LessonName));



        }
    }
}
