﻿using lab.DAL.Entities;
using lab.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace lab.Tests
{
    public class ListViewModelTests
    {
        [Fact]
        public void ListViewModelCountsPages()
        {
            // Act
            var model = ListViewModel<MusInstrument>.GetModel(TestData.GetInstrumentsList(), 1, 3);

            // Assert
            Assert.Equal(2, model.TotalPages);
        }

        [Theory]
        [MemberData(memberName: nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<MusInstrument>.GetModel(TestData.GetInstrumentsList(), page, 3);

            // Assert
            Assert.Equal(qty, model.Count);

        }

        [Theory]
        [MemberData(memberName: nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ListViewModelHasCorrectData(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<MusInstrument>.GetModel(TestData.GetInstrumentsList(), page, 3);

            // Assert
            Assert.Equal(id, model[0].Id);
        }
    }
}
