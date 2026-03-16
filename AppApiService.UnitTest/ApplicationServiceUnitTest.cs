using AppApiService.Domain.ApplicationService;
using AppApiService.UnitTest;
using Microsoft.Extensions.DependencyInjection;

namespace AppApiService.Tests.Domain.ApplicationService
{
    public class ApplicationServiceUnitTest : IClassFixture<StartupFixture>
    {
        private readonly StartupFixture startupFixture;

        public ApplicationServiceUnitTest(StartupFixture startupFixture)
        {
            this.startupFixture = startupFixture;
        }

        private IApplicationService GetApplicationService()
        {
            using var scope = startupFixture.ServiceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IApplicationService>();
        }

        [Fact]
        public void TestOrderStudentNos_WithNormalInput_ShouldReturnCorrectOrder()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 9, 3, 7, 1, 5 };
            var expected = new[] { 1, 9, 3, 7, 5 };

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithEvenNumberOfElements_ShouldReturnCorrectOrder()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 4, 2, 5, 1 };
            var expected = new[] { 1, 5, 2, 4 };

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithOddNumberOfElements_ShouldReturnCorrectOrder()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 8, 3, 6, 1, 9, 4, 7 };
            var expected = new[] { 1, 9, 3, 8, 4, 7, 6 }; // 修正后的正确结果

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithSingleElement_ShouldReturnSameElement()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 42 };
            var expected = new[] { 42 };

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithTwoElements_ShouldReturnCorrectOrder()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 3, 1 };
            var expected = new[] { 1, 3 };

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithEmptyArray_ShouldReturnEmptyArray()
        {
            // Arrange
            var service = GetApplicationService();
            var input = Array.Empty<int>();
            var expected = Array.Empty<int>();

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithNullInput_ShouldThrowException()
        {
            // Arrange
            var service = GetApplicationService();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.OrderStudentNos(null));
            Assert.Equal("studentNos is null !!!!", exception.Message);
        }

        [Fact]
        public void TestOrderStudentNos_WithDuplicateNumbers_ShouldHandleDuplicates()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 3, 1, 3, 2, 3 };
            var expected = new[] { 1, 3, 2, 3, 3 }; // 排序后：[1,2,3,3,3]，交替后：[1,3,2,3,3]

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithNegativeNumbers_ShouldHandleCorrectly()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { -5, 10, -3, 0, 7, -1 };
            var expected = new[] { -5, 10, -3, 7, -1, 0 }; // 排序后：[-5,-3,-1,0,7,10]，交替后：[-5,10,-3,7,-1,0]

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithLargeNumbers_ShouldHandleCorrectly()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 1000, 500, 2000, 100, 1500 };
            var expected = new[] { 100, 2000, 500, 1500, 1000 }; // 排序后：[100,500,1000,1500,2000]，交替后：[100,2000,500,1500,1000]

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithUnsortedInput_ShouldSortFirst()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 10, 1, 8, 3, 6, 2 };
            var expected = new[] { 1, 10, 2, 8, 3, 6 }; // 排序后：[1,2,3,6,8,10]，交替后：[1,10,2,8,3,6]

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithAllSameNumbers_ShouldReturnSameNumbers()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 5, 5, 5, 5 };
            var expected = new[] { 5, 5, 5, 5 }; // 排序后：[5,5,5,5]，交替后：[5,5,5,5]

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_WithMaxMinValues_ShouldHandleCorrectly()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { int.MaxValue, int.MinValue, 0 };
            var expected = new[] { int.MinValue, int.MaxValue, 0 }; // 排序后：[int.MinValue,0,int.MaxValue]，交替后：[int.MinValue,int.MaxValue,0]

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOrderStudentNos_PreservesAllOriginalElements()
        {
            // Arrange
            var service = GetApplicationService();
            var input = new[] { 9, 3, 7, 1, 5, 2, 8, 4, 6 };

            // Act
            var result = service.OrderStudentNos(input);

            // Assert
            Assert.Equal(input.Length, result.Length);

            // 验证所有原始元素都存在（顺序无关）
            var sortedInput = input.OrderBy(x => x).ToArray();
            var sortedResult = result.OrderBy(x => x).ToArray();
            Assert.Equal(sortedInput, sortedResult);
        }
    }
}