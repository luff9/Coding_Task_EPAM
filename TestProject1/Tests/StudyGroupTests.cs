using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestApp;

[TestFixture]
public class StudyGroupTests
{
    private StudyGroupController _studyGroupController;
    //ToDo Mocking will be replaced by api urls
    private Mock<IStudyGroupRepository> _mockStudyGroupRepository;

    [SetUp]
    public void Setup()
    {
        _mockStudyGroupRepository = new Mock<IStudyGroupRepository>();
        _studyGroupController = new StudyGroupController(_mockStudyGroupRepository.Object);
    }

    [Test]
    public async Task CreateStudyGroup_ValidInput_Success()
    {
        // Arrange
        var studyGroup = new StudyGroup { Name = "Physics Study Group", Subject = "Physics" };
        _mockStudyGroupRepository
            .Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>()))
            .ReturnsAsync(true);

        // Act
        var result = await _studyGroupController.CreateStudyGroup(studyGroup);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task CreateStudyGroup_DuplicateSubject_Failure()
    {
        // Arrange
        var studyGroup = new StudyGroup { Name = "Math Study Group", Subject = "Math" };
        _mockStudyGroupRepository
            .Setup(repo => repo.CreateStudyGroup(It.IsAny<StudyGroup>()))
            .ReturnsAsync(false);

        // Act
        var result = await _studyGroupController.CreateStudyGroup(studyGroup);

        // Assert
        Assert.IsNotInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task CreateStudyGroup_InvalidSubject_Failure()
    {
        // Arrange
        var studyGroup = new StudyGroup { Name = "Invalid Subject Group", Subject = "History" };

        // Act
        var result = await _studyGroupController.CreateStudyGroup(studyGroup);

        // Assert
        Assert.IsNotInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task JoinStudyGroup_ValidInput_Success()
    {
        // Arrange
        var studyGroupId = 1;
        var userId = 1;
        _mockStudyGroupRepository
            .Setup(repo => repo.JoinStudyGroup(studyGroupId, userId))
            .ReturnsAsync(true);

        // Act
        var result = await _studyGroupController.JoinStudyGroup(studyGroupId, userId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task LeaveStudyGroup_ValidInput_Success()
    {
        // Arrange
        var studyGroupId = 1;
        var userId = 1;
        _mockStudyGroupRepository
            .Setup(repo => repo.LeaveStudyGroup(studyGroupId, userId))
            .ReturnsAsync(true);

        // Act
        var result = await _studyGroupController.LeaveStudyGroup(studyGroupId, userId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task GetStudyGroups_ReturnsListOfStudyGroups()
    {
        // Arrange
        var mockStudyGroups = new List<StudyGroup> { new StudyGroup {  } };
        _mockStudyGroupRepository
            .Setup(repo => repo.GetStudyGroups())
            .ReturnsAsync(mockStudyGroups);

        // Act
        var result = await _studyGroupController.GetStudyGroups() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<StudyGroup>>(result.Value);
    }

    [Test]
    public async Task SearchStudyGroups_BySubject_ReturnsFilteredStudyGroups()
    {
        // Arrange
        var subject = "Physics";
        var mockStudyGroups = new List<StudyGroup>
        {
            new StudyGroup { Subject = "Physics",  },
            new StudyGroup { Subject = "Chemistry",  }
        };
        _mockStudyGroupRepository
            .Setup(repo => repo.SearchStudyGroups(subject))
            .ReturnsAsync(mockStudyGroups.Where(sg => sg.Subject == subject).ToList());

        // Act
        var result = await _studyGroupController.SearchStudyGroups(subject) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<StudyGroup>>(result.Value);
        var filteredStudyGroups = (List<StudyGroup>)result.Value;
        Assert.IsTrue(filteredStudyGroups.All(sg => sg.Subject == subject));
    }

    [Test]
    public async Task GetStudyGroups_SortedByCreationDate_ReturnsSortedStudyGroups()
    {
        // Arrange
        var mockStudyGroups = new List<StudyGroup>
        {
            new StudyGroup { CreationDate = DateTime.Now.AddDays(-3),  },
            new StudyGroup { CreationDate = DateTime.Now.AddDays(-1),  },
            new StudyGroup { CreationDate = DateTime.Now.AddDays(-2),  }
        };
        _mockStudyGroupRepository
            .Setup(repo => repo.GetStudyGroups())
            .ReturnsAsync(mockStudyGroups.OrderBy(sg => sg.CreationDate).ToList());

        // Act
        var result = await _studyGroupController.GetStudyGroups() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<StudyGroup>>(result.Value);
        var sortedStudyGroups = (List<StudyGroup>)result.Value;
        Assert.IsTrue(sortedStudyGroups.SequenceEqual(mockStudyGroups.OrderBy(sg => sg.CreationDate)));
    }

}