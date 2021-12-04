using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnicdaPlatform.Controllers.CareerSubject;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;

namespace UnicdaPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMatterDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiMatterDataController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        // POST: api/ApiMatterData
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ApiMatterData>> PostApiMatterData(ApiMatterData apiMatterData)
        {
            if (apiMatterData != null)
            {
                if (!string.IsNullOrEmpty(apiMatterData.StudentId))
                {

                    UserMatterController _userMatterController = new UserMatterController();
                    var user = _context.User.First(a => a.UserId == apiMatterData.StudentId);

                    var userMatter = _userMatterController.GetAvailableMatterToRemove(_context, user.UserId);

                    var uMatter = userMatter.First(a => a.MatterId == apiMatterData.MatterId && a.PeriodCycle == apiMatterData.PeriodCycle &&
                                                a.PeriodYear == apiMatterData.PeriodYear && a.SessionCode == apiMatterData.SessionCode);
                    if (uMatter != null) 
                    {
                        var data = _userMatterController.GetMatterInProgress(_context, user.UserId, 1).First(a => a.MatterId == uMatter.MatterId);

                        var request =_context.RequestUserMatter.First(a => a.CareerPensumId == data.CareerPensumId && a.UserId == user.MasterId);

                        request.UserResponseId = _context.User.First(a => a.UserId == apiMatterData.UserResponseId).MasterId;
                        request.ResponseComment = apiMatterData.UserResponseMsg;
                        request.Status = apiMatterData.IsApproved ? 3 : 2;

                        _context.RequestUserMatter.Update(request);
                        try
                        {
                            await _context.SaveChangesAsync();

                            var careerUserPensum = _context.CareerUserPensum.First(a => a.Id == data.CareerPensumId && a.UserId == user.MasterId);

                            careerUserPensum.Status = apiMatterData.IsApproved ? 3 : 2;
                            _context.CareerUserPensum.Update(careerUserPensum);

                            var careerUserPensumDetails = _context.CareerUserPensumDetails.First(a => a.CareerUserPensumId == careerUserPensum.Id);

                            careerUserPensumDetails.Status = apiMatterData.IsApproved ? 3 : 2;
                            _context.CareerUserPensumDetails.Update(careerUserPensumDetails);

                            await _context.SaveChangesAsync();

                        }
                        catch (DbUpdateException)
                        {
                            throw;
                        }
                    }                   
                }
            }


            return CreatedAtAction("GetApiMatterData", new { id = apiMatterData.StudentId }, apiMatterData);
        }
    }
}
