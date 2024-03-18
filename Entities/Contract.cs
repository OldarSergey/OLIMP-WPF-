using System;
using System.Collections.Generic;
using System.Text;

namespace OLIMP.Entities
{
    public class Contract
    {

        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime PlannedDateReturn {  get; set; }
        public decimal Deposit {  get; set; }
        public decimal Cost { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public decimal Penalty { get; set; }
        public decimal ReturnDeposit { get; set; }

        public Guid ClientId { get; set; }
        public Guid UserId { get; set; }

        public Client Client { get; set; }
        public User User { get; set; }

        public Contract()
        {
            
        }

        public Contract
        (   DateTime date,
            DateTime issueDate, 
            DateTime plannedDateReturn,
            decimal deposit, 
            decimal cost, 
            DateTime actualReturnDate, 
            decimal penalty, 
            decimal returnDeposit, 
            Guid clientId, 
            Guid userId)
        {
            Date = date;
            IssueDate = issueDate;
            PlannedDateReturn = plannedDateReturn;
            Deposit = deposit;
            Cost = cost;
            ActualReturnDate = actualReturnDate;
            Penalty = penalty;
            ReturnDeposit = returnDeposit;
            ClientId = clientId;
            UserId = userId;
        }

        
    }
}
