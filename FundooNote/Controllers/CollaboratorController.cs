// --------------------------------------------------------------------------------------------------------------------
// <copyright file="collaboratorController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Somwanshi Akshay Ramchandra"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNote.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModel;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CollaboratorController Class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// The collaborator manager
        /// </summary>
        private readonly ICollaboratorManager collaboratorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ollaboratorController"/> class.
        /// </summary>
        /// <param name="collaboratorManager">The collaborator manager.</param>
        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaboratorModel">The collaborator model.</param>
        /// <returns>return IActionResult code after adding collaborator</returns>
        [HttpPost]
        [Route("api/addCollaborator")]
        public async Task<IActionResult> AddCollaborator([FromBody] CollaboratorModel collaboratorModel)
        {
            try
            {
                CollaboratorModel result = await this.collaboratorManager.AddCollaborator(collaboratorModel);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<CollaboratorModel>() { Status = true, Message = "Collaborator Added!", Data = result });
                }

                return this.BadRequest(new { Status = false, Message = "Collaborator Not Added!" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Removes the collaborator.
        /// </summary>
        /// <param name="colId">The col identifier.</param>
        /// <returns>return IActionResult code after deleting collaborator</returns>
        [HttpDelete]
        [Route("api/deleteCollaborator")]
        public async Task<IActionResult> RemoveCollaborator(int colId)
        {
            try
            {
                CollaboratorModel result = await this.collaboratorManager.RemoveCollaborator(colId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<CollaboratorModel>(){ Status = true, Message = "Removed Collaborator", Data = result});
                }

                return this.BadRequest(new { Status = false, Message = "Collaborator Not Removed " });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Gets the collaborator.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>return IActionResult status after get collaborator</returns>
        [HttpPost]
        [Route("api/getCollaborator")]
        public async Task<IActionResult> GetCollaborator(int noteId)
        {
            try
            {
                var result = await this.collaboratorManager.GetCollaborator(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel <IEnumerable<CollaboratorModel>>(){ Status = true, Message = "Retrieved Collaborator", Data = result});
                }

                return this.BadRequest(new { Status = false, Message = "Failed to retrieve" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
