﻿using NUnit.Framework;
using InlineScheduler.Advanced;
using System.Threading.Tasks;
using System;
using FluentAssertions;
using System.Threading;

namespace InlineScheduler.Tests.Advanced
{
    public class When_work_item_is_run
    {
        WorkItem _item;

        [TestFixtureSetUp]
        public void Given_work_item() 
        {
            var factory = new WorkItemFactory();            
            _item = factory.Create();            

            // Lets run it
            _item.UpdateState();
            _item.Run();

            // and .. wait until it will complete
            while (true) 
            {
                if (_item.Status == WorkStatus.Pending) 
                {
                    break;
                }
                Thread.Sleep(200);
            }
        }

        [Test]
        public void Should_be_spending() 
        {
            _item.Status.Should().Be(WorkStatus.Pending);                       
        }

        [Test]
        public void Should_fill_previous_runs()
        {
            _item.PreviousRuns.Should().NotBeEmpty();
        }        
    }
}