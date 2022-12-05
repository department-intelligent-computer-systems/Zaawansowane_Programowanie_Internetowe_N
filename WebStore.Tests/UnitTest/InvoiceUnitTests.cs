using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DAL.EF;
using WebStore.Model.DataModels;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using Xunit;
namespace WebStore.Tests.UnitTests
{
    public class InvoiceServiceUnitTests : BaseUnitTests
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceServiceUnitTests(ApplicationDbContext dbContext,
        IInvoiceService invoiceService) : base(dbContext)
        {
            _invoiceService = invoiceService;
        }

        [Fact]
        public void GetInvoiceTest()
        {
            var invoice = _invoiceService.GetInvoice(i => i.totalPrice == 10);
            Assert.NotNull(invoice);
        }

        [Fact]
        public void GetMultipleOrdersTest()
        {
            var invoices = _invoiceService.GetInvoices(i => i.invoiceID >= 1 && i.invoiceID <= 2);
            Assert.NotNull(invoices);
            Assert.NotEmpty(invoices);
            Assert.Equal(2, invoices.Count());
        }

        [Fact]

        public void GetAllInvoicesTest()
        {
            var invoices = _invoiceService.GetInvoices();
            Assert.NotNull(invoices);
            Assert.NotEmpty(invoices);
            Assert.Equal(invoices.Count(), invoices.Count());
        }

        [Fact]

        public void AddNewInvoiceTest()
        {
            var newInvoiceVm = new AddOrUpdateInvoiceVm()
            {
                invoiceID = 3,
                totalPrice = 50,
                invoiceDate = new DateTime()
            };
            var createdInvoice = _invoiceService.AddOrUpdateInvoice(newInvoiceVm);
            Assert.NotNull(createdInvoice);
            Assert.Equal(3, createdInvoice.invoiceID);

        }

        [Fact]

        public void UpdateInvoiceTest()
        {
            var updateInvoiceVm = new AddOrUpdateInvoiceVm()
            {
                invoiceID = 4,
                totalPrice = 20,
                invoiceDate = new DateTime()
            };
            var editedInvoiceVm = _invoiceService.AddOrUpdateInvoice(updateInvoiceVm);
            Assert.NotNull(editedInvoiceVm);
            Assert.Equal(4, editedInvoiceVm.invoiceID);
            Assert.Equal(20, editedInvoiceVm.totalPrice);
        }
    }
}