// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooModel;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// LabelRepository Class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ILabelRepository" />
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// The user context
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public LabelRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns> return string after successful adding label</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<LabelModel> AddLabel(LabelModel labelModel)
        {
            try
            {
                if (labelModel.NotesModel.Title != labelModel.LabelName)
                {
                    await this.userContext.Label.AddAsync(labelModel);
                    await this.userContext.SaveChangesAsync();
                    return labelModel;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>return a string after editing label</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<LabelModel> EditLabel(LabelModel labelModel)
        {
            try
            {
                var exists = this.userContext.Label.Where(x => x.LabelId == labelModel.LabelId).SingleOrDefault();
                if (exists != null)
                {
                    this.userContext.Label.Update(exists);
                    await this.userContext.SaveChangesAsync();
                    return exists;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>return a string after deleting label</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<LabelModel> DeleteLabel(int labelId, string labelName)
        {
            try
            {
                var exists = await this.userContext.Label.Where(x => x.LabelName == labelName && x.LabelId == labelId).SingleOrDefaultAsync();
                if (exists != null)
                {
                    this.userContext.Label.RemoveRange(exists);
                    await this.userContext.SaveChangesAsync();
                    return exists;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label by notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return a get label by notes</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<LabelModel>> GetLabelByNotes(int notesId)
        {
            try
            {
                var exists = await this.userContext.Label.Where(x => x.NotesId == notesId).ToListAsync();
                if (exists.Count > 0)
                {
                    return exists;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label by user.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>return a get label by user</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<LabelModel>> GetLabelByUser(int userId)
        {
            try
            {
                var exists = await this.userContext.Label.Where(x => x.NotesId == userId).ToListAsync();
                if (exists.Count > 0)
                {
                    return exists;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}