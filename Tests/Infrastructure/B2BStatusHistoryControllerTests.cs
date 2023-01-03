using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Domain.B2BMasters;
using Domain.B2BMasters.Args;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.StateMachine.Constants;
using Infrastructure.StateMachine.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tests.TestCaseSources;

namespace B2BApi.Tests.ServiceTests
{
    [TestFixture]
    public class B2BStatusHistoryControllerTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public async Task CreateB2BStatusHistory_WhenNextStatusIsInvoicedCreated_ShouldNotCreateNewOne()
        {
            var b2BDataContextOptions = new DbContextOptionsBuilder<B2BContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var b2BDataContext = new B2BContext(b2BDataContextOptions);

            var b2BMaster = await CreateDummyB2BMasterEntity(b2BDataContext);

            var b2BStatusHistory = new B2BStatusHistory
            {
                B2BId = b2BMaster.Id,
                StatusId = (int)B2BStatusTypeNames.InvoiceCreated,
                CreationDate = DateTime.Now
            };
 
            b2BDataContext.B2BStatusHistories.Add(b2BStatusHistory);
            await b2BDataContext.SaveChangesAsync();

            var sut = new B2BStatusHistoryService(b2BDataContext);

            var historyResponse = await sut.AppendNew(b2BMaster.Id, (int)B2BStatusTypeNames.InvoiceReady);

            historyResponse.HasError.Should().BeTrue();
            b2BDataContext.B2BStatusHistories.Count().Should().Be(1);
            historyResponse.Messages.FirstOrDefault().Content.Should()
                .Be(StateMachineMessages.CantMoveNextFromInvoiceCreatedErrorMessage);
        }

        private async Task<B2BMaster> CreateDummyB2BMasterEntity(B2BContext b2BDataContext)
        {
            var b2BMasterArg = _fixture.Build<CreateB2BMasterArg>()
                .Without(x=> x.CarrierType)
                .Create();
            var b2BMaster = B2BMaster.Create(b2BMasterArg);
            b2BDataContext.B2BMasters.Add(b2BMaster);
            await b2BDataContext.SaveChangesAsync();
            return b2BMaster;
        }

        [Test, TestCaseSource(typeof(B2BStatusHistoryTestCases), "CancelingCases"),
         TestCaseSource(typeof(B2BStatusHistoryTestCases), "PickingWaitingCases"),
         TestCaseSource(typeof(B2BStatusHistoryTestCases), "PickingStartedCases"),
         TestCaseSource(typeof(B2BStatusHistoryTestCases), "WeighingCompletedCases"),
         TestCaseSource(typeof(B2BStatusHistoryTestCases), "WaitingShipmentCases"),
         TestCaseSource(typeof(B2BStatusHistoryTestCases), "ShippedCases"),
         TestCaseSource(typeof(B2BStatusHistoryTestCases), "InvoiceReadyCases"),
         TestCaseSource(typeof(B2BStatusHistoryTestCases), "InvoicingCases")]
        public async Task CreateB2BStatusHistory_WhenStateMachineMoves_ShouldAllowOrDenyAccordingToParams(
            int fromStatusId, int toStatusId, bool shouldError)
        {
            var b2BDataContextOptions = new DbContextOptionsBuilder<B2BContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var b2BDataContext = new B2BContext(b2BDataContextOptions);
            var b2BMaster = await CreateDummyB2BMasterEntity(b2BDataContext);
            var b2BStatusHistory = new B2BStatusHistory
            {
                B2BId = b2BMaster.Id,
                StatusId = fromStatusId,
                CreationDate = DateTime.Now
            };
            
            b2BDataContext.B2BStatusHistories.Add(b2BStatusHistory);
            await b2BDataContext.SaveChangesAsync();
            var sut = new B2BStatusHistoryService(b2BDataContext);
            var historyResponse = await sut.AppendNew(b2BMaster.Id, toStatusId);

            if (!shouldError)
            {
                historyResponse.HasError.Should().Be(false);
                var shouldCountBe = 2;
                b2BDataContext.B2BStatusHistories.Count().Should().Be(shouldCountBe);
            }
            else
            {
                var msg = string.Format(StateMachineMessages.StatusMustBeMoveLinearErrorF,
                    (B2BStatusTypeNames)fromStatusId,
                    (B2BStatusTypeNames)toStatusId);
                historyResponse.Messages.FirstOrDefault().Content.Should().Be(msg);
                b2BDataContext.B2BStatusHistories.Count().Should().Be((int)b2BMaster.Id);
                historyResponse.HasError.Should().Be(true);
            }
        }

        [Test, TestCaseSource(typeof(B2BStatusHistoryTestCases), "FirstStatusErrorCases")]
        public async Task CreateB2BStatusHistory_WhenFirstStatusIdIsNotSuitable_ShouldReturnError(int statusId)
        {
            var b2BDataContextOptions = new DbContextOptionsBuilder<B2BContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var b2BDataContext = new B2BContext(b2BDataContextOptions);

            var b2BMaster = await CreateDummyB2BMasterEntity(b2BDataContext);
             
            var sut = new B2BStatusHistoryService(b2BDataContext);

            var historyResponse = await sut.AppendNew(b2BMaster.Id, statusId);
            historyResponse.HasError.Should().BeTrue();
            b2BDataContext.B2BStatusHistories.Count().Should().Be(0);
            historyResponse.Messages.FirstOrDefault()?.Content.Should()
                .Be(StateMachineMessages.FirstStatusShouldBePickingWaitingMessage);
        }


        [Test, TestCaseSource(typeof(B2BStatusHistoryTestCases), "UnknownStatusErrorCases")]
        public async Task CreateB2BStatusHistory_WhenStatusIdIsUnknown_ShouldReturnError(int statusId)
        {
            var b2BDataContextOptions = new DbContextOptionsBuilder<B2BContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var b2BDataContext = new B2BContext(b2BDataContextOptions);
            
            var b2BMaster = await CreateDummyB2BMasterEntity(b2BDataContext);

            var b2BStatusHistory = new B2BStatusHistory
            {
                B2BId = b2BMaster.Id,
                StatusId = (int)B2BStatusTypeNames.PickingWaiting,
                CreationDate = DateTime.Now
            };
  
            b2BDataContext.B2BStatusHistories.Add(b2BStatusHistory);
            await b2BDataContext.SaveChangesAsync();

            var sut = new B2BStatusHistoryService(b2BDataContext);

            var historyResponse = await sut.AppendNew(b2BMaster.Id, statusId);

            historyResponse.HasError.Should().BeTrue();
            b2BDataContext.B2BStatusHistories.Count().Should().Be((int)b2BMaster.Id);
            historyResponse.Messages.FirstOrDefault()?.Content.Should().Be(StateMachineMessages.UnKnownStatus);
        }
    }
}