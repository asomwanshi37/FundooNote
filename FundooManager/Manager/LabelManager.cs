// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------


namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModel;
    using FundooRepository.Interface;

    /// <summary>
    /// LabelManager Class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ILabelManager" />
    public class LabelManager :ILabelManager
    {
        /// <summary>
        /// The label repository
        /// </summary>
        private readonly ILabelRepository labelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelManager"/> class.
        /// </summary>
        /// <param name="labelRepository">The label repository.</param>
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns string after successful adding label </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<LabelModel> AddLabel(LabelModel labelModel)
        {
            try
            {
                return await this.labelRepository.AddLabel(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns a string after editing label successful</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<LabelModel> EditLabel(LabelModel labelModel)
        {
            try
            {
                return await this .labelRepository.EditLabel(labelModel);
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
        /// <returns>returns a string after deleting label.</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<LabelModel> DeleteLabel(int labelId, string labelName)
        {
            try
            {
                return await this.labelRepository.DeleteLabel(labelId, labelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label by notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>returns label by notes</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<LabelModel>> GetLabelByNotes(int notesId)
        {
            try
            {
                return await this.labelRepository.GetLabelByNotes(notesId);
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
        /// <returns>returns a label by user</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<LabelModel>> GetLabelByUser(int userId)
        {
            try
            {
                return await this.labelRepository.GetLabelByUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
