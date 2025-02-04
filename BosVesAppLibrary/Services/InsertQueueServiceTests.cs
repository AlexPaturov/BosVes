using System.Threading.Tasks;
using Dapper;
using Moq;
using Xunit;

namespace BosVesAppLibrary.Services;

public class InsertQueueServiceTests
{
   private readonly Mock<GpriData> _mockDbOneGpri;
   private readonly InsertQueueService _insertQueueService;

   public InsertQueueServiceTests()
   {
      _mockDbOneGpri = new Mock<GpriData>();
      _insertQueueService = new InsertQueueService(_mockDbOneGpri.Object);
   }

   [Fact]
   public async Task Insert_SingleRecord_ShouldInsertSuccessfully()
   {
      var testVagon = new GpriModel { DT = DateTime.Parse("2025-01-25"), VR = TimeSpan.Parse("12:00:00"), NVAG = "1001", VESY = 500 };

      _mockDbOneGpri
          .Setup(db => db.InsNew(testVagon))
          .ReturnsAsync(1); // Симулируем успешную вставку с ID=1

      int insertedId = await _insertQueueService.EnqueueInsertAsync(testVagon);

      Assert.Equal(1, insertedId);
      _mockDbOneGpri.Verify(db => db.InsNew(testVagon), Times.Once);
   }

   [Fact]
   public async Task Insert_DuplicateRecord_ShouldReturnExistingId()
   {
      var testVagon = new GpriModel { DT = DateTime.Parse("2025-01-25"), VR = TimeSpan.Parse("12:00:00"), NVAG = "1001", VESY = 500 };

      _mockDbOneGpri
          .Setup(db => db.InsNew(testVagon))
          .ReturnsAsync(1);

      _mockDbOneGpri
          .Setup(db =>  db.CreateConnection().ExecuteScalarAsync<int?>(
              It.IsAny<string>(), It.IsAny<object>(), null, null, default))
          .ReturnsAsync(1); // Симулируем, что запись уже существует

      int insertedId = await _insertQueueService.EnqueueInsertAsync(testVagon);

      Assert.Equal(1, insertedId);
      _mockDbOneGpri.Verify(db => db.InsNew(testVagon), Times.Never); // Вставка не должна выполняться второй раз
   }

   [Fact]
   public async Task Insert_MultipleParallelRequests_ShouldInsertOnlyOnce()
   {
      var testVagon = new GpriModel { DT = DateTime.Parse("2025-01-25"), VR = TimeSpan.Parse("12:00:00"), NVAG = "1001", VESY = 500 };

      _mockDbOneGpri
          .Setup(db => db.InsNew(testVagon))
          .ReturnsAsync(1);

      Task<int> task1 = _insertQueueService.EnqueueInsertAsync(testVagon);
      Task<int> task2 = _insertQueueService.EnqueueInsertAsync(testVagon);
      Task<int> task3 = _insertQueueService.EnqueueInsertAsync(testVagon);

      int[] results = await Task.WhenAll(task1, task2, task3);

      Assert.All(results, id => Assert.Equal(1, id)); // Все должны вернуть один ID
      _mockDbOneGpri.Verify(db => db.InsNew(testVagon), Times.Once); // Только одна вставка
   }

}
