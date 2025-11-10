using KP_Sistema.DATA.Entities;
using KP_Sistema.DATA.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.DATA.Repositories.Repositories
{
    internal class CommunityRepository : ICommunityRepository
    {
        private readonly AppDbContext _dbContext;

        public CommunityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Community> AddCommunityAsync(Community community)
        {
            //Option #1
            //await _dbContext.Communities.AddAsync(community);
            //await _dbContext.SaveChangesAsync();

            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $"""
                INSERT INTO Communities (Name, UtilityTasks, Users) 
                VALUES({community.Name}, {community.UtilityTasks}, {community.Users})
                """);
            
              return community;
        }

        public async Task<Community> DeleteCommunityAsync(Community community)
        {
            //Option #1
            //_dbContext.Communities.Remove(community);
            //await _dbContext.SaveChangesAsync();

            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $"DELETE FROM Communities WHERE id={community.Id}");

            return community;
        }

        public async Task<Community> EditCommunityAsync(Community community)
        {
            //Option #1
            //_dbContext.Communities.Update(community);
            //await _dbContext.SaveChangesAsync();

            //Option #2
            await _dbContext.Database.ExecuteSqlAsync(
                $"""
                UPDATE Communities SET name={community.Name}, utilityTasks={community.UtilityTasks}, users={community.Users} WHERE id={community.Id}
                """);
                
            return community;
        }

        public async Task<Community?> FindCommunityByName(string name)
        {
            //Option #1
            //var community = _dbContext.Communities
            //    .FirstOrDefaultAsync(community => community.Name.Equals(name));

            //Option #2
            var community = await _dbContext.Communities.FromSqlInterpolated(
                $"SELECT * FROM Communities WHERE name={name}")
                .Include(community => community.Users)
                .FirstOrDefaultAsync();

            return community;

        }

        public async Task<List<Community>?> GetAllCommunities()
        {
            //Option #1
            //var communities = _dbContext.Communities.ToListAsync();

            //Option #2
            var communities = await _dbContext.Communities.FromSqlInterpolated(
                $"SELECT * FROM Communities").ToListAsync();

            return communities;
        }
    }
}
