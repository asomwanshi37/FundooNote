// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
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
    /// LabelController Class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// The label manager
        /// </summary>
        private readonly ILabelManager labelManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelManager">The label manager.</param>
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>return IActionResult Status after successful adding label</returns>
        [HttpPost]
        [Route("api/addLabel")]
        public async Task<IActionResult> AddLabel([FromBody] LabelModel label)
        {
            try
            {
                LabelModel result = await this.labelManager.AddLabel(label);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel>() { Status = true, Message="Label Added Sucessful" ,Data= result });
                }

                return this.BadRequest(new { Status = false, Message = "Label Added Failed " });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>return IActionResult Status after successful editing label</returns>
        [HttpPut]
        [Route("api/editLabel")]
        public async Task<IActionResult> EditLabel([FromBody] LabelModel labelModel,int LabelId)
        {
            try
            {
                LabelModel result = await this.labelManager.EditLabel(labelModel,LabelId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<LabelModel>() { Status = true, Message = "Label Edit Sucessful",Data = result });
                }

                return this.BadRequest(new { Status = false, Message ="Please Check Label Not Present"});
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>return IActionResult Status after successful deleting label</returns>
        [HttpDelete]
        [Route("api/DeleteLabel")]
        public async Task<IActionResult> DeleteLabel(int labelId, string labelName)
        {
            try
            {
                LabelModel valid = await this.labelManager.DeleteLabel(labelId, labelName);
                if (valid != null)
                {
                    return this.Ok(new ResponseModel<LabelModel>() { Status = true, Message = "Label Delete",Data = valid });
                }

                return this.BadRequest(new { Status = false, Message = "Label Not Present" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false,ex.Message });
            }
        }

        /// <summary>
        /// Gets the label by notes identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <returns>return IActionResult Status after successful get label bynotes</returns>
        [HttpPost]
        [Route("api/GetLabelByNotes")]
        public async Task<IActionResult> GetLabelByNotesId(int notesId)
        {
            try
            {
                var result = await this.labelManager.GetLabelByNotes(notesId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<LabelModel>>() { Status = true, Message = "Retrieved Label", Data = result });
                }

                return this.BadRequest(new { Status = false, Message = "Retrieved Label Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }

        /// <summary>
        /// Gets the label by user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>return IActionResult Status after successful get label byuser</returns>
        [HttpPost]
        [Route("api/GetLabelByUser")]
        public async Task<IActionResult> GetLabelByUserId(int userId)
        {
            try
            {
                var result = await this.labelManager.GetLabelByUser(userId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<LabelModel>>() { Status = true, Message = "Retrieved Label", Data = result });
                }

                return this.BadRequest(new { Status = false, Message = "Retrieved Label Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false,ex.Message });
            }
        }
    }
}
