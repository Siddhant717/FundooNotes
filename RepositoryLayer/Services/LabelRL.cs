using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{


    public class LabelRL : ILabelRL
    {
        readonly FundooContext fundooContext;
        private IConfiguration _config;
        public LabelRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this._config = config;
        }

        public async Task AddLabel(int userId, int NoteId, string labelName)
        {
            try
            {
                Label label = new Label();

                label.userId = userId;
                label.NoteId = NoteId;
                label.LabelName = labelName;
                fundooContext.Labels.Add(label);
               await fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Label> GetLabelByNoteId(int userId, int NoteId)
        {
            try
            {
                var user = fundooContext.Users.Where(x => x.userId == userId).FirstOrDefault();
                var note = fundooContext.Notes.Where(x => x.NoteId == NoteId && x.userId == userId).FirstOrDefault();
                var label = fundooContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefault();

                if (label == null)
                {
                    return null;
                }

                return await fundooContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            }



            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetLabelModel>> GetLabelByNoteIdwithJoin(int userId, int NoteId)
        {
            try
            {
                var label = await this.fundooContext.Labels.FirstOrDefaultAsync(x => x.userId == userId);
                var result = await (from user in fundooContext.Users
                              join notes in fundooContext.Notes on user.userId equals userId //where notes.NoteId == NoteId
                              join labels in fundooContext.Labels on notes.NoteId equals labels.NoteId
                              where labels.NoteId == NoteId && labels.userId == userId
                              select new GetLabelModel
                              {

                                  userId = userId,
                                  NoteId = notes.NoteId,
                                  Title = notes.Title,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  EmailId = user.EmailId,
                                  Description = notes.Description,
                                  Color = notes.Color,
                                  LabelName = labels.LabelName,
                                  CreatedDate = labels.user.CreatedDate
                              }).ToListAsync();
                return  result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetLabelModel>> GetLabelByUserIdWithJoin(int userId)
        {
            try
            {
                var label = this.fundooContext.Labels.FirstOrDefaultAsync(x => x.userId == userId);
                var result =await (from user in fundooContext.Users
                              join notes in fundooContext.Notes on user.userId equals userId //where notes.NoteId == NoteId
                              join labels in fundooContext.Labels on notes.NoteId equals labels.NoteId
                              where labels.userId == userId
                              select new GetLabelModel
                              {

                                  userId = userId,
                                  NoteId = notes.NoteId,
                                  Title = notes.Title,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  EmailId = user.EmailId,
                                  Description = notes.Description,
                                  Color = notes.Color,
                                  LabelName = labels.LabelName,
                                  CreatedDate = labels.user.CreatedDate
                              }).ToListAsync();
                return  result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
