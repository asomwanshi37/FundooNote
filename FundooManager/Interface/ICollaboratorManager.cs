// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------

namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModel;

    /// <summary>
    /// ICollaboratorManager Interface.
    /// </summary>
    public interface ICollaboratorManager
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns></returns>
        Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaborator);

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="colId">The col identifier.</param>
        /// <returns></returns>
        Task<CollaboratorModel> RemoveCollaborator(int colId);

        /// <summary>
        /// Gets the collaborator.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<CollaboratorModel>>GetCollaborator(int notesId);
    }
}
