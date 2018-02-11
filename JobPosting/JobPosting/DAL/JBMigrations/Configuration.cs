namespace JobPosting.DAL.JBMigrations
{
    using JobPosting.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<JobPosting.DAL.JBEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\JBMigrations";
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (Exception e)
            {
                throw new Exception(
                     "Seed Failed - errors follow:\n" +
                     e.InnerException.InnerException.Message.ToString(), e
                 ); // Add the original exception as the innerException
            }
        }

        protected override void Seed(JobPosting.DAL.JBEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var provinces = new List<Province>
            {
                new Province { province = "Ontario"},
                new Province { province = "Qubec"}
            };
            provinces.ForEach(a => context.Provinces.AddOrUpdate(n => n.province, a));
            SaveChanges(context);

            var unions = new List<Union>
                 {
                     new Union { UnionName = "Opseu 270" },
                     new Union { UnionName = "Opseu 250" },
                     new Union { UnionName = "Opseu 260" },
                     new Union { UnionName = "Opseu 290" },
                     new Union { UnionName = "Opseu 271" }
                 };
            unions.ForEach(a => context.Unions.AddOrUpdate(n => n.UnionName, a));
            SaveChanges(context);


            var jobGroups = new List<JobGroup>
                 {
                        new JobGroup {  GroupTitle = "Maintenance" },
                        new JobGroup {  GroupTitle = "Technical Support" },
                        new JobGroup {  GroupTitle = "Teacher" }
                 };
            jobGroups.ForEach(a => context.JobGroups.AddOrUpdate(n => n.GroupTitle, a));
            SaveChanges(context);

            var userRoles = new List<UserRole>
            {
                new UserRole { RoleTitle = "Hiring Team" },
                new UserRole { RoleTitle = "Admin" },
                new UserRole { RoleTitle = "Manager" },
                new UserRole { RoleTitle = "User" }
            };
            userRoles.ForEach(a => context.UserRoles.AddOrUpdate(n => n.RoleTitle, a));
            SaveChanges(context);

            var qualifications = new List<Qualification>
                        {
                            new Qualification { QlfDescription = "Hard Working"},
                            new Qualification { QlfDescription = "Punctual"},
                            new Qualification { QlfDescription = "French Language"},
                            new Qualification { QlfDescription = "Self Mostivated"},
                            new Qualification { QlfDescription = "High Initiative"},
                            new Qualification { QlfDescription = "positive Attitude"}
                        };
            qualifications.ForEach(a => context.Qualification.AddOrUpdate(n => n.QlfDescription, a));
            SaveChanges(context);


            var cities = new List<City>
            {
                new City { city = "St.Catharines", provinceID=1 },
                new City { city = "Wellend", provinceID=1},
                new City { city = "Fort Erie", provinceID=1},
                new City { city = "Niagara Falls", provinceID=1}

            };
            cities.ForEach(a => context.Cities.AddOrUpdate(n => n.city, a));
            SaveChanges(context);



            var applicants = new List<Applicant>
            {
                new Applicant { apFirstName = "Kevin", apMiddleName = "m", apLastName = "Marty", apPhone = 9056677777, apPostalCode = "L2S5G6", apEMail = "testuser@hotmail.com", apAddress = "13 fake lane" , apSubscripted = false,  cityID = (context.Cities.Where(p=>p.city == "Wellend").SingleOrDefault().ID), UserRoleID = (context.UserRoles.Where(p=>p.RoleTitle == "User").SingleOrDefault().ID) },
                new Applicant { apFirstName = "Bob", apMiddleName = "m", apLastName = "Doom", apPhone = 9056675555, apPostalCode = "L2S5G7", apEMail = "testuser1@hotmail.com", apAddress = "13 fake lane" , apSubscripted = false,  cityID = (context.Cities.Where(p=>p.city == "St.Catharines").SingleOrDefault().ID), UserRoleID = (context.UserRoles.Where(p=>p.RoleTitle == "User").SingleOrDefault().ID) },
                new Applicant { apFirstName = "Robert", apMiddleName = "D", apLastName = "McKnight", apPhone = 9056676666, apPostalCode = "L2S5G8", apEMail = "testuser2@hotmail.com", apAddress = "13 placeholder street" , apSubscripted = false, cityID = (context.Cities.Where(p=>p.city == "St.Catharines").SingleOrDefault().ID), UserRoleID = (context.UserRoles.Where(p=>p.RoleTitle == "User").SingleOrDefault().ID) }

            };
            applicants.ForEach(a => context.Applicants.AddOrUpdate(n => n.apEMail, a));
            SaveChanges(context);


            //var days = new HashSet<DayOfWeek>
            //{
            //    DayOfWeek.Monday,
            //    DayOfWeek.Friday,
            //    DayOfWeek.Tuesday,
            //    DayOfWeek.Wednesday
            //};

            var positions = new List<Position>
                        {
                            new Position { UnionID = (context.Unions.Where(p=>p.UnionName == "Opseu 250").SingleOrDefault().ID),  JobGroupID=(context.JobGroups.Where(p=>p.GroupTitle == "Teacher").SingleOrDefault().ID),
                             PositionSalary = 10000, PositionDescription = "basically this is a english teaching job", PositionFTE = 1, PositionCode = "10330",
                             PositionCompensationType = 1, Days = new List<Day>
                                                                {
                                                                    new Day { dayName = "Monday"},
                                                                    new Day { dayName = "Tuesday"},
                                                                    new Day { dayName = "Wednessday"},
                                                                    new Day { dayName = "Thrusday"},
                                                                    new Day { dayName = "Friday"},
                                                                }
                        },

                            new Position { UnionID = (context.Unions.Where(p=>p.UnionName == "Opseu 270").SingleOrDefault().ID),  JobGroupID=(context.JobGroups.Where(p=>p.GroupTitle == "Technical Support").SingleOrDefault().ID),
                             PositionSalary = 15000, PositionDescription = "basically this is a IT guy job", PositionFTE = 1, PositionCode = "10331",
                             PositionCompensationType = 1, Days = new List<Day>
                                                                {
                                                                    new Day { dayName = "Saturday"},
                                                                    new Day { dayName = "Sunday"}
                                                                }
                        }
                        };
            positions.ForEach(a => context.Positions.AddOrUpdate(n => n.PositionCode, a));
            SaveChanges(context);

            var postings = new List<Posting>
                        {
                             new Posting { pstNumPosition = 1, pstEndDate = DateTime.Parse("2019-11-15")
                           , pstJobDescription = "this job will take all the skills of teaching and more as the Vice Principle you are required to" +
                            "look into many different fields of the school...", PositionID=1}
                        };
            postings.ForEach(a => context.Postings.AddOrUpdate(n => n.ID, a));
            SaveChanges(context);

            var applications = new List<Application>
                                    {
                                       new Application { PostingID = 1,  ApplicantID=1, Priority = 2},
                                       new Application {  PostingID = 1,  ApplicantID=2, Priority = 1},
                                       new Application {  PostingID = 1,  ApplicantID=3, Priority = 3}

                                    };
            applications.ForEach(a => context.Applications.AddOrUpdate(n => new { n.ApplicantID, n.PostingID }, a));
            SaveChanges(context);




            var interviews = new List<Interview>
                               {
                                   new Interview { interviewDate =  DateTime.Parse("2018-12-15"), Accepted = true, InterviewID = 1}

                               };
            interviews.ForEach(a => context.Interviews.AddOrUpdate(n => n.InterviewID, a));
            SaveChanges(context);

            var Locations = new List<Location>
                        {
                            new Location {Street = "13 fake lane",  CityID = (context.Cities.Where(p=>p.city == "St.Catharines").SingleOrDefault().ID) },
                            new Location {Street = "13 placeholder street",  CityID = (context.Cities.Where(p=>p.city == "St.Catharines").SingleOrDefault().ID) }
                        };
            Locations.ForEach(a => context.Locations.AddOrUpdate(n => n.ID, a));
            SaveChanges(context);


            var jobRequirements = new List<JobRequirement>
                        {
                            new JobRequirement { PositionID = (context.Positions.Where(p=>p.PositionCode == "10330").SingleOrDefault().ID), QualificationID = (context.Qualification.Where(p=>p.QlfDescription == "Hard Working").SingleOrDefault().ID)  },
                            new JobRequirement { PositionID = (context.Positions.Where(p=>p.PositionCode == "10330").SingleOrDefault().ID), QualificationID = (context.Qualification.Where(p=>p.QlfDescription == "Punctual").SingleOrDefault().ID)  },
                            new JobRequirement { PositionID = (context.Positions.Where(p=>p.PositionCode == "10330").SingleOrDefault().ID), QualificationID = (context.Qualification.Where(p=>p.QlfDescription == "French Language").SingleOrDefault().ID)  },
                            new JobRequirement { PositionID = (context.Positions.Where(p=>p.PositionCode == "10331").SingleOrDefault().ID), QualificationID = (context.Qualification.Where(p=>p.QlfDescription == "Hard Working").SingleOrDefault().ID)  },

                        };
            jobRequirements.ForEach(a => context.JobRequirements.AddOrUpdate(n => new { n.PositionID, n.QualificationID }, a));
            SaveChanges(context);

            var jobLocations = new List<JobLocation>
                        {
                            new JobLocation { PositionID = (context.Positions.Where(p=>p.PositionCode == "10330").SingleOrDefault().ID), LocationID = 1},
                            new JobLocation { PositionID = (context.Positions.Where(p=>p.PositionCode == "10330").SingleOrDefault().ID), LocationID = 2},
                            new JobLocation { PositionID = (context.Positions.Where(p=>p.PositionCode == "10331").SingleOrDefault().ID), LocationID = 2}
                        };
            jobLocations.ForEach(a => context.JobLocations.AddOrUpdate(n => new { n.PositionID, n.LocationID }, a));
            SaveChanges(context);



            var applicationQualifications = new List<ApplicationQualification>
                   {
                       new ApplicationQualification { ApplicationID = (context.Applications.Where(p=>p.ID == 1).SingleOrDefault().ID), QualificationID = (context.Qualification.Where(p=>p.QlfDescription == "Hard Working").SingleOrDefault().ID)}
                   };
            applicationQualifications.ForEach(a => context.ApplicationQualification.AddOrUpdate(n => new { n.ApplicationID, n.QualificationID }, a));
            SaveChanges(context);


            var interviewCart = new List<InterviewCart>
                        {
                               new InterviewCart {  ApplicationID = 1, InterviewDate = DateTime.Parse("2018-11-15")}
                        };
            interviewCart.ForEach(a => context.InterviewCarts.AddOrUpdate(n => n.ApplicationID, a));
            SaveChanges(context);

            var applicationsCart = new List<ApplicationCart>
                        {
                            new ApplicationCart { PostingID = 1,  ApplicantID=(context.Applicants.Where(p=>p.apEMail == "testuser@hotmail.com").SingleOrDefault().ID), Priority = 2},

                        };
            applicationsCart.ForEach(a => context.ApplicationCarts.AddOrUpdate(n => n.ID, a));
            SaveChanges(context);
        }
    }
}