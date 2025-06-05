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
        var request = new TaskItemRequest { Title = "Test", Status = "Pending" };
        var taskItem = new TaskItem { Id = 1, Title = "Test", Status = "Pending", DueDate = DateTime.Now };
        var response = new TaskItemResponse { Id = 1, Title = "Test", Status = "Pending", DueDate = DateTime.Now };

        _mapperMock.Setup(m => m.Map<TaskItem>(request)).Returns(taskItem);
        _repoMock.Setup(r => r.Create(taskItem)).ReturnsAsync(taskItem);
        _mapperMock.Setup(m => m.Map<TaskItemResponse>(taskItem)).Returns(response);

        var result = await _service.Create(request);

        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsTaskItemResponse()
    {
        var taskItem = new TaskItem { Id = 1, Title = "Test", Status = "Pending", DueDate = DateTime.Now };
        var response = new TaskItemResponse { Id = 1, Title = "Test", Status = "Pending", DueDate = DateTime.Now };

        _repoMock.Setup(r => r.Delete(1)).ReturnsAsync(taskItem);
        _mapperMock.Setup(m => m.Map<TaskItemResponse>(taskItem)).Returns(response);

        var result = await _service.Delete(1);

        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsListOfTaskItemResponse()
    {
        var taskItems = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test", Status = "Pending", DueDate = DateTime.Now } };
        var responses = new List<TaskItemResponse> { new TaskItemResponse { Id = 1, Title = "Test", Status = "Pending", DueDate = DateTime.Now } };

        _repoMock.Setup(r => r.GetAll()).ReturnsAsync(taskItems);
        _mapperMock.Setup(m => m.Map<List<TaskItemResponse>>(taskItems)).Returns(responses);

        var result = await _service.GetAll();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllByDueDate_ReturnsListOfTaskItemResponse()
    {
        var date = DateTime.Today;
        var taskItems = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test", Status = "Pending", DueDate = date } };
        var responses = new List<TaskItemResponse> { new TaskItemResponse { Id = 1, Title = "Test", Status = "Pending", DueDate = date } };

        _repoMock.Setup(r => r.GetAllByDueDate(date)).ReturnsAsync(taskItems);
        _mapperMock.Setup(m => m.Map<List<TaskItemResponse>>(taskItems)).Returns(responses);

        var result = await _service.GetAllByDueDate(date);

        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllByStatus_ValidStatus_ReturnsListOfTaskItemResponse()
    {
        var status = "Pending";
        var taskItems = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test", Status = status, DueDate = DateTime.Now } };
        var responses = new List<TaskItemResponse> { new TaskItemResponse { Id = 1, Title = "Test", Status = status, DueDate = DateTime.Now } };

        _repoMock.Setup(r => r.GetAllByStatus(status)).ReturnsAsync(taskItems);
        _mapperMock.Setup(m => m.Map<List<TaskItemResponse>>(taskItems)).Returns(responses);

        var result = await _service.GetAllByStatus(status);

        Assert.Single(result);
    }

    [Fact]
    public async Task Update_ValidRequest_ReturnsTaskItemResponse()
    {
        var request = new TaskItemRequest { Title = "Updated", Status = "Done" };
        var taskItem = new TaskItem { Id = 1, Title = "Updated", Status = "Done", DueDate = DateTime.Now };
        var response = new TaskItemResponse { Id = 1, Title = "Updated", Status = "Done", DueDate = DateTime.Now };

        _mapperMock.Setup(m => m.Map<TaskItem>(request)).Returns(taskItem);
        _repoMock.Setup(r => r.Update(1, taskItem)).ReturnsAsync(taskItem);
        _mapperMock.Setup(m => m.Map<TaskItemResponse>(taskItem)).Returns(response);

        var result = await _service.Update(1, request);

        Assert.Equal(response.Id, result.Id);
    }
}
