// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooRepository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModel;


    /// <summary>
    /// ILabelRepository Interface
    /// </summary>
    public interface ILabelRepository
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>return string after successful adding label</returns>
        Task<LabelModel> AddLabel(LabelModel labelModel);

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>return a string after editing label</returns>
        Task<LabelModel> EditLabel(LabelModel labelModel);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>return a string after deleting label</returns>
        Task<LabelModel> DeleteLabel(int labelId, string labelName);

        /// <summary>
        /// Gets the label by notes.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return a get label by notes</returns>
        Task<IEnumerable<LabelModel>> GetLabelByNotes(int notesId);

        /// <summary>
        /// Gets the label by user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return a get label by user</returns>
        Task<IEnumerable<LabelModel>> GetLabelByUser(int userId);
    }
}
