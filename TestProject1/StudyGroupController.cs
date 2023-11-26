﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class StudyGroupController
    {
        private readonly IStudyGroupRepository _studyGroupRepository;

        public StudyGroupController(IStudyGroupRepository studyGroupRepository)
        {
            _studyGroupRepository = studyGroupRepository;
        }

        public async Task<IActionResult> CreateStudyGroup(StudyGroup studyGroup)
        {
            await _studyGroupRepository.CreateStudyGroup(studyGroup);
            return new OkResult();
        }

        public async Task<IActionResult> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();
            return new OkObjectResult(studyGroups);
        }

        public async Task<IActionResult> SearchStudyGroups(string subject)
        {
            var studyGroups = await _studyGroupRepository.SearchStudyGroups(subject);
            return new OkObjectResult(studyGroups);
        }

        public async Task<IActionResult> JoinStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

    }

}
