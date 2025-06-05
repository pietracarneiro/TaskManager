using Xunit;
using Moq;
using AutoMapper;
using TaskManager.Services;
using TaskManager.Repositories.Interfaces;
using TaskManager.Models;
using TaskManager.DTOs.Requests;
using TaskManager.DTOs.Responses;

public class TaskItemServiceTests
{
    private readonly Mock<ITaskItemRepository> _repoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly TaskItemService _service;

    public TaskItemServiceTests()
    {
        _repoMock = new Mock<ITaskItemRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new TaskItemService(_repoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Create_ValidRequest_ReturnsTaskItemResponse()
    {
        var request = new TaskItemRequest { Title = "Test", Status = "Pendente" };
        var taskItem = new TaskItem { Id = 1, Title = "Test", Status = "Pendente", DueDate = DateTime.Now };
        var response = new TaskItemResponse { Id = 1, Title = "Test", Status = "Pendente", DueDate = DateTime.Now };

        _mapperMock.Setup(m => m.Map<TaskItem>(request)).Returns(taskItem);
        _repoMock.Setup(r => r.Create(taskItem)).ReturnsAsync(taskItem);
        _mapperMock.Setup(m => m.Map<TaskItemResponse>(taskItem)).Returns(response);

        var result = await _service.Create(request);

        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsTaskItemResponse()
    {
        var taskItem = new TaskItem { Id = 1, Title = "Test", Status = "Pendente", DueDate = DateTime.Now };
        var response = new TaskItemResponse { Id = 1, Title = "Test", Status = "Pendente", DueDate = DateTime.Now };

        _repoMock.Setup(r => r.Delete(1)).ReturnsAsync(taskItem);
        _mapperMock.Setup(m => m.Map<TaskItemResponse>(taskItem)).Returns(response);

        var result = await _service.Delete(1);

        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsListOfTaskItemResponse()
    {
        var taskItems = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test", Status = "Pendente", DueDate = DateTime.Now } };
        var responses = new List<TaskItemResponse> { new TaskItemResponse { Id = 1, Title = "Test", Status = "Pendente", DueDate = DateTime.Now } };

        _repoMock.Setup(r => r.GetAll()).ReturnsAsync(taskItems);
        _mapperMock.Setup(m => m.Map<List<TaskItemResponse>>(taskItems)).Returns(responses);

        var result = await _service.GetAll();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllByDueDate_ReturnsListOfTaskItemResponse()
    {
        var date = DateTime.Today;
        var taskItems = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test", Status = "Pendente", DueDate = date } };
        var responses = new List<TaskItemResponse> { new TaskItemResponse { Id = 1, Title = "Test", Status = "Pendente", DueDate = date } };

        _repoMock.Setup(r => r.GetAllByDueDate(date)).ReturnsAsync(taskItems);
        _mapperMock.Setup(m => m.Map<List<TaskItemResponse>>(taskItems)).Returns(responses);

        var result = await _service.GetAllByDueDate(date);

        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllByStatus_ValidStatus_ReturnsListOfTaskItemResponse()
    {
        var status = "Pendente";
        var taskItems = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test", Status = status, DueDate = DateTime.Now } };
        var responses = new List<TaskItemResponse> { new TaskItemResponse { Id = 1, Title = "Test", Status = status, DueDate = DateTime.Now } };

        _repoMock.Setup(r => r.GetAllByStatus(status)).ReturnsAsync(taskItems);
        _mapperMock.Setup(m => m.Map<List<TaskItemResponse>>(taskItems)).Returns(responses);

        var result = await _service.GetAllByStatus(status);

        Assert.Single(result);
    }

    [Fact]
    public async Task Update_ShouldUpdateFieldsAndReturnMappedResponse()
    {
        // Arrange
        var taskId = 1;
        var existingTask = new TaskItem
        {
            Id = taskId,
            Title = "Old Title",
            Description = "Old Description",
            DueDate = DateTime.Today,
            Status = "Pending"
        };

        var updatedRequest = new TaskItemRequest
        {
            Title = "New Title",
            Description = null,
            DueDate = DateTime.Today.AddDays(5),
            Status = "Done"
        };

        var updatedTask = new TaskItem
        {
            Id = taskId,
            Title = "New Title",
            Description = "Old Description",
            DueDate = updatedRequest.DueDate.Value,
            Status = "Done"
        };

        var response = new TaskItemResponse { Id = taskId, Title = "New Title" };

        _repoMock.Setup(r => r.GetAll()).ReturnsAsync(new List<TaskItem> { existingTask });
        _repoMock.Setup(r => r.Update(taskId, It.IsAny<TaskItem>())).ReturnsAsync(updatedTask);
        _mapperMock.Setup(m => m.Map<TaskItemResponse>(It.IsAny<TaskItem>())).Returns(response);

        // Act
        var result = await _service.Update(taskId, updatedRequest);

        // Assert
        Assert.Equal(response.Id, result.Id);
        Assert.Equal("New Title", result.Title);
        _repoMock.Verify(r => r.Update(taskId, It.Is<TaskItem>(t =>
            t.Title == "New Title" &&
            t.Description == "Old Description" &&
            t.Status == "Done"
        )), Times.Once);
    }
}
