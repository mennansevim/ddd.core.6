using System.Collections.Generic;
using Domain.B2BMasters;
using NUnit.Framework;

namespace Tests.TestCaseSources
{
    public class B2BStatusHistoryTestCases
    {
        private static IEnumerable<TestCaseData> CancelingCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.PickingWaiting, true);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.PickingStarted, true);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.WeighingCompleted, true);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.Canceling, false);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.WaitingShipment, true);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.Shipped, true);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.InvoiceReady, true);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.Invoicing, true);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling, B2BStatusTypeNames.InvoiceCreated, true);
            }
        }

        private static IEnumerable<TestCaseData> FirstStatusErrorCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.PickingStarted);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted);
                yield return new TestCaseData(B2BStatusTypeNames.Canceling);
                yield return new TestCaseData(B2BStatusTypeNames.Cancelled);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceCreated);
            }
        }

        private static IEnumerable<TestCaseData> UnknownStatusErrorCases
        {
            get
            {
                yield return new TestCaseData(121);
                yield return new TestCaseData(220);
                yield return new TestCaseData(324);
                yield return new TestCaseData(4);
                yield return new TestCaseData(521);
                yield return new TestCaseData(624);
                yield return new TestCaseData(72);
                yield return new TestCaseData(-1);
            }
        }

        private static IEnumerable<TestCaseData> PickingWaitingCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.PickingWaiting, B2BStatusTypeNames.PickingWaiting,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.PickingWaiting, B2BStatusTypeNames.PickingStarted,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.PickingWaiting, B2BStatusTypeNames.WeighingCompleted,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.PickingWaiting, B2BStatusTypeNames.Canceling, false);
            }
        }

        private static IEnumerable<TestCaseData> PickingStartedCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.PickingStarted, B2BStatusTypeNames.PickingWaiting,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.PickingStarted, B2BStatusTypeNames.PickingStarted,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.PickingStarted, B2BStatusTypeNames.WeighingCompleted,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.PickingStarted, B2BStatusTypeNames.Canceling, false);
            }
        }

        private static IEnumerable<TestCaseData> WeighingCompletedCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.PickingWaiting,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.PickingStarted,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted,
                    B2BStatusTypeNames.WeighingCompleted, false);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.Canceling,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.Cancelled,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.WaitingShipment,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.InvoiceReady,
                    true);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.Invoicing,
                    true);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.InvoiceCreated,
                    true);
                yield return new TestCaseData(B2BStatusTypeNames.WeighingCompleted, B2BStatusTypeNames.Shipped,
                    true);
            }
        }

        private static IEnumerable<TestCaseData> WaitingShipmentCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.PickingStarted,
                    true);
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.WeighingCompleted,
                    true);
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.Canceling, true);
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.Cancelled, true);
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.WaitingShipment,
                    false);
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.Shipped, false);
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.InvoiceReady,
                    true);
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.Invoicing, true);
                yield return new TestCaseData(B2BStatusTypeNames.WaitingShipment, B2BStatusTypeNames.InvoiceCreated,
                    true);
            }
        }

        private static IEnumerable<TestCaseData> ShippedCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.PickingWaiting, true);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.PickingStarted, true);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.WeighingCompleted, true);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.Canceling, true);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.Cancelled, true);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.WaitingShipment, true);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.Shipped, false);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.InvoiceReady, false);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.Invoicing, true);
                yield return new TestCaseData(B2BStatusTypeNames.Shipped, B2BStatusTypeNames.InvoiceCreated, true);
            }
        }

        private static IEnumerable<TestCaseData> InvoiceReadyCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.PickingWaiting, true);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.PickingStarted, true);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.WeighingCompleted,
                    true);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.Canceling, true);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.Cancelled, true);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.WaitingShipment,
                    true);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.Shipped, true);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.InvoiceReady, false);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.Invoicing, false);
                yield return new TestCaseData(B2BStatusTypeNames.InvoiceReady, B2BStatusTypeNames.InvoiceCreated, true);
            }
        }

        private static IEnumerable<TestCaseData> InvoicingCases
        {
            get
            {
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.PickingWaiting, true);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.PickingStarted, true);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.WeighingCompleted, true);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.Canceling, true);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.Cancelled, true);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.WaitingShipment, true);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.Shipped, true);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.InvoiceReady, true);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.Invoicing, false);
                yield return new TestCaseData(B2BStatusTypeNames.Invoicing, B2BStatusTypeNames.InvoiceCreated, false);
            }
        }
    }
}