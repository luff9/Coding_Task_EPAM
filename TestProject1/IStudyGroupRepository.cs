
namespace TestApp
{
    public interface IStudyGroupRepository
    {
        Task<bool> CreateStudyGroup(StudyGroup studyGroup);

        Task<bool> JoinStudyGroup(int studyGroupId, int userId);

        Task<bool> LeaveStudyGroup(int studyGroupId, int userId);

        Task<List<StudyGroup>> GetStudyGroups();

        Task<List<StudyGroup>> SearchStudyGroups(string subject);
    }
}