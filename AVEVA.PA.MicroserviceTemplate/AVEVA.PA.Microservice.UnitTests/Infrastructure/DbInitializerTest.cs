using System;
using System.Linq;
using System.Collections.Generic;
using AVEVA.PA.MicroserviceTemplate.Infrastructure.Data;
using AVEVA.PA.DataAccess;
using AVEVA.PA.DataAccess.Models;

namespace Microservice.UnitTests.Infrastructure
{
    public class DbInitializerTest
    {
        public static void Initialize(PaDbContext context)
        {
            if (context.Projects.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(PaDbContext context)
        {

            #region ProjectData            

            var project = new Project
            {
                ProjectId = 1005,
                AssetId = 1003,
                Name = "Mock Project",
                Description = "Mock desc",
                Statuslastupdate = DateTime.UtcNow,
                FilePath = "test",
                LastDeployUser = "testuser",
                Lockeduser = "testuser",
                Lastmodifieduser = "testuser",
                Statusid = 123,
                Archivertsid = 123,
                Projecttypeid = 123,
                Aliasrtsid = 123,
                ParentTemplateId = 123,
                Rating = 123,
                Createdusername = "testname",
                TransientInterval = 123,
                Criticality = 123,
                InstanceId = 123,
                SignalMinPercentValidValue = 123,
                SignalMinPercentValidSource = 1234,
                AutoFilterEnableValue = 123456,
                AutoFilterEnableSource = 456123,
                PollingIntervalSource = 12345,
                CriticalitySource = 789,
                ProjectGuid = "testguid",
                ProcessingMode = 12345
            };

            var project1 = new Project
            {
                ProjectId = 1006,
                AssetId = 1003,
                Name = "Mock Project2",
                Description = "Mock desc2",
                Statuslastupdate = DateTime.UtcNow,
                FilePath = "test",
                LastDeployUser = "testuser",
                Lockeduser = "testuser",
                Lastmodifieduser = "testuser",
                Statusid = 123,
                Archivertsid = 123,
                Projecttypeid = 123,
                Aliasrtsid = 123,
                ParentTemplateId = 123,
                Rating = 123,
                Createdusername = "testname",
                TransientInterval = 123,
                Criticality = 123,
                InstanceId = 123,
                SignalMinPercentValidValue = 123,
                SignalMinPercentValidSource = 1234,
                AutoFilterEnableValue = 123456,
                AutoFilterEnableSource = 456123,
                PollingIntervalSource = 12345,
                CriticalitySource = 789,
                ProjectGuid = "testguid",
                ProcessingMode = 12345
            };


            var projects = new List<Project>
            {
                project,
                project1
            };

            #endregion

            context.Projects.AddRange(projects);
            context.SaveChanges();
        }

    }
}
