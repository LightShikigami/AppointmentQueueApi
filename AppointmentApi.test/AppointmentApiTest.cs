using Xunit;
using AppointmentQueueApi.Services;
using AppointmentQueueApi.Data;
using AppointmentQueueApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppointmentApi.test
{
    public class AppointmentApiTest
    {
        private AppDbContext GetInMemoryDbContext() 
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void CreateAppointment_ShouldAssignTokenNumber()
        {
            var dbContext = GetInMemoryDbContext();
            var service = new AppointmentService(dbContext);

            var customer = new Customer
            {
                Name = "test user",
                Phone = "081234567890",
                Email = "user@321.com"
            };

            var requestedDate = new DateTime(2025, 5, 12);

            var appointment = service.CreateAppointment(customer, requestedDate);

            Assert.NotNull(appointment);
            Assert.Equal("20250512-01", appointment.TokenNumber);
            Assert.Equal(requestedDate.Date, appointment.Date);
        }

        [Fact]
        public void CreateAppointment_ShouldSkipWeekend()
        {
            var dbContext = GetInMemoryDbContext();
            var service = new AppointmentService(dbContext);

            var customer = new Customer
            {
                Name = "Libur Sabtu",
                Phone = "08123456789",
                Email = "testlibur@mail.com"
            };

            var requestedDate = new DateTime(2025, 5, 17);

            var appointment = service.CreateAppointment(customer,requestedDate);

            Assert.NotEqual(requestedDate, appointment.Date);
            Assert.NotEqual(DayOfWeek.Saturday, appointment.Date.DayOfWeek);
            Assert.NotEqual(DayOfWeek.Sunday, appointment.Date.DayOfWeek);
        }

        [Fact]
        public void CreateAppointment_ShouldShiftToNextDay_WhenQuotaExceeded()
        {
            var dbContext = GetInMemoryDbContext();
            var service = new AppointmentService(dbContext);
            var requestedDate = new DateTime(2025, 5, 13);

            for (int i = 1; i <= 3; i++)
            {
                var customer = new Customer
                {
                    Name = $"Customer{i}",
                    Phone = $"08123{i}",
                    Email = $"cust{i}@mail.com"
                };

                service.CreateAppointment(customer, requestedDate);
            }

            var customer6 = new Customer
            {
                Name = "Customer4",
                Phone = "081234",
                Email = "cust4@mail.com"
            };


            var appointment = service.CreateAppointment(customer6, requestedDate);

            Assert.NotEqual(requestedDate.Date, appointment.Date); 
        }



    }
}