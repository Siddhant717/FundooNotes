using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
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
    }
}
