// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorManager.cs" company="Bridgelabz">
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
    /// CollaboratorManager Class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ICollaboratorManager" />
    public class CollaboratorManager : ICollaboratorManager
    {
        /// <summary>
        /// The collaborator repository
        /// </summary>
        private readonly ICollaboratorRepository collaboratorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorManager"/> class.
        /// </summary>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            this.collaboratorRepository = collaboratorRepository;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>returns string after adding collaborator</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                return await this.collaboratorRepository.AddCollaborator(collaborator);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="colId">The col identifier.</param>
        /// <returns>returns string after deleting collaborator</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<CollaboratorModel> RemoveCollaborator(int colId)
        {
            try
            {
                return await this.collaboratorRepository.RemoveCollaborator(colId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the collaborator.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>
        /// returns string after get collaborator
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<CollaboratorModel>> GetCollaborator(int notesId)
        {
            try
            {
               return await this .collaboratorRepository.GetCollaborator(notesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
