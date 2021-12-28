// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorRepository.cs" company="Bridgelabz">
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
    /// CollaboratorRepository Class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ICollaboratorRepository" />
    public class CollaboratorRepository : ICollaboratorRepository
    {
        /// <summary>
        /// The user context
        /// </summary>
        private readonly UserContext userContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRepository"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public CollaboratorRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>return string after adding collaborator</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<CollaboratorModel> AddCollaborator(CollaboratorModel collaborator)
        {
            try
            {
                if (collaborator.NotesModel.RegisterModel.Email != collaborator.ColEmail)
                {
                    await this.userContext.Collaborator.AddAsync(collaborator);
                    await this.userContext.SaveChangesAsync();
                    return collaborator;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <returns>return string after removing collaborator</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<CollaboratorModel> RemoveCollaborator(int collaborator)
        {
            try
            {
                var colPresent = this.userContext.Collaborator.Where(x => x.ColId == collaborator).SingleOrDefault();
                if (colPresent != null)
                {
                    this.userContext.Collaborator.Remove(colPresent);
                    await this.userContext.SaveChangesAsync();
                    return colPresent;
                }

                return null;

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
        /// <returns>return string after get collaborator</returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<CollaboratorModel>> GetCollaborator(int notesId)
        {
            try
            {
                var collaboratorList = await this.userContext.Collaborator.Where(x => x.NotesId == notesId).ToListAsync();
                if (collaboratorList.Count() != 0)
                {
                    return collaboratorList;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
