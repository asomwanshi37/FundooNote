// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------


namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModel;

    /// <summary>
    /// ILabelManager Interface
    /// </summary>
    public interface ILabelManager
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns string after successfully adding label</returns>
        Task<LabelModel> AddLabel(LabelModel labelModel);

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>returns string after successfully editing label</returns>
        Task<LabelModel> EditLabel(LabelModel labelModel);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>returns string after successfully deleting label</returns>
        Task<LabelModel> DeleteLabel(int labelId, string labelName);

        /// <summary>
        /// Gets the label by notes.
        /// </summary>
        /// <param name="NotesId">The notes identifier.</param>
        /// <returns>returns a result label by notes</returns>
        Task<IEnumerable<LabelModel>> GetLabelByNotes(int notesId);

        /// <summary>
        /// Gets the label by user.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>returns a result label by user</returns>
        Task<IEnumerable<LabelModel>> GetLabelByUser(int userId);
    }
}
