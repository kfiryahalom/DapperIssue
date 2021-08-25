using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;
using Moq;
using Moq.Dapper;
using Xunit;

namespace core21
{
    public class UnitTest1
    {
        [Fact]
        public async Task When_TestIssueWithDapperDateTime()
        {
            // Arrange
            var mockConnection = new Mock<DbConnection>();
            var expectedResponse = DateTime.UtcNow;

            var expectedResponseList = new List<DateTime> { expectedResponse };

            mockConnection.SetupDapperAsync(x => x.QueryAsync<DateTime>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(expectedResponseList);

            var actualResponse = (await mockConnection.Object.QueryAsync<DateTime>("someSql", 1, null, null, null)).FirstOrDefault();

            // Assert
            expectedResponse.Should().BeCloseTo(actualResponse,  TimeSpan.FromMinutes(1));
        }
    }
}
