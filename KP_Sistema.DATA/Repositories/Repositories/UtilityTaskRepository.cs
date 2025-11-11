using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Repositories
{
    public class UtilityTaskRepository : IUtilityTaskRepository
    {
        private readonly AppDbContext _dbContext;

        public UtilityTaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UtilityTask> AddUtilityTaskAsync(UtilityTask utilityTask)
        {
            //#1 Option
            //await _dbContext.UtilityTasks.AddAsync(utilityTask);
            //await _dbContext.SaveChangesAsync();

            //Option#2
            await _dbContext.Database.ExecuteSqlAsync(
                $"""
                INSERT INTO utilityTasks (name, description, price, comunityId, community)
                VALUES ({utilityTask.Name}, {utilityTask.Description}, {utilityTask.Price}, 
                {utilityTask.CommunityId}, {utilityTask.Community})
                """);

            return utilityTask;
        }

        public async Task<UtilityTask?> FindUtilityTaskByName(string name)
        {
            //Option #1
            //var utilityTask = await _dbContext.UtilityTasks.FirstOrDefaultAsync(utilityTask => utilityTask.Name.Equals(name));


            //Option #2
            var utilityTask = await _dbContext.UtilityTasks.FromSqlInterpolated(
                $"""
                SELECT * FROM utilityTasks WHERE name={name}
                """
                ).FirstOrDefaultAsync();

            return utilityTask;
        }

        public async Task<UtilityTask> EditUtilityTask(UtilityTask utilityTask)
        {
            //Option #1
            //_dbContext.UtilityTasks.Update(utilityTask);
            //await _dbContext.SaveChangesAsync();

            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $"""
                UPDATE utilityTasks SET name={utilityTask.Name}, description={utilityTask.Description},
                price={utilityTask.Price}, communityId={utilityTask.CommunityId}, community={utilityTask.Community}
                """
                );

            return utilityTask;
        }

        public async Task<UtilityTask> DeleteUtilityTask(UtilityTask utilityTask)
        {
            //Option #1
            //_dbContext.UtilityTasks.Remove(utilityTask);
            //await _dbContext.SaveChangesAsync();

            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $"""
                DELETE FROM utilityTasks WHERE id={utilityTask.Id}
                """);

            return utilityTask;
        }

        public async Task<List<UtilityTask>> GetAllUtilityTasksByCommunity(Community community)
        {
            //Option #1
            //var utilityTasks = await _dbContext.UtilityTasks.ToListAsync();

            //Option #2
            var utilityTasks = await _dbContext.UtilityTasks.FromSqlInterpolated(
                $"""
                SELECT * FROM utilityTasks WHERE communityId = {community.Id};
                """)
                .Include(UtilityTask => UtilityTask.Community)
                .ToListAsync();

            return utilityTasks;
        }
    }
}
