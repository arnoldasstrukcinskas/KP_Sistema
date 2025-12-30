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
            await _dbContext.UtilityTasks.AddAsync(utilityTask);
            await _dbContext.SaveChangesAsync();

            //Option#2
            //await _dbContext.Database.ExecuteSqlAsync(
            //    $"""
            //    INSERT INTO UtilityTasks (Name, Description, Price)
            //    VALUES ({utilityTask.Name}, {utilityTask.Description}, {utilityTask.Price})
            //    """);

            var createdUtilityTask = await GetUtilityTaskByName(utilityTask.Name);

            return createdUtilityTask;
        }

        public async Task<UtilityTask?> GetUtilityTaskById(int id)
        {

            var utilityTask = await _dbContext.UtilityTasks
                .Where(task => task.Id == id)
                .Include(task => task.Community)
                .FirstOrDefaultAsync();

            ////Option #2
            //var utilityTask = await _dbContext.UtilityTasks.FromSqlInterpolated(
            //    $"""
            //        SELECT * FROM UtilityTasks WHERE id={id}
            //    """)
            //    .Include(utilityTask => utilityTask.Community)
            //    .FirstOrDefaultAsync();

            return utilityTask;
        }

        public async Task<UtilityTask?> GetUtilityTaskByName(string name)
        {
            //Option #1
            //var utilityTask = await _dbContext.UtilityTasks.FirstOrDefaultAsync(utilityTask => utilityTask.Name.Equals(name));


            //Option #2
            var utilityTask = await _dbContext.UtilityTasks.FromSqlInterpolated(
                $"""
                SELECT * FROM UtilityTasks WHERE name={name}
                """
                ).Include(utilityTask => utilityTask.Community)
                .FirstOrDefaultAsync();

            return utilityTask;
        }

        public async Task<List<UtilityTask>> GetUtilityTasksByName(string name)
        {
            var utilityTasks = await _dbContext.UtilityTasks
                .Where(task => task.Name.Contains(name))
                .Include(task => task.Community)
                .ToListAsync();

            return utilityTasks;
        }

        public async Task<UtilityTask> EditUtilityTask(UtilityTask utilityTask)
        {
            //Option #1
            _dbContext.UtilityTasks.Update(utilityTask);
            await _dbContext.SaveChangesAsync();

            ////Option #2
            //await _dbContext.Database.ExecuteSqlAsync(
            //    $"""
            //    UPDATE UtilityTasks SET name={utilityTask.Name}, description={utilityTask.Description},
            //    price={utilityTask.Price}, communityId={utilityTask.CommunityId} 
            //    WHERE id={utilityTask.Id}
            //    """
            //    );

            var editedUtilityTask = await GetUtilityTaskById(utilityTask.Id);

            return editedUtilityTask;
        }

        public async Task<UtilityTask> DeleteUtilityTask(UtilityTask utilityTask)
        {
            //Option #1
            //_dbContext.UtilityTasks.Remove(utilityTask);
            //await _dbContext.SaveChangesAsync();

            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $"""
                DELETE FROM UtilityTasks WHERE id={utilityTask.Id}
                """);

            return utilityTask;
        }

        public async Task<List<UtilityTask>> GetAllUtilityTasks()
        {
            //Option #1
            var tasks = await _dbContext.UtilityTasks.ToListAsync();

            ////Option #2
            //var tasks = await _dbContext.UtilityTasks.FromSqlInterpolated(
            //    $"SELECT * FROM UtilityTasks").ToListAsync();

            return tasks;
        }
    }
}
