// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorRepository.cs" company="Bridgelabz">
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
    /// ICollaboratorRepository Interface
    /// </summary>
    public interface ICollaboratorRepository
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>return string after adding collaborator</returns>
        Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaborator);

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="colId">The col identifier.</param>
        /// <returns>return string after deleting collaborator</returns>
        Task<CollaboratorModel> RemoveCollaborator(int colId);

        /// <summary>
        /// Gets the collaborator.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return string after get collaborator</returns>
        Task<IEnumerable<CollaboratorModel>> GetCollaborator(int notesId);
    }
}
